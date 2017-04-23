using Project.Models;
using SQLitePCL;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            string username = Input_Username.Text;
            string password = Input_Password.Password;

            // 检查格式
            if (!Input_Check(username, password)) return;

            var dp = App.conn;
            string sql = @"SELECT Username, Password, Root FROM User WHERE Username = ?";
            using (var statement = dp.Prepare(sql))
            {
                statement.Bind(1, username);
                if (SQLiteResult.ROW == statement.Step())
                {
                    if ((string)statement[0] != "")
                    {
                        if (password == (string)statement[1])
                        {
                            UserItem user = new UserItem((string)statement[0], (string)statement[1], (long)statement[2]);
                            App.login_user = user;
                            Frame.Navigate(typeof(HomePage), user);
                            App.login = true;
                            return;
                        }
                    }
                }
                var t = new MessageDialog("Username or Password is not correct!").ShowAsync();
            }
}

        public bool Input_Check(string username, string password)
        {
            Username_bug.Text = "";
            if (username == "" || password == "")
            {
                Username_bug.Text = "用户名或密码不能为空！";
                return false;
            }
            return true;
        }

        private void SignUp_B_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage), "");
        }
    }
}
