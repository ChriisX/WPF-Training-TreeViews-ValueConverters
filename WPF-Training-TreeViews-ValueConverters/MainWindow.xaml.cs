using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WPF_Training_TreeViews_ValueConverters.Directory.ViewModels.Base;

namespace WPF_Training_TreeViews_ValueConverters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new DirectoryStructureViewModel();
        }
    }
}
