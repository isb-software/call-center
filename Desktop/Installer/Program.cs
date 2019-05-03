using System.Configuration;
using System.Diagnostics;

namespace Installer
{
    class Program
    {
        private static readonly string sourceDirectoryPathKey = "SourceFolder";

        static void Main(string[] args)
        {
            new UpdateAgent().Update();

            StartAgent();
        }

        private static void StartAgent()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            var sourceDirectoryPath = ConfigurationManager.AppSettings[sourceDirectoryPathKey];

            startInfo.Arguments = $"/C start {sourceDirectoryPath}Agent.exe";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
