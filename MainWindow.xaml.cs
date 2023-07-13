using Microsoft.Web.WebView2.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Numerics;

namespace Project_Narc
{
    public sealed partial class MainWindow : Window
    {
        private readonly AppWindow m_AppWindow;

        public MainWindow()
        {

            this.InitializeComponent();
            MyWebView.NavigationStarting += EnsureHttps;

            m_AppWindow = GetAppWindowForCurrentWindow();
            m_AppWindow.Title = "";

            PopupRectangle.Translation += new Vector3(0, 0, 32);

            var titlebar = m_AppWindow.TitleBar;
            titlebar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            titlebar.ButtonBackgroundColor = Colors.Transparent;
            titlebar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = m_AppWindow.TitleBar;
                // Hide default title bar.
                titleBar.ExtendsContentIntoTitleBar = true;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private async void EnsureHttps(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
        {
            string uri = args.Uri;

            // Remove "https://" and "www." prefixes from the displayed URL
            string displayedUrl = uri.Replace("https://", string.Empty);
            displayedUrl = displayedUrl.Replace("www.", string.Empty);

            // Check if there is a trailing slash and hide it if present
            if (displayedUrl.EndsWith("/"))
            {
                displayedUrl = displayedUrl.Substring(0, displayedUrl.Length - 1);
            }

            addressBar.Text = displayedUrl;
        }

        private void AddressBarKeyDown(object sender, KeyRoutedEventArgs e){
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                NavigateToAddress(sender, e);
                e.Handled = true; // Mark the event as handled to prevent further processing
            }
        }

        private void NavigateToAddress(object sender, RoutedEventArgs e)
        {
            string uri = addressBar.Text;

            if (!uri.StartsWith("https://") && !uri.StartsWith("http://"))
            {
                // Check if uri starts with "www."
                if (uri.StartsWith("www."))
                {
                    bool isValidHTTPWebsite = Uri.TryCreate("http://" + uri, UriKind.Absolute, out Uri validatedHTTPUri);
                    if (isValidHTTPWebsite)
                    {
                        MyWebView.Source = validatedHTTPUri;
                    } else
                    {
                        bool isValidHTPPSWebsite = Uri.TryCreate("https://" + uri, UriKind.Absolute, out Uri validatedHTTPSUri);
                        if (isValidHTPPSWebsite)
                        {
                            MyWebView.Source = validatedHTTPSUri;
                        }
                    }
                }
                else if (uri.EndsWith(".com"))
                {
                    bool isValidHTTPSWWWWebsite = Uri.TryCreate("https://www." + uri, UriKind.Absolute, out Uri validatedHTTPSWWWUri);
                    if (isValidHTTPSWWWWebsite)
                    {
                        MyWebView.Source = validatedHTTPSWWWUri;
                    }
                } else
                {
                    string searchQuery = Uri.EscapeDataString(addressBar.Text);
                    string googleSearchUrl = $"https://www.google.com/search?q={searchQuery}";
                    MyWebView.Source = new Uri(googleSearchUrl);
                }
            }
            else
            {
                MyWebView.Source = new Uri(uri);
            }
        }
    }
}
