Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Diagram
Imports System.IO
Imports System.Text

Namespace MDI_Diagram

    Public Interface IDiagramService

        Sub Restore(ByVal diagram As String)

        Function Save() As String

    End Interface

    Public Class DiagramService
        Inherits ServiceBase
        Implements IDiagramService

        Private ReadOnly Property diagram As DiagramControl
            Get
                Return TryCast(AssociatedObject, DiagramControl)
            End Get
        End Property

        Public Sub Restore(ByVal layout As String) Implements IDiagramService.Restore
            Dim stream = New MemoryStream(Encoding.UTF8.GetBytes(layout))
            diagram?.LoadDocument(stream)
        End Sub

        Public Function Save() As String Implements IDiagramService.Save
            Dim stream = New MemoryStream()
            diagram?.SaveDocument(stream)
            stream.Seek(0, SeekOrigin.Begin)
            Return Encoding.UTF8.GetString(stream.ToArray())
        End Function
    End Class
End Namespace
