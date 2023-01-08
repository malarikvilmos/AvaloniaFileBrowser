namespace FileBrowserApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static FolderViewViewModel LeftVM { get; }
        public static FolderViewViewModel RightVM { get; }

        static MainWindowViewModel()
        {
            LeftVM = new();
            RightVM = new();

            LeftVM.Other = RightVM;
            RightVM.Other = LeftVM;
        }
    }
}
