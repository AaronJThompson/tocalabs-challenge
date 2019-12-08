using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TocalabsRDP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ScreenStreamer streamer = new ScreenStreamer("ws://localhost:8080");
            streamer.Toggle();
            var endlessTask = new TaskCompletionSource<bool>().Task;
            endlessTask.Wait();
        }
    }
}
