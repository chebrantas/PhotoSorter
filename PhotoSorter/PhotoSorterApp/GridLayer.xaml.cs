using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace PhotoSorterApp
{
    /// <summary>
    /// Interaction logic for GridLayer.xaml
    /// </summary>
    public partial class GridLayer : Window
    {
        public GridLayer()
        {
            InitializeComponent();
        }


        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            MainWindow test = new MainWindow();
            test.Show();
            StatusProgresBar.IsIndeterminate = true;
        }

        private void BtnChooseDestination_Click(object sender, RoutedEventArgs e)
        {
            SetFolder(txtDestinationPath);
        }

        private void BtnChooseSource_Click(object sender, RoutedEventArgs e)
        {
            SetFolder(txtSourcePath);
        }

        //set Path to choosen folder
        public void SetFolder(TextBox textBox)
        {
            string currentDirectory = @"C:\";
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.Title = "Choose Source folder";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = currentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            var result = dlg.ShowDialog();
            StatusProgresBar.IsIndeterminate = true;
            if (result == CommonFileDialogResult.Ok)
            {
                textBox.Text = dlg.FileName;
            }
            StatusProgresBar.IsIndeterminate = false;

        }

        private void BtnPhotoNameType_Click(object sender, RoutedEventArgs e)
        {
            txtTest.Text = @"C:\asikiske\poliaud\beleka";
        }
    }
}
