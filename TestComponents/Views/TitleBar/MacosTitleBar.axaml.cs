using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TestComponents.Views.TitleBar
{
    public partial class MacosTitleBar : UserControl
    {
        private Button closeButton;
        private Button minimizeButton;
        private Button zoomButton;

        private DockPanel titleBarBackground;
        private StackPanel titleAndWindowIconWrapper;

        public static readonly StyledProperty<bool> IsSeamlessProperty =
        AvaloniaProperty.Register<MacosTitleBar, bool>(nameof(IsSeamless));

        public bool IsSeamless
        {
            get { return GetValue(IsSeamlessProperty); }
            set {
                SetValue(IsSeamlessProperty, value);
                if (titleBarBackground != null && titleAndWindowIconWrapper != null)
                {
                    titleBarBackground.IsVisible = IsSeamless ? false : true;
                    titleAndWindowIconWrapper.IsVisible = IsSeamless ? false : true;
                }
            }
        }

        public MacosTitleBar()
        {
            this.InitializeComponent();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == false)
            {
                this.IsVisible = false;
            }
            else
            {
                minimizeButton = this.FindControl<Button>("MinimizeButton");
                zoomButton = this.FindControl<Button>("ZoomButton");
                closeButton = this.FindControl<Button>("CloseButton");

                minimizeButton.Click += MinimizeWindow;
                zoomButton.Click += MaximizeWindow;
                closeButton.Click += CloseWindow;

                titleBarBackground = this.FindControl<DockPanel>("TitleBarBackground");
                titleAndWindowIconWrapper = this.FindControl<StackPanel>("TitleAndWindowIconWrapper");

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

            hostWindow.ExtendClientAreaTitleBarHeightHint = 44;
           
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
