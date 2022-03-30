using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MapSocNetXamarin.Models;

namespace MapSocNetXamarin.ViewModels
{
    public class PageLozalizator
    {
        private Page _page;
        private Dictionary<string, Dictionary<string, string>> _languages;
        public PageLozalizator(Page page)
        {
            _page = page;
            Init();
        }

        private void Init()
        {
            try
            {
                _languages = new Dictionary<string, Dictionary<string, string>>();
                _languages.Add(Localization.RU, new Dictionary<string, string>());
                _languages.Add(Localization.ENG, new Dictionary<string, string>());

                foreach (string res in _page.Resources.Keys)
                {
                    string lang = res.Split('_')[0];
                    string key = res.Split('_')[1];
                    string val = (string)_page.Resources[res];

                    try
                    {
                        if(lang == Localization.RU || lang == Localization.ENG)
                        _languages[lang].Add(key, val);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        System.Diagnostics.Debug.WriteLine("Not correct string localization resource");
                    }
                }
                Localization.LanguageChanged += onLangChanged;
                onLangChanged(null, null);
            }
            catch ( Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private void onLangChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("XXXXXXXXXXXXXXXXXXXXXX_" + Localization.CurrentLanguage);
            foreach(string key in  _languages[Localization.CurrentLanguage].Keys)
            {
                _page.Resources["CURR_" + key] = _languages[Localization.CurrentLanguage][key];
            }
        }
    }
}
