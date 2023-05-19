using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDI_Diagram {
    [XmlRoot("Root")]
    public class DiagramLayoutWrapper {
        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<string> Diagrams { get; set; } = new List<string>();
    }
}
