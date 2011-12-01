using System;
using Cells.Model.Mapping;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace Cells.Model.Brain.Brains
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IBrain))]
    public class GluttonBrain : BaseBrain, IBrain
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GluttonBrain()
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

            // Get the offset to the closest ressource pool
            IList<IOffsetVector> allOffsetsToRessources = surroundings.GetOffsetToAllRessourcesLocations();

            IOffsetVector closestRessourcePool = this.GetClosestMapTile(allOffsetsToRessources);

            if (closestRessourcePool == null)
                cellAction = new CellAction(GetRandomAction());
            else
            {
                AvailableActions action;

                if (Math.Abs(closestRessourcePool.X) <= 1 && Math.Abs(closestRessourcePool.Y) <= 1)
                    action = AvailableActions.EAT;
                else
                    action = this.Cell.GetRelativeMovment(closestRessourcePool);

                cellAction = new CellAction(action, closestRessourcePool);
            }

            return cellAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allCells"></param>
        /// <returns></returns>
        private IOffsetVector GetClosestMapTile(IList<IOffsetVector> allOffsetVectors)
        {
            IOffsetVector chosenOne = null;
            Int16? minDistance = null;

            foreach (IOffsetVector currentVector in allOffsetVectors)
            {
                // First vector
                if (minDistance == null)
                {
                    minDistance = this.Cell.Position.DistanceTo(currentVector);
                    chosenOne = currentVector;
                }

                // If the current cell is closer than the closest one
                if (this.Cell.Position.DistanceTo(currentVector) < minDistance)
                {
                    chosenOne = currentVector;
                    minDistance = this.Cell.Position.DistanceTo(currentVector);
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
