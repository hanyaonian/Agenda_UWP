using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Project
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TaskUpdatePage : Page
    {
        public TaskUpdatePage()
        {
            this.InitializeComponent();
        }

        private ViewModels.TaskItemViewModel ViewModel { get; set; }

        //get selected item
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(ViewModels.TaskItemViewModel))
            {
                this.ViewModel = (ViewModels.TaskItemViewModel)(e.Parameter);
                detail.Text = ViewModel.SelectedItem.description;
                title.Text = ViewModel.SelectedItem.title;
                creator.Text = ViewModel.SelectedItem.username + "  创建于 " + ViewModel.SelectedItem.datetime.ToString();
                img.Source = ViewModel.SelectedItem.source;
                date.Date = ViewModel.SelectedItem.datetime;
            }
        }

        private void Updata_Click(object sender, RoutedEventArgs e)
        {
            // 如果当前登录管理员与发布任务管理员不一致，则不能对任务进行修改
            if (App.login_user.Username != ViewModel.SelectedItem.username)
            {
                var i = new MessageDialog("You have no root to do this!").ShowAsync();
                return;
            }
            if (this.ViewModel != null)
            {
                if (!checkValid()) return;
                else
                {
                    var t = new MessageDialog("Update Successfully！").ShowAsync();
                    ViewModel.updatetaskitem(ViewModel.SelectedItem.id, title.Text, detail.Text, date.Date.DateTime, ViewModel.SelectedItem.source, ViewModel.SelectedItem.filepath, ViewModel.SelectedItem.username, ViewModel.SelectedItem.comments.ToString(), "");
                    Frame.Navigate(typeof(TaskListPage), "");
                }
            }
        }

        private bool checkValid()
        {
            string error = "";
            if (title.Text == "") error += "Title项不能为空！\n";
            if (detail.Text == "") error += "Detail项不能为空！\n";
            if (date.Date < DateTime.Now.Date) error += "您不能穿越！\n";

            if (error != "")
            {
                var i = new MessageDialog(error).ShowAsync();
                return false;
            }
            else return true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Models.TaskItem temp = ViewModel.SelectedItem;
            ViewModel.updatetaskitem(ViewModel.SelectedItem.id, title.Text, detail.Text, date.Date.DateTime, ViewModel.SelectedItem.source, ViewModel.SelectedItem.filepath, ViewModel.SelectedItem.username, ViewModel.SelectedItem.comments.ToString(), "");
            ViewModel.SelectedItem = temp;
            //after update selectedItem is set to null.re set it.
        }

        private void Delete_buttton_Click(object sender, RoutedEventArgs e)
        {
            if (App.login_user.Username != ViewModel.SelectedItem.username)
            {
                var i = new MessageDialog("You have no root to do this!").ShowAsync();
                return;
            }
            if (ViewModel != null)
            {
                ViewModel.RemoveTaskItem(ViewModel.SelectedItem.id);
                Frame.Navigate(typeof(TaskListPage), "");
            }
        }
    }
}
