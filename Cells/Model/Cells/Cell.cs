using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.Model;
using Cells.Properties;
using Cells.Utils;
using Cells.GameCore.Mapping;
using Cells.Interfaces;
using Ninject;
using Ninject.Modules;

namespace Cells.GameCore.Cells
{
    public class Cell: ICell
    {
        private readonly IWorld world;
        private IBrain Brain { get; set; }
        public ICoordinates Position { get; set; }
        public Int16 Life { get; set; }
        public Color Team { get; set; }

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
        /// <param name="teamColor"></param>
        public void SetTeam(Color teamColor)
        {
            this.Team = teamColor;
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
                    MoveLeft();
                    break;
                case AvailableActions.MOVERIGHT:
                    MoveRight();
                    break;
                case AvailableActions.MOVEUP:
                    MoveUp();
                    break;
                case AvailableActions.MOVEDOWN:
                    MoveDown();
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
                    Eat(action.GetTargetMapTile());
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
        /// <param name="coordinates">The coordinates where something to eat should be present</param>
        private void Eat(ICoordinates coordinates)
        {
            if (coordinates == null)
                return;

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
        /// Kills the cell
        /// </summary>
        public void Die()
        {
            DropAllRessources();
            DropEarth();
            UnregisterCell();
        }

        /// <summary>
        /// Gets the color of the team the cell belongs to
        /// </summary>
        /// <returns>The color of the team</returns>
        internal Color GetTeamColor()
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

        /// <summary>
        /// Move the cell left and save its old position
        /// </summary>
        private void MoveLeft()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid((Int16)(Position.X - 1), Position.Y) || !CanMoveLeft())
                return;

            ICoordinates oldPosition = Position.Clone();
            Position.X--;
            NotifyMovement(oldPosition, Position, Team);
        }

        /// <summary>
        /// Move the cell right and save its old position
        /// </summary>
        private void MoveRight()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid((Int16)(Position.X + 1), Position.Y) || !CanMoveRight())
                return;

            ICoordinates oldPosition = Position.Clone();
            Position.X++;
            NotifyMovement(oldPosition, Position, Team);
        }

        /// <summary>
        /// Move the cell up and save its old position
        /// </summary>
        private void MoveUp()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid(Position.X, (Int16)(Position.Y - 1)) || !CanMoveUp())
                return;

            ICoordinates oldPosition = Position.Clone();
            Position.Y--;
            NotifyMovement(oldPosition, Position, Team);
        }

        /// <summary>
        /// Move the cell down and save its old position
        /// </summary>
        private void MoveDown()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid(Position.X, (Int16)(Position.Y + 1)) || !CanMoveDown())
                return;

            ICoordinates oldPosition = Position.Clone();
            Position.Y++;
            NotifyMovement(oldPosition, Position, Team);
        }

        /// <summary>
        /// Checks that the given coordinate are in the limits of the game
        /// </summary>
        /// <param name="X">The x coordinate</param>
        /// <param name="Y">The y coordinate</param>
        /// <returns>True if they can be used, false otherwise</returns>
        private Boolean CoordinatesAreValid(Int16 X, Int16 Y)
        {
            if (X < 0
                || Y < 0
                || X >= Settings.Default.WorldWidth
                || Y >= Settings.Default.WorldHeight)
                return false;

            return true;
        }

        /// <summary>
        /// Notify the world that a cell moved
        /// </summary>
        /// <param name="oldCoordinates">The old coordinates where the cell was</param>
        /// <param name="newCoordinates">The new coordinates where the cell is</param>
        /// <param name="team">The color the team is on</param>
        private void NotifyMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, Color team)
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

        /// <summary>
        /// Checks if the up position is free to move to
        /// </summary>
        /// <returns>True if the cell can move up</returns>
        public bool CanMoveUp()
        {
            return (this.world.GetMap().GetCellAt(this.Position.X, (Int16)(this.Position.Y - 1)) == null);
        }

        /// <summary>
        /// Checks if the down position is free to move to
        /// </summary>
        /// <returns>True if the cell can move down</returns>
        public bool CanMoveDown()
        {
            return (this.world.GetMap().GetCellAt(this.Position.X, (Int16)(this.Position.Y + 1)) == null);
        }

        /// <summary>
        /// Checks if the left position is free to move to
        /// </summary>
        /// <returns>True if the cell can move left</returns>
        public bool CanMoveLeft()
        {
            return (this.world.GetMap().GetCellAt((Int16)(this.Position.X - 1), this.Position.Y) == null);
        }

        /// <summary>
        /// Checks if the right position is free to move to
        /// </summary>
        /// <returns>True if the cell can move right</returns>
        public bool CanMoveRight()
        {
            return (this.world.GetMap().GetCellAt((Int16)(this.Position.X + 1), this.Position.Y) == null);
        }
    }

    /// <summary>
    /// Ninject module used to create brains, world and coordinates
    /// </summary>
    internal class CellModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>().InSingletonScope();
            Bind<ICoordinates>().To<Coordinates>();
        }
    }
}