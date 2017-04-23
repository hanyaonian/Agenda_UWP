using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Message
    {
        public string Name { get; set; }

        public DateTime Published { get; set; }

        public string Comment { get; set; }

        public bool IsSelf { get; set; }

        public Message(string name, DateTime pub, string comment, bool itself)
        {
            Name = name;
            Published = pub;
            Comment = comment;
            IsSelf = itself;
        }
    }
}
