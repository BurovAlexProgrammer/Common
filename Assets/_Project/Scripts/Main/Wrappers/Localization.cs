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

        public Locales Locale => _locale;
        public LocaleInfo Info => _info;
        public Dictionary<string, LocalizedItem> LocalizedItems => _localizedItems;


        public Localization(string locale, string infoJson, string[] lines)
        {
            _locale = ParseLocale(locale);
            _info = JsonConvert.DeserializeObject<LocaleInfo>(infoJson);
            _localizedItems = new Dictionary<string, LocalizedItem>();
            
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

    public class LocaleInfo
    {
        public string name;
    }

    public enum Locales
    {
        en_EN,
        ru_RU,
    }
}
