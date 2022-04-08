using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddOurteam : System.Web.UI.Page
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
            DataTable dt = D.GetDataTable("select * from Ourteam");
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
        string img = "";
        int st = 1;
        int st1 = 0;
        if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/Photo/"), no + ext));
            img = "/Resources/Photo/" + no + ext;
            if (status.Checked == true)
            {
                qry = "insert into Ourteam (Name,Designation,Photo,Description,Facebook,Twitter,Google,Linkedin,Youtube,Status) values('" + txtname.Text + "','" + txtdes.Text + "','" + img + "','" + txtdesc.Text + "','" + txtfab.Text + "','" + txttwt.Text + "','" + txtglg.Text + "','" + txtlin.Text + "','" + txtyou.Text + "','" + st + "')";
            }
            else
            {
                qry = "insert into Ourteam (Name,Designation,Photo,Description,Facebook,Twitter,Google,Linkedin,Youtube,Status) values('" + txtname.Text + "','" + txtdes.Text + "','" + img + "','" + txtdesc.Text + "','" + txtfab.Text + "','" + txttwt.Text + "','" + txtglg.Text + "','" + txtlin.Text + "','" + txtyou.Text + "','" + st1 + "')";
            }
        }
        //else
        //    {
        //        if(status.Checked == true)
        //        {
        //              qry = "insert into Ourteam (Name,Designation,Description,Facebook,Twitter,Google,Linkedin,Youtube,Status) values('" + txtname.Text + "','" + txtdes.Text + "','" + txtdesc.Text + "','" + txtfab.Text + "','" + txttwt.Text + "','" + txtglg.Text + "','" + txtlin.Text + "','" + txtyou.Text + "','" + st + "')";
        //        }
        //        else{
        //             qry = "insert into Ourteam (Name,Designation,Description,Facebook,Twitter,Google,Linkedin,Youtube,Status) values('" + txtname.Text + "','" + txtdes.Text + "','" + txtdesc.Text + "','" + txtfab.Text + "','" + txttwt.Text + "','" + txtglg.Text + "','" + txtlin.Text + "','" + txtyou.Text + "','" + st1 + "')";
        //        }
        //    }
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
  txtname.Text = "";
        txtdes.Text = "";
        txtdesc.Text = "";
        txtfab.Text = "";
        txttwt.Text = "";
        txtglg.Text = "";
        txtlin.Text = "";
        txtyou.Text = "";
        status.Checked = false;
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
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/./Resources/Photo/"), no + ext));
            img = "/Resources/Photo/" + no + ext;
            if (status.Checked == true)
            {
                qry = "Update Ourteam set Name='" + txtname.Text + "',Designation='" + txtdes.Text + "',Description='" + txtdesc.Text + "',Photo='" + img + "',Facebook='" + txtfab.Text + "',Twitter='" + txttwt.Text + "',Google='" + txtglg.Text + "',Linkedin='" + txtlin.Text + "',Youtube='" + txtyou.Text + "',Status='" + st + "' where Id=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update Ourteam set Name='" + txtname.Text + "',Designation='" + txtdes.Text + "',Description='" + txtdesc.Text + "',Photo='" + img + "',Facebook='" + txtfab.Text + "',Twitter='" + txttwt.Text + "',Google='" + txtglg.Text + "',Linkedin='" + txtlin.Text + "',Youtube='" + txtyou.Text + "',Status='" + st1 + "' where Id=" + lbSubmit.CommandArgument;
            }
        }
        else
        {
            if (status.Checked == true)
            {
                qry = "Update Ourteam set Name='" + txtname.Text + "',Designation='" + txtdes.Text + "',Description='" + txtdesc.Text + "',Facebook='" + txtfab.Text + "',Twitter='" + txttwt.Text + "',Google='" + txtglg.Text + "',Linkedin='" + txtlin.Text + "',Youtube='" + txtyou.Text + "',Status='" + st + "' where Id=" + lbSubmit.CommandArgument;
            }
            else
            {
                qry = "Update Ourteam set Name='" + txtname.Text + "',Designation='" + txtdes.Text + "',Description='" + txtdesc.Text + "',Facebook='" + txtfab.Text + "',Twitter='" + txttwt.Text + "',Google='" + txtglg.Text + "',Linkedin='" + txtlin.Text + "',Youtube='" + txtyou.Text + "',Status='" + st1 + "' where Id=" + lbSubmit.CommandArgument;
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
        DataTable dt = D.GetData("select * from Ourteam where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            txtname.Text = dt.Rows[0]["Name"].ToString();
            txtdes.Text = dt.Rows[0]["Designation"].ToString();
            txtdesc.Text = dt.Rows[0]["Description"].ToString();
            txtfab.Text = dt.Rows[0]["Facebook"].ToString();
            txttwt.Text = dt.Rows[0]["Twitter"].ToString();
            txtglg.Text = dt.Rows[0]["Google"].ToString();
            txtlin.Text = dt.Rows[0]["Linkedin"].ToString();
            txtyou.Text = dt.Rows[0]["Youtube"].ToString();
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
            D.ExecuteQuery("delete from Ourteam where ID=" + id);
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