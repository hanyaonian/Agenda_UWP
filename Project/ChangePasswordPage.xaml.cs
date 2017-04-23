using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Project
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ChangePasswordPage : Page
    {
        public ChangePasswordPage()
        {
            this.InitializeComponent();
        }

        private void Change_B_Click(object sender, RoutedEventArgs e)
        {
            if (checkValid())
            {
                var db = App.conn;
                string Username = App.login_user.Username;
                string Password = Input_Password.Password;
                string sql_u = @"UPDATE  User SET Password = ? WHERE Username = ?";
                try
                {
                    using (var insertment = db.Prepare(sql_u))
                    {
                        insertment.Bind(1, Password);
                        insertment.Bind(2, Username);
                        insertment.Step();
                    }
                    var i = new Windows.UI.Popups.MessageDialog("successfully change!").ShowAsync();
                    Frame.Navigate(typeof(HomePage), "");
                }
                catch (Exception ex)
                {
                    var i = new Windows.UI.Popups.MessageDialog("Change Defeat!").ShowAsync();
                    throw (ex);
                }
            }
            else
            {
                return;
            }
        }

        private bool checkValid()
        {
            bool ans = true;
            if (Input_Password_B.Password.ToString() != App.login_user.Password.ToString())
            {
                var i = new MessageDialog("Current password not right").ShowAsync();
                ans = false;
                return ans;
            }
            if (ans == true && Input_Password.Password == Input_Password_A.Password &&
                Input_Password.Password.Length >= 4 && Input_Password.Password.Length < 20)
            {
                //do nothing 
            }
            else if (Input_Password.Password != Input_Password_A.Password)
            {
                var i = new MessageDialog("Entered passwords differ!").ShowAsync();
                ans = false; ;
            }
            else
            {
                var i = new MessageDialog("length error").ShowAsync();
                ans = false;
            }
            return ans;
        }

        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage), "");
        }
    }
}
