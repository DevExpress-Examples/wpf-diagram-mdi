Imports System.Collections.Generic
Imports System.Xml.Serialization

Namespace MDI_Diagram

    <XmlRoot("Root")>
    Public Class DiagramLayoutWrapper

        <XmlArray("Items")>
        <XmlArrayItem("Item")>
        Public Property Diagrams As List(Of String) = New List(Of String)()
    End Class
End Namespace
