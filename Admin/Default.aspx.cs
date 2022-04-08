using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Admin_Default : System.Web.UI.Page
{
    Connection D = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        DataTable dt = D.GetDataTable("select * from Admin_Login where Admin_name='" + txtUserName.Text.ToString() + "' and Admin_password='" + txtPassword.Text.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            Response.Cookies["Admin_id"].Value = txtUserName.Text.ToString();
            Session["Admin_id"] = "1";
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            txtPassword.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Suceess", "alert('Wrong Username or Password');", true);
        }
    }
}