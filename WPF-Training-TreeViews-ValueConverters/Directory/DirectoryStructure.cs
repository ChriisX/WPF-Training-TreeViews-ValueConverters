using System.Collections.Generic;
using WPF_Training_TreeViews_ValueConverters.Directory.Data;
using System.Linq;

namespace WPF_Training_TreeViews_ValueConverters.Directory
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
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

        public static List<DirectoryItem> GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, ItemType = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Get the directories top-level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            List<DirectoryItem> items = new List<DirectoryItem>();

            #region Get Directories

            try
            {
                string[] dirs = System.IO.Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, ItemType = DirectoryItemType.Folder }));
                }
            }
            catch { } // Bad practice

            #endregion

            #region Get Files

            try
            {
                string[] fs = System.IO.Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, ItemType = DirectoryItemType.File }));
                }
            }
            catch { } // Bad practice

            #endregion

            return items;
        }
    }
}
