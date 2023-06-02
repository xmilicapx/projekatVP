﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp
{
    internal class FileInUseChecker
    {
        private static int TIMEOUT = 10;
        private static int WAIT_MILLISECONDS = 500;

        public static bool IsFileInUse(string filePath)
        {
            int cnt = 0;
            while (cnt < TIMEOUT && CheckIsFileInUse(new FileInfo(filePath)))
            {
                Thread.Sleep(WAIT_MILLISECONDS);
                cnt++;
            }
            if (cnt >= TIMEOUT)
                return true;
            return false;
        }
        private static bool CheckIsFileInUse(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
            return false;
        }
    }
}
