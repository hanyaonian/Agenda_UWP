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
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
            Input_Password.Password = "密码";
            Input_Password.PasswordRevealMode = PasswordRevealMode.Visible;
            Input_Password_A.Password = "确认密码";
            Input_Password_A.PasswordRevealMode = PasswordRevealMode.Visible;
        }

        private void SignUp_B_Click(object sender, RoutedEventArgs e)
        {
            // 接收传入信息
            string username = Input_Username.Text;
            string password = Input_Password.Password;
            int root = (comboBox.SelectionBoxItem.ToString() == "Boss") ? 1 : 0;

            // 检查输入格式
            if (!Input_Check(username, password, Input_Password_A.Password)) return;

            var dp = App.conn;
            string sql_s = @"SELECT Username FROM User WHERE Username = ?";
            using (var statement = dp.Prepare(sql_s))
            {
                statement.Bind(1, username);
                if (SQLiteResult.ROW == statement.Step())
                {
                    if ((string)statement[0] != "")
                    {
                        var t = new MessageDialog("The Username has been signed up!").ShowAsync();
                        return;
                    }
                }
            }

            // 异常检查完后插入
            string sql_i = @"INSERT INTO User (Username, Password, Root) VALUES (?, ?, ?)";
            try
            {
                using (var insertment = dp.Prepare(sql_i))
                {
                    insertment.Bind(1, username);
                    insertment.Bind(2, password);
                    insertment.Bind(3, root);
                    insertment.Step();
                }
            }
            catch (Exception ex)
            {
                var i = new MessageDialog("Insert Defeat!").ShowAsync();
                throw (ex);
            }
            Frame.Navigate(typeof(LoginPage), "");
            var f = new MessageDialog("Sign Up successfully!").ShowAsync();
        }

        public bool Input_Check(string username, string password, string password_a)
        {
            Username_bug.Text = Password_bug.Text = Password_A_bug.Text = "";

            if (username == "") {
                Username_bug.Text = "The Username should not be empty!";
                return false;
            }
            if (username.Length > 20)
            {
                Username_bug.Text = "The Length of Username should not be longer than 20!";
                return false;
            }
            if (password == "")
            {
                Password_bug.Text = "The Password should not be empty!";
                return false;
            }
            if (password.Length > 20)
            {
                Password_bug.Text = "The Length of Password should not be longer than 20!";
                return false;
            }
            if (password.Length < 4)
            {
                Password_bug.Text = "The Length of Password should not be less than 4!";
                return false;
            }
            if (password_a != password)
            {
                Password_A_bug.Text = "Entered passwords differ!";
                return false;
            }
            return true;
        }

        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            Username_bug.Text = Password_bug.Text = Password_A_bug.Text = "";
            Frame.Navigate(typeof(LoginPage), "");
        }

        private void TextFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Username.Text == "用户名") Input_Username.Text = "";
        }

        private void TextLeave(object sender, RoutedEventArgs e)
        {
            if (Input_Username.Text == "") Input_Username.Text = "用户名";
        }

        private void Input_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Input_Password.PasswordRevealMode = PasswordRevealMode.Peek;
            if (Input_Password.Password == "密码")
            {
                Input_Password.Password = "";
            }
        }

        private void Input_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Password.Password == "")
            {
                Input_Password.PasswordRevealMode = PasswordRevealMode.Visible;
                Input_Password.Password = "密码";
            }
        }

        private void Input_Password_A_GotFocus(object sender, RoutedEventArgs e)
        {
            Input_Password_A.PasswordRevealMode = PasswordRevealMode.Peek;
            if (Input_Password_A.Password == "确认密码")
            {
                Input_Password_A.Password = "";
            }
        }

        private void Input_Password_A_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input_Password_A.Password == "")
            {
                Input_Password_A.PasswordRevealMode = PasswordRevealMode.Visible;
                Input_Password_A.Password = "确认密码";
            }
        }
    }
}
