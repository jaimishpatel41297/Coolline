using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for GetData
/// </summary>
/// 

public class GetData
{

    public SqlConnection conn;
    public SqlConnection conn1;
    public static String WEBSITE_NAME = "Buyerfox";
    public static String COMPLETE_ORDER_STATUS_ID = "1";
    public static String LOW_STOCK = "10";
    public static String WEBSITE = "buyerfox.com";
    //public static String WEBSITE_URL = "http://localhost:54370/";
    public static String WEBSITE_URL = "https://buyerfox.com/";
    public static String WEBSITE_TITLE = "Buyerfox Retails Pvt Ltd";

    public static String FIRST_FLIGHT_USERNAME = "1097344-N";
    public static String FIRST_FLIGHT_PASSWORD = "BuY@RfOx#1097N";

    public static String FIRST_FLIGHT_TRACK_PIN_USERNAME = "1097344-A";
    public static String FIRST_FLIGHT_TRACK_PIN_PASSWORD = "BuY@RfOx#1097";

    public static String FIRST_FLIGHT_CHECK_PIN_USERNAME = "ffcl";
    public static String FIRST_FLIGHT_CHECK_PIN_PASSWORD = "password1234";

    public static String FIRST_FLIGHT_BELOW_5KG_COD = "0002001708";
    public static String FIRST_FLIGHT_ABOVE_5KG_COD = "0002001709";
    public static String FIRST_FLIGHT_BELOW_5KG_NONCOD = "0001097345";
    public static String FIRST_FLIGHT_ABOVE_5KG_NONCOD = "0001097344";


    public static String INFO_BUYEROX_PWD = "Buyerfox@#*12345";
    public static String ORDER_BUYEROX_PWD = "#%rajveer9211";

    public GetData()
    {
        //Connection String
        conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DealJeeto"].ConnectionString);
        conn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DealJeeto"].ConnectionString);
    }

    public DataTable GetDataTable(string strqry)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cm = new SqlCommand(strqry, conn);
            conn.Open();
            cm.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public DataTable GetDataTableBySP(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cm = cmd;
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = conn;
            conn.Open();
            cm.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }

    public int GetMaxId(string SqlQuery)
    {
        int id = 0;
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cm = new SqlCommand(SqlQuery, conn);
            conn.Open();
            cm.ExecuteNonQuery();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        return id;
    }
    public string GetChallanNo()
    {
        int id = 0;
        string billid = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cm = new SqlCommand("select challanno from ChallanMaster order by challanno desc", conn);
            conn.Open();
            cm.ExecuteNonQuery();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
                id++;
            }
            else
            {
                id = 3560;
            }
            billid = id.ToString();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        return billid;
    }
    public string GetBillNo()
    {
        int id = 1000;
        string billid = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cm = new SqlCommand("select id from BillMaster order by id desc", conn);
            conn.Open();
            cm.ExecuteNonQuery();
            conn.Close();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                id = id + Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            billid = "BILL" + id.ToString();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        return billid;
    }
    public void ExecuteQuery(string SqlQuery)
    {
        try
        {
            SqlCommand cm = new SqlCommand(SqlQuery, conn);
            conn.Open();
            cm.ExecuteNonQuery();

        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }

    public void ExecuteQueryBySP(SqlCommand cmd)
    {
        try
        {
            SqlCommand cm = cmd;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cm.ExecuteNonQuery();

        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }

    public Int32 ExecuteQueryBySpWithReturn(SqlCommand cmd)
    {
        try
        {
            SqlCommand cm = cmd;
            cm.Connection = conn;
            cm.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cm.ExecuteNonQuery();
            Int32 id = Convert.ToInt32(cmd.Parameters["@paymentid"].Value.ToString());
            conn.Close();
            return id;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Fetch Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }




    public string StringEncrypt(string inText)
    {
        string key = "@NB$T|_|DYF!ELD@9879";
        byte[] bytesBuff = Encoding.Unicode.GetBytes(inText);
        using (Aes aes = Aes.Create())
        {
            Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            aes.Key = crypto.GetBytes(32);
            aes.IV = crypto.GetBytes(16);
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cStream.Write(bytesBuff, 0, bytesBuff.Length);
                    cStream.Close();
                }
                inText = Convert.ToBase64String(mStream.ToArray());
            }
        }
        return inText;
    }
    //Decrypting a string
    public string StringDecrypt(string cryptTxt)
    {
        try
        {
            string key = "@NB$T|_|DYF!ELD@9879";
            cryptTxt = cryptTxt.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(cryptTxt);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
                }
            }
        }
        catch
        {

        }
        return cryptTxt;
    }




}
