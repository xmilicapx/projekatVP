using Common.Interfaces;
using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClientApp
{
    public class WCFClient : IService
    {
        IService service;
        public WCFClient()
        {
            ChannelFactory<IService> channelFactory = new ChannelFactory<IService>("Service");
            service = channelFactory.CreateChannel();
        }
        public List<CSVFile> SendXML(MemoryStream memoryStream, string fileName)
        {
            return service.SendXML(memoryStream, fileName);
        }
    }
}
