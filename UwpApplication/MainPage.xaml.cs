using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Console.WriteLine("Main Page");
            this.Loaded += LoadedHandler;
        }

        private async void LoadedHandler(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
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
            }
        }
    }
}
