﻿using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Slithin.Controls;
using Slithin.Core;
using Slithin.UI.Tools;
using Slithin.ViewModels.Modals;
using Slithin.Models;

namespace Slithin.Tools
{
    public class NotebookCreationTool : ITool
    {
        public IImage Image
        {
            get
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

                return new Bitmap(assets.Open(new Uri($"avares://Slithin/Resources/pdf.png")));
            }
        }

        public ScriptInfo Info => new("Notebook Creator", "PDF", "Build PDF Notebooks");

        public bool IsConfigurable => false;

        public Control GetModal()
        {
            return null;
        }

        public void Invoke(object data)
        {
            var modal = new CreateNotebookModal();

            DialogService.Open(modal, ServiceLocator.Container.Resolve<CreateNotebookModalViewModel>());
        }
    }
}
