using System;
using System.IO;

namespace FileBrowserApp.Models
{
    public class FolderViewItem
    {
        public FileSystemInfo? Reference { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public int? Size { get; set; }
        public DateTime? Date { get; set; } 

        public FolderViewItem(FileSystemInfo? reference, string name, int? size, DateTime? date, string extension)
        {
            Reference = reference;
            Name = name;
            Size = size;
            Date = date;
            Extension = extension;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
