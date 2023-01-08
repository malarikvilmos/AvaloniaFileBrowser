using Avalonia.Collections;
using FileBrowserApp.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using System.Reactive;

namespace FileBrowserApp.ViewModels
{
    public class FolderViewViewModel : ViewModelBase
    {
        public FolderViewViewModel Other { get; set; }

        public bool IsDoubleClicked
        {
            get => false;
            set
            {
                if (SelectedItems[^1].Reference is DirectoryInfo dirinfo) 
                    CurrentPath = dirinfo.FullName;
            }
        }

        private string currentPath;
        public string CurrentPath
        {
            get => currentPath;
            set
            {
                this.RaiseAndSetIfChanged(ref currentPath, value);
                FolderViewManager.UpdateFiles(FolderViewItems, value, ShowHidden);
            }
        }

        private DriveInfo selectedDrive;
        public DriveInfo SelectedDrive
        {
            get => selectedDrive;
            set
            {
                if (value != null)
                    selectedDrive = value;
                CurrentPath = SelectedDrive.Name;
            }
        }


        private List<FolderViewItem> selectedItems = new();
        public List<FolderViewItem> SelectedItems
        {
            get => selectedItems;
            set => selectedItems = value;
        }

        private AvaloniaList<FolderViewItem> folderViewItems = new();
        public AvaloniaList<FolderViewItem> FolderViewItems
        {
            get => folderViewItems;
            set => this.RaiseAndSetIfChanged(ref folderViewItems, value);
        }

        public static List<DriveInfo> Drives => new(DriveInfo.GetDrives());

        private bool showHidden = false;
        public bool ShowHidden
        {
            get => showHidden;
            set
            {
                this.RaiseAndSetIfChanged(ref showHidden, value);
                FolderViewManager.UpdateFiles(folderViewItems, CurrentPath, ShowHidden);
            }
        }

        public FolderViewViewModel()
        {
            selectedDrive = Drives[0];
            currentPath = selectedDrive.Name;
        }

        public ReactiveCommand<Unit, Unit> Rename => ReactiveCommand.Create(RenameCommand);
        private void RenameCommand()
        {
            System.Console.WriteLine("Rename");
        }

        public ReactiveCommand<Unit, Unit> Copy => ReactiveCommand.Create(CopyCommand);
        private void CopyCommand()
        {
            FolderViewManager.Copy(this, Other);
        }

        public ReactiveCommand<Unit, Unit> Move => ReactiveCommand.Create(MoveCommand);
        private void MoveCommand()
        {
            FolderViewManager.Move(this, Other);
        }

        public ReactiveCommand<Unit, Unit> Remove => ReactiveCommand.Create(RemoveCommand);
        private void RemoveCommand()
        {
            FolderViewManager.Remove(this);
        }
    }
}
