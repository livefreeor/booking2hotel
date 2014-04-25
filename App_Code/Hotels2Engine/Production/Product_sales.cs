using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;


/// <summary>
/// Summary description for Product_sales
/// </summary>
/// 
namespace Hotels2thailand.Production
{

    public class Sales_Com_type : Hotels2BaseClass
    {
        public byte ComTypeID { get; set; }
        public string Comtitle { get; set; }
        public Sales_Com_type()
        {
            
        }

        public IDictionary<string, string> getDicComType()
        {
            IDictionary<string, string> idic = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT commission_type_id,title FROM tbl_product_sales_commission_type",cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                    idic.Add(reader[0].ToString(), reader[1].ToString());
            }

            return idic;
        }

        
    }
    public class Product_sales:Hotels2BaseClass
    {
        public byte SaleId { get; set; }
        public string SaleName { get; set; }
        public byte ComType { get; set; }
        public decimal Commission { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public bool IsmailNotice { get; set; }
        public string Comment { get;set; }

        public Product_sales()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public Product_sales getSales(byte bytSales)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_sales WHERE sale_id=@sale_id", cn);
                cmd.Parameters.Add("@sale_id", SqlDbType.TinyInt).Value = bytSales;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Product_sales)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public IList<object> getSaleList()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_sales WHERE sale_id <> 2 ORDER BY sale_name ", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public bool UpdateSales(Product_sales sales)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_sales SET sale_name=@sale_name, commission_type_id=@commission_type_id,commission=@commission, phone=@phone, fax=@fax, email=@email, comment=@comment WHERE sale_id=@sale_id", cn);
                cmd.Parameters.Add("@sale_name",SqlDbType.NChar).Value = sales.SaleName;
                cmd.Parameters.Add("@commission_type_id", SqlDbType.TinyInt).Value = sales.ComType;
                cmd.Parameters.Add("@commission", SqlDbType.SmallMoney).Value = sales.Commission;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = sales.Phone;
                cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = sales.Fax;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = sales.Email;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = sales.Comment;
                cmd.Parameters.Add("@sale_id", SqlDbType.TinyInt).Value = sales.SaleId;
                cn.Open();

                return (ExecuteNonQuery(cmd) == 1);
            }
        }

        public int InsertNewSales(Product_sales sales)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_sales (sale_name,commission_type_id,commission,phone,fax,email,comment) VALUES(@sale_name,@commission_type_id,@commission,@phone,@fax,@email,@comment) ; SET @sale_is = SCOUP_IDENTITY();", cn);
                cmd.Parameters.Add("@sale_name", SqlDbType.NChar).Value = sales.SaleName;
                cmd.Parameters.Add("@commission_type_id", SqlDbType.TinyInt).Value = sales.ComType;
                cmd.Parameters.Add("@commission", SqlDbType.SmallMoney).Value = sales.Commission;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = sales.Phone;
                cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = sales.Fax;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = sales.Email;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = sales.Comment;
                cmd.Parameters.Add("@sale_id", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);

                return (byte)cmd.Parameters["@sale_id"].Value;
            }
        }




    }

    
}