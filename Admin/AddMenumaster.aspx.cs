using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddMenumaster : System.Web.UI.Page
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
            DataTable dt = D.GetDataTable("select * from MenuMaster");
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

            qry = "insert into MenuMaster (Name,Url,[Content],ParentId,Sequence,Type,IsShown,ID,PageTitle,PageDescription,PageKeyword) values('" + txtname.Text + "','" + txturl.Text + "','" + txtcon.Text + "','" + txtpid.Text + "','" + txtseq.Text + "','" + txttype.Text + "','" + st + "','" + txtid.Text + "','" + txttit.Text + "','" + txtdes.Text + "','" + txtkey.Text + "')";
        }
        else
        {
            qry = "insert into MenuMaster (Name,Url,[Content],ParentId,Sequence,Type,IsShown,ID,PageTitle,PageDescription,PageKeyword) values('" + txtname.Text + "','" + txturl.Text + "','" + txtcon.Text + "','" + txtpid.Text + "','" + txtseq.Text + "','" + txttype.Text + "','" + st1 + "','" + txtid.Text + "','" + txttit.Text + "','" + txtdes.Text + "','" + txtkey.Text + "')";
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
        txtname.Text = "";
        txturl.Text = "";
        txtcon.Text = "";
        txtpid.Text = "";
        txtseq.Text = "";
        txttype.Text = "";
        txtid.Text = "";
        txttit.Text = "";
        txtdes.Text = "";
        txtkey.Text = "";
        status.Checked = false;
    }
    protected void lbUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void IbCancel_Click(object sender, EventArgs e)
    {

    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {

    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {

    }
}