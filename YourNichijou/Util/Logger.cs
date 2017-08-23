using System;
using System.Collections.Generic;
using System.Text;

namespace YourNichijou.Util
{
    class Logger
    {
        public static void WriteException(Exception e)
        {
            Console.Error.WriteLine($"Exception ({String.Format("0x{0:X}",e.HResult)}): \n\t{e.Message}");
        }
    }
}
