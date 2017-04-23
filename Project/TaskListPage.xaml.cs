using System;
using System.Collections.Generic;
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
    public sealed partial class TaskListPage : Page
    {
        ViewModels.TaskItemViewModel ViewModel { get; set; }

        public TaskListPage()
        {
            this.InitializeComponent();
            this.init();
        }

        private void TaskListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (Models.TaskItem)(e.ClickedItem);
            Frame.Navigate(typeof(TaskContentPage), ViewModel);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (App.login_user.Root == 0) return;
            MenuFlyoutItem ClickItem = (MenuFlyoutItem)sender;
            ViewModel.SelectedItem = (Models.TaskItem)ClickItem.DataContext;
            Frame.Navigate(typeof(TaskUpdatePage), ViewModel);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (App.login_user.Root == 0) return;
            MenuFlyoutItem ClickItem = (MenuFlyoutItem)sender;
            ViewModel.SelectedItem = (Models.TaskItem)ClickItem.DataContext;
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveTaskItem(ViewModel.SelectedItem.id);
            }
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem ClickItem = (MenuFlyoutItem)sender;
            ViewModel.SelectedItem = (Models.TaskItem)ClickItem.DataContext;
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            DataTransferManager.ShowShareUI();
        }

        private async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var dp = args.Request.Data;
            var deferral = args.Request.GetDeferral();
            dp.Properties.Title = ViewModel.SelectedItem.title;
            string content = "来自" + ViewModel.SelectedItem.username + "发布的任务:\n    " + ViewModel.SelectedItem.description;
            dp.Properties.Description = content;
            dp.SetText(content);

            if (ViewModel.SelectedItem.filepath != "")
            {
                StorageFile photoFile;
                photoFile = await StorageFile.GetFileFromPathAsync(ViewModel.SelectedItem.filepath);
                dp.SetStorageItems(new List<StorageFile> { photoFile });
            }
            deferral.Complete();
        }
        private void init()
        {
            ViewModel = new ViewModels.TaskItemViewModel();
        }
    }
}
