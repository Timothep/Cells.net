using System;
using System.Collections.Generic;
using Cells.Model.Mapping;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;

namespace Cells.Model.Brain.Brains
{
    /// <summary>
    /// The SwarmBrain does nothing else than moving
    /// It moves either randomly (10% chances)
    /// </summary>
    [Export(typeof(IBrain))]
    public class SwarmBrain : BaseBrain, IBrain
    {
        private const Int16 RandomMovementChances =10; //Expressed in %

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
            if (RandomGenerator.GetRandomInt32(100) < RandomMovementChances)
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
        private ICell GetClosestNeighbour(IEnumerable<ICell> neighbours)
        {
            Int16? minDistance = null;
            ICell chosenOne = null;

            foreach(ICell cell in neighbours)
            {
                Int16? distance = this.Cell.Position.DistanceTo(Cell.Position);

                if (minDistance == null)
                {
                    chosenOne = cell;
                    minDistance = distance;
                }
                else if (distance < minDistance)
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
