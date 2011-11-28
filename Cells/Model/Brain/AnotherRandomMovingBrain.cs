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
    public class AnotherRandomMovingBrain : BaseBrain, IBrain
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AnotherRandomMovingBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public override CellAction ChooseNextAction()
        {
            return new CellAction(GetRandomAction());
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
