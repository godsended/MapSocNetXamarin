using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MapSocNetXamarin.Models
{
    public static class Debug
    {
        private static string _logsPath = @"D:\XamarinAppDebug.txt";
        private static FileInfo _logFile;
        public static async void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
