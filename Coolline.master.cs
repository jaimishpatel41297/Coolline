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

public partial class Coolline : System.Web.UI.MasterPage
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindHeaderAddress();
            BindContact();
            BindFooterData();
            BindProduct();
        }
    }

    private void BindProduct()
    {
        try
        {
            string str = "";
            string qry = "select * from ProductCategoryMaster";
            DataTable dt = cc.GetDataTable(qry);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["ID"].ToString();
                    str += "<li><a href='Product.aspx?Id=" + id + "'>" + dt.Rows[i]["Name"] + "</a></li>";
                }
                ltrprd.Text = str;
            }
            //foreach (DataRow row in dt.Rows)
            //{

            //}
            
        }
        catch (Exception e66)
        {

        }
    }
    private void BindFooterData()
    {
        try
        {
            string str = "";
            string qry = "select * from Contact";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='widget'>" +
                           "<h5 class='widget-title'>Contact Us</h5>" +
                                "<div class='content-element3'>" +
                                    "<p class='content-element1'>" + row["Address"] + "</p>" +
                                   " <p>" +
                                        "Mobile: " + row["Phone"] + " <br/>" +
                                       " E-mail: <a href ='#' class='link-text'>" + row["Email"] + "</a>" +
                                    " </p> " +
                                "</div>" +

                                "<!--<div class='brend-box'>" +
                                    "<a href = '#' >< img src='images/brend-logo1.png' alt=''></a>" +
                                    "<a href = '#' >< img src='images/brend-logo2.png' alt=''></a>" +
                                    "<a href = '#' >< img src='images/brend-logo3.png' alt=''></a>" +
                                "</div>--> " +
                             "</div>";
            }
            ltrftr.Text = str;
        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindContact()
    {
        try
        {
            string str = "";
            string qry = "select * from Contact";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='item-info'>" +
                                    "   <h4>" + row["Phone"] + "</h4> " +
                                        "</div>";
            }
            ltrcontactno.Text = str;
            ltrcontactno1.Text = str;
        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }
    }

    private void BindHeaderAddress()
    {
        try
        {

            string str = "";
            string qry = "select * from contact";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='pre-header'>" +
                       "  <div class='container'>" +
                       "  <div class='flex-row flex-justify'>" +
                       "    <ul class='contact-info'>" +
                       "   <li><i class='licon-clock3'></i>Open 8am-5pm: Monday - Saturday</li>" +
                       "     <li><i class='licon-map-marker'></i>" + row["Address"] + "</li>" +
                       "    </ul>" +

                       "    <ul class='social-icons'>" +
                       "     <li><a href='#'><i class='icon-facebook'></i></a></li>" +
                       "    <li><a href='#'><i class='icon-twitter'></i></a></li>" +
                       "   <li><a href='#'><i class='icon-gplus-3'></i></a></li>" +
                       "      <li><a href='#'><i class='icon-linkedin-3'></i></a></li>" +
                       "     <li><a href='#'><i class='icon-youtube-play'></i></a></li>" +
                       "       <li><a href='#'><i class='icon-instagram-5'></i></a></li>" +
                       "  </ul>" +
                    "</div>" +
                "</div>" +
            "</div>";
            }

            ltraddress.Text = str;
        }
        catch (Exception e)
        {
            Response.Write("Failed");
        }

    }


}
