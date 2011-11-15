using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.GameCore.Mapping.Tiles;
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
        private Brain _brain;
        private Int32 _life;
        private Color _team;
        private CellAction CellPreviousAction = CellAction.NONE;
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
        public Cell(int x, int y, Int32 initialLife, World thisWorld, Color teamColor)
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
            return _brain.ChooseNextAction();
        }

        /// <summary>
        /// This function executes the action passed on by the brain
        /// (For gameplay perspective the brain should not be able to control the cell himself ;)
        /// </summary>
        /// <param name="action">The action to apply</param>
        public void Do(CellAction action)
        {
            CellPreviousAction = action;

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
                case CellAction.ATTACK:
                    break;
                case CellAction.DROP:
                    break;
                case CellAction.LIFT:
                    break;
                case CellAction.SPLIT:
                    break;
                case CellAction.DIE:
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
            return CellPreviousAction;
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
    }
}