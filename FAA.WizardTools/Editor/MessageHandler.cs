using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardTools.Editor
{
    public class MessageHandler
    {
        private Action<string> sendMessage;

        public MessageHandler(Action<string> messageHandler)
        {
            this.sendMessage = messageHandler;
        }

        public void SendMessage(string message)
        {
            sendMessage(message);
        }
    }
}
