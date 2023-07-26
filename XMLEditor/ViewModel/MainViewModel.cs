using System.Collections.ObjectModel;
using XMLEditor.Pages;

namespace XMLEditor.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<ValueID> DataGridItems { get; set; }
        public MainViewModel() { 
        DataGridItems = new ObservableCollection<ValueID>();
        }
    }
}
