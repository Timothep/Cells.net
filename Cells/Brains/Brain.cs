using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;
using Cells.Interfaces;

namespace Cells.Brains
{
    class Brain
    {
        private readonly ICell _cell;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCell">The cell that the brain should control</param>
        public Brain(ICell theCell)
        {
            _cell = theCell;
        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns></returns>
        public CellAction ChooseNextAction()
        {
            CellAction action = CellAction.NONE;
            SurroundingView surroundings = _cell.Sense();
            List<Cell> neighbors = surroundings.GetAllCells();

            int rand = RandomGenerator.GetRandomInteger(neighbors.Count + 4);
            switch (rand)
            {
                case 0:
                    action = CellAction.MOVERIGHT;
                    break;
                case 1:
                    //action = CellAction.MOVELEFT;
                    action = CellAction.MOVERIGHT;
                    break;
                case 2:
                    action = CellAction.MOVEDOWN;
                    break;
                case 3:
                    //action = CellAction.MOVEUP;
                    action = CellAction.MOVEDOWN;
                    break;
                default:
                    action = neighbors[rand - 4 ].GetPreviousAction();
                    break;
            }

            return action;
        }
    }
}
