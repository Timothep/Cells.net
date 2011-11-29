using System.Drawing;

namespace Cells.View
{
    internal class VisualTile
    {
        private Color color;

        public VisualTile(Color newColor)
        {
            this.color = newColor;
        }

        internal Color GetColor()
        {
            return this.color;
        }
    }
}
