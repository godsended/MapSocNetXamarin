using System;
using System.Collections.Generic;
using System.Text;
using MapSocNetXamarin.Models;
using MapSocNetXamarin.Networking;

using MapSocNetXamarin.Views;

namespace MapSocNetXamarin.ViewModels
{
    internal class ChatsPageFiller
    {
        private ChatsList _chatsList;
        public Dictionary<string, ChatItem> ChatItems;
        public ChatsPageFiller(ChatsList chatsList)
        {
            _chatsList = chatsList;
            ChatItems = new Dictionary<string, ChatItem>();
            Update();
        }

        public async void Update()
        {
            ClearChatItems();
            string data = await Chat.LoadChats();

            System.Diagnostics.Debug.WriteLine("LOADING CHATS REQUEST RESULT: " + data);

            if (data == "Error")
                return;

            string[] usetsInfo = data.Split('|');

            foreach (string info in usetsInfo)
            {
                if (info != "")
                {
                    string[] infoArr = info.Split('&');

                    string name = infoArr[0];
                    string chatID = infoArr[1];
                    string lmsgtxt = infoArr[2];
                    //string lmsguserAvataUrl = Network.URLs.avatarsById + infoArr[3];
                    //string avatarUrl = Network.URLs.avatarsById + infoArr[4];
                    string userID = infoArr[4];
                    //AddChatItemToList(avatarUrl, name, lmsguserAvataUrl, lmsgtxt, chatID, userID);
                }
            }
        }
        public void ClearChatItems()
        {
            if (ChatItems.Count > 0)
            {
                foreach (ChatItem item in ChatItems.Values)
                    _chatsList.GetRootParent().Children.Remove(item);
                ChatItems.Clear();
            }
        }
        public void AddChatItemToList(string avatarLink, string name, string lastMsgAvatar, string lastMsgText, string chatID, string userID)
        {
            ChatItems.Add(chatID, _chatsList.AddChatItem(avatarLink, name, lastMsgAvatar, lastMsgText, chatID, userID));
        }
    }
}
