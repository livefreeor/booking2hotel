using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for BookingItem
/// </summary>
/// 

namespace Hotels2thailand.Booking
{
    public class BookingItemDisplay : Hotels2BaseClass
    {
       

        public int BookingItemID { get; set; }
        public int BookingID { get; set; }
        public int BookingProductID { get; set; }
        public short SupplierId { get; set; }
        public DateTime dDateStart { get; set; }
        public DateTime dDateEnd { get; set; }
        public int OptionID { get; set; }
        public short OptionCAtID { get; set; }
        public string OptionTitle { get; set; }

        public string BookingItemDetail { get; set; }

        public DateTime? OptionTimeStart { get; set; }
        public DateTime? OptionTimeEnd { get; set; }
        public DateTime? OptionTimePickup { get; set; }
        public DateTime? OptionTimeSent { get; set; }

        public int ConditionID { get; set; }
        public string ConditionTitle { get; set; }
       
        public string ConditionDetail { get; set; }
        public bool? ConditionIsAdult { get; set; }
        public byte Unit { get; set; }
        //public byte Breakfast { get; set; }
        public decimal Price { get; set; }
        public decimal PriceDisplay { get; set; }
        public decimal PriceSupplier { get; set; }
        public string ItemDetail { get; set; }
        public int? PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public string PromotionDetail { get; set; }
        public bool Status { get; set; }
        public byte ProductCat { get; set; }

        public int ProductID { get; set; }
        
        public byte BreakfastBookingItem { get; set; }
        public bool StatusAllot { get; set; }
        public bool IsExtraNet { get; set; }
        
        public byte? NumAdult { get; set; }
        public byte? NumChild { get; set; }
        public string BookingOptionTitle { get; set; }
        private byte _condition_num_adult = 0;
        private byte _condition_num_child = 0;

        public byte Condition_NumAdult
        {
            get
            {
                if (_condition_num_adult == 0)
                {
                    if (this.IsExtraNet)
                    {
                        if (this.OptionCAtID == 43 || this.OptionCAtID == 44)
                        {
                            ProductOptionCondition cCondition = new ProductOptionCondition();
                            _condition_num_adult = cCondition.getConditionById(this.ConditionID).NumAdult;
                        }
                        else
                        {
                            ProductConditionExtra cConditionEx = new ProductConditionExtra();
                            _condition_num_adult = cConditionEx.getConditionByConditionId(this.ConditionID).NumAult;
                        }
                        
                       
                    }
                    else
                    {
                        ProductOptionCondition cCondition = new ProductOptionCondition();
                        _condition_num_adult = cCondition.getConditionById(this.ConditionID).NumAdult;
                    }

                }
                return _condition_num_adult;
            }
            
        }

        public byte Condition_NumChild
        {
            get
            {
                if (_condition_num_child == 0)
                {
                    if (this.IsExtraNet)
                    {
                        if (this.OptionCAtID == 43 || this.OptionCAtID == 44)
                        {
                            ProductOptionCondition cCondition = new ProductOptionCondition();
                            _condition_num_child = cCondition.getConditionById(this.ConditionID).NumChildren;
                        }
                        else
                        {
                            ProductConditionExtra cConditionEx = new ProductConditionExtra();
                            _condition_num_child = cConditionEx.getConditionByConditionId(this.ConditionID).NumChild;
                        }
                       
                    }
                    else
                    {
                        ProductOptionCondition cCondition = new ProductOptionCondition();
                        _condition_num_child = cCondition.getConditionById(this.ConditionID).NumChildren;
                        
                    }

                }
                return _condition_num_child;
            }
            
        }
        private byte? _promotion_cat = null;
        public byte? PromotionCat 
        {
            get
            {
                if (_promotion_cat == null)
                {
                    if(this.PromotionID.HasValue)
                    {
                        if (this.IsExtraNet)
                        {
                            PromotionExxtranet cPromotionEx = new PromotionExxtranet();
                            _promotion_cat = cPromotionEx.getPromotionExtranetByPromotionId((int)this.PromotionID).ProCatId;
                        }
                        else
                        {
                            Promotion cPromotion = new Promotion();
                            _promotion_cat = cPromotion.GetPromotionById((int)this.PromotionID).ProCatId;
                        }
                    }
                    
                }
                return _promotion_cat;
            }

        }
        //public decimal TotalPriceSales { get; set; }
        //public decimal TotalPriceSupplier{ get; set; }


        // Get For OrderCenter Detail Show All for status
        public List<object> getBookingItemListByBookingProductId(int intBookingProductId, byte bytBookingLang)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out, bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail,bi.condition_isadult,  bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");

            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = @lang_id");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id ");

            query.Append(" ORDER BY bi.status DESC, op.cat_id, bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = bytBookingLang;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        /// <summary>
        /// this fucntion is Extend for Not include transfer and Extra bed 
        /// </summary>
        /// <param name="intBookingProductId"></param>
        /// <param name="bytBookingLang"></param>
        /// <returns></returns>
        public List<object> getBookingItemListByBookingProductId_NotTransfer(int intBookingProductId, byte bytBookingLang)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out, bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail,bi.condition_isadult,  bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");

            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = @lang_id");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id AND op.cat_id NOT IN (44,43,39)");

