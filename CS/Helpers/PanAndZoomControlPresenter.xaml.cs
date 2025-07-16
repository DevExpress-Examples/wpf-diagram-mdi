using DevExpress.Xpf.Diagram;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MDI_Diagram.Helpers {
    /// <summary>
    /// Interaction logic for PanAndZoomControlPresenter.xaml
    /// </summary>
    public partial class PanAndZoomControlPresenter : UserControl {

        public static readonly DependencyProperty DiagramProperty =
            DependencyProperty.Register("Diagram", typeof(DiagramControl), typeof(PanAndZoomControlPresenter), new PropertyMetadata(null));

        public PanAndZoomControlPresenter() {
            InitializeComponent();
        }

        private Grid RootGrid { get { return FindName("PART_RootCustomPresenter") as Grid; } }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
            base.OnPropertyChanged(e);
            if (e.Property == DiagramProperty && RootGrid != null && ControlTemplate != null) {
                RootGrid.Children.Clear();
                var panAndZoom = ControlTemplate.LoadContent() as DiagramPanAndZoomControl;
                if (panAndZoom != null) {
                    panAndZoom.Diagram = e.NewValue as DiagramControl;
                    this.RootGrid.Children.Add(panAndZoom);
                }
            }
        }

        public DataTemplate ControlTemplate { get; set; }
        public DiagramControl Diagram {
            get { return (DiagramControl)GetValue(DiagramProperty); }
            set { SetValue(DiagramProperty, value); }
        }
    }
}
