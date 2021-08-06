﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Slithin.Core;
using Slithin.UI.ContextualMenus;
using Slithin.ViewModels;

namespace Slithin.UI.Pages
{
    public partial class TemplatesPage : UserControl, IPage
    {
        public TemplatesPage()
        {
            InitializeComponent();

            DataContext = new TemplatesPageViewModel();
        }

        public string Title => "Templates";

        public Control GetContextualMenu()
        {
            return new TemplatesContextualMenu();
        }

        bool IPage.IsEnabled() => true;

        public bool UseContextualMenu()
        {
            return true;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
