using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand;
using System.Data;

/// <summary>
/// Summary description for BoookingReport_Payment
/// </summary>

namespace Hotels2thailand.Report
{
    public class BookingReport_Payment : Hotels2BaseClass
    {
        public DateTime Date_Submit { get; set; }
        public int AllNumber { get; set; }
        public int PaymentNumber { get; set; }
        public decimal AllMoney { get; set; }
        public decimal PaymentMoney { get; set; }

        public IList<object> GetBookingReportPayment(int intProductID, string strDateFrom, string strDateTo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder cText = new StringBuilder();
                cText.Append(" select Convert(Date,b.date_submit,103),COUNT(b.booking_id) as AllNumber ");
                cText.Append(" ,COUNT(bpm.booking_id) as PaymentNumber ");
                cText.Append(" ,ISNull(sum(bi.price),0) as AllMoney ,ISNull(SUM(bpm.amount),0) as PaymentMoney ");
                cText.Append(" from tbl_booking b ");
                cText.Append(" left outer join tbl_booking_product bp on bp.booking_id = b.booking_id and bp.status = 1 ");
                cText.Append(" left outer join tbl_booking_payment bpm on bpm.booking_id = b.booking_id and bpm.status = 1 and bpm.confirm_payment is not null ");
                cText.Append(" left outer join (select booking_id,sum(price) as price from tbl_booking_item where status=1 group by booking_id) bi on bi.booking_id = b.booking_id ");
                cText.Append(" where b.date_submit between '" + strDateFrom + " 00:00:00' and '" + strDateTo + " 23:59:59'  ");
                cText.Append(" and name_full not like '%test%' ");
                cText.Append(" and email not in ('oh_darkman@hotmail.com','peerapong@hotels2thailand.com','apichart.pu@hotels2thailand.com','visa@hotels2thailand.com','kittiwan.in@hotels2thailand.com') ");
                cText.Append(" and bi.booking_id is not null ");
                cText.Append(" and product_id = @product_id ");
                cText.Append(" group by Convert(Date,b.date_submit,103) ");
                cText.Append(" order by Convert(Date,b.date_submit,103) ");
                SqlCommand cmd = new SqlCommand(cText.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }
}