using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MapSocNetXamarin.ViewModels;
using Xamarin.CommunityToolkit.Extensions;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        private delegate void SortHandler(int status);
        private SortHandler onSort;
        public FriendsPageFiller _filler;
        private PageLozalizator _pageLozalizator;
        private int _currStatus = -1;
        private int currStatus
        {
            get { return _currStatus; }
            set
            {
                int prev = _currStatus;
                if (_currStatus == value)
                    _currStatus = -1;
                else
                    _currStatus = value;
                OnSwitchSortStatus(prev, _currStatus);
                onSort?.Invoke(_currStatus);
            }
        }


        private string[] _statusImagesPaths, _statusNames;
        private Button[] _statusButtons;
        private Image[] _statusImages;
        public FriendsPage()
        {
            InitializeComponent();
            _pageLozalizator = new PageLozalizator(this);

            _statusButtons = new Button[9]{ HomeStatus, WalkStatus, BeerStatus, SoccerStatus, LoveStatus,
            BowlingStatus, OfferStatus, InstStatus, YoutubeStatus };
            _statusImages = new Image[9] { HomeStatusImage, WalkStatusImage, BeerStatusImage,  SoccerStatusImage , LoveStatusImage,
            BowlingStatusImage, OfferStatusImage, InstStatusImage, YoutubeStatusImage };

            InitStatusesNames();
            for (int i = 0; i < _statusButtons.Length; i++)
            {
                _statusButtons[i].Text = _statusNames[i];
                _statusImages[i].Source = _statusImagesPaths[i];
                int statusID = i;
                _statusButtons[i].Clicked += delegate { SetStatus(statusID); };
            }

            _filler = new FriendsPageFiller(this);
        }

        public void SetStatus(int i)
        {
            currStatus = i;
        }
        private void InitStatusesNames()
        {
            _statusImagesPaths = new string[9] {"homestatus.png", "walkstatus.png", "beerstatus.png", "soccerstatus.png", "lovestatus.png",
            "bowlingstatus.png", "offersstatus.png", "inststatus", "youtubestatus.png"};

            _statusNames = new string[9] {(string)Resources["CURR_StHome"], (string)Resources["CURR_StWalk"], (string)Resources["CURR_StParty"]
                , (string)Resources["CURR_StSport"], (string)Resources["CURR_StLove"],
            (string)Resources["CURR_StCheel"], (string)Resources["CURR_StOffers"], (string)Resources["CURR_StInst"], (string)Resources["CURR_StYouTube"]};
        }
        public FriendItem AddFriendToList(string avatarId, string name, int statusID, string points, string id)
        {
            string statusText = _statusNames[statusID];
            string statusImgLink = _statusImagesPaths[statusID];
            FriendItem friendItem = new FriendItem(avatarId, name, statusImgLink, statusText, points, id, statusID);
            onSort += friendItem.OnSort;
            RootParent.Children.Add(friendItem);
            return friendItem;
        }
        public Layout<View> GetRootParent()
        {
            return RootParent;
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

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            if (!SearchInput.IsReadOnly)
            {
                SearchInput.IsReadOnly = true;
                _filler.SearchUser(SearchInput.Text);
                SearchInput.Text = (string)Resources["CURR_Lable"];
            }
            else
            {
                SearchInput.IsReadOnly = false;
                SearchInput.Focus();
                SearchInput.Text = "";
            }
        }

        private void OnSwitchSortStatus(int prevSt, int newSt)
        {
            if (prevSt != -1)
            {
                _statusButtons[prevSt].TextColor = Color.Black;
            }
            if (newSt != -1)
            {
                _statusButtons[newSt].TextColor = (Color)Application.Current.Resources["CurrentMainColor"];
            }
        }

        public void ChangeSelectedStatusColor(object sender, EventArgs e)
        {
            if(currStatus >=0 && currStatus < _statusButtons.Length)
                _statusButtons[currStatus].TextColor = (Color)Application.Current.Resources["CurrentMainColor"];
        }

    }
}