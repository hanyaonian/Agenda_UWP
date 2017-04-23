using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class AddTaskPage : Page
    {
        private string path;
        private string currentUserName;
        public AddTaskPage()
        {
            this.InitializeComponent();
            //aaaaa
            this.ViewModel = new ViewModels.TaskItemViewModel();

            this.UserModels = new ViewModels.UserViewModel();
            //get all user in user model
            this.getUserModel();
            FileInfo pfile = new FileInfo("Project.exe");
            this.path = pfile.DirectoryName + "\\Assets\\5.jpg";
            this.currentUserName = App.login_user.Username;

        }

        ViewModels.TaskItemViewModel ViewModel { get; set; }
        ViewModels.UserViewModel UserModels { get; set; }
        ObservableCollection<Models.UserItem> selectedUser = new ObservableCollection<Models.UserItem>();
        ObservableCollection<Models.UserItem> UserModel = new ObservableCollection<Models.UserItem>();

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string imgPath;
            imgPath = this.path;
            bool valid = checkValid();
            string participate = this.getParticipate();
            if (valid == true)
            {
                ViewModel.AddTaskItem(title.Text.ToString(), details.Text.ToString(), time.Date.DateTime, imgPath, currentUserName, "", participate);
            }
            Frame.Navigate(typeof(HomePage), "");
        }

        private bool checkValid()
        {
            if (title.Text.ToString() == "" || details.Text.ToString() == "")
            {
                var i = new MessageDialog("something empty").ShowAsync();
                return false;
            } else if (time.Date.DateTime < DateTime.Today) {
                var i = new MessageDialog("time wrong!").ShowAsync();
            }
            return true;
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();  //允许用户打开和选择文件的UI
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();  //storageFile:提供有关文件及其内容以及操作的方式
            if (file != null)
            {
                path = file.Path;
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap 
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                    await bitmapImage.SetSourceAsync(fileStream);
                    img.Source = bitmapImage;
                }
            }
            else
            {
                var i = new MessageDialog("error with picture").ShowAsync();
            }
        }

        private void UserListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserModels.SelectedUser = (Models.UserItem)(e.ClickedItem);
            selectedUser.Add(UserModels.SelectedUser);
            UserModel.Remove(UserModels.SelectedUser);
        }

        private void SelectedListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Models.UserItem temp = (Models.UserItem)(e.ClickedItem);
            selectedUser.Remove(temp);
            UserModel.Add(temp);
        }

        private void getUserModel()
        {
            foreach(Models.UserItem user in UserModels.AllUsers)
            {
                this.UserModel.Add(user);
            }
        }
        private string getParticipate()
        {
            string participate = "";
            foreach (Models.UserItem user in selectedUser)
            {
                participate = participate + user.Username + ",";
            }
            return participate;
        }
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Int64 t = (Int64)value;
            string root;
            if (t == 0) root = "Worker";
            else root = "Boss";
            return root;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
