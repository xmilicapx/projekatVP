using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public static class IDGenerator
    {
        private static int LoadIdInterator = 0;
        private static int AuditIdInterator = 0;
        private static int ImportedFileIdInterator = 0;

        public static int GetLoadId()
        {
            LoadIdInterator++;
            return LoadIdInterator;
        }
        public static int GetAuditId()
        {
            AuditIdInterator++;
            return AuditIdInterator;
        }
        public static int GetImportedFileId()
        {
            ImportedFileIdInterator++;
            return ImportedFileIdInterator;
        }
    }
}
