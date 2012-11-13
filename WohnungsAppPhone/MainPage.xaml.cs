using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;

namespace WohnungsAppPhone
{
    public partial class MainPage : PhoneApplicationPage
    {

        private HttpNotificationChannel _channel;
        private string _channelName = "MeinPushNotificationService";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            

        int ID = 1;
        string Username = "user1";
        string PushNotificationURI = "notification blub";
        string AlertKeywords = "alert Keywords";
        bool UsePushNotifications = true;
        string PasswordHash = "meinpasswort";
        var client = new ServiceReference2.Service1Client();
        client.createUserAsync(ID, Username, PushNotificationURI, AlertKeywords, UsePushNotifications, PasswordHash);
  //      client.createUserCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_RegistrationCompleted);


        }
        void client_RegistrationCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           //textBlock1.Text = "PushService active";
        }

        private void ToastReceived(NotificationEventArgs e)
        {
        
        }


    }
}