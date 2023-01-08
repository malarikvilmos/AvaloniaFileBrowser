using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using FileBrowserApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FileBrowserApp.Views
{
    public partial class PowerDataGrid : DataGrid, IStyleable
    {
        Type IStyleable.StyleKey => typeof(DataGrid);

        public static readonly StyledProperty<bool> IsDoubleClickedProperty = AvaloniaProperty.Register<PowerDataGrid, bool>(nameof(IsDoubleClicked));

        public bool IsDoubleClicked
        {
            get { return GetValue(IsDoubleClickedProperty); }
            set { SetValue(IsDoubleClickedProperty, value); }
        }

        public static readonly StyledProperty<List<FolderViewItem>> CurrentlySelectedItemsProperty 
            = AvaloniaProperty.Register<PowerDataGrid, List<FolderViewItem>>(nameof(CurrentlySelectedItems));

        public List<FolderViewItem> CurrentlySelectedItems
        {
            get { return GetValue(CurrentlySelectedItemsProperty); }
            set { SetValue(CurrentlySelectedItemsProperty, value); }
        }

        public PowerDataGrid()
        {
            InitializeComponent();
            DoubleTapped += (sender, args) => IsDoubleClicked = true;
            SelectionChanged += (sender, args) => CurrentlySelectedItems = SelectedItems.Cast<FolderViewItem>().ToList();
        }
    }
}
