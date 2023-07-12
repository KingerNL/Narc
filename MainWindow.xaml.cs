using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing; // Needed for AppWindow .
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Project_Narc {

    public sealed partial class MainWindow : Window {
        private AppWindow m_AppWindow;

        public MainWindow()
        {
            this.InitializeComponent();

            m_AppWindow = GetAppWindowForCurrentWindow();
            m_AppWindow.Title = "";

            var titlebar = m_AppWindow.TitleBar;
            titlebar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            titlebar.ButtonBackgroundColor = Colors.Transparent;
            titlebar.ButtonHoverBackgroundColor = Colors.Transparent;
            titlebar.ButtonPressedBackgroundColor = Colors.Transparent;
            titlebar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = m_AppWindow.TitleBar;
                // Hide default title bar.
                titleBar.ExtendsContentIntoTitleBar = true;
            }
            else
            {
                // In the case that title bar customization is not supported, hide the custom title bar
                // element.
                AppTitleBar.Visibility = Visibility.Collapsed;
            }

        }

        //private void myButton_Click(object sender, RoutedEventArgs e)
        //{
        //    myButton.Content = "Clicked";
        //}
        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }
    }
}
