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

public partial class Product : System.Web.UI.Page
{
    Connection cc = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Id = Request.QueryString["Id"].ToString();
        if (Id != null)
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
            string qry1 = "select * from productcategorymaster where id=" + Id;
            DataTable dt1 = cc.GetDataTable(qry1);
            if (dt1.Rows.Count > 0)
            {
                st += "<div class='content-element4'>" +
                    "<h2 class='section-title'>" + dt1.Rows[0]["Name"] + "</h2>" +
                     "<div class='col-md-12'>" +
                     "<div class='col-md-8'>" +
                      "<p style='margin-left:-2%;'>" + dt1.Rows[0]["Description"] + "</P>" +
                     "</div>" +
                  "<div class='col-md-4'>" +
                   "<img src='" + dt1.Rows[0]["Image"] + "'  alt=''/>" +
                     "</div>" +
                     "</div>" +
                     "</div>" +
                     "<div class='c1'></div>";
            }
            productheader.Text = st;

            string qry = "select distinct pm.*,pt.Name as Producttype from productmaster pm,ProductType pt where pm.Type=pt.ID and category=" + Id;
            DataTable dt = cc.GetDataTable(qry);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s1 = dt.Rows[i]["Rating"].ToString();
                    string s2 = dt.Rows[i]["Availibility"].ToString();
                    string s3 = dt.Rows[i]["Color"].ToString();
                    string id = dt.Rows[i]["ID"].ToString();
                    if (s1 != "" && s2 != "" && s3 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                 "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                   "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                "</a>" +
                                    "<div class='entry'>" +
                                             "<div class='thumbnail-attachment'>" +
                                                "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                "</a>" +
                                             "</div>" +
                                             "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                             "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Star Rating:   </b><span style='margin-left:79px;;'>" + s1 + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Available in (kW):   </b><span style='margin-left:39px;'>" + s2 + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Color:   </b><span style='margin-left:122px;;'>" + s3 + "</span></div>" +
                                       "</div>" +
                                        "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                "</div>" +
                            "</div>";
                    }
                    else if (s1 != "" && s2 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                  "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                 "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                    "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                 "</a>" +
                                     "<div class='entry'>" +
                                              "<div class='thumbnail-attachment'>" +
                                                 "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                 "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                 "</a>" +
                                              "</div>" +
                                              "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                              "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                              "<div style='margin-top:1px;'><b>Star Rating:   </b><span style='margin-left:79px;;'>" + s1 + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Available in (kW):   </b><span style='margin-left:39px;'>" + s2 + "</span></div>" +
                                             "</div>" +
                                               "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                 "</div>" +
                             "</div>";
                    }
                    else if (s1 != "" && s3 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                 "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                   "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                "</a>" +
                                    "<div class='entry'>" +
                                             "<div class='thumbnail-attachment'>" +
                                                "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                "</a>" +
                                             "</div>" +
                                             "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                             "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Star Rating:   </b><span style='margin-left:79px;;'>" + s1 + "</span></div>" +
                                             "<div style='margin-top:1px;'><b>Color:   </b><span style='margin-left:122px;;'>" + s3 + "</span></div>" +
                                       "</div>" +
                                         "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                "</div>" +
                            "</div>";
                    }
                    else if (s2 != "" && s3 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                  "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                 "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                    "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                 "</a>" +
                                     "<div class='entry'>" +
                                              "<div class='thumbnail-attachment'>" +
                                                 "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                 "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                 "</a>" +
                                              "</div>" +
                                              "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                               "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                               "<div style='margin-top:1px;'><b>Available in (kW):   </b><span style='margin-left:39px;'>" + s2 + "</span></div>" +
                                               "<div style='margin-top:1px;'><b>Color:   </b><span style='margin-left:122px;;'>" + s3 + "</span></div>" +
                                        "</div>" +
                                          "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                 "</div>" +
                             "</div>";
                    }
                    else if (s1 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                 "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                   "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                "</a>" +
                                    "<div class='entry'>" +
                                             "<div class='thumbnail-attachment'>" +
                                                "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                "</a>" +
                                             "</div>" +
                                             "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                             "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                            "<div style='margin-top:1px;'><b>Star Rating:   </b><span style='margin-left:79px;;'>" + s1 + "</span></div>" +
                                          "</div>" +
                                            "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                "</div>" +
                            "</div>";
                    }
                    else if (s2 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                  "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                 "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                    "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                 "</a>" +
                                     "<div class='entry'>" +
                                              "<div class='thumbnail-attachment'>" +
                                                 "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                 "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                 "</a>" +
                                              "</div>" +
                                              "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                                 "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                              "<div style='margin-top:1px;'><b>Available in (kW):   </b><span style='margin-left:39px;'>" + s2 + "</span></div>" +
                                        "</div>" +
                                          "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                 "</div>" +
                             "</div>";
                    }
                    else if (s3 != "")
                    {
                        str += "<div class='col-md-12'>" +
                                  "<div class='col-md-4' style='margin-bottom:24px;'>" +
                                 "<a href='Viewproductdetail.aspx?Id=" + id + "''>" +
                                    "<h4 class='entry-title'>" + dt.Rows[i]["Name"] + "<span><img src='./images/123.jpg' style='height:4%;width:4.5%;margin-left:3%;'></span></h4>" +
                                 "</a>" +
                                     "<div class='entry'>" +
                                              "<div class='thumbnail-attachment'>" +
                                                 "<a href = " + dt.Rows[i]["Image"] + ">" +
                                                 "<img src='" + dt.Rows[i]["Image"] + "' alt=''/>" +
                                                 "</a>" +
                                              "</div>" +
                                              "<div style='margin-top:15px;'>" + dt.Rows[i]["ShortDescription"] + "</div>" +
                                              "<div style='margin-top:1px;'><b>Type:   </b><span style='margin-left:126px;'>" + dt.Rows[i]["Producttype"] + "</span></div>" +
                                               "<div style='margin-top:1px;'><b>Color:   </b><span style='margin-left:122px;;'>" + s3 + "</span></div>" +
                                        "</div>" +
                                         "<a href='Viewproductdetail.aspx?Id=" + id + "''><div style='margin:auto;height:30px;width:100px;padding-top:3px;background-color:#009acf;color:white;text-align:center;'>Know More</div></a>" +
                                 "</div>" +
                             "</div>";
                    }
                    //str += "<li class='nv-instafeed-item'><a class='fancybox nv-lightbox' data-fancybox='instagram' href='" + row["Image"] + "' title=''>" +
                    //        "<img src='" + row["Image"] + "'/></a>" +
                    //        "<div class='description-inner'>" +
                    //            "<h4 class='project-title'><a href='#'> " + row["Name"] + " </a></h4>" +
                    //            "<ul class='project-cats'>" +
                    //            "<li><a href='#'>" + row["Feature"] + "</a></li>" +
                    //            "</ul>" +  
                    //            "<a href='" + row["Brouchure"] + "' class='info-btn'>Product Detail</a>" +
                    //            "</div>" +
                    //        "</li>";

                }
                ltrproduct.Text = str;
            }
        }
        catch (Exception e)
        {

        }
    }
}