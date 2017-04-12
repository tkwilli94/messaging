using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TextingApp
{
    class Contact
    {
        public string name { get; set; }
        public string phNum { get; set; }

        public Contact(string name, string phNum)
        {
            this.name = name;
            this.phNum = phNum;
        }

        
        override public string ToString()
        {
            return "{\"Name\":\"" + name + "\",\"Phone\":\"" + phNum + "\"}";
        }
    }
}