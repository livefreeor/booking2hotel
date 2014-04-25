using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hotels2thailand.DataAccess;
/// <summary>
/// Summary description for FrontBookingAcknowledge
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class FrontBookingAcknowledge : Hotels2BaseClass
    {
        public int BookingID { get; set; }
        public int BookingProductID { get; set; }
        public String AcknowledgeID { get; set; }
        public String Fullname { get; set; }
        public byte BookingExtranetStatus { get; set; }
        public byte AcknowledgeStatus { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateConfirm { get; set; }
        public bool Status { get; set; }
        public String StaffTitle { get; set; }
        
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }

        public FrontBookingAcknowledge()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<object> GetBookingAcknowledgeByKeyword(int ProductID,byte typeSearch,string keySearch)
        {
            string sqlCondition = "";
            switch (typeSearch)
            {
                case 1:
                    //Booking
                    sqlCondition = sqlCondition + " and b.booking_id ="+keySearch;
                    break;
                case 2:
                    //AcknowID
                    sqlCondition = sqlCondition + " and ba.acknowledge_id like '%"+keySearch+"%'";
                    break;
                case 3:
                    //Guest name
                    sqlCondition = sqlCondition + " and b.name_full like '%" + keySearch + "%'";
                    break;
                case 4:
                    //Email
                    sqlCondition = sqlCondition + " and b.email like '%" + keySearch + "%'";
                    break;
            }

            
            return RenderSqlCommend(ProductID, sqlCondition);
        }

        public List<object> GetBookingAcknowledgeAll(int ProductID)
        {
            string sqlCondition = "";
            return RenderSqlCommend(ProductID, sqlCondition);      
        }

        public List<object> GetBookingAcknowledgeByDateSubmit(int ProductID, byte statusID, byte sortID, DateTime dateCheckStart, DateTime dateCheckEnd)
        {
            string sqlCondition = string.Empty;
            if (statusID != 0)
            {
                sqlCondition = sqlCondition + " and ba.status_extranet_id=" + statusID;
            }
            sqlCondition = sqlCondition + " and convert(datetime,cast(year(b.date_submit) as nvarchar)+'-'+cast(month(b.date_submit)as nvarchar)+'-'+cast(day(b.date_submit)as nvarchar)) between " + dateCheckStart.Hotels2DateToSQlString() + " and " + dateCheckEnd.Hotels2DateToSQlString();
            sqlCondition = sqlCondition + GetSqlSort(sortID);
            return RenderSqlCommend(ProductID, sqlCondition);
        }

        public List<object> GetBookingAcknowledgeByDateRecieve(int ProductID, byte statusID, byte sortID, DateTime dateRecieveStart, DateTime dateRecieveEnd)
        {
            string sqlCondition = string.Empty;
            if (statusID != 0)
            {
                sqlCondition = sqlCondition + " and ba.status_extranet_id=" + statusID;
            }
            sqlCondition = sqlCondition + " and (select top 1 sbp.date_time_check_in from tbl_booking_product sbp,tbl_product sp where sbp.product_id=sp.product_id and sbp.booking_id=b.booking_id and sp.cat_id=29) between " + dateRecieveStart.Hotels2DateToSQlString() + " and " + dateRecieveEnd.Hotels2DateToSQlString();
            sqlCondition = sqlCondition + GetSqlSort(sortID);
            return RenderSqlCommend(ProductID, sqlCondition);
        }

        public List<object> GetBookingAcknowledgeByDateAll(int ProductID, byte statusID, byte sortID, DateTime dateRecieveStart, DateTime dateRecieveEnd, DateTime dateCheckStart, DateTime dateCheckEnd)
        {
            string sqlCondition = string.Empty;
            if (statusID != 0)
            {
                sqlCondition = sqlCondition + " and ba.status_extranet_id=" + statusID;
            }
            sqlCondition = sqlCondition + " and convert(datetime,cast(year(b.date_submit) as nvarchar)+'-'+cast(month(b.date_submit)as nvarchar)+'-'+cast(day(b.date_submit)as nvarchar)) between " + dateRecieveStart.Hotels2DateToSQlString() + " and " + dateRecieveEnd.Hotels2DateToSQlString();
            sqlCondition = sqlCondition + " and (select top 1 sbp.date_time_check_in from tbl_booking_product sbp,tbl_product sp where sbp.product_id=sp.product_id and sbp.booking_id=b.booking_id and sp.cat_id=29) between " + dateCheckStart.Hotels2DateToSQlString() + " and " + dateCheckEnd.Hotels2DateToSQlString();
            sqlCondition = sqlCondition + GetSqlSort(sortID);
            return RenderSqlCommend(ProductID, sqlCondition);
        }

        public List<object> GetBookingAcknowledgeByStatus(int ProductID, byte statusID, byte sortID)
        {
            //DataConnect objConn = new DataConnect();
            string sqlCondition = string.Empty;
            if(statusID!=0)
            {
                sqlCondition=sqlCondition+" and ba.status_extranet_id=" + statusID;
            }
            sqlCondition = sqlCondition + GetSqlSort(sortID);
            return RenderSqlCommend(ProductID, sqlCondition);
            
        }

        public string GetSqlSort(byte sortID)
        {
            string result = string.Empty;
            switch (sortID)
            {
                case 1:
                    //booking id
                    result = " order by b.status_extranet_id asc,b.booking_id desc";
                    break;
                case 2:
                    //guest name
                    result = " order by b.status_extranet_id asc,b.name_full";
                    break;
                case 3:
                    //date request
                    result = " order by b.status_extranet_id asc,b.date_submit desc";
                    break;
                case 4:
                    //date check in
                    result = " order by b.status_extranet_id asc,date_time_check_in desc";
                    break;
            }
            return result;
        }
        public List<object> RenderSqlCommend(int ProductID,string sqlCondition)
        {
            string sqlCommand = "select b.booking_id,bp.booking_product_id,ba.acknowledge_id,b.name_full,b.status_extranet_id as booking_status_extranet,ba.status_extranet_id as booking_acknowledge_status,b.date_submit,ba.date_confirm,b.status,";
            sqlCommand = sqlCommand + " ISNULL((select top 1 ss.title from tbl_staff ss where ss.staff_id=ba.staff_id),'Auto') as staff_title, ";
            sqlCommand = sqlCommand + " (select top 1 sbp.date_time_check_in from tbl_booking_product sbp,tbl_product sp where sbp.product_id=sp.product_id and sbp.booking_id=b.booking_id and sp.cat_id=29) as date_time_check_in,";
            sqlCommand = sqlCommand + " (select top 1 sbp.date_time_check_out from tbl_booking_product sbp,tbl_product sp where sbp.product_id=sp.product_id and sbp.booking_id=b.booking_id and sp.cat_id=29) as date_time_check_out";
            sqlCommand = sqlCommand + " from tbl_booking_acknowledge ba,tbl_booking b,tbl_booking_product bp,tbl_product p";
            sqlCommand = sqlCommand + " where ba.booking_id=b.booking_id and b.booking_id=bp.booking_id and p.product_id=bp.product_id and p.cat_id=29 and ba.booking_id IN (";
            sqlCommand = sqlCommand + " select sb.booking_id";
            sqlCommand = sqlCommand + " from tbl_booking sb,tbl_booking_product sbp";
            sqlCommand = sqlCommand + " where sb.booking_id=sbp.booking_id and sb.status_extranet_id<>0 and sbp.product_id=" + ProductID;
            sqlCommand = sqlCommand + " )";
            sqlCommand = sqlCommand + " and ba.status_extranet_id=b.status_extranet_id";
            sqlCommand = sqlCommand + sqlCondition;
           // sqlCommand = sqlCommand + " order by b.status_extranet_id asc,b.booking_id desc";

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.End();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }
}