            query.Append(" ORDER BY bi.status DESC, op.cat_id, bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = bytBookingLang;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public List<object> getBookingItemListByBookingProductId(int intBookingProductId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out, bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail, bi.condition_isadult,bi.unit, bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");
            
            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b,tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = b.lang_id");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id ");
            
            query.Append(" ORDER BY bi.status DESC, op.cat_id, bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        //---------------------------------------------


        // Get For Customer Display  //Show Status = 1 Only // for Voucher, Booking print, Slip 

        public List<object> getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(int intBookingProductId, string CatId, byte BookingLangId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail, bi.condition_isadult,bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");
            
            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = @lang_id ");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id AND op.cat_id " + CatId);
            query.Append(" AND bi.status = 1 AND bp.status = 1 ");
            query.Append(" ORDER BY bi.status DESC, op.cat_id,  bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = BookingLangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> getBookingItemListByBookingProductId_ToCustomerDisplay(int intBookingProductId, byte BookingLangId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail, bi.condition_isadult,bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");

            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = @lang_id ");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id");
            query.Append(" AND bi.status = 1 AND bp.status = 1 ");
            query.Append(" ORDER BY bi.status DESC, op.cat_id,  bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = BookingLangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(int intBookingProductId, string CatId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail,bi.condition_isadult, bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");
            
            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = b.lang_id ");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id AND op.cat_id " + CatId);
            query.Append(" AND bi.status = 1 AND bp.status = 1 ");
            query.Append(" ORDER BY bi.status DESC, op.cat_id,  bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        //-------------------------------------------------------


        // Get For Extranet [tbl_product_option_condition_extranet] //Show Status = 1
        public BookingItemDisplay getBookingItemListByBookingProductIdAndCatId_ToExtranet(int intBookingProductId, string CatId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1 bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail,bi.condition_isadult, bi.unit,  bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");
            
            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id  AND b.booking_id = bi.booking_id AND opcon.lang_id = b.lang_id ");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_product_id = @booking_product_id AND opcon.option_id = op.option_id AND op.cat_id IN (" + CatId +")");
            query.Append(" AND bi.status = 1 AND bp.status = 1");
            query.Append(" ORDER BY bi.status DESC, op.cat_id,  bi.booking_item_id DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return (BookingItemDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }
        //---------------------------------------------
        public BookingItemDisplay getBookingItemByBookingItemId(int intBookingItemId, byte bytLangId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail,bi.condition_isadult,  bi.unit, bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");

            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b, tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = @langId");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_item_id = @booking_item_id AND opcon.option_id = op.option_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intBookingItemId;
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingItemDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }

        public BookingItemDisplay getBookingItemByBookingItemId(int intBookingItemId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bi.booking_item_id, bi.booking_id, bi.booking_product_id, bp.supplier_id, bp.date_time_check_in, bp.date_time_check_out,bi.option_id, op.cat_id, opcon.title, bi.detail,");
            query.Append(" op.time_start,op.time_end,op.time_pickup, op.time_sent,");

            query.Append(" bi.condition_id, bi.condition_title, bi.condition_detail, bi.condition_isadult, bi.unit, bi.price,");
            query.Append(" bi.price_display, bi.price_supplier, bi.detail,bi.promotion_id,bi.promotion_title,bi.promotion_detail,");
            query.Append(" bi.status ,p.cat_id,p.product_id,");
            query.Append(" bi.breakfast, bi.status_use_allotment, b.is_extranet, bi.num_adult, bi.num_children, bi.option_title");
            
            query.Append(" FROM tbl_booking_item bi, tbl_product_option op, tbl_booking_product bp, tbl_product p,tbl_booking b,  tbl_product_option_content opcon");
            query.Append(" WHERE bi.option_id = op.option_id AND bp.booking_product_id = bi.booking_product_id AND b.booking_id = bi.booking_id AND opcon.lang_id = b.lang_id");
            query.Append(" AND p.product_id = bp.product_id AND bi.booking_item_id = @booking_item_id AND opcon.option_id = op.option_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intBookingItemId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingItemDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;
                    
            }
        }


        public bool UpdateBookingItem(int BookingItemId, decimal decPrice, decimal decPriceSup, byte bytUnit, bool bolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item SET price=@price, price_supplier=@price_supplier, unit=@unit , status=@status WHERE booking_item_id=@booking_item_id", cn);
                cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = BookingItemId;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = decPrice;
                cmd.Parameters.Add("@price_supplier", SqlDbType.Money).Value = decPriceSup;
                cmd.Parameters.Add("@unit", SqlDbType.TinyInt).Value = bytUnit;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
               
            }
        }

        public string GetBookingTransferDetail (int intBookingId)
        {
            string Detail = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 detail FROM tbl_booking_item WHERE booking_id = @booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    Detail = reader[0].ToString();

                return Detail;    
            }
        }
        public bool UpdateBookingItemTransferDetail(int BookingId, string strDetail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item SET detail=@detail WHERE booking_id=@booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = BookingId;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;

                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }
        public bool UpdateBookingItemDetail(int BookingItemId, string strDetail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item SET detail=@detail WHERE booking_item_id=@booking_item_id", cn);
                cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = BookingItemId;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);

            }
        }

        public string[] getBookingProductNoteByBookingProductId(int intBookingProductId)
        {
            //
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT bp.product_id, p.comment FROM tbl_booking_product bp, tbl_product p WHERE p.product_id = bp.product_id AND bp.booking_product_id = @booking_product_id", cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    string[] arrString = { reader[0].ToString(), reader[1].ToString() };
                    return arrString;
                }
                else
                {
                    return null;
                }
                      
               
            }
        }

        public string[] getBookingProductNoteByBookingId(int intBookingId)
        {
            //
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT bp.product_id, p.comment FROM tbl_booking_product bp, tbl_product p WHERE p.product_id = bp.product_id AND bp.booking_id = @booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    string[] arrString = { reader[0].ToString(), reader[1].ToString() };
                    return arrString;
                }
                else
                {
                    return null;
                }


            }
        }

    }
}