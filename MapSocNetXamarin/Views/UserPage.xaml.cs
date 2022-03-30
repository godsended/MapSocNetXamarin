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
    public partial class UserPage : ContentPage
    {
        private ProfileDataFiller _profileDataFiller;
        private string[] _statusNames;
        private string[] _statusImagesPaths;
        private int currStatus;
        private string _id;
        public string id
        {
            get {  return _id; }
            set { _id = value; _profileDataFiller.LoadDescribtion(_id); System.Diagnostics.Debug.WriteLine("ID " + _id + " VAL " + value); }
        }
        private PageLozalizator _pageLozalizator;

        public UserPage()
        {
            InitializeComponent();

            _pageLozalizator = new PageLozalizator(this);
            _profileDataFiller = new ProfileDataFiller(this);

            CircleTransformation ct = new CircleTransformation();
            Avatar.Transformations.Add(ct);

            InitStatusesNames();
        }
        public UserPage(string id)
        {
            InitializeComponent();

            _pageLozalizator = new PageLozalizator(this);
            _profileDataFiller = new ProfileDataFiller(this);

            CircleTransformation ct = new CircleTransformation();
            Avatar.Transformations.Add(ct);

            InitStatusesNames();
            this.id = id;
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
            SetLocaleStatus(currStatus);
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
       
        public void SetLocaleStatus(int i)
        {
            StatusButton.Text = _statusNames[i];
            StatusImage.Source = _statusImagesPaths[i];
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

        private void FriendActionButton_Clicked(object sender, EventArgs e)
        {
            _profileDataFiller.OnAddFriendButtonClicked();
        }
        public ImageButton GetAddFriendButton()
        {
            return FriendActionButton;
        }

        public void SetupFriendActionButton(Friend.FriendType type)
        {
            switch(type)
            {
                case Friend.FriendType.Stranger:
                    FriendActionButton.Source = (FileImageSource)Application.Current.Resources["AddFriend"];
                    break;
                default:
                    FriendActionButton.Source = (FileImageSource)Application.Current.Resources["HorizontalDots"];
                    break;
            }
        }
        public async Task<bool> SetupAndShowFriendActAlert(Friend.FriendType type)
        {
            switch (type)
            {
                case Friend.FriendType.Friend:
                    return await DisplayAlert((string)Resources["CURR_Friends"],
                        (string)Resources["CURR_DeleteFriendText"],
                        (string)Resources["CURR_Submit"], (string)Resources["CURR_Cancel"]);
                case Friend.FriendType.Sended:
                    return await DisplayAlert((string)Resources["CURR_Friends"], 
                        (string)Resources["CURR_RemoveRequestText"],
                        (string)Resources["CURR_Submit"], (string)Resources["CURR_Cancel"]);
                case Friend.FriendType.Recivied:
                    return await DisplayAlert((string)Resources["CURR_Friends"],
                        (string)Resources["CURR_AcceptRequestText"],
                        (string)Resources["CURR_Submit"], (string)Resources["CURR_Cancel"]);
            }
            return false;
        }

        private void GoChat_Clicked(object sender, EventArgs e)
        {
            _profileDataFiller.UploadChat(id);
        }
    }
}