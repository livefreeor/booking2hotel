using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class CommissionCat : Hotels2BaseClass
    {

    }

    public class ComRevenue : Hotels2BaseClass
    {
        public int RevenueID { get; set; }
        public decimal revenueStart { get; set; }
        public decimal revenueEnd { get; set; }
        public decimal Commssion { get; set; }

        private bool _status = true;
        public bool Status {
            get { return _status; }
            set { _status = value; }
        }

        public IList<object> GetRevenueList(int RevenueId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_revenue_commission WHERE revenue_id =@revenue_id ORDER BY revenue_end ASC", cn);
                cmd.Parameters.Add("@revenue_id", SqlDbType.Int).Value = RevenueId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public decimal GetRevenuComStep(decimal PriceCompare,int RevenueId)
        {
            decimal decComret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX(commission) FROM tbl_revenue_commission WHERE CAST(@revenue AS money) BETWEEN revenue_start AND revenue_end AND revenue_id =@revenue_id ORDER BY revenue_end ASC", cn);
                cmd.Parameters.Add("@revenue_id", SqlDbType.Int).Value = RevenueId;
                cmd.Parameters.Add("@revenue", SqlDbType.Money).Value = PriceCompare;
                cn.Open();


                decComret = (decimal)ExecuteScalar(cmd);
            }

            return decComret;
        }

        public int InsertRevenue(ComRevenue revenue)
        {
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
             SqlCommand cmd = new SqlCommand("INSERT INTO tbl_revenue_commission (revenue_id,revenue_start,revenue_end,commission,status) VALUES(@revenue_id,@revenue_start,@revenue_end,@commission,@status)", cn);
             cmd.Parameters.Add("@revenue_id", SqlDbType.Int).Value = revenue.RevenueID;
             cmd.Parameters.Add("@revenue_start", SqlDbType.Money).Value = revenue.revenueStart;
             cmd.Parameters.Add("@revenue_end", SqlDbType.Money).Value = revenue.revenueEnd;
             cmd.Parameters.Add("@commission", SqlDbType.Money).Value = revenue.Commssion;
             cmd.Parameters.Add("@status", SqlDbType.Bit).Value = revenue.Status;
             cn.Open();

             return (int)ExecuteNonQuery(cmd);
         }
     }

        public bool DelAllCom(int RevenueId)
     {
         int ret = 0;
         using (SqlConnection cn = new SqlConnection(this.ConnectionString))
         {
             SqlCommand cmdDel = new SqlCommand("DELETE FROM tbl_revenue_commission WHERE revenue_id = @revenue_id", cn);
             cmdDel.Parameters.Add("@revenue_id", SqlDbType.Int).Value = RevenueId;
             cn.Open();

             ret = ExecuteNonQuery(cmdDel);
           

             return (ret == 1);
         }
     }


    }

    public enum ComActivityType : int
    {
        AddnewCommission = 1,
        UpdateCommssion = 2


    }
    public class ComActivity : Hotels2BaseClass
    {
        public int ActivityId { get; set; }
        public int ProductId { get; set; }
        public short StaffId { get; set; }
        private DateTime _date_activity = DateTime.MaxValue;
        public DateTime DateActivity
        {
            get { return _date_activity; }
            set { _date_activity = value; }
        }

        public string Detail { get; set; }


        public string StaffName {
            get 
            {
                Staff cStaff = new Staff();
                return cStaff.getStaffById(this.StaffId).Title;
            }
        }

        public ComActivity() { }

        public IList<object> GetComActivity(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_revenue_commission_activity WHERE product_id =@product_id ORDER BY activity_id DESC", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                
            }

        }

        public int InsertAutoActivity(ComActivity Comac,ComActivityType type,ProductBookingEngineCommission BookingCom, IList<ComRevenue> Comrevenue)
        {
            string Detail = string.Empty;
            string ComType = string.Empty;
            string RevenueVal = string.Empty;
            switch (BookingCom.CatId)
            {
                case 1:
                    ComType = "Flat Rate";
                    RevenueVal = "commission = " + BookingCom.Commission;
                    break;
                case 2:
                    ComType = "Monthly";
                    RevenueVal = "commission = " + BookingCom.Commission;
                    break;
                case 3:
                    ComType = "Step";
                    foreach (ComRevenue rev in Comrevenue)
                        RevenueVal = RevenueVal + "&nbsp;between" + rev.revenueStart + "&nbsp;to&nbsp;" + rev.revenueEnd + "&nbsp;commission = &nbsp;" + rev.Commssion + "&nbsp;";
                    break;
            }

            Detail = ComType + RevenueVal;

            switch (type)
            {
                case ComActivityType.AddnewCommission:
                    Detail = "add new commission [commission expired: " + BookingCom.ContractExpired.ToString("ddd, dd MMM yyyy") ;
                    Detail = Detail + ",commission type:" + ComType + "";
                    Detail = Detail + "," + RevenueVal;
                    break;
                case ComActivityType.UpdateCommssion:
                    Detail = "Update commission [commission expired: " + BookingCom.ContractExpired.ToString("ddd, dd MMM yyyy") ;
                    Detail = Detail + ",commission type:" + ComType + "";
                    Detail = Detail + "," + RevenueVal;
                    break;
                    
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_revenue_commission_activity(product_id,staff_id,date_activity,detail)VALUES(@product_id,@staff_id,@date_activity,@detail)", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = Comac.ProductId;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = Comac.StaffId;
                cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = Comac.DateActivity;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = Detail;
                cn.Open();
                return (int)ExecuteNonQuery(cmd);
            }

        }

        public int InsertActivity(ComActivity Comac)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_revenue_commission_activity(product_id,staff_id,date_activity,detail)VALUES(@product_id,@staff_id,@date_activity,@detail)", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = Comac.ProductId;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = Comac.StaffId;
                cmd.Parameters.Add("@date_activity", SqlDbType.SmallDateTime).Value = Comac.DateActivity;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = Detail;
                cn.Open();
                return (int)ExecuteNonQuery(cmd);
            }
        }
    }

    public class ProductBookingEngineCommission : Hotels2BaseClass
    {

        public int RevenueID { get; set; }
        public int ProductID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public byte CatId { get; set; }
        private string _title = string.Empty;
        public string Title {
            get
            {
                return _title;
            }
            set { _title = value; }
        }

        private decimal _com = 0;
        public decimal Commission { get { return _com; } set { _com = value; } }

        

        private DateTime _date_submit = DateTime.Now.Hotels2ThaiDateTime();

        public DateTime Date_Submit {
            get {
                return _date_submit;
            }
            set { _date_submit = value; }
        }
        public DateTime ContractExpired { get; set; }
        public bool Status { get; set; }



        public IDictionary<string, string> GetDicCom_cat()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<string,string> idic = new Dictionary<string, string>();
                SqlCommand cmd = new SqlCommand("SELECT cat_id,title FROM tbl_revenue_category",cn);
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                    idic.Add(reader[0].ToString(), reader[1].ToString());


                return idic;
            }

        }

        public ProductBookingEngineCommission GetCommission(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM tbl_revenue WHERE product_id =@product_id",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if(reader.Read())
                    return (ProductBookingEngineCommission)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public int InsertCom(ProductBookingEngineCommission ComProp)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int ret = 0;
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_revenue (product_id,cat_id,title,commission,contact_expired,date_submit)VAlUES(@product_id,@cat_id,@title,@commission,@contact_expired,@date_submit);SET @revenue_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@product_id",SqlDbType.Int).Value = ComProp.ProductID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = ComProp.CatId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = ComProp.Title;
                cmd.Parameters.Add("@contact_expired", SqlDbType.SmallDateTime).Value = ComProp.ContractExpired;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = ComProp.Date_Submit;
                cmd.Parameters.Add("@commission", SqlDbType.SmallMoney).Value = ComProp.Commission;
                cmd.Parameters.Add("@revenue_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                if(ret > 0)
                    ret = (int)cmd.Parameters["@revenue_id"].Value;
                
                    return ret;

            }
        }


        public bool UpdateCom(ProductBookingEngineCommission ComProp)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int ret = 0;
                SqlCommand cmd = new SqlCommand("UPDATE tbl_revenue SET product_id=@product_id,cat_id=@cat_id,title=@title,commission=@commission,contact_expired=@contact_expired,date_submit=@date_submit WHERE revenue_id=@revenue_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ComProp.ProductID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = ComProp.CatId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = ComProp.Title;
                cmd.Parameters.Add("@contact_expired", SqlDbType.SmallDateTime).Value = ComProp.ContractExpired;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = ComProp.Date_Submit;
                cmd.Parameters.Add("@commission", SqlDbType.SmallMoney).Value = ComProp.Commission;
                cmd.Parameters.Add("@revenue_id", SqlDbType.Int).Value = ComProp.RevenueID;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }



    }
}