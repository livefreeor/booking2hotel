using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Linq;
using System.Web;
using System.Reflection;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Suppliers;



/// <summary>
/// Summary description for Staff
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public partial class StaffProduct_Extra : Hotels2BaseClass
    {

        public int ProductID { get; set; }
        public short SupplierId { get; set; }
        public short StaffID { get; set; }
        public string ProductTitle { get; set; }



        public List<object> getProductByStaffId(short StaffID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.product_id,p.supplier_price,sp.staff_id,p.title ");
                query.Append(" FROM tbl_staff_product sp, tbl_product p WHERE p.product_id=sp.product_id AND sp.staff_id =@staff_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = StaffID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public short GetSupplierIdByProductId(int intProductId)
        {
            short shrResult = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_price FROM tbl_product WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();


                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return shrResult = (short)reader[0];

            }
            return shrResult;
        }
        public List<object> getProductByStaffId(short StaffID, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.product_id,p.supplier_price,sp.staff_id,pc.title ");
                query.Append(" FROM tbl_staff_product sp, tbl_product p , tbl_product_content pc WHERE p.product_id=sp.product_id AND p.product_id=pc.product_id AND pc.lang_id=@lang_id AND  sp.staff_id =@staff_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = StaffID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public StaffProduct_Extra GetProductTopDetaultByStaffId(short shrStaffId)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT TOP 1  sp.product_id,p.supplier_price,sp.staff_id,p.title  FROM tbl_staff_product sp, tbl_product p WHERE p.product_id=sp.product_id AND  sp.staff_id =@staff_id ORDER BY product_id DESC");
                
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (StaffProduct_Extra)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }
        public List<object> getProductExtraByProductId(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.product_id,p.supplier_price,sp.staff_id,p.title ");
                query.Append(" FROM tbl_staff_product sp, tbl_product p WHERE p.product_id=sp.product_id AND sp.staff_id IN");
                query.Append(" (SELECT st.staff_id FROM tbl_staff st , tbl_staff_product sp, tbl_staff_authorize sta");
                query.Append(" WHERE st.staff_id = sp.staff_id AND st.staff_id = sta.staff_id AND st.cat_id = 6 ");
                query.Append(" AND sta.authorize_id = 1 AND sp.product_id = @product_id)");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.SmallInt).Value = ProductID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public bool DeleteStaffProductByStaffId(short shrStaffId,int intProduct)
        {
            int ret = 0;
            //=== STAFF ACTIVITY =====================================================================================================================================
            IList<object[]> Ilistobj = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_staff_product", "staff_id,product_id", shrStaffId, intProduct);
            //========================================================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_staff_product WHERE staff_id=@staff_id AND product_id=@product_id",cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Staff, StaffLogActionType.Delete, StaffLogSection.NULL, null,
                "tbl_staff_product", Ilistobj, "staff_id,product_id", shrStaffId, intProduct);
            //========================================================================================================================================================
            return (ret == 1);
        }
        public bool DeleteStaffProductByStaffIdStaffMange(short shrStaffId, int intProduct)
        {
            int ret = 0;

            int count = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                SqlCommand cmdchk = new SqlCommand("SELECT COUNT(*) FROM tbl_staff_product WHERE staff_id=@staff_id AND product_id = @product_id", cn);
                cmdchk.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;
                cmdchk.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct;

                count = (int)ExecuteScalar(cmdchk);
                //IDataReader reader = ExecuteReader(cmdchk);
                
                //if (reader.Read())
                //{
                //    count = count + 1;
                //}

                //reader.Close();

                if (count > 0)
                {

                    //=== STAFF ACTIVITY =====================================================================================================================================
                    IList<object[]> Ilistobj = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_staff_product", "staff_id,product_id", shrStaffId, intProduct);
                    //========================================================================================================================================================

                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_staff_product WHERE staff_id=@staff_id AND product_id=@product_id", cn);
                    cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;
                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct;
                    ret = ExecuteNonQuery(cmd);

                    //=== STAFF ACTIVITY =====================================================================================================================================
                    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Staff, StaffLogActionType.Delete, StaffLogSection.NULL, null,
                        "tbl_staff_product", Ilistobj, "staff_id,product_id", shrStaffId, intProduct);
                    //========================================================================================================================================================
                }
            }
            
            return (ret == 1);
        }
        public int InsertStaffProduct(short staff_id, int intProduct_id)
        {
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_product (staff_id,product_id) VALUES(@staff_id,@product_id)", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = staff_id;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_staff_product", "staff_id,product_id", "staff_id,product_id", staff_id, intProduct_id);
            //========================================================================================================================================================

            return ret;
        }

        public int InsertStaffProductStaffManage(short staff_id, int intProduct_id)
        {
            int ret = 0;
            int count = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                SqlCommand cmdchk = new SqlCommand("SELECT COUNT(*) FROM tbl_staff_product WHERE staff_id=@staff_id AND product_id = @product_id",cn);
                cmdchk.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = staff_id;
                cmdchk.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                count = (int)ExecuteScalar(cmdchk);

                //IDataReader reader = ExecuteReader(cmdchk);
                
                //if (reader.Read())
                //{
                //    count = count + 1;
                //}
                //reader.Close();

                //HttpContext.Current.Response.Write(count);
                //HttpContext.Current.Response.Write(intProduct_id+ "total:" + count);
                //HttpContext.Current.Response.End();
                if (count == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_product (staff_id,product_id) VALUES(@staff_id,@product_id)", cn);
                    cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = staff_id;
                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;

                    ret = ExecuteNonQuery(cmd);

                    //HttpContext.Current.Response.Write(staff_id + "----" + intProduct_id);
                    //HttpContext.Current.Response.End();
                    //=== STAFF ACTIVITY =====================================================================================================================================
                    StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                        null, "tbl_staff_product", "staff_id,product_id", "staff_id,product_id", staff_id, intProduct_id);
                    //========================================================================================================================================================
                }
                

               
            }

           

            return ret;
        }
    }
}