using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using Android.Telephony;
using Android.Provider;

namespace TextingApp
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var notification = message.GetNotification();
           // Log.Debug(TAG, "Notification Message Title: " + notification.Title);
           // Log.Debug(TAG, "Notification Message Body: " + notification.Body);
            foreach (var pair in message.Data)
            {
                var phNum = pair.Key;
                var text = pair.Value;
                Log.Debug(TAG, "PhNum: {0}-> Message: {1}", phNum, text);
                SmsManager.Default.SendTextMessage(phNum, null, text, null, null);
            }
        }
    }
}