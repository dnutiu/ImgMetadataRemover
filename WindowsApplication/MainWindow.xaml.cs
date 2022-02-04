using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsApplication
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            UpdateNumberOfFilesText(0);
        }

        private async void PickFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();

            // Get the current window's HWND by passing in the Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                List<string> fileNames = new List<string>();
                var folderItems = await folder.GetItemsAsync();
                foreach (var item in folderItems)
                {
                    if (item.Attributes == Windows.Storage.FileAttributes.Archive || item.Attributes == Windows.Storage.FileAttributes.Normal)
                    {
                        fileNames.Add(item.Name);
                    }
                }
                FilesListView.ItemsSource = fileNames;
                UpdateNumberOfFilesText(fileNames.Count);
            }
        }

        private void UpdateNumberOfFilesText(int nrOfFiles)
        {
            NumberOfFiles.Text = $"Total files {nrOfFiles}.";
        }
    }
}
