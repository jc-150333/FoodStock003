using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]

namespace FoodStock01.iOS
{
    public class NotificationService : INotificationService
    {
        public void Regist()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNAuthorizationOptions types = UNAuthorizationOptions.Badge |
                                                UNAuthorizationOptions.Sound |
                                                UNAuthorizationOptions.Alert;

                UNUserNotificationCenter.Current.RequestAuthorization(types, (granted, err) =>
                 {
                     if (err != null)
                     {
                         System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + System.Environment.NewLine + err.LocalizedDescription);
                     }
                     if (granted)
                     {

                     }
                 });
            }
        }

        public void On(string title,string subtitle,string body)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                var content = new UNMutableNotificationContent();
                content.Title = title;
                content.Subtitle = title;
                content.Body = body;
                content.Sound = UNNotificationSound.Defoult;

                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

                var requestID = "notifyKey";
                content.UserInfo = NSDictionary.FromObjectAndKey(new NSString("notifyValue"), new NSString("notifyKey"));
                var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                UNUserNotificationCenter.Current.Delegate = new LocalNotificationCenterDelegate();

                //ローカル通知を予約する
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
                 {
                     if (err != null)
                     {
                         LogUtility.OutPutError(err.LocalizedFailureReason + System.Environment.NewLine + err.LocalizedDescription);
                     }
                 });
                UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1; //アイコン上に表示するバッジの数値
            });
        }

        public void Off()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { "notifyKey" });
            });
        }
    }
}