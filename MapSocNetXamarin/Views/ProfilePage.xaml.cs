using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapSocNetXamarin.ViewModels;
using MapSocNetXamarin.Models;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private ProfileDataFiller _profileDataFiller;
        private Button[] _statusButtons;
        private Image[] _statusImages;
        private string[] _statusNames;
        private string[] _statusImagesPaths;
        private int currStatus;
        private PageLozalizator _pageLozalizator;

        public ProfilePage()
        {
            InitializeComponent();
            _pageLozalizator = new PageLozalizator(this);
            _profileDataFiller = new ProfileDataFiller(this);
            _profileDataFiller.LoadDescribtion(Auth.id);

            CircleTransformation ct = new CircleTransformation();
            Avatar.Transformations.Add(ct);

            _statusButtons = new Button[9]{ HomeStatus, WalkStatus, BeerStatus, SoccerStatus, LoveStatus,
            BowlingStatus, OfferStatus, InstStatus, YoutubeStatus };
            _statusImages = new Image[9] { HomeStatusImage, WalkStatusImage, BeerStatusImage,  SoccerStatusImage , LoveStatusImage,
            BowlingStatusImage, OfferStatusImage, InstStatusImage, YoutubeStatusImage };

            Localization.LanguageChanged += ReloadStatusesNames;
            InitStatusesNames();

            for (int i = 0; i < _statusButtons.Length; i++)
            {
                _statusButtons[i].Text = _statusNames[i];
                _statusImages[i].Source = _statusImagesPaths[i];
                int statusID = i;
                _statusButtons[i].Clicked += delegate { SetStatus(statusID); };
            }
        }

        private void InitStatusesNames()
        {

            _statusImagesPaths = new string[9] {"homestatus.png", "walkstatus.png", "beerstatus.png", "soccerstatus.png", "lovestatus.png",
            "bowlingstatus.png", "offersstatus.png", "inststatus", "youtubestatus.png"};
            _statusNames = new string[9] {(string)Resources["CURR_StHome"], (string)Resources["CURR_StWalk"], (string)Resources["CURR_StParty"]
                , (string)Resources["CURR_StSport"], (string)Resources["CURR_StLove"],
            (string)Resources["CURR_StCheel"], (string)Resources["CURR_StOffers"], (string)Resources["CURR_StInst"], (string)Resources["CURR_StYouTube"]};
        }
        private void ReloadStatusesNames(object sender, EventArgs e)
        {
            InitStatusesNames();
            SetLocaleStatus(currStatus);
        }
        private void ShortDescribtion_TextChanged(object sender, TextChangedEventArgs e)
        {
            _profileDataFiller.ShortDescribtionChanged(e);
        }

        private void LongDescribtion_TextChanged(object sender, TextChangedEventArgs e)
        {
            _profileDataFiller.LongDescribtionChanged(e);
        }
        public void SetName(string value)
        {
            Nick.Text = value;
        }
        public CachedImage GetAvatar()
        {
            return Avatar;
        }
        public void SetShortDescribtion(string text)
        {
            ShortDescribtion.Text = text;
        }
        public void SetLongDescribtion(string text)
        {
            LongDescribtion.Text = text;
        }
        public async void OnLoadError()
        {
            await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_ProfileError"], (string)Resources["CURR_Cancel"]);
            await Navigation.PopAsync(true);
        }

        private async void AvatarButton_Clicked(object sender, EventArgs e)
        {
            AvatarButton.IsEnabled = false;
            bool succes = await _profileDataFiller.UploadAvatar();
            AvatarButton.IsEnabled = true;

            if (!succes)
                await DisplayAlert((string)Resources["CURR_Error"], (string)Resources["CURR_PhotoError"], (string)Resources["CURR_Cancel"]);
        }

        private async void StatusButton_Clicked(object sender, EventArgs e)
        {
            if (StatusesView.IsVisible)
            {
                await StatusesViewFill.FadeTo(1, 400, Easing.SinIn);
                //await StatusesView.TranslateTo(100, 0, 400, Easing.SinIn);
                StatusesView.IsVisible = !StatusesView.IsVisible;
            }
            else
            {
                StatusesView.IsVisible = !StatusesView.IsVisible;
                await StatusesViewFill.FadeTo(0, 400, Easing.SinOut);
                //await StatusesView.TranslateTo(0, 0, 400, Easing.SinOut);
            }
        }
        private async void Status_Clicked(object sender, EventArgs e)
        {
            if (StatusesView.IsVisible)
            {
                await StatusesViewFill.FadeTo(1, 400, Easing.SinIn);
                //await StatusesView.TranslateTo(100, 0, 400, Easing.SinIn);
                StatusesView.IsVisible = !StatusesView.IsVisible;
            }
            else
            {
                StatusesView.IsVisible = !StatusesView.IsVisible;
                await StatusesViewFill.FadeTo(0, 400, Easing.SinOut);
                //await StatusesView.TranslateTo(0, 0, 400, Easing.SinOut);
            }
        }
        public void SetLocaleStatus(int i)
        {
            StatusButton.Text = _statusNames[i];
            StatusImage.Source = _statusImagesPaths[i];
            currStatus = i;
        }
        private void SetStatus(int i)
        {
            StatusButton.Text = _statusNames[i];
            StatusImage.Source = _statusImagesPaths[i];
            _profileDataFiller.ChangeStatus(i);
            currStatus = i;
        }
        public void AddGaleryImage(string url)
        {
            CachedImage c = new CachedImage()
            {
                Source = ImageSource.FromUri(new Uri(url)),
                DownsampleToViewSize = true,

            };
            GaleryContent.Children.Insert(1, c);
        }
        public void AddGaleryImageByStream(byte[] data)
        {
            CachedImage c = new CachedImage()
            {
                Source = ImageSource.FromStream(() => new System.IO.MemoryStream(data)),
                DownsampleToViewSize = true
            };
            GaleryContent.Children.Insert(1, c);
        }
        private void AddPhotoButton_Clicked(object sender, EventArgs e)
        {
            _ = _profileDataFiller.UploadImage();
        }
    }
}