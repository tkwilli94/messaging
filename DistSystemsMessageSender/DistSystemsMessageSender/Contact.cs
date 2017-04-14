using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistSystemsMessageSender
{
    class Contact
    {
        public String Name { get; set; }
        public String phone { get; set; }
        public ObservableCollection<Message> messages { get; set; }

        public Contact(String name, String phone)
        {
            this.Name = name;
            this.phone = phone;
            messages = new ObservableCollection<Message>();
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
