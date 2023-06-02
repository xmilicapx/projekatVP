using Common.Model;
using System.Collections.Generic;

namespace Database
{
    public class InMemoryDatabase
    {
        private static Dictionary<long, Audit> auditDatabase = new Dictionary<long, Audit>();
        private static Dictionary<long, ImportedFile> importedFilesDatabase = new Dictionary<long, ImportedFile>();
        private static Dictionary<long, Load> loadsDatabase = new Dictionary<long, Load>();
        private static object lockObject = new object();
        public bool AddAudit(Audit audit)
        {
            lock (lockObject)
            {
                auditDatabase[audit.Id] = audit;
                return true;
            }
        }
        public bool AddLoad(Load load)
        {
            lock (lockObject)
            {
                foreach (var item in loadsDatabase)
                {
                    if (item.Value.Timestamp == load.Timestamp)
                    {
                        return false;
                    }
                }
                loadsDatabase[load.Id] = load;
                return true;
            }
        }
        public bool AddImportedFile(ImportedFile file)
        {
            lock (lockObject)
            {
                importedFilesDatabase[file.Id] = file;
                return true;
            }
        }
    }
}
