﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TestComponents.Views.TitleBar
{
    public partial class WindowsTitleBar : UserControl
    {
        private Button minimizeButton;
        private Button maximizeButton;
        private Path maximizeIcon;
        private ToolTip maximizeToolTip;
        private Button closeButton;
        private Image windowIcon;

        private DockPanel titleBar;
        private DockPanel titleBarBackground;
        private TextBlock systemChromeTitle;
        private NativeMenuBar seamlessMenuBar;
        private NativeMenuBar defaultMenuBar;

        public static readonly StyledProperty<bool> IsSeamlessProperty =
        AvaloniaProperty.Register<WindowsTitleBar, bool>(nameof(IsSeamless));

        public bool IsSeamless
        {
            get { return GetValue(IsSeamlessProperty); }
            set
            {
                SetValue(IsSeamlessProperty, value);
                if (titleBarBackground != null && 
                    systemChromeTitle != null &&
                    seamlessMenuBar != null &&
                    defaultMenuBar != null)
                {
                    titleBarBackground.IsVisible = IsSeamless ? false : true;
                    systemChromeTitle.IsVisible = IsSeamless ? false : true;
                    seamlessMenuBar.IsVisible = IsSeamless ? true : false;
                    defaultMenuBar.IsVisible = IsSeamless ? false : true;

                    if (IsSeamless == false)
                    {
                        titleBar.Resources["SystemControlForegroundBaseHighBrush"] = new SolidColorBrush { Color = new Color(255, 0, 0, 0) };
                    }
                }
            }
        }

        public WindowsTitleBar()
        {
            this.InitializeComponent();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            {
                this.IsVisible = false;
            }
            else
            {
                minimizeButton = this.FindControl<Button>("MinimizeButton");
                maximizeButton = this.FindControl<Button>("MaximizeButton");
                maximizeIcon = this.FindControl<Path>("MaximizeIcon");
                maximizeToolTip = this.FindControl<ToolTip>("MaximizeToolTip");
                closeButton = this.FindControl<Button>("CloseButton");
                windowIcon = this.FindControl<Image>("WindowIcon");

                minimizeButton.Click += MinimizeWindow;
                maximizeButton.Click += MaximizeWindow;
                closeButton.Click += CloseWindow;
                //windowIcon.DoubleTapped += CloseWindow;

                titleBar = this.FindControl<DockPanel>("TitleBar");
                titleBarBackground = this.FindControl<DockPanel>("TitleBarBackground");
                systemChromeTitle = this.FindControl<TextBlock>("SystemChromeTitle");
                seamlessMenuBar = this.FindControl<NativeMenuBar>("SeamlessMenuBar");
                defaultMenuBar = this.FindControl<NativeMenuBar>("DefaultMenuBar");

                SubscribeToWindowState();
            }
        }

        private void CloseWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window hostWindow = (Window)this.VisualRoot;
            hostWindow.Close();
        }

        private void MaximizeWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window hostWindow = (Window)this.VisualRoot;

            if (hostWindow.WindowState == WindowState.Normal)
            {
                hostWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                hostWindow.WindowState = WindowState.Normal;
            }
        }

        private void MinimizeWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window hostWindow = (Window)this.VisualRoot;
            hostWindow.WindowState = WindowState.Minimized;
        }

        private async void SubscribeToWindowState()
        {
            Window hostWindow = (Window)this.VisualRoot;

            while (hostWindow == null)
            {
                hostWindow = (Window)this.VisualRoot;
                await Task.Delay(50);
            }

            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
