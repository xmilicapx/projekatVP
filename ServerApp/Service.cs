using Common.Interfaces;
using Common.Model;
using Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;

namespace ServerApp
{
    public class Service : IService
    {
        InMemoryDatabase database = new InMemoryDatabase();
        [OperationBehavior(AutoDisposeParameters = true)]
        public List<CSVFile> SendXML(MemoryStream memoryStream, string fileName)
        {
            List<Load> loadList = new List<Load>();
            try
            {
                memoryStream.Position = 0;
                XDocument xdoc = XDocument.Load(memoryStream);//ucitavanje memory streama u xmldocument
                List<XElement> xmlLoads = xdoc.Descendants("row").ToList();//razdvajanje delova xml-a 

                foreach (XElement rowElement in xmlLoads)
                {

                    Load load = Load.CreateLoad(rowElement, out string errorMessage);//parsiranje dela xml-a u jedan load objekat
                    if (load == null)
                    {//u slucaju nevalidnog xml-a kreira se load objekat - kada nije validan 1 load objekat
                        Audit audit = new Audit(MessageType.Error, errorMessage);
                        database.AddAudit(audit);
                    }
                    else
                    {
                        loadList.Add(load);
                        database.AddLoad(load);
                    }
                }

                List<CSVFile> retValue = Program.CreateCSV(loadList);
                ImportedFile importedFile = new ImportedFile(fileName);
                database.AddImportedFile(importedFile);
                return retValue;
            }
            catch (Exception e)
            {//u slucaju nevalidnog xml-a kreira se load objekat - kada nije validan ceo xml
                Audit audit = new Audit(MessageType.Error, e.Message);
                database.AddAudit(audit);
                return null;
            }
            finally
            {
                memoryStream.Dispose();
            }
        }
    }
}
