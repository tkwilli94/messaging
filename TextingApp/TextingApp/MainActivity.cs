using Android.App;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Android.Provider;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using System.Threading;

namespace TextingApp
{
    [Activity(Label = "TextingApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string TAG = "MainActivity";
        TextView msgText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }
            else
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    var instanceId = FirebaseInstanceId.Instance;
                    instanceId.DeleteInstanceId();
                    Android.Util.Log.Debug("TAG", "{0} {1}", instanceId?.Token?.ToString(), instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
                });
            }
            IsPlayServicesAvailable();

            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };

            var subscribeButton = FindViewById<Button>(Resource.Id.subscribeButton);
            subscribeButton.Click += delegate {
                FirebaseMessaging.Instance.SubscribeToTopic("news");
                Log.Debug(TAG, "Subscribed to remote notifications");
            };

            /*
            var uri = ContactsContract.CommonDataKinds.Callable.ContentUri;
            
            string[] projection =
            {
                ContactsContract.CommonDataKinds.Callable.InterfaceConsts.Data4, //This is Normalized Phone Number. Regular Phone Number is data1
                ContactsContract.CommonDataKinds.Callable.InterfaceConsts.DisplayName,
                ContactsContract.CommonDataKinds.Callable.InterfaceConsts.Id
            };
           
            var cursor = ApplicationContext.ContentResolver.Query(uri, projection, null, null, null);
            var contactList = new List<Contact>();
            var contactList2 = new List<string>();
            if (cursor.MoveToFirst())
            {
                do
                {
                    int phoneContactID = cursor.GetInt(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id));

                    string contactNumber = cursor.GetString(cursor.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));
                    string contactName = cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));

                    contactList.Add(new Contact(contactName, contactNumber));
                    contactList2.Add(contactName);
                    
                } while (cursor.MoveToNext());
            }
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ContactItemView, contactList2);
            SmsManager.Default.SendTextMessage("5033330685", null, "HI MYSELF! My App just sent this to me :D", null, null);
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);

            //var request = WebRequest.Create("http://52.90.205.225:3001/init");
            var request = WebRequest.Create("http://testwebapplication420170403092835.azurewebsites.net/api/products");
            ASCIIEncoding encoding = new ASCIIEncoding();
            string requestData = "{\"Contacts\":[";
            int i = 0;
            foreach (Contact c in contactList)
            {
                requestData += c.ToString();
                if (++i != contactList.Count) requestData += ",";
            }
            
            requestData = JsonConvert.SerializeObject(contactList);
            byte[] data = encoding.GetBytes(requestData);

            request.Method = "PUT";
            request.ContentType = "application/json"; //eventually
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        // Do Nothing. Everything worked.
                        // If I need to check the HTTP Response, I'll do it here          
                    }
            }
            */
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available";
                return true;
            }
        }
    }
}

