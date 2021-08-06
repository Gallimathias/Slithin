﻿using System;
using System.Windows.Input;
using Slithin.Controls;
using Slithin.Core.Remarkable;
using Slithin.Core.Sync;
using Slithin.Core.Sync.Repositorys;
using Slithin.ViewModels;

namespace Slithin.Core.Commands
{
    public class RemoveTemplateCommand : ICommand
    {
        private readonly LocalRepository _localRepository;
        private readonly TemplatesPageViewModel _templatesPageViewModel;

        public RemoveTemplateCommand(TemplatesPageViewModel templatesPageViewModel, LocalRepository localRepository)
        {
            _templatesPageViewModel = templatesPageViewModel;
            _localRepository = localRepository;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter != null && parameter is Template;
        }

        public async void Execute(object parameter)
        {
            if (parameter is Template tmpl)
            {
                if (await DialogService.ShowDialog($"Would you really want to delete '{tmpl.Filename}'?"))
                {
                    _templatesPageViewModel.SelectedTemplate = null;
                    ServiceLocator.SyncService.TemplateFilter.Templates.Remove(tmpl);

                    TemplateStorage.Instance.Remove(tmpl);
                    _localRepository.Remove(tmpl);

                    var item = new SyncItem
                    {
                        Action = SyncAction.Remove,
                        Direction = SyncDirection.ToDevice,
                        Data = tmpl,
                        Type = SyncType.Template
                    };

                    ServiceLocator.SyncService.SyncQueue.Insert(item);
                }
            }
        }
    }
}
