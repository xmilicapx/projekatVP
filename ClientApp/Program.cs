using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WCFClient service = new WCFClient();
            XMLListener listener = new XMLListener(service);
            listener.SendXML();
            listener.Listen();
            Console.ReadKey();
        }
    }
}
