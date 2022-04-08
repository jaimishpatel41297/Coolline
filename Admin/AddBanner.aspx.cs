using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddAdvertisement : System.Web.UI.Page
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

        }
    }
    protected void lbSubmit_Click(Object Sender, EventArgs e)
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
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/Banner/"), no + ext));
            img = "../Resources/Banner/" + no + ext;
            if (status.Checked == true)
            {
                qry = "insert into Banner (Title,SubTitle,Image,Status) values('" + txttitle.Text + "','" + txtsub.Text + "','" + img + "','" + st + "')";
            }
            else
            {
                qry = "insert into Banner (Title,SubTitle,Image,Status) values('" + txttitle.Text + "','" + txtsub.Text + "','" + img + "','" + st1 + "')";

            }
            cmd.Connection = cn;
            cmd.CommandText = qry;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Clear();
            BindData();

            divSuccess.Visible = true;
            divError.Visible = false;

        }

    }

    private void Clear()
    {
        txtsub.Text = "";
        txttitle.Text = "";
        status.Checked = false;
    }

    protected void BindData()
    {
        try 
        {
            DataTable dt = D.GetDataTable("select * from Banner");
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
        catch 
        { }
    }
    protected void lbDelete_Click(Object Sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)Sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from Banner where ID=" + id);

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

}