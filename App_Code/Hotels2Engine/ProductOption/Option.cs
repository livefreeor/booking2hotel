using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Option
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class Option : Hotels2BaseClass
    {
        public int OptionID { get; set; }
        public short CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Title { get; set; }
       
        private  double _size;
        public double Size 
        {
            get { return _size; }
            set { _size = value; }
        }
        private byte _priority;
        public byte Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private bool _is_show;
        public bool IsShow
        {
            get { return _is_show; }
            set { _is_show = value; }
        }
        private Nullable<DateTime> _time_start;
        private Nullable<DateTime> _time_end;
        private Nullable<DateTime> _timepickup;
        private Nullable<DateTime> _timesent;

        public Nullable<DateTime> TimeStart
        {
            get { return _time_start; }
            set { _time_start = value; }
        }
        public Nullable<DateTime> TimeEnd
        {
            get { return _time_end; }
            set { _time_end = value; }
        }
        public Nullable<DateTime> TimePickUp
        {
            get { return _timepickup; }
            set { _timepickup = value; }
        }

        public Nullable<DateTime> TimeSent
        {
            get { return _timesent; }
            set { _timesent = value; }
        }
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public byte Night { get; set; }
        public DateTime? BookintDateStart { get; set; }
        public DateTime? BookingDAteEnd { get; set; }
        public DateTime? StayDateStart { get; set; }
        public DateTime? StayDateEnd { get; set; }

        public DateTime DateSubmit { get; set; }

        public Option()
        {
            _time_start = null;
            _time_end = null;
            _timepickup = null;
            _timesent = null; 
            _size = 0;
            _is_show = true;
            _status =  true;
            this.Night = 1;

            
        }

        //private LinqProductionDataContext dcOption = new LinqProductionDataContext();

        public int InsertOption(Option data)
        {
            
            //tbl_product_option option = new tbl_product_option
            //{
            //    cat_id = data.CategoryID,
            //    product_id = data.ProductID,
            //    title = data.Title,
            //    size = data.Size,
            //    IsShow =data.IsShow,
            //    time_start = data.TimeStart,
            //    time_end = data.TimeEnd,
            //    time_pickup = data.TimePickUp,
            //    time_sent = data.TimeSent,
            //    status = data.Status
                
                 
            //};
            //dcOption.tbl_product_options.InsertOnSubmit(option);
            //dcOption.SubmitChanges();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option(cat_id,product_id,title,size,IsShow,time_start,time_end,time_pickup,time_sent,status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit)VALUES(@cat_id,@product_id,@title,@size,@IsShow,@time_start,@time_end,@time_pickup,@time_sent,@status,@night,@booking_period_start,@booking_period_end,@stay_period_start,@stay_period_end,@date_submit); SET @option_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = data.CategoryID;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = data.Title;
                cmd.Parameters.Add("@size", SqlDbType.Float).Value = data.Size;
                cmd.Parameters.Add("@IsShow", SqlDbType.Bit).Value = data.IsShow;

                if (data.TimeStart.HasValue)
                    cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = data.TimeStart;
                else
                    cmd.Parameters.AddWithValue("@time_start", DBNull.Value);

                if (data.TimeEnd.HasValue)
                    cmd.Parameters.Add("@time_end", SqlDbType.SmallDateTime).Value = data.TimeEnd;
                else
                    cmd.Parameters.AddWithValue("@time_end", DBNull.Value);

                if (data.TimePickUp.HasValue)
                    cmd.Parameters.Add("@time_pickup", SqlDbType.SmallDateTime).Value = data.TimePickUp;
                else
                    cmd.Parameters.AddWithValue("@time_pickup", DBNull.Value);

                if (data.TimeSent.HasValue)
                    cmd.Parameters.Add("@time_sent", SqlDbType.SmallDateTime).Value = data.TimeSent;
                else
                    cmd.Parameters.AddWithValue("@time_sent", DBNull.Value);


                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = data.Status;
                cmd.Parameters.Add("@night", SqlDbType.TinyInt).Value = data.Night;

                if (data.BookintDateStart.HasValue)
                    cmd.Parameters.Add("@booking_period_start", SqlDbType.SmallDateTime).Value = data.BookintDateStart;
                else
                    cmd.Parameters.AddWithValue("@booking_period_start", DBNull.Value);

                if (data.BookingDAteEnd.HasValue)
                    cmd.Parameters.Add("@booking_period_end", SqlDbType.SmallDateTime).Value = data.BookingDAteEnd;
                else
                    cmd.Parameters.AddWithValue("@booking_period_end", DBNull.Value);

                if (data.StayDateStart.HasValue)
                    cmd.Parameters.Add("@stay_period_start", SqlDbType.SmallDateTime).Value = data.StayDateStart;
                else
                    cmd.Parameters.AddWithValue("@stay_period_start", DBNull.Value);

                if (data.StayDateEnd.HasValue)
                    cmd.Parameters.Add("@stay_period_end", SqlDbType.SmallDateTime).Value = data.StayDateEnd;
                else
                    cmd.Parameters.AddWithValue("@stay_period_end", DBNull.Value);


                cmd.Parameters.Add("@date_submit",SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Direction = ParameterDirection.Output; 
                cn.Open();
                ExecuteNonQuery(cmd);
                 ret = (int)cmd.Parameters["@option_id"].Value;
            }
            
            
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                data.ProductID, "tbl_product_option", "cat_id,product_id,title,size,IsShow,time_start,time_end,time_pickup,time_sent,status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit", "option_id", ret);
            //========================================================================================================================================================
            return ret;
        }


        public bool UpdatePriority(int intOptionId, byte bytPri)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET priority=@priority WHERE option_id=@option_id",cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPri;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }

            return (ret == 1);
        }

        public bool UpdateStatus(int intOptionId)
        {
            int ret = 0;
            bool Status = false;
            int intProductId = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "status", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                SqlCommand cmd1 = new SqlCommand("SELECT status,product_id FROM tbl_product_option WHERE option_id=@option_id", cn);
                cmd1.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                IDataReader reader = ExecuteReader(cmd1, CommandBehavior.SingleRow);
                if(reader.Read())
                {
                    bool bolStatusCurrent = (bool)reader[0];
                    intProductId = (int)reader[1];
                    if (!bolStatusCurrent)
                    Status = true;
                }

                reader.Close();
                     
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET status=@status WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                

                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option", "status", arroldValue, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        public bool UpdateOption(Option data)
        {
            int ret = 0;
            //tbl_product_option RsProductOption = dcOption.tbl_product_options.SingleOrDefault(o => o.option_id == data.OptionID);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "cat_id,product_id,title,size,IsShow,time_start,time_end,time_pickup,time_sent,status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end", "option_id", data.OptionID); ;
            //============================================================================================================================
            

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET cat_id=@cat_id,title =@title,size=@size,IsShow=@IsShow,time_start=@time_start,time_end=@time_end,time_pickup=@time_pickup,time_sent=@time_sent,status=@status,night=@night,booking_period_start=@booking_period_start,booking_period_end=@booking_period_end,stay_period_start=@stay_period_start,stay_period_end=@stay_period_end WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = data.CategoryID;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = data.Title;
                cmd.Parameters.Add("@size", SqlDbType.Float).Value = data.Size;
                cmd.Parameters.Add("@IsShow", SqlDbType.Bit).Value = data.IsShow;

                if (data.TimeStart.HasValue)
                    cmd.Parameters.Add("@time_start", SqlDbType.SmallDateTime).Value = data.TimeStart;
                else
                    cmd.Parameters.AddWithValue("@time_start", DBNull.Value);

                if (data.TimeEnd.HasValue)
                    cmd.Parameters.Add("@time_end", SqlDbType.SmallDateTime).Value = data.TimeEnd;
                else
                    cmd.Parameters.AddWithValue("@time_end", DBNull.Value);

                if (data.TimePickUp.HasValue)
                    cmd.Parameters.Add("@time_pickup", SqlDbType.SmallDateTime).Value = data.TimePickUp;
                else
                    cmd.Parameters.AddWithValue("@time_pickup", DBNull.Value);

                if (data.TimeSent.HasValue)
                    cmd.Parameters.Add("@time_sent", SqlDbType.SmallDateTime).Value = data.TimeSent;
                else
                    cmd.Parameters.AddWithValue("@time_sent", DBNull.Value);


                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = data.Status;
                cmd.Parameters.Add("@night", SqlDbType.TinyInt).Value = data.Night;

                if (data.BookintDateStart.HasValue)
                    cmd.Parameters.Add("@booking_period_start", SqlDbType.SmallDateTime).Value = data.BookintDateStart;
                else
                    cmd.Parameters.AddWithValue("@booking_period_start", DBNull.Value);

                if (data.BookingDAteEnd.HasValue)
                    cmd.Parameters.Add("@booking_period_end", SqlDbType.SmallDateTime).Value = data.BookingDAteEnd;
                else
                    cmd.Parameters.AddWithValue("@booking_period_end", DBNull.Value);

                if (data.StayDateStart.HasValue)
                    cmd.Parameters.Add("@stay_period_start", SqlDbType.SmallDateTime).Value = data.StayDateStart;
                else
                    cmd.Parameters.AddWithValue("@stay_period_start", DBNull.Value);

                if (data.StayDateEnd.HasValue)
                    cmd.Parameters.Add("@stay_period_end", SqlDbType.SmallDateTime).Value = data.StayDateEnd;
                else
                    cmd.Parameters.AddWithValue("@stay_period_end", DBNull.Value);

                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = data.OptionID;
                
                cn.Open();
                ret =ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.NULL,
                null, "tbl_product_option", "cat_id,product_id,title,size,IsShow,time_start,time_end,time_pickup,time_sent,status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end", arroldValue, "option_id", data.OptionID);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }


        public bool Update()
        {
            return Option.UpdateProductOption(this.OptionID, this.CategoryID, this.Title, this.Size, this.Priority, this.IsShow, this.TimeStart, this.TimeEnd, this.TimePickUp, this.TimeSent, this.Status, this.Night, this.BookintDateStart, this.BookingDAteEnd, this.StayDateStart, this.StayDateEnd);
        }

        public bool UpdateOptionExtranet(int intProductId, int intOptionId, string strTitle)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "title", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET title=@title WHERE option_id=@option_id",cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_option", "title", arroldValue, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool UpdateOptionExtranet_package(int intProductId, int intOptionId, string strTitle,byte bytNight, DateTime dBookingDateStart, DateTime dBookingDateEnd, DateTime dStayDateStart, DateTime dStayDateEnd)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option", "title,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end", "option_id", intOptionId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option SET title=@title,night=@night,booking_period_start=@booking_period_start,booking_period_end=@booking_period_end,stay_period_start=@stay_period_start,stay_period_end=@stay_period_end  WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@night", SqlDbType.TinyInt).Value = bytNight;

                cmd.Parameters.Add("@booking_period_start", SqlDbType.SmallDateTime).Value = dBookingDateStart;

                cmd.Parameters.Add("@booking_period_end", SqlDbType.SmallDateTime).Value = dBookingDateEnd;

                cmd.Parameters.Add("@stay_period_start", SqlDbType.SmallDateTime).Value = dStayDateStart;

                cmd.Parameters.Add("@stay_period_end", SqlDbType.SmallDateTime).Value = dStayDateEnd;


                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_option", "title,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end", arroldValue, "option_id", intOptionId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public static bool UpdateProductOption(int intOptionId, short shrCatId, string strTitle, double douSize, byte bytPriority, bool bolIsshow,
             Nullable<DateTime> datTimeStart, Nullable<DateTime> datTimeEnd, Nullable<DateTime> datTimePick, Nullable<DateTime> datTimeSent, bool bolStatus, byte bytNight, DateTime? dBookingStart, DateTime? dBookingEnd, DateTime? dStayStart, DateTime? dStayEnd)
        {
            Option cUpdate = new Option
            {
                OptionID = intOptionId,
                CategoryID = shrCatId,
                Title = strTitle,
                Size = douSize,
                Priority = bytPriority,
                IsShow = bolIsshow,
                TimeStart = datTimeStart,
                TimeEnd = datTimeEnd,
                TimePickUp = datTimePick,
                TimeSent = datTimeSent,
                Status = bolStatus,
                 Night = bytNight,
                BookintDateStart = dBookingStart,
                BookingDAteEnd = dBookingEnd,
                 StayDateStart = dStayStart,
                  StayDateEnd = dStayEnd
            };
            return cUpdate.UpdateOption(cUpdate);
        }

        public List<object> GetProductOptiontAll_Supplier(int intProductId, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option op, tbl_product_option_supplier ops WHERe op.product_id = @product_id AND op.status = 1 AND op.option_id = ops.option_id ANd ops.supplier_id = @supplier_id",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        

        public Option GetProductOptionById(int intOptionId)
        {
            //Option clOption = new Option();
            //var result = dcOption.tbl_product_options.SingleOrDefault(po => po.option_id == intOptionId);
            //return (Option)MappingObjectFromDataContext(result);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE option_id = @option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Option)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public Option GetProductOptionTop1_Extrnet(int intProductId, short shrOptionCat, short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOp 1 * FROM tbl_product_option op, tbl_product_option_supplier ops WHERE op.product_id =@product_id AND op.cat_id=@cat_id AND op.status = 1 AND op.option_id = ops.option_id  AND ops.supplier_id = @supplier_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = shrOptionCat;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Option)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }
        public List<object> GetProductOptionByCatIdAndProductId(int intProductId, short shrOptionCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE product_id =@product_id AND cat_id=@cat_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = shrOptionCat;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductOptionByCatId(int intProductId, short intCatId)
        {
            //var result = dcOption.tbl_product_options.SingleOrDefault(o => o.product_id == intProductId && o.cat_id == intCatId);
            //var result = from item in dcOption.tbl_product_options
            //             where item.product_id == intProductId && item.cat_id == intCatId
            //             orderby item.status descending,item.priority, item.title
            //             select item;
                         
            //return MappingObjectFromDataContextCollection(result);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE product_id=@product_id AND cat_id =@cat_id ORDER BY status DESC , priority, title",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = intCatId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> GetProductOptionByCatIdSpecifyCat(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT  supplier_price FROM tbl_product WHERE product_id=@product_id",cn);
                cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                int SupplierId = (short)ExecuteScalar(cmd1);

                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option op, tbl_product_option_supplier ops  WHERE op.product_id=@product_id AND op.cat_id IN (38, 48, 52, 53, 54, 55, 56) AND op.status = 1 AND op.option_id = ops.option_id AND ops.supplier_id = @supplier_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = SupplierId;
                

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
          
        }

        public List<object> GetProductOptionByCurrentSupplierANDProductIdANDCATID(short shrSupplierId, int intProductId, short shrCatId)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit FROM tbl_product_option op , tbl_product_option_supplier ops WHERE op.option_id = ops.option_id AND ops.supplier_id =@supplier_id AND op.product_id = @product_id AND op.cat_id=@cat_id ORDER BY op.status DESC , op.priority, op.title", cn);
                cn.Open();
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = shrCatId;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            //return MappingObjectFromDataContextCollection(PrimaryResult);
        }

        public List<object> GetProductOptionByCurrentSupplierANDProductIdANDCATID_OpenOnly(short shrSupplierId, int intProductId, short shrCatId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit FROM tbl_product_option op , tbl_product_option_supplier ops WHERE op.option_id = ops.option_id AND ops.supplier_id =@supplier_id AND op.product_id = @product_id AND op.cat_id=@cat_id AND op.status = 1 ORDER BY op.status DESC , op.priority, op.title", cn);
                cn.Open();
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = shrCatId;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            //return MappingObjectFromDataContextCollection(PrimaryResult);
        }

        public List<object> GetProductOptionByProductId_RoomOnly(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE product_id=@product_id AND cat_id=38", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return  MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductOptionByProductId_RoomOnlyExtranet(int intProductId, short shrSupplierId)
        {
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status");
            Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
            Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND op.status=1 AND op.cat_id IN (38) ORDER BY op.priority ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductOptionByProductId_PackageOnlyExtranet(int intProductId, short shrSupplierId, bool bolStatus)
        {
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit");
            Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
            Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND op.status=@status AND op.cat_id IN (57) ORDER BY op.priority ");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductOptionByProductId_PackageOnlyExtranet_Expired(int intProductId, short shrSupplierId, bool bolStatus, byte bytMonthExpired)
        {
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status,night,booking_period_start,booking_period_end,stay_period_start,stay_period_end,date_submit");
            Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
            Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND booking_period_end IS NOT NULL AND op.status=@status AND DATEDIFF(month,DATEADD(hh,14,getdate()),  booking_period_end) <= @expired AND op.cat_id IN (57) ORDER BY op.priority ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@expired", SqlDbType.TinyInt).Value = bytMonthExpired;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductOptionByProductId_Extranet_ByCat(int intProductId, short shrSupplierId, byte bytCatId, bool bolStatus)
        {
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status");
            Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
            Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND op.status=@status AND op.cat_id =@cat_id ORDER BY op.priority ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = bytCatId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }



        public List<object> GetProductOptionByCurrentSupplierANDProductId_All_Except_Gala(short shrSupplierId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status");
                Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
                Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND op.status=1 AND op.cat_id NOT IN (47)");

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }


        }

        // Get from Two Table , First from tbl_product_option_supplier(Mapping)
        public List<object> GetProductOptionByCurrentSupplierANDProductId(short shrSupplierId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT op.option_id,op.cat_id,op.product_id,op.title,op.size,op.priority,op.IsShow,op.time_start,op.time_end,op.time_pickup,op.time_sent,op.status");
                Query.Append(" FROM tbl_product_option op, tbl_product_option_supplier os");
                Query.Append(" WHERE op.option_id = os.option_id AND os.supplier_id=@supplier_id AND op.product_id=@product_id AND op.status=1 AND op.cat_id IN (38, 48, 52, 53, 54, 55, 56) ORDER BY op.title");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            
        }

        public Dictionary<int, string> getOptionListByOptionArray(string OptionArr)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT option_id, title FROM tbl_product_option WHERE");
                Query.Append(" option_id IN (" + OptionArr + ")");

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cn.Open();
                Dictionary<int, string> dicSupplier = new Dictionary<int, string>();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicSupplier.Add((int)reader[0], reader[1].ToString());
                }

                return dicSupplier;
            }
        }

        

        public static Dictionary<short, string> getSupplierListByOption(int intoptionId)
        {
            //LinqProductionDataContext dcOption = new LinqProductionDataContext();
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //var Result= from op in dcOption.tbl_product_option_suppliers
            //            where op.option_id == intoptionId
            //            select op.supplier_id;
            //Dictionary<short, string> dicSupplier = new Dictionary<short, string>();
            //foreach(var item in Result)
            //{
            //    Suppliers.Supplier cSupplier = new Suppliers.Supplier();
            //    cSupplier = cSupplier.getSupplierById(item);

            //    dicSupplier.Add(cSupplier.SupplierId, cSupplier.SupplierTitle);

            //}
            Dictionary<short, string> dicSupplier = new Dictionary<short, string>();
            Option cOption = new Option();
            using (SqlConnection cn = new SqlConnection(cOption.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ops.supplier_id,sup.title FROM tbl_product_option_supplier ops, tbl_supplier sup WHERE ops.supplier_id = sup.supplier_id AND ops.option_id=@option_id",cn);
                cmd.Parameters.Add("@option_id",SqlDbType.Int).Value = intoptionId;
                cn.Open();

                IDataReader reader = cOption.ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicSupplier.Add((short)reader[0], reader[1].ToString());
                }
            }

            return dicSupplier;

        }

        /// <summary>
        /// Get Supplier Not Current 
        /// for Option Duplicate
        /// </summary>
        /// <param name="intProductId"></param>
        /// <param name="shrSupplierId"></param>
        /// <returns></returns>
        public  Dictionary<short, string> getSupplierListNotCurrent(int intProductId , short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT ps.supplier_id, s.title  FROM tbl_product_supplier ps, tbl_supplier s WHERE");
                Query.Append(" ps.supplier_id = s.supplier_id AND ps.product_id = @product_id AND ps.status = 1 AND ps.supplier_id <> @supplier_id");
                

                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                Dictionary<short, string> dicSupplier = new Dictionary<short, string>();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicSupplier.Add((short)reader[0], reader[1].ToString());
                }

                return dicSupplier;
            }
        }
          
 

	  public Dictionary<int, string> getOPtionListNotCurrent(int intProductId, short shrSupplierId, short shrSupplierCurrentId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT op.option_id, op.title  FROM tbl_product_option_supplier ops, tbl_product_option op");
                Query.Append(" WHERE op.option_id = ops.option_id AND op.product_id = @product_id AND op.cat_id <> 47 AND op.status = 1 AND ops.supplier_id = @supplier_id AND op.option_id NOT IN");
                Query.Append(" (SELECT opa.option_id  FROM tbl_product_option_supplier opss, tbl_product_option opa");
                Query.Append(" WHERE opa.option_id = opss.option_id AND opa.product_id = @product_id AND opa.cat_id <> 47 AND opa.status = 1 AND opss.supplier_id IN");
                Query.Append(" (SELECT supplier_id FROM tbl_product_supplier WHERE product_id = @product_id AND status = 1 AND supplier_id = @supplier_current_id ))");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@supplier_current_id", SqlDbType.SmallInt).Value = shrSupplierCurrentId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                Dictionary<int, string> dicSupplier = new Dictionary<int, string>();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicSupplier.Add((int)reader[0], reader[1].ToString());
                }

                return dicSupplier;
            }
        }

      public int insertOptionMappingSupplier_ExtraNet(int intProductId, int intOptionId, short shrSupplierId)
      {
          int ret = 0;
          using (SqlConnection cn = new SqlConnection(this.ConnectionString))
          {
              SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_supplier(option_id,supplier_id) VALUES(@option_id,@supplier_id)", cn);
              cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
              cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
              cn.Open();
              ret = ExecuteNonQuery(cmd);
          }

          //=== STAFF ACTIVITY =====================================================================================================================================
          StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
              intProductId, "tbl_product_option_supplier", "option_id,supplier_id", "option_id,supplier_id", intOptionId, shrSupplierId);
          //========================================================================================================================================================
          return ret;
      }

        public static int insertOptionMappingSupplier(int OptionId, short shrSupplierId)
        {
            //LinqProductionDataContext dcOption = new LinqProductionDataContext();
            //var Result = dcOption.ExecuteCommand("INSERT INTO tbl_product_option_supplier(option_id,supplier_id) VALUES ({0},{1})", OptionId, shrSupplierId);
            //int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            ////=== STAFF ACTIVITY =====================================================================================================================================
            //StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
            //    ProductId, "tbl_product_option_supplier", "option_id,supplier_id", "option_id,supplier_id", OptionId, shrSupplierId);
            ////========================================================================================================================================================
            //return (int)Result;
            
            Option cOPtion = new Option();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cOPtion.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_supplier (option_id,supplier_id) VALUES(@option_id,@supplier_id)", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = OptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                ret = cOPtion.ExecuteNonQuery(cmd);
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_supplier", "option_id,supplier_id", "option_id,supplier_id", OptionId, shrSupplierId);
            //========================================================================================================================================================

            return ret;
        }
        public static int insertOptionMappingSupplierExtra(int intProductId, int OptionId, short shrSupplierId)
        {
            //LinqProductionDataContext dcOption = new LinqProductionDataContext();
            //var Result = dcOption.ExecuteCommand("INSERT INTO tbl_product_option_supplier(option_id,supplier_id) VALUES ({0},{1})", OptionId, shrSupplierId);
            
            ////=== STAFF ACTIVITY =====================================================================================================================================
            //StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
            //    intProductId, "tbl_product_option_supplier", "option_id,supplier_id", "option_id,supplier_id", OptionId, shrSupplierId);
            ////========================================================================================================================================================
            //return (int)Result;

            Option cOPtion = new Option();
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cOPtion.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_supplier (option_id,supplier_id) VALUES(@option_id,@supplier_id)", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = OptionId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                ret = cOPtion.ExecuteNonQuery(cmd);
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_supplier, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_supplier", "option_id,supplier_id", "option_id,supplier_id", OptionId, shrSupplierId);
            //========================================================================================================================================================
            return ret;
        }
        public List<object> GetProductOptionByCatIdandNotCurrentOption(int intProductId, short intCatId, int intOptionId)
        {
            //var result = dcOption.tbl_product_options.SingleOrDefault(o => o.product_id == intProductId && o.cat_id == intCatId);
            //var result = from item in dcOption.tbl_product_options
            //             where item.product_id == intProductId && item.cat_id == intCatId && item.option_id != intOptionId
            //             orderby item.status descending, item.priority, item.title
            //             select item;

            //return MappingObjectFromDataContextCollection(result);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE product_id =@product_id AND cat_id=@cat_id AND option_id <> @option_id ORDER BY status DESC , priority , title", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = intCatId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        
        public Option getOptionById(int intOptionId)
        {
            //Option clOption = new Option();
            //var result = dcOption.tbl_product_options.SingleOrDefault(po => po.option_id == intOptionId);
            //return (Option)MappingObjectFromDataContext(result);

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option WHERE option_id = @option_id",cn);
                cmd.Parameters.Add("@option_id",SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Option)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public  static short getSupplierId(int intOptionId)
        {
            //LinqProductionDataContext dcOption = new LinqProductionDataContext();
            //var ReSult = dcOption.tbl_product_option_suppliers.SingleOrDefault(po => po.option_id == intOptionId);
            //if (ReSult == null)
            //    return 0;
            //else
            //{
            //    return ReSult.supplier_id;
            //}
            Option cOption = new Option();
            using (SqlConnection cn = new SqlConnection(cOption.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 supplier_id FROM tbl_product_option_supplier WHERE option_id=@option_id ",cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                IDataReader reader = cOption.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return 0;
                else
                    return (short)reader[0];
            }

        }

        
    }
}