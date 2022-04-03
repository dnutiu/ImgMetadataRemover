using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Image.Core;
using ImageMagick;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpApplication
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            // TODO: Fa butonul sa fie mai fain.
        }

        public ObservableCollection<ImageFileInfo> Images { get; } = new ObservableCollection<ImageFileInfo>();

        private async void PickFilesButton_Click(object sender, RoutedEventArgs e)
        {
            // Clean current images
            Images.Clear();
            // Pick files
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add("*");
            var files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
                // Application now has read/write access to the picked file(s)
            {
                foreach (var file in files)
                    // Add image to current structure.
                {
                    Images.Add(await LoadImageInfo(file));
                }
            }
        }

        public async Task<ImageFileInfo> LoadImageInfo(StorageFile file)
        {
            var imageFileInfo = new ImageFileInfo(file.Path, file.Name);
            var bitmapImage = new BitmapImage();
            using (IRandomAccessStream fileStream = await file.OpenReadAsync())
            {
                // Create a bitmap to be the image source.
                bitmapImage.SetSource(fileStream);
            }

            imageFileInfo.ImageSource = bitmapImage;
            // todo: clean up
            imageFileInfo.StorageFile = file;

            return imageFileInfo;
        }

        private async void CleanFilesButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var image in Images)
            {
                try
                {
                    var fileStream = await image.StorageFile.OpenAsync(FileAccessMode.ReadWrite);
                    fileStream.Seek(0);
                    var remover = new ExifRemoverAndCompressor(new MagickImage(fileStream.AsStream()),
                        LosslessCompressor.Instance);
                    remover.CleanImage();
                    fileStream.Seek(0);
                    remover.SaveImage(fileStream.AsStream());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }
}