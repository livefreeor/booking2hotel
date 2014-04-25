using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;

/// <summary>
/// Summary description for StaffActivity
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public enum StaffLogModule : byte
    {
        Product_detail = 11,
        Product_policy = 12,
        Product_option_detail = 13,
        Product_rate = 14,
        Product_weekend = 15,
        Product_holiday = 16,
        Product_allotment = 17,
        Product_promotion = 18,
        Product_announcement = 19,
        Product_minimumstay = 20,
        Product_construction = 21,
        Product_gala = 22,
        Product_Itinerary = 23,
        Product_picture = 24,
        Booking = 25,
        Staff = 26,
        Review = 27,
        Central_data_Martket = 28,
        Product_option_supplier = 29,
        Product_supplier = 30,
        Product_option_condition = 31,
        Product_Period = 32,
        Product_Supplement = 33,
        Supplier_detail = 34,
        Supplier_account = 35,
        Supplier_contact = 36,
        Supplier_deposit = 37, 
        Supplier_payment = 38,
        Account_settle_report = 39,
        Product_paymentPlan = 40,
        Booking_Card_Detail = 41,
        Product_commission = 42,
        Product_Condition_Extra = 43,
        Product_Price_Extra = 44,
        Product_Cancel_Extra = 45,
        Central_Data_location = 46,
        Central_Data_FacilityTemplate = 47,
        Product_Deposit = 47
    }
}
