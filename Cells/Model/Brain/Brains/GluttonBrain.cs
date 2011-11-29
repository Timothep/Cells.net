using System;
using Cells.Model.Mapping;
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

            // Get the offset to the closest ressource pool
            IOffsetVector offsetVector = surroundings.GetClosestRessourcePool(this.Cell.Position, this.Cell);

            // If no ressources found, go random
            if (offsetVector == null)
                action = GetRandomAction();
            else
            {
                // If the ressources are directly in contact, eat otherwise move toward it
                if (Math.Abs(offsetVector.X) <= 1 && Math.Abs(offsetVector.Y) <= 1)
                    action = AvailableActions.EAT;
                else
                    action = this.Cell.GetRelativeMovment(offsetVector);
            }

            return action == AvailableActions.EAT ? new CellAction(action, offsetVector) : new CellAction(action);
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
