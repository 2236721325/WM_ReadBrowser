using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.ReadBrowser.Messages
{
    public class GoToUrlMessage : ValueChangedMessage<string>
    {
        public GoToUrlMessage(string value) : base(value)
        {
        }
    }
}
