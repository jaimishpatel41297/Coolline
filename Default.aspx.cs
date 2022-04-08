using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranch();
            BindTeam();
            BindProject();
            BindProject1();
            BindProject2();
            Bindbanner();
        }

    }

    private void Bindbanner()
    {
        try
        {
            string str = "";
            string qry = "select * from banner";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='slideshow-container'>" +
                         "<div class='mySlides fade'>" +
                         "<img src='"+row["Image"]+"' style='width: 100%' />" +
                        "</div>" +
                       "</div>";
            }
            ltrbann.Text = str;

        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindProject2()
    {
        try
        {
            string str = "";
            string qry = "select Top 6 * from OurProjectMaster where Status=1 order by id desc";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='col-md-4 col-sm-12' style='margin-bottom:24px;'>" +
                                 "<div class='entry'>" +
                                     "<div class='thumbnail-attachment'>" +
                                        "<a href = '#'>" +
                                        "<img src='" + row["Image"] + "' height='270' alt=''/>" +
                                        "</a>" +
                                    "</div>" +
                                    "<div class='entry-body'>" +
                                            "<time class='entry-date' datetime='2016-08-27'>August 27, 2018</time>" +
                                            "<a href = '#' class='entry-cat'>News</a>" +
                                        "<h4 class='entry-title'><a href = '" + row["Image"] + "' > " + row["Name"] + "</a></h4>" +
                                        "<p>" + row["Description"] + "</p>" +
                                     "</div>" +
                                "</div>" +
                            "</div>";
            }
            ltrres.Text = str;

        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindProject1()
    {
        try
        {
            string str = "";
            string qry = "select Top 6 * from OurProjectMaster where Status=1 order by id ";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='col-md-4 col-sm-12' style='margin-bottom:24px;'>" +
                                 "<div class='entry'>" +
                                     "<div class='thumbnail-attachment'>" +
                                        "<a href = '#'>" +
                                        "<img src='" + row["Image"] + "' height='270' alt=''/>" +
                                        "</a>" +
                                    "</div>" +
                                    "<div class='entry-body'>" +
                                            "<time class='entry-date' datetime='2016-08-27'>August 27, 2018</time>" +
                                            "<a href = '#' class='entry-cat'>News</a>" +
                                        "<h4 class='entry-title'><a href = '" + row["Image"] + "' > " + row["Name"] + "</a></h4>" +
                                        "<p>" + row["Description"] + "</p>" +
                                     "</div>" +
                                "</div>" +
                            "</div>";
            }
            ltrcom.Text = str;

        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindProject()
    {
        try
        {
            string str = "";
            string qry = "select Top 6 * from OurProjectMaster where Status=1 order by id desc";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='col-md-4 col-sm-12' style='margin-bottom:24px;'>" +
                                 "<div class='entry'>" +
                                     "<div class='thumbnail-attachment'>" +
                                        "<a href = '#'>" +
                                        "<img src='" + row["Image"] + "' height='270' alt=''/>" +
                                        "</a>" +
                                    "</div>" +
                                    "<div class='entry-body'>" +
                                            "<time class='entry-date' datetime='2016-08-27'>August 27, 2018</time>" +
                                            "<a href = '#' class='entry-cat'>News</a>" +
                                        "<h4 class='entry-title'><a href = '" + row["Image"] + "' > " + row["Name"] + "</a></h4>" +
                                        "<p>" + row["Description"] + "</p>" +
                                     "</div>" +
                                "</div>" +
                            "</div>";
            }
            ltrall.Text = str;

        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    //private void BindProject1()
    //{
    //    try
    //    {
    //        string str = "";
    //        string qry = "select * 6 from OurProjectMaster where Status=1 order by id desc";
    //        DataTable dt = cc.GetDataTable(qry);

    //        foreach (DataRow row in dt.Rows)
    //        {
    //            str += "<div class='col-md-4 col-sm-12' style='margin-bottom:24px;'>" +
    //                             "<div class='entry'>" +
    //                                 "<div class='thumbnail-attachment'>" +
    //                                    "<a href = '#'>" +
    //                                    "<img src='" + row["Image"] + "' height='270' alt=''/>" +
    //                                    "</a>" +
    //                                "</div>" +
    //                                "<div class='entry-body'>" +
    //                                        "<time class='entry-date' datetime='2016-08-27'>August 27, 2018</time>" +
    //                                        "<a href = '#' class='entry-cat'>News</a>" +
    //                                    "<h4 class='entry-title'><a href = '" + row["Image"] + "' > " + row["Name"] + "</a></h4>" +
    //                                    "<p>" + row["Description"] + "</p>" +
    //                                 "</div>" +
    //                            "</div>" +
    //                        "</div>";
    //        }
    //        ltrall.Text = "";
    //        //ltrres.Text = str;
    //        //ltrcom.Text = "";
    //    }
    //    catch (Exception e)
    //    {
    //        Response.Write("Failed");
    //    }
    //}

    //private void BindProject2()
    //{
    //    try
    //    {
    //        string str = "";
    //        string qry = "select * 3 from OurProjectMaster where Status=1 order by id desc";
    //        DataTable dt = cc.GetDataTable(qry);

    //        foreach (DataRow row in dt.Rows)
    //        {
    //            str += "<div class='col-md-4 col-sm-12' style='margin-bottom:24px;'>" +
    //                             "<div class='entry'>" +
    //                                 "<div class='thumbnail-attachment'>" +
    //                                    "<a href = '#'>" +
    //                                    "<img src='" + row["Image"] + "' height='270' alt=''/>" +
    //                                    "</a>" +
    //                                "</div>" +
    //                                "<div class='entry-body'>" +
    //                                        "<time class='entry-date' datetime='2016-08-27'>August 27, 2018</time>" +
    //                                        "<a href = '#' class='entry-cat'>News</a>" +
    //                                    "<h4 class='entry-title'><a href = '" + row["Image"] + "' > " + row["Name"] + "</a></h4>" +
    //                                    "<p>" + row["Description"] + "</p>" +
    //                                 "</div>" +
    //                            "</div>" +
    //                        "</div>";
    //        }
    //        ltrall.Text = "";
    //        //ltrres.Text = "";
    //        //ltrcom.Text = str;
    //    }
    //    catch (Exception e)
    //    {
    //        Response.Write("Failed");
    //    }
    //}

    private void BindTeam()
    {
        try
        {
            string str = "";
            string qry = "select * from BranchMaster";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += " <div class='icons-wrap'>" +
                                  "<div class='icons-item'>" +
                                    "<div class='item-box'>" +
                                        "<i class='icon-location'></i>" +
                                         "<h5 class='icons-box-title'><a href='#'>" + row["Name"] + "</a></h5>" +
                                          "<p>" + row["Address"] + "</p>" +
                                    "</div>" +
                                "</div>" +
                            "</div>";
            }
            ltrbranch.Text = str;
        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindBranch()
    {
        try
        {
            string str = "";
            string qry = "select * from OurClient";
            DataTable dt = cc.GetDataTable(qry);
            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='item - carousel'>" +
                    "<a href='ss#'><img src= " + row["Photo"] + " alt='' style='height:111px;'/></a>" +
                    " </div>";
                //str += "<div class='item-carousel'>" +
                //                "<div class='team-member'>" +
                //                    "<a href = '#' class='member-photo' >" +
                //                        "<img src = " + row["Photo"] + " alt='' style='height:300px;' />" +
                //                     "</a>" +
                //                     "<div class='team-desc'>" +
                //                        "<div class='member-info'>" +
                //                           "<h4 class='member-name'><a href = '#' > " + row["Name"] + " </a></h4>" +
                //                     "</div>" +
                //                    "</div>" +
                //                "</div>" +
                //            "</div>";
            }
            clientimg.Text = str;
        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }
    protected void lbSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "insert into InquiryMaster(Name,Email,CompanyName,Message,ContactNo) values('" + txtname.Text + "','" + txtemail.Text + "','" + txtcompany.Text + "','" + txtMeassage.Text + "','" + txtmob.Text + "')";
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
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>Hi, Coolline</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +

                   "</tr>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>I am " + txtname.Text + "</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +

                   "</tr>" +
    "               <tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>My Company name is " + txtcompany.Text + "</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +

                   "</tr>" +
    "              <tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>My Contact Number is" + txtmob.Text + "</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +

                   "</tr>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>My Email id is " + txtemail.Text + "</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +
                   "<tr>" +
                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: left; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 312px;' align='left'>" + txtMeassage.Text + "</td>" +

                   "<td style='font-size: 16px; font-family: Arial, sans-serif; text-align: right; vertical-align: top; font-weight: bold; background-color: #f1f1f1; color: #333333; width: 208px;' align='left'></td>" +

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
        catch (Exception ex)
        {

        }
    }
    private void Clear()
    {
        txtname.Text = "";
        txtemail.Text = "";
        txtcompany.Text = "";
        txtMeassage.Text = "";
        txtmob.Text = "";
    }
}