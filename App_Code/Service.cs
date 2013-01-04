using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	public int plusplus(int v1, int v2)
	{
        return v1 + v2;
	}

    const string CONSTRING = "Server=instance29437.db.xeround.com;Port=19153;Database=users;Uid=appharbor;Pwd=NNDKjRzh";

    public bool addUser(int changed, string PushNotificationUri, int delete, int UsePushNotifications, string UniqueID, string ImmoscoudID)
    {
        //connect
        MySqlConnection con = new MySqlConnection(CONSTRING);
        con.Open();

        //adapter
        MySqlDataAdapter adapter = new MySqlDataAdapter();


        //SQL Insert erstellen
        string cmdText = "INSERT INTO myapptable (ID, changed, PushNotificationUri, `delete`, UsePushNotifications, UniqueID, ImmoscoutURL) values ( 0, " + changed + ", '" + PushNotificationUri + "', " + delete + ", " + UsePushNotifications +", '"+UniqueID +"', '"+ ImmoscoudID+"');";
        MySqlCommand cmd = new MySqlCommand(cmdText, con);

        // SQL Insert durchführen
        cmd.ExecuteNonQuery();
        con.Close();
        return true;
    }
}
