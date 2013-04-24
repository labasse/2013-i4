using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace ImageScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine(
                    "Usage : {0} url [download-dir]",
                    Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)
                );
            }
            else
            {
                string dir = args.Length < 2 ? Environment.CurrentDirectory : args[1];
                Stopwatch stopwatch = Stopwatch.StartNew();
                ImageScrapper scrapper = new ImageScrapper(dir, args[0]);
                
                Trace.Listeners.Add(new ConsoleTraceListener(true));
                scrapper.Scrap();
                Console.WriteLine("Scrap time : {0:c}", stopwatch.Elapsed);
            }
        }
    }
}
