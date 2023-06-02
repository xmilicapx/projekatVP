using Common.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public delegate List<CSVFile> CreateCSV(List<Load> loads);
    internal class Program
    {
        public static CreateCSV CreateCSV;
        static void Main(string[] args)
        {
            if (ConfigurationManager.AppSettings["mode"] == "0")
            {
                CreateCSV = new CreateCSV(CSVHelper.CreateOneCSV);
            }
            else
            {
                CreateCSV = new CreateCSV(CSVHelper.CreateMultipleCSVs);
            }
            using (ServiceHost sh = new ServiceHost(typeof(Service)))
            {
                sh.Open();
                Console.WriteLine("Pritisnite Enter za zaustavljanje servisa.");
                Console.ReadLine();
            }
        }

    }
}
