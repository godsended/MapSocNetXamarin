using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MapSocNetXamarin.Models;
using MapSocNetXamarin.ViewModels;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExistAccountLoginPage : ContentPage
    {
        private PageLozalizator _pageLozalizator;
        public ExistAccountLoginPage()
        {
            InitializeComponent();
            Auth.isRestoringPass = false;
            _pageLozalizator = new PageLozalizator(this);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            setPageAlementsInterectable(false);

            string mail = MailInput.Text;
            Auth.AuthResponse ans = await Auth.SignIn(MailInput.Text, PassInput.Text);

            if (ans.Error != null)
            {
                if ((int)ans.Error == 3)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_DataError"], (string)Resources["CURR_Cancel"]);
                }
                else if ((int)ans.Error == 2)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_DataError"], (string)Resources["CURR_Cancel"]);
                }
                else if((int)ans.Error == 1)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_LoginError"], (string)Resources["CURR_Cancel"]);
                }
                setPageAlementsInterectable(true);
                return;
            }

            try
            {
                Auth.id = ans.Id;
                Auth.token = ans.Token;
                Auth.findToken = ans.FindToken;
                Auth.mail = mail;
                await Navigation.PushModalAsync(Pages.AppNavPage, true);
            }
            catch (Exception er)
            {
                await DisplayAlert("Ошибка", "Обратитесь в тех поддержку или разработчику\n" + er.ToString(), "Назад");
            }

            setPageAlementsInterectable(true);
        }
        private void setPageAlementsInterectable(bool isActive)
        {
            MailInput.IsReadOnly = !isActive;
            PassInput.IsReadOnly = !isActive;
            LoginButton.IsEnabled = isActive;
            OpenLoginPage.IsEnabled = isActive;
            RestorePassButton.IsEnabled = isActive;
        }
        private void OpenLoginPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        private void RestorePassButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RestorePassPage(), true);
        }
    }
}