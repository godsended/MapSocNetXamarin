using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapSocNetXamarin.Views;
using MapSocNetXamarin.Models;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
#region
[assembly: ExportFont("FiraSansLight.otf", Alias = "Light")]
[assembly: ExportFont("FiraSans-SemiBold.otf", Alias = "SemiBold")]
[assembly: ExportFont("FiraSans-Regular.otf", Alias = "Regular")]

#endregion
namespace MapSocNetXamarin
{
    
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Settings.Init();

            //MainPage = new FriendsPage();
            MainPage = new LoginPage();
            //MainPage = new ChatPage("1", Auth.id, "Gatling Bebra");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
