using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MapSocNetXamarin.Models
{
    public static class Settings
    {
        private static int _currentTheme;
        public static event EventHandler ThemeChanged;
        public static Theme CurrentTheme
        {
            get
            {
                if (_currentTheme == 0)
                {
                    if (App.Current.Properties.TryGetValue("Settings_theme", out object obj))
                    {
                        _currentTheme = (int)obj;
                        return (Theme)_currentTheme;
                    }
                    else
                    {
                        return (Theme)_currentTheme;
                    }
                }
                else
                {
                    return (Theme)_currentTheme;
                }
            }
            private set
            {
                _currentTheme = (int)value;
                App.Current.Properties["Settings_theme"] = _currentTheme;
                Application.Current.SavePropertiesAsync();

                InitThemes();
                ThemeChanged.Invoke(_currentTheme, EventArgs.Empty);
            }
        }
        
        public static void Init()
        {
            InitThemes();
        }

        public static void SelectTheme(Theme newTheme)
        {
            CurrentTheme = newTheme;
        }
        public static void InitThemes()
        {
            switch(CurrentTheme)
            {
                case Theme.Blue:
                    Application.Current.Resources["CurrentMainColor"] = Application.Current.Resources["BlueMainColor"];
                    Application.Current.Resources["CurrentSecondaryColor"] = Application.Current.Resources["BlueSecondaryColor"];

                    Application.Current.Resources["CurrentMainBrush"] = Application.Current.Resources["BlueMainBrush"];
                    Application.Current.Resources["CurrentSecondaryBrush"] = Application.Current.Resources["BlueSecondaryBrush"];
                    break;
                case Theme.DarkBlue:
                    Application.Current.Resources["CurrentMainColor"] = Application.Current.Resources["DarkBlueMainColor"];
                    Application.Current.Resources["CurrentSecondaryColor"] = Application.Current.Resources["DarkBlueSecondaryColor"];

                    Application.Current.Resources["CurrentMainBrush"] = Application.Current.Resources["DarkBlueMainBrush"];
                    Application.Current.Resources["CurrentSecondaryBrush"] = Application.Current.Resources["DarkBlueSecondaryBrush"];
                    break;
                case Theme.Default:
                    Application.Current.Resources["CurrentMainColor"] = Application.Current.Resources["DefaultMainColor"];
                    Application.Current.Resources["CurrentSecondaryColor"] = Application.Current.Resources["DefaultSecondaryColor"];

                    Application.Current.Resources["CurrentMainBrush"] = Application.Current.Resources["DefaultMainBrush"];
                    Application.Current.Resources["CurrentSecondaryBrush"] = Application.Current.Resources["DefaultSecondaryBrush"];
                    break;
                case Theme.Pink:
                    Application.Current.Resources["CurrentMainColor"] = Application.Current.Resources["PinkMainColor"];
                    Application.Current.Resources["CurrentSecondaryColor"] = Application.Current.Resources["PinkSecondaryColor"];

                    Application.Current.Resources["CurrentMainBrush"] = Application.Current.Resources["PinkMainBrush"];
                    Application.Current.Resources["CurrentSecondaryBrush"] = Application.Current.Resources["PinkSecondaryBrush"];
                    break;
            } 
        }
        public static void ClearCache()
        {
            try
            {
                var cachePath = System.IO.Path.GetTempPath();

                // If exist, delete the cache directory and everything in it recursivly
                if (System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.Delete(cachePath, true);

                // If not exist, restore just the directory that was deleted
                if (!System.IO.Directory.Exists(cachePath))
                    System.IO.Directory.CreateDirectory(cachePath);
            }
            catch (Exception) { }
        }
        public enum Theme
        {
            Default,
            Blue, 
            DarkBlue,
            Pink
        }
    }
}
