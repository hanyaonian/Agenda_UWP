using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    class CommentItem
    {
        public string comment { get; set; }

        public string datetime { get; set; }

        public string name { get; set; }

        public CommentItem(string _comment, string _datetime, string _name)
        {
            comment = _comment;
            datetime = _datetime;
            name = _name;
        }
    }
}
