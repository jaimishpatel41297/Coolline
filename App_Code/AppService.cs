using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for AppService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AppService : System.Web.Services.WebService
{

    string websiteclient = "http://dealjeeto.itfuturz.com/";
    string website = "http://dealjeeto.itfuturz.com/";

    SqlConnection cn = new SqlConnection("Data Source=103.87.173.217;Initial Catalog=Dealjeeto;uid=studyfield;pwd=arpit@17;");

    GetData D = new GetData();
    Connection cc = new Connection();

    public AppService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Login & Verification
    [WebMethod]
    public void SendOtp(string type, string code, string mobile)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        string res = "";
        if (type == "otp")
        {
            try
            {
                SendSms sms = new SendSms();
                res = sms.sendotp("Your Verification Code is " + code, mobile);

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
    public void VerifyOtp(string type, string mobile)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<GeneralData> g = new List<GeneralData>();
        if (type == "verify")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("Select * from Member where Mobile='" + mobile + "'");
                if (dt2.Rows.Count != 0)
                {
                    if (dt2.Rows[0]["Mobile"].ToString() == mobile)
                    {
                        D.ExecuteQuery("update Member set OtpVerification=1 where Mobile='" + mobile +"'");
                        GeneralData data = new GeneralData();
                        data.MESSAGE = "Otp Veryfied";
                        data.ORIGINAL_ERROR = "Otp Veryfied";
                        data.ERROR_STATUS = false;
                        data.RECORDS = false;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                }
                else
                {
                    GeneralData data = new GeneralData();
                    data.MESSAGE = "Otp Invalid";
                    data.ORIGINAL_ERROR = "Otp Invalid";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Otp Not Veryfied";
                data.ORIGINAL_ERROR = "Otp Not Veryfied";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
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
                DataTable dt = D.GetDataTable("select * from member where (Email='" + Username + "' or Mobile='"+ Username + "') and Password='" + password + "'");
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
                    u.password = dt.Rows[i]["password"].ToString();
                    u.OtpVerification = dt.Rows[i]["OtpVerification"].ToString();
                    u.MyReferralCode = dt.Rows[i]["MyReferralCode"].ToString();
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

    #endregion

    [WebMethod]
    public void GetMemberById(string type, string Memberid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<UserMaster> list = new List<UserMaster>();
        List<GeneralDataUser> g = new List<GeneralDataUser>();
        if (type == "getmember")
        {
            try
            {
                DataTable dt = D.GetDataTable("select * from member where id=" + Memberid);
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
                    u.password = dt.Rows[i]["password"].ToString();
                    u.MyReferralCode = dt.Rows[i]["MyReferralCode"].ToString();
                    DataTable dtQuizRunnerUp = D.GetDataTable("Select count(*) as QuizRunnerUp from LeagueMember where PriceType<>'Cash' and memberid=" + Memberid);
                    DataTable dtQuizWon = D.GetDataTable("Select count(*) as QuizWon from LeagueMember where PriceType='Cash' and memberid=" + Memberid);
                    DataTable dtQuizPlayed = D.GetDataTable("Select count(*) as QuizPlayed from LeagueMember where memberid=" + Memberid);

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
    public void RegisterMember2(string type, string Name, string email, string password, string dob, string gender, string mobile, string address, string city, string pincode, string state, string country, string MyReferralCode, string ReferralCode, string DeviceUniqueId, string OtpVerification)
    {
        string qry = "";
        string qrys = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralRegisterData> g = new List<GeneralRegisterData>();
        if (type == "Member")
        {
            try
            {
                DataTable dtMobile = D.GetDataTable("select * from Member where Mobile='" + mobile+"'");
                DataTable dtEmail = D.GetDataTable("select * from Member where Email='" + email+"'");

                if (dtMobile.Rows.Count > 0)
                {
                    // Mobile No Exists
                    GeneralRegisterData data = new GeneralRegisterData();
                    data.MESSAGE = "Mobile No Already Exists";
                    data.ORIGINAL_ERROR = "Mobile No Already Exists";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    data.Data = 0;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                    return;
                }

                if (dtEmail.Rows.Count > 0)
                {
                    // Email ID Exists
                    GeneralRegisterData data = new GeneralRegisterData();
                    data.MESSAGE = "Email Already Exists";
                    data.ORIGINAL_ERROR = "Email Already Exists";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    data.Data = 0;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                    return;
                }

                if (ReferralCode != "")
                {   
                    DataTable dtRef = D.GetDataTable("select * from Member where MyReferralCode='" + ReferralCode + "'");

                    if (dtRef.Rows.Count > 0)
                    {
                        DataTable dts = D.GetDataTable("select * from Member where DeviceUniqueId='" + DeviceUniqueId + "' and ReferralCode='" + ReferralCode + "'");

                        if (dts.Rows.Count > 0)
                        {
                            qry = "insert into Member (name,email,password,dob,gender,mobile,address,city,pincode,state,country,MyReferralCode,DeviceUniqueId,OtpVerification) values('" + Name + "','" + email + "','" + password + "','" + dob + "','" + gender + "','" + mobile + "','" + address + "','" + city + "','" + pincode + "','" + state + "','" + country + "','" + MyReferralCode + "','" + DeviceUniqueId + "','" + OtpVerification + "')";

                            D.ExecuteQuery(qry);

                            GeneralRegisterData data = new GeneralRegisterData();
                            data.MESSAGE = "SuccessFully! Referral Code Already Applied For this Device";
                            data.ORIGINAL_ERROR = "SuccessFully! Referral Code Already Applied For this Device";
                            data.ERROR_STATUS = false;
                            data.RECORDS = false;
                            data.Data = 1;
                            g.Add(data);
                            Context.Response.Write(js.Serialize(g[0]));
                            return;
                        }
                        else
                        {
                            qry = "insert into Member (name,email,password,dob,gender,mobile,address,city,pincode,state,country,MyReferralCode,ReferralCode,DeviceUniqueId,OtpVerification) values('" + Name + "','" + email + "','" + password + "','" + dob + "','" + gender + "','" + mobile + "','" + address + "','" + city + "','" + pincode + "','" + state + "','" + country + "','" + MyReferralCode + "','" + ReferralCode + "','" + DeviceUniqueId + "','" + OtpVerification + "')";

                            D.ExecuteQuery(qry);

                            string refMemberId = dtRef.Rows[0]["Id"].ToString();

                            string orderId = DateTime.Now.ToString("yyyyMMddhhmmssffffff");

                            qrys = "insert into Wallet (MemberId,Date,Amount,TransactionType,TransactionDetails,OrderId) values('" + refMemberId + "','" + DateTime.Now + "','10','Credit','Referral Bonous','" + orderId + "')";

                            D.ExecuteQuery(qrys);

                            GeneralRegisterData data = new GeneralRegisterData();
                            data.MESSAGE = "SuccessFully! Referral Code Already Applied For this Device";
                            data.ORIGINAL_ERROR = "SuccessFully! Referral Code Already Applied For this Device";
                            data.ERROR_STATUS = false;
                            data.RECORDS = false;
                            data.Data = 1;
                            g.Add(data);
                            Context.Response.Write(js.Serialize(g[0]));
                            return;
                        }
                    }
                    else
                    {   
                        GeneralRegisterData data = new GeneralRegisterData();
                        data.MESSAGE = "Faild! Invalid Referral Code";
                        data.ORIGINAL_ERROR = "Faild! Invalid Referral Code";
                        data.ERROR_STATUS = false;
                        data.RECORDS = false;
                        data.Data = 0;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                        return;
                    }
                }
                else
                {
                    qry = "insert into Member (name,email,password,dob,gender,mobile,address,city,pincode,state,country,MyReferralCode,DeviceUniqueId,OtpVerification) values('" + Name + "','" + email + "','" + password + "','" + dob + "','" + gender + "','" + mobile + "','" + address + "','" + city + "','" + pincode + "','" + state + "','" + country + "','" + MyReferralCode + "','" + DeviceUniqueId + "','" + OtpVerification + "')";

                    D.ExecuteQuery(qry);

                    GeneralRegisterData data = new GeneralRegisterData();
                    data.MESSAGE = "SuccessFully! Referral Code Already Applied For this Device";
                    data.ORIGINAL_ERROR = "SuccessFully! Referral Code Already Applied For this Device";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    data.Data = 1;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                    return;
                }
            }
            catch (Exception ex)
            {
                GeneralRegisterData data = new GeneralRegisterData();
                data.MESSAGE = "Failed To Insert";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                data.Data = 0;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }

    }

    [WebMethod]
    public void RegisterMember(string type, string Name, string email, string password, string dob, string gender, string mobile, string address, string city, string pincode, string state, string country)
    {
        string qry = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralRegisterData> g = new List<GeneralRegisterData>();
        if (type == "Member")
        {
            try
            {
                qry = "insert into Member (name,email,password,dob,gender,mobile,address,city,pincode,state,country) values('" + Name + "','" + email + "','" + password + "','" + dob + "','" + gender + "','" + mobile + "','" + address + "','" + city + "','" + pincode + "','" + state + "','" + country + "')";

                D.ExecuteQuery(qry);

                GeneralRegisterData data = new GeneralRegisterData();
                data.MESSAGE = "SuccessFully Inserted";
                data.ORIGINAL_ERROR = "SuccessFully Inserted";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                data.Data = 1;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralRegisterData data = new GeneralRegisterData();
                data.MESSAGE = "Failed To Insert";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                data.Data = 0;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void GetLeaguePrice(string type, string leagueid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeaguePrice> list = new List<LeaguePrice>();
        List<GeneralDataLeaguePrice> g = new List<GeneralDataLeaguePrice>();
        if (type == "leagueprice")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("Select * from LeaguePrice where LeagueId=" + leagueid + "");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeaguePrice hc = new LeaguePrice();

                        hc.CouponPrice = dt2.Rows[i]["CouponPrice"].ToString();
                        hc.Price = dt2.Rows[i]["Price"].ToString();
                        hc.PriceType = dt2.Rows[i]["PriceType"].ToString();
                        hc.RankFrom = dt2.Rows[i]["RankFrom"].ToString();
                        hc.RankTo = dt2.Rows[i]["RankTo"].ToString();
                        hc.RankType = dt2.Rows[i]["RankType"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataLeaguePrice data = new GeneralDataLeaguePrice();
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
                    GeneralDataLeaguePrice data = new GeneralDataLeaguePrice();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeaguePrice data = new GeneralDataLeaguePrice();
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
    public void SetLeagueAnswer(string type, string leagueid, string memberid, string questionid, string answer, string timeduration, string isRight)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;

        List<GeneralData> g = new List<GeneralData>();
        if (type == "leagueanswer")
        {
            try
            {
                D.ExecuteQuery("insert into LeagueMemberAnswer(LeagueId,MemberId,QuestionId,Answer,AnswerTimeDuration,isRight) values(" + leagueid + "," + memberid + "," + questionid + ",'" + answer + "'," + timeduration + "," + isRight + ")");

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
    public void SetLeagueAnswerMulti(string type, string Data)
    {
        //Data=[{"leagueid":"1","memberid":"1","questionid":"1","answer":"1","timeduration":"1","isRight":"1"},{"leagueid":"1","memberid":"1","questionid":"1","answer":"1","timeduration":"1","isRight":"1"}]
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        string leagueid = "";
        string memberid = "";
        string questionid = "";
        string answer = "";
        string timeduration = "";
        string isRight = "";
        string pts = "";
        if (type == "leagueanswer")
        {
            try
            {
                int points = 0;
                string url = Data;
                var _Items = js.Deserialize<LeagueAnswer[]>(url);
                int cnt = _Items.Length;
                if (cnt > 0)
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        leagueid = (new System.Web.UI.WebControls.ListItem(_Items[i].leagueid).ToString());
                        memberid = (new System.Web.UI.WebControls.ListItem(_Items[i].memberid).ToString());
                        questionid = (new System.Web.UI.WebControls.ListItem(_Items[i].questionid).ToString());
                        answer = (new System.Web.UI.WebControls.ListItem(_Items[i].answer).ToString());
                        timeduration = (new System.Web.UI.WebControls.ListItem(_Items[i].timeduration).ToString());
                        isRight = (new System.Web.UI.WebControls.ListItem(_Items[i].isRight).ToString());
                        pts = (new System.Web.UI.WebControls.ListItem(_Items[i].point).ToString());
                        D.ExecuteQuery("insert into LeagueMemberAnswer(LeagueId,MemberId,QuestionId,Answer,AnswerTimeDuration,isRight,Points) values(" + leagueid + "," + memberid + "," + questionid + ",N'" + answer + "'," + timeduration + "," + isRight + ",'" + pts + "')");
                        if (isRight == "1")
                        {
                            points += Convert.ToInt32(pts);
                        }
                    }

                    string qry = "update LeagueMember set points=" + points + " where LeagueId=" + leagueid + " and MemberId=" + memberid;
                    D.ExecuteQuery(qry);

                    GeneralData data = new GeneralData();

                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
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
    public void GetLeagueMember(string type, string leagueid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeagueMember> list = new List<LeagueMember>();
        List<GeneralDataLeagueMember> g = new List<GeneralDataLeagueMember>();
        if (type == "leaguemember")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("Select * from LeagueMember as L,Member as M where M.Id=L.MemberId and L.LeagueId=" + leagueid + " order by Points desc");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeagueMember hc = new LeagueMember();

                        hc.MemberId = dt2.Rows[i]["MemberId"].ToString();
                        hc.MemberName = dt2.Rows[i]["Name"].ToString();
                        hc.Payment = dt2.Rows[i]["Payment"].ToString();
                        hc.PaymentId = dt2.Rows[i]["PaymentId"].ToString();

                        hc.Points = dt2.Rows[i]["Points"].ToString();
                        hc.Rank = dt2.Rows[i]["Rank"].ToString();
                        hc.PriceType = dt2.Rows[i]["PriceType"].ToString();
                        hc.Price = dt2.Rows[i]["Price"].ToString();
                        hc.CouponId = dt2.Rows[i]["CouponId"].ToString();
                        hc.CouponRs = dt2.Rows[i]["CouponRs"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataLeagueMember data = new GeneralDataLeagueMember();
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
                    GeneralDataLeagueMember data = new GeneralDataLeagueMember();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueMember data = new GeneralDataLeagueMember();
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
    public void GetWalletDetails(string type, string memberid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<Wallet> list = new List<Wallet>();
        List<GeneralDataWallet> g = new List<GeneralDataWallet>();
        if (type == "walletinfo")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("Select * from Wallet where memberid=" + memberid + "");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        Wallet hc = new Wallet();

                        hc.Amount = dt2.Rows[i]["Amount"].ToString();
                        hc.TransDetails = dt2.Rows[i]["TransactionDetails"].ToString();
                        hc.TransType = dt2.Rows[i]["TransactionType"].ToString();
                        hc.Date = dt2.Rows[i]["Date"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataWallet data = new GeneralDataWallet();
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
                    GeneralDataWallet data = new GeneralDataWallet();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataWallet data = new GeneralDataWallet();
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
    public void GetWalletBalance(string type, string memberid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<WalletBalance> list = new List<WalletBalance>();
        List<GeneralDataWalletBalance> g = new List<GeneralDataWalletBalance>();
        if (type == "walletinfo")
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetMemberWallet");
                cmd.Parameters.AddWithValue("@memberid", memberid);
                DataTable dt2 = D.GetDataTableBySP(cmd);
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        WalletBalance hc = new WalletBalance();

                        hc.WalletAmount = dt2.Rows[i]["WalletAmount"].ToString();
                        hc.CashWin = dt2.Rows[i]["CashWin"].ToString();
                        hc.CouponWin = dt2.Rows[i]["CouponWin"].ToString();

                        list.Add(hc);
                    }
                    GeneralDataWalletBalance data = new GeneralDataWalletBalance();
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
                    GeneralDataWalletBalance data = new GeneralDataWalletBalance();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataWalletBalance data = new GeneralDataWalletBalance();
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
    public void GetLeagueResult(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeagueResult> list = new List<LeagueResult>();
        List<GeneralDataLeagueResult> g = new List<GeneralDataLeagueResult>();
        if (type == "leagueresult")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from LeagueMember");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeagueResult hc = new LeagueResult();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.LeagueId = dt2.Rows[i]["LeagueId"].ToString();
                        hc.MemberId = dt2.Rows[i]["MemberId"].ToString();
                        hc.Points = dt2.Rows[i]["Points"].ToString();
                        hc.Rank = dt2.Rows[i]["Rank"].ToString();
                        hc.PriceType = dt2.Rows[i]["PriceType"].ToString();
                        hc.Price = dt2.Rows[i]["Price"].ToString();
                        hc.CouponId = dt2.Rows[i]["CouponId"].ToString();
                        hc.CouponRs = dt2.Rows[i]["CouponRs"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataLeagueResult data = new GeneralDataLeagueResult();
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
                    GeneralDataLeagueResult data = new GeneralDataLeagueResult();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueResult data = new GeneralDataLeagueResult();
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
    public void GetLeagueQuestions(string type, string typeid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeagueQuestions> list = new List<LeagueQuestions>();
        List<GeneralDataLeagueQuestions> g = new List<GeneralDataLeagueQuestions>();
        if (type == "leaguequestions")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("Select top 10 * from QuestionBank where typeid = " + typeid + " order by newid()");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeagueQuestions hc = new LeagueQuestions();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Question = dt2.Rows[i]["Question"].ToString();
                        hc.OptionA = dt2.Rows[i]["OptionA"].ToString();
                        hc.isOptionA = dt2.Rows[i]["isOptionA"].ToString();
                        hc.OptionB = dt2.Rows[i]["OptionB"].ToString();
                        hc.isOptionB = dt2.Rows[i]["isOptionB"].ToString();
                        hc.OptionC = dt2.Rows[i]["OptionC"].ToString();
                        hc.isOptionC = dt2.Rows[i]["isOptionC"].ToString();
                        hc.OptionD = dt2.Rows[i]["OptionD"].ToString();
                        hc.isOptionD = dt2.Rows[i]["isOptionD"].ToString();
                        hc.Status = dt2.Rows[i]["Status"].ToString();
                        hc.TypeId = dt2.Rows[i]["TypeId"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataLeagueQuestions data = new GeneralDataLeagueQuestions();
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
                    GeneralDataLeagueQuestions data = new GeneralDataLeagueQuestions();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueQuestions data = new GeneralDataLeagueQuestions();
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
    public void GetCoupon(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<Coupons> list = new List<Coupons>();
        List<GeneralDataCoupon> g = new List<GeneralDataCoupon>();
        if (type == "coupon")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from CouponMaster");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        Coupons hc = new Coupons();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Title = dt2.Rows[i]["Title"].ToString();
                        hc.Image = website + dt2.Rows[i]["Image"].ToString();
                        hc.Status = dt2.Rows[i]["Status"].ToString();
                        hc.Amount = dt2.Rows[i]["Amount"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataCoupon data = new GeneralDataCoupon();
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
                    GeneralDataCoupon data = new GeneralDataCoupon();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataCoupon data = new GeneralDataCoupon();
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
    public void GetProductCategory(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<ProductCategoryMaster> list = new List<ProductCategoryMaster>();
        List<GeneralDataProductCategory> g = new List<GeneralDataProductCategory>();
        if (type == "productcategory")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from CategoryMaster");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        ProductCategoryMaster hc = new ProductCategoryMaster();
                        hc.id = dt2.Rows[i]["Id"].ToString();
                        hc.name = dt2.Rows[i]["name"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataProductCategory data = new GeneralDataProductCategory();
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
                    GeneralDataProductCategory data = new GeneralDataProductCategory();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataProductCategory data = new GeneralDataProductCategory();
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
    public void GetProductDetail(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<ProductMaster> list = new List<ProductMaster>();
        List<GeneralDataProductDetail> g = new List<GeneralDataProductDetail>();
        if (type == "productdetail")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from ProductMaster");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        ProductMaster hc = new ProductMaster();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Name = dt2.Rows[i]["Name"].ToString();
                        hc.Description = dt2.Rows[i]["Description"].ToString();
                        hc.Image = website + dt2.Rows[i]["Image"].ToString();
                        hc.Status = dt2.Rows[i]["Status"].ToString();
                        hc.Amount = dt2.Rows[i]["Amount"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataProductDetail data = new GeneralDataProductDetail();
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
                    GeneralDataProductDetail data = new GeneralDataProductDetail();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataProductDetail data = new GeneralDataProductDetail();
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
    public void GetType(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<CategoryMaster> list = new List<CategoryMaster>();
        List<GeneralDataCustomer> g = new List<GeneralDataCustomer>();
        if (type == "type")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from Type");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        CategoryMaster hc = new CategoryMaster();
                        hc.id = dt2.Rows[i]["Id"].ToString();
                        hc.name = dt2.Rows[i]["name"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataCustomer data = new GeneralDataCustomer();
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
                    GeneralDataCustomer data = new GeneralDataCustomer();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataCustomer data = new GeneralDataCustomer();
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
    public void GetLeaguePlayHistory(string type, String memberid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeagueHistory> list = new List<LeagueHistory>();
        List<GeneralDataLeagueHistory> g = new List<GeneralDataLeagueHistory>();
        if (type == "league")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select LM.*,LLM.*,T.Name,T.Timer,(Select count(*) from  LeagueMember as LMM where LM.Id=LMM.LeagueId) as LeagueMemberCount  from  LeagueMaster as LM,type as T,Leaguemember as LLM where LM.Id=LLM.LeagueId and T.Id=LM.TypeId and LLM.MemberId=" + memberid + " order by LM.Date desc");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeagueHistory hc = new LeagueHistory();

                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Title = dt2.Rows[i]["Title"].ToString();
                        hc.TypeName = dt2.Rows[i]["Name"].ToString();
                        hc.Timer = dt2.Rows[i]["Timer"].ToString();
                        hc.PrizeWinner = dt2.Rows[i]["PrizeWinner"].ToString();
                        hc.CouponWinner = dt2.Rows[i]["CouponWinner"].ToString();
                        hc.EntryFee = dt2.Rows[i]["EntryFee"].ToString();
                        hc.PrizePool = dt2.Rows[i]["PrizePool"].ToString();
                        hc.MemberAllow = dt2.Rows[i]["MemberAllow"].ToString();
                        hc.Date = dt2.Rows[i]["Date"].ToString();
                        hc.StartTime = dt2.Rows[i]["StartTime"].ToString();
                        hc.EndTime = dt2.Rows[i]["EndTime"].ToString();
                        hc.CouponPool = dt2.Rows[i]["CouponPool"].ToString();
                        hc.LeagueMemberCount = dt2.Rows[i]["LeagueMemberCount"].ToString();

                        hc.WinType = dt2.Rows[i]["PriceType"].ToString();
                        hc.WinAmt = dt2.Rows[i]["Price"].ToString();
                        hc.Points = dt2.Rows[i]["Points"].ToString();
                        hc.Rank = dt2.Rows[i]["Rank"].ToString();
                        hc.CouponId = dt2.Rows[i]["CouponId"].ToString();
                        hc.CouponRs = dt2.Rows[i]["CouponRs"].ToString();

                        list.Add(hc);

                    }
                    GeneralDataLeagueHistory data = new GeneralDataLeagueHistory();
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
                    GeneralDataLeagueHistory data = new GeneralDataLeagueHistory();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueHistory data = new GeneralDataLeagueHistory();
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
    public void GetLeague(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<League> list = new List<League>();
        List<GeneralDataLeague> g = new List<GeneralDataLeague>();
        if (type == "league")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select LM.*,T.Name,T.Timer,(Select count(*) from LeagueMember as LMM where LM.Id=LMM.LeagueId) as LeagueMemberCount from LeagueMaster as LM,type as T where T.Id=LM.TypeId and LM.Date=cast(getdate() as date) and CONVERT(TIME, GETDATE()) between lm.StartTime and lm.EndTime and (isresultgenerated = 0 or isresultgenerated is null) order by lm.Date desc");
                //DataTable dt2 = D.GetDataTable("select LM.*,T.Name,T.Timer,(Select count(*) from LeagueMember as LMM where LM.Id=LMM.LeagueId) as LeagueMemberCount from LeagueMaster as LM,type as T where T.Id=LM.TypeId and LM.Date=cast(getdate() as date) and CONVERT(TIME, GETDATE()) between lm.StartTime and lm.EndTime and (isresultgenerated = 0 or isresultgenerated is null) order by lm.Date desc,lm.ID desc");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        League hc = new League();

                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Title = dt2.Rows[i]["Title"].ToString();
                        hc.TypeId = dt2.Rows[i]["TypeId"].ToString();
                        hc.TypeName = dt2.Rows[i]["Name"].ToString();
                        hc.Timer = dt2.Rows[i]["Timer"].ToString();
                        hc.PrizeWinner = dt2.Rows[i]["PrizeWinner"].ToString();
                        hc.CouponWinner = dt2.Rows[i]["CouponWinner"].ToString();
                        hc.EntryFee = dt2.Rows[i]["EntryFee"].ToString();
                        hc.PrizePool = dt2.Rows[i]["PrizePool"].ToString();
                        hc.MemberAllow = dt2.Rows[i]["MemberAllow"].ToString();
                        hc.Date = dt2.Rows[i]["Date"].ToString();
                        hc.StartTime = dt2.Rows[i]["StartTime"].ToString();
                        hc.EndTime = dt2.Rows[i]["EndTime"].ToString();
                        hc.CouponPool = dt2.Rows[i]["CouponPool"].ToString();
                        hc.LeagueMemberCount = dt2.Rows[i]["LeagueMemberCount"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataLeague data = new GeneralDataLeague();
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
                    GeneralDataLeague data = new GeneralDataLeague();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeague data = new GeneralDataLeague();
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
    public void JoinLeague(string type, string MemberId, string LeagueId, string Payment)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralDataLeagueJoin> g = new List<GeneralDataLeagueJoin>();
        string id = "";

        if (type == "joinleague")
        {
            try
            {
                DataTable dt = D.GetDataTable("select * from leaguemember where memberid = " + MemberId + " and leagueid = " + LeagueId);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("JoinLeague");
                    cmd.Parameters.AddWithValue("@memberid", MemberId);
                    cmd.Parameters.AddWithValue("@leagueid", LeagueId);
                    cmd.Parameters.AddWithValue("@leagueamt", Payment);
                    cmd.Parameters.Add("@paymentid", SqlDbType.Int).Direction = ParameterDirection.Output;
                    id = Convert.ToString(D.ExecuteQueryBySpWithReturn(cmd));

                    if (id != "0")
                    {
                        GeneralDataLeagueJoin data = new GeneralDataLeagueJoin();
                        data.MESSAGE = "Successfully Joined!";
                        data.ORIGINAL_ERROR = "Successfully Joined!";
                        data.ERROR_STATUS = false;
                        data.RECORDS = true;
                        data.Data = id;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                    else
                    {
                        GeneralDataLeagueJoin data = new GeneralDataLeagueJoin();
                        data.MESSAGE = "Please Add Balance To Wallet!";
                        data.ORIGINAL_ERROR = "Please Add Balance To Wallet!";
                        data.ERROR_STATUS = false;
                        data.RECORDS = false;
                        g.Add(data);
                        Context.Response.Write(js.Serialize(g[0]));
                    }
                }
                else
                {
                    GeneralDataLeagueJoin data = new GeneralDataLeagueJoin();
                    data.MESSAGE = "You already joined this league.";
                    data.ORIGINAL_ERROR = "You already joined this league.";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueJoin data = new GeneralDataLeagueJoin();
                data.MESSAGE = "Failed To Join";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void CheckLeagueJoin(string type, string MemberId, string LeagueId)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        GeneralDataCheckLeagueJoin g = new GeneralDataCheckLeagueJoin();
        if (type == "chkleaguejoin")
        {
            try
            {
                DataTable dt = D.GetDataTable("select * from leaguemember where memberid = " + MemberId + " and leagueid = " + LeagueId);
                if (dt.Rows.Count != 0)
                {
                    g.MESSAGE = "You already joined this league.";
                    g.ORIGINAL_ERROR = "";
                    g.ERROR_STATUS = false;
                    g.RECORDS = true;
                    g.Data = 1;
                    Context.Response.Write(js.Serialize(g));
                }
                else
                {
                    g.MESSAGE = "You have not join this league yet.";
                    g.ORIGINAL_ERROR = "";
                    g.ERROR_STATUS = false;
                    g.RECORDS = false;
                    Context.Response.Write(js.Serialize(g));
                }
            }
            catch (Exception ex)
            {
                g.MESSAGE = "Failed";
                g.ORIGINAL_ERROR = ex.Message;
                g.ERROR_STATUS = true;
                g.RECORDS = false;
                Context.Response.Write(js.Serialize(g));
            }
        }
    }

    [WebMethod]
    public void GetLeagueMemberAnswer(string type, string memberId, string leagueId)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<LeagueMemberAnswer> list = new List<LeagueMemberAnswer>();
        List<GeneralDataLeagueMemberAnswer> g = new List<GeneralDataLeagueMemberAnswer>();
        if (type == "leaguememberanswer")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select lma.id,qb.Question,lma.Answer,lma.AnswerTimeDuration,lma.isRight, (case when qb.IsOptionA <> 0 then qb.OptionA when qb.IsOptionB <> 0 then qb.OptionB when qb.IsOptionC <> 0 then qb.OptionC else qb.OptionD end) as RightAnswer from leaguememberanswer lma,QuestionBank qb where lma.Questionid = qb.id and lma.leagueid=" + leagueId + " and lma.memberid=" + memberId);
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        LeagueMemberAnswer hc = new LeagueMemberAnswer();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Question = dt2.Rows[i]["Question"].ToString();
                        hc.Answer = dt2.Rows[i]["Answer"].ToString();
                        hc.AnswerTimeDuration = dt2.Rows[i]["AnswerTimeDuration"].ToString();
                        hc.IsRight = dt2.Rows[i]["IsRight"].ToString();
                        hc.RightAnswer = dt2.Rows[i]["RightAnswer"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataLeagueMemberAnswer data = new GeneralDataLeagueMemberAnswer();
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
                    GeneralDataLeagueMemberAnswer data = new GeneralDataLeagueMemberAnswer();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataLeagueMemberAnswer data = new GeneralDataLeagueMemberAnswer();
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
    public void GetTransactionHistory(string type, string memberId)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<TransactionHistory> list = new List<TransactionHistory>();
        List<GeneralDataTransactionHistory> g = new List<GeneralDataTransactionHistory>();
        if (type == "transactionhistory")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from [dbo].[Wallet] where memberid = " + memberId + " order by id desc");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        TransactionHistory hc = new TransactionHistory();
                        hc.Id = dt2.Rows[i]["Id"].ToString();
                        hc.Amount = dt2.Rows[i]["Amount"].ToString();
                        hc.Date = Convert.ToDateTime(dt2.Rows[i]["Date"]).ToString("dd-MM-yyyy");
                        hc.Type = dt2.Rows[i]["TransactionType"].ToString();
                        hc.Title = dt2.Rows[i]["TransactionDetails"].ToString();
                        hc.OrderId = dt2.Rows[i]["OrderId"].ToString();
                        list.Add(hc);

                    }
                    GeneralDataTransactionHistory data = new GeneralDataTransactionHistory();
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
                    GeneralDataTransactionHistory data = new GeneralDataTransactionHistory();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataTransactionHistory data = new GeneralDataTransactionHistory();
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
    public void GetBanner(string type)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<MainBanners> list = new List<MainBanners>();
        List<GeneralDataMainBanners> g = new List<GeneralDataMainBanners>();
        if (type == "banner")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from BannerMaster");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        MainBanners ban = new MainBanners();
                        ban.Id = dt2.Rows[i]["Id"].ToString();
                        ban.Image = website + dt2.Rows[i]["Image"].ToString();
                        list.Add(ban);
                    }
                    GeneralDataMainBanners data = new GeneralDataMainBanners();
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
                    GeneralDataMainBanners data = new GeneralDataMainBanners();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataMainBanners data = new GeneralDataMainBanners();
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
    public void UpdateProfile(string type, string id, string name, string email, string dob, string gender, string mobile, string address, string city, string pincode, string state, string country)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "update")
        {
            try
            {
                D.ExecuteQuery("update Member set name='" + name + "',email='" + email + "',dob='" + dob + "',gender='" + gender + "',mobile='" + mobile + "',address='" + address + "',city='" + city + "',pincode='" + pincode + "',state='" + state + "',country='" + country + "' where Id=" + id);
                //DataTable dt = D.GetDataTable("select id from CustomerMaster where name = '" + Name + "' order by id desc");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully Updated!";
                data.ORIGINAL_ERROR = "Successfully Updated!";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));


            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed To Update";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }


    [WebMethod]
    public void UpdatePassword(string type, string id, string password)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "updatepass")
        {
            try
            {
                D.ExecuteQuery("update Member set password='" + password + "' where Id=" + id);
                //DataTable dt = D.GetDataTable("select id from CustomerMaster where name = '" + Name + "' order by id desc");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully Updated!";
                data.ORIGINAL_ERROR = "Successfully Updated!";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));


            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed To Update";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void GetCategory(string type)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<Category> list = new List<Category>();
        List<GeneralDataCategory> g = new List<GeneralDataCategory>();
        if (type == "getcat")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from CategoryMaster");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        Category ban = new Category();
                        ban.Id = dt2.Rows[i]["Id"].ToString();
                        ban.Name = dt2.Rows[i]["Name"].ToString();
                        list.Add(ban);
                    }
                    GeneralDataCategory data = new GeneralDataCategory();
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
                    GeneralDataCategory data = new GeneralDataCategory();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataCategory data = new GeneralDataCategory();
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
    public void GetProductImage(string type, string id)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<ProductImage> list = new List<ProductImage>();
        List<GeneralDataProductImage> g = new List<GeneralDataProductImage>();
        if (type == "proimage")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select * from ProductImageMaster where ProductId=" + id);
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        ProductImage ban = new ProductImage();
                        ban.Id = dt2.Rows[i]["Id"].ToString();
                        ban.Image = website + dt2.Rows[i]["Image"].ToString();
                        list.Add(ban);
                    }
                    GeneralDataProductImage data = new GeneralDataProductImage();
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
                    GeneralDataProductImage data = new GeneralDataProductImage();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataProductImage data = new GeneralDataProductImage();
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
    public void GetProductDetails(string type, string catid)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<ProductDetails> list = new List<ProductDetails>();
        List<GeneralDataProductDetails> g = new List<GeneralDataProductDetails>();
        if (type == "prodec")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select pm.*,(select top 1 Image from ProductImageMaster pmi where pmi.productid = pm.id) as ProImage from ProductMaster pm where pm.Category_Id=" + catid);
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        ProductDetails ban = new ProductDetails();
                        ban.Id = dt2.Rows[i]["Id"].ToString();
                        ban.Name = dt2.Rows[i]["Name"].ToString();
                        ban.Description = dt2.Rows[i]["Description"].ToString();
                        ban.Amount = dt2.Rows[i]["Amount"].ToString();
                        ban.Status = dt2.Rows[i]["Status"].ToString();
                        ban.Image = dt2.Rows[i]["ProImage"] != System.DBNull.Value ? website + dt2.Rows[i]["ProImage"].ToString() : "";
                        list.Add(ban);
                    }
                    GeneralDataProductDetails data = new GeneralDataProductDetails();
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
                    GeneralDataProductDetails data = new GeneralDataProductDetails();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataProductDetails data = new GeneralDataProductDetails();
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
    public void AddWithdrawal(string type, string MemberId, string Amount)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "with")
        {
            try
            {

                DateTime dt = System.DateTime.Now;

                D.ExecuteQuery("insert into Withdrawal (date,amount,status,memberid,Type) values( '" + dt + "','" + Amount + "','Pending','" + MemberId + "','Debit')");
                //DataTable dt = D.GetDataTable("select id from CustomerMaster where name = '" + Name + "' order by id desc");
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully Inserted!";
                data.ORIGINAL_ERROR = "Successfully Inserted!";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));


            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed To Insert";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }

    }

    [WebMethod]
    public void GetMemberCoupon(string type, string memberid)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        int i = 0;
        List<CouponDetails> list = new List<CouponDetails>();
        List<GeneralDataCouponDetails> g = new List<GeneralDataCouponDetails>();
        if (type == "coup")
        {
            try
            {
                DataTable dt2 = D.GetDataTable("select cm.* from LeagueMember as lm,CouponMaster as cm where lm.CouponId=cm.Id and lm.MemberID=" + memberid + " and lm.CouponId !=0 and lm.CouponId Is not null");
                if (dt2.Rows.Count != 0)
                {
                    for (i = 0; i <= dt2.Rows.Count - 1; i++)
                    {
                        CouponDetails ban = new CouponDetails();
                        ban.Id = dt2.Rows[i]["Id"].ToString();
                        ban.Title = dt2.Rows[i]["Title"].ToString();
                        ban.Image = dt2.Rows[i]["Image"] != System.DBNull.Value ? website + dt2.Rows[i]["Image"].ToString() : "";
                        list.Add(ban);
                    }
                    GeneralDataCouponDetails data = new GeneralDataCouponDetails();
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
                    GeneralDataCouponDetails data = new GeneralDataCouponDetails();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataCouponDetails data = new GeneralDataCouponDetails();
                data.MESSAGE = "Failed";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    #region Notification

    [WebMethod]
    public void GetMemberNotification(string type, string memberid)
    {
        int i = 0;
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<MemberNotificaiton> list = new List<MemberNotificaiton>();
        List<GeneralDataMemberNotificaiton> g = new List<GeneralDataMemberNotificaiton>();

        if (type == "membernotification")
        {
            try
            {
                DataTable dt = D.GetDataTable("select mn.id,mn.isReaded,nm.Title,nm.Description,nm.Image from Member_Notification as mn left join Notification_Master as nm on nm.Id=mn.NotificationId where MemberId='" + memberid + "' order by isReaded,nm.Id desc ");
                if (dt.Rows.Count != 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        MemberNotificaiton mm = new MemberNotificaiton();
                        mm.id = dt.Rows[i]["Id"].ToString();
                        mm.isReaded = dt.Rows[i]["isReaded"].ToString();
                        mm.Title = dt.Rows[i]["Title"].ToString();
                        mm.Description = dt.Rows[i]["Description"].ToString();
                        string path = "http://dealjeeto.itfuturz.com/";
                        mm.Image = path + dt.Rows[i]["Image"].ToString();

                        list.Add(mm);
                    }
                    GeneralDataMemberNotificaiton data = new GeneralDataMemberNotificaiton();
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
                    GeneralDataMemberNotificaiton data = new GeneralDataMemberNotificaiton();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataMemberNotificaiton data = new GeneralDataMemberNotificaiton();
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
    public void GetMemberNotificationCount(string type, string memberid)
    {
        int i = 0;
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<MemberNotificaitonCount> lists = new List<MemberNotificaitonCount>();
        List<GeneralDataMemberNotificaitonCount> g = new List<GeneralDataMemberNotificaitonCount>();

        int count = 0;

        if (type == "membercount")
        {
            try
            {
                DataTable dt = D.GetDataTable("select COUNT(*) as Count from Member_Notification where MemberId='" + memberid + "' and isReaded=0 ");
                if (dt.Rows.Count != 0)
                {
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        MemberNotificaitonCount mm = new MemberNotificaitonCount();

                        //mm.count = dt.Rows[i]["Count"].ToString();

                        count = Convert.ToInt32(dt.Rows[i]["Count"]);

                        //lists.Add(mm);
                    }
                    GeneralDataMemberNotificaitonCount data = new GeneralDataMemberNotificaitonCount();
                    data.MESSAGE = "Successfully !";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = true;
                    data.Data = count;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
                else
                {
                    GeneralDataMemberNotificaitonCount data = new GeneralDataMemberNotificaitonCount();
                    data.MESSAGE = "Data Not Available";
                    data.ORIGINAL_ERROR = "";
                    data.ERROR_STATUS = false;
                    data.RECORDS = false;
                    g.Add(data);
                    Context.Response.Write(js.Serialize(g[0]));
                }
            }
            catch (Exception ex)
            {
                GeneralDataMemberNotificaitonCount data = new GeneralDataMemberNotificaitonCount();
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
    public void DeleteMemberNotification(string type, string membernotificationid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "deletemember")
        {
            try
            {
                D.ExecuteQuery("delete from Member_Notification where id=" + membernotificationid);
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully Delete!";
                data.ORIGINAL_ERROR = "Successfully Delete!";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed To Delete";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    [WebMethod]
    public void UpdateMemberNotification(string type, string membernotificationid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<GeneralData> g = new List<GeneralData>();
        if (type == "updatemembernotification")
        {
            try
            {
                D.ExecuteQuery("update Member_Notification set isReaded=1 where id=" + membernotificationid);
                GeneralData data = new GeneralData();
                data.MESSAGE = "Successfully Update!";
                data.ORIGINAL_ERROR = "Successfully update!";
                data.ERROR_STATUS = false;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
            catch (Exception ex)
            {
                GeneralData data = new GeneralData();
                data.MESSAGE = "Failed To Update";
                data.ORIGINAL_ERROR = ex.Message;
                data.ERROR_STATUS = true;
                data.RECORDS = false;
                g.Add(data);
                Context.Response.Write(js.Serialize(g[0]));
            }
        }
    }

    #endregion

}