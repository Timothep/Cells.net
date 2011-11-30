using System;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Cells.Model.Mapping;

namespace Cells.Model.Brain.Brains
{
    /// <summary>
    /// The UltraAgressiveBrain orders the cells to fight as soon as they can
    /// </summary>
    [Export(typeof(IBrain))]
    public class UltraAgressiveBrain : BaseBrain, IBrain
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UltraAgressiveBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public override CellAction ChooseNextAction()
        {
            CellAction cellAction;

            SurroundingView surroundings = this.Cell.Sense();

            // Get offset to the closest enemy
            IList<ICell> allCells = surroundings.GetAllCells();

            // Remove friendly cells
            allCells = GetEnemyCells(this.Cell.GetTeam(), allCells);

            // Get closest cell
            ICell closestCell = GetClosestCell(allCells);
            
            if (closestCell == null)
                cellAction = new CellAction(GetRandomAction());
            else
            {
                // Get vector to this cell
                IOffsetVector offsetVector = new OffsetVector(this.Cell.Position, closestCell.Position);

                AvailableActions action;

                if (Math.Abs(offsetVector.X) <= 1 && Math.Abs(offsetVector.Y) <= 1)
                    action = AvailableActions.ATTACK;
                else
                    action = this.Cell.GetRelativeMovment(offsetVector);

                cellAction = new CellAction(action, offsetVector);
            }
        
            return cellAction;
        }

        private List<ICell> GetEnemyCells(DisplayQualifier friendlyTeam, IList<ICell> allCells)
        {
            List<ICell> enemyCells = new List<ICell>();

            foreach (ICell potCell in allCells)
            {
                if (potCell.GetTeam() != friendlyTeam)
                    enemyCells.Add(potCell);
            }

            return enemyCells;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allCells"></param>
        /// <returns></returns>
        private ICell GetClosestCell(IList<ICell> allCells)
        {
            ICell chosenOne = null;
            Int16? minDistance = null;

            foreach (ICell currentCell in allCells)
            {
                // First cell
                if (minDistance == null)
                    minDistance = this.Cell.Position.DistanceTo(currentCell.Position);

                // If the current cell is closer than the closest one
                if (this.Cell.Position.DistanceTo(currentCell.Position) < minDistance)
                {
                    chosenOne = currentCell;
                    minDistance = this.Cell.Position.DistanceTo(currentCell.Position);
                }
            }

            return chosenOne;
        }

        /// <summary>
        /// Function randomly choosing among all the possible actions
        /// </summary>
        /// <returns>One of the possible action</returns>
        private AvailableActions GetRandomAction()
        {
            var randomNumber = (Int16)RandomGenerator.GetRandomInt32(5);

            switch (randomNumber)
            {
                case 0:
                    return AvailableActions.MOVERIGHT;
                case 1:
                    return AvailableActions.MOVELEFT;
                case 2:
                    return AvailableActions.MOVEDOWN;
                case 3:
                    return AvailableActions.MOVEUP;
                case 4:
                    return AvailableActions.NONE;
                default:
                    throw new Exception("Something went wrong with the random numbers");
            }
        }
    }
}
