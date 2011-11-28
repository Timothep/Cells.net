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
    public class AnotherRandomMovingBrain : BaseBrain, IBrain
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCell">The cell that the brain should control</param>
        public AnotherRandomMovingBrain()
        {

        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public override CellAction ChooseNextAction()
        {
            return GetRandomAction();
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
