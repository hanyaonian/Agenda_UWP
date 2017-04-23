using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    class UserViewModel
    {
        //selected
        private Models.UserItem selectedUser = default(Models.UserItem);
        public Models.UserItem SelectedUser { get { return selectedUser; } set { this.selectedUser = value; } }
        //all
        private ObservableCollection<Models.UserItem> allUsers = new ObservableCollection<Models.UserItem>();
        public ObservableCollection<Models.UserItem> AllUsers { get { return this.allUsers; } }
        //get all users
        public UserViewModel()
        {
            this.getAllUser();
        }
        
        private void getAllUser()
        {
            this.allUsers.Clear();
            try
            {
                var dp = App.conn;
                using (var statement = dp.Prepare(@"SELECT * FROM User"))
                {
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        this.allUsers.Add(new Models.UserItem((string)statement[0], (string)statement[1], (long)statement[2]));
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }
}
