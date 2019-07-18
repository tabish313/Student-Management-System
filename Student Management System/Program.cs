using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Student_Management_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYxMzlAMzEzNjJlMzQyZTMwUDlUWnV2VksrcGVZMDIvM3JIK3IyY2htMDZXeEs3MkFaTVVCZGRoVGc5TT0=");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] lines = File.ReadAllLines(Application.StartupPath + "/bin/User.Config");
            var arr = lines[2].Split(':');


            string firstcheck = arr[1].ToString();
            
            if (firstcheck == "True")
            {
                Application.Run(new FirstRun());
            }
            else
            {
                Application.Run(new Splash());
            }
        }
    }
}