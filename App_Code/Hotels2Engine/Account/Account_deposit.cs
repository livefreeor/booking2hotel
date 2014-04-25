using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Staffs;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Account_deposit
/// </summary>
/// 
namespace Hotels2thailand.Account
{

    public class Deposit_repay : Hotels2BaseClass
    {
        public int PaymentId { get; set; }
        public int DepositID { get; set; }
        public DateTime? ComfirmDeposit { get; set; }
        public decimal Amount { get; set; }

        private Account_deposit _classDep = null;
        public Account_deposit ClassDeposit
        {
            get
            {
                if (_classDep == null)
                {
                    Account_deposit cDep = new Account_deposit();
                    _classDep = cDep.getDepositByID(this.DepositID);
                }
                return _classDep;
            }
        }
        
        public Deposit_repay()
        {
        }

        public IList<object> getDepRepayListByPaymentID(int PaymentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account_deposit_repay WHERE payment_id = @payment_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = PaymentID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getDepRepayListByDepID(int intDepositID)
        {
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account_deposit_repay WHERE deposit_id = @deposit_id AND Confirm_deposit IS NOT NULL", cn);
                cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Value = intDepositID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertDepositRepay(Deposit_repay cDep)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_account_deposit_repay (payment_id,deposit_id,amount) VALUES (@payment_id,@deposit_id,@amount) ",cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = cDep.PaymentId ;
                cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Value = cDep.DepositID;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = cDep.Amount;
                cn.Open();

                return ExecuteNonQuery(cmd);
            }
        }

