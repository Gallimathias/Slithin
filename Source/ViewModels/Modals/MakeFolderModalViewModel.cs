﻿using System;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using Slithin.Controls;
using Slithin.Core;
using Slithin.Core.Remarkable;
using Slithin.Core.Services;
using Slithin.Core.Sync;

namespace Slithin.ViewModels.Modals
{
    public class MakeFolderModalViewModel : BaseViewModel
    {
        private readonly IPathManager _pathManager;
        private readonly SynchronisationService _synchronisationService;
        private string _name;

        public MakeFolderModalViewModel(IPathManager _pathManager, SynchronisationService synchronisationService)
        {
            MakeFolderCommand = new DelegateCommand(MakeFolder);
            this._pathManager = _pathManager;
            _synchronisationService = synchronisationService;
        }

        public ICommand MakeFolderCommand { get; set; }

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private void MakeFolder(object obj)
        {
            var id = Guid.NewGuid();

            var md = new Metadata
            {
                ID = id.ToString(),
                Parent = _synchronisationService.NotebooksFilter.Folder,
                Type = "CollectionType",
                VisibleName = Name
            };

            MetadataStorage.Local.Add(md, out var alreadyAdded);

            if (!alreadyAdded)
            {
                var path = Path.Combine(_pathManager.NotebooksDir, md.ID + ".metadata");
                var mdContent = JsonConvert.SerializeObject(md);
                File.WriteAllText(path, mdContent);

                _synchronisationService.NotebooksFilter.Documents.Add(md);
                _synchronisationService.NotebooksFilter.SortByFolder();

                var syncItem = new SyncItem
                {
                    Action = SyncAction.Add,
                    Data = md,
                    Direction = SyncDirection.ToDevice,
                    Type = SyncType.Notebook
                };

                _synchronisationService.SyncQueue.Insert(syncItem);

                DialogService.Close();
            }
            else
            {
                DialogService.OpenError($"'{md.VisibleName}' already exists");
            }
        }
    }
}
