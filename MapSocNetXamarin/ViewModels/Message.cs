using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
namespace MapSocNetXamarin.ViewModels
{
    internal class Message : ViewCell
    {
        internal DateTime sendingTime;
        internal DeliverStatus deliverStatus;
        internal enum DeliverStatus
        {
            Sending,
            Sended,
            Delivered,
            Readed
        }
    }
}
