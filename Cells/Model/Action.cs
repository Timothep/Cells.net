using Cells.Interfaces;

namespace Cells.Model
{
    public enum AvailableActions { NONE, MOVERIGHT, MOVELEFT, MOVEUP, MOVEDOWN, EAT, ATTACK, SPLIT, LIFT, DROP, DIE }

    public class CellAction
    {
        private AvailableActions Action { get; set; }
        private IOffsetVector OffsetToTarget { get; set; }
        //private ICoordinates TargetMapTile { get; set; }

        //public CellAction(AvailableActions theAction, ICoordinates targetCell = null)
        public CellAction(AvailableActions theAction, IOffsetVector targetOffset = null)
        {
            this.Action = theAction;
            this.OffsetToTarget = targetOffset;

            //this.TargetMapTile = targetCell;
        }

        public AvailableActions GetAction()
        {
            return this.Action;
        }

        //public ICoordinates GetTargetMapTile()
        //{
        //    return this.TargetMapTile;
        //}

        public IOffsetVector GetOffsetToTarget()
        {
            return this.OffsetToTarget;
        }
    }
}
