using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MapSocNetXamarin.Views;

namespace MapSocNetXamarin.Models
{
    public static class Pages
    {
        private static Page _profilePage, _appNavPage;

        public static Page ProfilePage
        {
            get
            {
                if (_profilePage == null)
                    _profilePage = new NavigationPage(new ProfilePage());
                return _profilePage;
            }
            set
            {
                _profilePage = value;
            }
        }
        public static Page AppNavPage
        {
            get
            {
                if (_appNavPage == null)
                    _appNavPage = new AppNavigationPage();
                return _appNavPage;
            }
            set
            {
                _appNavPage = value;
            }
        }
    }
}
