using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddContact : System.Web.UI.Page
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
    protected void BindData()
    {
        try
        {
            DataTable dt = D.GetDataTable("select * from Contact");
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
    protected void lbSubmit_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        string qry = "";
        int st = 1;
        int st1 = 0;

        if (status.Checked == true)
        {
            Random rd = new Random();
            int no = rd.Next();

            qry = "insert into Contact (Address,FullAddress,Longitude,Latitude,Status,Email,Phone) values('" + txtadd.Text + "','" + txtfulladd.Text + "','" + txtlong.Text + "','" + txtlat.Text + "','" + st + "','" + txtemail.Text + "','" + txtphn.Text + "')";
        }
        else
        {
            qry = "insert into Contact (Address,FullAddress,Longitude,Latitude,Status,Email,Phone) values('" + txtadd.Text + "','" + txtfulladd.Text + "','" + txtlong.Text + "','" + txtlat.Text + "','" + st1 + "','" + txtemail.Text + "','" + txtphn.Text + "')";
        }
            cmd.Connection = cn;
            cmd.CommandText = qry;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            BindData();
            divSuccess.Visible = true;
            divError.Visible = false;
            Clear();

      }
    private void Clear()
    {
        txtadd.Text = "";
        txtfulladd.Text = "";
        txtlong.Text = "";
        txtlat.Text = "";
        txtemail.Text = "";
        txtphn.Text = "";
        status.Checked = false;
    }

    protected void lbUpdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string qry = "";
        int st = 1;
        int st1 = 0;
        cmd.Parameters.AddWithValue("@ID", lbSubmit.CommandArgument);
        if (status.Checked == true)
        {
            qry = "Update Contact set Address='" + txtadd.Text + "',FullAddress='" + txtfulladd.Text + "',Longitude='" + txtlong.Text + "',Latitude='" + txtlat.Text + "',Phone='" + txtphn.Text + "',Email='" + txtemail.Text + "',Status='" + st + "' where Id=" + lbSubmit.CommandArgument;
        }
        else
        {
            qry = "Update Contact set Address='" + txtadd.Text + "',FullAddress='" + txtfulladd.Text + "',Longitude='" + txtlong.Text + "',Latitude='" + txtlat.Text + "',Phone='" + txtphn.Text + "',Email='" + txtemail.Text + "',Status='" + st1 + "' where Id=" + lbSubmit.CommandArgument;
        }

        cmd.Connection = cn;
        cmd.CommandText = qry;
        cn.Open();
        cmd.ExecuteNonQuery();
        cn.Close();
        BindData();

        divSuccess.Visible = true;
        divError.Visible = false;
        lbUpdate.Visible = false;
        lbSubmit.Visible = true;
        Clear();

    }
    
    protected void lbDelete_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from Contact where Id=" + id);

            divSuccess.Visible = true;
            divError.Visible = false;

            BindData();
            Clear();
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            divSuccess.Visible = false;
        }
    }


    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowpopupProduct()", true);
        LinkButton lbs = (LinkButton)sender;
        int ids = Convert.ToInt32(lbs.CommandArgument);
        lbSubmit.CommandArgument = ids.ToString();

        LinkButton lb = (LinkButton)sender;
        int id = Convert.ToInt32(lb.CommandArgument);
        DataTable dt = D.GetData("select * from Contact where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();

            txtadd.Text = dt.Rows[0]["Address"].ToString();
            txtfulladd.Text = dt.Rows[0]["FullAddress"].ToString();
            txtlong.Text = dt.Rows[0]["Longitude"].ToString();
            txtlat.Text = dt.Rows[0]["Latitude"].ToString();
            txtphn.Text = dt.Rows[0]["Phone"].ToString();
            txtemail.Text = dt.Rows[0]["Email"].ToString();
            string Status = dt.Rows[0]["Status"].ToString();
            if (Status == "True")
            {
                status.Checked = true;
            }
            else
            {
                status.Checked = false;
            }

            BindData();
        }
    }
    protected void IbCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lbSubmit.Visible = true;
        lbUpdate.Visible = false;
    }
}