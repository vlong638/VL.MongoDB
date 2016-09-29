using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utilities
{
    public class Logger
    {
        static string _logPath = @"E:\WorkSpace\0.公用经验\MyCode\VL\Database\MongoDB\Test\Test\Data\Log.txt";
        static string LogPath
        {
            get
            {
                return _logPath;
            }
        }
        static object locker = new object();
        public static void WriteLog(string pattern, params object[] args)
        {
            lock (locker)
            {
                using (var stream = File.AppendText(LogPath))
                {
                    stream.WriteLine(string.Format(pattern, args));
                }
            }
        }
    }
}
