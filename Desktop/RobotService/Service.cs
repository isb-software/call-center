using System;
using log4net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SipWrapper;

namespace RobotService
{
    public partial class Service : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        const string NBR_OF_THREADS_KEY = "threads";
        const int MAX_NBR_OF_THREADS = 5;

        static readonly int NBR_OF_THREADS = Math.Min(
            MAX_NBR_OF_THREADS,
            Int32.Parse(ConfigurationManager.AppSettings[NBR_OF_THREADS_KEY]));

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //while(true)
                //{
                //    SipPhone sipPhone = new SipPhone();
                //    Task.When
                //}
                
            }
            catch(Exception ex) {
                Log.Error($"Error on OnStart {ex.ToString()}.", ex);
            }
        }

        private void executeCall()
        {

        }

        private static string getNextPhoneToCall()
        {
            return "0746294444";
        }

        protected override void OnStop()
        {
        }
    }
}
