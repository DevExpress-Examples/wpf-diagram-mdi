using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;
using DevExpress.Xpf.Docking;
using System.Linq;

namespace MDI_Diagram {
    public class MDIBehavior : Behavior<DiagramControl> {

        void Init() {
            var panel = LayoutTreeHelper.GetVisualParents(AssociatedObject).OfType<LayoutPanel>().FirstOrDefault();
            if (panel != null)
                DiagramControl.SetDiagram(panel, AssociatedObject);
        }

        protected override void OnAttached() {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
                Init();
            else
                AssociatedObject.Loaded += (s, e) => {
                    Init();
                };
        }
    }
}
