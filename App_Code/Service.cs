﻿using System;
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

    const string CONSTRING = "server=8a1732a4-5501-4f63-ae2f-a213015171ab.mysql.sequelizer.com;database=db8a1732a455014f63ae2fa213015171ab;uid=tvzwzpkdytttjbtm;pwd=LZDsGqmDUXQBiRJrfALZZCZmiWgwBZgi4JYdu4pBNzgg467pjBP4z4Ki8rjSjWc4";

	public int plusplus(int v1, int v2)
	{
        return v1 + v2;
	}

    
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
            string cmdInsertText = "INSERT INTO myapptable (ID, changed, PushNotificationUri, deleted, UsePushNotifications, UniqueID, ImmoscoutURL) values ( 0, " + changed + ", '" + PushNotificationUri + "', " + delete + ", " + UsePushNotifications + ", '" + UniqueID + "', '" + ImmoscoutURL + "');";
            MySqlCommand cmdInsert = new MySqlCommand(cmdInsertText, con);
            cmdInsert.ExecuteNonQuery();

        }
        else
        {
            // SQL Update durchführen
            string cmdUpdateText = "UPDATE myapptable SET changed=" + changed + ", PushNotificationUri='" + PushNotificationUri + "', deleted=" + delete + ", UsePushNotifications=" + UsePushNotifications + ", ImmoscoutURL='" + ImmoscoutURL +"' WHERE UniqueID='" + UniqueID + "';";
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



    public string[] getWhgs(string url)
    {


        var webGet = new HtmlWeb();
        var document = webGet.Load(url);

        var atags = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__content\"]/descendant::a[position()=1]");
        var picture_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__objects-wrapper\"]/descendant::a[attribute::class=\"preview box\"]/descendant::img");
        var Miete_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"line medialist__criteria hideable \"]/descendant::dd[attribute::class=\"value\" and position()=1]");
        var Flaeche_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"line medialist__criteria hideable \"]/descendant::dd[attribute::class=\"value\" and position()=2]");
        var Zimmer_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"line medialist__criteria hideable \"]/descendant::dd[attribute::class=\"value\" and position()=3]");
        var lage_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__heading-wrapper\"]/descendant::p[attribute::class=\"medialist__address mts hideable\"]");
        var id_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]");


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




        for (int i = 0; i < count; i++)
        {
            string Header = atags[i].InnerText.Trim();
            string Picture = picture_tag[i].Attributes["src"].Value.Trim();
            string Miete = Miete_tag[i].InnerText.Trim();
            string Flaeche = Flaeche_tag[i].InnerText.Trim();
            string Zimmer = Zimmer_tag[i].InnerText.Trim();
            int j = i + 1;
            var detail_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"][position()=" + j +"]/descendant::ul[attribute::class=\"medialist__criteria-secondary unstyled inline mts hideable\"]/descendant::li[attribute::class=\"title\"]");
       //     string link1 = atags[i].Attributes["onclick"].Value.Trim();
       //     string link2 = atags[i].Attributes["href"].Value;
            string lage = lage_tag[i].InnerText.Trim();
            string ID = id_tag[i].Attributes["data-realEstateId"].Value.Trim();


            // Umformungen/Substrings und so
         //   string[] linkArray1, linkArray2;
       //     string[] sep = new string[1];
         //   sep[0] = "searchUrl=";
        //    linkArray1 = link1.Split(sep, System.StringSplitOptions.None);
        //    linkArray2 = link2.Split(';');

           // string link_komplett = linkArray2[0];



            // int countdetail = detail_tag.Count;

            // string detail1 = detail1_tag[i].InnerText.Trim();
           //  string detail2 = detail2_tag[i].InnerText.Trim();
            // string detail3 = " ";
            //if (detail_tag[i].InnerText != null)
            //  detail3 = detail3_tag[i].InnerText.Trim();
            //string detail4 = detail4_tag[i].InnerText.Trim();


            // Convertierungen
            Flaeche = Flaeche.Replace("m&sup2;", "m²");

            string[] detailArray = new string[4];
            detailArray[0] = " ";
            detailArray[1] = " ";
            detailArray[2] = " ";
            detailArray[3] = " ";

            int k = 0;
            if (detail_tag != null)
            {
                foreach (var tag in detail_tag)
                {
                    detailArray[k] = tag.InnerText.Trim();
                    k++;
                }
            }



            //Im gesplitteten Array
            //         0               1                 2               3                  4                      5                       6                          7                      8                      9                   10
            res[i] = Header + ";:;" + Picture + ";:;" + Miete + ";:;" + Zimmer + ";:;" + Flaeche + ";:;" + detailArray[0] + ";:;" + detailArray[1] + ";:;" + detailArray[2] + ";:;" + detailArray[3] + ";:;" + ID + ";:;" + lage + ";:;"; 



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
