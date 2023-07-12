// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

#pragma once

#include "BlankPage.g.h"

namespace winrt::App1::implementation
{
    struct BlankPage : BlankPageT<BlankPage>
    {
        BlankPage();

        int32_t MyProperty();
        void MyProperty(int32_t value);

        void myButton_Click(Windows::Foundation::IInspectable const& sender, Microsoft::UI::Xaml::RoutedEventArgs const& args);
    };
}

namespace winrt::App1::factory_implementation
{
    struct BlankPage : BlankPageT<BlankPage, implementation::BlankPage>
    {
    };
}
