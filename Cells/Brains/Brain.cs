using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Cells.GameCore.Mapping;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;
using Cells.Interfaces;

namespace Cells.Brains
{
    class Brain: IBrain
    {
        private readonly ICell _cell;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCell">The cell that the brain should control</param>
        internal Brain(ICell theCell)
        {
            _cell = theCell;
        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns></returns>
        public CellAction ChooseNextAction()
        {
            CellAction action;

            // The cell has 25% chance to continue as it was going before
            //if (0 == RandomGenerator.GetRandomInteger(4))
            //    action = _cell.GetPreviousAction();
            //else
            {
                // Pick one of its neighbors direction
                SurroundingView surroundings = _cell.Sense();
                List<Cell> neighbors = surroundings.GetAllCells();

                // If the cell has no neighbours or in 25% of the cases it goes random
                if (neighbors.Count == 0 || RandomGenerator.GetRandomInteger(100) == 0)
                    action = GetRandomAction();
                else
                {
                    // Else the cell follows a neighbour
                    action = this._cell.GetRelativePosition(neighbors[RandomGenerator.GetRandomInteger(neighbors.Count)].Position);
                }
            }

            return action;
        }

        /// <summary>
        /// Function randomly choosing among all the possible actions
        /// </summary>
        /// <returns>One of the possible action</returns>
        private CellAction GetRandomAction()
        {
            var randomNumber = (Int16)RandomGenerator.GetRandomInteger(5);

            switch (randomNumber)
            {
                case 0:
                    return CellAction.MOVERIGHT;
                case 1:
                    return CellAction.MOVELEFT;
                case 2:
                    return CellAction.MOVEDOWN;
                case 3:
                    return CellAction.MOVEUP;
                case 4:
                    return CellAction.NONE;
                default:
                    throw new Exception("Something went wrong with the random numbers");
            }
        }
    }
}
