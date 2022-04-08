using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml;
public partial class Viewproductdetail : System.Web.UI.Page
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["Id"].ToString() != null)
        {
            if (!IsPostBack)
            {
                BindProduct();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void BindProduct()
    {
        try
        {
            string Id = Request.QueryString["Id"].ToString();
            string str = "";
            string st = "";
            string st1 = "";
            string qry1 = "select * from ProductMaster where ID=" + Id;
            DataTable dt1 = cc.GetDataTable(qry1);
            if (dt1.Rows.Count > 0)
            {
                st += "<div class='content-element4'>" +
                    "<h2  style='color:white;'>" + dt1.Rows[0]["Name"] + "</h2>" +
                    "<h5  style='color:white;margin-t'>" + dt1.Rows[0]["Feature"] + "</h5>" +
                  "</div>";
            }
            ltrtop.Text = st;
            if (dt1.Rows.Count > 0)
            {
                str += "<div >" +
                    "<div class='col-md-8' style='margin-top:66px;'>" +
                      "<p style='font-family:verdana;'>" + dt1.Rows[0]["Description"] + "</P>" +
                     "</div>" +
                  "<div class='col-md-4' style='margin-top:66px;'>" +
                   "<img src='" + dt1.Rows[0]["Image"] + "'  alt=''/>" +
                     "</div>" +
                     "</div>";
            }
            ltrmid.Text = str;

            string qry = "select distinct pm.*,pc.Name as Productname from productmaster pm,productcategorymaster pc where pm.Category=pc.ID and pm.category=" + Id;
            DataTable dt = cc.GetDataTable(qry);
            if (dt.Rows.Count > 0)
            {
                string s2 = dt.Rows[0]["Availibility"].ToString();
                string s3 = dt.Rows[0]["Color"].ToString();
                if (s2 != "" && s3 != "")
                {
                    st1 += "<div><b style='font-size:15px;'>Product category: </b><span style='margin-left:9px;margin-right:9px;font-size:15px;'>" + dt.Rows[0]["Productname"] + "</span>><span  style='margin-left:9px;font-size:15px;'>" + dt.Rows[0]["Name"] + "</span></div>" +
                    "<div style='font-size:15px;'><b>Available in (kW):  </b><span style='margin-left:12px;font-size:15px;'>" + s2 + "</span></div>" +
                    "<div style='font-size:15px;'><b>Color:   </b><span style='margin-left:89px;font-size:15px;'>" + s3 + "</span></div>";
                }
                else if (s2 != "")
                {
                    st1 += "<div><b style='font-size:15px;'>Product category: </b><span style='margin-left:9px;margin-right:9px;font-size:15px;'>" + dt.Rows[0]["Productname"] + "</span>><span  style='margin-left:9px;font-size:15px;'>" + dt.Rows[0]["Name"] + "</span></div>" +
                       "<div style='font-size:15px;'><b>Available in (kW):  </b><span style='margin-left:12px;font-size:15px;'>" + s2 + "</span></div>";
                }
                else if (s3 != "")
                {
                    st1 += "<div><b style='font-size:15px;'>Product category: </b><span style='margin-left:9px;margin-right:9px;font-size:15px;'>" + dt.Rows[0]["Productname"] + "</span>><span  style='margin-left:9px;font-size:15px;'>" + dt.Rows[0]["Name"] + "</span></div>" +
                       "<div style='font-size:15px;'><b>Color:   </b><span style='margin-left:89px;font-size:15px;'>" + s3 + "</span></div>";
                }
            }
            ltrmidin.Text = st1;

            string st2 = "";
            string qry2 = "select * from productfeaturemaster where name=" + Id;
            DataTable dt2 = cc.GetDataTable(qry2);
            string data = dt2.Rows[0]["Title"].ToString();
            if (data != "")
            {
                ltrshow.Visible = true;
                foreach (DataRow row in dt2.Rows)
                {
                    st2 += "<div class='item-carousel' style='background-color:#eaecef;'>" +
                                            "<b style='font-size:12px;magin-left:10px;margin-top:15px;'>" + row["Title"] + "</b>" +
                                             "<hr style='border: 0.5px solid #595b5e;width: 100%;'>" +
                                            "<img src=" + row["Image"] + " alt='' style='height:100x;width:100px;margin:0 auto;'/>" +
                                            "<p style='font-size:13px;font-family:verdana;'>" + row["Description"] + "</p>" +
                                            " </div>";

                } ltrfea.Text = st2;
            }

            string Specification = dt1.Rows[0]["Specification"].ToString();
            string Brouchure = dt1.Rows[0]["Brouchure"].ToString();
            string st3 = "";
            if (Specification != "" && Brouchure != "")
            {
                st3 += "<div style='margin-bottom: 18px;height: 42px; width: 160px; background-color: #009acf;margin-left: 10px;text-align:center;padding-top:9px;'>" +
                                "<a target='_blank' href='" + dt1.Rows[0]["Specification"] + "'>" +
                                "<b style='color: white;'>Product Specification</b></a>" +
                                "</div>" +
                              "<span >" +
                              "<div style='margin-bottom: 18px;height: 42px; width: 160px; background-color: #009acf;margin-left: 10px;text-align:center;padding-top:9px;'>" +
                                  "<a target='_blank' href='" + dt1.Rows[0]["Brouchure"] + "'>" +
                                "<b style='color: white;'>Download Brochure</b></a>" +
                                "</div>" +
                             " </span>";

            }
            else if (Specification != "")
            {
                st3 += "<div style='margin-bottom: 18px;height: 42px; width: 160px; background-color: #009acf;margin-left: 10px;text-align:center;padding-top:9px;'>" +
                                "<a target='_blank' href='" + dt1.Rows[0]["Specification"] + "'>" +
                                "<b style='color: white;'>Product Specification</b></a>" +
                                "</div>";
            }
            else if (Brouchure != "")
            {

                st3 += "<div style='margin-bottom: 18px;height: 42px; width: 160px; background-color: #009acf;margin-left: 10px;text-align:center;padding-top:9px;'>" +
                       "<a target='_blank' href='" + dt1.Rows[0]["Brouchure"] + "'>" +
                                "<b style='color: white;'>Download Brochure</b></a>" +
                                "</div>";
            }
            ltrdonloadlink.Text = st3;
        }
        catch (Exception e1)
        {

        }
    }
}