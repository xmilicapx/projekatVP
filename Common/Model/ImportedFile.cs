using Common.Interfaces;

namespace Common.Model
{
    public class ImportedFile
    {
        public ImportedFile(string fileName)
        {
            Id = IDGenerator.GetLoadId();
            FileName = fileName;
        }

        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
