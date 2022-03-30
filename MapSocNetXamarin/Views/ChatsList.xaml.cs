using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MapSocNetXamarin.ViewModels;

namespace MapSocNetXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatsList : ContentPage
	{
		private ChatsPageFiller _filler;
		private PageLozalizator _localizator;
		public ChatsList()
		{
			InitializeComponent ();
			_filler = new ChatsPageFiller(this);
			_localizator = new PageLozalizator(this);
		}
		public ChatItem AddChatItem(string avatarLink, string name, string lastMsgAvatar, string lastMsgText, string chatID, string userID)
        {
			ChatItem ci = new ChatItem(avatarLink, name, lastMsgAvatar, lastMsgText, chatID, userID);
			RootParent.Children.Add(ci);
			return ci;
        }
		public Layout<View> GetRootParent()
        {
			return RootParent;
        }

        private void ContentPage_LayoutChanged(object sender, EventArgs e)
        {
			_filler.Update();
		}
    }
}