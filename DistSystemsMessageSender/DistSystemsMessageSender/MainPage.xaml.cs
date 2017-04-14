using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
        string DEFAULT_MESSAGE = "Enter message here";
        Contact selectedContact;

        public MainPage()
        {
            this.InitializeComponent();

            initFields();
            
            //contactList.DisplayMemberPath = "name";
            //contactList.Val
            //contactList.Items.Add(new DistSystemsMessageSender.Contact("Michael", "8016087747"));
        }

        private void initFields()
        {
            contacts.Add(new DistSystemsMessageSender.Contact("Michael", "8016087747"));
            contacts.Add(new DistSystemsMessageSender.Contact("Tommy", "5033330685"));

            contacts.ElementAt(0).messages.Add(new Message("Michael", "Hey there, partner!", new DateTime(2017, 4, 13, 15, 53, 23)));
            contacts.ElementAt(0).messages.Add(new Message("Me", "Why hello. This looks great, Michael!", new DateTime(2017, 4, 13, 16, 10, 23)));

            //contactList.SelectedIndex = 1;
            //selectedContact = contacts.ElementAt(0);
        }

        private void contactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedContact = (Contact) contactList.SelectedItem;

            Debug.WriteLine("The selected contact is " + selectedContact.Name);
            contactName.Text = selectedContact.Name;

            //display that contact's messages
            contactMessages.ItemsSource = selectedContact.messages;
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message("Me", textBox.Text, DateTime.Now);
            textBox.Text = DEFAULT_MESSAGE;

            selectedContact = (Contact) contactList.SelectedItem;
            selectedContact.messages.Add(message);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Equals(DEFAULT_MESSAGE))
            {
                tb.Text = string.Empty;
            }
            
            tb.GotFocus -= textBox_GotFocus;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text.Equals(string.Empty))
            {
                tb.Text = DEFAULT_MESSAGE;
            }

            tb.GotFocus += textBox_GotFocus;
        }
    }
}
