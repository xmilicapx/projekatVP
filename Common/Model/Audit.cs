using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Audit
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public Audit(MessageType type, string message)
        {
            Timestamp = DateTime.Now;
            MessageType = type;
            Message = message;
            Id = IDGenerator.GetAuditId();
        }
    }
}
