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
            String host = args.Length == 0 ? "ws://localhost:8080" : args[0];
            // Hardcode the server adress for now.
            ScreenStreamer streamer = new ScreenStreamer(host);
            streamer.Toggle();
            var endlessTask = new TaskCompletionSource<bool>().Task;
            endlessTask.Wait();
        }
    }
}
