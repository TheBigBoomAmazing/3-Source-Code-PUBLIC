using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.DDP.Common.CommonJob;
using System.Collections;
using eContract.Log;
using System.IO;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Net;
using Suzsoft.Smart.Data;

namespace eContract.DDP.Jobs
{
    public class TestJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            log("test", "D:\\log.txt");
        }

        private string logFile;
        private StreamWriter writer;
        private FileStream fileStream = null;
        
        public void log(string info,string fileName)
        {
            logFile = fileName;
            CreateDirectory(logFile);
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Create();
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(fileStream);
                }
                writer.WriteLine(DateTime.Now + ": " + info);

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public void CreateDirectory(string infoPath)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

    }
}
