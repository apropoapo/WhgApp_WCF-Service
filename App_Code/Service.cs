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
using System.IO;

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
        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        StreamReader reader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream(), Encoding.GetEncoding("iso-8859-1"));
        String htmlString = reader.ReadToEnd();
        document.LoadHtml(WebUtility.HtmlDecode(htmlString));


        var atags = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__content\"]/descendant::a[position()=1]");
        var picture_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__objects-wrapper\"]/descendant::a[attribute::class=\"preview box\"]/descendant::img");
        var Miete_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::dl[attribute::class=\"criteria pull-left\"]/descendant::dt[attribute::class=\"title\" and (text()=\"Kaltmiete\" or text()=\"Kaufpreis\")]/following-sibling::dd[attribute::class=\"value\"]");
        var Flaeche_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::dl[attribute::class=\"criteria pull-left\"]/descendant::dt[attribute::class=\"title\" and text()=\"Zimmer\"]/following-sibling::dd[attribute::class=\"value\"]");
        var Zimmer_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::dl[attribute::class=\"criteria pull-left\"]/descendant::dt[attribute::class=\"title\" and text()=\"Wohnfläche\"]/following-sibling::dd[attribute::class=\"value\"]");
        var lage_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"]/descendant::div[attribute::class=\"medialist__heading-wrapper\"]/descendant::p[attribute::class=\"medialist__address hideable\"]");
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
            string Header, Picture, Miete, Flaeche, Zimmer, lage, ID;
            string[] detailArray = new string[4];
            detailArray[0] = " ";
            detailArray[1] = " ";
            detailArray[2] = " ";
            detailArray[3] = " ";
            try
            {
                Header = atags[i].InnerText.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                Header = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                Header = " ";
            }

            try
            {
                Picture = picture_tag[i].Attributes["src"].Value.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                Picture = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                Picture = " ";
            }

            try
            {
                Miete = Miete_tag[i].InnerText.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                Miete = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                Miete = " ";
            }

            try
            {
                Flaeche = Flaeche_tag[i].InnerText.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                Flaeche = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                Flaeche = " ";
            }

            try
            {
                Zimmer = Zimmer_tag[i].InnerText.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                Zimmer = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                Zimmer = " ";
            }

            try
            {
                int j = i + 1;
                var detail_tag = document.DocumentNode.SelectNodes("/descendant::li[attribute::class=\"media medialist box\"][position()=" + j + "]/descendant::ul[attribute::class=\"medialist__criteria-secondary unstyled inline mts hideable\"]/descendant::li[attribute::class=\"title\"]");
                int k = 0;
                if (detail_tag != null)
                {
                    foreach (var tag in detail_tag)
                    {
                        detailArray[k] = tag.InnerText.Trim();
                        k++;
                    }
                }

            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            try
            {
                lage = lage_tag[i].InnerText.Trim();
            }
            catch (IndexOutOfRangeException)
            {
                lage = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                lage = " ";
            }

            try
            {
                ID = id_tag[i].Attributes["id"].Value.Trim();
                ID = ID.Split('-')[1];
            }
            catch (IndexOutOfRangeException)
            {
                ID = " ";
            }
            catch (ArgumentOutOfRangeException)
            {
                ID = " ";
            }


            //Im gesplitteten Array
            //         0               1                 2               3                  4                      5                       6                          7                      8                      9                   10
            res[i] = Header + ";:;" + Picture + ";:;" + Miete + ";:;" + Zimmer + ";:;" + Flaeche + ";:;" + detailArray[0] + ";:;" + detailArray[1] + ";:;" + detailArray[2] + ";:;" + detailArray[3] + ";:;" + ID + ";:;" + lage + ";:;";


        }



        return res;
    }
 

}
