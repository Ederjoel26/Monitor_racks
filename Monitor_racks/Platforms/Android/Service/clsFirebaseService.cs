using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_racks.Platforms.Android.Service
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class clsFirebaseService : FirebaseMessagingService
    {
        public clsFirebaseService() { }
        public override void OnNewToken(string sToken)
        {
            base.OnNewToken(sToken);
            if (Preferences.ContainsKey("DeviceToken"))
            {
                Preferences.Remove("DeviceToken");
            }
            Preferences.Set("DeviceToken", sToken);
        }

        public override void OnMessageReceived(RemoteMessage oMessage)
        {
            base.OnMessageReceived(oMessage);

            var notification = oMessage.GetNotification();

            SendNotification(notification.Body, notification.Title, oMessage.Data);
        }

        private void SendNotification(string messageBody, string title, IDictionary<string, string> data)
        {

            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);

            foreach (var key in data.Keys)
            {
                string value = data[key];
                intent.PutExtra(key, value);
            }

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.Channel_ID)
                .SetContentTitle(title)
                .SetContentText(messageBody)
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetChannelId(MainActivity.Channel_ID)
                .SetAutoCancel(true)
                .SetPriority((int)NotificationPriority.Max);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NotificationID, notificationBuilder.Build());
        }
    }
}
