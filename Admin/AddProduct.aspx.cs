using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddProduct : System.Web.UI.Page
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
            Bindtype();
        }
    }
    protected void BindData()
    {
        try
        {
            DataTable dt = D.GetDataTable("select distinct p.*,c.Name as PCategory,t.Name as PType from ProductMaster p,ProductCategoryMaster c,ProductType t where p.Category=c.ID  and p.Type=t.ID");
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
        string img1 = "";
        string img2 = "";
        if (file.HasFile && file1.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;
            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;
            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Image,Specification,Brouchure,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img + "','" + img1 + "','" + img2 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";
        }
        else if (file.HasFile && file1.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;
            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Image,Specification,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img + "','" + img1 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

        }
        else if (file.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Image,Brouchure,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img + "','" + img2 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

        }
        else if (file1.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;

            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Specification,Brouchure,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img1 + "','" + img2 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

        }
        else if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            qry = "insert into ProductMaster (Category,Type,Feature,Name,Image,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

        }
        else if (file1.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();

            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Specification,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img1 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

        }
        else if (file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "insert into ProductMaster (Category,Type,Feature,Name,Brouchure,ShortDescription,Description,Rating,Color,Availibility) values('" + ddlProductCategory.SelectedValue + "','" + ddlproducttype.SelectedValue + "','" + txtfea.Text + "','" + txtname.Text + "','" + img2 + "','" + txtshortdes.Text + "','" + txtdesc.Text + "','" + txtrat.Text + "','" + txtclr.Text + "','" + txtavail.Text + "')";

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

        txtfea.Text = "";
        txtdesc.Text = "";
        txtname.Text = "";
        txtshortdes.Text = "";
        txtdesc.Text = "";
        txtrat.Text = "";
        txtclr.Text = "";
        txtavail.Text = "";
        ddlProductCategory.SelectedValue = "0";
        ddlproducttype.SelectedValue = "0";
    }

    protected void lbUpdate_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        string img = "";
        string img1 = "";
        string img2 = "";
        string qry = "";
        cmd.Parameters.AddWithValue("@ID", lbSubmit.CommandArgument);
        if (file.HasFile && file1.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;

            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Image='" + img + "',Specification='" + img1 + "',Brouchure='" + img2 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;
        }
        else if (file.HasFile && file1.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Image='" + img + "',Specification='" + img1 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else if (file.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Image='" + img + "',Brouchure='" + img2 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else if (file1.HasFile && file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;

            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Specification='" + img1 + "',Brouchure='" + img2 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else if (file.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext = System.IO.Path.GetExtension(file.FileName);
            file.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductImage/"), no + ext));
            img = "../Resources/ProductImage/" + no + ext;

            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Image='" + img + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else if (file1.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();

            string ext1 = System.IO.Path.GetExtension(file1.FileName);
            file1.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductSpecification/"), no + ext1));
            img1 = "../Resources/ProductSpecification/" + no + ext1;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Specification='" + img1 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else if (file2.HasFile)
        {
            Random rd = new Random();
            int no = rd.Next();
            string ext2 = System.IO.Path.GetExtension(file2.FileName);
            file2.SaveAs(System.IO.Path.Combine(Server.MapPath("../Resources/ProductBrouchure/"), no + ext2));
            img2 = "../Resources/ProductBrouchure/" + no + ext2;
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',Brouchure='" + img2 + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;

        }
        else
        {
            qry = "Update ProductMaster set Category='" + ddlProductCategory.SelectedValue + "',Type='" + ddlproducttype.SelectedValue + "',Feature='" + txtfea.Text + "',Name='" + txtname.Text + "',ShortDescription='" + txtshortdes.Text + "',Description='" + txtdesc.Text + "',Rating='" + txtrat.Text + "',Color='" + txtclr.Text + "',Availibility='" + txtavail.Text + "' where ID=" + lbSubmit.CommandArgument;
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
        DataTable dt = D.GetData("select * from ProductMaster where ID=" + lb.CommandArgument);
        if (dt.Rows.Count > 0)
        {

            lbSubmit.Visible = false;
            lbUpdate.Visible = true;
            IbCancel.Visible = true;

            lbSubmit.CommandArgument = dt.Rows[0]["ID"].ToString();
            ddlProductCategory.Text = dt.Rows[0]["Category"].ToString();
            ddlproducttype.Text = dt.Rows[0]["Type"].ToString();
            txtfea.Text = dt.Rows[0]["Feature"].ToString();
            txtname.Text = dt.Rows[0]["Name"].ToString();
            txtshortdes.Text = dt.Rows[0]["ShortDescription"].ToString();
            txtdesc.Text = dt.Rows[0]["Description"].ToString();
            txtclr.Text = dt.Rows[0]["Color"].ToString();
            txtrat.Text = dt.Rows[0]["Rating"].ToString();
            txtavail.Text = dt.Rows[0]["Availibility"].ToString();
            BindData();
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            int id = Convert.ToInt32(lb.CommandArgument.ToString());
            D.ExecuteQuery("delete from ProductMaster where ID=" + id);
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
        DataTable dt = D.GetDataTable("select * from ProductCategoryMaster");
        if (dt.Rows.Count > 0)
        {
            ddlProductCategory.DataTextField = "Name";
            ddlProductCategory.DataValueField = "ID";
            ddlProductCategory.DataSource = dt;
            ddlProductCategory.DataBind();
        }
        ddlProductCategory.Items.Insert(0, new ListItem("Select Category", "0"));
    }
    public void Bindtype()
    {
        DataTable dt = D.GetDataTable("select * from ProductType");
        if (dt.Rows.Count > 0)
        {
            ddlproducttype.DataTextField = "Name";
            ddlproducttype.DataValueField = "ID";
            ddlproducttype.DataSource = dt;
            ddlproducttype.DataBind();
        }
        ddlproducttype.Items.Insert(0, new ListItem("Select Type", "0"));
    }

    protected void IbInfo_Click(object sender, EventArgs e)
    {
        LinkButton lbs = (LinkButton)sender;
        string ids = lbs.CommandArgument;
        Response.Redirect("ViewProductdetail.aspx?Id=" + ids);
    }
}