using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Memberphone
/// </summary>
/// 
namespace Hotels2thailand.Member
{
    public class MemberCustomerPhone
    {
        public int PhoneID { get; set; }
        public byte Category { get; set; }
        public int CustomerID { get; set; }
        public string CountryCode { get; set; }
        public string LocalCode { get; set; }
        public string PhoneNumber { get; set; }

        //public int Insert(MemberCustomerPhone data)
        //{
        //    tbl_customer_phone phone = new tbl_customer_phone
        //    {
        //        cat_id = data.Category,
        //        cus_id = data.CustomerID,
        //        code_country = data.CountryCode,
        //        code_local = data.LocalCode,
        //        number_phone = data.PhoneNumber
        //    };

        //    dcPhone.tbl_customer_phones.InsertOnSubmit(phone);
        //    dcPhone.SubmitChanges();
        //    return phone.phone_id;
        //}
    }
}
