﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Slithin.Controls;
using Slithin.Core;
using Slithin.Models;
using Slithin.UI.Pages.Store;

namespace Slithin.ViewModels.Pages
{
    public class SharablesPageViewModel : BaseViewModel
    {
        public SharablesPageViewModel()
        {
            ViewMoreCommand = new DelegateCommand(_ =>
            {
                NavigationHost.Navigate(new CategoryPage(), _);
            });
        }

        public ObservableCollection<Sharable> Items { get; set; } = new();

        public ICommand ViewMoreCommand { get; set; }

        public override void OnLoad()
        {
            base.OnLoad();

            Items.Add(new() { ID = "1", IsInstalled = false, Name = "Not Installed Template 1", Image = LoadImage("backup"), Author = "Furesoft" });
            Items.Add(new() { ID = "2", IsInstalled = true, Name = "Installed Template 2", Image = LoadImage("epub"), Author = "Furesoft" });
            Items.Add(new() { ID = "3", IsInstalled = false, Name = "Not Installed Template 3", Image = LoadImage("folder"), Author = "Furesoft" });
            Items.Add(new() { ID = "4", IsInstalled = true, Name = "Installed Template 4", Image = LoadImage("pdf"), Author = "Furesoft" });
            Items.Add(new() { ID = "5", IsInstalled = true, Name = "Installed Template 5", Image = LoadImage("folder"), Author = "Furesoft" });
            Items.Add(new() { ID = "6", IsInstalled = false, Name = "Not Installed Template 5", Image = LoadImage("backup"), Author = "Furesoft" });

            NavigationHost.Navigate(new MainStorePage());
        }

        private IImage LoadImage(string name)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            return new Bitmap(assets.Open(new Uri($"avares://Slithin/Resources/{name}.png")));
        }
    }
}
