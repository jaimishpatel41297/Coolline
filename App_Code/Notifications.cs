using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Android;
using System.Net;
using System.Collections;
using System.Data;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for IOSNotification
/// </summary>
public class Notification
{
    public static string DEMO_CLIENT_ID = "10001";
    GetData cls = new GetData();
    public Notification()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    private class NotificationMessage
    {
        public string Title;
        public string Message;
        public string ItemId;
    }


    public string SendMultipleNotification(List<string> deviceRegIds, string message, string title, string id, string clientid)
    {

        string SERVER_API_KEY = "";
        var SENDER_ID = "";
        string regIds = string.Join("\",\"", deviceRegIds);

        NotificationMessage nm = new NotificationMessage();
        nm.Title = title;
        nm.Message = message;
        nm.ItemId = id;

        var value = new JavaScriptSerializer().Serialize(nm);
        string type = "Text";
        string image = "";
        DataTable dtClient = cls.GetDataTable("select * from ClientRegistration where ClientId=" + clientid + "");
        if (dtClient.Rows.Count > 0)
        {
            title = title + " (" + dtClient.Rows[0]["ClientName"].ToString() + ")";
            SENDER_ID = dtClient.Rows[0]["ServerKey"].ToString();
            SERVER_API_KEY = dtClient.Rows[0]["Android"].ToString();
            if (dtClient.Rows[0]["Type"].ToString() == "demo")
            {
                DataTable dtClientDemo = cls.GetDataTable("select * from ClientRegistration where ClientId=" + DEMO_CLIENT_ID + "");
                if (dtClientDemo.Rows.Count > 0)
                {

                    SENDER_ID = dtClientDemo.Rows[0]["ServerKey"].ToString();
                    SERVER_API_KEY = dtClientDemo.Rows[0]["Android"].ToString();
                }
            }
        }
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = "application/json";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        string postData = "{\"collapse_key\":\"score_update\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"image\":\"" + image + "\",\"notificationid\":\"" + id + "\",\"badge\":7,\"sound\":\"sound.caf\",\"title\":\"" + title + "\",\"type\":\"" + type + "\",\"intenttype\":\"\", \"message\" : \"" + message + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + regIds + "\"]}";

        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();

        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
    }

