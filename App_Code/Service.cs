using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.Net;
using HtmlAgilityPack;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	public int plusplus(int v1, int v2)
	{
        return v1 + v2;
	}

    const string CONSTRING = "Server=instance29437.db.xeround.com;Port=19153;Database=users;Uid=appharbor;Pwd=NNDKjRzh";

    public bool addUser(int changed, string PushNotificationUri, int delete, int UsePushNotifications, string UniqueID, string ImmoscoutURL)
    {
        //connect
        MySqlConnection con = new MySqlConnection(CONSTRING);
        con.Open();


        //SQL COUNT-ABFRAGE durchführen (wird gebraucht für: prüfung ob UniqueID vorhanden ist)
        string cmdCountText = "SELECT count(*) FROM myapptable WHERE UniqueID='" + UniqueID + "';";
        MySqlCommand cmdCount = new MySqlCommand(cmdCountText, con);
        int count_result = int.Parse(cmdCount.ExecuteScalar().ToString());

        // prüfen ob uniqueID vorhanden ist
        if (count_result == 0)
        {
            //SQL Insert durchführen
            string cmdInsertText = "INSERT INTO myapptable (ID, changed, PushNotificationUri, `delete`, UsePushNotifications, UniqueID, ImmoscoutURL) values ( 0, " + changed + ", '" + PushNotificationUri + "', " + delete + ", " + UsePushNotifications + ", '" + UniqueID + "', '" + ImmoscoutURL + "');";
            MySqlCommand cmdInsert = new MySqlCommand(cmdInsertText, con);
            cmdInsert.ExecuteNonQuery();

        }
        else
        {
            // SQL Update durchführen
            string cmdUpdateText = "UPDATE myapptable SET changed=" + changed + ", PushNotificationUri='" + PushNotificationUri + "', `delete`=" + delete + ", UsePushNotifications=" + UsePushNotifications + ", ImmoscoutURL='" + ImmoscoutURL +"' WHERE UniqueID='" + UniqueID + "';";
            MySqlCommand cmdUpdate = new MySqlCommand(cmdUpdateText, con);
            cmdUpdate.ExecuteNonQuery();
        }
        con.Close();
        return true;
    }

    public int updateUri(int changed, string UniqueID, string PushNotificationUri)
    {
        //connect
        MySqlConnection con = new MySqlConnection(CONSTRING);
        con.Open();

        //SQL COUNT-ABFRAGE erstellen (wird gebraucht für: prüfung ob UniqueID vorhanden ist)
        string cmdText = "SELECT count(*) FROM myapptable WHERE UniqueID='"+ UniqueID+"';";
        MySqlCommand cmd = new MySqlCommand(cmdText, con);
        int count_result = int.Parse(cmd.ExecuteScalar().ToString());

        // prüfen ob uniqueID vorhanden ist
        if (count_result == 1)
        {
            string cmdText2 = "UPDATE myapptable SET PushNotificationUri='" + PushNotificationUri + "', changed=" + changed + " WHERE UniqueID='" + UniqueID+"';";
            MySqlCommand cmd2 = new MySqlCommand(cmdText2, con);
            // SQL Insert durchführen
            cmd2.ExecuteNonQuery();
        }// falls nicht, wird nichts geändert



        con.Close();

        return count_result;
    }

    public void SendToast(string title, string message, string PushNotificationUri)
    {
        string toastMsg = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<wp:Notification xmlns:wp=\"WPNotification\">" +
                "<wp:Toast>" +
                    "<wp:Text1>" + title + "</wp:Text1>" +
                    "<wp:Text2>" + message + "</wp:Text2>" +
                "</wp:Toast> " +
            "</wp:Notification>";

        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(toastMsg);
        Uri uri = new Uri(PushNotificationUri);
        SendMessage(uri, messageBytes);
    }

    private static void SendMessage(Uri uri, byte[] message)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.Method = WebRequestMethods.Http.Post;
        request.ContentType = "text/xml";
        request.ContentLength = message.Length;
        request.Headers.Add("X-MessageID", Guid.NewGuid().ToString());
        request.Headers["X-WindowsPhone-Target"] = "toast";
        request.Headers.Add("X-NotificationClass", "2");

        var requestStream = request.GetRequestStream();
        requestStream.Write(message, 0, message.Length);
    }



    public string[] getWhgs(string uri)
    {

        string testuri = "http://www.immobilienscout24.de/Suche/S-82/Wohnung-Miete/Bayern/Muenchen/Altstadt_Am-Hart_Freimann_Haidhausen_Laim_Lehel_Ludwigsvorstadt-Isarvorstadt_Maxvorstadt_Neuhausen_Nymphenburg_Schwabing_Schwabing-West_Schwanthalerhoehe_Sendling_Thalkirchen/2,00-3,00/-/EURO-450,00-800,00";
        var webGet = new HtmlWeb();
        var document = webGet.Load(testuri);

        var atags = document.DocumentNode.SelectNodes("/descendant::ol/descendant::li[attribute::class=\"is24-res-entry\"]/descendant::a[position()=1]");
        var picture_tag = document.DocumentNode.SelectNodes("/descendant::ol/descendant::li[attribute::class=\"is24-res-entry\"]/descendant::a[position()=2]/descentant::img");
        var Miete_tag = document.DocumentNode.SelectNodes("/descendant::ol/descendant::li[attribute::class=\"is24-res-entry\"]/descendant::dl[attribute::class=\"is24-res-details\"]/descendant::dd[position()=1]");
        var Flaeche_tag = document.DocumentNode.SelectNodes("/descendant::ol/descendant::li[attribute::class=\"is24-res-entry\"]/descendant::dl[attribute::class=\"is24-res-details\"]/descendant::dd[position()=2]");
        var Zimmer_tag = document.DocumentNode.SelectNodes("/descendant::ol/descendant::li[attribute::class=\"is24-res-entry\"]/descendant::dl[attribute::class=\"is24-res-details\"]/descendant::dd[position()=3]");

        string[] res;
        int count = atags.Count;
        if (count == 20)
        {
            res = new String[20];
        }
        else if (count > 0)
        {
            res = new String[count];
        }
        else
        {
            return null;
        }


        //Console.WriteLine("List scout.");
        //int i = 0;

        for (int i = 0; i < count; i++)
        {
            string Header = atags[i].InnerText.Trim();
            string Picture = picture_tag[i].Attributes["src"].Value.Trim();
            string Miete = Miete_tag[i].InnerText.Trim();
            string Flaeche = Flaeche_tag[i].InnerText.Trim();
            string Zimmer = Zimmer_tag[i].InnerText.Trim();
            res[i] = Header + ";" + Picture + ";" + Miete + ";" + Flaeche + ";" + Zimmer;

        }
        
        /*
         *
        foreach (var tag in atags)
        {
            // Beschreibung
            string s = tag.InnerText;
            s = s.Trim();
            res[i] = s;
            i++;
        }
        */

        return res;

        //  Console.ReadLine();
    }
 

}
