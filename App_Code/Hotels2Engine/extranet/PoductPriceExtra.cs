using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class PoductPriceExtra : Hotels2BaseClass
    {
        public string PriceId { get; set; }
        public int ConditionId { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePrice { get; set; }
        public bool Status { get; set; }

        public List<object> getPriceExtraAll(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT price_id,condition_id,price,date_price FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND status = 1",cn);

                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> getPriceExtraAllByDateRange(int intConditionId, DateTime dDateStart , DateTime dDateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT price_id,condition_id,price,date_price FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price BETWEEN @date_start AND @date_end AND status = 1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        //public IList<object> getProductPriceAndAllot()
        //{

        //}

        public PoductPriceExtra getLatestDatePriceByCondition(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND status = 1 ORDER BY price_id DESC", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PoductPriceExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }
        public PoductPriceExtra getLatestDatePriceByDatePrice(int intConditionId, DateTime dDatePrice)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price=@date_price AND status = 1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDatePrice.Date;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PoductPriceExtra)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }
        public List<object> getPriceExtrabyCurrentDate(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))  AND status = 1 ORDER BY date_price", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getPriceExtrabyCurrentDateAndprice(int intConditionId, decimal decPrice)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE  condition_id=@condition_id AND price=@price AND date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND status = 1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        } 

        public PoductPriceExtra getPriceByDateMax(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND status = 1 ORDER BY price_id DESC", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PoductPriceExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public PoductPriceExtra getPriceByDateMinCurrentDate(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price >= >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND status = 1 ORDER BY price_id ASC", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PoductPriceExtra)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }

        public PoductPriceExtra getPriceByDateMin(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND status = 1 ORDER BY price_id ASC", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PoductPriceExtra)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }


        // Insert PriceExtra Rate Control  Manage
        public string InsertPriceExtraRateControl(int intProductId, short shrSupplierId, short shrStaffId)
        {
            string result = "False";
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["chk_to_insert"]))
            {
                string[] AllotToInsert = HttpContext.Current.Request.Form["chk_to_insert"].Split(',');
                if (AllotToInsert.Count() > 0)
                {
                    foreach (string rate in AllotToInsert)
                    {
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["rate_control_price_" + rate]))
                        {

                            int intConditionId = int.Parse(rate.Split('_')[0]);
                            DateTime DatePrice = rate.Split('_')[1].Hotels2DateSplitYear("-");
                            decimal decRatePrice = decimal.Parse(HttpContext.Current.Request.Form["rate_control_price_" + rate]);
                            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                            {
                                if (decRatePrice > 0)
                                {
                                    string strGUID = System.Guid.NewGuid().ToString();
                                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet (price_id,condition_id,price,date_price) VALUES (@price_id,@condition_id,@price,@date_price)", cn);
                                        cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                                        cmd.Parameters.Add("@price", SqlDbType.Money).Value = decRatePrice;
                                        cmd.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = DatePrice;
                                        cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strGUID;
                                        cn.Open();

                                        ExecuteNonQuery(cmd);
                                        //PriceId = (int)cmd.Parameters["@price_id"].Value;

                                        Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                                        cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, decRatePrice, DatePrice, shrStaffId, StaffLogActionType.Insert, cn);

                                }
                            }
                                
                        }
                    }
                }
               
            }
            result = "True";
            return result;
        }

        // Condition Control Price Update Optional !!
        public string InsertPriceExtranet_ConditionControl(int intProductId, short shrSupplierId, int intConditionId, short shrStaffId)
        {
        
            string result = "False";
            foreach (string RateItem in HttpContext.Current.Request.Form["rate_result_checked"].Split(','))
            {

                DateTime dDateStart_rate = HttpContext.Current.Request.Form["hd_rate_date_form_" + RateItem].Hotels2DateSplitYear("-");
                DateTime dDateEnd_rate = HttpContext.Current.Request.Form["hd_rate_date_To" + RateItem].Hotels2DateSplitYear("-");


                int DateDiff = dDateEnd_rate.Subtract(dDateStart_rate).Days;
                DateTime dDateCurrent = DateTime.Now;

                decimal Price = decimal.Parse(HttpContext.Current.Request.Form["hd_amount" + RateItem]);

                string DateSurCharge = HttpContext.Current.Request.Form["hd_day_checked_sur" + RateItem];

                decimal SurAmount = 0;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]))
                    SurAmount = decimal.Parse(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]);


                string[] DaySurcharge = DateSurCharge.Hotels2RightCrl(1).Split(',');
                
                //using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                //{

                   
                    int count = 1;

                    for (int days = 0; days <= DateDiff; days++)
                    {
                        decimal totalPrice = Price;
                        dDateCurrent = dDateStart_rate.AddDays(days);

                        

                        if (DaySurcharge.Count() > 0)
                        {

                            DayOfWeek dWeekDay = DateTime.Now.DayOfWeek;
                            foreach (string day in DaySurcharge)
                            {

                                switch (day)
                                {
                                    case "0":
                                        dWeekDay = DayOfWeek.Sunday;
                                        break;
                                    case "1":
                                        dWeekDay = DayOfWeek.Monday;
                                        break;
                                    case "2":
                                        dWeekDay = DayOfWeek.Tuesday;
                                        break;
                                    case "3":
                                        dWeekDay = DayOfWeek.Wednesday;
                                        break;
                                    case "4":
                                        dWeekDay = DayOfWeek.Thursday;
                                        break;
                                    case "5":
                                        dWeekDay = DayOfWeek.Friday;
                                        break;
                                    case "6":
                                        dWeekDay = DayOfWeek.Saturday;
                                        break;

                                }

                                if (dWeekDay == dDateCurrent.DayOfWeek)
                                    totalPrice = totalPrice + SurAmount;


                            }


                        }

                        string DateHolidaySurCharge = HttpContext.Current.Request.Form["hd_holiday_Sur" + RateItem];
                        if (!string.IsNullOrEmpty(DateHolidaySurCharge))
                        {
                            string[] arrHolidaySurcharge = DateHolidaySurCharge.Hotels2RightCrl(1).Split('#');

                            if (arrHolidaySurcharge.Count() > 0)
                            {
                                foreach (string holidayVal in arrHolidaySurcharge)
                                {
                                    string[] arrHolidayValue = holidayVal.Split(';');
                                    DateTime DAteHoliday = arrHolidayValue[0].Hotels2DateSplitYear("-");
                                    decimal HolidayChargeAmount = 0;
                                    if (!string.IsNullOrEmpty(arrHolidayValue[1]))
                                        HolidayChargeAmount = decimal.Parse(arrHolidayValue[1]);


                                    if (HolidayChargeAmount > 0 && DAteHoliday == dDateCurrent)
                                        totalPrice = totalPrice + HolidayChargeAmount;

                                }

                            }
                        }

                        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                        {


                            SqlCommand cmd3 = new SqlCommand("UPDATE tbl_product_option_condition_price_extranet SET price=@price  WHERE date_price=@date_price AND condition_id=@condition_id", cn);
                            // cmd3.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strPriceID;
                            cmd3.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                            cmd3.Parameters.Add("@price", SqlDbType.Money).Value = totalPrice;
                            cmd3.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDateCurrent;
                            //intConditionId
                            cn.Open();
                            int intRowUpdate = ExecuteNonQuery(cmd3);
                            if (intRowUpdate > 0)
                            {
                                Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                                cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, totalPrice, dDateCurrent, shrStaffId, StaffLogActionType.Update, cn);
                            }
                            else
                            {
                                string strGUID = Guid.NewGuid().ToString();
                                SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet (price_id,condition_id,price,date_price) VALUES (@price_id,@condition_id,@price,@date_price); SET @price_id=SCOPE_IDENTITY();", cn);
                                cmd2.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                                cmd2.Parameters.Add("@price", SqlDbType.Money).Value = totalPrice;
                                cmd2.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDateCurrent;
                                cmd2.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strGUID;


                                ExecuteNonQuery(cmd2);

                                Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                                cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, totalPrice, dDateCurrent, shrStaffId, StaffLogActionType.Insert, cn);
                            }


                            //##7## Insert Price '' tbl_product_option_condition_price_extranet

                        }

                        
                       
                        count = count + 1;

                    }
                //}

                result = "True";
            }

            return result;
        }




        //Update PriceExtra  RateControl
        public string UpdatePriceExtra_rateControl(int intProductId, short shrSupplierId, short shrStaffId)
        {
            string result = "False";
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["chk_to_update"]))
            {
                string[] RateAllotToUpdate = HttpContext.Current.Request.Form["chk_to_update"].Split(',');

                
                if (RateAllotToUpdate.Count() > 0)
                {
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        cn.Open();
                        foreach (string rate in RateAllotToUpdate)
                        {
                            string strPriceId = rate;

                            decimal decRatePrice = decimal.Parse(HttpContext.Current.Request.Form["rate_control_price_" + rate]);

                            SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_price_extranet SET price=@price  WHERE price_id=@price_id", cn);
                            cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strPriceId;
                            cmd.Parameters.Add("@price", SqlDbType.Money).Value = decRatePrice;

                            
                            ExecuteNonQuery(cmd);

                            SqlCommand cmd2 = new SqlCommand("SELECT price_id, condition_id,date_price FROM tbl_product_option_condition_price_extranet WHERE price_id = @price_id", cn);
                            int intConditionId = 0;
                            DateTime dDateTime = DateTime.MinValue;
                            cmd2.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strPriceId;
                            IDataReader reader = ExecuteReader(cmd2, CommandBehavior.SingleRow);
                            
                            if(reader.Read())
                            {
                                intConditionId = (int)reader["condition_id"];
                                dDateTime = (DateTime)reader["date_price"];
                            }
                             
                            reader.Close();
                            
                            Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                            cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, decRatePrice, dDateTime, shrStaffId, StaffLogActionType.Update, cn);
                            

                        }

                    }
                }
               
            }
            result = "True";
            return result;
        }


        // Delete Prie  Extrabed manage ** Actually is update status = 0
        public string DeletePriceExtra_extrabed(int intProductId, short shrSupplierId, int intConditionId, DateTime dDateStart, DateTime dDateEnd, short shrStaffId)
        {
            string result = "False";
            

            StringBuilder query = new StringBuilder();
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_condition_price_extranet WHERE condition_id = @condition_id AND date_price BETWEEN @dateStart AND @dateEnd ", cn);
                //SqlCommand cmd = new SqlCommand(query.ToString(), cn);

                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@dateStart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
         

                ExecuteNonQuery(cmd);

                 result = "True";
            }


            return result;
        }


        //UPDATE Prie  Extrabed manage
        public string UPDATEPriceExtra_extrbed(int intProductId, short shrSupplierId, int intConditionId, DateTime dDateStart, DateTime dDateEnd, short shrStaffId, decimal decPrice)
        {
            string result = "False";


            StringBuilder query = new StringBuilder();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id = @condition_id AND date_price BETWEEN @dateStart AND @dateEnd AND status = 1", cn);
                cmd2.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd2.Parameters.Add("@dateStart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd2.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;

                IDataReader reader = ExecuteReader(cmd2);
                IList<object> obj = MappingObjectCollectionFromDataReader(reader);
                reader.Close();
                foreach (PoductPriceExtra cPrice in obj)
                {

                    Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                    cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, decPrice, cPrice.DatePrice, shrStaffId, StaffLogActionType.Update, cn);

                }

                query.Append("UPDATE tbl_product_option_condition_price_extranet SET price =@price WHERE price_id IN (");
                query.Append(" SELECT price_id FROM tbl_product_option_condition_price_extranet");
                query.Append(" WHERE condition_id = @condition_id AND date_price BETWEEN @dateStart AND @dateEnd AND status = 1 )");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);

                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@dateStart", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@dateend", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;

                ExecuteNonQuery(cmd);

                result = "True";
            }


            return result;
        }


        //Insert Price Extra bed
        public string InsertPriceExtra_extrabed(int intProductId, short shrSupplierId, int intConditionId, DateTime dDateStart, DateTime dDateEnd, short shrStaffId)
        {
            string Iscompleted = "False";


            DateTime dDateCurrentMax = DateTime.Now;
            DateTime dDateCurrentMin = DateTime.Now;

           

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101))  AND status = 1 ORDER BY date_price", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                

                IDataReader reader = ExecuteReader(cmd);
                
                int cPriceCount = 0;
                if (reader.Read())
                    cPriceCount = 1;

                reader.Close();
                if (cPriceCount > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT date_price FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND status = 1 ORDER BY price_id DESC", cn);
                    cmd2.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                    
                    IDataReader reader2 = ExecuteReader(cmd2, CommandBehavior.SingleRow);
                    reader2.Read();
                    dDateCurrentMax = (DateTime)reader2[0];
                    reader2.Close();


                    SqlCommand cmd3 = new SqlCommand("SELECT date_price FROM tbl_product_option_condition_price_extranet WHERE condition_id=@condition_id AND date_price >= CONVERT(DateTime, convert(varchar(20),GETDATE(),101)) AND status = 1 ORDER BY price_id ASC", cn);
                    cmd3.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                    
                    IDataReader reader3 = ExecuteReader(cmd3, CommandBehavior.SingleRow);
                    reader3.Read();
                    dDateCurrentMin = (DateTime)reader3[0];
                    reader3.Close();
                }
                else
                {
                    dDateCurrentMax = dDateStart;
                    dDateCurrentMin = dDateEnd;
                }

                

                decimal ExtraAmount = decimal.Parse(HttpContext.Current.Request.Form["extrabed_amount_rate"]);

                DateTime dDateCurrent = DateTime.Now;
                int DateDiff = dDateEnd.Subtract(dDateStart).Days;
                
                for (int days = 0; days <= DateDiff; days++)
                {
                    dDateCurrent = dDateStart.AddDays(days);

                    string strGuid = System.Guid.NewGuid().ToString();
                    SqlCommand cmd4 = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet (price_id,condition_id,price,date_price) VALUES (@price_id,@condition_id,@price,@date_price)", cn);
                    cmd4.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                    cmd4.Parameters.Add("@price", SqlDbType.Money).Value = ExtraAmount;
                    cmd4.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDateCurrent;
                    cmd4.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strGuid;
                    

                    ExecuteNonQuery(cmd4);
                    

                    Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                    cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, ExtraAmount, dDateCurrent, shrStaffId, StaffLogActionType.Insert, cn);

                    
                }

                
            }

            Iscompleted = "True";
            return Iscompleted;
            
        }


        public string InsertPriceExtra_Loadtariff(int intProductId, short shrSupplierId, int intConditionId, short shrStaffId)
        {
            string result = "False";
            
              
                foreach (string RateItem in HttpContext.Current.Request.Form["rate_result_checked"].Split(','))
                {

                    DateTime dDateStart_rate = HttpContext.Current.Request.Form["hd_rate_date_form_" + RateItem].Hotels2DateSplitYear("-");
                    DateTime dDateEnd_rate = HttpContext.Current.Request.Form["hd_rate_date_To" + RateItem].Hotels2DateSplitYear("-");



                    int DateDiff = dDateEnd_rate.Subtract(dDateStart_rate).Days;
                    DateTime dDateCurrent = DateTime.Now;

                    decimal Price = decimal.Parse(HttpContext.Current.Request.Form["hd_amount" + RateItem]);
                    //byte bytAllotment = byte.Parse(Request.Form["hd_allot" + RateItem]);
                    //byte bytCutoff = byte.Parse(Request.Form["hd_cutoff" + RateItem]);


                    string DateSurCharge = HttpContext.Current.Request.Form["hd_day_checked_sur" + RateItem];

                    decimal SurAmount = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]))
                        SurAmount = decimal.Parse(HttpContext.Current.Request.Form["hd_surAmount" + RateItem]);


                    string[] DaySurcharge = DateSurCharge.Hotels2RightCrl(1).Split(',');


                    for (int days = 0; days <= DateDiff; days++)
                    {
                        decimal totalPrice = Price;
                        dDateCurrent = dDateStart_rate.AddDays(days);
                        if (DaySurcharge.Count() > 0)
                        {

                            DayOfWeek dWeekDay = DateTime.Now.DayOfWeek;
                            foreach (string day in DaySurcharge)
                            {

                                switch (day)
                                {
                                    case "0":
                                        dWeekDay = DayOfWeek.Sunday;
                                        break;
                                    case "1":
                                        dWeekDay = DayOfWeek.Monday;
                                        break;
                                    case "2":
                                        dWeekDay = DayOfWeek.Tuesday;
                                        break;
                                    case "3":
                                        dWeekDay = DayOfWeek.Wednesday;
                                        break;
                                    case "4":
                                        dWeekDay = DayOfWeek.Thursday;
                                        break;
                                    case "5":
                                        dWeekDay = DayOfWeek.Friday;
                                        break;
                                    case "6":
                                        dWeekDay = DayOfWeek.Saturday;
                                        break;

                                }

                                if (dWeekDay == dDateCurrent.DayOfWeek)
                                    totalPrice = totalPrice + SurAmount;
                                //else
                                //    totalPrice = Price;

                            }


                        }

                        string DateHolidaySurCharge = HttpContext.Current.Request.Form["hd_holiday_Sur" + RateItem];
                        if (!string.IsNullOrEmpty(DateHolidaySurCharge))
                        {
                            string[] arrHolidaySurcharge = DateHolidaySurCharge.Hotels2RightCrl(1).Split('#');
                            // Response.Write(arrHolidaySurcharge[0]);
                            if (arrHolidaySurcharge.Count() > 0)
                            {
                                foreach (string holidayVal in arrHolidaySurcharge)
                                {
                                    string[] arrHolidayValue = holidayVal.Split(';');
                                    DateTime DAteHoliday = arrHolidayValue[0].Hotels2DateSplitYear("-");
                                    decimal HolidayChargeAmount = 0;
                                    if (!string.IsNullOrEmpty(arrHolidayValue[1]))
                                        HolidayChargeAmount = decimal.Parse(arrHolidayValue[1]);


                                    if (HolidayChargeAmount > 0 && DAteHoliday == dDateCurrent)
                                        totalPrice = totalPrice + HolidayChargeAmount;
                                    //else
                                    //    totalPrice = Price;
                                }
                            }
                        }


                        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                        {

                            string strGUid = System.Guid.NewGuid().ToString();
                            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet (price_id,condition_id,price,date_price) VALUES (@price_id,@condition_id,@price,@date_price);", cn);
                            cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                            cmd.Parameters.Add("@price", SqlDbType.Money).Value = totalPrice;
                            cmd.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDateCurrent;
                            cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strGUid;
                            cn.Open();

                            ExecuteNonQuery(cmd);


                            Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                            cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, totalPrice, dDateCurrent, shrStaffId, StaffLogActionType.Insert, cn);
                        }

                        //cPrice.InsertPriceExtra(ProductID, Condition_id, totalPrice, dDateCurrent);

                    }

                }

                result = "True";
           // }
            

            return result;
        }

        // Insert One by one
        public string InsertPriceExtra(int intProductId, short shrSupplierId, int intConditionId, decimal decPrice, short shrStaffId, DateTime dDatePrice)
        {
            int ret = 0;
            string strGUID = System.Guid.NewGuid().ToString();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_price_extranet (price_id,condition_id,price,date_price) VALUES (@price_id,@condition_id,@price,@date_price)", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;
                cmd.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDatePrice;
                cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strGUID;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                //PriceId = (int)cmd.Parameters["@price_id"].Value;

                if (ret == 1)
                {
                    Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                    cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, decPrice, dDatePrice, shrStaffId, StaffLogActionType.Insert, cn);
                }

            }


            return strGUID;
        }

        // Update one by One
        public bool UpdatePriceExtra(int intProductId, short shrSupplierId, string strPriceId, decimal decPrice, DateTime dDatePrice, short shrStaffId, int intConditionId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_price_extranet SET price=@price  WHERE price_id=@price_id", cn);
                cmd.Parameters.Add("@price_id", SqlDbType.VarChar).Value = strPriceId;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;
                cmd.Parameters.Add("@date_price", SqlDbType.SmallDateTime).Value = dDatePrice;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                if (ret == 1)
                {
                    Staff_Activity_Price cStaffPriceAc = new Staff_Activity_Price();
                    cStaffPriceAc.InsertActivityRate(intProductId, shrSupplierId, intConditionId, decPrice, dDatePrice, shrStaffId,
StaffLogActionType.Update, cn);
                }
            }

            return (ret == 1);
        }
    }
}