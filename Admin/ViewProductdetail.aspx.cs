using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_ViewProductdetail : System.Web.UI.Page
{
    Connection D = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Id = Request.QueryString["Id"].ToString();
        if (Id != null)
        {
            if (!IsPostBack)
            {
                Binddata();
            }
        }
        else
        {
            Response.Redirect("AddProduct.aspx");
        }

    }

    private void Binddata()
    {
        try
        {
            string Id = Request.QueryString["Id"].ToString();
            string qry = "";
            qry = "select * from productmaster where ID=" + Id;

            DataTable dt = D.GetDataTable(qry);
            if (dt.Rows.Count > 0)
            {
                ltrcolon.Text = dt.Rows[0]["ShortDescription"].ToString();
                ltrfrmst.Text = dt.Rows[0]["Description"].ToString();
                ltravail.Text = dt.Rows[0]["Availibility"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
}