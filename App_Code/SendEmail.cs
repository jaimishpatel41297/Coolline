using System.Configuration;
using Common;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
    Email oEmail;

    string VES;

    public SendEmail()
    {
        //
        // TODO: Add constructor logic here
        //

    }
   
    

    
    public void SendMailWithBody(string body, string email, string subject)
    {
        string st = email;

        //mailmessage mail = new mailmessage();
        //smtpclient smtpserver = new smtpclient("smtp.gmail.com");
        //mail.from = new mailaddress(configurationmanager.appsettings.get("emailfrom"));
        //mail.to.add(email);
        //mail.subject = subject;
        //mail.body = body;

        //mail.isbodyhtml = true;

        //smtpserver.port = 25;

        //smtpserver.credentials = new system.net.networkcredential(configurationmanager.appsettings.get("smtpauthemail"), configurationmanager.appsettings.get("smtpauthpassword"));
        //smtpserver.enablessl = true;
        //smtpserver.send(mail);

        oEmail = new Email(st, "coollinecorporation900@gmail.com", ConfigurationManager.AppSettings.Get("bccEmail"), subject, body);
        oEmail.SendAuthenticatedEmail = true;   
        oEmail.Send();
        //oEmail = new Email("beingexporter90@gmail.com", email, ConfigurationManager.AppSettings.Get("bccemail"), subject, body);
        //oEmail.SendAuthenticatedEmail = false;
        //oEmail.Send();
    }
    public void SendMailWithBody1 (string body,string subject,string email)
    {
        oEmail = new Email("beingexporter90@gmail.com", email, ConfigurationManager.AppSettings.Get("bccemail"), subject, body);
        oEmail.SendAuthenticatedEmail = true;
        oEmail.Send();
    }


}

