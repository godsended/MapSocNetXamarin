using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;

using MapSocNetXamarin.Models;
namespace MapSocNetXamarin.ViewModels
{
    internal class TextMessage : ViewCell
    {
        private bool isMyMessage;
        private string text;
        private string time;

        internal bool IsMyMessage
        {
            get { return isMyMessage; }
            set { isMyMessage = value; SetupMesageOwner(); }
        }

        internal string Text
        {
            get { return text; }
            set { text = value; textLabel.Text = value; }
        }

        internal string Time
        {
            get { return time; }
            set { time = value; timeLabel.Text = value; }
        }

        private Label textLabel, timeLabel;
        private Xamarin.Forms.Shapes.Rectangle backGround;
        public TextMessage()
        {

            Grid horizontalLayout = new Grid()
            {
                HorizontalOptions = LayoutOptions.End,
                RowSpacing = 1
            };
            horizontalLayout.RowDefinitions.Add(new RowDefinition());
            horizontalLayout.RowDefinitions.Add(new RowDefinition());
            backGround = new Xamarin.Forms.Shapes.Rectangle()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RadiusX = 25,
                RadiusY = 25,

            };
            horizontalLayout.Children.Add(backGround);
            textLabel = new Label()
            {
                FontFamily = "Light",
                FontSize = 17,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(15, 0, 15, 0),
                TextColor = Color.Black
            };
            timeLabel = new Label()
            {
                FontFamily = "Light",
                FontSize = 13,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(15, 0, 15, 0),
                HorizontalTextAlignment = TextAlignment.End,
                TextColor = Color.Black
            };
            horizontalLayout.Children.Add(textLabel);
            horizontalLayout.Children.Add(timeLabel);
            Grid.SetRow(backGround, 0);
            Grid.SetRowSpan(backGround, 2);
            Grid.SetRow(textLabel, 0);
            Grid.SetRow(timeLabel, 1);
            View = horizontalLayout;
        }

        private void SetupMesageOwner()
        {
            if (isMyMessage)
            {
                textLabel.HorizontalTextAlignment = TextAlignment.End;
                backGround.Fill = (Brush)Application.Current.Resources["CurrentSecondaryBrush"];
            }
            else
            {
                textLabel.HorizontalTextAlignment = TextAlignment.Start;
                //HorizontalOptions = LayoutOptions.Start;
                //WidthRequest = Math.Min(250, 20 * text.Length + 50);
                //HeightRequest = MinimumHeightRequest + 20;
                backGround.Fill = new SolidColorBrush(Color.FromHex("#F2F4F7"));
                View.HorizontalOptions = LayoutOptions.Start;
            }
        }
    }

}

