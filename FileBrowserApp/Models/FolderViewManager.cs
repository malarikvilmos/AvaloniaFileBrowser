using Avalonia.Collections;
using FileBrowserApp.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IRO.FileIO.ImprovedFileOperations;

namespace FileBrowserApp.Models
{
    public static class FolderViewManager
    {
        public static void UpdateFiles(AvaloniaList<FolderViewItem> list, string path, bool showHidden = false)
        {
            list.Clear();

            DirectoryInfo node = new(path);
            list.Add(new FolderViewItem(node.Parent, "...", null, null, ""));

            
            var files = node.GetFileSystemInfos();
            foreach (FileSystemInfo item in files)
            {
                var newItem = new FolderViewItem(
                        item,
                        item.Name,
                        item as DirectoryInfo == null ? (int?)((FileInfo)item).Length : null,
                        item.LastWriteTime,
                        item as DirectoryInfo == null ? item.Extension : "");

                if (!showHidden)
                {
                    if (!item.Attributes.HasFlag(FileAttributes.Hidden))
                        list.Add(newItem);           
                }
                else list.Add(newItem);
            }
        }

        public static void Copy(FolderViewViewModel source, FolderViewViewModel other)
        {
            Parallel.ForEach(source.SelectedItems, item =>
            {
                if (File.Exists(item.Reference?.FullName))
                    File.Copy(item.Reference.FullName, other.CurrentPath + '\\' + item.Name);
                else if (item.Reference is DirectoryInfo dirinfo)
                {
                    ImprovedFile impFile = new();
                    impFile.Copy(item.Reference.FullName, other.CurrentPath + '\\' + item.Name);
                } 
            });

            UpdateFiles(other.FolderViewItems, other.CurrentPath, other.ShowHidden);
        }
        public static void Move(FolderViewViewModel source, FolderViewViewModel other)
        {
            Parallel.ForEach(source.SelectedItems, item =>
            {
                if (File.Exists(item.Reference?.FullName))
                    File.Move(item.Reference.FullName, other.CurrentPath + '\\' + item.Name);
                else if (item.Reference is DirectoryInfo dirinfo)
                {
                    ImprovedFile impFile = new();
                    impFile.Copy(item.Reference.FullName, other.CurrentPath + '\\' + item.Name);
                    impFile.TryDelete(item.Reference.FullName);
                }
            });

            UpdateFiles(source.FolderViewItems, source.CurrentPath, source.ShowHidden);
            UpdateFiles(other.FolderViewItems, other.CurrentPath, other.ShowHidden);
        }

        public static void Remove(FolderViewViewModel source)
        {
            Parallel.ForEach(source.SelectedItems, item =>
            {
                if (File.Exists(item.Reference?.FullName))
                    File.Delete(item.Reference.FullName);
                else if (item.Reference is DirectoryInfo dirinfo)
                {
                    ImprovedFile impFile = new();
                    impFile.TryDelete(item.Reference.FullName);
                }
            });

            UpdateFiles(source.FolderViewItems, source.CurrentPath, source.ShowHidden);
        }
    }
}
