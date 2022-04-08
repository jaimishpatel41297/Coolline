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

public partial class AboutUs : System.Web.UI.Page
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPrivacy();
        }
    }

    private void BindPrivacy()
    {
        try
        {
            string str = "";
            string qry = "select * from Privacy";
            DataTable dt = cc.GetDataTable(qry);

            foreach (DataRow row in dt.Rows)
            {
                str += "<div class='item - box'>" +
                        "<h5 class='icons-box-title'><a href = '#' >" + row["Privacy"] + " </a></h5>" +
                      "<p>" + row["Disclaimer"] + "</p>" +
                    "</div>";

            }
            ltrprivacy.Text = str;
        }
        catch (Exception e)
        {
            Response.Write(" ");
        }
    }
}