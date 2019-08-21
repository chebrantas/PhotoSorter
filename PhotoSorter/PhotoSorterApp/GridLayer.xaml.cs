using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//using System.Windows.Shapes;
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
            string fileExtention = "*";
            string CopyOrMoveFiles = "";
            string fileNamePunctuationMark = "";
            string fileNamePrefixMark = "";
            int filesAmount = 0;

            //status bar start runing
            //StatusProgresBar.IsIndeterminate = true;

            if (txtDestinationPath.Text != "" && txtSourcePath.Text != "")
            {


                //original location of files
                string rootPath = @txtDestinationPath.Text;

                //new copy location of files
                string destinationPath = @txtSourcePath.Text;


                //if checkbox checked looking for all files, if not checked then looking for exactly selected files
                if (chbAll.IsChecked == false)
                {
                    if (radioJpg.IsChecked == true)
                    {
                        fileExtention = radioJpg.Content.ToString();
                    }
                    else if (radioPng.IsChecked == true)
                    {
                        fileExtention = radioPng.Content.ToString();
                    }
                    else if (radioMp4.IsChecked == true)
                    {
                        fileExtention = radioMp4.Content.ToString();
                    }
                }


                //checkbox for selecting Copy or Move original files
                if (radioPhotoCopy.IsChecked == true)
                {
                    CopyOrMoveFiles = "Copy";
                }
                else if (radioPhotoMove.IsChecked == true)
                {
                    CopyOrMoveFiles = "Move";
                }


                //type of files we are looking for.
                var allFiles = Directory.GetFiles(rootPath, $"*{fileExtention}", SearchOption.AllDirectories);
                filesAmount = allFiles.Count();
                //write all file names without extensions
                List<string> fileNames = new List<string>();
                //new directory names from distinct file names that showes dates
                List<string> newDirectoryNames = new List<string>();
                List<string> filterFileNames = new List<string>();


                //check what type of file name we have

                if (radioOne.IsChecked == true)
                {

                    //write to list substringed file names only first 8 symbols file name TYPE '20190320__13476'
                    foreach (string file in allFiles)
                    {
                        //we need only year\month so substring 6 symbols
                        fileNames.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 6));
                    }

                    foreach (var names in fileNames.Distinct())
                    {
                        //write new Directories years\month
                        newDirectoryNames.Add(Path.Combine(names.Substring(0, 4), names.Substring(4, 2)));
                        //filterFileNames.Add($"{names.Substring(0, 4) }{names.Substring(4, 2)}");
                    }

                }
                else if (radioTwo.IsChecked == true)
                {
                    //write to list substringed file names only first 8 symbols file name TYPE '2019-03-20__13476'
                    fileNamePunctuationMark = "-";
                    foreach (string file in allFiles)
                    {
                        //we need only year\month so substring 6 symbols
                        fileNames.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 7));

                    }

                    foreach (var names in fileNames.Distinct())
                    {
                        //write new Directories years\month
                        newDirectoryNames.Add(Path.Combine(names.Substring(0, 4), names.Substring(5, 2)));
                    }
                }
                else if (radioThree.IsChecked == true)
                {
                    //write to list substringed file names only first 8 symbols file name TYPE '2019.03.20__13476'
                    fileNamePunctuationMark = ".";
                    foreach (string file in allFiles)
                    {
                        //we need only year\month so substring 6 symbols
                        fileNames.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 7));

                    }

                    foreach (var names in fileNames.Distinct())
                    {
                        //write new Directories years\month
                        newDirectoryNames.Add(Path.Combine(names.Substring(0, 4), names.Substring(5, 2)));
                    }
                }
                else if (radioFour.IsChecked == true)
                {
                    //write to list substringed file names only first 8 symbols file name TYPE 'IMG_20190805_101154'
                    fileNamePunctuationMark = "";
                    fileNamePrefixMark = "IMG_";
                    foreach (string file in allFiles)
                    {
                        //we need only year\month so substring 6 symbols
                        fileNames.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 10));

                    }

                    foreach (var names in fileNames.Distinct())
                    {
                        //write new Directories years\month
                        newDirectoryNames.Add(Path.Combine(names.Substring(4, 4), names.Substring(8, 2)));
                    }
                }



                //create all needed directories
                foreach (var directoryName in newDirectoryNames)
                {
                    Directory.CreateDirectory(Path.Combine(destinationPath, directoryName));
                }

                int i = 0;


                //if selected Copy to.. makes copy of original files
                if (CopyOrMoveFiles == "Copy")
                {

                    foreach (var newDirectoryName in newDirectoryNames)
                    {
                        //looking for files like 201801*.jpg in directory 
                        var files = Directory.GetFiles(rootPath, $"{fileNamePrefixMark}{newDirectoryName}*{fileExtention}".Replace(@"\", fileNamePunctuationMark), SearchOption.AllDirectories);

                        foreach (var file in files)
                        {
                            //true for Owervrite true/false
                            File.Copy(file, $"{Path.Combine(destinationPath, newDirectoryName)}\\{Path.GetFileName(file)}", true);
                        }
                    }

                }
                //if selected Move to.. moves all original files
                else if (CopyOrMoveFiles == "Move")
                {
                    foreach (var newDirectoryName in newDirectoryNames)
                    {
                        //looking for files like 201801*.jpg in directory 
                        var files = Directory.GetFiles(rootPath, $"{fileNamePrefixMark}{newDirectoryName}*{fileExtention}".Replace(@"\", fileNamePunctuationMark), SearchOption.AllDirectories);

                        foreach (var file in files)
                        {
                            File.Move(file, $"{Path.Combine(destinationPath, newDirectoryName)}\\{Path.GetFileName(file)}");
                        }
                    }
                }

                //check if all files done and stop statusbar

                //StatusProgresBar.IsIndeterminate = false;



            }//make browse boxes red if no folder selected
            else if (txtDestinationPath.Text == "")
            {
                txtTitleDestination.Foreground = Brushes.Red;
            }
            else if (txtSourcePath.Text == "")
            {
                txtTitleSource.Foreground = Brushes.Red;
            }

        }


        //Choose Destination folder
        private void BtnChooseDestination_Click(object sender, RoutedEventArgs e)
        {
            SetFolder(txtDestinationPath);
            txtTitleDestination.Foreground = Brushes.Black;
        }

        //Choose Source folder
        private void BtnChooseSource_Click(object sender, RoutedEventArgs e)
        {
            SetFolder(txtSourcePath);
            txtTitleSource.Foreground = Brushes.Black;
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


        private void ChbAll_Checked(object sender, RoutedEventArgs e)
        {
            radioJpg.IsEnabled = radioPng.IsEnabled = radioMp4.IsEnabled = false;
        }

        private void ChbAll_Unchecked(object sender, RoutedEventArgs e)
        {
            radioJpg.IsEnabled = radioPng.IsEnabled = radioMp4.IsEnabled = true;
        }
    }
}
