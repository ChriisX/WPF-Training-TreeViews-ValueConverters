using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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

            foreach(string drive in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive
                };

                // Null item to make the expand icon available
                item.Items.Add(null);

                // Listen out for item being expanded
                item.Expanded += Item_Expanded;

                FolderView.Items.Add(item);
            }
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            TreeViewItem item = (TreeViewItem)sender;

            // If it doesn't only contain the null item
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }
            

            // Remove null item
            item.Items.Clear();

            string fullPath = (string)item.Tag;
            #endregion

            #region Get Directories

            List<string> directories = new List<string>();

            try
            {
                string[] dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { } // Bad practice

            directories.ForEach(directoryPath =>
            {
                TreeViewItem subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                subItem.Items.Add(null);

                subItem.Expanded += Item_Expanded;

                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            List<string> files = new List<string>();

            try
            {
                string[] fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch { } // Bad practice

            files.ForEach(filePath =>
            {
                TreeViewItem subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });

            #endregion
        }

        // Finds the file or folder name from the full path
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            string normalizedPath = path.Replace('/', '\\');

            string[] splitPath = normalizedPath.Split('\\');

            return splitPath[splitPath.Length - 1];
        }
    }
}
