using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.ProductOption;
/// <summary>
/// Summary description for FrontAollotmentUpdate
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontAllotmentUpdate : Hotels2BaseClass
    {
        public int OptionID { get; set; }
        public short SupplierID { get; set; }
        public byte Unit { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }

        public FrontAllotmentUpdate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private List<object> GetOptionList(int BookingID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = string.Empty;
                sqlCommand = "select bi.option_id,p.supplier_price,bi.unit,bp.date_time_check_in,bp.date_time_check_out";
                sqlCommand = sqlCommand + " from tbl_booking b,tbl_booking_product bp,tbl_booking_item bi,tbl_product p,tbl_product_option po";
                sqlCommand = sqlCommand + " where b.booking_id=bp.booking_id and bp.booking_product_id=bi.booking_product_id and bp.product_id=p.product_id and bi.option_id=po.option_id";
                sqlCommand = sqlCommand + " and p.cat_id=29 and po.cat_id=38";
                sqlCommand = sqlCommand + " and b.booking_id=" + BookingID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public bool UpdateAllotment(int BookingID)
        {
            List<object> optionList = GetOptionList(BookingID);
            Allotment objAllot = new Allotment();
            bool checkAllotmentAll = true;
            foreach(FrontAllotmentUpdate item in optionList)
            {

                if (!objAllot.CheckAllotAvaliable(item.SupplierID, item.OptionID,item.Unit,item.DateCheckIn,item.DateCheckOut))
                {
                    checkAllotmentAll = false;
                    break;
                }
            }
            if (checkAllotmentAll)
            {
                        
                foreach(FrontAllotmentUpdate item in optionList)
                {
                     objAllot.UpdateAllotFromBookingProcess(item.SupplierID, item.OptionID,item.Unit,item.DateCheckIn,item.DateCheckOut);
                }
            }
            
            return checkAllotmentAll;
        }
    }
}