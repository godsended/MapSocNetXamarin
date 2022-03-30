using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MapSocNetXamarin.Networking;

namespace MapSocNetXamarin.Models
{
    public class Galery
    {
        public static async Task<List<string>> LoadGaleryById(string userId)
        {
            List<string> res = new List<string>();

            Dictionary<string, string> req = new Dictionary<string, string>();
            req.Add("ID", userId);

            string ans = "";//await Network.SendRequest(req, Network.URLs.loadGalery);

            try
            {
                if (ans != "Error")
                {
                    string[] val = ans.Split('&');
                    foreach (string img in val)
                    {
                        res.Add(img.Split('|')[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return res;
        }
        public static async void UploadImage(byte[] img, bool isAvatar = false)
        {
            GaleryRequest req = new GaleryRequest();
            req.Id = Auth.id;
            req.Token = Auth.token;
            req.IsAvatar = isAvatar;

            string json = JsonConvert.SerializeObject(req);
            string url = Network.URLs.uploadImage;
        
            string ans = await Network.UploadData(json, img, url);

            System.Diagnostics.Debug.WriteLine("Upload image result: " + ans);
        }
        public static async Task<byte[]> LoadImage(string id, string imageId)
        {
            GaleryRequest req = new GaleryRequest();
            req.Id = id;
            req.ImageId = imageId;
            string json = JsonConvert.SerializeObject(req);
            return await Network.DownloadData(Network.URLs.loadImage, json);
        }
        public class GaleryResponse
        {
            public int? Error;
            public string[] Images;
        }
        public class GaleryRequest
        {
            public string Id;
            public string Token;
            public bool? IsAvatar;
            public string? ImageId;
        }
    }
}
