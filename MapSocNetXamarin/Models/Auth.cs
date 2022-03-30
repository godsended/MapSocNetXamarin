using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MapSocNetXamarin.Networking;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MapSocNetXamarin.Models
{
    public static class Auth
    {
        private static string _id, _token, _findToken, _mail;
        private static bool _isRestoringPass;
        public static string id
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                {
                    if (App.Current.Properties.TryGetValue("Auth_id", out object obj))
                    {
                        _id = (string)obj;
                        return _id;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return _id;
                }
            }
            set
            {
                _id = value;
                App.Current.Properties["Auth_id"] = value;
                Application.Current.SavePropertiesAsync();
            }
        }
        public static string token
        {
            get
            {
                if (string.IsNullOrEmpty(_token))
                {
                    if (App.Current.Properties.TryGetValue("Auth_token", out object obj))
                    {
                        _token = (string)obj;
                        return _token;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return _token;
                }
            }
            set
            {
                _token = value;
                App.Current.Properties["Auth_token"] = value;
                Application.Current.SavePropertiesAsync();
            }
        }
        public static string findToken
        {
            get
            {
                if (string.IsNullOrEmpty(_findToken))
                {
                    if (App.Current.Properties.TryGetValue("Auth_findToken", out object obj))
                    {
                        _findToken = (string)obj;
                        return _findToken;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return _findToken;
                }
            }
            set
            {
                _findToken = value;
                App.Current.Properties["Auth_findToken"] = value;
                Application.Current.SavePropertiesAsync();
            }
        }
        public static string mail
        {
            get
            {
                if (string.IsNullOrEmpty(_mail))
                {
                    if (App.Current.Properties.TryGetValue("Auth_mail", out object obj))
                    {
                        _mail = (string)obj;
                        return _mail;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return _mail;
                }
            }
            set
            {
                _findToken = value;
                App.Current.Properties["Auth_mail"] = value;
                Application.Current.SavePropertiesAsync();
            }
        }
        public static bool isRestoringPass
        {
            get
            {
                if (App.Current.Properties.TryGetValue("Auth_isRestoringPass", out object obj))
                {
                    _isRestoringPass = (bool)obj;
                    return _isRestoringPass;
                }
                else return false;
            }
            set
            {
                _isRestoringPass = value;
                App.Current.Properties["Auth_isRestoringPass"] = value;
                Application.Current.SavePropertiesAsync();
            }
        }
        public static async Task<AuthResponse> SignUp(string fName, string sName, string mail, string pass, string gender)
        {
            AuthResponse response;
            if (fName == null || sName == null || mail == null || pass == null || gender == null)
            {
                response = new AuthResponse();
                response.Error = 1;
                return response;
            }

            AuthRequest req = new AuthRequest();
            req.Mail = mail;
            req.Pass = Security.Encrypt(pass);
            req.FirstName = fName;
            req.SecondName = sName;
            req.Gender = gender;

            string json = JsonConvert.SerializeObject(req);

            response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.registration));
            if (response != null)
                System.Diagnostics.Debug.WriteLine(response);
            return response;
        }
        public static async Task<AuthResponse> SignIn(string mail, string pass)
        {
            AuthResponse response;
            if (mail == null || pass == null)
            {
                response = new AuthResponse();
                response.Error = 1;
                return response;
            }

            AuthRequest req = new AuthRequest();
            req.Mail = mail;
            req.Pass = Security.Encrypt(pass);

            string json = JsonConvert.SerializeObject(req);

            response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.login));
            if (response != null)
                System.Diagnostics.Debug.WriteLine(response);
            return response;
        }

        public static async Task<AuthResponse> SignInWithToken(string id, string token)
        {
            AuthResponse response;
            if (id == null || token == null)
            {
                response = new AuthResponse();
                response.Error = 1;
                return response;
            }

            AuthRequest req = new AuthRequest();
            req.Id = id;
            req.Token = token;

            string json = JsonConvert.SerializeObject(req);

            response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.loginWithToken));
            if (response != null)
                System.Diagnostics.Debug.WriteLine(response);
            return response;
        }
        public static async Task<bool> GeneratePassRestoreCode(string mail)
        {
            isRestoringPass = true;
            if (mail == null)
                return false;

            AuthRequest req = new AuthRequest();
            req.Mail = mail;

            string json = JsonConvert.SerializeObject(req);

            AuthResponse response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.genPrc));

            return response.Error == null;
        }
        public static async Task<bool> ApplyPassRestoreCode(string code, string mail)
        {
            isRestoringPass = true;
            if (code == null || mail == null)
                return false;

            AuthRequest req = new AuthRequest();
            req.Mail = mail;
            req.Code = code;

            string json = JsonConvert.SerializeObject(req);

            AuthResponse response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.aplPrc));

            return response.Error == null;
        }
        public static async Task<bool> ChangePasswordWithCode(string code, string mail, string pass)
        {
            isRestoringPass = true;
            if (code == null || mail == null || pass == null)
                return false;

            AuthRequest req = new AuthRequest();
            req.Mail = mail;
            req.Code = code;
            req.NewPass = Security.Encrypt(pass);

            string json = JsonConvert.SerializeObject(req);

            AuthResponse response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.changePassByCode));

            if (response.Error == null)
            {
                isRestoringPass = false;
                return true;
            }
            else
                return false;
        }

        public static async Task<bool> ChangePassword(string oldPass, string newPass)
        {
            AuthRequest req = new AuthRequest();
            req.Id = id;
            req.Token = token;
            req.NewPass = Security.Encrypt(newPass);
            req.Pass = Security.Encrypt(oldPass);

            string json = JsonConvert.SerializeObject(req);

            AuthResponse response = JsonConvert.DeserializeObject<AuthResponse>(await Network.SendRequest(json, Network.URLs.changePass));

            return response.Error == null;
        }

        public static void ClearData()
        {
            App.Current.Properties.Clear();
        }

        public class AuthResponse
        {
            public string? Mail;
            public string? Pass;
            public string? Id;
            public string? FirstName;
            public string? LastName;
            public string? Code;
            public string? NewPass;
            public string? Token;
            public string? Gender;
            public string? FindToken;
            public int? Error;
        }
        public class AuthRequest
        {
            public string? Id;
            public string? Token;
            public string? NewPass;
            public string? Pass;
            public string? Mail;
            public string? Code;
            public string? FirstName;
            public string? SecondName;
            public string? Gender;
        }
    }
}
