using System;
using System.IO;
using Microsoft.Win32;
using FileEditor.Models;

namespace FileEditor.Services
{
    public class FileOperator
    {
        private const string TxtFilter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        private const string HtmlFilter = "html files (*.html)|*.html|All files (*.*)|*.*|txt files (*.txt)|*.txt";
        public FileOperator()
        {
        }

        public string GetTextFileContent()
        {
            string filePath = GetPathByOpen(TxtFilter);
            return File.ReadAllText(filePath);
        }

        public string GetPathByOpen(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = filter};
            if (openFileDialog.ShowDialog() != true)
            {
                throw new Exception("Operation was failed");
            }
            return openFileDialog.FileName;
        }

        public string GetPathBySaveAs(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog {Filter = filter};
            if (saveFileDialog.ShowDialog() != true)
            {
                throw new Exception("Operation was failed");
            }
            return saveFileDialog.FileName;
        }

        public string SaveAsTxtFile(string content)
        {
            string filePath = GetPathBySaveAs(TxtFilter);
            File.WriteAllText(filePath, content);
            return filePath;
        }

        public string SaveAsHtmlFile(string content)
        {
            string filePath = GetPathBySaveAs(HtmlFilter);
            content = new HtmlGenerator().ConvertToHtml(content);
            File.WriteAllText(filePath, content);
            return filePath;
        }

        public FileData GetFileInfo(string filePath = null)
        {
            filePath ??= GetPathByOpen(null);
            FileInfo info = new FileInfo(filePath);
            return new FileData(info.Name, info.Length, info.CreationTimeUtc);
        }

        public string SaveAsTxtFileWithFileInfo(string content)
        {
            string filePath = GetPathBySaveAs(TxtFilter);
            string fileInfo = GetFileInfo(filePath).GetFileInfo();
            content = $"{content}\n{fileInfo}";
            File.WriteAllText(filePath, content);
            return filePath;
        }
    }
}
