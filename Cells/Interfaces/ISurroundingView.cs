using System;
using Cells.Interfaces;
using System.Collections.Generic;

namespace Cells.Interfaces
{
    public interface ISurroundingView
    {
        IList<ICell> GetAllCells();
    }
}
