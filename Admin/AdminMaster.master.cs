using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

public partial class cp_sms_AdminMaster : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["Admin_id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    
}
