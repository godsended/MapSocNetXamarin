using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;

using FFImageLoading.Forms;
using MapSocNetXamarin.Views;
using MapSocNetXamarin.Models;
using FFImageLoading.Transformations;

namespace MapSocNetXamarin.ViewModels
{
    public class FriendItem : Grid
    {
        private int statusID = -1;
        public string id;

        public FriendItem(string avatarId, string name, string statusImgLink, string status, string points, string id, int statusID)
        {
            this.id = id;
            this.statusID = statusID;
            System.Diagnostics.Debug.WriteLine("STATUS: " + status);
            HeightRequest = 70;
            Margin = new Thickness(10, 0, 10, 0);

            LoadAvatar(avatarId);

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
            CachedImage si = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(80, 0, 0, 0),
                HeightRequest = 30,
                WidthRequest = 30,
                Source = statusImgLink
            };
            this.Children.Add(si);
            Label statusLabel = new Label()
            {
                Text = status,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(120, 0, 0, 0),
                FontSize = 17,
                TextColor = Color.Black,
                FontFamily = "Light",
            };
            this.Children.Add(statusLabel);
            CachedImage trophy = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 17, 10),
                HeightRequest = 30,
                WidthRequest = 30,
                Source = new DataUrlImageSource("rate4.png"),
            };
            this.Children.Add(trophy);
            Grid rate = new Grid()
            {
                Margin = new Thickness(0, 50, 0, 0),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
            };
            this.Children.Add(rate);
            CachedImage bg = new CachedImage()
            {
                HeightRequest = 65,
                WidthRequest = 65,
                Source = new DataUrlImageSource("rateBG.png"),
            };
            rate.Children.Add(bg);
            Label rateLabel = new Label()
            {
                Text = points,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.White,
                FontFamily = "SemiBold",
            };
            rate.Children.Add(rateLabel);
            Button actionButton = new Button()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Transparent,
                
            };
            actionButton.Clicked += OnClick;
            this.Children.Add(actionButton);
        }

        public void OnSort(int status)
        {
            if (this.statusID == status || status == -1)
                IsVisible = true;
            else
                IsVisible = false;
        }

        public void Delete()
        {
            Parent = null;
        }
        public void OnClick(object sender, EventArgs e)
        {
            UserPage up = new UserPage();
            Navigation.PushAsync(up, true);
            up.id = id;
        }
        public async void LoadAvatar(string avatarId)
        {

            byte[] avData = await Galery.LoadImage(Auth.id, avatarId);

            Stream stream = new MemoryStream(avData);
            ImageSource source = ImageSource.FromStream(() => stream);

            CachedImage ci = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 70,
                WidthRequest = 70,
                Source = source,
            };
            CircleTransformation ct = new CircleTransformation();
            ci.Transformations.Add(ct);
            this.Children.Add(ci);
        }
    }

}
