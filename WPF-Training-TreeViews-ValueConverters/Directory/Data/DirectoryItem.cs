using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Training_TreeViews_ValueConverters.Directory.Data
{
    /// <summary>
    /// Information about a directory item such as a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return ItemType == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);
            }
        }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType ItemType { get; set; }
    }
}
