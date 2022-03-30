using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MapSocNetXamarin.Models;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestorePassPage : ContentPage
    {
        private RestoreStep _restoreStep = 0;
        private string _currMail, _currCode;
        public RestorePassPage()
        {
            InitializeComponent();
            Auth.isRestoringPass = false;
        }
        
        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            switch(_restoreStep)
            {
                case RestoreStep.GenerateCode:

                    SubmitButton.IsEnabled = false;
                    MailInput.IsReadOnly = true;

                    _currMail = MailInput.Text;

                    if(await Auth.GeneratePassRestoreCode(_currMail))
                    {
                        CodeInputElement.IsVisible = true;

                        _restoreStep++;

                        Auth.mail = _currMail;
                    }
                    else
                    {
                        MailInput.IsReadOnly = false;

                        await DisplayAlert("Ошибка", "Проверьте введенную почту и попробуйте еще раз", "Назад");
                    }

                    SubmitButton.IsEnabled = true;

                    break;
                case RestoreStep.ApplyCode:

                    SubmitButton.IsEnabled = false;
                    MailInput.IsReadOnly = true;
                    CodeInput.IsReadOnly = true;

                    _currCode = CodeInput.Text;

                    if (await Auth.ApplyPassRestoreCode(_currCode, _currMail))
                    {
                        NewPassInputElement.IsVisible = true;

                        _restoreStep++;
                    }
                    else
                    {
                        CodeInput.IsReadOnly = false;
                        CodeInput.Text = "";

                        await DisplayAlert("Ошибка", "Неверный код", "Назад");
                    }
                    SubmitButton.IsEnabled = true;
                    break;
                case RestoreStep.CreateNewPass:
                    SubmitButton.IsEnabled = false;
                    MailInput.IsReadOnly = true;
                    CodeInput.IsReadOnly = true;
                    NewPassInput.IsReadOnly = true;
                    if (await Auth.ChangePasswordWithCode(_currCode, _currMail, NewPassInput.Text))
                    {
                        await DisplayAlert("Смена пароля", "Пароль успешно изменен", "Назад");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        SubmitButton.IsEnabled = true;
                        NewPassInput.IsReadOnly = false;
                        await DisplayAlert("Ошибка", "Придумайте другой пароль", "Назад");
                    }

                    break;
            }
        }
        public void OnAfterclosePassRestore()
        {
            SubmitButton.IsEnabled = true;
            CodeInputElement.IsVisible = true;
            MailInput.IsReadOnly = true;
            CodeInput.IsReadOnly = false;

            MailInput.Text = Auth.mail;

            _restoreStep = RestoreStep.ApplyCode;
            _currMail = Auth.mail;

            Auth.isRestoringPass = true;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        public enum RestoreStep
        {
            GenerateCode,
            ApplyCode,
            CreateNewPass
        }
    }
}