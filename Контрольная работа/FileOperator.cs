using System.IO;

namespace ControlWork
{
    public class FileOperator
    {
        private const string TxtFilter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        private const string JsonFilter = "json files (*.json)|*.json|All files (*.*)|*.*";

        public FileOperator()
        {
        }

        public string GetFileContentByPath(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public string WriteFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
            return filePath;
        }
    }
}
