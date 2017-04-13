using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DistSystemsMessageSender
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Contact> contacts = new List<Contact>();

        public MainPage()
        {
            this.InitializeComponent();


            contacts.Add(new DistSystemsMessageSender.Contact("Michael", "8016087747"));
            contacts.Add(new DistSystemsMessageSender.Contact("Tommy", "5033330685"));

            contacts.ElementAt(0).messages.Add(new Message("Michael", "Hey there, partner!", new DateTime(2017, 4, 13, 15, 53, 23)));
            //contactList.DisplayMemberPath = "name";
            //contactList.Val
            //contactList.Items.Add(new DistSystemsMessageSender.Contact("Michael", "8016087747"));
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }

        private void contactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactList.SelectedItem;

            Debug.WriteLine("The selected contact is " + selectedContact.Name);
            contactName.Text = selectedContact.Name;

            //display that contact's messages
            contactMessages.ItemsSource = selectedContact.messages;
        }
    }
}
