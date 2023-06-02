using Common.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClientApp
{
    public class XMLListener
    {
        private string fileName;
        private string folderName;
        private FileSystemWatcher fileSystemWatcher;
        WCFClient wcfClient;
        public XMLListener(WCFClient service)
        {
            this.wcfClient = service;
            folderName = ConfigurationManager.AppSettings["folderName"];
            fileName = ConfigurationManager.AppSettings["fileName"];
            CreateFolderIfNotExists();
            CreateFileIfNotExists();
        }
        public void Listen()
        {
            fileSystemWatcher = new FileSystemWatcher(folderName)
            {
                IncludeSubdirectories = false,
                InternalBufferSize = 32768,
                Filter = fileName,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite,
            };
            fileSystemWatcher.Created += SendXML;
            fileSystemWatcher.Changed += SendXML;
        }

        private void SendXML(object sender, FileSystemEventArgs e)
        {
            SendXML();
        }

        public void SendXML()
        {
            try
            {
                //ucitavanje xml fajla u memory stream
                MemoryStream memoryStream = GetXML();
                if (memoryStream == null)
                {
                    Console.WriteLine("XML nije validan");
                }
                else
                {
                    List<CSVFile> files = wcfClient.SendXML(memoryStream, fileName);
                    //files sadrzi listu memory streamova koje treba upisati u file
                    if (files == null)
                    {
                        Console.WriteLine("Doslo je do greške");
                    }
                    else
                    {
                        foreach (CSVFile file in files)
                        {
                            string path = Path.Combine(folderName, file.Name);
                            using (file.MemoryStream)
                            {

                                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                                {
                                    file.MemoryStream.Position = 0;

                                    file.MemoryStream.CopyTo(fileStream);
                                }
                                Console.WriteLine("Uspešno napravljena datoteka: " + path);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private MemoryStream GetXML()
        {
            string path = Path.Combine(folderName, fileName);
            if (FileInUseChecker.IsFileInUse(path))
            {
                Console.WriteLine($"File {path} je u upotrebi od strane drugog procesa");
                return null;
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                MemoryStream memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                fileStream.Close();
                return memoryStream;
            }
        }
        private void CreateFolderIfNotExists()
        {
            if (string.IsNullOrEmpty(folderName))
            {
                folderName = "C:/Temp";
            }
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }
        private void CreateFileIfNotExists()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                folderName = "data.xml";
            }
            if (!File.Exists(Path.Combine(folderName, fileName)))
                File.Create(Path.Combine(folderName, fileName));
        }
    }
}
