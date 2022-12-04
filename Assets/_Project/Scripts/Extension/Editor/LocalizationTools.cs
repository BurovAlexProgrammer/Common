using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using _Project.Scripts.Main.Wrappers;

namespace _Project.Scripts.Extension.Editor
{
    public class LocalizationTools
    {
        private static LocalizationTools _instance;
        private static LocalizationToolsSettings _settings;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        public static LocalizationTools Instance => _instance ?? new LocalizationTools();
        public Dictionary<Locales, Localization> Localizations => _localizations;
        public Localization OriginalLocalization => _originalLocalization;


        private LocalizationTools()
        {
            _localizations = new Dictionary<Locales, Localization>();
            _settings = Common.LoadSingleAsset<LocalizationToolsSettings>();
            if (_settings == null) throw new Exception("LocalizationToolsSettings asset not found.");
            
            LoadLocalizations();
            _originalLocalization = _localizations.Single(x => x.Key == _settings.OriginalLocale).Value;
        }

        private void LoadLocalizations()
        {
            var filePaths = Directory.GetFiles(_settings.LocalizationStorePath, "*.csv");
            
            foreach (var filePath in filePaths)
            {
                var fileLines = File.ReadAllLines(filePath);
                var newLocalization = Parse(fileLines, filePath); 
                _localizations.Add(newLocalization.Locale, newLocalization);
            }
        }

        private Localization Parse(string[] lines, string filePath)
        {
            var locale = lines[0];
            var formatInfoMaybeJson = lines[1];
            var hint = lines[2];

            var itemList = new List<string>();
            for (var i = 3; i < lines.Length; i++)
            {
                itemList.Add(lines[i]);
            }

            return new Localization(locale, formatInfoMaybeJson, itemList.ToArray(), filePath);
        }
    }
}