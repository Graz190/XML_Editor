using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLEditor.Core;
using XMLEditor.ViewModel;

namespace XMLEditor.Pages
{
    /// <summary>
    /// Interaktionslogik für SpecialSearchPage.xaml
    /// </summary>
    public partial class SpecialSearchPage : Page
    {
        readonly BackgroundWorker worker1 = new BackgroundWorker();
        readonly MainWindow window;
        readonly XmlSearchCore searchCore;
        private MainViewModel mainViewModel;
        public SpecialSearchPage(MainWindow window)
        {
            InitializeComponent();
            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
            this.window = window;
            this.mainViewModel = window.ViewModel;
            searchCore = new XmlSearchCore(window);
            DataContext = mainViewModel;
        }
        private void Worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
