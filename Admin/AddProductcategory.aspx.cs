using Common.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddProductcategory : System.Web.UI.Page
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
            DataTable dt = D.GetDataTable("select * from ProductCategoryMaster");
       
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
        txtname.Text = "";
        status.Checked = false;
        txtdes.Text = "";
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
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/Productcategory/"), no + ext));

            img = "../Resources/Productcategory/" + no + ext;
            if (status.Checked == true)
            {
                qry = "insert into ProductCategoryMaster (Name,Image,Status,Description) values('" + txtname.Text + "','" + img + "','" + st + "','" + txtdes.Text + "')";
            }
            else
            {
                qry = "insert into ProductCategoryMaster (Name,Image,Status,Description) values('" + txtname.Text + "','" + img + "','" + st1 + "','" + txtdes.Text + "')";

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
    protected void lbUpdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string img = "";
        string qry = "";
        int st = 1;
        int st1 = 0;
        cmd.Parameters.AddWithValue("@ID", lbSubmit.CommandArgument);
        if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/Productcategory/"), no + ext));
            img = "../Resources/Productcategory/" + no + ext;

            if (status.Checked == true)
            {
                qry = "Update ProductCategoryMaster set Name='" + txtname.Text + "',Image='" + img + "',Status='" + st + "',Description='" + txtdes.Text + "' where ID=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update ProductCategoryMaster set Name='" + txtname.Text + "',Image='" + img + "',Status='" + st1 + "',Description='" + txtdes.Text + "' where ID=" + lbSubmit.CommandArgument;
            }
        }
        else
        {
            if (status.Checked == true)
            {
                qry = "Update ProductCategoryMaster set Name='" + txtname.Text + "',Status='" + st + "',Description='" + txtdes.Text + "' where ID=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update ProductCategoryMaster set Name='" + txtname.Text + "',Status='" + st1 + "',Description='" + txtdes.Text + "' where ID=" + lbSubmit.CommandArgument;
            }
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
        DataTable dt = D.GetData("select * from ProductCategoryMaster where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            txtname.Text = dt.Rows[0]["Name"].ToString();
            txtdes.Text = dt.Rows[0]["Description"].ToString();

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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from ProductCategoryMaster where ID=" + id);

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