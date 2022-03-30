using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MapSocNetXamarin.Networking;
using Newtonsoft.Json;

namespace MapSocNetXamarin.Models
{
    class UserInfo
    {
        public static async Task<UserInfoResponse> LoadDescribtion(string id)
        {
            UserInfoRequest req = new UserInfoRequest();
            req.Id = id;
            req.MyId = Auth.id;

            string json = JsonConvert.SerializeObject(req);
            string sans = await Network.SendRequest(json, Network.URLs.loadDescribtion);
            Debug.Log("Try to response: " + sans);
            UserInfoResponse ans = JsonConvert.DeserializeObject<UserInfoResponse>(sans);

            Debug.Log("Succesfully responsed");
            return ans;
        }
        public static async void UpdateShortDescribtion(string text)
        {
            UserInfoRequest req = new UserInfoRequest();
            req.MyId = Auth.id;
            req.Token = Auth.token;
            req.Text = text;

            string json = JsonConvert.SerializeObject(req);
            string ans = await Network.SendRequest(json, Network.URLs.updateShortDescription);

            System.Diagnostics.Debug.WriteLine(ans);
        }
        public static async void UpdateLongDescribtion(string text)
        {
            UserInfoRequest req = new UserInfoRequest();
            req.MyId = Auth.id;
            req.Token = Auth.token;
            req.Text = text;

            string json = JsonConvert.SerializeObject(req);
            string ans = await Network.SendRequest(json, Network.URLs.updateDescription);

            System.Diagnostics.Debug.WriteLine(ans);
        }
        public static async void ChangeStatus(int status)
        {
            UserInfoRequest req = new UserInfoRequest();
            req.MyId = Auth.id;
            req.Token = Auth.token;
            req.Status = status;

            string json = JsonConvert.SerializeObject(req);
            string ans = await Network.SendRequest(json, Network.URLs.updateStatus);

            System.Diagnostics.Debug.WriteLine(ans);
        }
        public struct UserInfoResponse
        {
            public int? Error;
            public string? LongDescriptuon;
            public string? ShortDescription;
            public int? Status;
            public string? Name;
            public string? FriendType;
            public ImageResponse[] Images;
            public struct ImageResponse
            {
                public string? ImageId;
                public bool? IsAvatar;
            }
        }
        public class UserInfoRequest
        {
            public string? Id;
            public string? MyId;
            public string? Token;
            public string? Text;
            public int? Status;
        }

    }
}
