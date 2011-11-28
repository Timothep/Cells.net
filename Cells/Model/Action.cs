using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Model
{
    public enum AvailableActions { NONE, MOVERIGHT, MOVELEFT, MOVEUP, MOVEDOWN, EAT, ATTACK, SPLIT, LIFT, DROP, DIE }

    public class CellAction
    {
        private AvailableActions action { get; set; }

        private ICoordinates targetCell;

        public CellAction(AvailableActions theAction, ICoordinates targetCell = null)
        {
            this.action = theAction;
            this.targetCell = targetCell;
        }

        public AvailableActions GetAction()
        {
            return this.action;
        }
    }
}
