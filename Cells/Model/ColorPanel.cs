using System;
using System.Drawing;
using Cells.Interfaces;

namespace Cells.Model
{
    internal class ColorPanel : IColorPanel
    {
        public Color GetCorrespondingColor(DisplayQualifier qualifier)
        {
            switch(qualifier)
            {
                 // Altitude
                case DisplayQualifier.VeryLowAltitude:
                    return Color.SaddleBrown;
                case DisplayQualifier.LowAltitude:
                    return Color.Sienna;
                case DisplayQualifier.GroundAltitude:
                    return Color.Peru;
                case DisplayQualifier.HighAltitude:
                    return Color.BurlyWood;
                case DisplayQualifier.VeryHighAltitude:
                    return Color.Wheat;
                // Teams
                case DisplayQualifier.Team1:
                    return Color.Red;
                case DisplayQualifier.Team2:
                    return Color.DodgerBlue;
                case DisplayQualifier.Team3:
                    return Color.BlueViolet;
                case DisplayQualifier.Team4:
                    return Color.Fuchsia;
                // Other
                case DisplayQualifier.DeadCell:
                    return Color.LightBlue;
                case DisplayQualifier.RessourcesAvailable:
                    return Color.LimeGreen;
                case DisplayQualifier.RessourcesPlant:
                    return Color.LawnGreen;

                default:
                    return Color.Transparent;
            }
        }
    }
}
