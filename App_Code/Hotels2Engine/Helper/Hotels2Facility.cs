using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
/// <summary>
/// Summary description for Hotels2Facility
/// </summary>
/// 
namespace Hotels2thailand
{
    public class Hotels2Facility
    {
        public Hotels2Facility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static string[] _fac_EN_Product = new string[] { 
         "Airport Shuttle","Aerobics","ATM Service","Babysitting Service","Bar","Beauty Salon","Business Center","Car Park","Car Rental","Children Playground","Coffee Shop","Concierge",
         "Conference Room","Currency Exchange","Daily Maid Service","Doctor on call","Dry Cleaning Service","Elevator","Express Check In/Out"
        ,"Fitness Room","Front Desk","Game Room","Garage Parking","Gift Shop","Golf Course","Guest Services Desk","Gym","High Speed Internet Access","Internet Corner"
        ,"Internet Shop","Laundry Service","Limousine Service","Lounge","Luggage Storage","Meeting room","Massage","Playground","Postal Service","Restaurant","Room Service","Safety Boxes at Front Desk","Sauna"
        ,"Spa","Swimming pool","Steam room","Shuttle Bus Service","Security 24 hours","Taxi/Hires Service","Telephone Service","Tour Desk","Transfer Service","Travel Service","Wake up Service","Wi - Fi Internet","Wireless Internet Connection","Dining room"};

        private static string[] _fac_TH_Product = new string[] { 
         "รถรับ-ส่ง สนามบิน","แอโรบิค","ตู้เอทีเอ็ม","บริการรับเลี้ยงเด็กเล็ก","บาร์","ร้านเสริมสวย","บริการสำหรับนักธุรกิจ","ที่จอดรถ","รถเช่า","คอฟฟี่ชอป","แผนกสอบถาม","สนามเทนนิส","สวน","คอวซ์","สนุกเกอร์","ที่รับแลกเงินตราต่างประเทศ","พนักงานทำความสะอาดทุกวัน","บริการด้านการแพทย์","บริการซักแห้ง","ลิฟต์","บริการเช๊คอิน/เอ้าท์แบบเร่งด่วน","ห้องออกกำลังกาย","พนักงานต้อนรับ","ห้องเกมส์","โรงจอดรถ","ร้านขายของ","สนามกอล์ฟ"
        ,"บริการดูแลลูกค้า","สถานที่ออกกำลังกาย","บริการอินเตอร์เนตความเร็วสูง","มุมบริการอินเตอร์เนต","ร้านบริการอินเตอร์เนต","บริการซักรีดเสื้อผ้า","ลีมูซีน","เล้าจ์น","ฝากสัมภาระ","ห้องประชุม","บริการนวด","สนามเด็กเล่น","บริการด้านไปรษณีย์","ร้านอาหาร","บริการเสริฟ์อาหารในห้องพัก","บริการตู้เซฟที่แผนกต้อนรับ","ซาวน่า","สปา","สระว่ายน้ำ","ห้องอบไอน้ำ","รถรับ-ส่ง บริการท่องเที่ยว","รักษาความปลอดภัย 24 ชม.","บริการเรียกรถแท็กซี่",
        "บริการโทรศัพท์","บริการทัวร์","บริการรถรับ-ส่ง","บริการจัดโปรแกรมท่องเที่ยว","บริการปลุกตอนเช้า","อินเตอร์เน็ตไร้สาย","ห้องอาหาร"};

        private static string[] _fac_EN_Option = new string[] { 
         "Air-Conditioning","Balcony","Bath Amenities Set","Bath Tub","Bathrobe","Cable TV","CD/DVD Player","Ceiling fan","Coffee/Tea Making Facilities","Electric Kettle ","Electric Oven","Fan","Fridge ","Furniture","Hairdryer","High Speed Internet","IDD Telephone"
        ,"Internet Access","Kitchen area","Kitchenette","Living room","Microwave","Mini Bar","Pantry","Rain Shower","Refrigerator","Room Service ","Safe Deposit Box","Satellite TV","Shower","Slipper","Telephone ","Terrace","Toaster","Toilet Amenities","TV","Umbrella",
        "Walk-in Closet","Wardrobe","Washing Machine","Wifi Internet access","Working Desk","Water Heat"};

        private static string[] _fac_TH_Option = new string[] { 
         "เครื่องปรับอากาศ","เฉลียง","ของใช้สำหรับอาบน่ำ","อ่างอาบน้ำ","เสื้อคลุมอาบน้ำ","เคเบิลทีวี","เครื่องเล่น CD/ DVD","พัดลมเพดาน","ชุดชงชากาแฟ","กระติกน้ำร้อนไฟฟ้า","เตาอบไฟฟ้า","พัดลม","ตู้เย็น","เฟอร์นิเจอร์","เครื่องเป่าผม",
         "อินเตอร์เนต - ความเร็วสูง","โทรศัพท์สายตรง","อินเตอร์เนต","ห้องครัว","ชุดเครื่องครัว","ห้องนั่งเล่น","เตาอบไมโครเวฟ","มินิบาร์","บริเวณครัว","ฝักบัวแบบเรนชาวเว่อร์","บริการอาหารในห้องพัก","ตู้เซฟในห้อง","ทีวีผ่านดาวเทียม","ฝักบัว","รองเท้าแตะ","โทรศัพท์","ระเบียง","เครื่องปิ้งขนมปัง","ของใช้สำหรับห้องน้ำ"
        ,"ทีวี","ร่ม","ตู้เสื้อผ้าใหญ่","ตู้เสื้อผ้า","เครื่องซักผ้า","อินเตอร์เนตไร้สาย","โต๊ะทำงาน","เครื่องทำน้ำอุ่น"};

        private static string[] _empty_Lang = new string[] { 
         "Please select one..."};

        public static StringCollection GetFacilityProduct(int intLangId)
        {
            StringCollection countries = new StringCollection();

            switch (intLangId)
            {
                case 1:
                    countries.AddRange(_fac_EN_Product);
                    break;
                case 2:
                    countries.AddRange(_fac_TH_Product);
                    break;
                default :
                    countries.AddRange(_empty_Lang);
                    break;
            }
            
            return countries;
        }

        public static StringCollection GetFacilityOption(int intLangId)
        {
            StringCollection countries = new StringCollection();

            switch (intLangId)
            {
                case 1:
                    //HttpContext.Current.Response.Write("1");
                    //HttpContext.Current.Response.End();
                    countries.AddRange(_fac_EN_Option);
                    break;
                case 2:
                    //HttpContext.Current.Response.Write("2");
                    //HttpContext.Current.Response.End();
                    countries.AddRange(_fac_TH_Option);
                    break;
                default:
                    countries.AddRange(_empty_Lang);
                    break;
            }

            return countries;
        }
        
        //public static SortedList GetCountries(bool insertEmpty)
        //{
        //    SortedList countries = new SortedList();
        //    if (insertEmpty)
        //        countries.Add("", "Please select one...");
        //    foreach (String country in _fac_TH)
        //        countries.Add(country, country);
        //    return countries;
        //}

    }
}