using DevExpress.Diagram.Core;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace MDI_Diagram {
    public class ViewModel : ViewModelBase {
        public ViewModel() {
            SelectedDiagramStencils = new StencilCollection() { "BasicShapes" };
            DiagramTheme = DiagramThemes.Office;
        }

        IDocumentManagerService DocumentManagerService => GetService<IDocumentManagerService>();
        IOpenFileDialogService OpenFileDialogService => GetService<IOpenFileDialogService>();
        ISaveFileDialogService SaveFileDialogService => GetService<ISaveFileDialogService>();
        IMessageBoxService MessageBoxService => GetService<IMessageBoxService>();

        [Command]
        public void CreateDocument(DocumentViewModel vm = null) {
            var docCount = DocumentManagerService.Documents.Count();
            IDocument doc = DocumentManagerService.FindDocumentById(docCount);
            if (doc == null) {
                if (vm == null)
                    vm = new DocumentViewModel();
                vm.Name = $"Document {docCount}";
                doc = DocumentManagerService.CreateDocument(vm);
                doc.Id = docCount;
            }
            doc.Show();
        }

        [Command]
        public void OnLoaded() {
            CreateDocument();
        }

        [Command]
        public void OpenMany() {
            try {
                if (OpenFileDialogService.ShowDialog()) {
                    var serializer = new XmlSerializer(typeof(DiagramLayoutWrapper));

                    var wrapper = serializer.Deserialize(OpenFileDialogService.File.OpenText()) as DiagramLayoutWrapper;

                    if (wrapper != null)
                        foreach (var diagram in wrapper.Diagrams)
                            CreateDocument(new DocumentViewModel() { Diagram = diagram });
                }
            } catch (Exception ex) {
                MessageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
            }
        }

        [Command]
        public void SaveAll() {
            if (SaveFileDialogService.ShowDialog()) {
                var wrapper = new DiagramLayoutWrapper();

                foreach (var doc in DocumentManagerService.Documents)
                    if (doc.Content is DocumentViewModel) {
                        var vm = (DocumentViewModel)doc.Content;
                        wrapper.Diagrams.Add(vm.Save());
                    }

                var serializer = new XmlSerializer(typeof(DiagramLayoutWrapper));

                serializer.Serialize(SaveFileDialogService.OpenFile(), wrapper);
            }
        }

        public DiagramTool ActiveTool { get => GetValue<DiagramTool>(); set => SetValue(value); }
        public DiagramTheme DiagramTheme { get => GetValue<DiagramTheme>(); set => SetValue(value); }
        public ObservableCollection<DocumentViewModel> Documents { get; set; }
        public StencilCollection SelectedDiagramStencils {
            get => GetValue<StencilCollection>();
            set {
                if (value.Count > 0) {
                    SetValue(value);
                }
            }
        }
    }

    public class DocumentViewModel : ViewModelBase, IDocumentContent {
        IDiagramService DiagramService => GetService<IDiagramService>();

        public void OnClose(CancelEventArgs e) { }

        public void OnDestroy() { }

        [Command]
        public void OnLoaded() {
            if (!string.IsNullOrEmpty(Diagram))
                DiagramService?.Restore(Diagram);
        }
        public string Save() {
            return DiagramService?.Save();
        }

        public string Diagram { get; set; }
        public IDocumentOwner DocumentOwner { get; set; }
        public string Name { get; set; }
        public object Title => Name;
    }
}
