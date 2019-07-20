using System.ComponentModel;

namespace WPF_Training_TreeViews_ValueConverters.Directory.ViewModels
{
    /// <summary>
    /// A base view model tthat fires property changed events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}