using System;
using Cells.Interfaces;
using System.Collections.Generic;

namespace Cells.Model.Mapping
{
    public interface ISurroundingView
    {
        IList<ICell> GetAllCells();
    }
}
