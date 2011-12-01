using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cells.Interfaces
{
    public interface IMapFactory
    {
        Int16[,] CreateMapFromFile(String mapPath);
    }
}
