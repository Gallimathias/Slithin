﻿using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Renci.SshNet;
using Slithin.Core;
using Slithin.Core.Services;
using Slithin.Core.Sync.Repositorys;
using Slithin.Messages;
using Slithin.ViewModels;

namespace Slithin.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            ServiceLocator.Container.Resolve<LiteDB.LiteDatabase>().Dispose();
            ServiceLocator.Container.Resolve<SshClient>().Dispose();
            ServiceLocator.Container.Resolve<ScpClient>().Dispose();

            Environment.Exit(0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            DataContext = ServiceLocator.Container.Resolve<MainWindowViewModel>();
            var notebooksDir = ServiceLocator.Container.Resolve<IPathManager>().NotebooksDir;
            var mailboxService = ServiceLocator.Container.Resolve<IMailboxService>();

            if (!ServiceLocator.Container.Resolve<LocalRepository>().GetTemplates().Any())
            {
                mailboxService.Post(new InitStorageMessage());
            }

            if (!Directory.GetFiles(notebooksDir).Any())
            {
                mailboxService.Post(new DownloadNotebooksMessage());
            }
        }
    }
}
