﻿using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Slithin.Core.Remarkable
{
    public class TemplateStorage
    {
        public static TemplateStorage? Instance = new();

        [JsonProperty("templates")]
        public Template[]? Templates { get; set; }

        public void Add(Template template)
        {
            var tmp = new Template[Templates.Length + 1];
            Array.Copy(Templates, tmp, Templates.Length);

            tmp[^1] = template;

            Templates = tmp;
        }

        public void Apply()
        {
            var result = ServiceLocator.Client.RunCommand("systemctl restart xochitl");

            if (result.ExitStatus != 0)
            {
                System.Console.WriteLine(result.Error);
            }
        }

        public void Load()
        {
            Instance.Templates = ServiceLocator.Local.GetTemplates();

            foreach (var item in Instance.Templates)
            {
                item.OnDevice = true;
                item.Load();
            }
        }

        public void Remove(Template tmpl)
        {
            var tmp = Templates.ToList();
            tmp.Remove(tmpl);

            Templates = tmp.ToArray();

            Save();
        }

        public void Save()
        {
            var path = Path.Combine(ServiceLocator.ConfigBaseDir, "templates.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
