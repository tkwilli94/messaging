using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistSystemsMessageSender
{
    class Contact
    {
        public String Name { get; set; }
        public String phnum { get; set; }
        public List<Message> messages { get; set; }

        public Contact(String name, String phnum)
        {
            this.Name = name;
            this.phnum = phnum;
            messages = new List<Message>();
        }

        //maybe a constructor that creates Contacts from a json string

        //public toJSON()
        //{
        //    StringBuilder json = new StringBuilder();

        //    json.Append("{ \"name\":")
        //        .Append(name)
        //        .Append(",\"phnum\":")
        //        .Append(phnum)
        //        .Append(",\"messages\":[)
        //}

        public String toString()
        {
            return Name;
        }
    }
}
