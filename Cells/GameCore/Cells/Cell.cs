using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.GameCore.Mapping.Tiles;
using Cells.Properties;
using Cells.Utils;
using Cells.GameCore.Mapping;
using Cells.Brains;
using Cells.Interfaces;

namespace Cells.GameCore.Cells
{
    public enum CellAction { NONE, MOVERIGHT, MOVELEFT, MOVEUP, MOVEDOWN, ATTACK, SPLIT, LIFT, DROP, DIE }
    
    public class Cell: ICell
    {
        public Coordinates Position { get; private set; }
        private readonly IBrain _brain;
        private Int16 _life;
        private readonly Color _team;
        private CellAction _cellPreviousAction = CellAction.NONE;
        private Boolean _carryingWeight = false;

        // Hold reference to the World
        private readonly World _world;

        /// <summary>
        /// Cell constructor
        /// </summary>
        /// <param name="x">The x position where the cell is spawned</param>
        /// <param name="y">The y position where the cell is spawned</param>
        /// <param name="initialLife">life the cell is going to spawn with</param>
        /// <param name="thisWorld">A reference to the world the cell lives in</param>
        public Cell(Int16 x, Int16 y, Int16 initialLife, World thisWorld, Color teamColor)
        {
            Position = new Coordinates(x, y);
            _brain = new SwarmBrain(this as ICell);
            _life = initialLife;
            _world = thisWorld;
            _team = teamColor;
        }

        /// <summary>
        /// Function triggering the brain to chose the next action
        /// </summary>
        /// <returns>The chose action</returns>
        public CellAction Think()
        {
            return _brain.ChooseNextAction();
        }

        /// <summary>
        /// This function executes the action passed on by the brain
        /// (For gameplay perspective the brain should not be able to control the cell himself ;)
        /// </summary>
        /// <param name="action">The action to apply</param>
        public void Do(CellAction action)
        {
            _cellPreviousAction = action;

            switch (action)
            {
                case CellAction.MOVELEFT:
                    MoveLeft();
                    break;
                case CellAction.MOVERIGHT:
                    MoveRight();
                    break;
                case CellAction.MOVEUP:
                    MoveUp();
                    break;
                case CellAction.MOVEDOWN:
                    MoveDown();
                    break;
                //case CellAction.ATTACK: //Not yet implemented
                //    Attack();
                //    break;
                case CellAction.DROP:
                    DropEarth();
                    break;
                case CellAction.LIFT:
                    LiftEarth();
                    break;
                case CellAction.SPLIT:
                    Split();
                    break;
                case CellAction.DIE:
                    Die();
                    break;
                case CellAction.NONE:
                    break;
                default:
                    throw new NotImplementedException();
            }
            return;
        }

        /// <summary>
        /// Function returning the previous action that the cell did
        /// </summary>
        /// <returns>The last CellAction the cell did</returns>
        public CellAction GetPreviousAction()
        {
            return _cellPreviousAction;
        }

        /// <summary>
        /// A cell is very much blind. 
        /// When it senses its surroundings, it can see at most a square around itself
        /// </summary>
        /// <returns>A MapView describing its immediate surroundings</returns>
        public SurroundingView Sense()
        {
            return _world.GetSurroundingsView(this);
        }

        /// <summary>
        /// Function returning a cell action indicating in which direction the cell should move to approach the given coordinates
        /// </summary>
        /// <param name="coordinates">The target coordinates</param>
        /// <returns>A cell action indicating how the cell should move to get there</returns>
        public CellAction GetRelativePosition(Utils.Coordinates coordinates)
        {
            List<CellAction> actions = new List<CellAction>();

            if (coordinates.Y > this.Position.Y)
                actions.Add(CellAction.MOVEDOWN);

            if (coordinates.Y < this.Position.Y)
                actions.Add(CellAction.MOVEUP);
            
            if (coordinates.X < this.Position.X)
                actions.Add(CellAction.MOVELEFT);
            
            if (coordinates.X > this.Position.X)
                actions.Add(CellAction.MOVERIGHT);
            
            if (actions.Count > 0)
                return actions[RandomGenerator.GetRandomInteger(actions.Count)];
            else
                return CellAction.NONE;
        }

        /// <summary>
        /// Kills the cell
        /// </summary>
        internal void Die()
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
            return this._team;
        }

        /// <summary>
        /// Decreases the life of the cell by a given amount
        /// </summary>
        /// <param name="malus">The amount of life to remove to the cell</param>
        internal void DecreaseLife(Int16 malus = 1)
        {
            _life -= malus;
        }

        /// <summary>
        /// Increases the life of the cell by a given amount
        /// </summary>
        /// <param name="bonus">The amount of life to add to the cell</param>
        internal void IncreaseLife(Int16 bonus)
        {
            _life += bonus;
        }

        /// <summary>
        /// Retrieves the current amount of life the cell has
        /// </summary>
        /// <returns>The amount as an Int16</returns>
        internal Int16 GetLife()
        {
            return _life;
        }

        /// <summary>
        /// Drops all contained ressources at the current position
        /// </summary>
        private void DropAllRessources()
        {
            if (_life > 0)
            {
                try
                {
                    _world.DropRessources(Position, _life);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                _life = 0;
            }
        }

        /// <summary>
        /// Removes the cell from the game
        /// </summary>
        private void UnregisterCell()
        {
            this._world.UnregisterCell(this);
        }

        /// <summary>
        /// Drops one unit of weight at the current position
        /// </summary>
        private void DropEarth()
        {
            if (_carryingWeight)
            {
                try
                {
                    _world.RaiseLandscape(Position);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                _carryingWeight = false;
            }
        }

        /// <summary>
        /// Lift one unit of weight from the current position
        /// </summary>
        private void LiftEarth()
        {
            if (_carryingWeight)
            {
                try
                {
                    _world.LowerLandscape(Position);
                }
                catch (InvalidOperationException e)
                {
                    return;
                }
                _carryingWeight = true;
            }
        }

        /// <summary>
        /// Divide the cell
        /// </summary>
        private void Split()
        {
            if (_life > Settings.Default.CostOfCellDivision + 2 * Settings.Default.SpawnLifeThreshold)
            {
                Int16 spawnLife = (Int16)Math.Truncate((float)(_life - Settings.Default.CostOfCellDivision) / 2);

                // Create the first spawn
                _world.CreateSpawns(spawnLife, this);

                this.Die();
            }
        }

        /// <summary>
        /// Move the cell left and save its old position
        /// </summary>
        private void MoveLeft()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid((Int16)(Position.X - 1), Position.Y))
                return;

            Coordinates oldPosition = Position.Clone();
            Position.X--;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell right and save its old position
        /// </summary>
        private void MoveRight()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid((Int16)(Position.X + 1), Position.Y))
                return;

            Coordinates oldPosition = Position.Clone();
            Position.X++;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell up and save its old position
        /// </summary>
        private void MoveUp()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid(Position.X, (Int16)(Position.Y - 1)))
                return;

            Coordinates oldPosition = Position.Clone();
            Position.Y--;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell down and save its old position
        /// </summary>
        private void MoveDown()
        {
            // Check the potentially new coordinate
            if (!CoordinatesAreValid(Position.X, (Int16)(Position.Y + 1)))
                return;

            Coordinates oldPosition = Position.Clone();
            Position.Y++;
            NotifyMovement(oldPosition, Position, _team);
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
        private void NotifyMovement(Coordinates oldCoordinates, Coordinates newCoordinates, Color team)
        {
            _world.RegisterCellMovement(oldCoordinates, newCoordinates, team);
            return;
        }
    }
}