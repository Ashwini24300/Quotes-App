using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace QuotesApplivcation.LOGGING_TASK
{
    public class Logger
    {

        // Singleton Logger Instance :
        //------------------------------------------------------------------
        private Logger() { }

        private static Logger Instance = new Logger();
        public static Logger GetLogger() { return Instance; }

        // Method to write log :
        //------------------------------------------------------------------
        FileStream fs = null;
        StreamWriter streamWriter = null;
        string filepath = "C:\\SHUBHAM\\New folder\\quotesLog.txt";

        public void Log(string message)
        {
            // create fs.
            if (File.Exists(filepath))
                fs = new FileStream(filepath, FileMode.Append, FileAccess.Write);
            else
                fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);

            // create streamWriter for fs.
            streamWriter = new StreamWriter(fs);    

            // write using streamWriter
            streamWriter.WriteLine("AT Time : "+DateTime.Now.ToString() +"Message : "+message);

            // close streamWriter and fs.
            streamWriter.Close();
            fs.Close();

            // nullifying streamWriter and fs.
            streamWriter = null; fs = null;
        }
    }
}