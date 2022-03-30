using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapSocNetXamarin.Models
{
    class Localization
    {
        public const string RU = "RU";
        public const string ENG = "ENG";
        public static EventHandler LanguageChanged;

        private static string _currentLanguage;
        public static string CurrentLanguage
        {
            get
            {
                if (_currentLanguage == "" || _currentLanguage == null)
                {
                    if (App.Current.Properties.TryGetValue("Settings_language", out object obj))
                    {
                        _currentLanguage = (string)obj;
                        if (_currentLanguage != RU && _currentLanguage != ENG)
                            _currentLanguage = RU;
                        return _currentLanguage;;

                    }
                    else
                    {
                        _currentLanguage = RU;
                        return _currentLanguage; ;
                    }
                }
                else
                {
                    return _currentLanguage;
                }
            }
            set
            {
                _currentLanguage = value;
                App.Current.Properties["Settings_language"] = _currentLanguage;
                Application.Current.SavePropertiesAsync();
                LanguageChanged.Invoke(null, null);
            }
        }
    }
}
