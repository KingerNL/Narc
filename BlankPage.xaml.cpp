// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

#include "pch.h"
#include "BlankPage.xaml.h"
#if __has_include("BlankPage.g.cpp")
#include "BlankPage.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::App1::implementation
{
    BlankPage::BlankPage()
    {
        InitializeComponent();
    }

    int32_t BlankPage::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void BlankPage::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    void BlankPage::myButton_Click(IInspectable const&, RoutedEventArgs const&)
    {
        myButton().Content(box_value(L"Clicked"));
    }
}
