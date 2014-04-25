using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;
using Hotels2thailand.Production;
using System.Data.SqlClient;
using Hotels2thailand;
using System.Data;


/// <summary>
/// Summary description for Supplier
/// include tbl_Supplier and tbl_SupplierCategory
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public partial class Supplier : Hotels2BaseClass
    {
        

        /// <summary>
        /// Property of Supplier
        /// </summary>
        public short SupplierId { get; set; }
        public byte CategoryId { get; set; }
        public short PaymentTypeId { get; set; }
        public string SupplierTitle { get; set; }
        public string SupplierTitleCommon { get; set; }
        public string SupplerTitleCompany{ get;set;}
        // delete from Db 
        //public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string AddressOffice { get; set; }
        public string Comment { get; set; }
        public byte TaxVat { get; set; }
        public byte TaxService { get; set; }
        public byte TaxLocal { get; set; }

        private bool _status = true;
        public bool Status {
            get { return _status; }
            set { _status = value; }
        }

        public string SuffixUserExtra { get; set; }

        private string _catTitle = string.Empty;

        
        /// <summary>
        /// Special Property ; Returns Category Title
        /// </summary>
        public string CatTitle
        {
            get
            {
                 if (string.IsNullOrEmpty(_catTitle))
                    _catTitle = Supplier.getSupplierCategoryById(this.CategoryId);
                return _catTitle;
            }
            
        }

       

        /// <summary>
        /// Special Property ;Returns instance SupplierPaymentType
        /// </summary>
        private SupplierPaymentType _payment_type = null;

        public SupplierPaymentType PaymentType
        {
            get 
            {
                if (_payment_type == null)
                    _payment_type = SupplierPaymentType.getSupplierPaymentTypeById(this.PaymentTypeId);
                return _payment_type;
            }
            
        }

        public string _payment_type_title;
        public string PaymentTypeTitle
        {
            get
            {
                _payment_type_title = SupplierPaymentType.getSupplierPaymentTypeById(this.PaymentTypeId).Title;
                return _payment_type_title;
            }
        }

        public string _payment_type_detail;
        public string PaymentTypeDetail
        {
            get
            {
                _payment_type_detail = SupplierPaymentType.getSupplierPaymentTypeById(this.PaymentTypeId).Detail;
                return _payment_type_detail;
            }
        }

        public Supplier()
        {
            _status = true;
        }

        //declare Instance Linq for Supplier Module
        LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
        LinqProductionDataContext dcProduct = new LinqProductionDataContext();


        //===============================================================================================
        //================================= Supplier  =======================================================
        

        //public bool update()
        //{
        //    return Supplier.updateSuppplier(this.SupplierId, this.CategoryId, this.PaymentTypeId, this.SupplierTitle, this.SupplierTitleCommon, this.SupplerTitleCompany,
        //         this.Address, this.Comment, this.TaxVat, this.TaxService, this.TaxLocal, this.Status);
        //}

        public List<object> getListSupplierAllbyStatus(bool Status)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status FROM tbl_supplier WHERE status=@Status  ORDER BY title", cn);
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = Status;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> getListSupplierAllAdVancesearch(string strTxtSearch)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status FROM tbl_supplier WHERE title LIKE '%" + strTxtSearch + "%' ORDER BY title", cn);
                //cmd.Parameters.Add("@StringText", SqlDbType.NVarChar).Value = strTxtSearch;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public Supplier GetSupplierbyId(short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status,suffix_user_extranet FROM tbl_supplier WHERE supplier_id=@supplierId", cn);
                cmd.Parameters.Add("@supplierId", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    //HttpContext.Current.Response.Write("TEST");
                    //HttpContext.Current.Response.End();
                    return (Supplier)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        

        public string GEtSuffixByChainId(short shrChainId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT suffix_user_extranet FROM tbl_product_chain pc , tbl_product p, tbl_supplier sup WHERE  pc.product_id = p.product_id AND sup.supplier_id = p.supplier_price AND pc.chain_id = " + shrChainId + "", cn);
                
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    //HttpContext.Current.Response.Write("TEST");
                    //HttpContext.Current.Response.End();
                    return reader[0].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        

        /// <summary>
        /// get List Of Supplier ; Instance method ; Returns List Collection
        /// </summary>
        /// <returns></returns>
        //public List<object> getListSupplierAll()
        //{
        //    var result = from sp in dcSupplier.tbl_suppliers
        //                 select sp;
        //    return MappingObjectFromDataContextCollection(result);
        //}

        public List<object> getListSupplierListWholeSaleOnly()
        {
            var result = from sp in dcSupplier.tbl_suppliers
                         where sp.cat_id == 1
                         select sp;
            return MappingObjectFromDataContextCollection(result);
        }

        

        /// <summary>
        /// !waiting for comment .... 
        /// </summary>
        /// <returns></returns>
        public List<object> GetSupplierListByHaveStaff()
        {
            Staff clStaff = new Staff();

            var result = from sfc in dcSupplier.tbl_suppliers
                         select sfc;

            return MappingObjectFromDataContextCollection(result);
        }

        /// <summary>
        /// get List Supplier By Aphabet ; Instance Method ; Returns List Collection
        /// </summary>
        /// <param name="strKeyWord"></param>
        /// <returns></returns>
        /// 

        public List<object> getListSupplierByAlphabet(string strKeyWord)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status FROM tbl_supplier WHERE title LIKE '" + strKeyWord + "%' ORDER BY title", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> getListSupplierByAlphabet(string strKeyWord, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status FROM tbl_supplier WHERE title LIKE '" + strKeyWord + "%' AND supplier_id NOT IN (SELECT supplier_id FROM tbl_product_supplier WHERE product_id =@product_id) ORDER BY title", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }

        public List<object> getListSupplierByAlphabet(string strKeyWord, IList<Supplier> IList)
        {
            string strKeyAphabet = strKeyWord.Trim();
            ArrayList ss = new ArrayList();
            foreach (var item in IList)
            {
                int ggg = item.SupplierId;
                ss.Add(ggg);
            }
            var query = from c in dcSupplier.tbl_suppliers
                        where !ss.ToArray().Contains(c.supplier_id) && c.title.StartsWith(strKeyAphabet)
                        select c;

            return MappingObjectFromDataContextCollection(query);
        }

        /// <summary>
        /// get List of Supplier By CategoryId ;Instance method ; Returns List Collection
        /// </summary>
        /// <param name="CatId"></param>
        /// <returns></returns>
        public List<object> getListSupplierByCat_Id(int CatId)
        {
            var result = from sp in dcSupplier.tbl_suppliers
                         where sp.cat_id == CatId
                         select sp;

            return MappingObjectFromDataContextCollection(result);
        }

        /// <summary>
        /// get Supplier By Id ; Instance Method ; Return instance of Supplier
        /// </summary>
        /// <param name="SuppleierId"></param>
        /// <returns></returns>
        public Supplier getSupplierById(int intSuppleierId)
        {
            Supplier clSupplier = new Supplier();
            var result = dcSupplier.tbl_suppliers.SingleOrDefault(sp => sp.supplier_id == intSuppleierId);
           
            clSupplier = (Supplier)MappingObjectFromDataContext(result);
            return clSupplier;
        }

        /// <summary>
        /// Insert new Supplier ; Instance Method ; Return Identity Id
        /// </summary>
        /// <param name="clSupplier"></param>
        /// <returns></returns>
        public int insertNewSupplier(Supplier clSupplier)
        {
            //try
            //{
                var varInsert = new tbl_supplier
                {
                    supplier_id = clSupplier.SupplierId,
                    cat_id = clSupplier.CategoryId,
                    payment_type_id = clSupplier.PaymentTypeId,
                    title = clSupplier.SupplierTitle,
                    title_common = clSupplier.SupplierTitleCommon,
                    title_company = clSupplier.SupplerTitleCompany,
                    address = clSupplier.Address,
                    address_office = clSupplier.AddressOffice,
                    comment = clSupplier.Comment,
                    tax_vat = clSupplier.TaxVat,
                    tax_service = clSupplier.TaxService,
                    tax_local = clSupplier.TaxLocal,
                    status = clSupplier.Status
                };

                dcSupplier.tbl_suppliers.InsertOnSubmit(varInsert);
                dcSupplier.SubmitChanges();
                
                int ret = varInsert.supplier_id;

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_detail, StaffLogActionType.Insert, StaffLogSection.Supplier,
                    varInsert.supplier_id, "tbl_supplier", "cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status", "supplier_id", varInsert.supplier_id);
                //========================================================================================================================================================
                return ret;

                
            //}
            //catch
            //{
            //    int ret = 0;
            //    return ret;
            //}
        }

        /// <summary>
        /// Update Supplier ; Instance Method ; Return Bool IsUpdated
        /// </summary>
        /// <param name="clSupplier"></param>
        /// <returns></returns>
        public bool updateSupplier(Supplier clSupplier)
        {
            //HttpContext.Current.Response.Write(clSupplier.SupplierId);
            //HttpContext.Current.Response.End();
                var varUpdate = dcSupplier.tbl_suppliers.SingleOrDefault(sp => sp.supplier_id == clSupplier.SupplierId);
                //#Staff_Activity_Log================================================================================================ STEP 1 ==
                ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(varUpdate.cat_id, varUpdate.payment_type_id, varUpdate.title, varUpdate.title_common, varUpdate.title_company, varUpdate.address, varUpdate.address_office,
                    varUpdate.comment, varUpdate.tax_vat, varUpdate.tax_service, varUpdate.tax_local, varUpdate.status);
                //============================================================================================================================
                varUpdate.cat_id = clSupplier.CategoryId;
                varUpdate.payment_type_id = clSupplier.PaymentTypeId;
                varUpdate.title = clSupplier.SupplierTitle;
                varUpdate.title_common = clSupplier.SupplierTitleCommon;
                varUpdate.title_company = clSupplier.SupplerTitleCompany;
                varUpdate.address = clSupplier.Address;
                varUpdate.address_office = clSupplier.AddressOffice;
                varUpdate.comment = clSupplier.Comment;
                varUpdate.tax_vat = clSupplier.TaxVat;
                varUpdate.tax_service = clSupplier.TaxService;
                varUpdate.tax_local = clSupplier.TaxLocal ;
                varUpdate.status = clSupplier.Status;

                dcSupplier.SubmitChanges();
                bool ret = true;
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_detail, StaffLogActionType.Update, StaffLogSection.Supplier, clSupplier.SupplierId,
                    "tbl_supplier", "cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status", arroldValue, "supplier_id", clSupplier.SupplierId);
                //==================================================================================================================== COMPLETED ========
                return ret;
          
        }

        public bool UpdateSupplierSurfix(short shrSupplierId, string strSurfix)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier", "suffix_user_extranet", "supplier_id", shrSupplierId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier SET suffix_user_extranet=@suffix_user_extranet WHERE supplier_id=@supplier_id",cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@suffix_user_extranet", SqlDbType.VarChar).Value = strSurfix;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_detail, StaffLogActionType.Update, StaffLogSection.Supplier, shrSupplierId,
                "tbl_supplier", "suffix_user_extranet", arroldValue, "supplier_id", shrSupplierId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        //===============================================================================================
        //================================= Supplier Category ===========================================


        /// <summary>
        /// get All Supplier Category; Static Method ; Returns Dictionary of Supplier Cat
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getDictionarySupplierCat()
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            Dictionary<int, string> dicSupplierCat = new Dictionary<int, string>();

            var result = from spc in dcSupplier.tbl_supplier_categories
                         select spc;

            foreach (var item in result)
            {
                dicSupplierCat.Add(item.cat_id,item.title);
            }

            return dicSupplierCat;
        }
        /// <summary>
        /// get Supplier Category; Static method ;Returns String of Title
        /// </summary>
        /// <param name="intCat"></param>
        /// <returns></returns>
        public static string getSupplierCategoryById(int intCat)
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var result = dcSupplier.tbl_supplier_categories.SingleOrDefault(spc => spc.cat_id == intCat);

            return result.title;

        }

        /// <summary>
        /// Insert New SupplierCategory; Static Method ; Return int Identity of Cat_id
        /// </summary>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static int insertNewSuppliercategory(string strTitle)
        {
            
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var varInsert = new tbl_supplier_category { cat_id = 0, title = strTitle };

            dcSupplier.tbl_supplier_categories.InsertOnSubmit(varInsert);
            dcSupplier.SubmitChanges();

            int ret = varInsert.cat_id;
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_detail, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_supplier_category", "title", "cat_id", varInsert.cat_id);
            //========================================================================================================================================================
            return ret;
            

            
        }

        /// <summary>
        /// Update SupplierCategory; Static Method ; Return bool IsUpdated;
        /// </summary>
        /// <param name="intCat"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool updateSupplierCat(int intCat, string strTitle)
        {
            try
            {
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                var varUpdate = dcSupplier.tbl_supplier_categories.SingleOrDefault(spc => spc.cat_id == intCat);
                //#Staff_Activity_Log================================================================================================ STEP 1 ==
                ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(varUpdate.title);
                //============================================================================================================================
                varUpdate.title = strTitle;

                dcSupplier.SubmitChanges();

                bool ret = true;
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_detail, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                    "tbl_supplier_category", "title", arroldValue, "cat_id", intCat);
                //==================================================================================================================== COMPLETED ========
                return ret;
            }
            catch
            {
                bool ret = false;
                return ret;
            }

        }

    }
}