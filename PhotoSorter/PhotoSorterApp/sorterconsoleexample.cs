using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorterApp
{
    
        class Program
        {
        public void Main(string[] args)
        {
            string fileExtention = "dng";
            //new copy location of files
            string destinationPath = @"D:\PhotoGrouping\outputDNG";
            //original location of files
            string rootPath = @"D:\PhotoGrouping\2019-03";

            //type of files we are looking for.
            var allFiles = Directory.GetFiles(rootPath, $"*.{fileExtention}", SearchOption.AllDirectories);

            List<string> fileNames8Symbols = new List<string>();
            List<string> newDirectoryNames = new List<string>();
            List<string> filterFileNames = new List<string>();

            //write to list substringed file names only first 8 symbols
            foreach (string file in allFiles)
            {
                fileNames8Symbols.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 8));
            }

            foreach (var names in fileNames8Symbols.Distinct())
            {
                //years\month
                //newDirectoryNames.Add(Path.Combine(destinationPath, names.Substring(0, 4),names.Substring(4, 2)));
                newDirectoryNames.Add(Path.Combine(names.Substring(0, 4), names.Substring(4, 2)));
                filterFileNames.Add($"{names.Substring(0, 4) }{names.Substring(4, 2)}");
            }

            //create all needed directories
            foreach (var directoryName in newDirectoryNames)
            {
                Directory.CreateDirectory(Path.Combine(destinationPath, directoryName));
            }

            int i = 0;
            foreach (var newDirectoryName in newDirectoryNames)
            {
                var files = Directory.GetFiles(rootPath, $"{newDirectoryName}*.{fileExtention}".Replace(@"\", ""), SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    File.Copy(file, $"{Path.Combine(destinationPath, newDirectoryName)}\\{Path.GetFileName(file)}", true);
                    Console.Clear();
                    Console.Write("{0}-{1}", Path.GetFileNameWithoutExtension(file), i++);
                }

            }


            //string[] dirs = Directory.GetDirectories(rootPath,"*",SearchOption.AllDirectories);

            //foreach (string dir in dirs)
            //{
            //    Console.WriteLine(dir);
            //}

            // var files = Directory.GetFiles(rootPath, "*.mp4", SearchOption.AllDirectories);

            //List<string> test = new List<string>();


            //foreach (string file in files)
            //{
            //    //Console.WriteLine(Path.GetFileName(file));
            //    //Console.WriteLine(Path.GetFileNameWithoutExtension(file).Substring(0,8).Distinct().ToList());


            //    test.Add(Path.GetFileNameWithoutExtension(file).Substring(0, 8));

            //    var strings = Path.GetFileNameWithoutExtension(file);
            //    //string[] unique = strings.Distinct().ToArray();

            //    //var info = new FileInfo(file);
            //    //Console.WriteLine(info.LastWriteTime);

            //}

            //foreach (var tes in test)
            //{
            //    Console.WriteLine(tes.Substring(0,4));
            //    Console.WriteLine(tes.Substring(4, 2));
            //    Console.WriteLine(tes.Substring(6, 2));
            //    Console.WriteLine("#");

            //}




            //creating directory
            //string newPath = @"D:\PhotoGrouping\2\3\5\8";
            //bool directoryExists = Directory.Exists(newPath);

            //if (directoryExists)
            //{
            //    Console.WriteLine("Directory Exists");
            //}
            //else
            //{
            //    Console.WriteLine("Directory not Exists");
            //    Directory.CreateDirectory(newPath);
            //}

            Console.ReadLine();
        }
    }
    
}