    public string SendPushNotification(string deviceId, string message, string title, string clientid, string type, string image, string intenttype)
    {
        try
        {
            string ApplicationID;
            string SENDER_ID = "";
            string RegId;

            string api_key = "";
            //string api_key = "AIzaSyDpsl-yyDCrWJm8jJG5WBqfPASOqFxQBds";
            if (image == "")
                type = "Text";
            else
            {
                type = "Image";
                image = "http://ims.studyfield.com/" + image;
            }

            SENDER_ID = "280311916805";
            api_key = "AIzaSyDBhrkdfgXFEh345oWfyZ6xFDZHYtyV4T0";
            //DataTable dtClient = cls.GetDataTable("select * from ClientRegistration where ClientId=" + clientid + "");
            //if (dtClient.Rows.Count > 0)
            //{
            //    title = title + " (" + dtClient.Rows[0]["ClientName"].ToString() + ")";
            //    SENDER_ID = dtClient.Rows[0]["ServerKey"].ToString();
            //    api_key = dtClient.Rows[0]["Android"].ToString();
            //    if (dtClient.Rows[0]["Type"].ToString() == "demo")
            //    {
            //        DataTable dtClientDemo = cls.GetDataTable("select * from ClientRegistration where ClientId=" + DEMO_CLIENT_ID + "");
            //        if (dtClientDemo.Rows.Count > 0)
            //        {

            //            SENDER_ID = dtClientDemo.Rows[0]["ServerKey"].ToString();
            //            api_key = dtClientDemo.Rows[0]["Android"].ToString();
            //        }
            //    }
            //}

            Random rd = new Random();
            int notification_id = rd.Next(100, 500);


            RegId = deviceId;
            ApplicationID = api_key;// "AIzaSyCGb6uLiwOHTORZ1qmK4ScC5fJ9rry8bJs";

            //SENDER_ID = "187862974348";
            //ApplicationID = "AIzaSyBjOgFpdPQE0T512ilH2fN7PdnFTGyoEto";

            var value = message; //message text box

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID));
            httpWebRequest.Headers.Add(string.Format("Sender: key={0}", SENDER_ID));
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //.WithJson("{\"type\":\"" + type + "\",\"message\":\"" + message + "\",\"badge\":7,\"sound\":\"sound.caf\",\"title\":\"" + title + "\",\"image\":\"" + image + "\",\"notificationid\":\"" + notificationid + "\"}"));
                string json = "{\"registration_ids\":[\"" + RegId + "\"]," + "\"data\": {\"intenttype\":\"" + intenttype + "\", \"type\":\"" + type + "\",\"message\":\"" + message + "\",\"badge\":7,\"sound\":\"sound.caf\",\"title\":\"" + title + "\",\"image\":\"" + image + "\",\"notificationid\":\"" + notification_id + "\"}}";
                Console.WriteLine(json);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "1";
    }

    public void SendPushNotification(string deviceId, string message, string title, string clientid)
    {
        string api_key = "";
        //string api_key = "AIzaSyDpsl-yyDCrWJm8jJG5WBqfPASOqFxQBds";
        
        api_key = "AIzaSyDBhrkdfgXFEh345oWfyZ6xFDZHYtyV4T0";
        

        var push = new PushBroker();

        //Wire up the events for all the services that the broker registers
        push.OnNotificationSent += NotificationSent;
        push.OnChannelException += ChannelException;
        push.OnServiceException += ServiceException;
        push.OnNotificationFailed += NotificationFailed;
        push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
        push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
        push.OnChannelCreated += ChannelCreated;
        push.OnChannelDestroyed += ChannelDestroyed;

        //---------------------------
        // ANDROID GCM NOTIFICATIONS
        //---------------------------
        //Configure and start Android GCM
        //IMPORTANT: The API KEY comes from your Google APIs Console App, under the API Access section, 
        //  by choosing 'Create new Server key...'
        //  You must ensure the 'Google Cloud Messaging for Android' service is enabled in your APIs Console
        push.RegisterGcmService(new GcmPushChannelSettings("" + api_key + ""));
        ////Fluent construction of an Android GCM Notification
        ////IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!
        push.QueueNotification(new GcmNotification()
            .ForDeviceRegistrationId("" + deviceId + "")
            .WithJson("{\"message\":\"" + message + "\",\"badge\":7,\"sound\":\"sound.caf\",\"title\":\"" + title + "\"}"));


        push.StopAllServices();
    }

    public string SendAndroidNotification(string deviceId, string message, string title, string clientid)
    {
        string ApplicationID;
        string SENDER_ID = "";
        string RegId;

        string api_key = "";
        //string api_key = "AIzaSyDpsl-yyDCrWJm8jJG5WBqfPASOqFxQBds";

        DataTable dtClient = cls.GetDataTable("select * from ClientRegistration where ClientId=" + clientid + "");

        if (dtClient.Rows.Count > 0)
        {
            api_key = dtClient.Rows[0]["Android"].ToString();
            SENDER_ID = dtClient.Rows[0]["ServerKey"].ToString();
        }

        RegId = deviceId;
        ApplicationID = api_key;// "AIzaSyCGb6uLiwOHTORZ1qmK4ScC5fJ9rry8bJs";

        //SENDER_ID = "187862974348";
        //ApplicationID = "AIzaSyBjOgFpdPQE0T512ilH2fN7PdnFTGyoEto";

        var value = message; //message text box

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID));
        httpWebRequest.Headers.Add(string.Format("Sender: key={0}", SENDER_ID));
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {

            string json = "{\"registration_ids\":[\"" + RegId + "\"]," + "\"data\": { \"message\" : \"" + message + "\",\"title\" : \"" + "Hello" + "\"}}";
            Console.WriteLine(json);
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
                return "1";
            }
        }
        return "0";
    }
    public void IOSSendNotification(string deviceId, string message, string title, string dt, string clientid)
    {
        //Create our push services broker
        var push = new PushBroker();

        //Wire up the events for all the services that the broker registers
        push.OnNotificationSent += NotificationSent;
        push.OnChannelException += ChannelException;
        push.OnServiceException += ServiceException;
        push.OnNotificationFailed += NotificationFailed;
        push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
        push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
        push.OnChannelCreated += ChannelCreated;
        push.OnChannelDestroyed += ChannelDestroyed;

        var push1 = new PushBroker();

        //Wire up the events for all the services that the broker registers
        push1.OnNotificationSent += NotificationSent;
        push1.OnChannelException += ChannelException;
        push1.OnServiceException += ServiceException;
        push1.OnNotificationFailed += NotificationFailed;
        push1.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
        push1.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
        push1.OnChannelCreated += ChannelCreated;
        push1.OnChannelDestroyed += ChannelDestroyed;

        string Android = "";
        string IOSprod = "";
        string IOSlive = "";
        DataTable dtClient = cls.GetDataTable("select * from ClientRegistration where ClientId=" + clientid + "");

        if (dtClient.Rows.Count > 0)
        {
            Android = dtClient.Rows[0]["Android"].ToString();
            IOSprod = dtClient.Rows[0]["IOSprod"].ToString();
            IOSlive = dtClient.Rows[0]["IOSlive"].ToString();
        }

        var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "E:\\Radiant_Prod.p12"));
        //var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IOSprod));
        //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server 
        //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
        //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
        //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
        push.RegisterAppleService(new ApplePushChannelSettings(appleCert, "1")); //Extension method
        //Fluent construction of an iOS notification
        //IMPORTANT: For iOS you MUST MUST MUST use your own DeviceToken here that gets generated within your iOS app itself when the Application Delegate
        //  for registered for remote notifications is called, and the device token is passed back to you


        Hashtable a = new Hashtable();
        a.Add("1", "A");
        a.Add("2", "B");
        a.Add("3", "C");

        //Object[] obj = new Object[3];
        //obj[0] = a;

        StringBuilder JSON = new StringBuilder();
        JavaScriptSerializer js = new JavaScriptSerializer();

        js.Serialize(a);

        JSON.Append("{");


        JSON.Append("\"alert\":\"" + title + "\", ");
        JSON.Append("\"date\":\"" + dt + "\",");
        JSON.Append("\"message\":\"" + message + "\"");
        JSON.Append("}");

        // JSON.Append("{"aps": {"alert": "joetheman","sound": "default"},"message": "Some custom message for your app","id": 1234}");

        AppleNotificationPayload aa = new AppleNotificationPayload("adasd", 1, "sound.caf", "asdadsasd");

        push.QueueNotification(new AppleNotification()
                                   .ForDeviceToken("" + deviceId + "")
                                   .WithAlert("" + title + "")
                                   .WithBadge(1)
                                   .WithSound("sound.caf")
                                   .WithCustomItem("Title", title)
                                   .WithCustomItem("Date", dt)
                                   .WithCustomItem("Message", message));

        push.StopAllServices();

        var appleCert1 = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "E:\\Radiant_Dev.p12"));
        //  var appleCert1 = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IOSlive));
        //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server 
        //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
        //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
        //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
        push1.RegisterAppleService(new ApplePushChannelSettings(appleCert1, "1")); //Extension method
        //Fluent construction of an iOS notification
        //IMPORTANT: For iOS you MUST MUST MUST use your own DeviceToken here that gets generated within your iOS app itself when the Application Delegate
        //  for registered for remote notifications is called, and the device token is passed back to you


        //AppleNotificationPayload aa1 = new AppleNotificationPayload("adasd", 1, "sound.caf", "asdadsasd");

        push1.QueueNotification(new AppleNotification()
                                   .ForDeviceToken("" + deviceId + "")
                                   .WithAlert("" + title + "")
                                   .WithBadge(1)
                                   .WithSound("sound.caf")
                                   .WithCustomItem("Title", title)
                                   .WithCustomItem("Date", dt)
                                   .WithCustomItem("Message", message));


        push1.StopAllServices();



    }

    static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
    {
        //Currently this event will only ever happen for Android GCM
        Console.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
    }

    static void NotificationSent(object sender, INotification notification)
    {
        Console.WriteLine("Sent: " + sender + " -> " + notification);
    }

    static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
    {
        Console.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
    }

    static void ChannelException(object sender, IPushChannel channel, Exception exception)
    {
        Console.WriteLine("Channel Exception: " + sender + " -> " + exception);
    }

    static void ServiceException(object sender, Exception exception)
    {
        Console.WriteLine("Service Exception: " + sender + " -> " + exception);
    }

    static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
    {
        Console.WriteLine("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
    }

    static void ChannelDestroyed(object sender)
    {
        Console.WriteLine("Channel Destroyed for: " + sender);
    }

    static void ChannelCreated(object sender, IPushChannel pushChannel)
    {
        Console.WriteLine("Channel Created for: " + sender);
    }


}
