using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Booking;
using Hotels2thailand.Suppliers;
/// <summary>
/// Summary description for Account_payment
/// </summary>
/// 
namespace Hotels2thailand.Account
{

   
    public class Account_bht_bank_payment : Hotels2BaseClass
    {
        public byte AccountID { get; set; }
        public int PaymentID { get; set; }

        private decimal _fee = 0;
        public decimal Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        public DateTime? ComfrimBankPayment { get; set; }

        public Account_bht_bank_payment()
        {

        }

        public int InsertBankPayment(Account_bht_bank_payment cBankPayment)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_account_bht_bank_payment (account_id,payment_id,fee,confirm_bank_payment) VALUES(@account_id,@payment_id,@fee,@confirm_bank_payment)", cn);

                cmd.Parameters.Add("@account_id", SqlDbType.TinyInt).Value = cBankPayment.AccountID;
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = cBankPayment.PaymentID;
                cmd.Parameters.Add("@fee", SqlDbType.Int).Value = cBankPayment.Fee;

                if (cBankPayment.ComfrimBankPayment.HasValue)
                    cmd.Parameters.Add("@confirm_bank_payment", SqlDbType.SmallDateTime).Value = cBankPayment.ComfrimBankPayment;
                else
                    cmd.Parameters.AddWithValue("@confirm_bank_payment", DBNull.Value);

                cn.Open();

