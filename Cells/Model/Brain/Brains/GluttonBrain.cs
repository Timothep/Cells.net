using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;

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
            AvailableActions action;

            SurroundingView surroundings = this.Cell.Sense();
            ICoordinates coordinates = surroundings.GetClosestRessourcePool();

            // If no ressources found, go random
            if (coordinates == null)
                action = GetRandomAction();
            else
                // If the ressources are directly in contact, eat otherwise move toward it
                action = this.Cell.Position.DistanceTo(coordinates) == 1 ? AvailableActions.EAT : this.Cell.GetRelativeMovment(coordinates);

            return action == AvailableActions.EAT ? new CellAction(action, coordinates) : new CellAction(action);
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
