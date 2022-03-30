using MapSocNetXamarin.Networking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapSocNetXamarin.Models
{
    public class Chat
    {
        internal static async Task<string> LoadChats()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ID", Auth.id);
            data.Add("Token", Auth.token);

            return "";//await Network.SendRequest(data, Network.URLs.loadChats);
        }
        internal static async Task<string> UploadChat(string userID)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MyID", Auth.id);
            data.Add("Token", Auth.token);
            data.Add("FriendID", userID);

            return "";//await Network.SendRequest(data, Network.URLs.uploadChat);
        }
        internal static async Task<string> InitChat(string chatID)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("MyID", Auth.id);
            data.Add("Token", Auth.token);
            data.Add("ChatID", chatID);

            return "";//await Network.SendRequest(data, Network.URLs.loadChat);
        }
        internal static async Task<string> SendMessage(string chatID, string message)
        {
            Dictionary<string, string> data = new Dictionary<string , string>();
            data.Add("MyID", Auth.id);
            data.Add("Token", Auth.token);
            data.Add("Text", message);
            data.Add("ChatID", chatID);

            return "";//await Network.SendRequest(data, Network.URLs.sendMessage);
        }
    }
}
