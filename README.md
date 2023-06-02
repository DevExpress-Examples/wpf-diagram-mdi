<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/642837868/22.2.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1167110)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# DiagramControl for WPF - How to display multiple diagrams as MDI documents

This example shows how to create multiple diagram documents using DockLayoutManager.

![](img/wpf-diagram-mdi.png)

The main idea there is to use the [Document Management System](https://docs.devexpress.com/WPF/18234/mvvm-framework/services/predefined-set/document-services/document-management-system) to create multiple document panels displaying diagrams and connect them to utility elements (toolbox, property panel, and ribbon) using data binding.

## Files to Review

[MainWindow.xaml](CS/MainWindow.xaml) (VB: [MainWindow.xaml](VB/MainWindow.xaml))

[ViewModel.cs](CS/ViewModel.cs) (VB: [ViewModel.vb](VB/ViewModel.vb))

## More Examples

[WPF Diagram - Use the DiagramDataBindingBehavior to Generate a Diagram from a Collection](https://github.com/DevExpress-Examples/wpf-diagram-use-diagramdatabindingbehavior-to-generate-diagram-from-collection#wpf-diagram---use-the-diagramdatabindingbehavior-to-generate-a-diagram-from-a-collection)
