using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping;
using Cells.GameCore.Cells;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;
using Cells.Model.Brain;

namespace Cells.Brain
{
    /// <summary>
    /// The SwarmBrain does nothing else than moving
    /// It moves either randomly (10% chances)
    /// </summary>
    [Export(typeof(IBrain))]
    public class SwarmBrain : BaseBrain, IBrain
    {
        private const Int16 randomMovementChances =10; //Expressed in %

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCell">The cell that the brain should control</param>
        public SwarmBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns></returns>
        public override CellAction ChooseNextAction()
        {
            CellAction action;

            SurroundingView surroundings = _cell.Sense();
            IList<ICell> neighbours = surroundings.GetAllCells();

            // In randomMovementChances % of the cases => it goes random
            if (RandomGenerator.GetRandomInt32(100) < randomMovementChances)
                action = GetRandomAction();
            else
            {
                // Else the cell goes toward the closest neighbour
                ICell closestNeighbour = GetClosestNeighbour(neighbours);

                if (closestNeighbour == null)
                    action = GetRandomAction();
                else
                    action = _cell.GetRelativeMovment(closestNeighbour.Position);
            }

            return action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neighbours"></param>
        /// <returns></returns>
        private ICell GetClosestNeighbour(IList<ICell> neighbours)
        {
            Int16? minDistance = null;
            ICell chosenOne = null;

            foreach(ICell cell in neighbours)
            {
                if (minDistance == null)
                    chosenOne = cell;
                else if (Math.Abs((UInt16)(this._cell.Position.X - cell.Position.X)) + Math.Abs((UInt16)(this._cell.Position.Y - cell.Position.Y)) < minDistance)
                    chosenOne = cell;
            }

            return chosenOne;
        } 

        /// <summary>
        /// Function randomly choosing among all the possible actions
        /// </summary>
        /// <returns>One of the possible action</returns>
        private CellAction GetRandomAction()
        {
            var randomNumber = (Int16)RandomGenerator.GetRandomInt32(5);

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
