using System;
using Foundation;
using UIKit;

using UserNotifications;

using Xamarin.Forms;

[assembly: Dependency(typeof(FoodStock01.NotificationService))]

namespace FoodStock01
{
    public class NotificationService : INotificationService
    {
        public void Regist()
        {
            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{
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
            //}
        }

        public void On(string title, string subtitle, string body)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                var content = new UNMutableNotificationContent();
                content.Title = title;
                content.Subtitle = subtitle;
                content.Body = body;
                content.Sound = UNNotificationSound.Default;

                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(15, false);

                //日時を指定する場合は以下の情報を付与
                NSDateComponents components = new NSDateComponents();
                components.TimeZone = NSTimeZone.DefaultTimeZone;
                components.Year = LocalDateTime.Year;
                components.Month = LocalDateTime.Month;
                components.Day = LocalDateTime.Day;
                //components.Hour = _notifyDate.LocalDateTime.Hour;
                components.Hour = 10;
                //components.Minute = _notifyDate.LocalDateTime.Minute;
                components.Minute = 0;
                components.Second = 0;
                var calendarTrigger = UNCalendarNotificationTrigger.CreateTrigger(components, false);

                var requestID = "notifyKey";
                content.UserInfo = NSDictionary.FromObjectAndKey(new NSString("notifyValue"), new NSString("notifyKey"));
                //var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                var request = UNNotificationRequest.FromIdentifier(requestID, content, calendarTrigger);

                //UNUserNotificationCenter.Current.Delegate = new LocalNotificationCenterDelegate();

                //ローカル通知を予約する
                UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
                 {
                     if (err != null)
                     {
                         //LogUtility.OutPutError(err.LocalizedFailureReason + System.Environment.NewLine + err.LocalizedDescription);
                     }
                 });
                UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1; //アイコン上に表示するバッジの数値
            });
        }

        public void Off()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                //UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { "notifyKey" });

                UNUserNotificationCenter.Current.RemoveAllDeliveredNotifications();
            });
        }

        public void return0()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            });
        }
    }
}