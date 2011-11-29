using System;
using Cells.Model;
namespace Cells.Interfaces
{
    public interface IOffsetVector
    {
        IOffsetVector Clone();
        void SetOffset(short inX, short inY);
        short X { get; set; }
        short Y { get; set; }
    }
}
