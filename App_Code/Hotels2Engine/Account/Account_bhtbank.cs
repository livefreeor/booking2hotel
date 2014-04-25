using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Suppliers;

/// <summary>
/// Summary description for Account_bhtbank
/// </summary>
/// 
namespace Hotels2thailand.Account
{
    public class Account_bhtbank : Hotels2BaseClass
    {

        public byte AccountId { get; set; }
        public byte BankId { get; set; }
        public byte TypeId { get; set; }
        public string Accounttitle { get; set; }
        public string AccountNum { get; set; }
        public string AccountName { get; set; }
        public string AccountBranch { get; set; }


        private string _account_type_title = string.Empty;
        public string AccountTypeTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_account_type_title))
                    _account_type_title = SupplierAccount.getAccountTitleById(this.TypeId);
                return _account_type_title;
            }
            //set { _account_type_title = value; }
        }

        /// <summary>
        /// special Property !!1 Lazy Load; Returns for Title of BankTitle
        /// </summary>
        private string _bank_title = string.Empty;

        public string BankTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_bank_title))
                    _bank_title = SupplierAccount.getBanktitlebyId(this.BankId);
                return _bank_title;
            }
            //set { _bank_title = value; }
        }
        public Account_bhtbank()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public IList<object> getBhtBankAccountCurrentPayment()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                {
                    //SqlCommand cmd = new SqlCommand("SELECT ac.account_id, ac.bank_id, ac.type_id, ac.title, ac.bank_nubmer,ac.account_name,ac.branch FROM tbl_account ac , tbl_account_bht_bank_payment abht , tbl_account_hotel_payment acp WHERE ac.account_id = abht.account_id AND abht.confirm_bank_payment IS NULL  AND acp.payment_id= abht.payment_id AND acp.status = 1 ", cn);

                     SqlCommand cmd = new SqlCommand("SELECT ac.account_id, ac.bank_id, ac.type_id, ac.title, ac.bank_nubmer,ac.account_name,ac.branch FROM tbl_account ac   WHERE (SELECT COUNT(*) FROM tbl_account_bht_bank_payment abht ,tbl_account_hotel_payment acp WHERE  ac.account_id = abht.account_id AND abht.confirm_bank_payment IS NULL AND acp.payment_id=abht.payment_id AND acp.status = 1  ) > 0", cn);

                    
                    cn.Open();
                    return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                }
            }

        }

        public IList<object> getBhtBankAccountCurrentPaymentPaid()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                {
                    //SqlCommand cmd = new SqlCommand("SELECT ac.account_id, ac.bank_id, ac.type_id, ac.title, ac.bank_nubmer,ac.account_name,ac.branch FROM tbl_account ac , tbl_account_bht_bank_payment abht , tbl_account_hotel_payment acp WHERE ac.account_id = abht.account_id AND abht.confirm_bank_payment IS NULL  AND acp.payment_id= abht.payment_id AND acp.status = 1 ", cn);

                    SqlCommand cmd = new SqlCommand("SELECT ac.account_id, ac.bank_id, ac.type_id, ac.title, ac.bank_nubmer,ac.account_name,ac.branch FROM tbl_account ac   WHERE (SELECT COUNT(*) FROM tbl_account_bht_bank_payment abht ,tbl_account_hotel_payment acp WHERE  ac.account_id = abht.account_id AND abht.confirm_bank_payment IS NOT NULL AND acp.payment_id=abht.payment_id AND acp.status = 1  ) > 0", cn);


                    cn.Open();
                    return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                }
            }

        }
        public IList<object> getBhtBankAccount()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_account", cn);
                    cn.Open();
                    return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                }
            }

        }
    }
}