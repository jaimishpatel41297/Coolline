using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddLogomaster : System.Web.UI.Page
{
    Connection D = new Connection();
    SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Coolline"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            divSuccess.Visible = false;
            divError.Visible = false;
            BindData();
            Clear();
        }
    }
    protected void BindData()
    {
        try
        {
            DataTable dt = D.GetDataTable("select * from LogoMaster");
            if (dt.Rows.Count > 0)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            else
            {
                dt = null;
                gv.DataSource = dt;
                gv.DataBind();
            }
        }
        catch { }
    }
 
    public void Clear()
    {
        txttitle.Text = "";
        status.Checked = false;
    }
    protected void lbSubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string qry = "";
        string img = "";

        if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            int st = 1;
            int st1 = 0;
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/Logo/"), no + ext));

            img = "/Resources/Logo/" + no + ext;
            if (status.Checked == true)
            {
                qry = "insert into LogoMaster (Title,Image,Status) values('" + txttitle.Text + "','" + img + "','" + st + "')";
            }
            else
            {
                qry = "insert into LogoMaster (Title,Image,Status) values('" + txttitle.Text + "','" + img + "','" + st1 + "')";

            }
            cmd.Connection = cn;
            cmd.CommandText = qry;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            BindData();
            Clear();
            divSuccess.Visible = true;
            divError.Visible = false;

        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from LogoMaster where LogoId=" + id);

            divSuccess.Visible = true;
            divError.Visible = false;

            BindData();
       
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            divSuccess.Visible = false;

        }
    }
    protected void IbCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}