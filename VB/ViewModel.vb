Imports DevExpress.Diagram.Core
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Xml.Serialization

Namespace MDI_Diagram

    Public Class ViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Public Sub New()
            Me.SelectedDiagramStencils = New DevExpress.Diagram.Core.StencilCollection() From {"BasicShapes"}
            Me.DiagramTheme = DevExpress.Diagram.Core.DiagramThemes.Office
        End Sub

        Private ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetService(Of DevExpress.Mvvm.IDocumentManagerService)()
            End Get
        End Property

        Private ReadOnly Property OpenFileDialogService As IOpenFileDialogService
            Get
                Return GetService(Of DevExpress.Mvvm.IOpenFileDialogService)()
            End Get
        End Property

        Private ReadOnly Property SaveFileDialogService As ISaveFileDialogService
            Get
                Return GetService(Of DevExpress.Mvvm.ISaveFileDialogService)()
            End Get
        End Property

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetService(Of DevExpress.Mvvm.IMessageBoxService)()
            End Get
        End Property

        <Command>
        Public Sub CreateDocument(ByVal Optional vm As MDI_Diagram.DocumentViewModel = Nothing)
            Dim docCount = Me.DocumentManagerService.Documents.Count()
            Dim doc As DevExpress.Mvvm.IDocument = Me.DocumentManagerService.FindDocumentById(docCount)
            If doc Is Nothing Then
                If vm Is Nothing Then vm = New MDI_Diagram.DocumentViewModel()
                vm.Name = $"Document {docCount}"
                doc = Me.DocumentManagerService.CreateDocument(vm)
                doc.Id = docCount
            End If

            doc.Show()
        End Sub

        <Command>
        Public Sub OnLoaded()
            Me.CreateDocument()
        End Sub

        <Command>
        Public Sub OpenMany()
            Try
                If Me.OpenFileDialogService.ShowDialog() Then
                    Dim serializer = New System.Xml.Serialization.XmlSerializer(GetType(MDI_Diagram.DiagramLayoutWrapper))
                    Dim wrapper = TryCast(serializer.Deserialize(Me.OpenFileDialogService.File.OpenText()), MDI_Diagram.DiagramLayoutWrapper)
                    If wrapper IsNot Nothing Then
                        For Each diagram In wrapper.Diagrams
                            Me.CreateDocument(New MDI_Diagram.DocumentViewModel() With {.Diagram = diagram})
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.MessageBoxService.ShowMessage(ex.Message, "Error", DevExpress.Mvvm.MessageButton.OK, DevExpress.Mvvm.MessageIcon.[Error])
            End Try
        End Sub

        <Command>
        Public Sub SaveAll()
            Dim vm As MDI_Diagram.DocumentViewModel = Nothing
            If Me.SaveFileDialogService.ShowDialog() Then
                Dim wrapper = New MDI_Diagram.DiagramLayoutWrapper()
                For Each doc In Me.DocumentManagerService.Documents
                    If CSharpImpl.__Assign(vm, TryCast(doc.Content, MDI_Diagram.DocumentViewModel)) IsNot Nothing Then wrapper.Diagrams.Add(vm.Save())
                Next

                Dim serializer = New System.Xml.Serialization.XmlSerializer(GetType(MDI_Diagram.DiagramLayoutWrapper))
                serializer.Serialize(Me.SaveFileDialogService.OpenFile(), wrapper)
            End If
        End Sub

        Public Property ActiveTool As DiagramTool
            Get
                Return GetValue(Of DevExpress.Diagram.Core.DiagramTool)()
            End Get

            Set(ByVal value As DiagramTool)
                SetValue(value)
            End Set
        End Property

        Public Property DiagramTheme As DiagramTheme
            Get
                Return GetValue(Of DevExpress.Diagram.Core.DiagramTheme)()
            End Get

            Set(ByVal value As DiagramTheme)
                SetValue(value)
            End Set
        End Property

        Public Property Documents As ObservableCollection(Of MDI_Diagram.DocumentViewModel)

        Public Property SelectedDiagramStencils As StencilCollection
            Get
                Return GetValue(Of DevExpress.Diagram.Core.StencilCollection)()
            End Get

            Set(ByVal value As StencilCollection)
                If value.Count > 0 Then
                    SetValue(value)
                End If
            End Set
        End Property

        Private Class CSharpImpl

            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class DocumentViewModel
        Inherits DevExpress.Mvvm.ViewModelBase
        Implements DevExpress.Mvvm.IDocumentContent

        Private ReadOnly Property DiagramService As IDiagramService
            Get
                Return GetService(Of MDI_Diagram.IDiagramService)()
            End Get
        End Property

        Public Sub OnClose(ByVal e As System.ComponentModel.CancelEventArgs) Implements Global.DevExpress.Mvvm.IDocumentContent.OnClose
        End Sub

        Public Sub OnDestroy() Implements Global.DevExpress.Mvvm.IDocumentContent.OnDestroy
        End Sub

        <Command>
        Public Sub OnLoaded()
            If Not String.IsNullOrEmpty(Me.Diagram) Then Me.DiagramService?.Restore(Me.Diagram)
        End Sub

        Public Function Save() As String
            Return Me.DiagramService?.Save()
        End Function

        Public Property Diagram As String

        Public Property DocumentOwner As IDocumentOwner Implements Global.DevExpress.Mvvm.IDocumentContent.DocumentOwner

        Public Property Name As String

        Public ReadOnly Property Title As Object Implements Global.DevExpress.Mvvm.IDocumentContent.Title
            Get
                Return Me.Name
            End Get
        End Property
    End Class
End Namespace
