using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPolicyAdmin: Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public int PolicyID { get; set; }
        public byte PolicyCat { get; set; }
        public byte PolicyType { get; set; }
        public int ProductID { get; set; }
        public string Title { get; set; }
        public DateTime  DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Status { get; set; }

        public int Insert(ProductPolicyAdmin policy)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_policy (cat_id,type_id,product_id,title,date_start,date_end,status) VALUES (@cat_id,@type_id,@product_id,@title,@date_start,@date_end,@status); SET @policy_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = policy.PolicyCat;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = policy.PolicyType;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = policy.ProductID;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = policy.Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = policy.DateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = policy.DateEnd;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = policy.Status;
                cmd.Parameters.Add("@policy_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@policy_id"].Value;

               
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_policy, StaffLogActionType.Insert, StaffLogSection.Product,
                policy.ProductID, "tbl_product_policy", "cat_id,type_id,product_id,title,date_start,date_end,status", "policy_id", ret);
            //========================================================================================================================================================
            return ret;
            
        }

        public bool Update()
        {
            return ProductPolicyAdmin.UpdateProductPolicy(this.PolicyID, this.PolicyCat, this.PolicyType, this.ProductID, this.Title, this.DateStart, this.DateEnd, this.Status);
        }

        public static bool UpdateProductPolicy(int intPolicyId, byte bytCatId, byte bytTypId, int intProductId, string strTitle, DateTime dDateStart, DateTime dDateEnd, bool bolStatus)
        {
            ProductPolicyAdmin cPolicy = new ProductPolicyAdmin
            {
                PolicyID = intPolicyId,
                PolicyCat = bytCatId,
                PolicyType = bytTypId,
                ProductID = intPolicyId,
                Title = strTitle,
                DateStart = dDateStart,
                DateEnd = dDateEnd,
                Status = bolStatus
            };

            return cPolicy.UpdatePolicy(cPolicy);
        }


        public bool UpdatePolicy(ProductPolicyAdmin policy)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_policy", "cat_id,type_id,title,date_start,date_end,status", "policy_id", policy.PolicyID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_policy SET cat_id=@cat_id,type_id=@type_id,title=@title,date_start=@date_start,date_end=@date_end,status=@status WHERE policy_id=@policy_id", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = policy.PolicyCat;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = policy.PolicyType;
                
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = policy.Title;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = policy.DateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = policy.DateEnd;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = policy.Status;
                cmd.Parameters.Add("@policy_id",SqlDbType.Int).Value = policy.PolicyID;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_policy, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_policy", "cat_id,type_id,title,date_start,date_end,status", arroldValue, "policy_id", policy.PolicyID);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public List<object> GetProductPolicy(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT policy_id,cat_id,type_id,product_id,title,date_start,date_end,status FROM tbl_product_policy WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
               
            }
            
        }

        public List<object> GetProductPolicyByProductANDCatIDANDTypId(int ProductID, byte bytCatId, byte bytTypeId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT policy_id,cat_id,type_id,product_id,title,date_start,date_end,status FROM tbl_product_policy WHERE product_id=@product_id AND cat_id=@cat_id AND type_id=@type_id ORDER BY status DESC", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            
        }

        public List<object> GetProductPolicyByProductANDCatID(int ProductID, byte bytCatId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT policy_id,cat_id,type_id,product_id,title,date_start,date_end,status FROM tbl_product_policy WHERE product_id=@product_id AND cat_id=@cat_id ", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
          
        }

        public List<object> GetProductPolicyByProductANDCatIDANDNotDuplicatCurrent(int ProductID, byte bytCatId, int intConditionId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT policy_id,cat_id,type_id,product_id,title,date_start,date_end,status FROM tbl_product_policy WHERE product_id= @product_id AND cat_id = @cat_id AND status = 1");
                query.Append(" AND policy_id NOT IN (SELECT policy_id FROM tbl_product_option_condition_policy WHERE condition_id = @condition_id)");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            //int[] arrInt = ProductOptionCondition.GetPolicyListbyConditionId(intConditionId);
            //var policy = (from item in dcProduct.tbl_product_policies
            //             where item.product_id == ProductID && item.cat_id == bytCatId
            //              select item).Where(po => !arrInt.Contains(po.policy_id));
            //return MappingObjectFromDataContextCollection(policy);
        }

        public ProductPolicyAdmin GetPolicyByID(int Policy)
        {
            
            var result = dcProduct.tbl_product_policies.Single(pc => pc.policy_id == Policy );
            return (ProductPolicyAdmin)MappingObjectFromDataContext(result);
        }

        public List<object> GetPolicyAll()
        {
            var result = from item in dcProduct.tbl_product_policies
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }


        //==================== POLICY CONTENT LANGUAGE ==================
        public static ArrayList getPolicycontentBYIdAndLangId(int intPolicyId, byte bytLangId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = dcProduct.tbl_product_policy_contents.SingleOrDefault(plc => plc.policy_id == intPolicyId && plc.lang_id == bytLangId);
            if (Result == null)
                return null;
            else
            {
                ArrayList arryItem = new ArrayList();
                arryItem.Add(Result.title);
                arryItem.Add(Result.detail);
                return arryItem;
            }
        }

        //public static int dd(params object[] parameters)
        //{
        //    int count = parameters.Count();
        //    return 1;
        //}

        //public void dda()
        //{
        //    int ddaa = 0;
        //    string ad = "";
        //    byte ll = 5;

        //    int kk = ProductPolicy.dd(ddaa, ad,ll);
        //}

        public static int InsertNewContentLangPolicy(int intPolicyId, byte bytLangId, string strTitle, string strDetail)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            int Insert = dcProduct.ExecuteCommand("INSERT INTO tbl_product_policy_content (policy_id,lang_id,title,detail)Values({0},{1},{2},{3})", intPolicyId, bytLangId, strTitle, strDetail.Hotels2SPcharacter_removeONe());
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_policy, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_policy_content", "policy_id,lang_id,title,detail", "policy_id,lang_id", intPolicyId,bytLangId);
            //========================================================================================================================================================

            return Insert;
        }

        public static bool UpdateContentLangPolicy(int intPolicyId, byte bytLangId, string strTitle, string strDetail)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_policy_content", "title,detail", "policy_id,lang_id", intPolicyId, bytLangId);
            //============================================================================================================================

            int Update = dcProduct.ExecuteCommand("UPDATE tbl_product_policy_content SET title={0},detail={1} WHERE policy_id={2} AND lang_id={3}", strTitle, strDetail.Hotels2SPcharacter_removeONe(), intPolicyId, bytLangId);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_policy, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_policy_content", "title,detail", arroldValue, "policy_id,lang_id", intPolicyId, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (Update == 1);
            
        }

        //==================== POLICY CATEGORY ==========================

        public static Dictionary<byte, string> getPolicyCategoryall()
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            Dictionary<byte, string> dicPolicyCat = new Dictionary<byte, string>();

            var Result = from plc in dcProduct.tbl_product_policy_cats
                         select plc;
            foreach (var item in Result)
            {
                dicPolicyCat.Add(item.cat_id, item.title);
            }

            return dicPolicyCat;
        }


        //==================== POLICT TYPE ==============================

        public static Dictionary<byte, string> getPolicyType()
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            Dictionary<byte, string> dicPolicyCat = new Dictionary<byte, string>();

            var Result = from plc in dcProduct.tbl_product_policy_types
                         select plc;
            foreach (var item in Result)
            {
                dicPolicyCat.Add(item.type_id, item.title);
            }

            return dicPolicyCat;
        }

        public static Dictionary<byte, string> getPolicyTypeIsHaveRecord(int ProductID, byte bytCatId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            Dictionary<byte, string> dicPolicyCat = new Dictionary<byte, string>();

            var Result = from plc in dcProduct.tbl_product_policy_types
                         select plc;
            ProductPolicyAdmin cProductPolicy = new ProductPolicyAdmin();
            

            foreach (var item in Result)
            {
                if (cProductPolicy.GetProductPolicyByProductANDCatIDANDTypId(ProductID, bytCatId, item.type_id).Count > 0)
                {
                    dicPolicyCat.Add(item.type_id, item.title);
                }
            }

            return dicPolicyCat;
        }


        //=================== PRODUCT POLICY CANCEL ===========================

        //public static int InsertPolicycancel(int intPolicyId, byte bytDayCancel, byte bytCPercenthotel, byte bytCpercentbht, byte bytCRoomHotel, byte bytCRoombht)
        //{
        //    LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        //    int Insert = dcProduct.ExecuteCommand("INSERT INTO tbl_product_policy_cancel(policy_id,day_cancel,charge_percent_hotel,charge_percent_bht,charge_room_hotel,charge_room_bht)VALUES({0},{1},{2},{3},{4},{5})", intPolicyId, bytDayCancel, bytCPercenthotel, bytCpercentbht, bytCRoomHotel, bytCRoombht);
        //    return Insert;
        //}

        //public static bool DeletePolicyCancel(int intCancelId)
        //{
        //    LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        //    int IntDel = dcProduct.ExecuteCommand("DELETE FROM tbl_product_policy_cancel WHERE cancel_id ={0}", intCancelId);
        //    return (IntDel == 1);
        //}

        //public static List<tbl_product_policy_cancel> GetPolicyCancelByPolicyId(int intPolicyId)
        //{
        //    LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        //    var ReSult = dcProduct.ExecuteQuery<tbl_product_policy_cancel>("SELECT * FROM tbl_product_policy_cancel WHERE policy_id={0}", intPolicyId);
        //    return ReSult.ToList();
        //}
    }
}