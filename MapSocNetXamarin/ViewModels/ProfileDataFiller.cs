using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MapSocNetXamarin.Views;
using MapSocNetXamarin.Models;
using System.IO;
using System.Threading.Tasks;

namespace MapSocNetXamarin.ViewModels
{
    public class ProfileDataFiller
    {
        private ProfilePage _profilePage;
        private UserPage _userPage;
        private string _shortDescribtionString, _longDescribtionString, _name;
        private string _status;
        private IDeviceDataLoader dataLoader;
        private Friend.FriendType friendType;
        public string ShortDescribtionText
        {
            get
            {
                return _shortDescribtionString;
            }
            set
            {
                _shortDescribtionString = value;
                if (_profilePage != null)
                {
                    _profilePage.SetShortDescribtion(value);
                }
                else
                {
                    _userPage.SetShortDescribtion(value);
                }
            }
        }
        public string LongDescribtionText
        {
            get
            {
                return _longDescribtionString;
            }
            set
            {
                _longDescribtionString = value;
                if (_profilePage != null)
                    _profilePage.SetLongDescribtion(value);
                else
                    _userPage.SetLongDescribtion(value);
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (_profilePage != null)
                    _profilePage.SetName(value);
                else
                    _userPage.SetName(value);
            }
        }
        public string Status;
        public ProfileDataFiller(ProfilePage pp)
        {
            _profilePage = pp;
        }
        public ProfileDataFiller(UserPage pp)
        {
            _userPage = pp;
        }
        private void LoadAvatar(byte[] data)
        {
            Stream s = new MemoryStream(data);

            ImageSource source = ImageSource.FromStream(() => s);
            if (_profilePage != null)
            {
                _profilePage.GetAvatar().Source = source;
            }
            else
            {
                _userPage.GetAvatar().Source = source;
            }
        }
        public void LoadImages()
        {

        }
        public async Task<bool> UploadAvatar()
        {
            Stream stream = await DependencyService.Get<IDeviceDataLoader>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] data;

                using (BinaryReader br = new BinaryReader(stream))
                {
                    data = br.ReadBytes((int)stream.Length);
                    _profilePage.GetAvatar().Source = ImageSource.FromStream(() => new MemoryStream(data));
                }
                System.Diagnostics.Debug.WriteLine("Avatar size: " + data.Length);
                Galery.UploadImage(data, true);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UploadImage()
        {
            Stream stream = await DependencyService.Get<IDeviceDataLoader>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] data;

                using (BinaryReader br = new BinaryReader(stream))
                {
                    data = br.ReadBytes((int)stream.Length);
                    _profilePage.AddGaleryImageByStream(data);
                }
                System.Diagnostics.Debug.WriteLine("Image size: " + data.Length);
                Galery.UploadImage(data, false);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ShortDescribtionChanged(TextChangedEventArgs e)
        {
            if (e.OldTextValue == _shortDescribtionString)
                return;
            _shortDescribtionString = e.NewTextValue;

            System.Diagnostics.Debug.WriteLine("Uploading short descr");

            UserInfo.UpdateShortDescribtion(_shortDescribtionString);
        }
        public void LongDescribtionChanged(TextChangedEventArgs e)
        {
            if (e.OldTextValue == _longDescribtionString)
                return;
            _longDescribtionString = e.NewTextValue;

            System.Diagnostics.Debug.WriteLine("Uploading long descr");

            UserInfo.UpdateLongDescribtion(_longDescribtionString);
        }
        public void ChangeStatus(int i)
        {
            UserInfo.ChangeStatus(i);
        }
        public async void LoadDescribtion(string id)
        {
            try
            {
                UserInfo.UserInfoResponse ans = await UserInfo.LoadDescribtion(id);
                System.Diagnostics.Debug.WriteLine("Load desc of id: " + id + " result: " + ans);
                if (ans.Error != null)
                {
                    if (_profilePage != null)
                        _profilePage.OnLoadError();
                    else
                        _userPage.OnLoadError();
                }
                else
                {

                    ShortDescribtionText = ans.ShortDescription;
                    LongDescribtionText = ans.LongDescriptuon;
                    Status = ans.Status.ToString();
                    Name = ans.Name;
                    switch (ans.FriendType)
                    {
                        case "Y":
                            friendType = Friend.FriendType.Friend;
                            break;
                        case "S":
                            friendType = Friend.FriendType.Sended;
                            break;
                        case "MS":
                            friendType = Friend.FriendType.Recivied;
                            break;
                        case "N":
                            friendType = Friend.FriendType.Stranger;
                            break;
                        default:

                            break;

                    }

                    if (_userPage != null)
                        _userPage.SetupFriendActionButton(friendType);
                    if (_profilePage != null)
                        _profilePage.SetLocaleStatus(int.Parse(Status));
                    else
                        _userPage.SetLocaleStatus(int.Parse(Status));

                    if (ans.Images != null)
                    {
                        foreach (var img in ans.Images)
                        {
                            byte[] data = await Galery.LoadImage(id, img.ImageId);
                            if (img.IsAvatar == true)
                            {
                                LoadAvatar(data);
                            }
                            if (_profilePage != null)
                            {
                                _profilePage.AddGaleryImageByStream(data);
                            }
                            else
                            {
                                _userPage.AddGaleryImageByStream(data);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR" + e.Message);

                if (_profilePage != null)
                    _profilePage.OnLoadError();
                else
                    _userPage.OnLoadError();
            }
        }

        public async void OnAddFriendButtonClicked()
        {
            switch (friendType)
            {
                case Friend.FriendType.Friend:
                    if (await _userPage.SetupAndShowFriendActAlert(friendType))
                    {
                        _userPage.GetAddFriendButton().IsEnabled = false;
                        Friend.Delete(_userPage.id);
                        _userPage.GetAddFriendButton().IsEnabled = true;
                        friendType = Friend.FriendType.Stranger;
                        _userPage.SetupFriendActionButton(friendType);
                    }
                    break;
                case Friend.FriendType.Recivied:
                    if (await _userPage.SetupAndShowFriendActAlert(friendType))
                    {
                        _userPage.GetAddFriendButton().IsEnabled = false;
                        Friend.SendRequest(_userPage.id);
                        _userPage.GetAddFriendButton().IsEnabled = true;
                        friendType = Friend.FriendType.Friend;
                        _userPage.SetupFriendActionButton(friendType);
                    }
                    break;
                case Friend.FriendType.Sended:
                    if (await _userPage.SetupAndShowFriendActAlert(friendType))
                    {
                        _userPage.GetAddFriendButton().IsEnabled = false;
                        Friend.Delete(_userPage.id);
                        _userPage.GetAddFriendButton().IsEnabled = true;
                        friendType = Friend.FriendType.Stranger;
                        _userPage.SetupFriendActionButton(friendType);
                    }
                    break;
                case Friend.FriendType.Stranger:
                    _userPage.GetAddFriendButton().IsEnabled = false;
                    Friend.SendRequest(_userPage.id);
                    _userPage.GetAddFriendButton().IsEnabled = true;
                    friendType = Friend.FriendType.Sended;
                    _userPage.SetupFriendActionButton(friendType);
                    break;
            }
        }
        public async void UploadChat(string userID)
        {
            if (userID != Auth.id)
            {
                await Chat.UploadChat(userID);
            }
        }
    }
}
