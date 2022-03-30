using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using MapSocNetXamarin.Models;
using MapSocNetXamarin.ViewModels;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private PageLozalizator _pageLocalizator;
        public SettingsPage()
        {
            InitializeComponent();
            _pageLocalizator = new PageLozalizator(this);
            InitLocalization(null, null);
            Settings.ThemeChanged += InitLocalization;
        }
        private void InitLocalization(object sender, EventArgs e)
        {
            if (Localization.CurrentLanguage == Localization.RU)
            {
                SelectRussianLanguageButton_Clicked(null, null);
            }
            if (Localization.CurrentLanguage == Localization.ENG)
            {
                SelectEnglishLanguageButton_Clicked(null, null);
            }
        }
        private void DefaultThemeButton_Clicked(object sender, EventArgs e)
        {
            Settings.SelectTheme(Settings.Theme.Default);
        }

        private void BlueThemeButton_Clicked(object sender, EventArgs e)
        {
            Settings.SelectTheme(Settings.Theme.Blue);
        }

        private void DarkBlueThemeButton_Clicked(object sender, EventArgs e)
        {
            Settings.SelectTheme(Settings.Theme.DarkBlue);
        }

        private void PinkThemeButton_Clicked(object sender, EventArgs e)
        {
            Settings.SelectTheme(Settings.Theme.Pink);
        }

        private async void ShareIdButoon_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(Auth.findToken);
            await DisplayAlert((string)Resources["CURR_YourId"], (string)Resources["CURR_SuccesChanged"], (string)Resources["CURR_Cancel"]);
        }

        private void SelectRussianLanguageButton_Clicked(object sender, EventArgs e)
        {
            UncolorLanguageLabels();
            RussianLanguageLabel.TextColor = (Color)Application.Current.Resources["CurrentMainColor"];
            if (Localization.CurrentLanguage != Localization.RU)
                Localization.CurrentLanguage = Localization.RU;
        }

        private void SelectEnglishLanguageButton_Clicked(object sender, EventArgs e)
        {
            UncolorLanguageLabels();
            EnglishLanguageLabel.TextColor = (Color)Application.Current.Resources["CurrentMainColor"];
            if (Localization.CurrentLanguage != Localization.ENG)
                Localization.CurrentLanguage = Localization.ENG;
        }
        private void UncolorLanguageLabels()
        {
            RussianLanguageLabel.TextColor = Color.Black;
            EnglishLanguageLabel.TextColor = Color.Black;
        }

        private async void ChangePass_Clicked(object sender, EventArgs e)
        {
            if (NewPassEntry.Text != null && OldPassEntry.Text != null && RepNewPassEntry.Text != null)
            {
                if (NewPassEntry.Text.Length < 6)
                {
                    await DisplayAlert((string)Resources["CURR_PassChanging"], (string)Resources["CURR_ShortPassError"], (string)Resources["CURR_Cancel"]);
                }
                else if (NewPassEntry.Text != RepNewPassEntry.Text)
                {
                    await DisplayAlert((string)Resources["CURR_PassChanging"], (string)Resources["CURR_RepPassError"], (string)Resources["CURR_Cancel"]);
                }
                else
                {
                    if (await Auth.ChangePassword(OldPassEntry.Text, NewPassEntry.Text))
                    {
                        await DisplayAlert((string)Resources["CURR_PassChanging"], (string)Resources["CURR_PassChangingText"], (string)Resources["CURR_Cancel"]);
                    }
                    else
                    {
                        await DisplayAlert((string)Resources["CURR_PassChanging"], (string)Resources["CURR_PassError"], (string)Resources["CURR_Cancel"]);
                    }
                }
            }
            else
            {
                await DisplayAlert((string)Resources["CURR_PassChanging"], (string)Resources["CURR_PassError"], (string)Resources["CURR_Cancel"]);
            }
        }

        private async void CacheButton_Clicked(object sender, EventArgs e)
        {
            Settings.ClearCache();
            await DisplayAlert((string)Resources["CURR_CacheClearing"], (string)Resources["CURR_CacheClearingText"], (string)Resources["CURR_Cancel"]);
        }

        private void LeaveAccButton_Clicked(object sender, EventArgs e)
        {
            Settings.ClearCache();
            Auth.ClearData();
            Navigation.PopModalAsync();
        }
    }
}