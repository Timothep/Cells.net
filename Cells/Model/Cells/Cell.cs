﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.Model.Mapping;
using Cells.Properties;
using Cells.Utils;
using Cells.Interfaces;
using Ninject;
using Ninject.Modules;

namespace Cells.Model.Cells
{
    public class Cell: ICell
    {
        private readonly IWorld world;
        private IBrain Brain { get; set; }
        public ICoordinates Position { get; set; }
        public Int16 Life { get; set; }
        public DisplayQualifier Team { get; set; }
        private AvailableActions cellPreviousAction = AvailableActions.NONE;
        private Boolean carryingWeight = false;

        /// <summary>
        /// Cell constructor
        /// </summary> 
        public Cell()
        {
            world = NinjectGlobalKernel.GlobalKernel.Get<IWorld>();
            IKernel cellKernel = new StandardKernel(new CellModule());
            Position = cellKernel.Get<ICoordinates>();
        }

        /// <summary>
        /// Set life
        /// </summary>
        /// <param name="life"></param>
        public void SetLife(Int16 life)
        {
            this.Life = life;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        public void SetTeam(DisplayQualifier team)
        {
            this.Team = team;
        }

        /// <summary>
        /// Function triggering the brain to chose the next action
        /// </summary>
        /// <returns>The chose action</returns>
        public CellAction Think()
        {
            return this.Brain.ChooseNextAction();
        }

        /// <summary>
        /// This function executes the action passed on by the brain
        /// (For gameplay perspective the brain should not be able to control the cell himself ;)
        /// </summary>
        /// <param name="action">The action to apply</param>
        public void Do(CellAction action)
        {
            this.cellPreviousAction = action.GetAction();

            switch (action.GetAction())
            {
                case AvailableActions.MOVELEFT:
                    Move(-1,0);
                    break;
                case AvailableActions.MOVERIGHT:
                    Move(1, 0);
                    break;
                case AvailableActions.MOVEUP:
                    Move(0, -1);
                    break;
                case AvailableActions.MOVEDOWN:
                    Move(0, 1);
                    break;
                case AvailableActions.DROP:
                    DropEarth();
                    break;
                case AvailableActions.LIFT:
                    LiftEarth();
                    break;
                case AvailableActions.SPLIT:
                    Split();
                    break;
                case AvailableActions.DIE:
                    Die();
                    break;
                case AvailableActions.EAT:
                    Eat(action.GetOffsetToTarget());
                    break;
                case AvailableActions.ATTACK:
                    throw new NotImplementedException();
                    break;
                case AvailableActions.NONE:
                    break;
                default:
                    throw new NotImplementedException();
            }
            return;
        }

        /// <summary>
        /// Tries to eat ressources present at the given coordinates
        /// </summary>
        /// <param name="offsetToTarget"></param>
        private void Eat(IOffsetVector offsetToTarget)
        {
            if (offsetToTarget == null)
                return;

            ICoordinates coordinates = new Coordinates(this.Position.X, this.Position.Y);
            coordinates.X += offsetToTarget.X;
            coordinates.Y += offsetToTarget.Y;

            Int16 ressourcesLeft = this.world.GetAmountOfRessourcesLeft(coordinates);
            Int16 lifeBonus = ressourcesLeft < Settings.Default.MaxEatingPerRoundQuantity ? ressourcesLeft : Settings.Default.MaxEatingPerRoundQuantity;

            if (lifeBonus > 0)
            {
                this.world.ReduceRessources(coordinates, lifeBonus);
                this.IncreaseLife(lifeBonus);
            }
        }

        /// <summary>
        /// Function returning the previous action that the cell did
        /// </summary>
        /// <returns>The last CellAction the cell did</returns>
        public CellAction GetPreviousAction()
        {
            return new CellAction(cellPreviousAction);
        }

        /// <summary>
        /// A cell is very much blind. 
        /// When it senses its surroundings, it can see at most a square around itself
        /// </summary>
        /// <returns>A MapView describing its immediate surroundings</returns>
        public SurroundingView Sense()
        {
            return world.GetSurroundingsView(this);
        }

        /// <summary>
        /// Function returning a cell action indicating in which direction the cell should move to approach the given coordinates
        /// </summary>
        /// <param name="coordinates">The target coordinates</param>
        /// <returns>A cell action indicating how the cell should move to get there</returns>
        public AvailableActions GetRelativeMovment(ICoordinates coordinates)
        {
            var actions = new List<AvailableActions>();

            if (coordinates.Y > this.Position.Y)
                actions.Add(AvailableActions.MOVEDOWN);

            if (coordinates.Y < this.Position.Y)
                actions.Add(AvailableActions.MOVEUP);
            
            if (coordinates.X < this.Position.X)
                actions.Add(AvailableActions.MOVELEFT);
            
            if (coordinates.X > this.Position.X)
                actions.Add(AvailableActions.MOVERIGHT);
            
            if (actions.Count > 0)
                return actions[RandomGenerator.GetRandomInt32(actions.Count)];

            return AvailableActions.NONE;
        }

        /// <summary>
        /// Function returning a cell action indicating in which direction the cell should move to approach the given coordinates
        /// </summary>
        /// <param name="coordinates">The target coordinates</param>
        /// <returns>A cell action indicating how the cell should move to get there</returns>
        public AvailableActions GetRelativeMovment(IOffsetVector offsetVector)
        {
            var actions = new List<AvailableActions>();

            if (offsetVector.Y > 0)
                actions.Add(AvailableActions.MOVEDOWN);

            if (offsetVector.Y < 0)
                actions.Add(AvailableActions.MOVEUP);

            if (offsetVector.X < 0)
                actions.Add(AvailableActions.MOVELEFT);

            if (offsetVector.X > 0)
                actions.Add(AvailableActions.MOVERIGHT);

            if (actions.Count > 0)
                return actions[RandomGenerator.GetRandomInt32(actions.Count)];

            return AvailableActions.NONE;
        }

        /// <summary>
        /// Kills the cell
        /// </summary>
        public void Die()
        {
            DropAllRessources();
            DropEarth();
            UnregisterCell();
        }

        /// <summary>
        /// Gets the qualifier of the team the cell belongs to
        /// </summary>
        /// <returns>The qualifier of the team</returns>
        internal DisplayQualifier GetTeamQualifier()
        {
            return this.Team;
        }

        /// <summary>
        /// Decreases the life of the cell by a given amount
        /// </summary>
        /// <param name="malus">The amount of life to remove to the cell</param>
        public void DecreaseLife(Int16 malus = 1)
        {
            Life -= malus;
        }

        /// <summary>
        /// Increases the life of the cell by a given amount
        /// </summary>
        /// <param name="bonus">The amount of life to add to the cell</param>
        internal void IncreaseLife(Int16 bonus)
        {
            Life += bonus;
        }

        /// <summary>
        /// Retrieves the current amount of life the cell has
        /// </summary>
        /// <returns>The amount as an Int16</returns>
        public Int16 GetLife()
        {
            return Life;
        }

        /// <summary>
        /// Drops all contained ressources at the current position
        /// </summary>
        private void DropAllRessources()
        {
            if (Life > 0)
            {
                try
                {
                    world.DropRessources(Position, Life);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                Life = 0;
            }
        }

        /// <summary>
        /// Removes the cell from the game
        /// </summary>
        private void UnregisterCell()
        {
            this.world.UnregisterCell(this);
        }

        /// <summary>
        /// Drops one unit of weight at the current position
        /// </summary>
        private void DropEarth()
        {
            if (carryingWeight)
            {
                try
                {
                    world.RaiseLandscape(Position);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                carryingWeight = false;
            }
        }

        /// <summary>
        /// Lift one unit of weight from the current position
        /// </summary>
        private void LiftEarth()
        {
            if (carryingWeight)
            {
                try
                {
                    world.LowerLandscape(Position);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                carryingWeight = true;
            }
        }

        /// <summary>
        /// Divide the cell
        /// </summary>
        private void Split()
        {
            // Compute the spawn life level after cell division
            var spawnLife = (Int16)Math.Truncate((float)(Life - Settings.Default.CostOfCellDivision) / 2);

            if (spawnLife > Settings.Default.SpawnLifeThreshold)
            {
                // Create the spawns
                this.world.CreateSpawns(spawnLife, this);
                this.Die();
            }
        }

        private void Move(Int16 offsetX, Int16 offsetY)
        {
            ICoordinates currentCoordinates = this.Position.Clone();
            ICoordinates targetCoordinates = this.Position.Clone();

            targetCoordinates.X += offsetX;
            targetCoordinates.Y += offsetY;

            // Normalize coordinates to create a continuous map (surrounding views have to be modified as well)
            // --> targetCoordinates.Normalize((Int16)(this.world.GetMap().GetMapWidth() - 1), (Int16)(this.world.GetMap().GetMapHeight() - 1));
            
            // Check that the coordinates are within the bounds of the map
            if (!CoordinatesAreWithinBounds(targetCoordinates))
                return;

            // Check that the terrain permits the cell to move and that the target tile is not already occupied
            if (!TerrainAllowsMovingTo(targetCoordinates) || TargetTileIsOccupied(targetCoordinates))
                return;

            if (this.world.GetMap().GetCellAt(targetCoordinates) == null)
            {
                this.Position = targetCoordinates;
                NotifyMovement(currentCoordinates, targetCoordinates, Team);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetCoordinates"></param>
        /// <returns></returns>
        private bool TargetTileIsOccupied(ICoordinates targetCoordinates)
        {
            bool canMove = false;
            IMap thisMap = this.world.GetMap();

            // If the target cell is occupied
            if ((thisMap.GetCellAt(targetCoordinates.X, targetCoordinates.Y)) != null)
                canMove = true;

            return canMove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetCoordinates"></param>
        /// <returns></returns>
        private bool TerrainAllowsMovingTo(ICoordinates targetCoordinates)
        {
            bool canMove = true;
            IMap thisMap = this.world.GetMap();

            // If the geometry does not allow to move
            if (Math.Abs(thisMap.GetTileAt(this.Position.X, this.Position.Y).GetAltitude()
                - thisMap.GetTileAt(targetCoordinates.X, targetCoordinates.Y).GetAltitude()) > 1)
                canMove = false;

            return canMove;
        }

        /// <summary>
        /// Checks that the given coordinate are in the limits of the game
        /// </summary>
        /// <param name="X">The x coordinate</param>
        /// <param name="Y">The y coordinate</param>
        /// <returns>True if they can be used, false otherwise</returns>
        private Boolean CoordinatesAreWithinBounds(Int16 X, Int16 Y)
        {
            if (X < 0
                || Y < 0
                || X > this.world.GetMap().GetMapWidth()
                || Y > this.world.GetMap().GetMapHeight())
                return false;

            return true;
        }

        private Boolean CoordinatesAreWithinBounds(ICoordinates coord)
        {
            return this.CoordinatesAreWithinBounds(coord.X, coord.Y);
        }

        /// <summary>
        /// Notify the world that a cell moved
        /// </summary>
        /// <param name="oldCoordinates">The old coordinates where the cell was</param>
        /// <param name="newCoordinates">The new coordinates where the cell is</param>
        /// <param name="team">The qualifier of the team</param>
        private void NotifyMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, DisplayQualifier team)
        {
            world.RegisterCellMovement(oldCoordinates, newCoordinates, team);
            return;
        }

        /// <summary>
        /// Builds up the relation between the brain and the cell
        /// </summary>
        /// <param name="newBrain">The IBrain to use</param>
        public void SetBrain(IBrain newBrain)
        {
            this.Brain = newBrain;
            this.Brain.SetCell(this);
        }

        /// <summary>
        /// Returns the type of brain currently attached to the cell
        /// </summary>
        /// <returns>The string describing the brain</returns>
        internal String GetAttachedBrainType()
        {
            return this.Brain.GetType().ToString();
        }

        /// <summary>
        /// Proofs if the cell has enough life to divide itself
        /// </summary>
        /// <returns>True if the division is allowed</returns>
        public bool CanDivide()
        {
            return (this.Life - Settings.Default.CostOfCellDivision) / 2 > 0;
        }
    }

    /// <summary>
    /// Ninject module used to create brains, world and coordinates
    /// </summary>
    internal class CellModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World.World>().InSingletonScope();
            Bind<ICoordinates>().To<Coordinates>();
        }
    }
}