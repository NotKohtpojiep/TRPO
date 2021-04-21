using System;

namespace FileEditor.Models
{
    public class FileData
    {
        private long _length;
        private string _name;
        public long Length
        {
            get => _length;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Length value is incorrect");
                }
                _length = value;
            }
                
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name value is incorrect");
                }
                _name = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public FileData(string name, long length, DateTime createdAt)
        {
            Length = length;
            Name = name;
            CreatedAt = createdAt;
        }

        public string GetFileInfo()
        {
            return string.Join("\n",
                new string[] {$"File name: {Name}", $"File size: {Length} bytes", $"File created: {CreatedAt}"});
        }
    }
}