        public bool MakeDepositUsed(int intPaymentid , DateTime dDateConfirm)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_deposit_repay SET confirm_deposit=@confirm_deposit WHERE payment_id=@payment_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentid;
                //cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Value = cDep.DepositID;
                cmd.Parameters.Add("@confirm_deposit", SqlDbType.SmallDateTime).Value = dDateConfirm;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }


        
    }

    public class Deposit_HotelList : Hotels2BaseClass
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public decimal DepositBalance { get; set; }

        public Deposit_HotelList() { }

        public IList<object> GetDepositHotel()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                //SqlCommand cmd = new SqlCommand("SELECT p.product_id, p.product_code, p.title, (SELECT SUM(adp.amount) FROM tbl_account_deposit adp WHERE p.product_id = adp.product_id) AS Deposit_balance FROM tbl_product p WHERE (ISNULL((SELECT SUM(adp.amount) FROM tbl_account_deposit adp WHERE p.product_id = adp.product_id) ,0) - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp , tbl_account_deposit adps WHERE dp.deposit_id = adps.deposit_id AND adps.product_id = p.product_id AND dp.confirm_deposit IS NOT NULL),0)) > 0", cn);
                SqlCommand cmd = new SqlCommand("SELECT p.product_id, p.product_code, p.title,(ISNULL((SELECT SUM(adp.amount) FROM tbl_account_deposit adp WHERE p.product_id = adp.product_id AND adp.status = 1) ,0) - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp , tbl_account_deposit adps WHERE dp.deposit_id = adps.deposit_id AND adps.status  = 1 AND adps.product_id = p.product_id AND dp.confirm_deposit IS NOT NULL),0)) AS Deposit_balance FROM tbl_product p ", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }

    public class Account_deposit:Hotels2BaseClass
    {
        public int DepositID { get; set; }
        public int? BookingID { get; set; }
        public int ProductID { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckout { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateSubmit { get; set; }
        public decimal AmountBooking { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public short StaffId { get; set; }
        public string StaffHotel { get; set; }

        public string StaffName { get; set; }
        public decimal DepositUsed { get; set; }

       


        private Production.Product _cproduct = null;
        public Production.Product cProduct
        {
            get
            {
                if (_cproduct == null)
                {
                    Production.Product cProducts = new Production.Product();
                    _cproduct = cProducts.GetProductById((int)this.ProductID);

                }
                return _cproduct;
            }
        }


        private IList<object> _irepay = null;
        public IList<object> IlistDepRepay
        {
            get
            {
                if (_irepay == null)
                {
                    Deposit_repay repay = new Deposit_repay();
                    _irepay = repay.getDepRepayListByDepID(this.DepositID);
                }

                return _irepay;
            }
        }

        private BookingdetailDisplay _classBookingDetail = null;
        public BookingdetailDisplay ClassBookingDetail
        {
            get
            {
                if (_classBookingDetail == null)
                {
                    BookingdetailDisplay cBooking = new BookingdetailDisplay();
                    _classBookingDetail = cBooking.GetBookingDetailListByBookingId((int)this.BookingID);
                }
                return _classBookingDetail;
            }

        }

        public Account_deposit()
        {
            //
            // TODO: Add constructor logic here
            //        
        }
        public Account_deposit getDepositByID(int intDepID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT dep.deposit_id,dep.booking_id,dep.product_id,dep.datetime_check_in,dep.datetime_check_out,dep.customer_name,dep.date_submit,dep.amount_booking,dep.amount,dep.status,dep.comment,dep.staff_id,dep.staff_hotel , st.title,(dep.amount - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp WHERE dp.deposit_id = dep.deposit_id AND dp.confirm_deposit IS NOT NULL  ),0)) AS dep_used FROM tbl_account_deposit dep , tbl_staff st WHERE dep.status = 1 AND dep.staff_id = st.staff_id AND (dep.amount - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp WHERE dp.deposit_id = dep.deposit_id AND dp.confirm_deposit IS NOT NULL  ),0)) >= 0 AND dep.deposit_id = @deposit_id", cn);
                cn.Open();
                cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Value = intDepID;
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Account_deposit)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }

        }

        public IList<object> getDepositByProductID_available(int intProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT dep.deposit_id,dep.booking_id,dep.product_id,dep.datetime_check_in,dep.datetime_check_out,dep.customer_name,dep.date_submit,dep.amount_booking,dep.amount,dep.status,dep.comment,dep.staff_id,dep.staff_hotel , st.title,(dep.amount - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp WHERE dp.deposit_id = dep.deposit_id AND dp.confirm_deposit IS NOT NULL  ),0)) AS dep_used FROM tbl_account_deposit dep , tbl_staff st WHERE dep.status = 1 AND dep.staff_id = st.staff_id AND (dep.amount - ISNULL((SELECT SUM(dp.amount) FROM tbl_account_deposit_repay dp WHERE dp.deposit_id = dep.deposit_id AND dp.confirm_deposit IS NOT NULL  ),0)) > 0 AND dep.product_id = @product_id", cn);
                cn.Open();
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }


        public int inserDeposit(Account_deposit cDep)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_account_deposit (booking_id,product_id,datetime_check_in,datetime_check_out,customer_name,date_submit,amount_booking,amount,comment,staff_id,staff_hotel) VALUES(@booking_id,@product_id,@datetime_check_in,@datetime_check_out,@customer_name,@date_submit,@amount_booking,@amount,@comment,@staff_id,@staff_hotel) ; SET @deposit_id = SCOPE_IDENTITY();", cn);

                if (cDep.BookingID.HasValue)
                    cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = cDep.BookingID;
                else
                    cmd.Parameters.AddWithValue("@booking_id", DBNull.Value);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = cDep.ProductID;

                if (cDep.DateCheckIn.HasValue)
                    cmd.Parameters.Add("@datetime_check_in", SqlDbType.SmallDateTime).Value = cDep.DateCheckIn;
                else
                    cmd.Parameters.AddWithValue("@datetime_check_in", DBNull.Value);
                if (cDep.DateCheckIn.HasValue)
                    cmd.Parameters.Add("@datetime_check_out", SqlDbType.SmallDateTime).Value = cDep.DateCheckout;
                else
                    cmd.Parameters.AddWithValue("@datetime_check_out", DBNull.Value);

                cmd.Parameters.Add("@customer_name", SqlDbType.NVarChar).Value = cDep.CustomerName;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = cDep.DateSubmit;
                cmd.Parameters.Add("@amount_booking", SqlDbType.Money).Value = cDep.AmountBooking;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = cDep.Amount;
                
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = cDep.Comment;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = cDep.StaffId;
                cmd.Parameters.Add("@staff_hotel", SqlDbType.NVarChar).Value = cDep.StaffHotel;

                cmd.Parameters.Add("@deposit_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();

                if (ExecuteNonQuery(cmd) == 1)
                    ret = (int)cmd.Parameters["@deposit_id"].Value;
               
                return ret;
            }
        }


    }
}