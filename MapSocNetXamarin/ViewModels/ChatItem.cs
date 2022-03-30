using System;

using Xamarin.Forms;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;

using MapSocNetXamarin.Views;

namespace MapSocNetXamarin.ViewModels
{
    public class ChatItem : Grid
    {
        public string chatID, userID, name;

        public ChatItem(string avatarLink, string name, string lastMsgAvatar, string lastMsgText, string chatID, string userID)
        {
            this.chatID = chatID;
            this.userID = userID;
            this.name = name;
            HeightRequest = 70;
            Margin = new Thickness(10, 0, 10, 0);
            CachedImage avatar = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 70,
                WidthRequest = 70,
                Source = new DataUrlImageSource(avatarLink),
            };
            CircleTransformation ct = new CircleTransformation();
            avatar.Transformations.Add(ct);
            this.Children.Add(avatar);
            Label nick = new Label()
            {
                Text = name,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(80, 0, 0, 0),
                FontSize = 20,
                TextColor = Color.Black,
                FontFamily = "SemiBold",
            };
            this.Children.Add(nick);
            CachedImage msgAvatar = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(80, 0, 0, 0),
                HeightRequest = 30,
                WidthRequest = 30,
                Source = lastMsgAvatar
            };
            this.Children.Add(msgAvatar);
            Label lastMsg = new Label()
            {
                Text = lastMsgText,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(120, 0, 0, 0),
                FontSize = 17,
                TextColor = Color.Black,
                FontFamily = "Light",
            };
            this.Children.Add(lastMsg);
            Button actionButton = new Button()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Transparent,

            };
            actionButton.Clicked += OnClick;
            this.Children.Add(actionButton);
        }

        public void Delete()
        {
            Parent = null;
        }
        public void OnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatPage(chatID, userID, name));
        }
    }
}

