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
    public sealed partial class TaskContentPage : Page
    {
        public TaskContentPage()
        {
            this.InitializeComponent();
        }

        private ViewModels.TaskItemViewModel ViewModel { get; set; }
        private ViewModels.CommentViewModel Comment { get; set; } = new ViewModels.CommentViewModel();

        // 获取评论
        private void intitComments()
        {
            if (ViewModel.SelectedItem.comments == "") return;

            var array_Comment = ViewModel.SelectedItem.comments.Split(',');
            for (int i = 0; i < array_Comment.Count()-1; )
            {
                Comment.addList(new Models.CommentItem(array_Comment[i], array_Comment[i + 1], array_Comment[i + 2]));
                i = i + 3;
            }
        }

        // 获取选择的Item
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(ViewModels.TaskItemViewModel))
            {
                this.ViewModel = (ViewModels.TaskItemViewModel)(e.Parameter);
                detail.Text = ViewModel.SelectedItem.description;
                title.Text = ViewModel.SelectedItem.title;
                creator.Text = "Created by " + ViewModel.SelectedItem.username;
                img.Source = ViewModel.SelectedItem.source;
                time.Text = ViewModel.SelectedItem.datetime.ToString("yyyy-MM-yy");
                this.intitComments();
            }
        }

        // 提交评论并更新数据库
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Models.TaskItem temp = ViewModel.SelectedItem;
            Comment.AllItems.Add(new Models.CommentItem(write.Text.ToString(), DateTime.Now.ToString(), App.login_user.Username));
            string speak = write.Text.ToString() + "," + DateTime.Now.ToString() + "," + App.login_user.Username + ",";
            ViewModel.SelectedItem.comments = speak + ViewModel.SelectedItem.comments;
            ViewModel.updatetaskitem(ViewModel.SelectedItem.id, title.Text, detail.Text, ViewModel.SelectedItem.datetime, ViewModel.SelectedItem.source, ViewModel.SelectedItem.filepath, ViewModel.SelectedItem.username, ViewModel.SelectedItem.comments.ToString(), "");
            ViewModel.SelectedItem = temp;
        }
    }
}
