using Outliner.Model;
using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class MainWindowViewModel :  ObservableObject
    {
        private OutlineDocumentViewModel _currentDocument;
        private ObservableCollection<OutlineDocumentViewModel> _openedDocuments = new ObservableCollection<OutlineDocumentViewModel>();
        private IFileDialogService _fileDialogService;
        private const string FileFilter = "Outline files (*.otl)|*.otl|All files (*.*)|*.*";

        public MainWindowViewModel(IFileDialogService service)
        {
            _fileDialogService = service;
            NewFileCommand = new RelayCommand(NewFile);
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveAsFileCommand = new RelayCommand(SaveAsFile);
            CloseCurrentDocumentCommand = new RelayCommand(CloseCurrentDocument);
            _openedDocuments.CollectionChanged += OnDocumentsChanged;
        }        

        public RelayCommand NewFileCommand { get; private set; }
        public RelayCommand OpenFileCommand { get; private set; }
        public RelayCommand SaveFileCommand { get; private set; }
        public RelayCommand SaveAsFileCommand { get; private set; }
        public RelayCommand CloseCurrentDocumentCommand { get; private set; }

        public OutlineDocumentViewModel CurrentDocument         
        {
            get { return _currentDocument; }
            set { Set(ref _currentDocument, value); }
        }

        public ObservableCollection<OutlineDocumentViewModel> OpenedDocuments
        {
            get { return _openedDocuments; }           
        }

        public void NewFile()
        {
            var doc = new OutlineDocumentViewModel() { Text = "NewOutline" };
            doc.Add();
            OpenDocument(doc);
        }

        public void OpenFile()
        {
            var path = _fileDialogService.GetOpenFilePath(FileFilter);
            if (String.IsNullOrWhiteSpace(path))
                return;
            var document = OutlinerReader.Read(path);
            OpenDocument(document);
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

        public void OpenDocument(OutlineDocumentViewModel document)
        {
            OpenedDocuments.Add(document);
            CurrentDocument = document;
        }

        public void CloseDocument(OutlineDocumentViewModel document)
        {
            OpenedDocuments.Remove(document);
            //TODO: if(CurrentDocument == document)
        }

        public void CloseCurrentDocument()
        {
            OpenedDocuments.Remove(CurrentDocument);            
        }

        private void OnDocumentsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (OutlineDocumentViewModel doc in e.NewItems)
                    doc.RequestClose += OnDocumentRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (OutlineDocumentViewModel doc in e.OldItems)
                    doc.RequestClose -= OnDocumentRequestClose;
        }

        private void OnDocumentRequestClose(object sender, EventArgs e)
        {
            OutlineDocumentViewModel doc = sender as OutlineDocumentViewModel;
            CloseDocument(doc);
        }
    }
}
