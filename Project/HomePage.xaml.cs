using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class HomePage : Page
    {
        //get todo task
        ObservableCollection<Models.TaskItem> MyToDoTask = new ObservableCollection<Models.TaskItem>();

        ObservableCollection<Models.TaskItem> all = new ObservableCollection<Models.TaskItem>();
        //get all task
        ViewModels.TaskItemViewModel ViewModel { get; set; }

        public HomePage()
        {
            this.InitializeComponent();
            Welcome.Text = "Welcome! " + App.login_user.Username;
            ViewModel = new ViewModels.TaskItemViewModel();
            all = ViewModel.getNoPicAllItems();
            this.getTaskModel();
        }

        private void LoginOut_B_Click(object sender, RoutedEventArgs e)
        {
            App.login = false;
            App.login_user = null;
            Frame.Navigate(typeof(LoginPage), "Login Out Successfully!");
        }

        private void toDoList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Models.TaskItem temp = (Models.TaskItem)(e.ClickedItem);
            ViewModel.setSelectedItemById(temp.id);
            Frame.Navigate(typeof(TaskContentPage), ViewModel);
        }

        private void Password_change_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangePasswordPage), "");
        }

        //got patrticipate work
        private bool gotParticipate(string userName, string participants)
        {
            bool got = false;
            if (participants != null)
            {
                string[] users = participants.Split(',');
                for (int i = 0; i < users.Length; i++)
                {
                    if (users[i] == userName)
                        got = true;
                }
            }
            return got;
        }

        private void getTaskModel()
        {
            foreach(Models.TaskItem task in all)
            {
                if (gotParticipate(App.login_user.Username, task.participatants))
                MyToDoTask.Add(task);
            }
        }
    }

    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime t = (DateTime)value;
            return t.Date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
