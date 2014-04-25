using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for RoomDetail
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class RoomDetail:Hotels2BaseClass
    {
        public int OptionID { get; set; }
        public int ProductID { get; set; }
        public string Title { get; set; }
        public double Size { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
        public RoomDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<RoomDetail> LoadOptionByProductID(int ProductID,byte langID)
        {


            string roomTitle = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select po.option_id,poc.title,";
                sqlCommand = sqlCommand + " (";
                sqlCommand = sqlCommand + " select top 1 spoc.title from tbl_product_option_content spoc where spoc.option_id=po.option_id and spoc.lang_id=2";
                sqlCommand = sqlCommand + " ) as second_lang,";
                sqlCommand = sqlCommand + " isnull(po.size,'0') as size,po.product_id,poc.detail,po.status";
                sqlCommand = sqlCommand + " from tbl_product_option po,tbl_product_option_content poc";
                sqlCommand = sqlCommand + " where po.option_id=poc.option_id";
                sqlCommand = sqlCommand + " and poc.lang_id=1 and po.product_id=" + ProductID + " and po.cat_id=38 and po.status=1";
                sqlCommand = sqlCommand + " order by po.priority asc";
                //HttpContext.Current.Response.Write(sqlCommand+"<br>");
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<RoomDetail> result = new List<RoomDetail>();

                while (reader.Read())
                {
                    if (langID==1)
                    {
                        roomTitle = reader["title"].ToString();
                    }else{
                        roomTitle = reader["second_lang"].ToString();
                        if(string.IsNullOrEmpty(roomTitle))
                        {
                            roomTitle = reader["title"].ToString();
                        }
                    }
                    result.Add(new RoomDetail
                    {
                        OptionID = (int)reader["option_id"],
                        ProductID = (int)reader["product_id"],
                        Title = roomTitle,
                        Size = (double)reader["size"],
                        Detail = reader["detail"].ToString(),
                        Status = (bool)reader["status"]
                    });
                }
                return result;
            }
            
        }

        
    }
}