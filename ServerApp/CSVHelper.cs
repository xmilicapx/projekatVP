using Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public static class CSVHelper
    {
        private static string GetFileName(DateTime date)
        {
            return "result_data_" + date.ToString("yyyy_MM_dd") + ".csv";
        }
        public static List<CSVFile> CreateOneCSV(List<Load> loads)
        {
            CSVFile cSVFile = new CSVFile();
            cSVFile.Name = "result_data.csv";
            cSVFile.MemoryStream = CreateCSV(loads);
            return new List<CSVFile>() { cSVFile };
        }
        private static MemoryStream CreateCSV(List<Load> loads)
        {
            string csvText = "DATE;TIME;FORECAST_VALUE;MEASURED_VALUE\n";
            foreach (Load load in loads)
            {
                csvText += $"{load.Timestamp.ToString("yyyy-MM-dd")};{load.Timestamp.ToString("HH:mm")};{load.ForecastValue.ToString(CultureInfo.InvariantCulture)};{load.MeasuredValue.ToString(CultureInfo.InvariantCulture)}\n";
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(csvText));
        }
        public static List<CSVFile> CreateMultipleCSVs(List<Load> loads)
        {
            List<CSVFile> retValue = new List<CSVFile>();

            Dictionary<DateTime, List<Load>> loadsDic = new Dictionary<DateTime, List<Load>>();
            foreach (Load load in loads)
            {
                if (loadsDic.ContainsKey(load.Timestamp.Date))
                    loadsDic[load.Timestamp.Date].Add(load);
                else
                    loadsDic[load.Timestamp.Date] = new List<Load> { load };
            }
            foreach (var item in loadsDic)
            {
                CSVFile cSVFile = new CSVFile();
                cSVFile.Name = GetFileName(item.Key);
                cSVFile.MemoryStream = CreateCSV(item.Value);
                retValue.Add(cSVFile);
            }
            return retValue;
        }
    }
}
