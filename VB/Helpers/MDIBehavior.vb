Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Diagram
Imports System.Linq

Namespace MDI_Diagram

    Public Class MDIBehavior
        Inherits Behavior(Of DiagramControl)

        Private Sub Init()
            Dim panel = LayoutTreeHelper.GetVisualParents(AssociatedObject).OfType(Of LayoutPanel)().FirstOrDefault()
            If panel IsNot Nothing Then DiagramControl.SetDiagram(panel, AssociatedObject)
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            If AssociatedObject.IsLoaded Then
                Init()
            Else
                Me.AssociatedObject.Loaded += AddressOf AssociatedObject_Loaded
            End If
        End Sub

        Private Sub AssociatedObject_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Init()
        End Sub
    End Class
End Namespace
