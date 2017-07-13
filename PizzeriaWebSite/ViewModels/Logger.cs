using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaWebSite.ViewModels
{
    public class Logger
    {
        public static string LogDirectroyPath = Environment.CurrentDirectory;

        public static void Log(String Lines)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(LogDirectroyPath + "\\Error.log", true);
                file.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " --> " + Lines);
                file.Close();
            }
            catch
            {

            }
        }
    }
}