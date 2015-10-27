using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FlashWindow
{
    class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref Flashwinfo pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct Flashwinfo
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public Int32 dwTimeout;
        }

        public const UInt32 FlashwAll = 3;

        static void Main(string[] args)
        {
            foreach (Process p in Process.GetProcessesByName(args[0]))
            {
                if (p.MainWindowTitle.Equals(args[1]))
                {
                    FlashWindow(p.MainWindowHandle);
                }            
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void FlashWindow(IntPtr hWnd)
        {
            Flashwinfo fInfo = new Flashwinfo();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;
            fInfo.dwFlags = FlashwAll;
            fInfo.uCount = UInt32.MaxValue;
            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }
    }
}
