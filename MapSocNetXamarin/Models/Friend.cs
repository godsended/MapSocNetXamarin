using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MapSocNetXamarin.Networking;

namespace MapSocNetXamarin.Models
{
    public class Friend
    {
        public int id;

        public static async void SendRequest(string id)
        {
            FriendRequest req = new FriendRequest();
            req.MyId = Auth.id;
            req.Id = id;
            req.Token = Auth.token;

            string json = JsonConvert.SerializeObject(req);
            await Network.SendRequest(json, Network.URLs.addFriend);
        }
        public static async void Delete(string id)
        {
            FriendRequest req = new FriendRequest();
            req.MyId = Auth.id;
            req.Id = id;
            req.Token = Auth.token;

            string json = JsonConvert.SerializeObject(req);
            await Network.SendRequest(json, Network.URLs.deleteFriend);
        }
        public static async Task<FriendResponse> UpdateFriendsList()
        {
            FriendRequest req = new FriendRequest();
            req.MyId = Auth.id;
            req.Token = Auth.token;

            string json = JsonConvert.SerializeObject(req);

            string ans = await Network.SendRequest(json, Network.URLs.loadFriendsList);
            return JsonConvert.DeserializeObject<FriendResponse>(ans);
        }

        public static async Task<FriendInfo> Find(string code)
        {
            FriendRequest req = new FriendRequest();
            req.MyId = Auth.id;
            req.Token = Auth.token;
            req.Code = code;

            string json = JsonConvert.SerializeObject(req);

            string ans = await Network.SendRequest(json, Network.URLs.findFriend);
            return JsonConvert.DeserializeObject<FriendInfo>(ans);
        }
        public enum FriendType
        {
            Stranger,
            Sended,
            Recivied,
            Friend
        }

        public class FriendRequest
        {
            public string? MyId { get; set; }
            public string? Id { get; set; }
            public string? Token { get; set; }
            public string? Code { get; set; }
        }
        public struct FriendResponse
        {
            public int? Error;
            public FriendInfo[]? Friends;
        }
        public struct FriendInfo
        {
            public string? Id { get; set; }
            public string? AvatarId { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
            public string? Points { get; set; }
            public int? Error { get; set; }
        }
    }
}
