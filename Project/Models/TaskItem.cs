using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace Project.Models
{
    public class TaskItem
    {
        public string id;

        public string title { get; set; }

        public string description { get; set; }

        public bool? completed { get; set; }

        public DateTime datetime { get; set; }

        public string filepath { get; set; }

        public BitmapSource source;

        public string username;

        public string comments;

        public string participatants;

        public TaskItem(string id, string title, string description, BitmapImage img, DateTime datetime, string filepath, string name, string _comment, string _participatants)
        {
            this.id = id; //生成id
            this.title = title;
            this.description = description;
            this.datetime = datetime;
            completed = false; //默认为未完成
            this.filepath = filepath;
            source = img;
            username = name;
            comments = _comment;
            participatants = _participatants;
        }
    }
}