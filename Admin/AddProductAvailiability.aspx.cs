using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddProductAvailiability : System.Web.UI.Page
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
            Bindname();
        }
    }

    private void Bindname()
    {
        DataTable dt = D.GetDataTable("select * from ProductMaster");
        if (dt.Rows.Count > 0)
        {
            ddlname.DataTextField = "Name";
            ddlname.DataValueField = "ID";
            ddlname.DataSource = dt;
            ddlname.DataBind();
            ddlname.Items.Insert(0, new ListItem("Select Name", "0"));
        }

    }
    protected void BindData()
    {
        try
        {
            DataTable dt = D.GetDataTable("select distinct p.*,pm.Name as PAvail from ProductAvailability p,ProductMaster pm where p.Name=pm.ID  ");
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
            qry = "insert into ProductAvailability (Name,Availability,Status) values('" + ddlname.SelectedValue + "','" + txtavail.Text + "','" + st + "')";
        }
        else
        {
            qry = "insert into ProductAvailability (Name,Availability,Status) values('" + ddlname.SelectedValue + "','" + txtavail.Text + "','" + st1 + "')";
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
        txtavail.Text = "";
        ddlname.SelectedValue = "0";
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
            qry = "Update ProductAvailability set Name='" + ddlname.SelectedValue + "',Availability='" + txtavail.Text + "',Status='" + st + "' where ID=" + lbSubmit.CommandArgument;
        }
        else
        {
            qry = "Update ProductAvailability set Name='" + ddlname.SelectedValue + "',Availability='" + txtavail.Text + "',Status='" + st1 + "' where ID=" + lbSubmit.CommandArgument;
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
        DataTable dt = D.GetData("select * from ProductAvailability where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            ddlname.SelectedValue = dt.Rows[0]["Name"].ToString();
            txtavail.Text = dt.Rows[0]["Availability"].ToString();
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
            D.ExecuteQuery("delete from ProductAvailability where ID=" + id);
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