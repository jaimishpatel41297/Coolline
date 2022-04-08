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


public partial class Faq : System.Web.UI.Page
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFaq();
        }
    }

    private void BindFaq()
    {
        try
        {
            string str = "";
            string qry = "select * from FaqMaster";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='accordion-item'>" +
                   "<h6 class='a-title'>Q. " + row["Title"] + "</h6>" +
                   "<div class='a-content'>" +
                    "<p>" + row["Description"] + "</p>" +
                    "<p>Donec porta diam eu massa. Quisque diam lorem, interdum vitae,dapibus ac, scelerisque vitae, pede. Donec eget tellus non erat lacinia fermentum. Donec in velit vel ipsum auctor pulvinar. Vestibulum iaculis lacinia est. Proin dictum elementum velit. Fusce euismod consequat ante. Lorem ipsum dolor sit amet, consectetuer adipis. Mauris accumsan nulla vel diam.</p>" +
                   "</div>" +
                "</div>";

            }
            ltrfaq.Text = str;
        }
        catch (Exception e)
        {
            Response.Write(" ");
        }
    }
}