                return ExecuteNonQuery(cmd);
            }
        }

        public bool MakebhtPaymentCompleted(byte bytAccountId, int intPaymentId, decimal decFee, DateTime dDateComfirm)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_bht_bank_payment SET fee =@fee, confirm_bank_payment=@confirm_bank_payment WHERE account_id=@account_id AND payment_id=@payment_id", cn);
                cmd.Parameters.Add("@fee", SqlDbType.SmallMoney).Value = decFee;
                cmd.Parameters.Add("@account_id", SqlDbType.TinyInt).Value = bytAccountId;
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentId;
                cmd.Parameters.Add("@confirm_bank_payment", SqlDbType.SmallDateTime).Value = dDateComfirm;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }

    }


    public class Account_payment_Booking : Hotels2BaseClass
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public byte KindID { get; set; }
        public byte ComCat { get; set; }
        public decimal ComVal { get; set; }

       
        public decimal PriceAmount { get; set; }

        public decimal? CostAmount { get; set; }
        public decimal? ComAmount { get; set; }
        
        public bool Status { get; set; }

        private BookingdetailDisplay _classBookingDetail = null;
        public BookingdetailDisplay ClassBookingDetail
        {
            get {
                if (_classBookingDetail == null)
                {
                    BookingdetailDisplay cBooking = new BookingdetailDisplay();
                    _classBookingDetail = cBooking.GetBookingDetailListByBookingId(this.BookingID);
                }
                return _classBookingDetail;
            }

        }

       
        public Account_payment_Booking()
        {

        }

        public IList<object> getPaymentBookingList(int intPaymentID)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account_hotel_payment_booking WHERE payment_id =@payment_id AND status = 1", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public bool DisableBookingPayment(int intPaymentID , int intBookingId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_hotel_payment_booking SET status = 0 WHERE payment_id =@payment_id AND booking_id=@booking_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }
        public bool DisableBookingPayment(int intPaymentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_hotel_payment_booking SET status = 0 WHERE payment_id =@payment_id ", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
               
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }


        public int InsertAccountPaymentBooking(Account_payment_Booking cPaymentBooking)
        {
            
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_account_hotel_payment_booking (payment_id,booking_id,kind_id,cat_id,compercentandVal,price_amount,cost_amount,com_amount) VALUES(@payment_id,@booking_id,@kind_id,@cat_id,@compercentandVal,@price_amount,@cost_amount,@com_amount)", cn);
                cmd.Parameters.Add("@payment_id",SqlDbType.Int).Value = cPaymentBooking.PaymentID;
                cmd.Parameters.Add("@booking_id",SqlDbType.Int).Value = cPaymentBooking.BookingID;
                cmd.Parameters.Add("@kind_id",SqlDbType.TinyInt).Value = cPaymentBooking.KindID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = cPaymentBooking.ComCat;
                cmd.Parameters.Add("@compercentandVal", SqlDbType.SmallMoney).Value = cPaymentBooking.ComVal;
                cmd.Parameters.Add("@price_amount", SqlDbType.Money).Value = cPaymentBooking.PriceAmount;
                if (cPaymentBooking.CostAmount.HasValue)
                    cmd.Parameters.AddWithValue("@cost_amount", cPaymentBooking.CostAmount);
                else
                    cmd.Parameters.AddWithValue("@cost_amount", DBNull.Value);

                if (cPaymentBooking.ComAmount.HasValue)
                    cmd.Parameters.AddWithValue("@com_amount", cPaymentBooking.ComAmount);
                else
                    cmd.Parameters.AddWithValue("@com_amount", DBNull.Value);

              

                cn.Open();

                return ExecuteNonQuery(cmd);
            }
        }
    }


    public class Account_payment:Hotels2BaseClass
    {
        public int PaymentID { get; set; }
        public int ProductId { get; set; }
        public byte ManageId { get; set; }
        
        public byte? ComCat { get; set; }
        public short StaffId { get; set; }
        public decimal?  ComVal { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal? CostTotal { get; set; }
        public decimal? ComTotal { get; set; }
        public decimal? SaleComTotal { get; set; }
        public decimal? DepositTotal { get; set; }
        public decimal? VatTotal { get; set; }
        public decimal? WithholdingTaxTotal { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime? DatePayment { get; set; }
        public bool Status { get; set; }

        public byte? BankId { get; set; }

        private string _account_name = string.Empty;
        private string _account_num = string.Empty;
        private string _account_branch = string.Empty;
        private string _account_type = string.Empty;

        public string AccountName { get { return _account_name; } set { _account_name = value; } }
        public string AccountNum { get { return _account_num; } set { _account_num = value; } }
        public string AccountBranch { get { return _account_branch; } set { _account_branch = value; } }
        public string AccountType { get { return _account_type; } set { _account_type = value; } }

        private string _months_detail = string.Empty;
        public string MonthsDetail {
            get { return _months_detail; }
            set { _months_detail = value; }
        }


        public DateTime? DateSalePayment { get; set; }
        public byte? Vat { get; set; }
        public byte? WithholdingTax { get; set; }
        public decimal Fee { get; set; }


        private string _bank_title = string.Empty;
        public string BankTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_bank_title))
                {
                    if(this.BankId.HasValue)
                        _bank_title = SupplierAccount.getBanktitlebyId((byte)this.BankId);
                }
                return _bank_title;
            }
            //set { _bank_title = value; }
        }

        private string _product_Title = string.Empty;

        public string ProductTitle
        {
            get
            {
                Hotels2thailand.Production.Product  cProduct = new Hotels2thailand.Production.Product();
                _product_Title = cProduct.GetProductById(this.ProductId).Title;
                return _product_Title;
            }
            
            
        }

        private decimal _com_hotel_manage_total = 0;
        public decimal ComhotelManageTotal
        {
            get
            {
                if (_com_hotel_manage_total == 0)
                {
                    _com_hotel_manage_total = (decimal)this.ComTotal;
                    if (this.VatTotal.HasValue && (decimal)this.VatTotal > 0)
                    {

                        _com_hotel_manage_total = _com_hotel_manage_total + ((decimal)this.ComTotal * (byte)this.Vat) / 100;


                        if (this.WithholdingTaxTotal.HasValue && (decimal)this.WithholdingTaxTotal > 0)
                        {
                           
                            _com_hotel_manage_total = _com_hotel_manage_total - ((decimal)this.ComTotal * (byte)this.WithholdingTax) / 100;
                        }
                    }
                }
                return _com_hotel_manage_total;
            }
        }
        public Account_payment()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public IList<object> getAccountPayment_hotel_manage_pendding()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  acp.payment_id,acp.product_id,acp.manage_id,acp.cat_id,acp.staff_id,acp.compercentandVal,acp.price_total,acp.cost_total,acp.com_total,acp.sale_com_total,acp.deposit_total,acp.vat_total,acp.withholding_total,acp.date_submit,acp.date_payment,acp.status,acp.bank_id,acp.account_name,acp.account_number,acp.account_branch ,acp.account_type,acp.months_detail,acp.date_sale_payment,acp.vat,acp.withholding_tax FROM tbl_account_hotel_payment acp WHERE acp.status = 1 AND acp.manage_id = 1 AND acp.cat_id IS NULL ", cn);
                //cmd.Parameters.Add("@account_id", SqlDbType.TinyInt).Value = BhtAccountId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }

        public IList<object> getAccountPaymentBybhtAccountID(byte BhtAccountId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  acp.payment_id,acp.product_id,acp.manage_id,acp.cat_id,acp.staff_id,acp.compercentandVal,acp.price_total,acp.cost_total,acp.com_total,acp.sale_com_total,acp.deposit_total,acp.vat_total,acp.withholding_total,acp.date_submit,acp.date_payment,acp.status,acp.bank_id,acp.account_name,acp.account_number,acp.account_branch ,acp.account_type,acp.months_detail,acp.date_sale_payment,acp.vat,acp.withholding_tax,abht.fee FROM tbl_account_hotel_payment acp, tbl_account_bht_bank_payment abht WHERE acp.status = 1 AND acp.payment_id = abht.payment_id AND abht.confirm_bank_payment IS NULL AND abht.account_id = @account_id", cn);
                cmd.Parameters.Add("@account_id", SqlDbType.TinyInt).Value = BhtAccountId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
 
            }
        }

        public IList<object> getAccountPaymentBybhtAccountIDHistory(byte BhtAccountId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  acp.payment_id,acp.product_id,acp.manage_id,acp.cat_id,acp.staff_id,acp.compercentandVal,acp.price_total,acp.cost_total,acp.com_total,acp.sale_com_total,acp.deposit_total,acp.vat_total,acp.withholding_total,acp.date_submit,acp.date_payment,acp.status,acp.bank_id,acp.account_name,acp.account_number,acp.account_branch ,acp.account_type,acp.months_detail,acp.date_sale_payment,acp.vat,acp.withholding_tax,abht.fee  FROM tbl_account_hotel_payment acp, tbl_account_bht_bank_payment abht WHERE acp.status = 1 AND acp.payment_id = abht.payment_id AND abht.confirm_bank_payment IS NOT NULL AND abht.account_id = @account_id", cn);
                cmd.Parameters.Add("@account_id", SqlDbType.TinyInt).Value = BhtAccountId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

            }
        }


        public Account_payment getAccountPayment(int intPaymentID)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account_hotel_payment WHERE payment_id =@payment_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Account_payment)MappingObjectFromDataReader(reader);
                else
                    return null;
            }


        }

        public IList<object> getAccountPaymentByProductId(int intProductId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account_hotel_payment WHERE product_id =@product_id AND cat_id = 2 AND status = 1 ORDER BY date_payment", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }


        }

        public int InsertPayment(Account_payment cPyment)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_account_hotel_payment (product_id,manage_id,cat_id,staff_id,compercentandVal,price_total,cost_total,com_total,sale_com_total,deposit_total,vat_total,withholding_total,date_submit,date_payment,status,bank_id,account_name,account_number,account_branch,account_type,months_detail,vat,withholding_tax) VALUES(@product_id,@manage_id,@cat_id,@staff_id,@compercentandVal,@price_total,@cost_total,@com_total,@sale_com_total,@deposit_total,@vat_total,@withholding_total,@date_submit,@date_payment,@status,@bank_id,@account_name,@account_number,@account_branch,@account_type,@months_detail,@vat,@withholding_tax); SET @payment_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = cPyment.ProductId;
                cmd.Parameters.Add("@manage_id", SqlDbType.TinyInt).Value = cPyment.ManageId;

                if (cPyment.ComCat.HasValue)
                    cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = cPyment.ComCat;
                else
                    cmd.Parameters.AddWithValue("@cat_id", DBNull.Value);

               
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = cPyment.StaffId;

                if (cPyment.ComVal.HasValue)
                    cmd.Parameters.Add("@compercentandVal", SqlDbType.SmallMoney).Value = cPyment.ComVal;
                else
                    cmd.Parameters.AddWithValue("@compercentandVal", DBNull.Value);

                cmd.Parameters.Add("@price_total", SqlDbType.Money).Value = cPyment.PriceTotal;

                if (cPyment.CostTotal.HasValue)
                    cmd.Parameters.AddWithValue("@cost_total", cPyment.CostTotal);
                else
                    cmd.Parameters.AddWithValue("@cost_total", DBNull.Value);

                if (cPyment.ComTotal.HasValue)
                    cmd.Parameters.AddWithValue("@com_total", cPyment.ComTotal);
                else
                    cmd.Parameters.AddWithValue("@com_total", DBNull.Value);

                if (cPyment.SaleComTotal.HasValue)
                    cmd.Parameters.AddWithValue("@sale_com_total", cPyment.SaleComTotal);
                else
                    cmd.Parameters.AddWithValue("@sale_com_total", DBNull.Value);

                if (cPyment.DepositTotal.HasValue)
                    cmd.Parameters.AddWithValue("@deposit_total", cPyment.DepositTotal);
                else
                    cmd.Parameters.AddWithValue("@deposit_total", DBNull.Value);

                if (cPyment.VatTotal.HasValue)
                    cmd.Parameters.AddWithValue("@vat_total", cPyment.VatTotal);
                else
                    cmd.Parameters.AddWithValue("@vat_total", DBNull.Value);

                if (cPyment.WithholdingTaxTotal.HasValue)
                    cmd.Parameters.AddWithValue("@withholding_total", cPyment.WithholdingTaxTotal);
                else
                    cmd.Parameters.AddWithValue("@withholding_total", DBNull.Value);


                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = cPyment.DateSubmit;

                if (cPyment.DatePayment.HasValue)
                    cmd.Parameters.AddWithValue("@date_payment", cPyment.DatePayment);
                else
                    cmd.Parameters.AddWithValue("@date_payment", DBNull.Value);

                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = cPyment.Status;

                if (cPyment.BankId.HasValue)
                    cmd.Parameters.AddWithValue("@bank_id", cPyment.BankId);
                else
                    cmd.Parameters.AddWithValue("@bank_id", DBNull.Value);

                cmd.Parameters.Add("@account_name", SqlDbType.NVarChar).Value = cPyment.AccountName;
                cmd.Parameters.Add("@account_number", SqlDbType.NVarChar).Value = cPyment.AccountNum;
                cmd.Parameters.Add("@account_branch", SqlDbType.NVarChar).Value = cPyment.AccountBranch;
                cmd.Parameters.Add("@account_type", SqlDbType.NVarChar).Value = cPyment.AccountType;

                cmd.Parameters.Add("@months_detail", SqlDbType.NVarChar).Value = cPyment.MonthsDetail;

                if (cPyment.Vat.HasValue)
                    cmd.Parameters.AddWithValue("@vat", cPyment.Vat);
                else
                    cmd.Parameters.AddWithValue("@vat", DBNull.Value);

                if (cPyment.WithholdingTax.HasValue)
                    cmd.Parameters.AddWithValue("@withholding_tax", cPyment.WithholdingTax);
                else
                    cmd.Parameters.AddWithValue("@withholding_tax", DBNull.Value);

                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();


                if(ExecuteNonQuery(cmd) > 0)
                    ret = (int)cmd.Parameters["@payment_id"].Value;
            }

            return ret;
        }
        public bool UpdatePayment(Account_payment cPyment)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_hotel_payment SET product_id=@product_id,manage_id=@manage_id,cat_id=@cat_id,compercentandVal=@compercentandVal,price_total=@price_total,cost_total=@cost_total,com_total=@com_total,sale_com_total=@sale_com_total,deposit_total=@deposit_total,vat_total=@vat_total,withholding_total=@withholding_total,bank_id=@bank_id,account_name=@account_name,account_number=@account_number,account_branch=@account_branch,account_type=@account_type,months_detail=@months_detail,vat=@vat,withholding_tax=@withholding_tax WHERE payment_id =@payment_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = cPyment.ProductId;
                cmd.Parameters.Add("@manage_id", SqlDbType.TinyInt).Value = cPyment.ManageId;

                if (cPyment.ComCat.HasValue)
                    cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = cPyment.ComCat;
                else
                    cmd.Parameters.AddWithValue("@cat_id", DBNull.Value);


                

                if (cPyment.ComVal.HasValue)
                    cmd.Parameters.Add("@compercentandVal", SqlDbType.SmallMoney).Value = cPyment.ComVal;
                else
                    cmd.Parameters.AddWithValue("@compercentandVal", DBNull.Value);

                cmd.Parameters.Add("@price_total", SqlDbType.Money).Value = cPyment.PriceTotal;

                if (cPyment.CostTotal.HasValue)
                    cmd.Parameters.AddWithValue("@cost_total", cPyment.CostTotal);
                else
                    cmd.Parameters.AddWithValue("@cost_total", DBNull.Value);

                if (cPyment.ComTotal.HasValue)
                    cmd.Parameters.AddWithValue("@com_total", cPyment.ComTotal);
                else
                    cmd.Parameters.AddWithValue("@com_total", DBNull.Value);

                if (cPyment.SaleComTotal.HasValue)
                    cmd.Parameters.AddWithValue("@sale_com_total", cPyment.SaleComTotal);
                else
                    cmd.Parameters.AddWithValue("@sale_com_total", DBNull.Value);

                if (cPyment.DepositTotal.HasValue)
                    cmd.Parameters.AddWithValue("@deposit_total", cPyment.DepositTotal);
                else
                    cmd.Parameters.AddWithValue("@deposit_total", DBNull.Value);

                if (cPyment.VatTotal.HasValue)
                    cmd.Parameters.AddWithValue("@vat_total", cPyment.VatTotal);
                else
                    cmd.Parameters.AddWithValue("@vat_total", DBNull.Value);

                if (cPyment.WithholdingTaxTotal.HasValue)
                    cmd.Parameters.AddWithValue("@withholding_total", cPyment.WithholdingTaxTotal);
                else
                    cmd.Parameters.AddWithValue("@withholding_total", DBNull.Value);


                

                if (cPyment.DatePayment.HasValue)
                    cmd.Parameters.AddWithValue("@date_payment", cPyment.DatePayment);
                else
                    cmd.Parameters.AddWithValue("@date_payment", DBNull.Value);

                

                if (cPyment.BankId.HasValue)
                    cmd.Parameters.AddWithValue("@bank_id", cPyment.BankId);
                else
                    cmd.Parameters.AddWithValue("@bank_id", DBNull.Value);

                cmd.Parameters.Add("@account_name", SqlDbType.NVarChar).Value = cPyment.AccountName;
                cmd.Parameters.Add("@account_number", SqlDbType.NVarChar).Value = cPyment.AccountNum;
                cmd.Parameters.Add("@account_branch", SqlDbType.NVarChar).Value = cPyment.AccountBranch;
                cmd.Parameters.Add("@account_type", SqlDbType.NVarChar).Value = cPyment.AccountType;

                cmd.Parameters.Add("@months_detail", SqlDbType.NVarChar).Value = cPyment.MonthsDetail;

                if (cPyment.Vat.HasValue)
                    cmd.Parameters.AddWithValue("@vat", cPyment.Vat);
                else
                    cmd.Parameters.AddWithValue("@vat", DBNull.Value);

                if (cPyment.WithholdingTax.HasValue)
                    cmd.Parameters.AddWithValue("@withholding_tax", cPyment.WithholdingTax);
                else
                    cmd.Parameters.AddWithValue("@withholding_tax", DBNull.Value);

                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = cPyment.PaymentID;

                cn.Open();


                return (ExecuteNonQuery(cmd) == 1);
                    
            }

        }

        // for disable Payment 
        public bool CancelPayment(int intPaymentID)
        {
            bool ret = false;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_hotel_payment SET status = 0 WHERE payment_id=@payment_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
                cn.Open();


                Account_payment_Booking cAcc = new Account_payment_Booking();
                if (ExecuteNonQuery(cmd) == 1)
                {
                    ret = cAcc.DisableBookingPayment(intPaymentID);
                }

                return ret;


            }
        }

        public bool MakePaymentCompleted(int intPaymentID, DateTime dDatePayment)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_account_hotel_payment SET date_payment = @date_payment WHERE payment_id=@payment_id", cn);
                cmd.Parameters.Add("@payment_id", SqlDbType.Int).Value = intPaymentID;
                cmd.Parameters.Add("@date_payment", SqlDbType.SmallDateTime).Value = dDatePayment;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }


    }
}