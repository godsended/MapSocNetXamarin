using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using MapSocNetXamarin.ViewModels;
using MapSocNetXamarin.Models;

namespace MapSocNetXamarin.Helpers
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(Message));
            this.outgoingDataTemplate = new DataTemplate(typeof(Message));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as TextMessage;
            if (messageVm == null)
                return null;


            return (messageVm.IsMyMessage) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}
