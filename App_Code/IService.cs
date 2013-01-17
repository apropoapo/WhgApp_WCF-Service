using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;



// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
	[OperationContract]
	int plusplus(int v1, int v2);

    [OperationContract]
    bool addUser(int changed, string PushNotificationUri, int delete, int UsePushNotifications, string UniqueID, string ImmoscoutURL);

    [OperationContract]
    int updateUri(int changed, string UniqueID, string PushNotificationUri);

    [OperationContract]
    void SendToast(string title, string message, string PushNotificationUri);

    [OperationContract]
    string[] getWhgs(string uri);

}


