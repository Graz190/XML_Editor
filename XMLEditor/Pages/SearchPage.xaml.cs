﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using XMLEditor.Core;
using XMLEditor.Setting;
using XMLEditor.ViewModel;

namespace XMLEditor
{
    /// <summary>
    /// Interaktionslogik für SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        BackgroundWorker worker1 = new BackgroundWorker();
        MainWindow window;
        XmlSearchCore searchCore;
        MainViewModel mainViewModel;
        public SearchPage(MainWindow window)
        {
            InitializeComponent();
            worker1.DoWork += Worker1_DoWork;
            worker1.RunWorkerCompleted += Worker1_RunWorkerCompleted;
            this.window = window;
            searchCore = new XmlSearchCore(window);
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }
        private void runSearch(object sender, RoutedEventArgs e)
        {
            if (PropertySetting.read_Setting(SettingsName.ResultFile) != "")
            {
                if (this.openFilePathBox.Text == "")
                    window.changeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
                if (this.searchTermBox.Text.Equals("")) window.changeInformationText(ColorText.error, "Bitte geben sie einen gesuchten Term an");
                else
                {
                    PropertySetting.save_Setting(SettingsName.FilePath, this.openFilePathBox.Text);
                    PropertySetting.save_Setting(SettingsName.searchTerm, this.searchTermBox.Text);
                    if (!worker1.IsBusy)
                    {
                        worker1.RunWorkerAsync();
                    }
                }
            }
            else
            {
                window.changeInformationText(ColorText.error, "Bitte wählen sie die Xml Datei aus");
            }
        }
        private void openFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "XML Dateien (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true) openFilePathBox.Text = openFileDialog.FileName;

        }
        private void shutdownApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
            this.Dispatcher.Invoke(() =>
            {
                this.searchTermLabel.Content = "";
                this.searchTermLabel.Content = PropertySetting.read_Setting(SettingsName.searchTerm);
                window.changeInformationText(ColorText.loading, "Bitte warten Datei wird verarbeitet");
                mainViewModel = DataContext as MainViewModel;
            });
            searchCore.basicSearch(SettingsName.searchTerm, mainViewModel);
            
        }

    }
}
