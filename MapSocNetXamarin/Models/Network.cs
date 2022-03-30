using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace MapSocNetXamarin.Networking
{
    public static class Network
    {
        private static Thread _thread;
        private static Dictionary<string, string> _urlPostRequests;
        private static Dictionary<string, string> _urlPostRequestsAnswers;
        private static List<string> _urls;
        public static async Task<string> SendRequest(string data, string url)
        {
            System.Diagnostics.Debug.WriteLine("Start Sending Request");
            if (_urls == null)
            {
                _urls = new List<string>();
            }

            if (_urlPostRequests == null)
            {
                _urlPostRequests = new Dictionary<string, string>();
            }

            if (_urlPostRequestsAnswers == null)
            {
                _urlPostRequestsAnswers = new Dictionary<string, string>();
            }

            if (_thread == null)
            {
                ThreadStart start = new ThreadStart(Main);
                _thread = new Thread(start);
                _thread.Start();
            }

            while (_urls.Contains(url))
            {
                await Task.Yield();
            }

            while (_urls.Contains(url))
                await Task.Yield();

            _urls.Add(url);

            while (_urlPostRequests.ContainsKey(url))
                await Task.Yield();

            _urlPostRequests.Add(url, data);

            while (!_urlPostRequestsAnswers.ContainsKey(url))
            {
                await Task.Yield();
            }

            string ans = _urlPostRequestsAnswers[url];

            _urlPostRequests.Remove(url);
            _urlPostRequestsAnswers.Remove(url);

            System.Diagnostics.Debug.WriteLine(string.Format("NETWORK RESULT:\nREQ:\n{0}\nURL:\n{1}\n:RESULT:\n{2}", data, url, ans));

            return ans;
        }

        public static async Task<string> UploadData(string req, byte[] data, string url)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage msg = new HttpRequestMessage();

            msg.Method = HttpMethod.Post;
            msg.RequestUri = new Uri(url);

            MultipartFormDataContent content = new MultipartFormDataContent("12345");
            StringContent sc = new StringContent(req);

            StreamContent bac = new StreamContent(new MemoryStream(data));
            content.Add(sc, "gr");
            content.Add(bac, "file", "file");
            //foreach (string key in info.Keys)
            //{
            //    content.Add(new StringContent(info[key]), key);
            //}
            msg.Content = content;
            string ans = "";

            Thread th = new Thread(async () =>
            {
                HttpResponseMessage hrm = await client.SendAsync(msg);
                ans = await hrm.Content.ReadAsStringAsync();
                Thread.CurrentThread.Abort();
            });
            th.Start();

            while (th.IsAlive || ans == "")
                await Task.Yield();

            System.Diagnostics.Debug.WriteLine(string.Format("NETWORK RESULT:\nREQ:\n{0}\nURL:\n{1}\n:RESULT:\n{2}", req, url, ans));

            return ans;
        }
        public static async Task<byte[]> DownloadData(string url, string req)
        {
            HttpClient client = new HttpClient();

            StringContent sc = new StringContent(req, Encoding.UTF8, "application/json");

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Post;
            requestMessage.RequestUri = new Uri(url);
            requestMessage.Content = sc;

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            byte[] data = await response.Content.ReadAsByteArrayAsync();
            System.Diagnostics.Debug.WriteLine("Loaded avatar length\n" + data.Length + '\n');
            

            return data;
        }
        private async static void Main()
        {
            HttpClient client = new HttpClient();

            System.Diagnostics.Debug.WriteLine("Thread started");

            while (true)
            {
                if (_urls.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Start Sending Http Request");
                    HttpRequestMessage msg = new HttpRequestMessage();
                    msg.Method = HttpMethod.Post;
                    msg.RequestUri = new Uri(_urls[0]);
                    msg.Content = new StringContent(_urlPostRequests[_urls[0]], Encoding.UTF8, "application/json");

                    HttpResponseMessage ans = await client.SendAsync(msg);

                    _urlPostRequestsAnswers.Add(_urls[0], await ans.Content.ReadAsStringAsync());

                    System.Diagnostics.Debug.WriteLine("Complited");

                    _urls.RemoveAt(0);

                    msg = null;
                    ans = null;
                }
            }
        }
        public static class URLs
        {
            public static string mainDomain = "https://app.bumber.ru/api/";
            #region Auth
            public static string registration = mainDomain + "auth/reg";
            public static string login = mainDomain + "auth/log";
            public static string loginWithToken = mainDomain + "auth/toklog";
            public static string genPrc = mainDomain + "auth/genprc";
            public static string aplPrc = mainDomain + "auth/aplprc";
            public static string changePass = mainDomain + "auth/changepass";
            public static string changePassByCode = mainDomain + "auth/changepassbycode";
            #endregion
            #region User
            public static string loadDescribtion = mainDomain + "userinfo/lddesc";
            public static string updateDescription = mainDomain + "userinfo/upddesc";
            public static string updateShortDescription = mainDomain + "userinfo/updsdesc";
            public static string updateStatus = mainDomain + "userinfo/updst";
            public static string loadImage = mainDomain + "userinfo/getimg";
            public static string uploadImage = mainDomain + "userinfo/uplimg";

            public static string addFriend = mainDomain + "friends/addfr";
            public static string deleteFriend = mainDomain + "friends/delfr";
            public static string loadFriendsList = mainDomain + "friends/getfrls";
            public static string findFriend = mainDomain + "friends/findfr";
            #endregion
            public static string describtion = mainDomain + "uploadDescribtion.php";
            public static string loadGalery = mainDomain + "loadUserGalery.php";
            public static string uploadGaleryImage = mainDomain + "addImageToGalery.php";
            public static string changePassword = mainDomain + "changePassword.php";
            public static string loadChats = mainDomain + "loadChats.php";
            public static string loadChat = mainDomain + "loadChat.php";
            public static string uploadChat = mainDomain + "uploadChat.php";
            public static string sendMessage = mainDomain + "sendmsg.php";
        }
    }
}
