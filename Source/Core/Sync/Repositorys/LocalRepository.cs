﻿using System;
using System.IO;
using Newtonsoft.Json;
using Slithin.Core.Remarkable;

namespace Slithin.Core.Sync.Repositorys
{
    public class LocalRepository : IRepository
    {
        public LocalRepository()
        {
        }

        public void Add(Template template)
        {
            var path = Path.Combine(ServiceLocator.ConfigBaseDir, "templates.json");

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "{\"templates\": []}");
            }

            var templateJson = JsonConvert.DeserializeObject<TemplateStorage>(File.ReadAllText(path));
            var templates = new Template[templateJson.Templates.Length + 1];
            Array.Copy(templateJson.Templates, templates, templateJson.Templates.Length);

            templates[^1] = template;

            templateJson.Templates = templates;

            File.WriteAllText(path, JsonConvert.SerializeObject(templateJson, Formatting.Indented));
        }

        public void AddScreen(string filename, string localfilename)
        {
            File.Delete(Path.Combine(ServiceLocator.CustomScreensDir, localfilename));
            File.Copy(filename, Path.Combine(ServiceLocator.CustomScreensDir, localfilename));
        }

        public Template[] GetTemplates()
        {
            var path = Path.Combine(ServiceLocator.ConfigBaseDir, "templates.json");
            return JsonConvert.DeserializeObject<TemplateStorage>(File.ReadAllText(path)).Templates;
        }

        public Version GetVersion()
        {
            if (!File.Exists(Path.Combine(ServiceLocator.ConfigBaseDir, ".version")))
            {
                File.WriteAllText(Path.Combine(ServiceLocator.ConfigBaseDir, ".version"), ServiceLocator.Device.GetVersion().ToString());
            }

            return new Version(File.ReadAllText(Path.Combine(ServiceLocator.ConfigBaseDir, ".version")));
        }

        public void Remove(Metadata md)
        {
            if (md.Type == "DocumentType")
            {
                var files = Directory.GetFiles(ServiceLocator.NotebooksDir, md.ID + ".*");
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                var di = new DirectoryInfo(Path.Combine(ServiceLocator.NotebooksDir, md.ID));
                if (di.Exists)
                {
                    di.Delete(true);
                }
            }
            else
            {
                File.Delete(Path.Combine(ServiceLocator.NotebooksDir, md.ID + ".metadata"));
            }
        }

        public void Remove(Template template)
        {
            File.Delete(Path.Combine(ServiceLocator.TemplatesDir, template.Filename + ".png"));
        }

        public void UpdateVersion(Version version)
        {
            File.WriteAllText(Path.Combine(ServiceLocator.ConfigBaseDir, ".version"), version.ToString());
        }
    }
}
