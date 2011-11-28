using System;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;

namespace Cells.Model.Brain.Brains
{
    /// <summary>
    /// The WildDuplicatingBrain orders the cells to split as long as they have enough ressources to do so
    /// </summary>
    [Export(typeof(IBrain))]
    public class WildDuplicatingBrain : BaseBrain, IBrain
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WildDuplicatingBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public override CellAction ChooseNextAction()
        {
            AvailableActions action = AvailableActions.SPLIT;

            if (RandomGenerator.GetRandomInt16(2) == 1 || !this.Cell.CanDivide())
                action = GetRandomAction();

            return new CellAction(action); 
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
