using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace HandsOnMVCUsingExceptionHandling
{
    public class Logger
    {
        public static void WriteError(Exception ex,string action,string controller)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\TwitterClone_errinfo.txt", true))
            {
                string err = @"Error: " + ex.GetBaseException().Message + Environment.NewLine +
                    "Action: " + action + Environment.NewLine +
                    "Controller: " + controller + Environment.NewLine +
                    "Date: " + System.DateTime.Now;
                sw.WriteLine(err);
            }
        }
    }
}