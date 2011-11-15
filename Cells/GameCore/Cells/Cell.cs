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
        private readonly Brain _brain;
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
        public Cell(int x, int y, Int16 initialLife, World thisWorld, Color teamColor)
        {
            Position = new Coordinates(x, y);
            _brain = new Brain(this as ICell);
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
            // Death comes first
            DecreaseLife();
            if (_life <= 0)
            {
                Die();
                return CellAction.NONE;
            }

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
                //case CellAction.ATTACK:
                //    Attack();
                //    break;
                case CellAction.DROP:
                    Drop();
                    break;
                case CellAction.LIFT:
                    Lift();
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
        /// Kills the cell
        /// </summary>
        public void Die()
        {
            DropAllRessources();
            DropEarth();
            UnregisterCell();
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
        /// Lifts earth from the ground
        /// </summary>
        private void Lift()
        {
            LiftEarth();
        }

        /// <summary>
        /// Drops carried earth on the ground
        /// </summary>
        private void Drop()
        {
            DropEarth();
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
        /// Move the cell left and save its old position
        /// </summary>
        private void MoveLeft()
        {
            Coordinates oldPosition = Position.Clone();
            Position.X--;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell right and save its old position
        /// </summary>
        private void MoveRight()
        {
            Coordinates oldPosition = Position.Clone();
            Position.X++;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell up and save its old position
        /// </summary>
        private void MoveUp()
        {
            Coordinates oldPosition = Position.Clone();
            Position.Y++;
            NotifyMovement(oldPosition, Position, _team);
        }

        /// <summary>
        /// Move the cell down and save its old position
        /// </summary>
        private void MoveDown()
        {
            Coordinates oldPosition = Position.Clone();
            Position.Y--;
            NotifyMovement(oldPosition, Position, _team);
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
        /// Gets the color of the team the cell belongs to
        /// </summary>
        /// <returns>The color of the team</returns>
        internal Color GetTeamColor()
        {
            return this._team;
        }

        ///// <summary>
        ///// Attack a cell
        ///// </summary>
        //private void Attack()
        //{
        //    throw new NotImplementedException();
        //}

        internal void DecreaseLife()
        {
            _life--;
        }

        internal Int16 GetLife()
        {
            return _life;
        }
    }
}