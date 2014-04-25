using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Activities.Statements;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for allotment
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class Allotment:Hotels2BaseClass
    {
        

        public int AllotmentId { get; set; }
        public short SupplierId { get; set; }
        public int OptionId { get; set; }
        public DateTime DateAllotment { get; set; }
        public int NumDateCutOff { get; set; } 
        public DateTime DateCutOff { get; set; }
        private Nullable<int> _total_quantity;
        public Nullable<int> TotalQuantity
        {
            get { return _total_quantity;}
            set{_total_quantity = (int)value;}
        }
        public bool Status { get; set; }

        
        private int _product_id = 0;
        public int ProductId
        {
            get {
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pid"]))
                    {
                        _product_id = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                    }
                
                    return _product_id;
                }
            set { _product_id = value; }
        }

        public Allotment()
        {
            _total_quantity = 0;
        }

        public Allotment(int ProductID)
        {
            _total_quantity = 0;
            this.ProductId = ProductID;
        }


        //public IList<ArrayList> getAllotmentCheckDashBoard(int intOptionId, short shrSupplierId)
        //{
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        IList<ArrayList> iArrList = new List<ArrayList>();

        //        SqlCommand cmd = new SqlCommand("bk_extranet_get_allotment_check_dashboard", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
        //        cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
        //        cn.Open();
        //        IDataReader reader = ExecuteReader(cmd);

        //        while (reader.Read())
        //        {
        //            ArrayList list = new ArrayList();
        //            list.Add(reader[0]);
        //            list.Add(reader[1]);

        //            iArrList.Add(list);
        //        }

        //        return iArrList;
        //    }
        //}


        public List<object> getAllotMentListByOptionId(int intOptionId, short shrSupplierId,DateTime dDateStart, DateTime dDateEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT al.allotment_id,al.supplier_id, al.option_id, al.date_allotment , DATEDIFF(day, al.date_cut_off, al.date_allotment) AS no_of_date_cut_off , al.date_cut_off, (");
                query.Append(" (SELECT SUM(ala.quantity) FROM tbl_allotment_activity ala WHERE ala.allotment_id = al.allotment_id AND ala.cat_id IN (1,3,6)) - coalesce((SELECT SUM(alas.quantity)");
                query.Append(" FROM tbl_allotment_activity alas WHERE alas.allotment_id = al.allotment_id AND alas.cat_id IN (2,4,5)),0) ) AS total_quantity, al.status FROM tbl_allotment al");
                query.Append(" WHERE al.option_id = @option_id AND al.supplier_id = @supplier_id AND al.date_allotment BETWEEN @date_start AND @date_end ORDER BY al.date_allotment");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_start", SqlDbType.DateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.DateTime).Value = dDateEnd;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public Allotment getRoomQuantityByAllotMentId(int intAllotmentId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT al.allotment_id,al.supplier_id, al.option_id, al.date_allotment , DATEDIFF(day, al.date_cut_off, al.date_allotment) AS no_of_date_cut_off , al.date_cut_off, (");
                query.Append(" (SELECT SUM(ala.quantity) FROM tbl_allotment_activity ala WHERE ala.allotment_id = al.allotment_id AND ala.cat_id IN (1,3,6)) - coalesce((SELECT SUM(alas.quantity)");
                query.Append(" FROM tbl_allotment_activity alas WHERE alas.allotment_id = al.allotment_id AND alas.cat_id IN (2,4,5)),0) ) AS total_quantity, al.status FROM tbl_allotment al");
                query.Append(" WHERE al.allotment_id = @allotment_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intAllotmentId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Allotment)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }

        private IList<object> allotmentResultAvaliable = null;
        public string CheckAllotAvaliable_Cutoff(short shrSupplierId, int intOptionId, int Quantity, DateTime dDateStart, DateTime dDateEnd)
        {
           
            int countAllotComplete = 0;
            DateTime dateCheck = dDateStart;
            //IList<object> result = null;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT al.allotment_id,al.supplier_id, al.option_id, al.date_allotment , DATEDIFF(day, al.date_cut_off, al.date_allotment) AS");
                query.Append(" no_of_date_cut_off , al.date_cut_off, (");
                query.Append(" (SELECT SUM(ala.quantity) FROM tbl_allotment_activity ala WHERE ala.allotment_id = al.allotment_id AND ala.cat_id IN (1,3,6)) - ");
                query.Append(" coalesce((SELECT SUM(alas.quantity)");
                query.Append(" FROM tbl_allotment_activity alas WHERE alas.allotment_id = al.allotment_id AND alas.cat_id IN (2,4,5)),0) ) AS total_quantity,");
                query.Append(" al.status FROM tbl_allotment al");
                query.Append(" WHERE al.supplier_id = @supplier_id AND al.option_id = @option_id AND al.status = 1");
                query.Append(" AND al.date_allotment BETWEEN @date_start AND @date_end  ORDER BY al.date_allotment ");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_start", SqlDbType.DateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.DateTime).Value = dDateEnd;
                cn.Open();

                allotmentResultAvaliable = MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            int TotalNight = (int)dDateEnd.Subtract(dDateStart).Days;
            int DateCheckInCutoff = dDateStart.Subtract(DateTime.Now.Hotels2ThaiDateTime().Date).Days;
            string REsult = string.Empty;
            if (allotmentResultAvaliable.Count > 0)
            {

                //int ResultAvaliable = (allotmentResultAvaliable.Count - 1);
                for (int intNight = 0; intNight < TotalNight; intNight++)
                {
                    dateCheck = dDateStart.AddDays(intNight);
                    foreach (Allotment allot in allotmentResultAvaliable)
                    {
                        if (dateCheck.Subtract(allot.DateAllotment).Days == 0 && allot.TotalQuantity > 0 && dateCheck.Subtract(allot.DateCutOff).Days >= 0 && DateCheckInCutoff >= allot.NumDateCutOff)
                        {
                            countAllotComplete = countAllotComplete + 1;
                            REsult = REsult + allot.TotalQuantity + ",";
                        }
                    }
                }
            }
            // HttpContext.Current.Response.Write(countAllotComplete);
            if (countAllotComplete == TotalNight)
            {
                REsult = REsult.Hotels2RightCrl(1);
            }
            else
            {
                
                REsult = string.Empty;
            }
            return REsult;
        }
        public bool CheckAllotAvaliable(short shrSupplierId, int intOptionId, int Quantity, DateTime dDateStart, DateTime dDateEnd)
        {
            bool IsAval = true;
            int countAllotComplete = 0;
            DateTime dateCheck = dDateStart;
            //IList<object> result = null;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT al.allotment_id,al.supplier_id, al.option_id, al.date_allotment , DATEDIFF(day, al.date_cut_off, al.date_allotment) AS");
                query.Append(" no_of_date_cut_off , al.date_cut_off, (");
                query.Append(" (SELECT SUM(ala.quantity) FROM tbl_allotment_activity ala WHERE ala.allotment_id = al.allotment_id AND ala.cat_id IN (1,3,6)) - ");
                query.Append(" coalesce((SELECT SUM(alas.quantity)");
                query.Append(" FROM tbl_allotment_activity alas WHERE alas.allotment_id = al.allotment_id AND alas.cat_id IN (2,4,5)),0) ) AS total_quantity,");
                query.Append(" al.status FROM tbl_allotment al");
                query.Append(" WHERE al.supplier_id = @supplier_id AND al.option_id = @option_id AND al.status = 1");
                query.Append(" AND al.date_allotment BETWEEN @date_start AND @date_end  ORDER BY al.date_allotment ");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_start", SqlDbType.DateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.DateTime).Value = dDateEnd;
                cn.Open();

                allotmentResultAvaliable = MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            int TotalNight = (int)dDateEnd.Subtract(dDateStart).Days;
            int DateCheckInCutoff = dDateStart.Subtract(DateTime.Now.Hotels2ThaiDateTime().Date).Days;
            if (allotmentResultAvaliable.Count > 0)
            {

                //int ResultAvaliable = (allotmentResultAvaliable.Count - 1);
                for (int intNight = 0; intNight < TotalNight; intNight++)
                {
                    dateCheck = dDateStart.AddDays(intNight);
                    foreach (Allotment allot in allotmentResultAvaliable)
                    {
                        if (dateCheck.Subtract(allot.DateAllotment).Days == 0 && allot.TotalQuantity >= Quantity && dateCheck.Subtract(allot.DateCutOff).Days >= 0 && DateCheckInCutoff >= allot.NumDateCutOff)
                        {
                            countAllotComplete = countAllotComplete + 1;
                        }
                    }
                }
            }
            // HttpContext.Current.Response.Write(countAllotComplete);
            if (countAllotComplete == TotalNight)
            {
                IsAval = true;
            }
            else
            {
                IsAval = false;
            }
            return IsAval;
        }

        public bool UpdateAllotFromBookingProcess(short shrSupplierId, int intOptionId, byte Quantity, DateTime dDateStart, DateTime dDateEnd)
        {
            bool IsCompleted = true;

            byte BookingProcessCat = 7;

            //if(this.CheckAllotAvaliable(shrSupplierId,intOptionId,Quantity,dDateStart,dDateEnd) == true)
            //{

            foreach (Allotment allot in allotmentResultAvaliable)
            {
                this.UpdateAllotViaBookingProcess(allot.AllotmentId, BookingProcessCat, Quantity);
            }

            //}
            return IsCompleted;
        }



        public void UpdateAllotViaBookingProcess(int intAllotId, byte bytStaffCatAndBookingProcess, byte bytRoomQuantity)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO tbl_allotment_activity(allotment_id,cat_id,quantity,date_activity,comment)VALUES");
            query.Append(" (@allotment_id, @cat_id, @quantity, @date_activity, @comment); SET @activity_id = SCOPE_IDENTITY()");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intAllotId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = 5;
                cmd.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = bytRoomQuantity;
                cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = "Allotment has used by booking system";
                cmd.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                int intNewactivity = (int)cmd.Parameters["@activity_id"].Value;

                ////#Staff_Activity_Log==========================================================================================================
                //StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_allotment, StaffLogActionType.Insert, StaffLogSection.Product, this.ProductId,
                //    "tbl_allotment_activity", "allotment_id,cat_id,quantity,date_activity,comment", "activity_id", intNewactivity);
                ////============================================================================================================================

            }
        }



        public ArrayList getDateRangeByOptionId(int intOptionId ,short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT MIN( date_allotment) AS datestart ,  MAX(date_allotment) AS dateEnd");
                query.Append(" FROM tbl_allotment WHERE option_id = @option_id AND supplier_id=@supplier_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ArrayList arryOutput = new ArrayList();
                    arryOutput.Add((DateTime)reader[0]);
                    arryOutput.Add((DateTime)reader[1]);
                    return arryOutput;
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdateAllotmentAndInsertNewActivityByAllotmentId(int intAllotId, byte bytStaffCatAndBookingProcess, byte bytRoomQuantity)
        {
            //HttpContext.Current.Response.Write("TEST");
            //HttpContext.Current.Response.End();

            int RoomQuant = (int)this.getRoomQuantityByAllotMentId(intAllotId).TotalQuantity;
            if (RoomQuant != bytRoomQuantity)
            {
                int bytQuan = 0;
                int bytAllotmentActivityCat = 0;

                string StrComment = string.Empty;
                if (RoomQuant < bytRoomQuantity)
                {
                    bytQuan = (bytRoomQuantity - RoomQuant);
                    // check Staff Cat Not Hotels Partner 
                    if (bytStaffCatAndBookingProcess != 6)
                    {
                        //Add By Bluehouse
                        bytAllotmentActivityCat = 1;
                        StrComment = "Add allotment By BlueHouse";

                        // check From BOoking Process
                        if (bytStaffCatAndBookingProcess == 7)
                        {
                            bytAllotmentActivityCat = 6;
                            StrComment = "Release Allotment From Booking Process";
                        }
                    }
                    else
                    {
                        //Add By Hotel
                        bytAllotmentActivityCat = 3;
                        StrComment = "Add allotment By Hotel";
                    }
                }
                else
                {

                    bytQuan = (RoomQuant - bytRoomQuantity);
                    // check Staff Cat Not Hotels Partner 
                    if (bytStaffCatAndBookingProcess != 6)
                    {
                        //Remove By Bluehouse
                        bytAllotmentActivityCat = 2;
                        StrComment = "Remove allotment By BlueHouse";

                        // check From BOoking Process
                        if (bytStaffCatAndBookingProcess == 7)
                        {
                            bytAllotmentActivityCat = 5;
                            StrComment = "Allotment Sold From BookingProcess";
                        }
                    }
                    else
                    {
                        //Remove By Hotel
                        bytAllotmentActivityCat = 4;
                        StrComment = "Remove allotment By Hotel";
                    }
                }

                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO tbl_allotment_activity(allotment_id,cat_id,quantity,date_activity,comment)VALUES");
                query.Append(" (@allotment_id, @cat_id, @quantity, @date_activity, @comment); SET @activity_id = SCOPE_IDENTITY()");
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intAllotId;
                    cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytAllotmentActivityCat;
                    cmd.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = bytQuan;
                    cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                    cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = StrComment;
                    cmd.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cn.Open();
                    ExecuteNonQuery(cmd);
                    int intNewactivity = (int)cmd.Parameters["@activity_id"].Value;

                    //#Staff_Activity_Log==========================================================================================================
                    StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_allotment, StaffLogActionType.Insert, StaffLogSection.Product, this.ProductId,
                        "tbl_allotment_activity", "allotment_id,cat_id,quantity,date_activity,comment", "activity_id", intNewactivity);
                    //============================================================================================================================

                }

            }


        }

        public void UpdateAllotmentAndInsertNewActivityByAllotmentId(int intAllotId, byte bytStaffCatAndBookingProcess, byte bytRoomQuantity, byte bytCutoff, bool status)
        {
            //HttpContext.Current.Response.Write("TEST");
            //HttpContext.Current.Response.End();
            
                int RoomQuant = (int)this.getRoomQuantityByAllotMentId(intAllotId).TotalQuantity;
                if (RoomQuant != bytRoomQuantity)
                {
                    int bytQuan = 0;
                    int bytAllotmentActivityCat = 0;

                    string StrComment = string.Empty;
                    if (RoomQuant < bytRoomQuantity)
                    {
                        bytQuan = (bytRoomQuantity - RoomQuant);
                        // check Staff Cat Not Hotels Partner 
                        if (bytStaffCatAndBookingProcess != 6)
                        {
                            //Add By Bluehouse
                            bytAllotmentActivityCat = 1;
                            StrComment = "Add allotment By BlueHouse";

                            // check From BOoking Process
                            if(bytStaffCatAndBookingProcess == 7)
                            {
                                bytAllotmentActivityCat = 6;
                                StrComment = "Release Allotment From Booking Process";
                            }
                        }
                        else
                        {
                            //Add By Hotel
                            bytAllotmentActivityCat = 3;
                            StrComment = "Add allotment By Hotel";
                        }
                    }
                    else
                    {

                        bytQuan = (RoomQuant - bytRoomQuantity);
                        // check Staff Cat Not Hotels Partner 
                        if (bytStaffCatAndBookingProcess != 6)
                        {
                            //Remove By Bluehouse
                            bytAllotmentActivityCat = 2;
                            StrComment = "Remove allotment By BlueHouse";

                            // check From BOoking Process
                            if(bytStaffCatAndBookingProcess == 7)
                            {
                                bytAllotmentActivityCat = 5;
                                StrComment = "Allotment Sold From BookingProcess";
                            }
                        }
                        else
                        {
                            //Remove By Hotel
                            bytAllotmentActivityCat = 4;
                            StrComment = "Remove allotment By Hotel";
                        }
                    }

                    StringBuilder query = new StringBuilder();
                    query.Append("INSERT INTO tbl_allotment_activity(allotment_id,cat_id,quantity,date_activity,comment)VALUES");
                    query.Append(" (@allotment_id, @cat_id, @quantity, @date_activity, @comment); SET @activity_id = SCOPE_IDENTITY()");
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                        cmd.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intAllotId;
                        cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytAllotmentActivityCat;
                        cmd.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = bytQuan;
                        cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                        cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = StrComment;
                        cmd.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cn.Open();
                        ExecuteNonQuery(cmd);
                        int intNewactivity = (int)cmd.Parameters["@activity_id"].Value;

                        //#Staff_Activity_Log==========================================================================================================
                        StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_allotment, StaffLogActionType.Insert, StaffLogSection.Product, this.ProductId,
                            "tbl_allotment_activity", "allotment_id,cat_id,quantity,date_activity,comment", "activity_id", intNewactivity);
                        //============================================================================================================================
                             
                    }

                }
                
                bool Update = this.UpdateAllotmentDateCuoffAndStatusByAllotId(intAllotId, bytCutoff, status);

                
            
        }
        public void InsertNewallotandUpdateBydateAllot(short shrSupplierId, int intOptionId, DateTime dDateAllot,  byte bytCutoff, byte bytStaffCat, byte bytRoomQuantity, bool bolStatus)
        {
            
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT allotment_id FROM tbl_allotment WHERE supplier_id=@supplier_id AND option_id=@option_id AND date_allotment=@date_allotment", cn);
                    cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                    cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                    cmd.Parameters.Add("@date_allotment", SqlDbType.SmallDateTime).Value = dDateAllot;
                    cn.Open();

                    IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);

                    if (reader.Read())
                    {

                        this.UpdateAllotmentAndInsertNewActivityByAllotmentId((int)reader[0], bytStaffCat, bytRoomQuantity, bytCutoff, bolStatus);
                    }
                    else
                    {
                        this.InsertAllotmentAndActivity(intOptionId, shrSupplierId, bytCutoff, dDateAllot, bolStatus, bytStaffCat, bytRoomQuantity);
                    }

                }
            

        }

        public void InsertNewallotandUpdateBydateRange(short shrSupplierId, int intOptionId, DateTime dDateStart, DateTime dDateEnd, byte bytCutoff, byte bytStaffCat, byte bytRoomQuantity, bool bolStatus)
        {
            int dateDiff = (int)dDateStart.Hotels2DateDiff(DateInterval.Day, dDateEnd);
            
            for (int count = 0; count <= dateDiff; count++)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    DateTime DateAllot = dDateStart.AddDays(count);

                    SqlCommand cmd = new SqlCommand("SELECT allotment_id FROM tbl_allotment WHERE supplier_id=@supplier_id AND option_id=@option_id AND date_allotment=@date_allotment", cn);
                    cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                    cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                    cmd.Parameters.Add("@date_allotment", SqlDbType.SmallDateTime).Value = DateAllot;
                    cn.Open();

                    IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);

                    if (reader.Read())
                    {

                        this.UpdateAllotmentAndInsertNewActivityByAllotmentId((int)reader[0], bytStaffCat, bytRoomQuantity, bytCutoff, bolStatus);
                    }
                    else
                    {
                        this.InsertAllotmentAndActivity(intOptionId, shrSupplierId, bytCutoff, DateAllot, bolStatus, bytStaffCat, bytRoomQuantity);
                    }

                }
            }

         }

        public bool UpdateAllotmentDateCuoffAndStatusByAllotId(int intAllotId , byte bytCutoff, bool status)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(bytCutoff, status);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_allotment SET date_cut_off = (date_allotment - @bytCutoff ) , status = @status WHERE allotment_id = @allotment_id ", cn);
                cmd.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intAllotId;
                cmd.Parameters.Add("@bytCutoff", SqlDbType.TinyInt).Value = bytCutoff;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_allotment, StaffLogActionType.Update, StaffLogSection.Product, this.ProductId,
                "tbl_allotment", "date_cut_off,status", arroldValue, "allotment_id", intAllotId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
            
        }

        public int InsertAllotmentAndActivity(int intOptionId, short shrSupplierId, byte bytCutoff, DateTime dDateAllotment, bool status, byte bytStaffCat, byte bytQuantity)
        {


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                DateTime dDateCutOff = dDateAllotment.AddDays(-bytCutoff);
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_allotment (supplier_id,option_id,date_allotment,date_cut_off,status)VALUES(@supplier_id,@option_id,@date_allotment,@date_cut_off,@status);SET @AllotID = SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@date_allotment", SqlDbType.SmallDateTime).Value = dDateAllotment;
                cmd.Parameters.Add("@date_cut_off", SqlDbType.SmallDateTime).Value = dDateCutOff;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;
                cmd.Parameters.Add("@AllotID", SqlDbType.Int).Direction = ParameterDirection.Output;


                cn.Open();
                ExecuteNonQuery(cmd);
                int intNewAllotId = (int)cmd.Parameters["@AllotID"].Value;
                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_allotment, StaffLogActionType.Insert, StaffLogSection.Product, this.ProductId,
                    "tbl_allotment", "supplier_id,option_id,date_allotment,date_cut_off,status", "allotment_id", intNewAllotId);
                //============================================================================================================================

                
                string strComment = string.Empty;
                byte RealActivity = 0;
                if (bytStaffCat != 6)
                {
                    strComment = "Add Allotment By BlueHouse";
                    RealActivity = 1;
                }
                else
                {
                    strComment = "Add Allotment By Hotel";
                    RealActivity = 3;
                }

                SqlCommand cmda = new SqlCommand("INSERT INTO tbl_allotment_activity (allotment_id,cat_id,quantity,date_activity,comment)VALUES(@allotment_id,@cat_id,@quantity,@date_activity,@comment); SET @activity_id = SCOPE_IDENTITY()", cn);
                cmda.Parameters.Add("@allotment_id", SqlDbType.Int).Value = intNewAllotId;
                cmda.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = RealActivity;
                cmda.Parameters.Add("@quantity", SqlDbType.TinyInt).Value = bytQuantity;
                cmda.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmda.Parameters.Add("@comment", SqlDbType.VarChar).Value = strComment;
                cmda.Parameters.Add("@activity_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                int ret = ExecuteNonQuery(cmda);
                int intNewactivity = (int)cmda.Parameters["@activity_id"].Value;


                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_allotment, StaffLogActionType.Insert, StaffLogSection.Product, this.ProductId,
                    "tbl_allotment_activity", "allotment_id,cat_id,quantity,date_activity,comment", "activity_id", intNewactivity);
                //============================================================================================================================
                
                return ret;
            }
        }


        //========= Allotment Activity
        public IDictionary<int, string> getDicActiveOptionAllotment(int intProductId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT DISTINCT(al.option_id) , opp.title FROM tbl_allotment al, tbl_product_option opp");
                query.Append(" WHERE al.option_id = opp.option_id AND  supplier_id = @supplier_id AND opp.option_id IN");
                query.Append(" (SELECT op.option_id FROM tbl_product_option op, tbl_product_option_supplier ops");
                query.Append(" WHERE op.option_id = ops.option_id AND op.cat_id IN (38,48,52,53,54,55,56) AND status = 1");
                query.Append(" AND op.product_id =  @product_id AND ops.supplier_id = @supplier_id)");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                IDictionary<int, string> dicOutPut = new Dictionary<int, string>();
                while (reader.Read())
                {
                    dicOutPut.Add((int)reader[0], reader[1].ToString());
                }

                return dicOutPut;
            }
        }

    }
}