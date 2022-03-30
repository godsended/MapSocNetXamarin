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
    public partial class LoginPage : ContentPage
    {
        private Gender _selectedGender;
        private bool _isGenderSelected;
        private PageLozalizator _pageLozalizator;
        private enum Gender
        {
            Male,
            Female
        }
        public LoginPage()
        {
            InitializeComponent();
            _pageLozalizator = new PageLozalizator(this);
            MaleButton_Clicked(this, new EventArgs());
            if (Auth.isRestoringPass)
            {
                RestorePassPage rpp = new RestorePassPage();
                _ = Navigation.PushModalAsync(rpp);
                rpp.OnAfterclosePassRestore();
                _ = DisplayAlert((string)Resources["CURR_RestorePass"], (string)Resources["CURR_RestorePassText"], (string)Resources["CURR_Submit"]);
            }
            else
            {
                TryAutoLogin();
            }
        }
        private async void TryAutoLogin()
        {
            if (!string.IsNullOrEmpty(Auth.id) && !string.IsNullOrEmpty(Auth.token))
            {
                setPageAlementsInterectable(false);
                Auth.AuthResponse ans = await Auth.SignInWithToken(Auth.id, Auth.token);
                if (ans != null)
                {
                    if (ans.Error == null)
                    {
                        System.Diagnostics.Debug.WriteLine("AutoLogin successful");
                        await Navigation.PushModalAsync(Pages.AppNavPage, true);
                        setPageAlementsInterectable(true);
                        return;
                    }
                }
                System.Diagnostics.Debug.WriteLine("AutoLogin failed");
                setPageAlementsInterectable(true);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Id or token is empty. AutoLogin failed");
            }
        }

        private void MaleButton_Clicked(object sender, EventArgs e)
        {
            _selectedGender = Gender.Male;
            MaleButtonImage.Fill = new SolidColorBrush(Color.FromHex("7BD0FF"));
            MaleButton.TextColor = Color.White;
            MaleImage.Source = "whitemale.png";
            FemaleButtonImage.Fill = Brush.White;
            FemaleButton.TextColor = Color.FromHex("0F0A24");
            FemaleImage.Source = "female.png";
            _isGenderSelected = true;
        }

        private void FemaleButton_Clicked(object sender, EventArgs e)
        {
            _selectedGender = Gender.Female;
            MaleButtonImage.Fill = Brush.White;
            MaleButton.TextColor = Color.FromHex("0F0A24");
            MaleImage.Source = "male.png";
            FemaleButtonImage.Fill = new SolidColorBrush(Color.FromHex("FFB0FC"));
            FemaleButton.TextColor = Color.White;
            FemaleImage.Source = "whitefemale.png";
            _isGenderSelected = true;
        }

        private async void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            setPageAlementsInterectable(false);
            if (!await validateInputs())
            {
                setPageAlementsInterectable(true);
                return;
            }

            string fName = NameInput.Text.Split(' ')[0];
            string sName = NameInput.Text.Split(' ')[1];
            string gender = "";

            if (_selectedGender == Gender.Male)
                gender = "M";
            if (_selectedGender == Gender.Female)
                gender = "F";

            string mail = MailInput.Text;

            Auth.AuthResponse ans = await Auth.SignUp(fName, sName, mail, PassInput.Text, gender);

            if (ans.Error != null)
            {
                if (ans.Error == 2)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_ExMailError"], (string)Resources["CURR_Cancel"]);
                }
                else if (ans.Error == 3)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_DataError"], (string)Resources["CURR_Cancel"]);
                }
                else if (ans.Error == 1)
                {
                    await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_RegError"], (string)Resources["CURR_Cancel"]);
                }

                setPageAlementsInterectable(true);
                return;
            }

            try
            {
                Auth.id = ans.Id;
                Auth.token = ans.Token;
                Auth.findToken = ans.FindToken;
                Auth.mail = ans.Mail;
                await Navigation.PushModalAsync(Pages.AppNavPage, true);

            }
            catch (Exception er)
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_DevError"] + '\n' + er.ToString(),
                    (string)Resources["CURR_Cancel"]);
            }

            setPageAlementsInterectable(true);
        }
        private void setPageAlementsInterectable(bool isActive)
        {
            MailInput.IsReadOnly = !isActive;
            NameInput.IsReadOnly = !isActive;
            PassInput.IsReadOnly = !isActive;
            RegistrationButton.IsEnabled = isActive;
            FemaleButton.IsEnabled = isActive;
            MaleButton.IsEnabled = isActive;
            OpenExAccLoginPage.IsEnabled = isActive;
        }
        private void OpenExAccLoginPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ExistAccountLoginPage(), true);
        }
        private async Task<bool> validateInputs()
        {
            if (string.IsNullOrEmpty(NameInput.Text) || string.IsNullOrWhiteSpace(NameInput.Text))
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_NameError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (!_isGenderSelected)
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_GenderError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (string.IsNullOrEmpty(MailInput.Text) || string.IsNullOrWhiteSpace(MailInput.Text))
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_MailError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (string.IsNullOrEmpty(PassInput.Text) || string.IsNullOrWhiteSpace(PassInput.Text))
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_PassError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (NameInput.Text.Split(' ').Length != 2)
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_NameError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (PassInput.Text.Length < 6)
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_ShortPassError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            if (!isValidEmail(MailInput.Text))
            {
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_IncorrectMailError"], (string)Resources["CURR_Cancel"]);
                return false;
            }
            return true;
        }
        private bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}