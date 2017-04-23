using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;
using System.Collections.ObjectModel;

namespace Project.ViewModels
{
    class CommentViewModel
    {
        private ObservableCollection<Models.CommentItem> allItems;
        public ObservableCollection<Models.CommentItem> AllItems { get { return this.allItems; } }

        public CommentViewModel()
        {
            allItems = new ObservableCollection<Models.CommentItem>();
        }

        public void addList(CommentItem comment)
        {
            allItems.Add(comment);
        }
    }
}
