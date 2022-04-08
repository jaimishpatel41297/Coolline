using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddOurProject : System.Web.UI.Page
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
            Binadcategory();
         
        }

    }
    protected void BindData()
    {
        try
        {
            DataTable dt = D.GetDataTable("select * from OurProjectMaster");
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
    protected void ddlProjectCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    private void Clear()
    {
        //ddlProjectCategory.SelectedValue = "";
        txtname.Text = "";
        txtshortdes.Text = "";
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
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/ProjectImage/"), no + ext));
            img = "/Resources/ProjectImage/" + no + ext;

            if (status.Checked == true)
            {
                qry = "insert into OurProjectMaster (Category,Name,Image,Description,Status) values('" + ddlProjectCategory.SelectedValue + "','" + txtname.Text + "','" + img + "','" + txtshortdes.Text + "','" + st + "')";
            }
            else
            {
                qry = "insert into OurProjectMaster (Category,Name,Image,Description,Status) values('" + ddlProjectCategory.SelectedValue + "','" + txtname.Text + "','" + img + "','" + txtshortdes.Text + "','" + st1 + "')";
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
    }
    protected void lbUpdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        string img = "";
        string qry = "";
        int st = 1;
        int st1 = 0;
        cmd.Parameters.AddWithValue("@ID", lbSubmit.CommandArgument);
        if (file.HasFile )
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/ProjectImage/"), no + ext));
            img = "/Resources/ProjectImage/" + no + ext;

           if (status.Checked == true)
            {
                qry = "Update OurProjectMaster set Category='" + ddlProjectCategory.SelectedValue + "',Name='" + txtname.Text + "',Image='" + img + "',Description='" + txtshortdes.Text + "',Status='" + st + "' where ID=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update OurProjectMaster set Category='" + ddlProjectCategory.SelectedValue + "',Name='" + txtname.Text + "',Image='" + img + "',Description='" + txtshortdes.Text + "',Status='" + st1 + "' where ID=" + lbSubmit.CommandArgument;
            }
        }

        else
        {
            if (status.Checked == true)
            {
                qry = "Update OurProjectMaster set Category='" + ddlProjectCategory.SelectedValue + "',Name='" + txtname.Text + "',Description='" + txtshortdes.Text + "',Status='" + st + "' where ID=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update OurProjectMaster set Category='" + ddlProjectCategory.SelectedValue + "',Name='" + txtname.Text + "',Description='" + txtshortdes.Text + "',Status='" + st + "' where ID=" + lbSubmit.CommandArgument;
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
        DataTable dt = D.GetData("select * from OurProjectMaster where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            ddlProjectCategory.Text = dt.Rows[0]["Category"].ToString();
            txtname.Text = dt.Rows[0]["Name"].ToString();
            txtshortdes.Text = dt.Rows[0]["Description"].ToString();
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
            D.ExecuteQuery("delete from OurProjectMaster where ID=" + id);
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
    public void Binadcategory()
    {
        DataTable dt = D.GetDataTable("select * from ProjectCategory");
        if (dt.Rows.Count > 0)
        {
            ddlProjectCategory.DataTextField = "Name";
            ddlProjectCategory.DataValueField = "ID";
            ddlProjectCategory.DataSource = dt;
            ddlProjectCategory.DataBind();
        }
        ddlProjectCategory.Items.Insert(0, new ListItem("Select Project Category", "0"));
    }
}