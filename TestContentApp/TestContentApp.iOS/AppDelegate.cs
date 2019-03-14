using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Plugin.AzurePushNotification;
using Plugin.AzurePushNotification.Abstractions;
using UIKit;
using UserNotifications;

namespace TestContentApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            AzurePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }
        [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            AzurePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            RegisterNotifications();
            AzurePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Badge;

            return base.FinishedLaunching(app, options);
        }
        private void RegisterNotifications()
        {
            AzurePushNotificationManager.Initialize(
                "Endpoint=...",
                "...", null
                , new NotificationUserCategory[]
                {
                    new NotificationUserCategory("critical", new List<NotificationUserAction>())
                }
                , true);

            string[] tags = new string[1];
            tags[0] = "testapp";

            Console.WriteLine(tags[0]);

            CrossAzurePushNotification.Current.RegisterAsync(tags);
            //CrossAzurePushNotification.Current.OnNotificationReceived += Notifications.OnNotificationReceived;
        }
    }
}
