using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public partial class Product : Hotels2BaseClass
    {
        private short _statusProcessId;
        private bool _status_active;
        private byte _point_popular;
        private bool _flag_feature;

        public int ProductID { get; set; }
        public byte ProductCategoryID { get; set; }
        public short DestinationID { get; set; }
        public short SupplierPrice { get; set; }

        private short _group_nearby;
        public short GroupNearByID
        {
            get { return _group_nearby; }
            set { _group_nearby = value; }
        }
        public short StatusID
        {
            get { return _statusProcessId; }
            set { _statusProcessId = value; }
        }
        public byte PaymentTypeID { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public Single Star { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Comment { get; set; }



        public bool IsInternetFree { get; set; }
        public bool IsHaveInterner { get; set; }

        public byte PointPopular
        {
            get { return _point_popular; }
            set { _point_popular = value; }
        }

        public bool FlagFeature
        {
            get { return _flag_feature; }
            set { _flag_feature = value; }
        }

        public bool Status
        {
            get { return _status_active; }
            set { _status_active = value; }
        }

        private string _product_phone;

        public string ProductPhone
        {
            get { return _product_phone; }
            set { _product_phone = value; }
        }

        public byte NumPic { get; set; }
        public bool IsnewPic { get; set; }
        public bool IsFav { get; set; }
        public bool IsExtranet { get; set; }
        public bool ExtranetActive { get; set; }
        public DateTime? DateSubmit { get; set; }
        public string ProductEmail { get; set; }

        private string _product_file_main = string.Empty;
        public string ProductFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_product_file_main))
                {
                    ProductContent cProductContent = new ProductContent();
                    _product_file_main = cProductContent.GetProductContentById(this.ProductID, 1).FileMain;

                }
                return _product_file_main;
            }
        }

        private string _statusTitle = string.Empty;


        public string StatusTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_statusTitle))
                {
                    _statusTitle = Hotels2thailand.Production.Status.GetStatusTitleById(this.StatusID);

                }
                return _statusTitle;
            }
        }

        private string _typeID = string.Empty;

        public string TypeID
        {
            get
            {
                if (string.IsNullOrEmpty(_typeID))
                {
                    _typeID = Hotels2thailand.Production.ProductType.GetTypeIdById(this.ProductCategoryID);

                }
                return _typeID;
            }

        }

        public string _destitle = string.Empty;
        public string DestinationTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_destitle))
                {
                    Destination clDestination = new Destination();
                    _destitle = clDestination.GetDestinationById(this.DestinationID).Title;

                }
                return _destitle;
            }
        }
        public string _des_folder_name = string.Empty;
        public string DestinationFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_des_folder_name))
                {
                    Destination clDestination = new Destination();
                    _des_folder_name = clDestination.GetDestinationById(this.DestinationID).FolderDestination;

                }
                return _des_folder_name;
            }
        }

        public string _supplier_title = string.Empty;
        public string SupplierTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_supplier_title))
                {
                    Suppliers.Supplier clSupplier = new Suppliers.Supplier();
                    _supplier_title = clSupplier.getSupplierById(this.SupplierPrice).SupplierTitle;

                }
                return _supplier_title;
            }
        }


        private string _supplier_cat_title = string.Empty;
        public string SupplierCatTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_supplier_cat_title))
                {
                    Suppliers.Supplier clSupplier = new Suppliers.Supplier();
                    _supplier_cat_title = clSupplier.getSupplierById(this.SupplierPrice).CatTitle;

                }
                return _supplier_cat_title;
            }
        }

        private byte _supplier_cat_Id = 0;
        public byte SupplierCatID
        {
            get
            {
                Suppliers.Supplier clSupplier = new Suppliers.Supplier();
                _supplier_cat_Id = clSupplier.getSupplierById(this.SupplierPrice).CategoryId;


                return _supplier_cat_Id;
            }
        }

        public Product()
        {

            //first status Process automatic Set to New Process**
            _statusProcessId = 61;
            _point_popular = 0;
            _flag_feature = false;

            //NearBy Group is not use for now
            // then  Define default value = 1 Ref from tbl_product-nearby_group
            _group_nearby = 1;
            //first status active Default to False;
            _status_active = true;
            _product_phone = null;
        }

        public string GetLatestProductCode(byte bytProductCat, short shrDesId)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 product_code FROM tbl_product WHERE  destination_id = @destination_id AND cat_id = @cat_id AND title NOT LIKE '%test%' AND title NOT LIKE '%Blue House%' AND title NOT LIKE '%Loechom%' ORDER BY product_code DESC", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;

                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    result = reader[0].ToString();
                }

            }

            return result;
        }


        public int CheckProductCode(string strPCode)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_product WHERE LOWER(product_code) =  LOWER(" + strPCode + ")", cn);

                //cmd.Parameters.Add("@product_code", SqlDbType.VarChar).Value = strPCode;

                cn.Open();
                return (int)ExecuteScalar(cmd);

            }


        }



        public int Insert(byte bytCatId, short shrDesId, short shrSupplirPrice, byte bytPaymentId, string strProductCode, string strTitle, Single decStar, string strLat, string strLong,
            string strComment, bool bolIsInternet, bool bolIsInternetFree, string strProductPhone, string strEmail)
        {
            //HttpContext.Current.Response.Write(shrSupplirPrice);
            //HttpContext.Current.Response.End();
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO tbl_product (cat_id,destination_id,supplier_price,status_id,payment_type_id,product_code,title,");
            query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,status,product_phone,date_submit,email)");

            query.Append(" VALUES(@cat_id,@destination_id,@supplier_price,@status_id,@payment_type_id,@product_code,@title,");
            query.Append(" @star,@coor_lat,@coor_long,@comment,@is_internet_free,@is_internet_have,@status,@product_phone,@date_submit, @email) ; SET @Product_id = SCOPE_IDENTITY()");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplirPrice;
                //cmd.Parameters.Add("@group_nearby_id", SqlDbType.SmallInt).Value = data.GroupNearByID;
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = 61;
                cmd.Parameters.Add("@payment_type_id", SqlDbType.TinyInt).Value = bytPaymentId;
                cmd.Parameters.Add("@product_code", SqlDbType.VarChar).Value = strProductCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@star", SqlDbType.Real).Value = decStar;
                cmd.Parameters.Add("@coor_lat", SqlDbType.VarChar).Value = strLat;
                cmd.Parameters.Add("@coor_long", SqlDbType.VarChar).Value = strLong;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
                cmd.Parameters.Add("@is_internet_free", SqlDbType.Bit).Value = bolIsInternetFree;
                cmd.Parameters.Add("@is_internet_have", SqlDbType.Bit).Value = bolIsInternet;
                //cmd.Parameters.Add("@point_popular", SqlDbType.TinyInt).Value = data.PointPopular;
                //cmd.Parameters.Add("@flag_feature", SqlDbType.Bit).Value = data.FlagFeature;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@product_phone", SqlDbType.VarChar).Value = strProductPhone;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@Product_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();

                ExecuteNonQuery(cmd);
                int Product_id = (int)cmd.Parameters["@Product_id"].Value;

                //#Staff_Activity_Log==========================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    Product_id, "tbl_product", "cat_id,destination_id,supplier_price,status_id,payment_type_id,product_code,title,star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,status,product_phone,email",
                    "product_id", Product_id);
                //============================================================================================================================


                return Product_id;
            }
        }

        public bool UpdateProductComment(int intProductId, string strComment)
        {

            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_product SET comment=@comment");
            query.Append(" WHERE product_id=@product_id");

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "comment", "product_id", intProductId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
                cmd.Parameters.Add("@Product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                    "tbl_product", "comment", arroldValue, "product_id", intProductId);
                //==================================================================================================================== COMPLETED ========
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }

        }

        public bool Update(int intProductId, short shrDesId, byte bytPaymentId, string strProductCode, string strTitle, Single decStar, string strLat, string strLong,
            string strComment, bool bolIsInternet, bool bolIsInternetFree, string strProductPhone, string strEmail)
        {

            StringBuilder query = new StringBuilder();
            query.Append("UPDATE tbl_product SET destination_id=@destination_id,payment_type_id=@payment_type_id,product_code=@product_code,title=@title,");
            query.Append(" star=@star,coor_lat=@coor_lat,coor_long=@coor_long,comment=@comment,is_internet_free=@is_internet_free,is_internet_have=@is_internet_have,product_phone=@product_phone, email=@email");
            query.Append(" WHERE product_id=@product_id");

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "destination_id,payment_type_id,product_code,title,star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,product_phone", "product_id", intProductId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                //cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = data.ProductCategoryID;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                //cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplirPrice;
                //cmd.Parameters.Add("@group_nearby_id", SqlDbType.SmallInt).Value = data.GroupNearByID;
                //cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = data.StatusID;
                cmd.Parameters.Add("@payment_type_id", SqlDbType.TinyInt).Value = bytPaymentId;
                cmd.Parameters.Add("@product_code", SqlDbType.VarChar).Value = strProductCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@star", SqlDbType.Real).Value = decStar;
                cmd.Parameters.Add("@coor_lat", SqlDbType.VarChar).Value = strLat;
                cmd.Parameters.Add("@coor_long", SqlDbType.VarChar).Value = strLong;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
                cmd.Parameters.Add("@is_internet_free", SqlDbType.Bit).Value = bolIsInternetFree;
                cmd.Parameters.Add("@is_internet_have", SqlDbType.Bit).Value = bolIsInternet;
                //cmd.Parameters.Add("@point_popular", SqlDbType.TinyInt).Value = data.PointPopular;
                //cmd.Parameters.Add("@flag_feature", SqlDbType.Bit).Value = data.FlagFeature;
                //cmd.Parameters.Add("@status", SqlDbType.Bit).Value = data.Status;
                cmd.Parameters.Add("@product_phone", SqlDbType.VarChar).Value = strProductPhone;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@Product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                    "tbl_product", "destination_id,payment_type_id,product_code,title,star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,product_phone", arroldValue, "product_id", intProductId);
                //==================================================================================================================== COMPLETED ========
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }

        }

        public bool UpdateProductNewPic(int intProductId, bool bolIsNewPic)
        {

            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "is_new_pic", "product_id", intProductId);
            //===================================================================================================================================================================================================

            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET is_new_pic=@is_new_pic WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@is_new_pic", SqlDbType.Bit).Value = bolIsNewPic;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "is_new_pic", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public bool UpdateProductConfiguration(int intProductId, short shrStatusId, bool bolFeature, bool bolStatus)
        {

            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "status_id,flag_feature,status", "product_id", intProductId);
            //===================================================================================================================================================================================================

            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET status_id=@status_id, flag_feature=@flag_feature , status=@status WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@status_id", SqlDbType.TinyInt).Value = shrStatusId;
                cmd.Parameters.Add("@flag_feature", SqlDbType.Bit).Value = bolFeature;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "status_id,flag_feature,status", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public bool UpdatestatusProcessProduct(int intProductId, short bolStatus)
        {
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "status", "product_id", intProductId);
            //===================================================================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET status_id=@status_id WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@status", SqlDbType.SmallInt).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "status", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public bool UpdateProductToExtranet(int intProductId, bool bolIsExtra)
        {
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "Isextranet", "product_id", intProductId);
            //===================================================================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET Isextranet=@Isextranet WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@Isextranet", SqlDbType.Bit).Value = bolIsExtra;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "Isextranet", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public bool UpdateSupplierPrice(int intProductId, short shrSupplierPrice)
        {
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "supplier_price", "product_id", intProductId);
            //===================================================================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET supplier_price=@supplier_price WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierPrice;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "supplier_price", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public bool UpdateSupplierFeatureAndPoint(int intProductId, bool bolFeature, byte bytPoint)
        {
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            ArrayList arrOldvalue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "flag_feature,point_popular", "product_id", intProductId);
            //===================================================================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET flag_feature=@flag_feature , point_popular=@point_popular WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@flag_feature", SqlDbType.Bit).Value = bolFeature;
                cmd.Parameters.Add("@point_popular", SqlDbType.TinyInt).Value = bytPoint;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            //=== STAFF ACTIVITY ========================================================================================================================================================FIRST STEP==============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId, "tbl_product", "flag_feature,point_popular", arrOldvalue, "product_id", intProductId);
            //===================================================================================================================================================================================================
            return (ret == 1);
        }

        public List<object> GetProductAllByCatAndDesAndLocation(byte bytProductCatId, short shrDestinationId, short shrLocationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id,p.cat_id,p.destination_id,p.supplier_price,p.group_nearby_id,p.status_id,p.payment_type_id,p.product_code,p.title,");
                query.Append(" p.star,p.coor_lat,p.coor_long,p.comment,p.is_internet_free,p.is_internet_have,p.point_popular,p.flag_feature,p.status,p.product_phone,p.num_pic,p.is_new_pic,p.is_favorite,p.Isextranet");
                query.Append(" FROM tbl_product p , tbl_product_location pl");
                query.Append(" WHERE p.product_id=pl.product_id AND p.cat_id =@cat_id AND p.destination_id=@destination_id AND pl.location_id = @location_id AND p.status= 1");
                query.Append(" ORDER BY p.point_popular DESC, p.flag_feature DESC, p.title");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCatId;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDestinationId;
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrLocationId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductAllByCatAndDesAndLocation(byte bytProductCatId, short shrDestinationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id,p.cat_id,p.destination_id,p.supplier_price,p.group_nearby_id,p.status_id,p.payment_type_id,p.product_code,p.title,");
                query.Append(" p.star,p.coor_lat,p.coor_long,p.comment,p.is_internet_free,p.is_internet_have,p.point_popular,p.flag_feature,p.status,p.product_phone,p.num_pic,p.is_new_pic,p.is_favorite,p.isextranet");
                query.Append(" FROM tbl_product p ");
                query.Append(" WHERE p.cat_id =@cat_id AND p.destination_id=@destination_id AND p.status= 1");
                query.Append(" ORDER BY p.point_popular DESC, p.flag_feature DESC, p.title");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCatId;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDestinationId;

                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetProductAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet");
                query.Append(" FROM tbl_product ORDER BY title");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }



        public List<object> GetProductListShow(byte bytProductCatId, short shrDesId, short shrStatusProcess, bool bolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet");
                query.Append(" FROM tbl_product WHERE cat_id=@cat_id AND destination_id=@destination_id AND status_id=@status_id AND status=@status");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCatId;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = shrStatusProcess;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolstatus;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }



        public Product GetProductById(int intProductId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active,date_submit,email");
                query.Append(" FROM tbl_product WHERE product_id=@product_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return (Product)MappingObjectFromDataReader(reader);
                else
                    return null;
            }

        }

        public ArrayList GetProductByDestionIdAndCatId(short shrDesId, byte bytProductCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id FROM tbl_product p ");
                query.Append(" WHERE p.destination_id=@destination_id AND p.cat_id=@cat_id AND p.status = 1");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cn.Open();
                ArrayList Product_id = new ArrayList();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Product_id.Add((int)reader[0]);
                }
                return Product_id;
            }
        }


        public ArrayList GetProductByDestionIdAndCatIdAll(short shrDesId, byte bytProductCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id FROM tbl_product p");
                query.Append(" WHERE p.destination_id=@destination_id AND p.cat_id=@cat_id AND p.status = 1");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cn.Open();
                ArrayList Product_id = new ArrayList();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    Product_id.Add((int)reader[0]);
                }
                return Product_id;
            }
        }

        public DataTable GetB2BProductAllByCatAndDes(int intProductCatId, int intDestinationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                DataTable dtProduct = new DataTable();
                StringBuilder cStringBuilder = new StringBuilder();
                cStringBuilder.Append(" select p.product_id,p.title ,d.title as DestinationTitle ");
                cStringBuilder.Append(" from tbl_product p , tbl_product_booking_engine pb, tbl_destination d ");
                cStringBuilder.Append(" where p.product_id = pb.product_id and p.destination_id = d.destination_id ");
                cStringBuilder.Append(" and pb.is_B2b = 1 and pb.cat_id = @cat_id and p.destination_id=@destination_id ");
                cStringBuilder.Append(" ORDER BY p.point_popular DESC, p.flag_feature DESC, p.title ");
                SqlCommand cmd = new SqlCommand(cStringBuilder.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = intProductCatId;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = intDestinationId;

                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtProduct);
                return dtProduct;
            }
        }

        public DataTable GetB2BProductAllByCatAndDes(int intProductCatId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                DataTable dtProduct = new DataTable();
                StringBuilder cStringBuilder = new StringBuilder();
                cStringBuilder.Append(" select p.product_id,p.title ,d.title as DestinationTitle ");
                cStringBuilder.Append(" from tbl_product p , tbl_product_booking_engine pb, tbl_destination d ");
                cStringBuilder.Append(" where p.product_id = pb.product_id and p.destination_id = d.destination_id ");
                cStringBuilder.Append(" and pb.is_B2b = 1 and pb.cat_id = @cat_id ");
                cStringBuilder.Append(" ORDER BY p.point_popular DESC, p.flag_feature DESC, p.title ");
                SqlCommand cmd = new SqlCommand(cStringBuilder.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = intProductCatId;

                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtProduct);
                return dtProduct;
            }
        }

    }
}