using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GlobalVar
/// </summary>
public static class GlobalVar
{
    public static string[,] GetProductCategoryTitle() 
    {
        string[,] strProductType = new string[7, 5]; //4record

        strProductType[0, 0] = "29";
        strProductType[0, 1] = "Hotels";
        strProductType[0, 2] = "thailand-hotels.asp";
        strProductType[0, 3] = "hotels";
        strProductType[0, 4] = "โรงแรม";
        strProductType[1, 0] = "32";
        strProductType[1, 1] = "Golf Courses";
        strProductType[1, 2] = "thailand-golf-courses.asp";
        strProductType[1, 3] = "golf";
        strProductType[1, 4] = "สนามกอล์ฟ";
        strProductType[2, 0] = "36";
        strProductType[2, 1] = "Water Activities";
        strProductType[2, 2] = "thailand-water-activity.asp";
        strProductType[2, 3] = "water-activity";
        strProductType[2, 4] = "กิจกรรมทางน้ำ";
        strProductType[3, 0] = "38";
        strProductType[3, 1] = "Shows";
        strProductType[3, 2] = "thailand-show-event.asp";
        strProductType[3, 3] = "show-event";
        strProductType[3, 4] = "การแสดงโชว์";
        strProductType[4, 0] = "39";
        strProductType[4, 1] = "Health Check Up";
        strProductType[4, 2] = "thailand-health-check-up.asp";
        strProductType[4, 3] = "health-check-up";
        strProductType[4, 4] = "ศูนย์ตรวจสุขภาพ";
        strProductType[5, 0] = "40";
        strProductType[5, 1] = "Spa";
        strProductType[5, 2] = "thailand-spa.asp";
        strProductType[5, 3] = "spa";
        strProductType[5, 4] = "สปา";
        strProductType[6, 0] = "34";
        strProductType[6, 1] = "Day Trips";
        strProductType[6, 2] = "thailand-day-trips.asp";
        strProductType[6, 3] = "day-trips";
        strProductType[6, 4] = "เดย์ทริป";

        return strProductType;
    }

    public static string GetPolicyTitleExtranet(string strPolicy)
    {
        string result = string.Empty;
        switch (strPolicy)
        {
            case "Check-in":
                result = "เช็คอิน";
                break;
            case "Check-out":
                result = "เช็คเอ้าท์";
                break;
            case "Pets" :
                result = "สัตว์เลี้ยง";
                break;
            case "Child":
                result = "เด็ก";
                break;
            case "Refundable":
                result = "สามารถคืนเงินได้";
                break;
            case "Non refundable & No amendment":
                result="ไม่สามารถเปลี่ยนแปลงและคืนเงินได้";
                break;
            case "Non refundable":
                result = "ไม่สามารถคืนเงินได้";
                break;
            default:
                result = strPolicy;
                break;

        }
        return result;
    }



}