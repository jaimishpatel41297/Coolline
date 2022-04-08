using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{

    SqlConnection cnn = new SqlConnection("Data Source=103.242.119.138;Initial Catalog=InceptionLearning;uid=sa;pwd=tech@123;;");
    string cn = System.Configuration.ConfigurationManager.ConnectionStrings["ParentingWebsite"].ConnectionString;
    //SqlCommand cmd = new SqlCommand();
    GetData D = new GetData();
    Connection cc = new Connection();

    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void login(string type, string Username, string password)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<UserMaster> list = new List<UserMaster>();
        List<GeneralDataUser> g = new List<GeneralDataUser>();
        if (type == "login")
        {
            try
            {
                DataTable dt = D.GetDataTable("select * from member where Email='" + Username + "' and PASSWORD='" + password + "'");
                if (dt.Rows.Count > 0)
                {
                    UserMaster u = new UserMaster();
                    u.clientid = dt.Rows[i]["Id"].ToString();
                    u.name = dt.Rows[i]["Name"].ToString();
                    u.email = dt.Rows[i]["Email"].ToString();
                    u.dob = dt.Rows[i]["DOB"].ToString();
                    u.gender = dt.Rows[i]["Gender"].ToString();
                    u.mobile = dt.Rows[i]["Mobile"].ToString();
                    u.address = dt.Rows[i]["Address"].ToString();
                    u.city = dt.Rows[i]["City"].ToString();
                    u.pincode = dt.Rows[i]["Pincode"].ToString();
                    u.state = dt.Rows[i]["State"].ToString();
                    u.country = dt.Rows[i]["Country"].ToString();

                    DataTable dtQuizRunnerUp = D.GetDataTable("Select count(*) as QuizRunnerUp from LeagueMember where PriceType<>'Cash'");
                    DataTable dtQuizWon = D.GetDataTable("Select count(*) as QuizWon from LeagueMember where PriceType='Cash'");
                    DataTable dtQuizPlayed = D.GetDataTable("Select count(*) as QuizPlayed from LeagueMember");

                    if (dtQuizRunnerUp.Rows.Count > 0)
                    {
                        u.quizrunnerup = dtQuizRunnerUp.Rows[0]["QuizRunnerUp"].ToString();
                    }

                    if (dtQuizWon.Rows.Count > 0)
                    {
                        u.quizwon = dtQuizWon.Rows[0]["QuizWon"].ToString();
                    }

                    if (dtQuizPlayed.Rows.Count > 0)
                    {
                        u.quizplayed = dtQuizPlayed.Rows[0]["QuizPlayed"].ToString();
                    }

                    list.Add(u);

                    GeneralDataUser data = new GeneralDataUser();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataUser data = new GeneralDataUser();
                    data.MESSAGE = "Wrong Data !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataUser data = new GeneralDataUser();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void InsertFCMToken(string type, string fcmtoken, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "fcmtoken")
        {
            try
            {


                //fcmtoken = HttpUtility.UrlDecode(fcmtoken);

                cc.ExecuteQuery("Update ClientMaster set FcmToken = '" + fcmtoken + "' where Id = '" + userid + "'");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }



        }
    }

    [WebMethod]
    public void ForgotPassword(string type, string email)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        string res = "";
        if (type == "forgotpassword")
        {
            try
            {
                //Check user mobile exist or not
                string query = "select * from ClientMaster where email='" + email + "'";
                DataTable dt = cc.GetData(query);
                if (dt.Rows.Count != 0)
                {

                    string password = "";
                    //Check password empty or not.If password empty new generate and send to user
                    if (dt.Rows[0]["Password"] == "")
                    {

                        Random rng = new Random();
                        // Assume there'd be more logic here really
                        int random_password = rng.Next(1000, 9999);
                        password = random_password.ToString();
                        query = "update ClientMaster set Password='" + password + "' where email='" + email + "'";
                        cc.ExecuteQuery(query);

                    }
                    else
                    {

                        password = dt.Rows[0]["Password"].ToString();




                    }





                    SendSms sms = new SendSms();
                    res = sms.sendotp("Your  I learn app  password is :  " + password, dt.Rows[0]["Mobile"].ToString());
                    if (res.Equals("ok"))
                    {
                        GeneralData data = new GeneralData();
                        data.MESSAGE = "Successfully !";
                        data.ORIGINAL_ERROR = "";
                        data.ERROR_STATUS = false;
                        data.RECORDS = false;

                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                    else
                    {
                        GeneralData data = new GeneralData();
                        data.MESSAGE = "Failed";
                        data.ORIGINAL_ERROR = res;
                        data.ERROR_STATUS = true;
                        data.RECORDS = false;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }


                }
                else
                {

                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Email id not exist";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = true;
                    data.RECORDS = false;

                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }



            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }



    [WebMethod]
    public void VerificationService(string type, string code, string mobile)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        string res = "";
        if (type == "verification")
        {
            try
            {
                SendSms sms = new SendSms();
                res = sms.sendotp("you Verification Code is " + code, mobile);

                if (res.Equals("ok"))
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;

                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Failed";
                    data.ORIGINAL_ERROR = res;
                    data.ERROR_STATUS = true;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void VerificationStatus(string type, string mobile)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "status")
        {
            try
            {
                //fcmtoken = HttpUtility.UrlDecode(fcmtoken);
                cc.ExecuteQuery("Update ClientMaster set VerificationStatus = '1' where Mobile = '" + mobile + "'");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod(Description = "Get States Data")]
    public void GetStatesData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataState> g = new List<GeneralDataState>();
        if (action == "state")
        {
            int i = 0;
            DataTable dt = cc.GetData("select * from StateMaster");
            List<StateMaster> list = new List<StateMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    StateMaster state = new StateMaster();
                    state.stateid = dt.Rows[i]["stateid"].ToString();
                    state.name = dt.Rows[i]["name"].ToString();
                    state.type = dt.Rows[i]["type"].ToString();
                    list.Add(state);
                }
            }
            GeneralDataState data = new GeneralDataState();
            data.MESSAGE = "Successfull";
            data.ORIGINAL_ERROR = "";
            data.ERROR_STATUS = false;
            data.RECORDS = true;
            data.Data = list;
            g.Add(data);
            Context.Response.Write(js.Serialize(g[0]));
        }
    }

    [WebMethod(Description = "Get City Data By States")]
    public void GetCityData(string action, string stateid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataCity> g = new List<GeneralDataCity>();
        if (action == "city")
        {
            int i = 0;
            int sid = Convert.ToInt32(stateid);
            DataTable dt = cc.GetData("select * from CityMaster where stateid = " + stateid);
            List<CityMaster> list = new List<CityMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    CityMaster city = new CityMaster();
                    city.id = dt.Rows[i]["ID"].ToString();
                    city.city = dt.Rows[i]["city"].ToString();
                    city.stateid = dt.Rows[i]["stateid"].ToString();
                    list.Add(city);
                }
                GeneralDataCity data = new GeneralDataCity();
                data.MESSAGE = "Successfull";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;
                data.Data = list;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));

            }
            else
            {
                GeneralDataCity data = new GeneralDataCity();
                data.MESSAGE = "Recored Not Found";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void InsertClient(string type, string name, string email, string password, string mobile, string stateid, string cityid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();

        List<GeneralData> g = new List<GeneralData>();

        try
        {
            DataTable dt = cc.GetData("select * from ClientMaster where Mobile = '" + mobile + "'");
            if (dt.Rows.Count == 0)
            {
                DataTable dt2 = cc.GetData("select * from ClientMaster where Email = '" + email + "'");
                if (dt2.Rows.Count == 0)
                {
                    name = HttpUtility.UrlDecode(name);
                    email = HttpUtility.UrlDecode(email);
                    password = HttpUtility.UrlDecode(password);

                    cc.ExecuteQuery("insert into ClientMaster (Name,Email,Password,Mobile,StateId,CityId,VerificationStatus,logintype) values('" + name + "','" + email + "','" + password + "','" + mobile + "','" + stateid + "','" + cityid + "','0','0')");
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;

                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Email Already Exists ";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = true;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            else
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Mobile No Already Exists";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }


        }
        catch (Exception ex)
        {
            GeneralData data = new GeneralData();
            data.MESSAGE = "Failed";
            data.ORIGINAL_ERROR = ex.Message;
            data.ERROR_STATUS = true;
            data.RECORDS = false;
            g.Add(data);
            Context.Response.Write(js.Serialize(g[0]));
        }

    }

    [WebMethod]
    public void InsertSchool(string type, string schoolname, string address, string email, string password, string principlename, string mobile, string stateid, string cityid, string noofstudent)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "school")
        {

            try
            {
                DataTable dt = cc.GetData("select * from SchoolMaster where Mobile = '" + mobile + "'");
                if (dt.Rows.Count == 0)
                {
                    DataTable dt2 = cc.GetData("select * from SchoolMaster where Email = '" + email + "'");
                    if (dt2.Rows.Count == 0)
                    {
                        schoolname = HttpUtility.UrlDecode(schoolname);
                        address = HttpUtility.UrlDecode(address);
                        principlename = HttpUtility.UrlDecode(principlename);
                        email = HttpUtility.UrlDecode(email);
                        password = HttpUtility.UrlDecode(password);

                        cc.ExecuteQuery("insert into SchoolMaster (SchoolName,Address,Email,Password,StateId,CityId,PrincipleName,Mobile,NoOfStudent,VerificationStatus) values('" + schoolname + "','" + address + "','" + email + "','" + password + "','" + stateid + "','" + cityid + "','" + principlename + "','" + mobile + "','" + noofstudent + "','0')");
                        GeneralData data = new GeneralData();
                        data.MESSAGE = "Successfully !";
                        data.ORIGINAL_ERROR = "";
                        data.ERROR_STATUS = false;
                        data.RECORDS = true;

                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                    else
                    {
                        GeneralData data = new GeneralData();
                        data.MESSAGE = "Email Already Exists";
                        data.ORIGINAL_ERROR = "";
                        data.ERROR_STATUS = true;
                        data.RECORDS = false;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                }
                else
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Mobile No Already Exists";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = true;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }


            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }


    [WebMethod]
    public void GetTourData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataTour> g = new List<GeneralDataTour>();
        if (type == "tour")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[TourMaster]");
                List<Tour> list = new List<Tour>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Tour tr = new Tour();
                        tr.visiontitle = dt.Rows[i]["VisionTitle"].ToString();
                        tr.visiondesc = dt.Rows[i]["VisionDesc"].ToString();
                        tr.schooltitle = dt.Rows[i]["SchoolTitle"].ToString();
                        tr.schooldesc = dt.Rows[i]["SchoolDesc"].ToString();
                        tr.prospecttitle = dt.Rows[i]["ProspectTitle"].ToString();
                        tr.prospectdesc = dt.Rows[i]["ProspectDesc"].ToString();

                        list.Add(tr);
                    }
                    GeneralDataTour data = new GeneralDataTour();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataTour data = new GeneralDataTour();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataTour data = new GeneralDataTour();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetStoriesData(string type, string catid, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataStory> g = new List<GeneralDataStory>();
        if (type == "story")
        {
            try
            {
                int i = 0;
                //DataTable dt = cc.GetData("select * from [dbo].[StoriesMaster]");
                DataTable dt = cc.GetData("select sm.*, CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE from StoriesMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id and fm.CategoryId='" + catid + "' and fm.Userid='" + userid + "'");
                List<Story> list = new List<Story>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Story tr = new Story();
                        tr.storyid = dt.Rows[i]["Id"].ToString();
                        tr.Title = dt.Rows[i]["Title"].ToString();
                        tr.favourite = dt.Rows[i]["IS_FAVOURITE"].ToString();
                        string story = dt.Rows[i]["Description"].ToString();

                        //Regex re = new Regex("[\r\n;\\/:*\"<>|\n',(-)]");
                        //string ques = story
                        //    .Replace("\n", " ")
                        //    .Replace(".", "")
                        //    .Replace(@"\s+", " ")
                        //    .Replace(Environment.NewLine, " ")
                        //    .Replace("‘", "")
                        //    .Replace("’", "")
                        //    .Replace("`", "")
                        //    .Replace("\r", " ");

                        //ques = re.Replace(ques, " ");


                        ////ques = Regex.Replace(ques, "'", "dns");
                        //string qu = WebUtility.HtmlEncode(ques);

                        tr.storydesc = story;
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.image = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.image = "";
                        }
                        tr.link = dt.Rows[i]["link"].ToString();

                        list.Add(tr);
                    }
                    GeneralDataStory data = new GeneralDataStory();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataStory data = new GeneralDataStory();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataStory data = new GeneralDataStory();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetVideoData(string type, string categoryid, string catid, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataVideo> g = new List<GeneralDataVideo>();
        if (type == "video")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select sm.*,vc.Category, CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE from [VideoCategoryMaster] as vc, VideoMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id and  sm.vcid =" + categoryid + " and fm.CategoryId=" + catid + " and fm.Userid=" + userid + " where sm.vcid = " + categoryid + " and sm.vcid = vc.id");
                //DataTable dt = cc.GetData("select sm.*,CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE ,v.*,vc.Category from StoriesMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id, VideoMaster as v ,VideoCategoryMaster as vc where v.vcid = vc.id and v.vcid ='" + categoryid + "' OR fm.CategoryId='" + catid + "' and fm.Userid='" + userid + "'");
                //DataTable dt = cc.GetData("select sm.*,CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE ,v.*,vc.Category from StoriesMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id, VideoMaster as v , VideoCategoryMaster as vc where v.vcid = vc.id and v.vcid ='" + categoryid + "' and fm.CategoryId='" + catid + "' and fm.Userid='" + userid + "'");
                //DataTable dt = cc.GetData("select v.*,vc.Category from  VideoMaster as v ,[VideoCategoryMaster] as vc where v.vcid = vc.id and v.vcid = " + categoryid);
                List<Video> list = new List<Video>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Video tr = new Video();
                        tr.videoid = dt.Rows[i]["Id"].ToString();
                        tr.categoryid = dt.Rows[i]["vcid"].ToString();
                        tr.categoryname = dt.Rows[i]["Category"].ToString();
                        tr.videotitle = dt.Rows[i]["Title"].ToString();
                        tr.videourl = dt.Rows[i]["Url"].ToString();
                        tr.favourite = dt.Rows[i]["IS_FAVOURITE"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataVideo data = new GeneralDataVideo();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataVideo data = new GeneralDataVideo();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataVideo data = new GeneralDataVideo();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetVideoCategoryData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataVideoCategory> g = new List<GeneralDataVideoCategory>();
        if (type == "videocategory")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[VideoCategoryMaster]");
                List<VideoCategory> list = new List<VideoCategory>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        VideoCategory tr = new VideoCategory();

                        tr.categoryid = dt.Rows[i]["id"].ToString();
                        tr.categoryname = dt.Rows[i]["Category"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.categoryimg = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.categoryimg = "";
                        }
                        list.Add(tr);
                    }
                    GeneralDataVideoCategory data = new GeneralDataVideoCategory();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataVideoCategory data = new GeneralDataVideoCategory();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataVideoCategory data = new GeneralDataVideoCategory();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }
    [WebMethod]
    public void GetQuotesData(string type, string catid, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataQuotes> g = new List<GeneralDataQuotes>();
        if (type == "quotes")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select sm.*, CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE from Quotes as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id and fm.CategoryId='" + catid + "' and fm.Userid='" + userid + "'");
                //DataTable dt = cc.GetData("select * from [dbo].[Quotes]");
                List<Quotes> list = new List<Quotes>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Quotes tr = new Quotes();

                        tr.quotesid = dt.Rows[i]["id"].ToString();
                        tr.title = dt.Rows[i]["title"].ToString();
                        tr.favourite = dt.Rows[i]["IS_FAVOURITE"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.img = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.img = "";
                        }
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataQuotes data = new GeneralDataQuotes();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataQuotes data = new GeneralDataQuotes();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataQuotes data = new GeneralDataQuotes();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetAboutUsData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataaboutus> g = new List<GeneralDataaboutus>();
        if (type == "aboutus")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[AboutUs]");
                List<aboutus> list = new List<aboutus>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        aboutus tr = new aboutus();

                        tr.mission = dt.Rows[i]["Mission"].ToString();
                        tr.vision = dt.Rows[i]["Vision"].ToString();
                        tr.expertise = dt.Rows[i]["Expertise"].ToString();
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataaboutus data = new GeneralDataaboutus();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataaboutus data = new GeneralDataaboutus();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataaboutus data = new GeneralDataaboutus();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetContactUsData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataContactUs> g = new List<GeneralDataContactUs>();
        if (type == "contactus")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[ContactUs]");
                List<ContactUs> list = new List<ContactUs>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ContactUs tr = new ContactUs();

                        tr.Address = dt.Rows[i]["Address"].ToString();
                        DataTable dt2 = cc.GetData("select * from [dbo].[ContactUsNumbers]");
                        List<ContactUsNumber> list2 = new List<ContactUsNumber>();
                        if (dt2.Rows.Count > 0)
                        {
                            for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                            {
                                ContactUsNumber cu = new ContactUsNumber();

                                cu.Id = dt2.Rows[j]["id"].ToString();
                                cu.Name = dt2.Rows[j]["name"].ToString();
                                cu.No = dt2.Rows[j]["mobile"].ToString();
                                list2.Add(cu);
                            }
                        }
                        tr.Numbers = list2;
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataContactUs data = new GeneralDataContactUs();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataContactUs data = new GeneralDataContactUs();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataContactUs data = new GeneralDataContactUs();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }
    [WebMethod]
    public void InsertTopicData(string type, string clientid, string subname, string description)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "topic")
        {
            try
            {
                subname = HttpUtility.UrlDecode(subname);
                description = HttpUtility.UrlDecode(description);

                cc.ExecuteQuery("insert into Topic (userid,subname,description) values('" + clientid + "','" + subname + "','" + description + "')");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void InsertFeedbackData(string type, string clientid, string ftype, string description)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "feedback")
        {
            try
            {
                description = HttpUtility.UrlDecode(description);

                cc.ExecuteQuery("insert into Feedback (userid,Type,description) values('" + clientid + "','" + ftype + "','" + description + "')");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void GetAlbumData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataAlbum> g = new List<GeneralDataAlbum>();
        if (type == "album")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[AlbumMaster]");
                List<Album> list = new List<Album>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Album tr = new Album();

                        tr.id = dt.Rows[i]["Id"].ToString();
                        tr.name = dt.Rows[i]["Name"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.image = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.image = "";
                        }
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataAlbum data = new GeneralDataAlbum();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataAlbum data = new GeneralDataAlbum();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataAlbum data = new GeneralDataAlbum();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }
    [WebMethod]
    public void GetSubAlbumData(string type, string albumid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataAlbum> g = new List<GeneralDataAlbum>();
        if (type == "subalbum")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[SubAlbum] where albumid = " + albumid);
                List<Album> list = new List<Album>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Album tr = new Album();

                        tr.id = dt.Rows[i]["Id"].ToString();
                        tr.name = dt.Rows[i]["Name"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.image = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.image = "";
                        }
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataAlbum data = new GeneralDataAlbum();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataAlbum data = new GeneralDataAlbum();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataAlbum data = new GeneralDataAlbum();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }
    [WebMethod]
    public void GetEventData(string type, string catid, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataEvent> g = new List<GeneralDataEvent>();
        if (type == "event")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select sm.*,cm.city as cityname, CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE from CityMaster as cm,EventMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id and fm.CategoryId='" + catid + "' and fm.Userid='" + userid + "' where cm.id = sm.location");
                //DataTable dt = cc.GetData("select * from [dbo].[EventMaster]");
                List<Event> list = new List<Event>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Event tr = new Event();

                        tr.id = dt.Rows[i]["Id"].ToString();
                        tr.title = dt.Rows[i]["Title"].ToString();
                        tr.favourite = dt.Rows[i]["IS_FAVOURITE"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            tr.image = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            tr.image = "";
                        }
                        tr.location = dt.Rows[i]["cityname"].ToString();
                        if (dt.Rows[i]["Date"].ToString() != "")
                        {

                            tr.date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd-MM-yyyy");
                            //hc.time = Convert.ToDateTime(dt2.Rows[i]["date"].ToString()).ToString("hh:mm tt");
                        }

                        tr.type = dt.Rows[i]["Type"].ToString();
                        tr.latitude = dt.Rows[i]["Latitude"].ToString();
                        tr.longitude = dt.Rows[i]["Longitude"].ToString();
                        tr.address = dt.Rows[i]["Address"].ToString();
                        tr.description = dt.Rows[i]["Description"].ToString();
                        //tr.img = dt.Rows[i]["Category"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataEvent data = new GeneralDataEvent();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataEvent data = new GeneralDataEvent();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataEvent data = new GeneralDataEvent();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void GetEventDetailData(string type, string eventid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataEventDetail> g = new List<GeneralDataEventDetail>();
        if (type == "eventdetail")
        {
            try
            {
                int i = 0;
                DataTable dt = cc.GetData("select * from [dbo].[EventPasses] where eventid =" + eventid);
                DataTable dt2 = cc.GetData("select * from [dbo].[EventMaster] where id =" + eventid);
                List<EventDetail> list = new List<EventDetail>();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        EventDetail tr = new EventDetail();

                        tr.id = dt.Rows[i]["Id"].ToString();
                        tr.name = dt.Rows[i]["Name"].ToString();
                        if (dt.Rows[i]["Time"].ToString() != "")
                        {

                            //tr.date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd-MM-yyyy");
                            tr.time = Convert.ToDateTime(dt.Rows[i]["Time"].ToString()).ToString("hh:mm tt");
                        }
                        else
                        {
                            tr.time = "";
                        }

                        tr.price = dt.Rows[i]["Price"].ToString();
                        list.Add(tr);
                    }
                    GeneralDataEventDetail data = new GeneralDataEventDetail();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    if (dt2.Rows[0]["Date"].ToString() != "")
                    {

                        data.Day = Convert.ToDateTime(dt2.Rows[0]["Date"].ToString()).ToString("dd");
                        data.Month = Convert.ToDateTime(dt2.Rows[0]["Date"].ToString()).ToString("MM");
                        data.Year = Convert.ToDateTime(dt2.Rows[0]["Date"].ToString()).ToString("yyyy");
                        //tr.time = Convert.ToDateTime(dt.Rows[i]["Time"].ToString()).ToString("hh:mm tt");
                    }
                    else
                    {
                        data.Day = "";
                        data.Month = "";
                        data.Year = "";
                    }
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataEventDetail data = new GeneralDataEventDetail();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralDataEventDetail data = new GeneralDataEventDetail();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    [WebMethod]
    public void InsertSchoolReviewData(string type, string userid, string schoolname, string reviewer, string designation, string stateid, string cityid, string review, string ratingbox, string noofstudent)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "schoolreview")
        {
            try
            {
                schoolname = HttpUtility.UrlDecode(schoolname);
                reviewer = HttpUtility.UrlDecode(reviewer);
                review = HttpUtility.UrlDecode(review);
                designation = HttpUtility.UrlDecode(designation);
                ratingbox = HttpUtility.UrlDecode(ratingbox);
                string today = DateTime.Now.ToString("yyyy/MM/dd");
                cc.ExecuteQuery("insert into SchoolReview (Userid,Schoolname,Reviewer,Designation,Stateid,Cityid,Review,RatingBox,NoOfStudent,Date,Status) values('" + userid + "','" + schoolname + "','" + reviewer + "','" + designation + "','" + stateid + "','" + cityid + "','" + review + "','" + ratingbox + "','" + noofstudent + "','" + today + "','0')");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = false;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void InsertEventDetailData(string type, string userid, string eventid, string fname, string lname, string email, string mobile, string whatsapp, string stateid, string cityid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "eventdetail")
        {
            try
            {
                fname = HttpUtility.UrlDecode(fname);
                lname = HttpUtility.UrlDecode(lname);
                email = HttpUtility.UrlDecode(email);
                cc.ExecuteQuery("insert into EventDetail (Userid,Eventid,Firstname,Lastname,Email,Mobile,WhatsappNo,stateid,Cityid) values('" + userid + "','" + eventid + "','" + fname + "','" + lname + "','" + email + "','" + mobile + "','" + whatsapp + "','" + stateid + "','" + cityid + "')");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = false;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void GetBannerData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataBanner> g = new List<GeneralDataBanner>();
        if (type == "banner")
        {
            try
            {
                int i = 0;


                List<DetailData> list = new List<DetailData>();
                DetailData tr = new DetailData();
                List<Banner> list2 = new List<Banner>();
                DataTable dt = cc.GetData("select * from [dbo].[Banner]");
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Banner bd = new Banner();

                        bd.id = dt.Rows[i]["Id"].ToString();
                        if (dt.Rows[i]["img"].ToString() != "")
                        {
                            bd.img = "http://inceptionlearning.com/A" + dt.Rows[i]["img"].ToString();
                        }
                        else
                        {
                            bd.img = "";
                        }

                        list2.Add(bd);
                    }

                }
                List<MenuDataDetail> list3 = new List<MenuDataDetail>();
                DataTable dt2 = cc.GetData("select * from [dbo].[MenuMasterAnd]");
                if (dt2.Rows.Count > 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        MenuDataDetail bd = new MenuDataDetail();

                        bd.id = dt2.Rows[i]["Id"].ToString();
                        bd.name = dt2.Rows[i]["Name"].ToString();
                        if (dt2.Rows[i]["image"].ToString() != "")
                        {
                            bd.image = "http://inceptionlearning.com/A" + dt2.Rows[i]["image"].ToString();
                        }
                        else
                        {
                            bd.image = "";
                        }
                        if (dt2.Rows[i]["bgimage"].ToString() != "")
                        {
                            bd.bgimage = "http://inceptionlearning.com/A" + dt2.Rows[i]["bgimage"].ToString();
                        }
                        else
                        {
                            bd.bgimage = "";
                        }
                        bd.sequence = dt2.Rows[i]["Sequence"].ToString();
                        bd.parentid = dt2.Rows[i]["parentid"].ToString();
                        list3.Add(bd);
                    }

                }

                tr.BannerData = list2;
                tr.MenuData = list3;
                list.Add(tr);
                GeneralDataBanner data = new GeneralDataBanner();
                data.MESSAGE = "Successfull";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = true;
                data.Data = list;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralDataBanner data = new GeneralDataBanner();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }

    //[WebMethod]
    //public void GetMenuData(string type)
    //{
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    List<GeneralDataMenu> g = new List<GeneralDataMenu>();
    //    if (type == "menus")
    //    {
    //        try
    //        {
    //            int i = 0;
    //            DataTable dt = cc.GetData("select * from [dbo].[MenuMaster] order by rank");
    //            List<Menu> list = new List<Menu>();
    //            if (dt.Rows.Count > 0)
    //            {
    //                for (i = 0; i <= dt.Rows.Count - 1; i++)
    //                {
    //                    Menu tr = new Menu();

    //                    tr.id = dt.Rows[i]["Id"].ToString();
    //                    tr.name = dt.Rows[i]["name"].ToString();
    //                    if (dt.Rows[i]["image"].ToString() != "")
    //                    {
    //                        tr.image = "http://inceptionlearning.com/A" + dt.Rows[i]["image"].ToString();
    //                    }
    //                    else
    //                    {
    //                        tr.image = "";
    //                    }
    //                    tr.color = dt.Rows[i]["Color"].ToString();
    //                    tr.rank = dt.Rows[i]["Rank"].ToString();
    //                    list.Add(tr);
    //                }
    //                GeneralDataMenu data = new GeneralDataMenu();
    //                data.MESSAGE = "Successfull";
    //                data.ORIGINAL_ERROR = "";
    //                data.ERROR_STATUS = false;
    //                data.RECORDS = true;
    //                data.Data = list;
    //                g.Add(data);
    //                Context.Response.Write(js.Serialize(g[0]));
    //            }
    //            else
    //            {
    //                GeneralDataMenu data = new GeneralDataMenu();
    //                data.MESSAGE = "No Data Found";
    //                data.ORIGINAL_ERROR = "";
    //                data.ERROR_STATUS = false;
    //                data.RECORDS = false;
    //                g.Add(data);
    //                Context.Response.Write(js.Serialize(g[0]));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            GeneralDataMenu data = new GeneralDataMenu();
    //            data.MESSAGE = "Failed";
    //            data.ORIGINAL_ERROR = ex.Message;
    //            data.ERROR_STATUS = true;
    //            data.RECORDS = false;
    //            g.Add(data);
    //            Context.Response.Write(js.Serialize(g[0]));
    //        }

    //    }
    //}

    [WebMethod]
    public void ViewTareAasmanKeData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<Ngo> list = new List<Ngo>();
        List<GeneralDataNgo> g = new List<GeneralDataNgo>();
        if (type == "ngo")
        {
            try
            {
                DataTable dt = cc.GetData("select * from [dbo].[NGOMaster]");
                DataTable dt4 = cc.GetData("select sum(noofstudent) as totalstudents,count(id) as totalschools from SchoolReview where status = 1");
                DataTable dt5 = cc.GetData("select schoolname from SchoolReview where status = 1 group by schoolname ");
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Ngo hc = new Ngo();

                        hc.id = dt.Rows[i]["id"].ToString();
                        hc.title = dt.Rows[i]["title"].ToString();
                        hc.totalschools = dt5.Rows.Count.ToString();
                        hc.totalstudents = dt4.Rows[0]["totalstudents"].ToString();
                        if (dt.Rows[i]["videourl"].ToString() != "")
                        {
                            hc.video = "http://inceptionlearning.com/A" + dt.Rows[i]["videourl"].ToString();
                        }
                        else
                        {
                            hc.video = "";
                        }
                        //hc.video = "http://inceptionlearning.com/A" + dt.Rows[i]["videourl"].ToString();
                        hc.ngotitle = dt.Rows[i]["titlengo"].ToString();
                        hc.description = dt.Rows[i]["description"].ToString();

                        List<ReviewDetail> list3 = new List<ReviewDetail>();

                        DataTable dt3 = cc.GetData("select top(3)* from [dbo].[SchoolReview]  where status = 1 order by id desc");
                        for (int j = 0; j <= dt3.Rows.Count - 1; j++)
                        {
                            ReviewDetail rd = new ReviewDetail();
                            rd.reviewid = dt3.Rows[j]["id"].ToString();
                            rd.userid = dt3.Rows[j]["userid"].ToString();
                            rd.name = dt3.Rows[j]["reviewer"].ToString();
                            rd.rating = dt3.Rows[j]["ratingbox"].ToString();
                            if (dt3.Rows[j]["date"].ToString() != "")
                            {

                                rd.date = Convert.ToDateTime(dt3.Rows[j]["date"].ToString()).ToString("dd-MM-yyyy");
                                //hc.time = Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString("hh:mm tt");
                            }
                            rd.description = dt3.Rows[j]["review"].ToString();
                            list3.Add(rd);
                        }

                        List<CalculateRate> list4 = new List<CalculateRate>();
                        DataTable dt6 = cc.GetData("select Count(RatingBox) as total,ratingbox as rate from SchoolReview where status = 1 group by ratingbox ");
                        for (int j = 0; j <= dt6.Rows.Count - 1; j++)
                        {
                            CalculateRate rd = new CalculateRate();

                            rd.star = dt6.Rows[j]["rate"].ToString();
                            rd.total = dt6.Rows[j]["total"].ToString();

                            list4.Add(rd);
                        }

                        //List<ReviewDetail> list4 = new List<ReviewDetail>();
                        //DataTable dt4 = cc.GetData("select * from [dbo].[SchoolReview]  where status = 1 order by id desc");
                        //for (int j = 0; j <= dt4.Rows.Count - 1; j++)
                        //{
                        //    ReviewDetail rd = new ReviewDetail();

                        //    rd.name = dt4.Rows[j]["reviewer"].ToString();
                        //    rd.rating = dt4.Rows[j]["ratingbox"].ToString();
                        //    if (dt4.Rows[j]["date"].ToString() != "")
                        //    {

                        //        rd.date = Convert.ToDateTime(dt4.Rows[j]["date"].ToString()).ToString("dd-MM-yyyy");
                        //        //hc.time = Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString("hh:mm tt");
                        //    }
                        //    rd.description = dt4.Rows[j]["review"].ToString();
                        //    list4.Add(rd);
                        //}

                        //List<UserMaster> list5 = new List<UserMaster>();
                        //DataTable dt5 = cc.GetData("select um.*,sm.name as statename,cm.city as cityname from ClientMaster as um,[StateMaster] as sm,[CityMaster] as cm where um.stateid = sm.stateid and um.cityid = cm.id and um.Id = '" + userid + "'");
                        //for (int j = 0; j <= dt5.Rows.Count - 1; j++)
                        //{
                        //    UserMaster um = new UserMaster();


                        //    um.clientid = dt5.Rows[j]["Id"].ToString();
                        //    um.name = dt5.Rows[j]["Name"].ToString();
                        //    um.mobile = dt5.Rows[j]["Mobile"].ToString();
                        //    um.email = dt5.Rows[j]["Email"].ToString();
                        //    um.stateid = dt5.Rows[j]["stateid"].ToString();
                        //    um.state = dt5.Rows[j]["Statename"].ToString();
                        //    um.cityid = dt5.Rows[j]["cityid"].ToString();
                        //    um.city = dt5.Rows[j]["Cityname"].ToString();
                        //    um.verificationstatus = dt5.Rows[j]["VerificationStatus"].ToString();
                        //    list5.Add(um);
                        //}

                        List<NgoDetail> list2 = new List<NgoDetail>();
                        DataTable dt2 = cc.GetData("select * from [dbo].[NgoDetail] where ngoid =  " + dt.Rows[i]["id"].ToString());
                        for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                        {
                            NgoDetail rd = new NgoDetail();

                            rd.detailid = dt2.Rows[j]["id"].ToString();
                            rd.listname = dt2.Rows[j]["listname"].ToString();
                            list2.Add(rd);
                        }
                        hc.Review = list3;
                        hc.Rates = list4;
                        //hc.UserData = list5;
                        hc.Detail = list2;
                        list.Add(hc);

                    }
                    GeneralDataNgo data = new GeneralDataNgo();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }
                else
                {
                    GeneralDataNgo data = new GeneralDataNgo();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataNgo data = new GeneralDataNgo();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void InsertHireUsToSpeak(string type, string userid, string name, string email, string subject, string message)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "hireus")
        {
            try
            {
                name = HttpUtility.UrlDecode(name);
                email = HttpUtility.UrlDecode(email);
                message = HttpUtility.UrlDecode(message);
                cc.ExecuteQuery("insert into HireUs (Userid,Name,Email,Subject,Message) values('" + userid + "','" + name + "','" + email + "','" + subject + "','" + message + "')");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = false;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void ViewAllReviewData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<ReviewDetail> list = new List<ReviewDetail>();
        List<GeneralDataReviewDetail> g = new List<GeneralDataReviewDetail>();
        if (type == "review")
        {
            try
            {



                DataTable dt = cc.GetData("select * from [dbo].[SchoolReview]  where status = 1 order by id desc");
                if (dt.Rows.Count != 0)
                {

                    for (int j = 0; j <= dt.Rows.Count - 1; j++)
                    {

                        ReviewDetail rd = new ReviewDetail();
                        rd.reviewid = dt.Rows[j]["id"].ToString();
                        rd.userid = dt.Rows[j]["userid"].ToString();
                        rd.name = dt.Rows[j]["reviewer"].ToString();
                        rd.rating = dt.Rows[j]["ratingbox"].ToString();
                        if (dt.Rows[j]["date"].ToString() != "")
                        {

                            rd.date = Convert.ToDateTime(dt.Rows[j]["date"].ToString()).ToString("dd-MM-yyyy");
                            //hc.time = Convert.ToDateTime(dt.Rows[i]["date"].ToString()).ToString("hh:mm tt");
                        }
                        rd.description = dt.Rows[j]["review"].ToString();
                        list.Add(rd);
                    }

                    GeneralDataReviewDetail data = new GeneralDataReviewDetail();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }
                else
                {
                    GeneralDataReviewDetail data = new GeneralDataReviewDetail();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataReviewDetail data = new GeneralDataReviewDetail();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void ViewRecentTaskData(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<RecentTask> list = new List<RecentTask>();
        List<GeneralDataRecentTask> g = new List<GeneralDataRecentTask>();
        if (type == "recenttask")
        {
            try
            {



                DataTable dt = cc.GetData("select * from [dbo].[RecentTask]");
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RecentTask hc = new RecentTask();

                        hc.id = dt.Rows[i]["id"].ToString();
                        if (dt.Rows[i]["Date"].ToString() != "")
                        {

                            hc.date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd-MM-yyyy");
                            //hc.time = Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString("hh:mm tt");
                        }
                        hc.title = dt.Rows[i]["title"].ToString();
                        if (dt.Rows[i]["image"].ToString() != "")
                        {
                            hc.image = "http://inceptionlearning.com/A" + dt.Rows[i]["image"].ToString();
                        }
                        else
                        {
                            hc.image = "";
                        }
                        //hc.video = "http://inceptionlearning.com/A" + dt.Rows[i]["videourl"].ToString();
                        hc.shortdesc = dt.Rows[i]["ShortDesc"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataRecentTask data = new GeneralDataRecentTask();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }
                else
                {
                    GeneralDataRecentTask data = new GeneralDataRecentTask();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataRecentTask data = new GeneralDataRecentTask();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void ViewRecentTaskDetailData(string type, string recenttypeid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<RecentTaskDetail> list = new List<RecentTaskDetail>();
        List<GeneralDataRecentTaskDetail> g = new List<GeneralDataRecentTaskDetail>();
        if (type == "recenttaskdetail")
        {
            try
            {
                DataTable dt = cc.GetData("select * from [dbo].[RecentTask] where id = " + recenttypeid);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RecentTaskDetail hc = new RecentTaskDetail();

                        hc.id = dt.Rows[i]["id"].ToString();
                        if (dt.Rows[i]["Date"].ToString() != "")
                        {

                            hc.date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd-MM-yyyy");
                            //hc.time = Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString("hh:mm tt");
                        }
                        hc.title = dt.Rows[i]["title"].ToString();
                        if (dt.Rows[i]["image"].ToString() != "")
                        {
                            hc.image = "http://inceptionlearning.com/A" + dt.Rows[i]["image"].ToString();
                        }
                        else
                        {
                            hc.image = "";
                        }
                        //hc.video = "http://inceptionlearning.com/A" + dt.Rows[i]["videourl"].ToString();
                        hc.shortdesc = dt.Rows[i]["ShortDesc"].ToString();
                        hc.longdesc = dt.Rows[i]["LongDesc"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataRecentTaskDetail data = new GeneralDataRecentTaskDetail();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }
                else
                {
                    GeneralDataRecentTaskDetail data = new GeneralDataRecentTaskDetail();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataRecentTaskDetail data = new GeneralDataRecentTaskDetail();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void ViewJustFactsData(string type, string dykid, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<JustFacts> list = new List<JustFacts>();
        List<GeneralDataJustFacts> g = new List<GeneralDataJustFacts>();
        if (type == "justfacts")
        {
            try
            {
                DataTable dt = cc.GetData("select sm.*, CASE WHEN fm.Id IS NULL THEN '0' ELSE '1' END as IS_FAVOURITE from JustFactsMaster as sm left outer join FavouriteMaster as fm on fm.ItemId=sm.Id and fm.CategoryId='" + dykid + "' and fm.Userid='" + userid + "' where sm.dykid = " + dykid + " order by factid desc");
                //DataTable dt = cc.GetData("select * from [dbo].[JustFactsMaster] where dykid = " + dykid + " order by factid");
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        JustFacts hc = new JustFacts();

                        hc.id = dt.Rows[i]["id"].ToString();
                        hc.hashid = "#" + dt.Rows[i]["factid"].ToString();
                        hc.text = dt.Rows[i]["text"].ToString();
                        hc.favourite = dt.Rows[i]["IS_FAVOURITE"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataJustFacts data = new GeneralDataJustFacts();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));


                }
                else
                {
                    GeneralDataJustFacts data = new GeneralDataJustFacts();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataJustFacts data = new GeneralDataJustFacts();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void ViewSingleReviewData(string type, string reviewid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<ReviewData> list = new List<ReviewData>();
        List<GeneralDataReviewData> g = new List<GeneralDataReviewData>();
        if (type == "review")
        {
            try
            {
                DataTable dt2 = cc.GetData("select sr.*,sm.name as statename,cm.city as cityname from [dbo].[SchoolReview] as sr,[dbo].[StateMaster] as sm,[dbo].[CityMaster] as cm where sr.stateid = sm.stateid and cm.id = sr.cityid and sr.Id = '" + reviewid + "'");
                if (dt2.Rows.Count != 0)
                {

                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        ReviewData hc = new ReviewData();


                        hc.reviewid = dt2.Rows[i]["Id"].ToString();
                        hc.schoolname = dt2.Rows[i]["Schoolname"].ToString();
                        hc.reviewer = dt2.Rows[i]["Reviewer"].ToString();
                        hc.designation = dt2.Rows[i]["Designation"].ToString();
                        hc.stateid = dt2.Rows[i]["stateid"].ToString();
                        hc.state = dt2.Rows[i]["statename"].ToString();
                        hc.cityid = dt2.Rows[i]["cityid"].ToString();
                        hc.city = dt2.Rows[i]["cityname"].ToString();
                        hc.review = dt2.Rows[i]["Review"].ToString();
                        hc.ratingbox = dt2.Rows[i]["RatingBox"].ToString();
                        hc.noofstudent = dt2.Rows[i]["NoOfStudent"].ToString();


                        if (dt2.Rows[i]["Date"].ToString() != "")
                        {

                            hc.date = Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()).ToString("dd-MM-yyyy");
                            //hc.time = Convert.ToDateTime(dt2.Rows[i]["date"].ToString()).ToString("hh:mm tt");
                        }



                        list.Add(hc);

                    }
                    GeneralDataReviewData data = new GeneralDataReviewData();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {

                    GeneralDataReviewData data = new GeneralDataReviewData();
                    data.MESSAGE = "No Record Found !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }



            }
            catch (Exception ex)
            {
                GeneralDataReviewData data = new GeneralDataReviewData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void InsertFavouriteData(string type, string userid, string categoryid, string itemid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "favourite")
        {
            try
            {
                DataTable dt = cc.GetData("select * from FavouriteMaster where userid = " + userid + " and itemid = " + itemid + " and categoryid = " + categoryid);
                if (dt.Rows.Count == 0)
                {
                    //name = HttpUtility.UrlDecode(name);
                    //email = HttpUtility.UrlDecode(email);
                    //message = HttpUtility.UrlDecode(message);
                    string today = DateTime.Now.ToString("yyyy/MM/dd");
                    cc.ExecuteQuery("insert into FavouriteMaster (Categoryid,Userid,ItemId,Date) values('" + categoryid + "','" + userid + "','" + itemid + "','" + today + "')");
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;

                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "You Already Favourite this Data";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }
    [WebMethod]
    public void DeleteFavouriteData(string type, string userid, string itemid, string categoryid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "deletefavourite")
        {
            try
            {
                //name = HttpUtility.UrlDecode(name);
                //email = HttpUtility.UrlDecode(email);
                //message = HttpUtility.UrlDecode(message);
                string today = DateTime.Now.ToString("yyyy/MM/dd");
                cc.ExecuteQuery("delete from [dbo].[FavouriteMaster] where userid = " + userid + " and itemid = " + itemid + " and categoryid = " + categoryid);
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully !";
                data.ORIGINAL_ERROR = "";
                data.ERROR_STATUS = false;
                data.RECORDS = false;

                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }


    }

    [WebMethod]
    public void GetFavouriteData(string type, string userid, string categoryid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataMenu> g = new List<GeneralDataMenu>();
        List<Menu> list = new List<Menu>();
        if (type == "favourite")
        {
            try
            {
                int i = 0;
                //DataTable dt = cc.GetData("select distinct categoryid from [dbo].[FavouriteMaster] where userid = " + userid);
                //if (dt.Rows.Count > 0)
                //{

                //    for (i = 0; i <= dt.Rows.Count - 1; i++)
                //    {

                DataTable dt2 = cc.GetData("select * from [dbo].[MenuMasterAnd] where id = " + categoryid);
                if (dt2.Rows.Count > 0)
                {


                    for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                    {
                        Menu tr = new Menu();

                        tr.id = dt2.Rows[j]["Id"].ToString();
                        tr.name = dt2.Rows[j]["name"].ToString();
                        if (dt2.Rows[j]["image"].ToString() != "")
                        {
                            tr.image = "http://inceptionlearning.com/A" + dt2.Rows[j]["image"].ToString();
                        }
                        else
                        {
                            tr.image = "";
                        }
                        if (dt2.Rows[j]["bgimage"].ToString() != "")
                        {
                            tr.bgimage = "http://inceptionlearning.com/A" + dt2.Rows[j]["bgimage"].ToString();
                        }
                        else
                        {
                            tr.bgimage = "";
                        }
                        tr.sequence = dt2.Rows[j]["sequence"].ToString();
                        tr.parentid = dt2.Rows[j]["parentid"].ToString();
                        List<MenuData> list2 = new List<MenuData>();
                        if (dt2.Rows[j]["Id"].ToString() == "3")
                        {
                            //List<MenuData> list2 = new List<MenuData>();
                            DataTable dt3 = cc.GetData("select sm.* from [dbo].[StoriesMaster] as sm,FavouriteMaster as fm where fm.categoryid = 3 and fm.itemid = sm.id");
                            for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                            {
                                MenuData md = new MenuData();
                                md.id = dt3.Rows[k]["Id"].ToString();
                                md.name = dt3.Rows[k]["title"].ToString();
                                md.desc = dt3.Rows[k]["description"].ToString();
                                if (dt3.Rows[k]["img"].ToString() != "")
                                {
                                    md.image = "http://inceptionlearning.com/A" + dt3.Rows[k]["img"].ToString();
                                }
                                else
                                {
                                    md.image = "";
                                }
                                md.link = dt3.Rows[k]["link"].ToString();
                                md.location = "";
                                md.date = "";
                                md.address = "";
                                list2.Add(md);
                            }
                        }
                        if (dt2.Rows[j]["Id"].ToString() == "4")
                        {
                            //List<MenuData> list2 = new List<MenuData>();
                            DataTable dt3 = cc.GetData("select sm.* from [dbo].[EventMaster] as sm,FavouriteMaster as fm where fm.categoryid = 4 and fm.itemid = sm.id");
                            for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                            {
                                MenuData md = new MenuData();
                                md.id = dt3.Rows[k]["Id"].ToString();
                                md.name = dt3.Rows[k]["title"].ToString();
                                md.desc = dt3.Rows[k]["description"].ToString();
                                if (dt3.Rows[k]["img"].ToString() != "")
                                {
                                    md.image = "http://inceptionlearning.com/A" + dt3.Rows[k]["img"].ToString();
                                }
                                else
                                {
                                    md.image = "";
                                }
                                md.link = "";
                                md.location = dt3.Rows[k]["location"].ToString();
                                //md.date = dt3.Rows[k]["location"].ToString();
                                if (dt3.Rows[k]["date"].ToString() != "")
                                {

                                    md.date = Convert.ToDateTime(dt3.Rows[k]["date"].ToString()).ToString("dd-MM-yyyy");

                                }
                                else
                                {
                                    md.date = "";
                                }
                                md.address = dt3.Rows[k]["address"].ToString();
                                list2.Add(md);
                            }
                        }
                        if (dt2.Rows[j]["Id"].ToString() == "5")
                        {
                            //List<MenuData> list2 = new List<MenuData>();
                            DataTable dt3 = cc.GetData("select sm.* from [dbo].[VideoMaster] as sm,FavouriteMaster as fm where fm.categoryid = 5 and fm.itemid = sm.id");
                            for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                            {
                                MenuData md = new MenuData();
                                md.id = dt3.Rows[k]["Id"].ToString();
                                md.name = dt3.Rows[k]["title"].ToString();
                                md.desc = "";
                                //if (dt3.Rows[k]["img"].ToString() != "")
                                //{
                                //    md.image = "http://inceptionlearning.com/A" + dt3.Rows[k]["img"].ToString();
                                //}
                                //else
                                //{
                                //    md.image = "";
                                //}
                                md.image = "";
                                md.link = dt3.Rows[k]["url"].ToString();
                                md.location = "";
                                md.date = "";
                                md.address = "";
                                list2.Add(md);
                            }
                        }
                        if (dt2.Rows[j]["Id"].ToString() == "6")
                        {
                            //List<MenuData> list2 = new List<MenuData>();
                            DataTable dt3 = cc.GetData("select sm.* from [dbo].[Quotes] as sm,FavouriteMaster as fm where fm.categoryid = 6 and fm.itemid = sm.id");
                            for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                            {
                                MenuData md = new MenuData();
                                md.id = dt3.Rows[k]["Id"].ToString();
                                md.name = dt3.Rows[k]["title"].ToString();
                                md.desc = "";
                                if (dt3.Rows[k]["img"].ToString() != "")
                                {
                                    md.image = "http://inceptionlearning.com/A" + dt3.Rows[k]["img"].ToString();
                                }
                                else
                                {
                                    md.image = "";
                                }
                                md.link = "";
                                md.location = "";
                                md.date = "";
                                md.address = "";
                                list2.Add(md);
                            }
                        }
                        if (dt2.Rows[j]["Id"].ToString() == "9" || dt2.Rows[j]["Id"].ToString() == "10" || dt2.Rows[j]["Id"].ToString() == "11" || dt2.Rows[j]["Id"].ToString() == "12" || dt2.Rows[j]["Id"].ToString() == "13" || dt2.Rows[j]["Id"].ToString() == "14" || dt2.Rows[j]["Id"].ToString() == "15" || dt2.Rows[j]["Id"].ToString() == "16")
                        {

                            //List<MenuData> list2 = new List<MenuData>();
                            DataTable dt3 = cc.GetData("select sm.* from [dbo].[JustFactsMaster] as sm,FavouriteMaster as fm where fm.categoryid = " + dt2.Rows[j]["Id"].ToString() + " and fm.itemid = sm.id and  sm.dykid = fm.categoryid");
                            for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                            {
                                MenuData md = new MenuData();
                                md.id = dt3.Rows[k]["Id"].ToString();
                                md.name = dt3.Rows[k]["text"].ToString();
                                md.desc = "#" + dt3.Rows[k]["factid"].ToString();
                                //if (dt3.Rows[k]["img"].ToString() != "")
                                //{
                                //    md.image = "http://inceptionlearning.com/A" + dt3.Rows[k]["img"].ToString();
                                //}
                                //else
                                //{
                                //    md.image = "";
                                //}
                                md.image = "";
                                md.link = "";
                                md.location = "";
                                md.date = "";
                                md.address = "";
                                list2.Add(md);
                            }
                        }
                        tr.Detail = list2;
                        list.Add(tr);

                    }
                    GeneralDataMenu data = new GeneralDataMenu();
                    data.MESSAGE = "Successfull";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = list;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataMenu data = new GeneralDataMenu();
                    data.MESSAGE = "No Data Found";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }

                //    }


                //}
                //else
                //{
                //    GeneralDataMenu data = new GeneralDataMenu();
                //    data.MESSAGE = "No Data Found";
                //    data.ORIGINAL_ERROR = "";
                //    data.ERROR_STATUS = false;
                //    data.RECORDS = false;
                //    g.Add(data);
                //    Context.Response.Write(js.Serialize(g[0]));
                //}

            }
            catch (Exception ex)
            {
                GeneralDataMenu data = new GeneralDataMenu();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }

        }
    }
}
