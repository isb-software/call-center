﻿using log4net;
using log4net.Config;
using SipWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestDriver
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            AutoPhone autoPhone = new AutoPhone();
            autoPhone.OnCompletion += AutoPhone_OnCompletion;

            autoPhone.StartCall("40746294444");

            Console.ReadLine();
        }

        private static void AutoPhone_OnCompletion(object sender, RobotCallDataEventArgs e)
        {
            var a = e;
            int b = 23;
        }
    }
}
