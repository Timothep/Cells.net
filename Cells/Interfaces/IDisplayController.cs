using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Interfaces
{
    public enum DisplayQualifier { 
        VeryLowAltitude = 0,
        LowAltitude, 
        GroundAltitude, 
        HighAltitude, 
        VeryHighAltitude,
        Team1, //5
        Team2, 
        Team3, 
        Team4, 
        DeadCell, // 9
        RessourcesAvailable, //10
        RessourcesPlant //11
    }

    public interface IDisplayController
    {
        IDictionary<ICoordinates, Color> GetPaintJobs();

        IDictionary<ICoordinates, Color> UpdatedElements { get; set; }

        void SetStaticElement(ICoordinates elementCoordinates, DisplayQualifier qualifier);

        void SetDynamicElement(ICoordinates elementCoordinates, DisplayQualifier qualifier);

        void SetBackgroundToBePaintAt(ICoordinates coordinates);

        void ResetDynamicDisplay();

        void PaintWholeBackground();

        void ResetStaticElements();
    }
}
