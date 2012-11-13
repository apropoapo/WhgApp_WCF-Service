using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace myRegService
{
    

    public class User
    {
        public int ID {get; set;}
        public string Username { get; set; }
        public string PushNotificationURI { get; set; }
        public string AlertKeywords { get; set; }
        public bool UsePushNotifications { get; set; }
        public string PasswordHash { get; set; }

        public User(int ID, string Username, string PushNotificationUri, string AlertKeywords, bool UsePushNotifications, string PasswordHash)
        {
            this.ID = ID;
            this.Username = Username;
            this.PushNotificationURI = PushNotificationUri;
            this.AlertKeywords = AlertKeywords;
            this.UsePushNotifications = UsePushNotifications;
            this.PasswordHash = PasswordHash;
        }
    

    }

    
}