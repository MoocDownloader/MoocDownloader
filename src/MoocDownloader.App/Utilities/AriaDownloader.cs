using System;
using System.Diagnostics;

namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// ARIA2C downloader.
    /// </summary>
    public class AriaDownloader
    {
        private const string ARIA_EXE = "";

        public void Start()
        {
            var args = "";

            var process = new Process
            {
                StartInfo =
                {
                    FileName               = ARIA_EXE, // command  
                    Arguments              = args,     // arguments  
                    CreateNoWindow         = true,
                    UseShellExecute        = false, // do not create a window.  
                    RedirectStandardInput  = true,  // redirect input.  
                    RedirectStandardOutput = true,  // redirect output.  
                    RedirectStandardError  = true   // redirect error.  
                },
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (sender, eventArgs) => { Console.WriteLine(eventArgs.Data); };
            process.ErrorDataReceived  += (sender, eventArgs) => { Console.WriteLine(eventArgs.Data); };

            process.Exited += (sender, eventArgs) => { };

            process.Start();
        }
    }
}