using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Data;
using MedicalDatasetTableAdapters;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for MedicalService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MedicalService : System.Web.Services.WebService
{
    GetData D = new GetData();
    CustomerMasterTableAdapter _user = new CustomerMasterTableAdapter();
    PincodeMasterTableAdapter _pincode = new PincodeMasterTableAdapter();
    CityMasterTableAdapter _city = new CityMasterTableAdapter();
    StateMasterTableAdapter _state = new StateMasterTableAdapter();
    CategoryMasterTableAdapter _category = new CategoryMasterTableAdapter();
    ItemMasterTableAdapter _item = new ItemMasterTableAdapter();
    LanguageMasterTableAdapter _lang = new LanguageMasterTableAdapter();
    LabelMasterTableAdapter _label = new LabelMasterTableAdapter();
    LanguageLabelsTableAdapter _ll = new LanguageLabelsTableAdapter();
    StatusMasterTableAdapter _status = new StatusMasterTableAdapter();
    CustomerOrderTableAdapter _custorder = new CustomerOrderTableAdapter();
    CustOrderDetailTableAdapter _custorderdetail = new CustOrderDetailTableAdapter();
    PolicyMasterTableAdapter _policy = new PolicyMasterTableAdapter();
    PrescriptionEntryTableAdapter _presc_entry = new PrescriptionEntryTableAdapter();
    FeedbackMasterTableAdapter _feed_mst = new FeedbackMasterTableAdapter();

    public MedicalService()
    {
        string ref_point = "0";
        string ref_limit = "0";
        DataTable dt = D.GetDataTable("select * from GeneralSetting");
        if (dt.Rows.Count > 0)
        {
            ref_limit = dt.Rows[0]["ReferralLimit"].ToString();
            ref_point = dt.Rows[0]["ReferralPoint"].ToString();
        }
        Application["referral_point"] = ref_point;
        Application["default_points"] = 0;
        Application["referral_capping"] = 1;
        Application["recharge_api_key"] = "378833141386212";
        Application["copyright"] = "pmr2015";
        Application["AppVersion"] = "156";
        Application["referral_limit"] = ref_limit;
        Application["call"] = "webview";
        Application["api_key"] = "1234567890";
        //InitializeComponent(); 
    }

    private string md5(string sPassword)
    {
        MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }

    [WebMethod(Description = "Get Mobile Verification")]
    public void GetMobileVerification(string action, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "mobverify")
        {
            DataTable dt = _user.GetCustomerByID(Convert.ToInt32(userid));
            if (dt.Rows.Count > 0)
            {

                D.fun_sendOTP("Your Verification Code is " + dt.Rows[0]["OTP"].ToString(), dt.Rows[0]["MobileNo"].ToString());
                List<Message> mg = new List<Message>();
                Message Msg = new Message();
                Msg.msg = "Message Sent";
                mg.Add(Msg);
                Context.Response.Write(js.Serialize(Msg));
                return;
            }
            else
            {
                List<Message> mg = new List<Message>();
                Message Msg = new Message();
                Msg.msg = "Message Not Sent";
                mg.Add(Msg);
                Context.Response.Write(js.Serialize(Msg));
                return;
            }
        }
    }


    [WebMethod]
    public void UpdateStatus(string action, string mobile, string device_id)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string userid = "";
        string myencrypt = "";
        if (action == "update")
        {

            DataTable dtemail = _user.GetCustomerByMobnoAndDevice(mobile, device_id);
            if (dtemail.Rows.Count > 0)
            {
                userid = dtemail.Rows[0]["CustId"].ToString();

                //update OTP 
                D.ExecuteQuery("update customermaster set Status=1 where CustId=" + userid);
            }
            Context.Response.Write("Success");
        }
        else
        {
            Context.Response.Write("Failed");
        }
    }

    [WebMethod]
    public void SignupCall(string action, string mobile, string device_type, string device_id, string enc, string otp)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string userid = "";
        string myencrypt = "";
        if (action == "signup")
        {
            myencrypt = md5(mobile + "medi");
            if (device_type == "ios")
            {
                enc = md5(mobile + "medi");
            }
            if (enc == myencrypt)
            {
                DataTable dtemail = D.GetDataTable("select * from CustomerMaster where MobileNo='" + mobile + "'"); //_user.GetCustomerByMobnoAndDevice(mobile, device_id);
                if (dtemail.Rows.Count > 0)
                {
                    userid = dtemail.Rows[0]["CustId"].ToString();

                    //update OTP 
                    D.ExecuteQuery("update customermaster set OTP='" + otp + "' where CustId=" + userid);

                    DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(userid));
                    List<Customers> list = new List<Customers>();
                    if (Dt.Rows.Count > 0)
                    {
                        Customers users = new Customers();
                        users.CustId = Dt.Rows[0]["CustId"].ToString();
                        users.Name = Dt.Rows[0]["Name"].ToString();
                        users.Address = Dt.Rows[0]["Address"].ToString();
                        users.Email = Dt.Rows[0]["Email"].ToString();
                        users.MobileNo = Dt.Rows[0]["MobileNo"].ToString();
                        users.Pincode = Dt.Rows[0]["Pincode"].ToString();
                        if (Dt.Rows[0]["ReferralCode"].ToString() != "")
                        {
                            users.ReferralCode = Dt.Rows[0]["ReferralCode"].ToString();
                        }
                        else
                        {
                            users.ReferralCode = "";
                        }
                        if (Dt.Rows[0]["Area"].ToString() != "")
                        {
                            users.Area = Dt.Rows[0]["Area"].ToString();
                        }
                        else
                        {
                            users.Area = "";
                        }
                        if (Dt.Rows[0]["CityId"].ToString() != "")
                        {
                            users.CityId = Dt.Rows[0]["CityId"].ToString();
                        }
                        else
                        {
                            users.CityId = "0";
                        }

                        if (Dt.Rows[0]["CountryId"].ToString() != "")
                        {
                            users.CountryId = Dt.Rows[0]["CountryId"].ToString();
                        }
                        else
                        {
                            users.CountryId = "0";
                        }

                        if (Dt.Rows[0]["StateId"].ToString() != "")
                        {
                            users.StateId = Dt.Rows[0]["StateId"].ToString();
                        }
                        else
                        {
                            users.StateId = "0";
                        }
                        users.DeviceId = Dt.Rows[0]["DeviceId"].ToString();
                        users.DeviceType = Dt.Rows[0]["DeviceType"].ToString();
                        users.Status = Dt.Rows[0]["Status"].ToString();
                        users.NewUser = "0";
                        list.Add(users);
                        Context.Response.Write(js.Serialize(list));
                        return;
                    }
                }

                //DataTable dtemailchk = _user.GetCustomerByMobnoOrDevice(mobile, device_id);
                //if (dtemailchk.Rows.Count > 0)
                //{
                //    DataTable dtckemail = D.GetDataTable("select CustId from CustomerMaster where MobileNo='" + mobile + "'");
                //    if (dtckemail.Rows.Count > 0)
                //    {
                //        userid = dtckemail.Rows[0]["CustId"].ToString();
                //        DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(userid));
                //        List<Customers> list = new List<Customers>();
                //        if (Dt.Rows.Count > 0)
                //        {
                //            Customers users = new Customers();
                //            users.CustId = Dt.Rows[0]["CustId"].ToString();
                //            users.Name = Dt.Rows[0]["Name"].ToString();
                //            users.Address = Dt.Rows[0]["Address"].ToString();
                //            users.Email = Dt.Rows[0]["Email"].ToString();
                //            users.MobileNo = Dt.Rows[0]["MobileNo"].ToString();
                //            users.Pincode = Dt.Rows[0]["Pincode"].ToString();
                //            if (Dt.Rows[0]["ReferralCode"].ToString() != "")
                //            {
                //                users.ReferralCode = Dt.Rows[0]["ReferralCode"].ToString();
                //            }
                //            else
                //            {
                //                users.ReferralCode = "";
                //            }
                //            if (Dt.Rows[0]["Area"].ToString() != "")
                //            {
                //                users.Area = Dt.Rows[0]["Area"].ToString();
                //            }
                //            else
                //            {
                //                users.Area = "";
                //            }
                //            if (Dt.Rows[0]["CityId"].ToString() != "")
                //            {
                //                users.CityId = Dt.Rows[0]["CityId"].ToString();
                //            }
                //            else
                //            {
                //                users.CityId = "0";
                //            }

                //            if (Dt.Rows[0]["CountryId"].ToString() != "")
                //            {
                //                users.CountryId = Dt.Rows[0]["CountryId"].ToString();
                //            }
                //            else
                //            {
                //                users.CountryId = "0";
                //            }

                //            if (Dt.Rows[0]["StateId"].ToString() != "")
                //            {
                //                users.StateId = Dt.Rows[0]["StateId"].ToString();
                //            }
                //            else
                //            {
                //                users.StateId = "0";
                //            }
                //            users.DeviceId = Dt.Rows[0]["DeviceId"].ToString();
                //            users.DeviceType = Dt.Rows[0]["DeviceType"].ToString();
                //            users.Status = Dt.Rows[0]["Status"].ToString();
                //            users.NewUser = "1";
                //            list.Add(users);
                //            Context.Response.Write(js.Serialize(list));
                //            return;
                //        }
                //        //List<Message> list = new List<Message>();
                //        //Message Msg = new Message();
                //        //Msg.msg = "Mobile Number is already exist.";
                //        //list.Add(Msg);
                //        //Context.Response.Write(js.Serialize(Msg));
                //        //Context.Response.Write("Mobile Number is already exist.");
                //        return;
                //    }
                //    //else
                //    //{
                //    //    //List<Message> list = new List<Message>();
                //    //    //Message Msg = new Message();
                //    //    //Msg.msg = "You are already registered with this device";
                //    //    //list.Add(Msg);
                //    //    //Context.Response.Write(js.Serialize(Msg));
                //    //    Context.Response.Write("You are already registered with this device");
                //    //    return;
                //    //}
                //}
                else
                {
                    DateTime dateAndTime = DateTime.Now;
                    if (device_type == "ios" || device_type == "and")
                    {
                        DataTable dtquery = D.GetDataTable("SELECT  LEFT(NEWID(),8) as refcode");
                        string uniqueid = dtquery.Rows[0]["refcode"].ToString();

                        if (device_type == "and") //&& device_id.Length == 15
                        {
                            _user.Insert("", mobile, "", "", "", "", 0, 0, 0, device_id, device_type, otp, 0, "", uniqueid, 0);
                            //D.ExecuteQuery("INSERT INTO manage_users (email, password, status, created_date, usertype, refpoints, refcapping, deviceid, device_type, referral_code,reviewpoints) VALUES('" + email + "', '" + pwd + "' ,'active','" + dateAndTime + "','0','" + points + "','" + capping + "','" + device_id + "','" + device_type + "','" + uniqueid + "',0)");
                        }
                        else if (device_type == "ios") //&& device_id.Length == 64
                        {
                            _user.Insert("", mobile, "", "", "", "", 0, 0, 0, device_id, device_type, otp, 0, "", uniqueid, 0);
                        }
                        else
                        {
                            return;
                        }
                    }
                    DataTable dtemailnew = _user.GetCustomerByMobnoAndDevice(mobile, device_id);
                    if (dtemailnew.Rows.Count > 0)
                    {
                        userid = dtemailnew.Rows[0]["CustId"].ToString();
                        DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(userid));
                        List<Customers> list = new List<Customers>();
                        if (Dt.Rows.Count > 0)
                        {
                            Customers users = new Customers();
                            users.CustId = Dt.Rows[0]["CustId"].ToString();
                            users.Name = Dt.Rows[0]["Name"].ToString();
                            users.Address = Dt.Rows[0]["Address"].ToString();
                            users.Email = Dt.Rows[0]["Email"].ToString();
                            users.MobileNo = Dt.Rows[0]["MobileNo"].ToString();
                            users.Pincode = Dt.Rows[0]["Pincode"].ToString();
                            if (Dt.Rows[0]["ReferralCode"].ToString() != "")
                            {
                                users.ReferralCode = Dt.Rows[0]["ReferralCode"].ToString();
                            }
                            else
                            {
                                users.ReferralCode = "";
                            }
                            if (Dt.Rows[0]["Area"].ToString() != "")
                            {
                                users.Area = Dt.Rows[0]["Area"].ToString();
                            }
                            else
                            {
                                users.Area = "";
                            }
                            if (Dt.Rows[0]["CityId"].ToString() != "")
                            {
                                users.CityId = Dt.Rows[0]["CityId"].ToString();
                            }
                            else
                            {
                                users.CityId = "0";
                            }

                            if (Dt.Rows[0]["CountryId"].ToString() != "")
                            {
                                users.CountryId = Dt.Rows[0]["CountryId"].ToString();
                            }
                            else
                            {
                                users.CountryId = "0";
                            }

                            if (Dt.Rows[0]["StateId"].ToString() != "")
                            {
                                users.StateId = Dt.Rows[0]["StateId"].ToString();
                            }
                            else
                            {
                                users.StateId = "0";
                            }
                            users.DeviceId = Dt.Rows[0]["DeviceId"].ToString();
                            users.DeviceType = Dt.Rows[0]["DeviceType"].ToString();
                            users.Status = Dt.Rows[0]["Status"].ToString();
                            users.NewUser = "1";
                            list.Add(users);
                            Context.Response.Write(js.Serialize(list));
                            return;
                        }
                    }
                }
            }
            else
            {
                Context.Response.Write("[]");
                return;
            }
        }
    }


    [WebMethod]
    public void SigninCall(string mobile, string device_type, string device_token)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string wherecondition = "";
        if (device_type == "ios")
        {
            if (device_token == "")
            {
                List<Message> mg = new List<Message>();
                Message Msg = new Message();
                Msg.msg = "device token can not be blank";
                mg.Add(Msg);
                Context.Response.Write(js.Serialize(Msg));
                return;
            }
        }
        DataTable Dt = D.GetQuery("CustomerMaster", "*", " MobileNo='" + mobile + "' ");
        List<Customers> list = new List<Customers>();
        if (Dt.Rows.Count > 0)
        {
            if (device_token != "" && device_type == "ios")
            {
                wherecondition = " ,deviceid='" + device_token + "'";
            }
            else if (device_token != "" && device_type != "ios")
            {
                wherecondition = " ,deviceid='" + device_token + "'";
            }
            D.ExecuteQuery("update CustomerMaster set DeviceType='" + device_type + "' " + wherecondition + " where CustId='" + Dt.Rows[0]["CustId"].ToString() + "'");

            Customers users = new Customers();
            users.CustId = Dt.Rows[0]["CustId"].ToString();
            users.Name = Dt.Rows[0]["Name"].ToString();
            users.Address = Dt.Rows[0]["Address"].ToString();
            users.Email = Dt.Rows[0]["Email"].ToString();
            users.MobileNo = Dt.Rows[0]["MobileNo"].ToString();
            users.Pincode = Dt.Rows[0]["Pincode"].ToString();

            //if (Dt.Rows[0]["ReferralCode"].ToString() != "")
            //{
            //    users.ReferralCode = Dt.Rows[0]["ReferralCode"].ToString();
            //}
            //else
            //{
            //    users.ReferralCode = "";
            //}
            if (Dt.Rows[0]["Area"].ToString() != "")
            {
                users.Area = Dt.Rows[0]["Area"].ToString();
            }
            else
            {
                users.Area = "";
            }
            if (Dt.Rows[0]["CityId"].ToString() != "")
            {
                users.CityId = Dt.Rows[0]["CityId"].ToString();
            }
            else
            {
                users.CityId = "0";
            }

            if (Dt.Rows[0]["CountryId"].ToString() != "")
            {
                users.CountryId = Dt.Rows[0]["CountryId"].ToString();
            }
            else
            {
                users.CountryId = "0";
            }

            if (Dt.Rows[0]["StateId"].ToString() != "")
            {
                users.StateId = Dt.Rows[0]["StateId"].ToString();
            }
            else
            {
                users.StateId = "0";
            }
            users.DeviceId = Dt.Rows[0]["DeviceId"].ToString();
            users.DeviceType = Dt.Rows[0]["DeviceType"].ToString();
            users.NewUser = "0";
            list.Add(users);
        }
        else
        {
            List<Message> mg = new List<Message>();
            Message Msg = new Message();
            Msg.msg = "Invalid Mobile Number.";
            mg.Add(Msg);
            Context.Response.Write(js.Serialize(Msg));
            return;
        }
        Context.Response.Write(js.Serialize(list));
    }

    [WebMethod(Description = "Get Social Login")]
    public void GetSocialLogin(string action, string email, string device_type, string device_id, string name, string enc, string otp)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<SocialLogin> list = new List<SocialLogin>();
        string userid = "";
        DataTable dtemailnew = null;
        DataTable dtemail = null;
        DataTable dtemailchk = null;
        DataTable dtckemail = null;
        SocialLogin login = new SocialLogin();
        string myencrypt = "";
        if (action == "sociallogin")
        {
            myencrypt = md5(email + "medi");
            if (device_type == "ios")
            {
                enc = md5(email + "medi");
            }
            if (enc == myencrypt)
            {

                if (device_type == "and" || device_type == "ios")
                {
                    dtemail = _user.GetCustIdByEmailAndDevice(email, device_id);
                }
                if (dtemail.Rows.Count > 0)
                {
                    userid = dtemail.Rows[0]["CustId"].ToString();
                    //update OTP 
                    D.ExecuteQuery("update customermaster set OTP='" + otp + "' where CustId=" + userid);
                    DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(userid));
                    if (Dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in Dt.Rows)
                        {
                            login.CustId = row["CustId"].ToString();
                            login.Email = email;
                            if (row["Name"].ToString() != "")
                            {
                                login.Name = row["Name"].ToString();
                            }
                            else
                            {
                                login.Name = "";
                            }
                            if (row["Address"].ToString() != "")
                            {
                                login.Address = row["Address"].ToString();
                            }
                            else
                            {
                                login.Address = "";
                            }

                            if (row["MobileNo"].ToString() != "")
                            {
                                login.MobileNo = row["MobileNo"].ToString();
                            }
                            else
                            {
                                login.MobileNo = "";
                            }


                            if (row["Area"].ToString() != "")
                            {
                                login.Area = row["Area"].ToString();
                            }
                            else
                            {
                                login.Area = "";
                            }
                            if (row["CountryId"].ToString() != "")
                            {
                                login.CountryId = row["CountryId"].ToString();
                            }
                            else
                            {
                                login.CountryId = "0";
                            }
                            if (row["StateId"].ToString() != "")
                            {
                                login.StateId = row["StateId"].ToString();
                            }
                            else
                            {
                                login.StateId = "0";
                            }
                            if (row["CityId"].ToString() != "")
                            {
                                login.CityId = row["CityId"].ToString();
                            }
                            else
                            {
                                login.CityId = "0";
                            }
                            if (row["ReferralCode"].ToString() != "")
                            {
                                login.ReferralCode = row["ReferralCode"].ToString();
                            }
                            else
                            {
                                login.ReferralCode = "";
                            }
                            login.Pincode = row["Pincode"].ToString();
                            login.DeviceType = row["DeviceType"].ToString();
                            login.DeviceId = row["DeviceId"].ToString();
                            login.Status = row["Status"].ToString();
                            login.NewUser = "0";
                            list.Add(login);
                            Context.Response.Write(js.Serialize(list));
                            return;
                        }
                    }
                }

                if (device_type == "and" || device_type == "ios")
                {
                    dtemailchk = _user.GetCustIdByEmailORDevice(email, device_id);
                }

                if (dtemailchk.Rows.Count > 0)
                {
                    if (device_type == "and" || device_type == "ios")
                    {
                        dtckemail = D.GetDataTable("select CustId from CustomerMaster where DeviceId='" + device_id + "'");
                    }
                    if (dtckemail.Rows.Count > 0)
                    {
                        Context.Response.Write("You are Already Registered with this app.");
                        return;
                    }
                    else
                    {
                        Context.Response.Write("Your login account is invalid.");
                        return;
                    }
                }
                else
                {
                    if (device_type == "and" || device_type == "ios") //&& device_id.Length == 15
                    {
                        DataTable dtquery = D.GetDataTable("SELECT  LEFT(NEWID(),8) as refcode");
                        string uniqueid = dtquery.Rows[0]["refcode"].ToString();
                        _user.Insert(name, "", email, "", "", "", 0, 0, 0, device_id, device_type, otp, 0, "", uniqueid, 0);
                    }

                    if (device_type == "and" || device_type == "ios")
                    {
                        dtemailnew = _user.GetCustIdByEmailAndDevice(email, device_id);
                    }

                    if (dtemailnew.Rows.Count > 0)
                    {
                        userid = dtemailnew.Rows[0]["CustId"].ToString();
                        DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(userid));
                        if (Dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in Dt.Rows)
                            {
                                login.Email = email;
                                login.CustId = row["CustId"].ToString();
                                if (row["Name"].ToString() != "")
                                {
                                    login.Name = row["Name"].ToString();
                                }
                                else
                                {
                                    login.Name = "";
                                }
                                if (row["Address"].ToString() != "")
                                {
                                    login.Address = row["Address"].ToString();
                                }
                                else
                                {
                                    login.Address = "";
                                }
                                if (row["MobileNo"].ToString() != "")
                                {
                                    login.MobileNo = row["MobileNo"].ToString();
                                }
                                else
                                {
                                    login.MobileNo = "";
                                }
                                if (row["Area"].ToString() != "")
                                {
                                    login.Area = row["Area"].ToString();
                                }
                                else
                                {
                                    login.Area = "";
                                }
                                if (row["CountryId"].ToString() != "")
                                {
                                    login.CountryId = row["CountryId"].ToString();
                                }
                                else
                                {
                                    login.CountryId = "0";
                                }
                                if (row["StateId"].ToString() != "")
                                {
                                    login.StateId = row["StateId"].ToString();
                                }
                                else
                                {
                                    login.StateId = "0";
                                }
                                if (row["CityId"].ToString() != "")
                                {
                                    login.CityId = row["CityId"].ToString();
                                }
                                else
                                {
                                    login.CityId = "0";
                                }
                                if (row["ReferralCode"].ToString() != "")
                                {
                                    login.ReferralCode = row["ReferralCode"].ToString();
                                }
                                else
                                {
                                    login.ReferralCode = "";
                                }
                                login.Pincode = row["Pincode"].ToString();
                                login.DeviceType = row["DeviceType"].ToString();
                                login.DeviceId = row["DeviceId"].ToString();
                                login.Status = row["Status"].ToString();
                                login.NewUser = "1";
                                list.Add(login);
                                Context.Response.Write(js.Serialize(list));
                                return;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Context.Response.Write("[]");
            return;
        }
    }

    [WebMethod(Description = "Get or Update Users Details")]
    public void GetCustomerDetail(string action, string id, string email, string deviceid, string device_type, string mobile, string name, string area, string city, string state, string country, string pincode, string address)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string userid = "";
        bool chk = false;
        if (action == "custdetail")
        {
            DataTable dtemailMain = _user.GetCustIdByEmailORDevice(email, deviceid);
            //D.GetDataTable("SELECT id FROM manage_users WHERE email='" + Email + "' or deviceid='" + Deviceid + "'");
            if (dtemailMain.Rows.Count > 0)
            {
                userid = dtemailMain.Rows[0]["CustId"].ToString();
                if (userid != null && userid != "")
                {
                    if (device_type == "and")
                    {
                        DataTable dtchkDevice = D.GetDataTable("select CustId from CustomerMaster where DeviceId='" + deviceid + "'");
                        if (dtchkDevice.Rows.Count > 0)
                        {
                            userid = dtchkDevice.Rows[0]["CustId"].ToString();
                            if (id != "")
                            {
                                chk = false;
                            }
                            else
                            {
                                id = userid;
                                chk = true;
                            }
                        }
                    }
                    if (device_type == "ios")
                    {
                        DataTable dtchkDevice = D.GetDataTable("select CustId from CustomerMaster where DeviceId='" + deviceid + "'");
                        if (dtchkDevice.Rows.Count > 0)
                        {
                            userid = dtchkDevice.Rows[0]["CustId"].ToString();
                            if (id != "")
                            {
                                chk = false;
                            }
                            else
                            {
                                id = userid;
                                chk = true;
                            }
                        }
                    }
                }
            }
            else //if (userid != null && userid != "")
            {
                DataTable dtuid = D.GetDataTable("select isnull(max(CustId),0) as userid from CustomerMaster");
            }

            DataTable dtuid1 = new DataTable();
            if (device_type == "and" || device_type == "ios")
            {
                dtuid1 = _user.GetCustIdByEmailORDevice(email, deviceid);
            }

            DataTable Dt = _user.GetCustomerByID(Convert.ToInt32(dtuid1.Rows[0]["CustId"].ToString()));
            if (id != "")
            {
                chk = false;
            }
            else
            {
                id = dtuid1.Rows[0]["CustId"].ToString();
                chk = true;
            }

            if (chk == false)
            {
                if (id == "")
                {
                    DataTable dtemail = _user.GetCustIdByEmailORDevice(email, deviceid);
                    if (dtemail.Rows.Count > 0)
                    {
                        userid = dtemail.Rows[0]["CustId"].ToString();
                        if (userid != null && userid != "")
                        {
                            DataTable dtmobileno = D.GetDataTable("SELECT CustId FROM CustomerMaster WHERE MobileNo='" + mobile + "'");
                            if (dtemail.Rows.Count > 0)
                            {
                                userid = dtemail.Rows[0]["CustId"].ToString();
                                if (userid != null && userid != "")
                                {
                                    _user.Insert(name, mobile, email, address, area, pincode, Convert.ToInt32(city), Convert.ToInt32(state), Convert.ToInt32(country), deviceid, device_type, "", 0, "", "", 0);
                                    DataTable dtuid = D.GetDataTable("select isnull(max(CustId),0) as userid from CustomerMaster");
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtusername = D.GetDataTable("SELECT * FROM CustomerMaster WHERE (Email = '" + email + "'  or DeviceId='" + deviceid + "') AND CustId = " + id + "");
                    if (dtusername.Rows.Count > 0)
                    {
                        if (device_type == "and" || device_type == "ios")
                        {
                            _user.UpdateCustomerwithoutOTP(name, mobile, email, address, area, Convert.ToInt32(pincode), Convert.ToInt32(city), Convert.ToInt32(state), Convert.ToInt32(country), deviceid, device_type, Convert.ToInt32(id));
                        }
                    }
                }
                Context.Response.Write(id);
                return;
            }
            Context.Response.Write(id);
            return;
        }
    }

    [WebMethod(Description = "Get States Data")]
    public void GetStatesData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "state")
        {
            int i = 0;
            DataTable dt = _state.GetStatesByOrder();
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
            Context.Response.Write(js.Serialize(list));
        }
    }

    [WebMethod(Description = "Get City Data By States")]
    public void GetCityData(string action, string stateid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "city")
        {
            int i = 0;
            int sid = Convert.ToInt32(stateid);
            DataTable dt = _city.GetCityByStateId(sid);
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

                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }


    [WebMethod(Description = "Get Item Details")]
    public void GetItemDetails(string action, string itemid)
    {
        string path = "http://yourcarepharmacy.com/Resources/";
        JavaScriptSerializer js = new JavaScriptSerializer();

        if (action == "itemdetails")
        {
            int i = 0;
            DataTable dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i,CompanyMaster as c where c.ID=i.CompanyId and i.ItemId=" + itemid + "");
            //DataTable dt = _item.GetData();
            List<ItemMst> list = new List<ItemMst>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ItemMst item = new ItemMst();
                    item.ItemId = dt.Rows[i]["ItemId"].ToString();
                    item.ItemName = dt.Rows[i]["ItemName"].ToString();
                    item.MRP = dt.Rows[i]["Price"].ToString();
                    item.Description = dt.Rows[i]["Description"].ToString();
                    item.CatId = dt.Rows[i]["CatId"].ToString();
                    item.SubCatId = dt.Rows[i]["SubCatId"].ToString();
                    item.IconImg = path + "Icon/" + dt.Rows[i]["IconImg"].ToString();
                    item.SmallImg = path + "Small/" + dt.Rows[i]["SmallImg"].ToString();
                    item.BigImg = path + "Big/" + dt.Rows[i]["BigImg"].ToString();
                    //item.BigImg = path + "Big/big.jpg";
                    if (dt.Rows[i]["SellingPrice"].ToString() == "")
                    {
                        item.SellingPrice = "0";
                    }
                    else
                    {
                        item.SellingPrice = dt.Rows[i]["SellingPrice"].ToString();
                    }

                    if (dt.Rows[i]["Percentage"].ToString() == "")
                    {
                        item.Per = "0";
                    }
                    else
                    {
                        item.Per = dt.Rows[i]["Percentage"].ToString();
                    }
                    item.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                    list.Add(item);
                }

                //Context.Response.ContentType = "application/json; charset=utf-8";
                Context.Response.Write(js.Serialize(list));
            }
            else
            {

                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Max Order Data")]
    public void GetMaxOrder(string action, string custid, string orderdt)
    {
        MaxOrder order = new MaxOrder();

        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "maxorder")
        {

            //int i = 0;
            //DataTable dt = D.GetDataTable("Select * from customerOrder where custid=" + custid + " and status=0");
            //List<MaxOrder> list = new List<MaxOrder>();
            //if (dt.Rows.Count > 0)
            //{
            //    DateTime d = Convert.ToDateTime(dt.Rows[0]["OrderDt"].ToString());

            //    order.CustId = dt.Rows[0]["CustId"].ToString();
            //    order.OrderId = Convert.ToDouble(dt.Rows[0]["OrderId"].ToString());
            //    order.OrderDt = d.ToString("yyyy/MM/dd");
            //    list.Add(order);
            //}
            //else
            //{
            //    DataTable dtm = D.GetDataTable("Select max(orderid) from customerOrder");

            //    order.CustId = custid;
            //    order.OrderId = Convert.ToDouble(dtm.Rows[0][0].ToString());
            //    order.OrderDt = orderdt;
            //    order.OrderId = order.OrderId + 1;

            //    //  string query = "insert into customerOrder(OrderId,OrderDt,CustId,Status)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ")";

            //    D.ExecuteQuery("insert into customerOrder(OrderId,OrderDt,CustId,Status)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ",0)");

            //    //pincode.State = dt.Rows[i]["State"].ToString();
            //    //pincode.Country = dt.Rows[i]["Country"].ToString();
            //    list.Add(order);
            //}
            //DataTable dt = D.GetDataTable("Select max(orderid) from customerOrder where custid=" + custid + " and status=0");


            //if (dt.Rows.Count > 0)
            //{
            //    for (i = 0; i <= dt.Rows.Count - 1; i++)
            //    {

            //        order.CustId = custid;
            //        order.OrderId = Convert.ToDouble(dt.Rows[0][0].ToString());
            //        order.OrderDt = orderdt;

            //        order.OrderId = order.OrderId + 1;

            //        string query = "insert into customerOrder(OrderId,OrderDt,CustId,Status)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ")";

            //        D.ExecuteQuery("insert into customerOrder(OrderId,OrderDt,CustId)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ",0)"); 

            //        //pincode.State = dt.Rows[i]["State"].ToString();
            //        //pincode.Country = dt.Rows[i]["Country"].ToString();
            //        list.Add(order);
            //    }

            Context.Response.ContentType = "application/json; charset=utf-8";
            Context.Response.Write(js.Serialize(GetMaxOrderId(custid, orderdt)));   // Call a function that return max orderid and order dt

        }
    }



    // Return New Max Order Id and Orderdt By Cust Id
    private object GetMaxOrderId(string custid, string orderdt)
    {
        MaxOrder order = new MaxOrder();
        JavaScriptSerializer js = new JavaScriptSerializer();

        int i = 0;
        DataTable dt = D.GetDataTable("Select * from customerOrder where custid=" + custid + " and status=0");
        List<MaxOrder> list = new List<MaxOrder>();
        if (dt.Rows.Count > 0)
        {
            DateTime d = Convert.ToDateTime(dt.Rows[0]["OrderDt"].ToString());
            order.CustId = dt.Rows[0]["CustId"].ToString();
            order.OrderId = Convert.ToDouble(dt.Rows[0]["OrderId"].ToString());
            order.OrderDt = d.ToString("yyyy/MM/dd");
            list.Add(order);
        }
        else
        {
            DataTable dtm = D.GetDataTable("Select COALESCE(MAX(orderid),0) from customerOrder");

            order.CustId = custid;
            order.OrderId = Convert.ToDouble(dtm.Rows[0][0].ToString());
            order.OrderDt = orderdt;
            order.OrderId = order.OrderId + 1;
            D.ExecuteQuery("insert into customerOrder(OrderId,OrderDt,CustId,Status)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ",0)");
            list.Add(order);
        }
        return list;

    }

    [WebMethod(Description = "Get Pincode Data")]
    public void GetPincodeData(string action, string cityid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "pincode")
        {
            int i = 0;
            DataTable dt = _pincode.GetPincodeByCityId(cityid);
            List<PincodeMaster> list = new List<PincodeMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    PincodeMaster pincode = new PincodeMaster();
                    pincode.Pincode = dt.Rows[i]["Pincode"].ToString();
                    pincode.Address = dt.Rows[i]["Address"].ToString();
                    pincode.City = dt.Rows[i]["City"].ToString();
                    //pincode.State = dt.Rows[i]["State"].ToString();
                    //pincode.Country = dt.Rows[i]["Country"].ToString();
                    list.Add(pincode);
                }

                //Context.Response.ContentType = "application/json; charset=utf-8";
                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Category Data")]
    public void GetCategoryData(string action, string parentid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "category")
        {
            int i = 0;
            string path = "";
            // DataTable dt = _category.GetCategoryByParentID(Convert.ToInt32(parentid));
            DataTable dt = D.GetDataTable("select * from CategoryMaster where ParentId=" + Convert.ToInt32(parentid) + " and Status=1");
            //if (parentid == "0")
            //{
            //    dt = D.GetDataTable("select * from CategoryMaster where ParentId=" + Convert.ToInt32(parentid));
            //}
            //else
            //{
            //    dt = D.GetDataTable("select * from CategoryMaster where ParentId!=" + Convert.ToInt32(parentid));
            //}
            List<Categories> list = new List<Categories>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    path = "http://yourcarepharmacy.com/Resources/CategoryImage/Thumb/" + dt.Rows[i]["Image"].ToString();
                    Categories cat = new Categories();
                    cat.CatId = dt.Rows[i]["CatId"].ToString();
                    cat.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    cat.ParentId = dt.Rows[i]["ParentId"].ToString();
                    cat.desc = dt.Rows[i]["Description"].ToString();
                    cat.imgpath = path;
                    list.Add(cat);
                }
                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Items Data")]
    public void GetItemsData(string action, string SubcatId, string brandid, string sorting, string freeitems, string per)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string path = "http://yourcarepharmacy.com/Resources/";
        if (action == "item")
        {
            int i = 0;
            DataTable dt = new DataTable();
            if (freeitems == "0")
            {


                if (brandid != "0" && sorting != "0")
                {
                    dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " and i.CompanyId=" + Convert.ToInt32(brandid) + "order by i.itemname " + sorting);
                }
                else if (brandid == "0" && sorting == "0")
                {
                    dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " ");
                }
                else if (sorting != "0" && brandid == "0")
                {
                    dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " order by i.itemname " + sorting);
                }
                else if (brandid != "0" && sorting == "0")
                {
                    dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " and i.CompanyId=" + Convert.ToInt32(brandid) + "order by i.itemname asc");
                }
                if (per != "0")
                {
                    if (per == "5")
                    {
                        dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " and Percentage>0 and Percentage<=5");
                    }
                    else if (per == "10")//For per 5-10
                    {
                        dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " and Percentage>=5 and Percentage<=10");
                    }
                    else if (per == "100") //For per 10- onword
                    {
                        dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId where i.subcatid=" + Convert.ToInt32(SubcatId) + " and Percentage >=10");
                    }
                }

            }
            else
            {
                dt = D.GetDataTable("SELECT  c.CompanyName as CompanyName,i.* FROM ItemMaster as i ,CompanyMaster as c,FreeItem as fi where c.ID=i.CompanyId and fi.ItemId=i.ItemId and " + Convert.ToDecimal(freeitems) + " between FrmAmt and ToAmt ");
            }

            //DataTable dt = D.GetDataTable("SELECT c.CompanyName as CompanyName,i.* FROM ItemMaster as i LEFT OUTER JOIN CompanyMaster as c ON c.ID=i.CompanyId ");
            //DataTable dt = _item.GetData();
            List<ItemMst> list = new List<ItemMst>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ItemMst item = new ItemMst();

                    if (dt.Rows[i]["Percentage"].ToString() == "")
                    {
                        item.Per = "0";
                    }
                    else
                    {
                        item.Per = dt.Rows[i]["Percentage"].ToString();
                    }
                    item.ItemId = dt.Rows[i]["ItemId"].ToString();
                    item.ItemName = dt.Rows[i]["ItemName"].ToString();
                    item.MRP = dt.Rows[i]["Price"].ToString();
                    item.Description = dt.Rows[i]["Description"].ToString();
                    item.CatId = dt.Rows[i]["CatId"].ToString();
                    item.SubCatId = dt.Rows[i]["SubCatId"].ToString();
                    item.IconImg = path + "Icon/" + dt.Rows[i]["IconImg"].ToString();
                    item.SmallImg = path + "Small/" + dt.Rows[i]["SmallImg"].ToString();
                    item.BigImg = path + "Big/" + dt.Rows[i]["BigImg"].ToString();

                    if (dt.Rows[i]["SellingPrice"].ToString() == "")
                    {
                        item.SellingPrice = "0";
                    }
                    else
                    {
                        item.SellingPrice = dt.Rows[i]["SellingPrice"].ToString();
                    }
                    item.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                    list.Add(item);
                }
                js.MaxJsonLength = Int32.MaxValue;
                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Languages Data")]
    public void GetLanguagesData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "language")
        {
            int i = 0;
            DataTable dt = _lang.GetData();
            List<Languages> list = new List<Languages>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Languages lang = new Languages();
                    lang.Id = dt.Rows[i]["Id"].ToString();
                    lang.Name = dt.Rows[i]["Name"].ToString();
                    list.Add(lang);
                }

                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Labels Data")]
    public void GetLabelsData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "label")
        {
            int i = 0;
            DataTable dt = _label.GetData();
            List<Labels> list = new List<Labels>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Labels lbl = new Labels();
                    lbl.Id = dt.Rows[i]["Id"].ToString();
                    lbl.LabelName = dt.Rows[i]["LabelName"].ToString();
                    list.Add(lbl);
                }

                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Language's Labels Data")]
    public void GetLangLabelsData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "langlabel")
        {
            int i = 0;
            DataTable dt = _ll.GetData();
            List<langLabels> list = new List<langLabels>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    langLabels langlbl = new langLabels();
                    langlbl.ID = dt.Rows[i]["ID"].ToString();
                    langlbl.LangId = dt.Rows[i]["LangId"].ToString();
                    langlbl.LabelId = dt.Rows[i]["LabelId"].ToString();
                    list.Add(langlbl);
                }

                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Policy Data")]
    public void GetPolicysData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "policy")
        {
            int i = 0;
            DataTable dt = _policy.GetData();
            List<PolicyMaster> list = new List<PolicyMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    PolicyMaster policy = new PolicyMaster();
                    policy.ID = dt.Rows[i]["ID"].ToString();
                    policy.Name = dt.Rows[i]["Name"].ToString();
                    list.Add(policy);
                }

                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Get Status Data")]
    public void GetStatusData(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "status")
        {
            int i = 0;
            DataTable dt = _status.GetData();
            List<StatusMst> list = new List<StatusMst>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    StatusMst status = new StatusMst();
                    status.ID = dt.Rows[i]["ID"].ToString();
                    status.Status = dt.Rows[i]["Status"].ToString();
                    list.Add(status);
                }
                Context.Response.Write(js.Serialize(list));
            }
            else
            {
                Context.Response.Write("");
            }
        }
    }

    [WebMethod(Description = "Updates GCM Register ID Data")]
    public void UpdateGCMData(string action, string custid, string regid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "updategcm")
        {
            D.ExecuteQuery("update CustomerMaster set RegId='" + regid + "' where CustId=" + custid);
            Context.Response.Write("1");
        }
        else
        {
            Context.Response.Write("0");
        }
    }

    [WebMethod(Description = "Get Customers Order Data")]
    public void GetCustomersOrderHistoryIOS(string action, string custid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "custorder")
        {
            int i = 0;
            DataTable dt = _custorder.GetCustomerOrderByCustId(Convert.ToInt32(custid));
            List<OrderMaster> list = new List<OrderMaster>();
            if (dt.Rows.Count > 0)
            {

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataTable dtO = D.GetDataTable("select * from CustomerOrder where OrderId=" + Convert.ToInt32(dt.Rows[i]["OrderId"].ToString()) + " and Status=4");
                    if (dtO.Rows.Count > 0)
                    {
                        DataTable dtOrderDetail = D.GetDataTable("select * from CustOrderDetail where OrderId=" + Convert.ToInt32(dt.Rows[i]["OrderId"].ToString()));
                        if (dtOrderDetail.Rows.Count > 0)
                        {

                            OrderMaster order = new OrderMaster();
                            order.OrderId = dt.Rows[i]["OrderId"].ToString();
                            order.CustId = dt.Rows[i]["CustId"].ToString();
                            if (dt.Rows[i]["NetAmt"].ToString() != "")
                            {
                                order.NetAmt = dt.Rows[i]["NetAmt"].ToString();
                            }
                            else
                            {
                                order.NetAmt = "0";
                            }

                            if (dt.Rows[i]["TotalAmt"].ToString() != "")
                            {
                                order.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                            }
                            else
                            {
                                order.TotalAmt = "0";
                            }

                            if (dt.Rows[i]["DiscountAmt"].ToString() != "")
                            {
                                order.DiscountAmt = dt.Rows[i]["DiscountAmt"].ToString();
                            }
                            else
                            {
                                order.DiscountAmt = "0";
                            }

                            if (dt.Rows[i]["OrderDt"] != "")
                            {
                                string odate = dt.Rows[i]["OrderDt"].ToString();
                                DateTime date = Convert.ToDateTime(odate.ToString());
                                var shortDate = date.ToString("yyyy-MM-dd");
                                order.OrderDt = shortDate;
                            }
                            else
                            {
                                order.OrderDt = "";
                            }
                            list.Add(order);
                        }

                    }
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }


    [WebMethod(Description = "Get Customers Order Data")]
    public void GetCustomersOrderData(string action, string custid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "custorder")
        {
            int i = 0;
            DataTable dt = _custorder.GetCustomerOrderByCustId(Convert.ToInt32(custid));
            List<OrderMaster> list = new List<OrderMaster>();
            if (dt.Rows.Count > 0)
            {

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataTable dtO = D.GetDataTable("select * from CustomerOrder where OrderId=" + Convert.ToInt32(dt.Rows[i]["OrderId"].ToString()) + " and Status=4");
                    if (dtO.Rows.Count > 0)
                    {
                        DataTable dtOrderDetail = D.GetDataTable("select * from CustOrderDetail where OrderId=" + Convert.ToInt32(dt.Rows[i]["OrderId"].ToString()));
                        if (dtOrderDetail.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtOrderDetail.Rows)
                            {

                                OrderMaster order = new OrderMaster();
                                order.OrderId = dt.Rows[i]["OrderId"].ToString();
                                order.CustId = dt.Rows[i]["CustId"].ToString();
                                if (row["ItemType"].ToString() != "")
                                {
                                    order.ItemType = row["ItemType"].ToString();
                                }
                                else
                                {
                                    order.ItemType = "";
                                }

                                if (row["ItemId"].ToString() != "")
                                {
                                    order.ItemId = row["ItemId"].ToString();
                                }
                                else
                                {
                                    order.ItemId = "0";
                                }

                                if (row["Qty"].ToString() != "")
                                {
                                    order.Qty = row["Qty"].ToString();
                                }
                                else
                                {
                                    order.Qty = "";
                                }

                                if (row["Price"].ToString() != "")
                                {
                                    order.ItemPrice = row["Price"].ToString();
                                }
                                else
                                {
                                    order.ItemPrice = "";
                                }

                                if (row["ItemName"].ToString() != "")
                                {
                                    order.ItemName = row["ItemName"].ToString();
                                }
                                else
                                {
                                    order.ItemName = "";
                                }

                                if (dt.Rows[i]["NetAmt"].ToString() != "")
                                {
                                    order.NetAmt = dt.Rows[i]["NetAmt"].ToString();
                                }
                                else
                                {
                                    order.NetAmt = "0";
                                }

                                if (dt.Rows[i]["TotalAmt"].ToString() != "")
                                {
                                    order.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                                }
                                else
                                {
                                    order.TotalAmt = "0";
                                }

                                if (dt.Rows[i]["DiscountAmt"].ToString() != "")
                                {
                                    order.DiscountAmt = dt.Rows[i]["DiscountAmt"].ToString();
                                }
                                else
                                {
                                    order.DiscountAmt = "0";
                                }

                                if (dt.Rows[i]["OrderDt"] != "")
                                {
                                    string odate = dt.Rows[i]["OrderDt"].ToString();
                                    DateTime date = Convert.ToDateTime(odate.ToString());
                                    var shortDate = date.ToString("yyyy-MM-dd");
                                    order.OrderDt = shortDate;
                                }
                                else
                                {
                                    order.OrderDt = "";
                                }
                                list.Add(order);
                            }
                        }
                    }
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }

    [WebMethod(Description = "Get Customers Order's Details Data")]
    public void GetCustomersOrderDetailsData(string action, string orderid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "custorderdet")
        {
            int i = 0;
            DataTable dt = _custorderdetail.GetCustOrderDetailByOrderId(Convert.ToInt32(orderid));
            List<OrderDetailMaster> list = new List<OrderDetailMaster>();
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = D.GetDataTable("select * from customerOrder where OrderId=" + Convert.ToInt32(orderid));
                DateTime odate = new DateTime();
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    OrderDetailMaster orderdet = new OrderDetailMaster();

                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows[0]["OrderDt"].ToString() != "")
                        {
                            odate = Convert.ToDateTime(dt1.Rows[0]["OrderDt"].ToString());
                            orderdet.OrderDt = odate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            orderdet.OrderDt = "";
                        }
                    }

                    orderdet.OrderDetailId = dt.Rows[i]["OrderDetailId"].ToString();
                    orderdet.OrderId = dt.Rows[i]["OrderId"].ToString();
                    orderdet.ItemId = dt.Rows[i]["ItemId"].ToString();
                    orderdet.ItemName = dt.Rows[i]["ItemName"].ToString();

                    if (dt1.Rows[0]["ShippingCharge"].ToString() != "")
                    {
                        orderdet.ShippingCharges = dt1.Rows[0]["ShippingCharge"].ToString();
                    }
                    else
                    {
                        orderdet.ShippingCharges = "";
                    }
                    if (dt1.Rows[0]["RefPonits"].ToString() != "")
                    {
                        orderdet.wallet = dt1.Rows[0]["RefPonits"].ToString();
                    }
                    else
                    {
                        orderdet.wallet = "";
                    }
                    if (dt.Rows[i]["ItemType"].ToString() != "")
                    {
                        orderdet.ItemType = dt.Rows[i]["ItemType"].ToString();
                    }
                    else
                    {
                        orderdet.ItemType = "";
                    }

                    if (dt.Rows[i]["Price"].ToString() != "")
                    {
                        orderdet.ItemPrice = dt.Rows[i]["Price"].ToString();
                    }
                    else
                    {
                        orderdet.ItemPrice = "0";
                    }

                    if (dt.Rows[i]["Amt"].ToString() != "")
                    {
                        orderdet.TotalAmt = dt.Rows[i]["Amt"].ToString();
                    }
                    else
                    {
                        orderdet.TotalAmt = "0";
                    }

                    if (dt.Rows[i]["Qty"].ToString() != "")
                    {
                        orderdet.Qty = dt.Rows[i]["Qty"].ToString();
                    }
                    else
                    {
                        orderdet.TotalAmt = "0";
                    }
                    list.Add(orderdet);
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }
    // This Content Move on getData.aspx

    [WebMethod(Description = "Insert Order as per customers request")]
    public void InsertOrderData(string action, string data, string walletamt)
    {
        int total = 0;
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "custorder")
        {
            var _Items = js.Deserialize<InsertOrders[]>(data);
            //  Context.Response.Write(" Items : " + data);
            int cnt = _Items.Length;
            string CustId = "";
            string OrderDt = "";
            for (int i = 0; i < cnt; i++)
            {
                string OrderId = (new ListItem(_Items[i].OrderId).ToString());
                string ItemId = (new ListItem(_Items[i].ItemId).ToString());
                string ItemName = (new ListItem(_Items[i].ItemName).ToString());
                string Price = (new ListItem(_Items[i].Price).ToString());
                string Qty = (new ListItem(_Items[i].Qty).ToString());
                //string Amt = (new ListItem(_Items[i].Amt).ToString());
                CustId = (new ListItem(_Items[i].CustId).ToString());
                string DiscountAmt = (new ListItem(_Items[i].DiscountAmt).ToString());
                string NetAmt = (new ListItem(_Items[i].NetAmt).ToString());
                string TotalAmt = (new ListItem(_Items[i].TotalAmt).ToString());
                OrderDt = (new ListItem(_Items[i].OrderDt).ToString());
                string ShippingCharges = (new ListItem(_Items[i].ShippingCharges).ToString());
                string Status = (new ListItem(_Items[i].status).ToString());
                string ItemType = (new ListItem(_Items[i].ItemType).ToString());
                DateTime odate = Convert.ToDateTime(OrderDt);

                decimal Amt = Convert.ToDecimal(Price) * Convert.ToInt32(Qty);


                _custorderdetail.Insert(Convert.ToInt32(ItemId), ItemName, Convert.ToDecimal(Price), Convert.ToInt32(Qty), Convert.ToDecimal(Amt), Convert.ToInt32(OrderId), ItemType);

                //  D.ExecuteQuery("update CustomerMaster set RefPoints=RefPoints-" + Convert.ToInt32(walletamt) + " where CustId=" + Convert.ToInt32(CustId));

                _custorder.Update(odate, Convert.ToInt32(CustId), Convert.ToDecimal(TotalAmt), Convert.ToDecimal(DiscountAmt), Convert.ToInt32(NetAmt), Convert.ToInt32(Status), Convert.ToDecimal(ShippingCharges), "", "", 0, Convert.ToInt32(walletamt), Convert.ToInt32(OrderId));
            }
            D.ExecuteQuery("update CustomerMaster set RefPoints=RefPoints-" + Convert.ToInt32(walletamt) + " where CustId=" + Convert.ToInt32(CustId));
            //data=[{"Qty":2,"ItemId":34,"status":"2","Price":143,"ItemName":"CLEAR ICE COOL MENTHOL SHAMPOO 170ML","OrderId":2,"DiscountAmt":0,"OrderDt":"2016-01-30","TotalAmt":345,"CustId":22,"ShippingCharges":0,"NetAmt":345},{"Qty":1,"ItemId":37,"status":"2","Price":59,"ItemName":"CLEAR ACTIVE CARE SHAMPOO 100ML","OrderId":2,"DiscountAmt":0,"OrderDt":"2016-01-30","TotalAmt":345,"CustId":22,"ShippingCharges":0,"NetAmt":345}]
            //Data=[{"OrderId": "45", "ItemId": "45",  "ItemName": "2",  "Price": "3", "Qty": "chapter 1",  "Amt": "Fairy Land", "CustId": "2015-05-19", "DiscountAmt": "1", "NetAmt": "1", "TotalAmt": "1" },{"OrderId": "45", "ItemId": "45",  "ItemName": "2",  "Price": "3", "Qty": "chapter 1",  "Amt": "Fairy Land", "CustId": "2015-05-19", "DiscountAmt": "1", "NetAmt": "1", "TotalAmt": "1" }]

            List<Message> mg = new List<Message>();
            Message Msg = new Message();
            Msg.msg = "Data insert.";
            mg.Add(Msg);
            Context.Response.Write(js.Serialize(Msg));
            return;
        }
    }
    //    http://yourcarepharmacy.com/MedicalService.asmx/?userid=7&descr=&imgname=IMG_20160112_100831.jpg&newfilename=71452573555310

    [WebMethod(Description = "Insert Prescription ")]
    public void InsertPrescription(string action, string desc, string imgname, string newfilename, string orderid, string status, string custid, string orderdt)
    {
        MaxOrder order = new MaxOrder();
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "insertpresc")
        {
            try
            {
                string ext = Path.GetExtension(imgname);
                string newfname = "";
                newfname = newfilename + ext;

                //string path = "~/PrescriptionImages/uploads/" + newfname;
                System.IO.File.Move(Server.MapPath("~/PrescriptionImages/uploads/" + imgname), Server.MapPath("~/PrescriptionImages/uploads/" + newfname));
                string path = "http://yourcarepharmacy.com/PrescriptionImages/uploads/" + newfname;
                //_presc_entry.Insert(DateTime.Now, path, Convert.ToInt32(userid), desc);
                D.ExecuteQuery("update CustomerOrder set PreImg='" + path + "',PreDescription='" + desc + "',status=" + Convert.ToInt32(status) + " where OrderId=" + Convert.ToDecimal(orderid));

                DataTable dt = D.GetDataTable("Select * from customerOrder where custid=" + custid + " and status=0");
                List<MaxOrder> list = new List<MaxOrder>();
                if (dt.Rows.Count > 0)
                {
                    DateTime d = Convert.ToDateTime(dt.Rows[0]["OrderDt"].ToString());


                    order.CustId = dt.Rows[0]["CustId"].ToString();
                    order.OrderId = Convert.ToDouble(dt.Rows[0]["OrderId"].ToString());
                    order.OrderDt = d.ToString("yyyy/MM/dd");
                    list.Add(order);
                }
                else
                {
                    DataTable dtm = D.GetDataTable("Select max(orderid) from customerOrder");

                    order.CustId = custid;
                    order.OrderId = Convert.ToDouble(dtm.Rows[0][0].ToString());
                    order.OrderDt = orderdt;
                    order.OrderId = order.OrderId + 1;



                    D.ExecuteQuery("insert into customerOrder(OrderId,OrderDt,CustId,Status)values(" + Convert.ToInt64(order.OrderId) + ",'" + order.OrderDt + "'," + Convert.ToInt64(custid) + ",0)");

                    list.Add(order);
                }
                Context.Response.Write(order.OrderId.ToString());  // If Inserted return new Max OrderID
            }
            catch
            {
                Context.Response.Write("0");
            }
        }


        //List<Message> mg = new List<Message>();
        //Message Msg = new Message();
        //Msg.msg = "Prescription has ben Uploaded !";
        //mg.Add(Msg);
        //Context.Response.Write(js.Serialize(Msg));
        //return;
    }
    [WebMethod(Description = "Get Order Status by customer id and Order Id")]
    public void GetOrderStatus(string action, string orderid, string custid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "orderstatus")
        {
            int i = 0;
            DataTable dt = D.GetDataTable("select * from CustomerOrder where OrderId=" + Convert.ToDecimal(orderid) + " and CustId=" + Convert.ToDecimal(custid));

            if (dt.Rows.Count > 0)
            {
                Context.Response.Write(dt.Rows[0]["Status"].ToString());
            }
        }
    }

    [WebMethod(Description = "Get Customers Order Date by OrderId and CustId")]
    public void GetOrderItems(string action, string custid, string orderid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "orderitems")
        {
            int i = 0;
            DataTable dt = D.GetDataTable("select * from CustomerOrder where CustId=" + Convert.ToDecimal(custid) + " and OrderId=" + Convert.ToDecimal(orderid));
            List<OrderDetailMaster> list = new List<OrderDetailMaster>();
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {

                    DataTable dtOrder = D.GetDataTable("select * from CustOrderDetail where OrderId=" + Convert.ToDecimal(dt.Rows[j]["OrderId"]));
                    if (dtOrder.Rows.Count > 0)
                    {
                        for (i = 0; i <= dtOrder.Rows.Count - 1; i++)
                        {
                            OrderDetailMaster orderdet = new OrderDetailMaster();


                            if (dtOrder.Rows[i]["OrderDetailId"].ToString() != "")
                            {
                                orderdet.ItemType = dtOrder.Rows[i]["ItemType"].ToString();
                            }
                            else
                            {
                                orderdet.ItemType = "";
                            }

                            if (dt.Rows[0]["TotalAmt"].ToString() != "")
                            {
                                orderdet.TotalAmt = dt.Rows[0]["TotalAmt"].ToString();
                            }
                            else
                            {
                                orderdet.TotalAmt = "0";
                            }

                            if (dtOrder.Rows[i]["Price"].ToString() != "")
                            {
                                orderdet.ItemPrice = dtOrder.Rows[i]["Price"].ToString();
                            }
                            else
                            {
                                orderdet.ItemPrice = "0";
                            }

                            if (dt.Rows[0]["ShippingCharge"].ToString() != "")
                            {
                                orderdet.ShippingCharges = dt.Rows[0]["ShippingCharge"].ToString();
                            }
                            else
                            {
                                orderdet.ShippingCharges = "0";
                            }


                            if (dt.Rows[0]["RefPoints"].ToString() != "")
                            {
                                orderdet.wallet = dt.Rows[0]["RefPoints"].ToString();
                            }
                            else
                            {
                                orderdet.wallet = "0";
                            }

                            orderdet.OrderDetailId = dtOrder.Rows[i]["OrderDetailId"].ToString();
                            orderdet.OrderId = dtOrder.Rows[i]["OrderId"].ToString();
                            orderdet.ItemId = dtOrder.Rows[i]["ItemId"].ToString();
                            orderdet.ItemName = dtOrder.Rows[i]["ItemName"].ToString();
                            orderdet.Qty = dtOrder.Rows[i]["Qty"].ToString();
                            DateTime date = Convert.ToDateTime(dt.Rows[j]["OrderDt"].ToString());
                            orderdet.OrderDt = date.ToString("yyyy-MM-dd");
                            list.Add(orderdet);
                        }
                    }
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }
    [WebMethod(Description = "Insert Feedback")]
    public void InsertFeedback(string action, string custid, string msg)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "insertfeedback")
        {
            try
            {
                string content = System.Uri.UnescapeDataString(msg);
                _feed_mst.Insert(Convert.ToDecimal(custid), content);
                Context.Response.Write("1");
            }
            catch
            {
                Context.Response.Write("0");
            }
        }
    }


    [WebMethod(Description = "Get Check Referral Code Data")]
    public void GetCheckRefcode(string action, string custid, string refcode, string userrefcode)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        int pointslimit = 0;
        int referralpoint = 0;
        string api_key = "";
        string capping = "";
        string uid = "";
        string totalcnt = "";
        decimal point = 0;
        string deviceid = "";
        string refdeviceid = "";
        string url = "";
        if (action == "checkrefcode")
        {
            if (refcode == "")
            {
                Context.Response.Write("Referral Code can not be blank.");
                return;
            }

            pointslimit = Convert.ToInt32(Application["referral_limit"]); //Convert.ToInt32(dtGeneralsetting.Rows[0]["referral_limit"].ToString());
            referralpoint = Convert.ToInt32(Application["referral_point"]);  // Convert.ToInt32(dtGeneralsetting.Rows[0]["referral_point"].ToString());
            api_key = Application["api_key"].ToString(); //dtGeneralsetting.Rows[0]["api_key"].ToString();
            DataTable dtchk = _user.GetCustomerByID(Convert.ToInt32(custid));
            if (dtchk.Rows.Count > 0)
            {

                DataTable dtchkPoint = D.GetDataTable("select count(points) as point from referral where userid= " + custid + "");
                if (dtchkPoint.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtchkPoint.Rows[0]["point"]) >= referralpoint)
                    {
                        Context.Response.Write("You have reached the maximum limit for referral.");
                        return;
                    }
                }
                DataTable dtchkPointLimit = D.GetDataTable("select isnull(SUM(convert(int,points)),0) as point from referral where userid= " + custid + "");
                if (dtchkPointLimit.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtchkPointLimit.Rows[0]["point"]) >= pointslimit)
                    {
                        Context.Response.Write("You have reached the maximum limit for referral.");
                        return;
                    }
                }
                DataTable dt = D.GetQuery("CustomerMaster", "CustId as userid,refpoints,DeviceId,RegId,DeviceType", "ReferralCode='" + refcode + "'");
                if (dt.Rows.Count > 0)
                {
                    uid = dt.Rows[0]["userid"].ToString();
                    refdeviceid = dt.Rows[0]["deviceid"].ToString();
                    DataTable dtDevice = D.GetQuery("CustomerMaster", "DeviceId,ReferralCode,DeviceType", " CustId=" + custid + "");
                    if (dtDevice.Rows.Count > 0)
                    {
                        deviceid = dtDevice.Rows[0]["DeviceId"].ToString();
                        if (userrefcode == "" || userrefcode == null)
                        {
                            userrefcode = dtDevice.Rows[0]["ReferralCode"].ToString();
                        }
                    }
                    DataTable dtRef = D.GetQuery("referral", "id", " device_id='" + deviceid + "'");
                    if (refdeviceid.Equals(deviceid))
                    {
                        Context.Response.Write("The referral device can not be same.");
                        return;
                    }

                    if (dtRef.Rows.Count > 0)
                    {
                        Context.Response.Write("This device has already been referred.");
                        return;
                    }
                    else
                    {
                        DateTime dateAndTime = DateTime.Now;

                        D.ExecuteQuery("insert into referral (userid,referral_dt,points,status,capstatus,download_dt,device_id,duserid,referralcode) values('" + uid + "','" + dateAndTime.ToString("yyyy/MM/dd HH:mm:ss") + "','" + referralpoint + "','1','0','" + dateAndTime.ToString("yyyy/MM/dd HH:mm:ss") + "','" + deviceid + "','" + custid + "','" + refcode + "')");
                        DataTable dtcnt = D.GetQuery("referral", "count(*) as totalcnt", " userid='" + uid + "' and status='1' and capstatus='0'");
                        if (dtcnt.Rows.Count > 0)
                        {
                            D.ExecuteQuery("update CustomerMaster set RefPoints=RefPoints + " + referralpoint + " where CustId=" + Convert.ToInt32(uid) + "");
                            D.ExecuteQuery("update referral set capstatus='1' where userid='" + uid + "'");
                            if (api_key != "" && dt.Rows[0]["regid"].ToString() != "")
                            {
                                //DataTable dtpoint = D.GetQuery("manage_users", "refpoints", " id='" + uid + "' and status='active'");
                                //if (dtpoint.Rows.Count > 0)
                                //{
                                //    string message = "You have earned points on invites.Your recharge points are " + dtpoint.Rows[0]["refpoints"].ToString();
                                //       _GCM.SendNotification(api_key, dt.Rows[0]["regid"].ToString(), message, "Paymyreview");
                                //}
                            }
                        }
                    }
                }
                else
                {
                    Context.Response.Write("Referral Code does not match.");
                    return;
                }
            }
            else
            {
                Context.Response.Write("Invalid User.");
                return;
            }

            Context.Response.Write("1");
            return;
        }
    }
    [WebMethod(Description = "Get Doctor Details Data")]
    public void GetDoctorDetail(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "doctor")
        {
            int i = 0;
            string path = "http://yourcarepharmacy.com/Resources/Doctor/Thumb/";
            DataTable dt = D.GetDataTable("select * from DoctorMaster");
            List<DoctorMaster> list = new List<DoctorMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DoctorMaster doctor = new DoctorMaster();
                    doctor.DoctorId = dt.Rows[i]["ID"].ToString();
                    doctor.Name = dt.Rows[i]["DoctorName"].ToString();
                    doctor.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    doctor.Photo = path + dt.Rows[i]["Photo"].ToString();
                    doctor.Address = dt.Rows[i]["Address"].ToString();
                    doctor.Specialization = dt.Rows[i]["Specialization"].ToString();
                    doctor.Description = dt.Rows[i]["Description"].ToString();
                    list.Add(doctor);
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }

    [WebMethod(Description = "Get Contact Details Data")]
    public void GetContact(string action)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "contact")
        {
            int i = 0;

            DataTable dt = D.GetDataTable("select * from Contact where Status=1");
            List<ContactMaster> list = new List<ContactMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ContactMaster contact = new ContactMaster();

                    contact.Address = dt.Rows[i]["Address"].ToString();
                    contact.Phone = dt.Rows[i]["Phone"].ToString();
                    contact.Latitude = dt.Rows[i]["Latitude"].ToString();
                    contact.Longitude = dt.Rows[i]["Longitude"].ToString();
                    contact.Email = dt.Rows[i]["Email"].ToString();
                    list.Add(contact);
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }

    [WebMethod(Description = "Get MyWallete  Data")]
    public void GetMyWallete(string action, string custid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "mywallete")
        {
            int i = 0;

            DataTable dt = D.GetDataTable("select * from CustomerMaster where CustId=" + Convert.ToInt32(custid) + "");

            if (dt.Rows.Count > 0)
            {
                Context.Response.Write(dt.Rows[0]["RefPoints"].ToString());
            }
        }
    }


    [WebMethod(Description = "Get Brand/Company Details Data")]
    public void GetBrand(string action, string SubcatId)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "brand")
        {
            int i = 0;

            //DataTable dt = D.GetDataTable("select * from CompanyMaster where Status=1");
            DataTable dt = D.GetDataTable("select cm.CompanyName, i.CompanyId as ID,COUNT(i.CompanyId) as total from ItemMaster as i , CompanyMaster as cm where i.SubCatId=" + Convert.ToInt32(SubcatId) + " and i.CompanyId=cm.ID group by i.CompanyId,cm.CompanyName");
            List<BrandMaster> list = new List<BrandMaster>();
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    BrandMaster brand = new BrandMaster();

                    brand.brandid = dt.Rows[i]["ID"].ToString();
                    brand.brandname = dt.Rows[i]["CompanyName"].ToString();
                    brand.count = dt.Rows[i]["total"].ToString();
                    list.Add(brand);
                }
            }
            Context.Response.Write(js.Serialize(list));
        }
    }

    [WebMethod(Description = "Get Discount Amount by Coupon Code")]
    public void GetDiscount(string action, string couponcode, string totalamt)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "coupon")
        {
            string discount = "";
            // This Query Check Only Date ,CouponCode and Status
            // DataTable dt = D.GetDataTable("select * from PromoCodeMaster where PromoCode='" + couponcode + "' and Status=1 and (convert(date,GETDATE()) between CONVERT(date,FromDate)  and CONVERT(date,Todate))");    

            // This Query Check Only Date ,CouponCode and Status and Amount also
            //            DataTable dt = D.GetDataTable("select * from PromoCodeMaster where PromoCode='" + couponcode + "' and Status=1 and (convert(date,GETDATE()) between CONVERT(date,FromDate)  and CONVERT(date,Todate)) and (" + Convert.ToDecimal(totalamt) + " between FromAmt and ToAmt)");
            DataTable dt = D.GetDataTable("select * from PromoCodeMaster where PromoCode='" + couponcode + "' and Status=1 and (convert(date,GETDATE()) between CONVERT(date,FromDate)  and CONVERT(date,Todate)) and (" + Convert.ToDecimal(totalamt) + " >=FromAmt)");
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Type"].ToString() == "fix")
                {
                    discount = dt.Rows[0]["Discount"].ToString();
                    Context.Response.Write(discount);
                }
                else
                {
                    int amt = 0;
                    discount = dt.Rows[0]["Discount"].ToString();
                    amt = Convert.ToInt32(Convert.ToDecimal(dt.Rows[0]["FromAmt"].ToString()) / 100 * Convert.ToInt32(discount));
                    Context.Response.Write(amt.ToString());
                }
            }
            else
            {
                Context.Response.Write("0");
            }
        }
    }

    [WebMethod(Description = "Update Wallete")]
    public void UpdateWallete(string action, string userid, string walleteamt, string orderid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "updatewallete")
        {
            DataTable dt = D.GetDataTable("select * from CustomerOrder where OrderId=" + Convert.ToInt32(orderid));
            if (dt.Rows.Count > 0)
            {
                //D.ExecuteQuery("update CustomerOrder set RefPoints=" + Convert.ToInt32(walleteamt) + " where OrderId=" + Convert.ToInt32(orderid));  // Insert OID and Walleteamt in other table for keep track of order wallete's
                D.ExecuteQuery("delete from CutomerOrderWallete where OrderId=" + Convert.ToInt32(orderid));
                D.ExecuteQuery("insert into CutomerOrderWallete values(" + Convert.ToInt32(orderid) + "," + Convert.ToInt32(walleteamt) + ")");
                DataTable dt1 = D.GetDataTable("select * from CustomerMaster where CustId=" + Convert.ToInt32(userid));
                if (dt1.Rows.Count > 0)
                {
                    D.ExecuteQuery("update CustomerMaster set RefPoints= RefPoints +" + Convert.ToInt32(walleteamt) + " where CustId=" + Convert.ToInt32(userid));
                    Context.Response.Write("1");
                }
                else
                {
                    Context.Response.Write("0");
                }
            }
            else
            {
                Context.Response.Write("0");
            }
        }
    }

    [WebMethod(Description = "Delete Customer By Id")]
    public void DeleteCustomer(string action, string userid)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "custdel")
        {
            DataTable dt = D.GetDataTable("select * from CustomerMaster where CustId=" + Convert.ToInt32(userid));
            if (dt.Rows.Count > 0)
            {
                D.ExecuteQuery("delete CustomerMaster  where CustId=" + Convert.ToInt32(userid));
                Context.Response.Write("1");
            }
            else
            {
                Context.Response.Write("0");
            }
        }
    }
    [WebMethod(Description = "Update Cuastomer")]
    public void UpdateCustomer(string action, string userid, string name, string mobile, string email, string address, string pincode, string area)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        if (action == "updatecustomer")
        {
            try
            {
                name = HttpUtility.UrlDecode(name);
                email = HttpUtility.UrlDecode(email);
                address = HttpUtility.UrlDecode(address);
                area = HttpUtility.UrlDecode(area);
                D.ExecuteQuery("update CustomerMaster set Name='" + name + "',MobileNo='" + mobile + "',Email='" + email + "',Address='" + address + "',PinCode='" + pincode + "',Area='" + area + "'  where CustId=" + Convert.ToInt32(userid));
                Context.Response.Write("1");
            }
            catch
            {
                Context.Response.Write("0");
            }
        }
    }
}


