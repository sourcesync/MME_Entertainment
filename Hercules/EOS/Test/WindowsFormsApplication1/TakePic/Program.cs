using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TakePic
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                fork();
            }
        }

        public static int fork()
        {
            string filename = Process.GetCurrentProcess().MainModule.FileName.Replace(".vshot", "");

            ProcessStartInfo info = new ProcessStartInfo(filename);

            info.UseShellExecute = false;

            info.Arguments = "child";
            Process child = Process.Start(info);

            return 0;
        }

    }
}
