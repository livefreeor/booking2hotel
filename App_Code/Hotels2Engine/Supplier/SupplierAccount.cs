using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for SupplierAccount
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public class SupplierAccount:Hotels2BaseClass
    {
        /// <summary>
        ///SupplierAccount Property 
        /// </summary>
        public short AccountId { get; set; }
        public byte BankId { get; set; }
        public byte AccountTypeId { get; set; }
        public short SupplierId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountBranch { get; set; }
        public bool FlagDefault { get; set; }
        public string Comment { get; set; }

        

        /// <summary>
        /// special Property!!! Lazy Load Return For Title of AccountTitle
        /// </summary>
        private string _account_type_title = string.Empty;
        public string AccountTypeTitle
        {
            get 
            {
                if(string.IsNullOrEmpty(_account_type_title))
                    _account_type_title = SupplierAccount.getAccountTitleById(this.AccountTypeId);
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



        //===============================================================================================
        //================================= Supplier Account ============================================

        //declare Instance Linq for Supplier Module
        LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

        public bool Update()
        {
            return SupplierAccount.updatesSupplierAccount(this.AccountId, this.BankId, this.AccountTypeId, this.SupplierId, this.AccountTitle, this.AccountName, this.AccountNumber,
                this.AccountBranch, this.FlagDefault, this.Comment);
        }

        /// <summary>
        /// get List All Supplier Account ; Instance Method ; Returns list Collection of SupplierAccount
        /// </summary>
        /// <returns></returns>
        public List<object> getSupplierAccountAll()
        {
            
            var result = from spa in dcSupplier.tbl_supplier_accounts
                         select spa;
            return MappingObjectFromDataContextCollection(result);
        }

        public List<object> getSupplierAccountAllBySupplierID(short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_supplier_account WHERE supplier_id = @supplier_id", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
          
        }

        public List<object> getSupplierAccountAllBySupplierIDAndFlagDefaultTrue(short shrSupplierId)
        {
            var result = from spa in dcSupplier.tbl_supplier_accounts
                         where spa.supplier_id == shrSupplierId && spa.flag_default == true
                         select spa;
            return MappingObjectFromDataContextCollection(result);
        }


        /// <summary>
        /// get Supplier Account By Id ; Instance Method; Returns Instance of SupplierAccount
        /// </summary>
        /// <param name="shrSupplierAc"></param>
        /// <returns></returns>
        public SupplierAccount getSupplierAccountById(short shrSupplierAc)
        {
            SupplierAccount clSupplierAcc = new SupplierAccount();
            var result = dcSupplier.tbl_supplier_accounts.SingleOrDefault(spa => spa.acount_id == shrSupplierAc);
            return (SupplierAccount)MappingObjectFromDataContext(result);
        }

        
        /// <summary>
        /// insert New Supplier Account ; Instance Method ; Return int Identity of AccountId
        /// </summary>
        /// <param name="clSupplierAc"></param>
        /// <returns></returns>
        /// 
        public int insertNewSupplierAccount(SupplierAccount clSupplierAc)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO tbl_supplier_account (bank_id,type_id,supplier_id,account_title,account_name,account_number,account_branch,flag_default,comment)");
                query.Append(" VALUES ( @bank_id, @type_id, @supplier_id, @account_title, @account_name, @account_number, @account_branch, @flag_default, @comment); SET @account_id = SCOPE_IDENTITY();");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                //cmd.Parameters.Add("@acount_id", SqlDbType.SmallInt).Value = clSupplierAc.AccountId;
                cmd.Parameters.Add("@bank_id", SqlDbType.TinyInt).Value = clSupplierAc.BankId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = clSupplierAc.AccountTypeId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = clSupplierAc.SupplierId;
                cmd.Parameters.Add("@account_title", SqlDbType.NVarChar).Value = clSupplierAc.AccountTitle;
                cmd.Parameters.Add("@account_name", SqlDbType.NVarChar).Value = clSupplierAc.AccountName;
                cmd.Parameters.Add("@account_number", SqlDbType.NVarChar).Value = clSupplierAc.AccountNumber;
                cmd.Parameters.Add("@account_branch", SqlDbType.NVarChar).Value = clSupplierAc.AccountBranch;
                cmd.Parameters.Add("@flag_default", SqlDbType.Bit).Value = clSupplierAc.FlagDefault;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = clSupplierAc.Comment;
                cmd.Parameters.Add("@account_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (short)cmd.Parameters["@account_id"].Value;
                
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_detail, StaffLogActionType.Insert, StaffLogSection.Supplier,
                clSupplierAc.SupplierId, "tbl_supplier_account", "bank_id,type_id,supplier_id,account_title,account_name,account_number,account_branch,flag_default,comment", "account_id", ret);
            //========================================================================================================================================================
            return ret;
        }
        //public int insertNewSupplierAccount(SupplierAccount clSupplierAc)
        //{
            
        //        var varInsert = new tbl_supplier_account
        //        {
        //            acount_id = clSupplierAc.AccountId,
        //            bank_id = clSupplierAc.BankId,
        //            type_id = clSupplierAc.AccountTypeId,
        //            supplier_id = clSupplierAc.SupplierId,
        //            account_title = clSupplierAc.AccountTitle,
        //            account_name = clSupplierAc.AccountName,
        //            account_number = clSupplierAc.AccountNumber,
        //            account_branch = clSupplierAc.AccountBranch,
        //            flag_default = clSupplierAc.FlagDefault,
        //            comment = clSupplierAc.Comment
                    
        //        };

        //        dcSupplier.tbl_supplier_accounts.InsertOnSubmit(varInsert);
        //        dcSupplier.SubmitChanges();

        //        int ret = varInsert.acount_id;
        //        return ret;
           
        //}

        /// <summary>
        /// Update SupplierAccount ; Instance Method ; Return Bool IsUpdated
        /// </summary>
        /// <param name="clSupplierAc"></param>
        /// <returns></returns>
        public bool updateSupplierAccount(SupplierAccount clSupplierAc)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_account", "bank_id,type_id,supplier_id,account_title,account_name,account_number,account_branch,flag_default,comment", "acount_id", clSupplierAc.AccountId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("UPDATE tbl_supplier_account SET bank_id=@bank_id, type_id=@type_id, supplier_id=@supplier_id, account_title= @account_title,");
                query.Append(" account_name=@account_name, account_number=@account_number, account_branch=@account_branch, flag_default=@flag_default, comment=@comment WHERE acount_id=@acount_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@acount_id", SqlDbType.SmallInt).Value = clSupplierAc.AccountId;
                cmd.Parameters.Add("@bank_id", SqlDbType.TinyInt).Value = clSupplierAc.BankId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = clSupplierAc.AccountTypeId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = clSupplierAc.SupplierId;
                cmd.Parameters.Add("@account_title", SqlDbType.NVarChar).Value = clSupplierAc.AccountTitle;
                cmd.Parameters.Add("@account_name", SqlDbType.NVarChar).Value = clSupplierAc.AccountName;
                cmd.Parameters.Add("@account_number", SqlDbType.NVarChar).Value = clSupplierAc.AccountNumber;
                cmd.Parameters.Add("@account_branch", SqlDbType.NVarChar).Value = clSupplierAc.AccountBranch;
                cmd.Parameters.Add("@flag_default", SqlDbType.Bit).Value = clSupplierAc.FlagDefault;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = clSupplierAc.Comment;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_detail, StaffLogActionType.Update, StaffLogSection.Supplier, clSupplierAc.SupplierId,
                "tbl_supplier_account", "bank_id,type_id,supplier_id,account_title,account_name,account_number,account_branch,flag_default,comment", arroldValue, "acount_id", clSupplierAc.AccountId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
            //    var varUpdate = dcSupplier.tbl_supplier_accounts.Single(spa => spa.acount_id == clSupplierAc.AccountId);

            //    varUpdate.bank_id = clSupplierAc.BankId;
            //    varUpdate.type_id = clSupplierAc.AccountTypeId;
            //    varUpdate.supplier_id = clSupplierAc.SupplierId;
            //    varUpdate.account_title = clSupplierAc.AccountTitle;
            //    varUpdate.account_name = clSupplierAc.AccountName;
            //    varUpdate.account_number = clSupplierAc.AccountNumber;
            //    varUpdate.account_branch = clSupplierAc.AccountBranch;
            //    varUpdate.flag_default = clSupplierAc.FlagDefault;
            //    varUpdate.comment = clSupplierAc.Comment;


            //    dcSupplier.SubmitChanges();
            //    bool ret = true;
            //    return ret;
            ////}
            ////catch
            ////{
            ////    bool ret = false;
            ////    return ret;
            ////}
        }
        
        /// <summary>
        /// insert New Supplier Account ; Static Method ; Returns int Identity of AccountId
        /// </summary>
        /// <param name="bytBankId"></param>
        /// <param name="bytTypeId"></param>
        /// <param name="shrSupplierId"></param>
        /// <param name="strAcTitle"></param>
        /// <param name="strAcName"></param>
        /// <param name="strAcNumber"></param>
        /// <param name="strAcBranch"></param>
        /// <param name="FlagDefault"></param>
        /// <returns></returns>
        //public static int insertSupplierAccount( byte bytBankId, byte bytTypeId, short shrSupplierId,
        //    string strAcTitle, string strAcName, string strAcNumber, string strAcBranch, bool FlagDefault)
        //{
        //    SupplierAccount clSupplierAc = new SupplierAccount
        //    {
        //        AccountId = 0,
        //        BankId = bytBankId,
        //        AccountTypeId = bytTypeId,
        //        SupplierId = shrSupplierId,
        //        AccountTitle = strAcTitle,
        //        AccountName = strAcName,
        //        AccountNumber = strAcNumber,
        //        AccountBranch = strAcBranch,
        //        FlagDefault = FlagDefault
        //    };
        //    return clSupplierAc.insertNewSupplierAccount(clSupplierAc);
        //}

        /// <summary>
        /// update Supplier Account ; Static Method ; Returns bool IsUpdated
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="bytBankId"></param>
        /// <param name="bytTypeId"></param>
        /// <param name="shrSupplierId"></param>
        /// <param name="strAcTitle"></param>
        /// <param name="strAcName"></param>
        /// <param name="strAcNumber"></param>
        /// <param name="strAcBranch"></param>
        /// <param name="FlagDefault"></param>
        /// <returns></returns>
        private static bool updatesSupplierAccount(short shrAccountId, byte bytBankId, byte bytTypeId, short shrSupplierId,
            string strAcTitle, string strAcName, string strAcNumber, string strAcBranch, bool FlagDefault, string strComment)
        {
            SupplierAccount clSupplierAc = new SupplierAccount
         {
             AccountId = shrAccountId,
             BankId = bytBankId,
             AccountTypeId = bytTypeId,
             SupplierId = shrSupplierId,
             AccountTitle = strAcTitle,
             AccountName = strAcName,
             AccountNumber = strAcNumber,
             AccountBranch = strAcBranch,
             FlagDefault = FlagDefault,
              Comment = strComment
         };
            return clSupplierAc.updateSupplierAccount(clSupplierAc);
        }

        //===========================================================================================
        //================================= Account Type ============================================

        //public static List<string> getListAccountTitle(tbl_supplier_account clAccountType)
        //{
        //    //declare Instance Linq for Supplier Module
        //    LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

        //    List<string> listTAccountTypeTitle = new List<string>();

        //    var result = from at in dcSupplier.tbl_account_types
        //                 orderby at.title ascending
        //                 select at;

        //    foreach (var item in result)
        //    {
        //        listTAccountTypeTitle.Add(item.title);
        //    }
        //    return listTAccountTypeTitle;
        //}

        public static Dictionary<int, string> getListAccountTitle()
        {
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();


            Dictionary<int, string> dicAccountTypeTitle = new Dictionary<int, string>();


            var result = from at in dcSupplier.tbl_account_types
                         orderby at.title ascending
                         select at;
            foreach (var at in result)
            {
                dicAccountTypeTitle.Add(at.type_id, at.title);
            }

            return dicAccountTypeTitle;
        }

        public static string getAccountTitleById(byte bytAccountTypeId)
        {
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var result = dcSupplier.tbl_account_types.SingleOrDefault(spt => spt.type_id == bytAccountTypeId);
            return result.title;
        }

        public static int  insertNewAccountType(string strTitle)
        {
            
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var varInsert = new tbl_account_type { type_id = 0, title = strTitle };

            dcSupplier.tbl_account_types.InsertOnSubmit(varInsert);
            dcSupplier.SubmitChanges();

            int ret = varInsert.type_id;
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_account, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_account_type", "title", "type_id", varInsert.type_id);
            //========================================================================================================================================================
            return ret;
            
        }

        

        //===========================================================================================
        //================================= tbl_Bank ============================================

        public static Dictionary<int, string> getListBankTitleALL()
        {
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            
            Dictionary<int,string> dicBank = new Dictionary<int,string>();


            var result = from bank in dcSupplier.tbl_banks
                         orderby bank.title ascending
                         select bank;
            foreach (var bank in result)
            {
                dicBank.Add(bank.bank_id, bank.title);
            }

            return dicBank;
        }

       
        public static string getBanktitlebyId(byte bytBankId)
        {
            SupplierAccount cAccount = new SupplierAccount();
            using (SqlConnection cn = new SqlConnection(cAccount.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_bank WHERE bank_id = @bank_id",cn);
                cmd.Parameters.Add("@bank_id", SqlDbType.TinyInt).Value = bytBankId;
                cn.Open();
                IDataReader reader = cAccount.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;

            }
            ////declare Instance Linq for Supplier Module
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            //var result = dcSupplier.tbl_banks.SingleOrDefault();
            //return result.title;
        }

        public static int insertNewBank(string strTitle)
        {
            
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var varInsert = new tbl_bank { bank_id = 0, title = strTitle };

            dcSupplier.tbl_banks.InsertOnSubmit(varInsert);
            dcSupplier.SubmitChanges();

            int ret = varInsert.bank_id;

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_account, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_bank", "title", "bank_id", varInsert.bank_id);
            //========================================================================================================================================================
            return ret;
           
        }

        public static bool updateBank(byte bytBankId, string strTitle)
        {
            
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var varInsert = dcSupplier.tbl_banks.Single(spb => spb.bank_id == bytBankId);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(varInsert.title);
            //============================================================================================================================
            varInsert.title = strTitle;

            dcSupplier.SubmitChanges();

            bool ret = true;
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_account, StaffLogActionType.Update, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_bank", "title", arroldValue, "bank_id", bytBankId);
            //==================================================================================================================== COMPLETED ========
            return ret;
            
            
        }
    }
}