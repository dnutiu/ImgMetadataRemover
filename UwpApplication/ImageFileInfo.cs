using System.ComponentModel;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace UwpApplication
{
    public sealed class ImageFileInfo : INotifyPropertyChanged
    {
        public StorageFile StorageFile;

        public ImageFileInfo(string path, string name)
        {
            FullImagePath = path;
            Name = name;
        }

        public BitmapImage ImageSource { get; set; }
        public string FullImagePath { get; set; }
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}