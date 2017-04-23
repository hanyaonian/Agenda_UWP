using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class ChatWindowViewModel
    {
        public List<Models.Message> MessageList { get; set; }

        public ChatWindowViewModel()
        {
            MessageList = new List<Models.Message>();
        }

        public void addMessage(string name, DateTime pub, string comment, bool itself)
        {
            MessageList.Add(new Models.Message(name, pub, comment, itself));
        }
    }
}
