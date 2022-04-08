using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServicesClass
/// </summary>

public class Message
{
    public string msg { get; set; }
}

public class Customers
{
    public string CustId { set; get; }
    public string Name { set; get; }
    public string MobileNo { set; get; }
    public string Email { set; get; }
    public string Address { set; get; }
    public string Area { set; get; }
    public string Pincode { set; get; }
    public string CityId { set; get; }
    public string StateId { set; get; }
    public string CountryId { set; get; }
    public string DeviceId { set; get; }
    public string DeviceType { set; get; }
    public string Status { set; get; }
    public string NewUser { set; get; }
    public string ReferralCode { set; get; }
}

public class SocialLogin
{
    public string CustId { set; get; }
    public string Name { set; get; }
    public string MobileNo { set; get; }
    public string Email { set; get; }
    public string Address { set; get; }
    public string Area { set; get; }
    public string Pincode { set; get; }
    public string CityId { set; get; }
    public string StateId { set; get; }
    public string CountryId { set; get; }
    public string DeviceId { set; get; }
    public string DeviceType { set; get; }
    public string Status { set; get; }
    public string NewUser { set; get; }
    public string ReferralCode { set; get; }
}

public class PincodeMaster
{
    public string Pincode { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    //public string State { get; set; }
    //public string Country { get; set; }
}

public class MaxOrder
{
    public double OrderId { get; set; }
    public string CustId { get; set; }
    public string OrderDt { get; set; }
    //public string State { get; set; }
    //public string Country { get; set; }
}


public class CityMaster
{
    public string id { get; set; }
    public string city { get; set; }
    public string stateid { get; set; }
}

public class StateMaster
{
    public string stateid { get; set; }
    public string name { get; set; }
    public string type { get; set; }
}

public class Categories
{
    public string CatId { get; set; }
    public string CategoryName { get; set; }
    public string ParentId { get; set; }
    public string desc { get; set; }
    public string imgpath { get; set; }
}

public class ItemMst
{
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public string CatId { get; set; }
    public string SubCatId { get; set; }
    public string MRP { get; set; }
    public string Description { get; set; }
    public string BigImg { get; set; }
    public string SmallImg { get; set; }
    public string IconImg { get; set; }
    public string SellingPrice { get; set; }
    public string CompanyName { get; set; }
    public string Per { get; set; }
}

public class Languages
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Labels
{
    public string Id { get; set; }
    public string LabelName { get; set; }
}

public class langLabels
{
    public string ID { get; set; }
    public string LangId { get; set; }
    public string LabelId { get; set; }
}

public class StatusMst
{
    public string ID { get; set; }
    public string Status { get; set; }
}

public class PolicyMaster
{
    public string ID { get; set; }
    public string Name { get; set; }
}

public class OrderMaster
{
    public string OrderId { get; set; }
    public string OrderDt { get; set; }
    public string CustId { get; set; }
    public string TotalAmt { get; set; }
    public string DiscountAmt { get; set; }
    public string NetAmt { get; set; }
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public string Qty { get; set; }
    public string ItemPrice { get; set; }
    public string ItemType { get; set; }

}

public class OrderDetailMaster
{
    public string OrderDetailId { get; set; }
    public string OrderId { get; set; }
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public string ItemPrice { get; set; }
    public string Qty { get; set; }
    public string TotalAmt { get; set; }
    public string OrderDt { get; set; }
    public string ItemType { get; set; }
    public string ShippingCharges { get; set; }
    public string wallet { get; set; }
}

public class InsertOrders
{
    public string OrderDetailId { get; set; }
    public string OrderId { get; set; }
    public string OrderDt { get; set; }
    public string CustId { get; set; }
    public string TotalAmt { get; set; }
    public string DiscountAmt { get; set; }
    public string NetAmt { get; set; }
    public string ItemId { get; set; }
    public string ItemName { get; set; }
    public string Price { get; set; }
    public string Qty { get; set; }
    public string Amt { get; set; }
    public string ShippingCharges { get; set; }
    public string status { get; set; }
    public string ItemType { get; set; }

}
public class DoctorMaster
{
    public string DoctorId { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Specialization { get; set; }
    public string MobileNo { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }

}
public class ContactMaster
{
    public string Address { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class BrandMaster
{
    public string brandid { get; set; }
    public string brandname { get; set; }
    public string count { get; set; }
}