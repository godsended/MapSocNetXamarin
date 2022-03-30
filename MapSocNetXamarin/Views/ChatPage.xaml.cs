using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MapSocNetXamarin.ViewModels;
using FFImageLoading.Transformations;
using System.Collections.ObjectModel;

namespace MapSocNetXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private ChatPageFiller _filler;
        private PageLozalizator _localizator;
        private ObservableCollection<TextMessage> Messages { get; set; } = new ObservableCollection<TextMessage>();

        internal string NameText { get { return NameLabel.Text; } set { NameLabel.Text = value; } }
        internal string OnlineText => OnlineLabel.Text;
        internal string AvatarImageUrl
        {
            private get { return AvatarImage.Source.ToString(); }
            set { AvatarImage.Source = value; }
        }

        internal ChatPage(string chatID, string userID, string userName)
        {
            InitializeComponent();
            _filler = new ChatPageFiller(this, chatID, userID);
            _localizator = new PageLozalizator(this);
            AvatarImage.Transformations.Add(new CircleTransformation());
            NameText = userName;
            ListView.ItemsSource = Messages;
            ListView.ItemTemplate = new DataTemplate(typeof(TextMessage));
        }

        internal void SetInputInteractable(bool b)
        {
            MessageInput.IsReadOnly = !b;
            SendMessageButton.IsEnabled = b;
        }
        internal void ShowMeassageErrorAlert()
        {

        }
        internal void AddTextMessage(bool isMy, string text, string time)
        {
            Messages.Add(new TextMessage
            {
                IsMyMessage = isMy,
                Text = text,
                Time = time,
            });
            System.Diagnostics.Debug.WriteLine("\n" + Messages.Count);
            //MessagesParent.Children.Add(new TextMessage(text, "00:00", isMy));
            //System.Diagnostics.Debug.WriteLine("MSG ADDED " + MessagesParent.Children.Count);
        }
        internal void ClearInput()
        {
            MessageInput.Text = "";
        }
        private void ProfileButton_Clicked(object sender, EventArgs e)
        {
            _filler.OpenProfile();
        }

        private void SendMessageButton_Clicked(object sender, EventArgs e)
        {
            _filler.SendMessage(MessageInput.Text);
        }
    }
}