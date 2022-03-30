using System;
using System.Collections.Generic;
using System.Text;

using MapSocNetXamarin.Views;
using MapSocNetXamarin.Models;
using MapSocNetXamarin.Networking;

namespace MapSocNetXamarin.ViewModels
{
    internal class ChatPageFiller
    {
        private ChatPage _chatPage;
        private string _chatID, _userID;
        internal ChatPageFiller(ChatPage chatPage, string chatID, string userID)
        {
            _chatPage = chatPage;
            _chatID = chatID;
            _userID = userID;

            //_chatPage.AvatarImageUrl = Network.URLs.avatarsById + userID;
            Init();
        }
        internal async void Init()
        {
            string data = await Chat.InitChat(_chatID);

            string[] masgsData  = data.Split('|');
            for(int i = 0; i < masgsData.Length - 1; i++)
            {
                string[] msgData = masgsData[i].Split('&');
                _chatPage.AddTextMessage(Auth.id == msgData[2], msgData[1], msgData[0]);
            }
        }
        internal void OpenProfile()
        {
            _chatPage.Navigation.PushAsync(new UserPage(_userID));
        }
        internal async void SendMessage(string message)
        {
            _chatPage.SetInputInteractable(false);
            string ans = await Chat.SendMessage(_chatID, message);
            if(ans == "ok")
            {
                _chatPage.ClearInput();
                _chatPage.AddTextMessage(true, message, DateTime.Now.TimeOfDay.ToString());
            }
            else
            {
                _chatPage.ShowMeassageErrorAlert();
            }
            _chatPage.SetInputInteractable(true);

        }
    }
}
