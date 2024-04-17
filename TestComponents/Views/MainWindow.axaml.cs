using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace TestComponents.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
    }

    private void Path_PointerEntered(object sender, PointerEventArgs a)
    {
        Console.WriteLine( "enter");
    }
}
