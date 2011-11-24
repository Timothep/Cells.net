using System.Runtime.InteropServices;

namespace Cells.Controller
{
    class Timer
    {
        [DllImport("kernel32.dll")]
        private static extern long GetTickCount();

        private long StartTick = 0;

        public Timer()
        {
            Reset();
        }

        public void Reset()
        {
            StartTick = GetTickCount();
        }

        public long GetTicks()
        {
            return GetTickCount() - StartTick;
        }

    }
}
