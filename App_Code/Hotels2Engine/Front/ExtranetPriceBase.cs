using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for ExtranetPriceBase
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ExtranetPriceBase:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public short SupplierPrice { get; set; }
        public string ProductTitle { get; set; }
        public int ConditionID { get; set; }
        public string ConditionTitle { get; set; }
        public int OptionID { get; set; }
        public string OptionDetail { get; set; }
        public string OptionTitle { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte NumExtra { get; set; }
        public DateTime DatePrice { get; set; }
        public decimal Price { get; set; }
        public short OptionCategoryID { get; set; }
        public byte IsBreakfast { get; set; }
        public bool IsRoomShow { get; set; }
        public byte Breakfast { get; set; }
        public string OptionPicture { get; set; }
        private string conditionGroup = string.Empty;
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }

        public ExtranetPriceBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<ExtranetPriceBase> GetPriceBase(int ProductID, int OptionCateID, int ProductTransferID, DateTime DateStart, DateTime DateEnd)
        {

            string condition = string.Empty;
            //string sqlCommand = "select p.product_id,p.product_code,p.supplier_price,pc.title as product_title,poc_ex.condition_id,poc_ex.title as condition_title,po.option_id,poct.title as option_title,poc_ex.num_adult,poc_ex.num_children,poc_ex.num_extra,pocp_ex.date_price,pocp_ex.price,po.cat_id,poc_ex.breakfast,po.isshow,poct.detail as option_detail";
            //sqlCommand = sqlCommand+" from tbl_product p,tbl_product_option po,tbl_product_option_condition_extra_net poc_ex,tbl_product_option_condition_cancel_extra_net pocc_ex,tbl_product_option_condition_price_extranet pocp_ex,tbl_product_option_supplier pos,tbl_product_content pc,tbl_product_option_content poct";
            //sqlCommand = sqlCommand+" where p.product_id=po.product_id and po.option_id=poc_ex.option_id and poc_ex.condition_id=pocc_ex.condition_id and pocc_ex.cancel_id=pocp_ex.cancel_id";
            //sqlCommand = sqlCommand+" and p.supplier_price=pos.supplier_id and pos.option_id=po.option_id";
            //sqlCommand = sqlCommand+" and p.product_id=pc.product_id and po.option_id=poct.option_id and pc.lang_id=1 and poct.lang_id=1 and p.product_id="+ProductID+" and po.cat_id=";
            //sqlCommand = sqlCommand+" and poc_ex.status=1 and po.status=1 and pocp_ex.date_price between '2011-07-25' and '2011-07-28'";
            //sqlCommand = sqlCommand+" order by po.priority asc,po.option_id asc,poc_ex.priority asc,poc_ex.condition_id asc,pocp_ex.date_price asc";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "fr_extra_net_base_price2 '" + ProductID + "','" + OptionCateID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString()+",'"+_langID+"'";
                //HttpContext.Current.Response.Write(sqlCommand);
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ExtranetPriceBase> PriceList = new List<ExtranetPriceBase>();

                int conditionTmp = 0;
                conditionGroup = "";
                string optionTitle = string.Empty;
                while (reader.Read())
                {
                    if (_langID == 1)
                    {
                        optionTitle = reader["option_title"].ToString();
                    }
                    else
                    {
                        optionTitle = reader["second_lang"].ToString();
                        if (string.IsNullOrEmpty(optionTitle))
                        {
                            optionTitle = reader["option_title"].ToString();
                        }
                    }
                    PriceList.Add(new ExtranetPriceBase
                    {
                        ProductID = (int)reader["product_id"],
                        ProductCode = reader["product_code"].ToString(),
                        SupplierPrice = (short)reader["supplier_price"],
                        ProductTitle = reader["product_title"].ToString(),
                        ConditionID = (int)reader["condition_id"],
                        ConditionTitle = reader["condition_title"].ToString(),
                        OptionID = (int)reader["option_id"],
                        OptionDetail = reader["option_detail"].ToString(),
                        OptionTitle = optionTitle,
                        NumAdult = (byte)reader["num_adult"],
                        NumChild = (byte)reader["num_children"],
                        NumExtra = (byte)reader["num_extra"],
                        DatePrice = (DateTime)reader["date_price"],
                        Price = (decimal)reader["price"],
                        OptionCategoryID = (short)reader["cat_id"],
                        OptionPicture=reader["picture"].ToString(),
                        IsBreakfast = (byte)reader["breakfast"],
                        IsRoomShow = (bool)reader["isshow"],
                        Breakfast = (byte)reader["breakfast"]
                    });

                    if ((int)reader["condition_id"] != conditionTmp)
                    {
                        conditionGroup = conditionGroup + ((int)reader["condition_id"]).ToString() + ",";
                        //HttpContext.Current.Response.Write(((int)reader["condition_id"]).ToString() + "<br>");
                    }

                    conditionTmp = (int)reader["condition_id"];

                }
                return PriceList;
            }

            
        }

        public string GetConditionGroup()
        {
            return this.conditionGroup;
        }
    }
}