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
        private AvailableActions Action { get; set; }
        private ICoordinates TargetMapTile { get; set; }

        public CellAction(AvailableActions theAction, ICoordinates targetCell = null)
        {
            this.Action = theAction;
            this.TargetMapTile = targetCell;
        }

        public AvailableActions GetAction()
        {
            return this.Action;
        }

        public ICoordinates GetTargetMapTile()
        {
            return this.TargetMapTile;
        }

    }
}
