using System;

namespace Cells.Interfaces
{
    public interface ICoordinates
    {
        ICoordinates Clone();
        void SetCoordinates(Int16 inX, Int16 inY);
        short X { get; set; }
        short Y { get; set; }
        Int16? DistanceTo(ICoordinates position);

        void Normalize(Int16 maxX, Int16 maxY);
    }
}
