using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using System.IO;

namespace emlViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private string _fileToOpen = "";
        public string FileToOpen 
        {
            get => _fileToOpen;
            set {
                _fileToOpen = value;
            } 
        }

        public  List<string> text = new List<string>();
        public async void openFile(Window window)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Veuillez sélectionner un fichier";
            fileDialog.AllowMultiple = false;

            var file = await fileDialog.ShowAsync(window);

            if (file != null)
            {
                FileToOpen = string.Join(" ", file); ;
            }

            checkFileExtension(FileToOpen);
            parseFile(FileToOpen);
        }

        private void checkFileExtension(string filePath)
        {
            string extension = filePath.Split(".")[1];

            if (extension != "eml") {
                FileToOpen = "";
                Console.WriteLine("Wrong file format");
            }

            Console.WriteLine(filePath);
        }

        public void parseFile(string filePath)
        {
             try
            {
             
                string[] lines = System.IO.File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line == "") break;

                    if (line[0] == ' ') {
                         text.Insert(text.Count - 1, $" {line}");
                    } else {
                        text.Add(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}
