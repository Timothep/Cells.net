using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.Interfaces;

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
