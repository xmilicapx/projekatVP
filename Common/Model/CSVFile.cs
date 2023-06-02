using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class CSVFile
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public MemoryStream MemoryStream { get; set; }
    }
}
