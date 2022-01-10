using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using System.IO;
using HLIB.MailFormats;
using ReactiveUI;

namespace emlViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private string _fileToOpen = "";
        public string FileToOpen 
        {
            get => _fileToOpen;
            set {
                _fileToOpen = this.RaiseAndSetIfChanged(ref _fileToOpen, value);
            } 
        }

        
        private string _from = "";
        public string From 
        {
            get => _from;
            set {
                _from =  this.RaiseAndSetIfChanged(ref _from, value);
            } 
        }

        private string _to = "";
        public string To 
        {
            get => _to;
            set {
                _to =  this.RaiseAndSetIfChanged(ref _to, value);
            } 
        }

        private string _cc = "";
        public string Cc 
        {
            get => _cc;
            set {
                _cc =  this.RaiseAndSetIfChanged(ref _cc, value);
            } 
        }

        private string _subject = "";
        public string Subject 
        {
            get => _subject;
            set {
                _subject =  this.RaiseAndSetIfChanged(ref _subject, value);
            } 
        }

        private string _date = "";
        public string Date 
        {
            get => _date;
            set {
                _date =  this.RaiseAndSetIfChanged(ref _date, value);
            } 
        }

        private string _received = "";
        public string Received 
        {
            get => _received;
            set {
                _received =  this.RaiseAndSetIfChanged(ref _received, value);
            } 
        }

        private string _xreceiver = "";
        public string XReceiver 
        {
            get => _xreceiver;
            set {
                _xreceiver =  this.RaiseAndSetIfChanged(ref _xreceiver, value);
            } 
        }

        private string _mimeVersion = "";
        public string MimeVersion 
        {
            get => _mimeVersion;
            set {
                _mimeVersion =  this.RaiseAndSetIfChanged(ref _mimeVersion, value);
            } 
        }

        private string _contentType = "";
        public string ContentType 
        {
            get => _contentType;
            set {
                _contentType =  this.RaiseAndSetIfChanged(ref _contentType, value);
            } 
        }

        private string _messageId = "";
        public string MessageID 
        {
            get => _messageId;
            set {
                _messageId =  this.RaiseAndSetIfChanged(ref _messageId, value);
            } 
        }

        private string _body = "";
        public string Body 
        {
            get => _body;
            set {
                _body =  this.RaiseAndSetIfChanged(ref _body, value);
            } 
        }

        private string _html = "";
        public string Html 
        {
            get => _html;
            set {
                _html =  this.RaiseAndSetIfChanged(ref _html, value);
            } 
        }


        // public  List<string> text = new List<string>();
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

        // public void parseFile(string filePath)
        // {
        //      try
        //     {
             
        //         string[] lines = System.IO.File.ReadAllLines(filePath);

        //         foreach (string line in lines)
        //         {
        //             if (line == "") break;

        //             if (line[0] == ' ') {
        //                  text.Insert(text.Count - 1, $" {line}");
        //             } else {
        //                 text.Add(line);
        //             }
        //         }
        //     }
        //     catch (IOException e)
        //     {
        //         Console.WriteLine("The file could not be read:");
        //         Console.WriteLine(e.Message);
        //     }
        // }

        public void parseFile(string filePath)
        {
            FileStream fs = File.Open(filePath, FileMode.Open, 
            FileAccess.ReadWrite);

            EMLReader reader = new EMLReader(fs);
            fs.Close();

            From = reader.From;
            To = reader.To;
            Cc = reader.CC;
            Subject = reader.Subject;
            Date = reader.Date.ToString();
            Received = reader.Received;
            MimeVersion = reader.Mime_Version;
            ContentType = reader.Content_Type;
            MessageID = reader.Message_ID;
            Body = reader.Body;
            Html = reader.HTMLBody;

        }
        

    }
}
