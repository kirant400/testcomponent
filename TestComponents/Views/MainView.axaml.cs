using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TestComponents.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        //do something on click
        var window = new Information();
        window.Show();
    }
    
}
