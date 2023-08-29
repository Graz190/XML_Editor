using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLEditor.Pages;

namespace XMLEditor.ViewModel
{
    public class ListViewModel : ViewModelBase
    {
        public ObservableCollection<ValueID> DataGridItems { get; set; }
        public ViewModelBase CurrentViewModel { get; }
        public ListViewModel()=>DataGridItems = new ObservableCollection<ValueID>();

    }
}
