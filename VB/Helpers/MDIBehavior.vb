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
                AssociatedObject.Loaded += Function(s, e)
                    Init()
                End Function
            End If
        End Sub
    End Class
End Namespace
