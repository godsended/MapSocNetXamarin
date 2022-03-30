using System;
using System.Collections.Generic;
using System.Text;

using MapSocNetXamarin.Models;
using MapSocNetXamarin.Views;
using MapSocNetXamarin.Networking;

namespace MapSocNetXamarin.ViewModels
{
    public class FriendsPageFiller
    {
        public Dictionary<string, FriendItem> FriendItems;
        public FriendsPage FriendsPage;
        public FriendsPageFiller(FriendsPage fp)
        {
            FriendsPage = fp;
            FriendItems = new Dictionary<string, FriendItem>();
            Settings.ThemeChanged += fp.ChangeSelectedStatusColor;
            Update();
        }
        public async void Update()
        {
            ClearFriendItems();

            var response = await Friend.UpdateFriendsList();

            if (response.Error != null)
                return;

            foreach (var info in response.Friends)
            {
                AddFriendToList(info.AvatarId, info.Name, (int)info.Status, info.Points, info.Id);
            }
        }
        public void ClearFriendItems()
        {
            if (FriendItems.Count > 0)
            {
                foreach (FriendItem item in FriendItems.Values)
                    FriendsPage.GetRootParent().Children.Remove(item);
                FriendItems.Clear();
            }
        }
        public async void SearchUser(string text)
        {
            if (text == "")
            {
                Update();
                return;
            }

            ClearFriendItems();


            Friend.FriendInfo friend = await Friend.Find(text);

            Debug.Log("\nFriend page filler: find request result recieved\n");
            if (friend.Error == null)
                AddFriendToList(friend.AvatarId, friend.Name, (int)friend.Status, friend.Points, friend.Id);
        }
        public void AddFriendToList(string avatarLink, string name, int statusID, string points, string id)
        {
            FriendItems.Add(id, FriendsPage.AddFriendToList(avatarLink, name, statusID, points, id));
        }
    }
}
