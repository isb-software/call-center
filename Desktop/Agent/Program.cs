using System;
using System.Threading;
using System.Windows.Forms;

namespace Agent
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(false, "isbsoftware.com CallCenter"))
            {
                bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
                if (isAnotherInstanceOpen)
                {
                    Console.WriteLine(@"Only one instance of this app is allowed.");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CallAgent());
            }
        }
    }
}
