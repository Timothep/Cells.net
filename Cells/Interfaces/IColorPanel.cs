using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cells.Interfaces
{
    interface IColorPanel
    {
        Color GetCorrespondingColor(DisplayQualifier qualifier);
    }
}
