using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Wrappers
{
    public static class Localization
    {

    }

    public class LocalizedItem
    {
        public string Key;
        public string Description;
        public string Original;
        public string Text;
    }

    public class LocaleItem
    {
        public Locales Locale;
        public string name;
    }

    public enum Locales
    {
        en_EN,
        ru_RU,
    }
}
