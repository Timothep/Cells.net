using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Interfaces
{
    public interface IDisplayController
    {
        IDictionary<ICoordinates, Color> GetPaintJobs();

        IDictionary<ICoordinates, Color> UpdatedElements { get; set; }

        void SetStaticElement(ICoordinates elementCoordinates, Color elementColor);

        void SetDynamicElement(ICoordinates elementCoordinates, Color elementColor);

        void SetBackgroundToBePaintAt(ICoordinates coordinates);

        void ResetDynamicDisplay();

        void PaintWholeBackground();
    }
}
