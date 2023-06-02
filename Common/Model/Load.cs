using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Model
{
    public class Load
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public float ForecastValue { get; set; }
        public float MeasuredValue { get; set; }
        public Load()
        {

        }

        private Load(DateTime timestamp, float forecastValue, float measuredValue)
        {
            Id = IDGenerator.GetLoadId();
            Timestamp = timestamp;
            ForecastValue = forecastValue;
            MeasuredValue = measuredValue;
        }

        public static Load CreateLoad(XElement xElement, out string errorMessage)
        {
            errorMessage = "";
            string timestampValueStr = xElement.Element("TIME_STAMP").Value;
            if (string.IsNullOrEmpty(timestampValueStr) ||
                !DateTime.TryParse(timestampValueStr, out DateTime timestamp))
            {
                errorMessage = "Greška u XML. TIME_STAMP nije validan";
                return null;
            }
            string forecastValueStr = xElement.Element("FORECAST_VALUE").Value;
            if (string.IsNullOrEmpty(forecastValueStr) ||
                !float.TryParse(forecastValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float forecastValue))
            {
                errorMessage = "Greška u XML. FORECAST_VALUE nije validan";
                return null;
            }

            string measuredValueStr = xElement.Element("MEASURED_VALUE").Value;
            if (string.IsNullOrEmpty(measuredValueStr) ||
                !float.TryParse(measuredValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float measuredValue))
            {
                errorMessage = "Greška u XML. MEASURED_VALUE nije validan";
                return null;
            }
            Load load = new Load(timestamp, forecastValue, measuredValue);
            return load;
        }

    }
}
