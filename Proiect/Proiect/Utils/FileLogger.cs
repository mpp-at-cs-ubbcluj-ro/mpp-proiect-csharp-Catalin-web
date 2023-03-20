using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class FileLogger : Logger
    {
        public void Log(string message)
        {
            File.AppendAllText(Constants.LogFilePath, Environment.NewLine + message);
        }
    }
}
