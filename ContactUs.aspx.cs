using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml;

public partial class ContactUs : System.Web.UI.Page
{
    Connection cc = new Connection();
    SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Coolline"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            BindAddress();
        }
    }

   

    private void BindAddress()
    {
        try
        {
            string m1 = "";
            string qry = "select *  from Contact";
            DataTable dt = cc.GetDataTable(qry);
            
            foreach (DataRow row in dt.Rows)
            {            
                m1 += "<div class='content-element4'>" +
                        "<ul class='contact-info v-type'>" +
                       "<li class='info-item'>" +
                       "<i class='licon-map-marker'></i>" +
                       "<div class='item-info'>" +
                       "<span>" + row["Address"] + "</span>" +
                       "</div>" +
                "</li>" +
                "<li class='info-item'>" +
                  "<i class='licon-telephone2'></i>" +
                   "<div class='item-info'>" +
                    "<span>Phone:</span>" +
                     "<a href = '#'>" + row["Phone"] + " </a>" +
                    "</div>" +
                "</li>" +
                 "<li class='info-item'>" +
                  "<i class='licon-at-sign'></i>" +
                   "<div class='item-info'>" +
                    "<span>E-mail:</span>" +
                    "<a href = 'mailto:#'>" + row["Email"] + " </a>" +
                  "</div>" +
                "</li>" +
                 "<li class='info-item'>" +
                  "<i class='licon-clock3'></i>" +
                   "<div class='item-info'>" +
                    "<span> Open 8am-5pm: <br> Monday- Saturday</span>" +
                  "</div>" +
                 "</li>" +
              "</ul>" +
             "</div>";

            }
            ltradd.Text = m1;
        }
        catch (Exception e )
        {

        }
    }


    protected void lbSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "insert into ProductInquiryMaster(Name,CompanyName,Email,ContactNo,Series,Message) values('" + txtname.Text + "','" + txtcompany.Text + "','" + txtemail.Text + "','" + txtcontact.Text + "','" + txtproduct.Text + "','"+ txtmessage.Text + "')";
            cc.ExecuteQuery(qry);


            SendEmail sm = new SendEmail();
            string bodyPart;
            bodyPart = "<html> " +
                   "<body>" +
                   "<table style='margin: 0 auto; padding: 35px 25px; min-width: 650px; max-width: 650px; border: 1px solid #e5e5e5;' border='0' width='600' cellspacing='0' cellpadding='0' align='center' bgcolor='ffffff'>" +
                   "<tbody>" +
                   "<tr style='height: 52px;'>" +
                   "<td style='width: 600px; background-color: #ffffff; height: 52px;' colspan='3'><a style='text-decoration: none; color: #010101;' href='http://coolline.itfuturz.com/' target='_blank' rel='noopener' data-saferedirecturl='#'>" + "<img class='CToWUd' style='margin: 0 auto; padding: 0 10px 10px 10px; display: block; background-color: #ffffff; color: #010101;' src='http://coolline.itfuturz.com//images/Logo.png' width='150' border='0' /></a></td>" +
                   "</tr>" +
                  "<tr>" +
                   "<table style='width: 100%; padding: 20px 25px 10px 25px; border-bottom: 2px dashed #f1f1f1; font-family: Arial,sans-serif; margin: 0 auto;' cellspacing='0' cellpadding='0'>" +
                   "<tbody>" +
                         "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>Hi, Coolline</td>" +

                   "</tr>" +
                    "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>I am " + txtname.Text + "</td>" +
                   "</tr>" +
                    "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>My Company name is " + txtcompany.Text + "</td>" +                  
                   "</tr>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>My Email id is " + txtemail.Text + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>My Contact Number is " + txtcontact.Text + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>I Would Like to know about " + txtproduct.Text + "</td>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 100%;' align='left'>" + txtmessage.Text + "</td>" +
                   "</tr>" +
                   "</tbody>" +
                   "</table>" +
                   "</td>" +
                   "</tr>" +
                   "</tbody>" +
                   "</table>" +
                   "</body>" +
                   "</html>";

            sm.SendMailWithBody(bodyPart, txtemail.Text, "Inquiry");
            Clear();
            
        }
        catch(Exception e12)
        {

        }
     }

  
  
    private void Clear()
    {
        txtname.Text = "";
        txtemail.Text = "";
        txtcompany.Text = "";
        txtmessage.Text = "";
        txtcontact.Text = "";
        txtproduct.Text = "";
    }
}      