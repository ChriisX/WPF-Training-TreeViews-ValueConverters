using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPF_Training_TreeViews_ValueConverters.Directory.Data;

namespace WPF_Training_TreeViews_ValueConverters.Directory.ViewModels
{
    /// <summary>
    /// View model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            ExpandCommand = new RelayCommand(Expand);
            FullPath = fullPath;
            Type = type;

            ClearChildren();
        }

        #region Public Properties
        public string FullPath { get; set; }
        public string Name {
            get
            {
                return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(FullPath);
            }
        }
        public DirectoryItemType Type { get; set; }
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }
        public bool CanExpand { get { return Type != DirectoryItemType.File; } }
        public bool IsExpanded
        {
            get
            {
                return Children?.Count(f => f != null) > 0;
            }
            set
            {
                if(value)
                {
                    Expand();
                }
                else
                {
                    ClearChildren();
                }
            }
        }
        #endregion

        #region Public Commands
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Helper Methods
        private void ClearChildren()
        {
            Children = new ObservableCollection<DirectoryItemViewModel>();
            if (Type != DirectoryItemType.File)
            {
                Children.Add(null);
            }
        }
        #endregion

        private void Expand()
        {
            if (Type == DirectoryItemType.File) return;

            List<DirectoryItem> children = DirectoryStructure.GetDirectoryContents(FullPath);

            Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.ItemType)));
        }
    }
}
