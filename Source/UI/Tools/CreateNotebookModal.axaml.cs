﻿using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Slithin.ViewModels;

namespace Slithin.UI.Tools
{
    public partial class CreateNotebookModal : UserControl
    {
        public CreateNotebookModal()
        {
            InitializeComponent();
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            // Only allow Copy or Link as Drop Operations.
            e.DragEffects &= (DragDropEffects.Copy | DragDropEffects.Link);

            // Only allow if the dragged data contains text or filenames.
            if (!e.Data.Contains(DataFormats.Text)
                && !e.Data.Contains(DataFormats.FileNames))
                e.DragEffects = DragDropEffects.None;
        }

        private void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
            {
                var filename = e.Data.GetFileNames().First();

                if (DataContext is CreateNotebookModalViewModel vm)
                {
                    vm.Filename = filename;
                    vm.LoadCover();
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }
    }
}
