using System.Runtime.InteropServices;

namespace Cells.Controller
{
    class Timer
    {
        [DllImport("kernel32.dll")]
        private static extern long GetTickCount();

        private long startTick = 0;

        public Timer()
        {
            Reset();
        }

        public void Reset()
        {
            startTick = GetTickCount();
        }

        public long GetTicks()
        {
            return GetTickCount() - startTick;
        }

    }
}
