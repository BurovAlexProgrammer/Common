using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _Project.Scripts.Main.Wrappers
{
    public class Localization
    {
        private Locales _locale;
        private LocaleInfo _info;
        private Dictionary<string, LocalizedItem> _localizedItems;
        private string _filePathInEditor;

        public Locales Locale => _locale;
        public LocaleInfo Info => _info;
        public Dictionary<string, LocalizedItem> LocalizedItems => _localizedItems;
        public string FilePathInEditor => _filePathInEditor;

        public Localization() {}

        public Localization(Localization other)
        {
            _locale = other._locale;
            _localizedItems = other._localizedItems;
            _filePathInEditor = other._filePathInEditor;
            _info = new LocaleInfo(other.Info);
        }
        
        public Localization(string locale, string infoJson, string[] lines, string filePathInEditor)
        {
            _locale = ParseLocale(locale);
            _info = JsonConvert.DeserializeObject<LocaleInfo>(infoJson);
            _localizedItems = new Dictionary<string, LocalizedItem>();
            _filePathInEditor = filePathInEditor;
            
            foreach (var line in lines)
            {
                var localizedItem = ParseLine(line);
                if (localizedItem == null) continue;
                _localizedItems.Add(localizedItem.Key, localizedItem);
            }
        }

        private LocalizedItem ParseLine(string line)
        {
            var localizedItem = new LocalizedItem();
            var items = line.Split(";");
            if (items.Length < 4)
            {
                return null;
            }
            localizedItem.Key = items[0];
            localizedItem.Description = items[1];
            localizedItem.Original = items[2];
            localizedItem.Text = items[3];
            return localizedItem;
        }

        private Locales ParseLocale(string localeLine)
        {
            return Enum.Parse<Locales>(localeLine, true);
        }
    }

    public class LocalizedItem
    {
        public string Key;
        public string Description;
        public string Original;
        public string Text;
    }

    public class LocaleInfo: IEquatable<LocaleInfo>
    {
        public string name;
        public string fullName;
        
        public LocaleInfo() {}

        public LocaleInfo(LocaleInfo other)
        {
            if (other == null) return;
            name = other.name;
            fullName = other.fullName;
        }

        public bool Equals(LocaleInfo other)
        {
            if (other == null) return false;
            return name == other.name && fullName == other.fullName;
        }
    }

    public enum Locales
    {
        en_EN,
        ru_RU,
    }
}
