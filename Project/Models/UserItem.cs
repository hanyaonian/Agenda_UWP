using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Project.Models
{
    public class UserItem
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public  long Root { get; set; }

        public UserItem(string username, string password, long root)
        {
            this.Username = username;
            this.Password = password;
            this.Root = root;
        }
    }
}