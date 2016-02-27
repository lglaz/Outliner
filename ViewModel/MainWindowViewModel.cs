using Outliner.Model;
using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class MainWindowViewModel :  ObservableObject
    {
        private OutlineDocumentViewModel _currentDocument;
        private IFileDialogService _fileDialogService;
        private const string FileFilter = "Outline files (*.otl)|*.otl|All files (*.*)|*.*";

        public MainWindowViewModel(IFileDialogService service)
        {
            _fileDialogService = service;
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveAsFileCommand = new RelayCommand(SaveAsFile);
        }

        public RelayCommand OpenFileCommand { get; private set; }
        public RelayCommand SaveFileCommand { get; private set; }
        public RelayCommand SaveAsFileCommand { get; private set; }

        public OutlineDocumentViewModel CurrentDocument         
        {
            get { return _currentDocument; }
            set { Set(ref _currentDocument, value); }
        }

        public void OpenFile()
        {
            var path = _fileDialogService.GetOpenFilePath(FileFilter);
            if (String.IsNullOrWhiteSpace(path))
                return;
            CurrentDocument = OutlinerReader.Read(path);
        }

        public void SaveFile()
        {
            SaveAsFile();
        }

        public void SaveAsFile()
        {
            var path = _fileDialogService.GetSaveFilePath("otl", FileFilter);
            if (String.IsNullOrWhiteSpace(path))
                return;

            OutlinerWriter.Write(CurrentDocument, path);
        }
    }
}
