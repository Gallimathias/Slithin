﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Slithin.UI.Modals
{
    public partial class MessageBoxModal : UserControl
    {
        public MessageBoxModal()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
