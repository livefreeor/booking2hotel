using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BookingSupplierShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingSupplierDisplay : Hotels2BaseClass
    {
        public short SupplierId { get; set; }
        public byte SupplierCat { get; set; }
        public string SupplierCatTitle { get; set; }
        public string PaymentTitle { get; set; }
        public string PaymentDetail { get; set; }
        public byte? PaymentPolicyDueDate { get; set; }
        public string SupTitle { get; set; }
        public string SupTitle_common { get; set; }
        public string SupTitle_company { get; set; }
        public string Address { get; set; }
        public string Address_Office { get; set; }
        public string Comment { get; set; }
        public byte TaxVAt { get; set; }
        public byte TaxService { get; set; }
        public byte TaxLocal { get; set; }
        public bool Staus { get; set; }


        //string sqlSupplierProduct = "";
        //string strSupplierProductTable = "";

        public BookingSupplierDisplay getSupplierDetailByBookingProductId(int intBookingProductId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT s.supplier_id, s.cat_id");
            query.Append(" ,(select ssc.title from tbl_supplier_category ssc where ssc.cat_id = s.cat_id) catTitle");
            query.Append(" ,(select sspt.title from tbl_supplier_payment_type sspt where sspt.payment_type_id = s.payment_type_id) paymentTypeTitle");
            query.Append(" ,(select sspt.detail from tbl_supplier_payment_type sspt where sspt.payment_type_id = s.payment_type_id) paymentTypeDetail");
            query.Append(" ,(SELECT TOP 1 spc.day_advance FROM tbl_supplier_payment_policy spc WHERE spc.supplier_id = bp.supplier_id AND bp.date_time_check_in BETWEEN spc.date_start AND spc.date_end) AS paySupDay");
            query.Append(" ,s.title,s.title_common,s.title_company,s.address,s.address_office,s.comment,s.tax_vat,s.tax_service,s.tax_local,s.status FROM tbl_supplier s , tbl_booking_product bp WHERE bp.supplier_id=s.supplier_id AND bp.booking_product_id = @booking_product_id");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingSupplierDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }
     
        
    }
}