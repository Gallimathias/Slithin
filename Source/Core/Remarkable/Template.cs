﻿using System.ComponentModel;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using LiteDB;
using Newtonsoft.Json;
using Slithin.Core.Services;

namespace Slithin.Core.Remarkable
{
    public class Template : INotifyPropertyChanged
    {
        private IImage? _image;

        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonProperty("categories")]
        public string[]? Categories { get; set; }

        [JsonProperty("filename")]
        public string? Filename { get; set; }

        [JsonProperty("iconCode")]
        public string? IconCode { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public IImage Image
        {
            get { return _image; }
            set { _image = value; PropertyChanged?.Invoke(this, new(nameof(Image))); }
        }

        [JsonProperty("landscape")]
        public bool Landscape { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        public void Load()
        {
            var templatesDir = ServiceLocator.Container.Resolve<IPathManager>().TemplatesDir;

            if (Directory.Exists(templatesDir))
            {
                var path = Path.Combine(templatesDir, Filename);

                if (Image is null)
                {
                    if (File.Exists(path + ".png"))
                    {
                        Image = Bitmap.DecodeToWidth(File.OpenRead(path + ".png"), 150, Avalonia.Visuals.Media.Imaging.BitmapInterpolationMode.HighQuality);
                    }
                }
            }
        }
    }
}
