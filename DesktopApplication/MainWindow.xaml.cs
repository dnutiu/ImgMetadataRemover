using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static readonly List<string> files = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("adsd");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            files.Add("aaaaaxxx");
            PickedFilesView.ItemsSource = files;
        }

        private void PickFilesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("PickFilesBtn_OnClick - On Click");
        }

        private void CleanFilesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CleanFilesBtn_OnClick - On Click");

        }
    }
}