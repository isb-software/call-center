using System.Configuration;
using System.Diagnostics;

namespace Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Add exception handling and logging.
            UpdateAgent.Update();
        }

        private static void StartAgent()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            startInfo.Arguments = $"/C start {UpdateAgent.TargetDirectoryPath}Agent.exe";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
