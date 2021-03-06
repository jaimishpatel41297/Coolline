using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddCareer : System.Web.UI.Page
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
            DataTable dt = D.GetDataTable("select * from Career");
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
    private void Clear()
    {
        txtname.Text = "";
        txteml.Text = "";
        txtmob.Text = "";
        txtjob.Text = "";
       
    }

    protected void lbSubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string qry = "";
        string fl = "";

        if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/Resume/"), no + ext));
            fl = "/Resources/Resume/" + no + ext;

            qry = "insert into Career (Name,Email,MobileNo,JobFor,Resume) values('" + txtname.Text + "','" + txteml.Text + "','" + txtmob.Text + "','" + txtjob.Text + "','" + fl + "')";

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

    }
    protected void lbUpdate_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        string fl = "";
        string qry = "";

        cmd.Parameters.AddWithValue("@ID", lbSubmit.CommandArgument);
        if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/Resume/"), no + ext));
            fl = "/Resources/Resume/" + no + ext;

            qry = "Update Career set Name='" + txtname.Text + "',Email='" + txteml.Text + "',MobileNo='" + txtmob.Text + "',JobFor='" + txtjob.Text + "',Resume='" + fl + "' where Id=" + lbSubmit.CommandArgument;
          
        }
        else
        {
            qry = "Update Career set Name='" + txtname.Text + "',Email='" + txteml.Text + "',MobileNo='" + txtmob.Text + "',JobFor='" + txtjob.Text + "' where Id=" + lbSubmit.CommandArgument;
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
    protected void IbCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lbSubmit.Visible = true;
        lbUpdate.Visible = false;
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowpopupProduct()", true);
        LinkButton lbs = (LinkButton)sender;
        int ids = Convert.ToInt32(lbs.CommandArgument);
        lbSubmit.CommandArgument = ids.ToString();

        LinkButton lb = (LinkButton)sender;
        int id = Convert.ToInt32(lb.CommandArgument);
        DataTable dt = D.GetData("select * from Career where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            txtname.Text = dt.Rows[0]["Name"].ToString();
            txteml.Text = dt.Rows[0]["Email"].ToString();
            txtmob.Text = dt.Rows[0]["MobileNo"].ToString();
            txtjob.Text = dt.Rows[0]["JobFor"].ToString();
         
            BindData();
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
         try
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from Career where ID=" + id);
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
    
}