using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Diagram;
using System.IO;
using System.Text;

namespace MDI_Diagram {
    public interface IDiagramService {
        void Restore(string diagram);
        string Save();
    }

    public class DiagramService : ServiceBase, IDiagramService {
        DiagramControl diagram => AssociatedObject as DiagramControl;

        public void Restore(string layout) {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(layout));
            diagram?.LoadDocument(stream);
        }

        public string Save() {
            var stream = new MemoryStream();
            diagram?.SaveDocument(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
