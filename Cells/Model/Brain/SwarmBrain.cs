using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping;
using Cells.GameCore.Cells;
using Cells.Model;
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
        public SwarmBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns></returns>
        public override CellAction ChooseNextAction()
        {
            AvailableActions action;

            SurroundingView surroundings = this.Cell.Sense();
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
                    action = this.Cell.GetRelativeMovment(closestNeighbour.Position);
            }

            return new CellAction(action);
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
                else if (Math.Abs((UInt16)(this.Cell.Position.X - cell.Position.X)) + Math.Abs((UInt16)(this.Cell.Position.Y - cell.Position.Y)) < minDistance)
                    chosenOne = cell;
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
