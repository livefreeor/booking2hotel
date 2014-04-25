using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using System.Text;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Country
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class CountryMarket:Hotels2BaseClass
    {
        public byte MarketId { get; set; }
        public Nullable<byte> CountryId { get; set; }
       
        public Nullable<byte> GroupId { get; set; }
        public bool Isexecpt { get; set; }


        //================ COUNTRY RATE MARKET ==========================
        public static List<tbl_country_rate_market> getMarketALLWithoutWorldwide()
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = dcProduct.ExecuteQuery<tbl_country_rate_market>("SELECT * FROM tbl_country_rate_market WHERE market_id <> 1");

            return Result.ToList();
        }
        public static IDictionary<byte,string> getMarketALL()
        {
            CountryMarket MarketSelect = new CountryMarket();
            IDictionary<byte, string> IdicResult = new Dictionary<byte, string>();
            using(SqlConnection cn = new SqlConnection(MarketSelect.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT market_id,title FROM tbl_country_rate_market", cn);
                cn.Open();
                IDataReader reader = MarketSelect.ExecuteReader(cmd);
                while (reader.Read())
                {
                    IdicResult.Add((byte)reader[0], reader[1].ToString());
                }
                
            }
            return IdicResult;
            
        }

        public string GetMarkettitleById(byte bytMarketId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_country_rate_market WHERE market_id=@market_id", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
                else
                {
                    return null;
                }


            }
        }

        public int InsertNewMarket(string title)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_country_rate_market (title)VALUES(@title);SET @market_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@market_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                int intMarketId = (int)cmd.Parameters["@market_id"].Value;

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_data_Martket, StaffLogActionType.Insert, StaffLogSection.NULL,
                    null, "tbl_country_rate_market", "title", "market_id", intMarketId);
                //========================================================================================================================================================
                return intMarketId;
            }

        }

        public bool UPdateMarket(byte bytGroupId, string title)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_country_rate_market", "title", "market_id", bytGroupId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_country_rate_market SET title=@title WHERE market_id=@market_id", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytGroupId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

           
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Central_data_Martket, StaffLogActionType.Update, StaffLogSection.NULL, null, "tbl_country_rate_market", "title", arroldValue, "market_id", bytGroupId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        
        //================ COUNTRY MARKET MAPPING===============================


        public static int InsertMappingCountryMarket_country(byte bytMarketId, byte bytCountryId, bool bolIsExcept)
        {
            short market_group_id = 0;
            int ret = 0;
            CountryMarket cMarket = new CountryMarket();
            using (SqlConnection cn = new SqlConnection(cMarket.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_country_market (market_id,country_id,Isexecpt)VALUES(@market_id,@country_id,@Isexecpt); SET @market_group_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountryId;
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                cmd.Parameters.Add("@Isexecpt", SqlDbType.Bit).Value = bolIsExcept;
                cmd.Parameters.Add("@market_group_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();
                ret = cMarket.ExecuteNonQuery(cmd);
                market_group_id = (short)cmd.Parameters["@market_group_id"].Value;
            }

            //LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            //int intInsert = dcProduct.ExecuteCommand("INSERT INTO tbl_country_market (market_id,country_id,Isexecpt)VALUES({0},{1},{2})", bytMarketId, bytCountryId, bolIsExcept);

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_data_Martket, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_country_market", "market_id,country_id,Isexecpt", "market_group_id", market_group_id);
            //========================================================================================================================================================
            return ret;
        }

        public static int InsertMappingCountryMarket_Group(byte bytMarketId, byte bytGroupId, bool bolIsExcept)
        {
            short market_group_id = 0;
            int ret = 0;
            CountryMarket cMarket = new CountryMarket();
            using (SqlConnection cn = new SqlConnection(cMarket.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_country_market (market_id,group_id,Isexecpt)VALUES(@market_id,@group_id,@Isexecpt); SET @market_group_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = bytGroupId;
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                cmd.Parameters.Add("@Isexecpt", SqlDbType.Bit).Value = bolIsExcept;
                cmd.Parameters.Add("@market_group_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();
                ret = cMarket.ExecuteNonQuery(cmd);
                market_group_id = (short)cmd.Parameters["@market_group_id"].Value;
            }
            //LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            //int intInsert = dcProduct.ExecuteCommand("INSERT INTO tbl_country_market (market_id,group_id,Isexecpt)VALUES({0},{1},{2})", bytMarketId, bytGroupId, bolIsExcept);

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_data_Martket, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_country_market", "market_id,group_id,Isexecpt", "market_group_id", market_group_id);
            //========================================================================================================================================================
            return ret;
        }

        public IList<object> getMarketGroupByMarketId(byte bytMarketId, bool bolIsexcept)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT market_id,country_id, group_id, Isexecpt FROM tbl_country_market WHERE market_id=@market_id AND Isexecpt=@Isexecpt", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                cmd.Parameters.Add("@Isexecpt", SqlDbType.Bit).Value = bolIsexcept;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
             }
        }
        public IList<object> getMarketGroupByMarketId(byte bytMarketId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT market_id,country_id, group_id, Isexecpt FROM tbl_country_market WHERE market_id=@market_id", cn);
                cmd.Parameters.Add("@market_id", SqlDbType.TinyInt).Value = bytMarketId;
                
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        

        public bool DeleteMargetMapping(byte bytMargetId, Nullable<byte> bytCountryId, Nullable<byte> bytGroupId)
        {
            IList<object[]> arroldValue = null;
            int ret = 0;
            //HttpContext.Current.Response.Write(bytMargetId + "---" + bytCountryId + "---" + bytGroupId);
            //HttpContext.Current.Response.End();
            string query = string.Empty;
            if (bytCountryId == null)
            {
                query = "DELETE FROM tbl_country_market WHERE market_id=" + bytMargetId + " AND country_id IS NULL AND group_id=" + bytGroupId + "";

                //#Staff_Activity_Log================================================================================================ STEP 1 ==
                arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_country_market", "market_id,country_id,group_id", bytMargetId, bytCountryId, bytGroupId);
                //============================================================================================================================
            }
            if (bytGroupId == null)
            {
                query = "DELETE FROM tbl_country_market WHERE market_id=" + bytMargetId + " AND country_id =" + bytCountryId + " AND group_id IS NULL";

                //#Staff_Activity_Log================================================================================================ STEP 1 ==
                arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_country_market", "market_id,country_id,group_id", bytMargetId, bytCountryId, bytGroupId);
                //============================================================================================================================
            }
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                 ret = ExecuteNonQuery(cmd);
                
            }

            
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Central_data_Martket, StaffLogActionType.Delete, StaffLogSection.NULL, null, "tbl_country_market", arroldValue, "market_id,country_id,group_id", bytMargetId, bytCountryId, bytGroupId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        

        //==================COUNTRY MARKET GROUP================================================

        public static IDictionary<byte, string> getMarketGroupALL()
        {
            CountryMarket MarketSelect = new CountryMarket();
            IDictionary<byte, string> IdicResult = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(MarketSelect.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT group_id, title FROM tbl_country_market_group", cn);
                cn.Open();
                IDataReader reader = MarketSelect.ExecuteReader(cmd);
                while (reader.Read())
                {
                    IdicResult.Add((byte)reader[0], reader[1].ToString());
                }

            }
            return IdicResult;

        }

        public static IDictionary<byte, string> getMarketGroupALL(string Param)
        {
            CountryMarket MarketSelect = new CountryMarket();
            IDictionary<byte, string> IdicResult = new Dictionary<byte, string>();

            string Query = string.Empty;
            if (string.IsNullOrEmpty(Param))
                Query = "SELECT group_id, title FROM tbl_country_market_group";
            else
                Query = "SELECT group_id, title FROM tbl_country_market_group WHERE group_id NOT IN (" + Param + ")";
            //HttpContext.Current.Response.Write(Query);
            //HttpContext.Current.Response.End();
            using (SqlConnection cn = new SqlConnection(MarketSelect.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, cn);
                cn.Open();
                IDataReader reader = MarketSelect.ExecuteReader(cmd);
                while (reader.Read())
                {
                    IdicResult.Add((byte)reader[0], reader[1].ToString());
                }

            }
            return IdicResult;

        }

        public string GetGroptitleById(byte bytGroupId)
        {
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_country_market_group WHERE group_id=@group_id", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = bytGroupId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
                else
                {
                    return null;
                }

                
            }
        }

        public int InsertNewMarketGroup(string title)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_country_market_group (title)VALUES(@title);SET @group_id=SCOPe_IDENTITY()", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@group_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                int GroupId = (int)cmd.Parameters["@group_id"].Value;

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_data_Martket, StaffLogActionType.Insert, StaffLogSection.NULL,
                    null, "tbl_country_market_group", "title", "group_id", GroupId);
                //========================================================================================================================================================
                return GroupId;
            }


        }

        public bool UPdateNewMarketGroup(byte bytGroupId,string title)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_country_market_group", "title", "group_id", bytGroupId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_country_market_group SET title=@title WHERE group_id=@group_id", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = bytGroupId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Central_data_Martket, StaffLogActionType.Update, StaffLogSection.NULL, null, "tbl_country_market_group", "title", arroldValue, "group_id", bytGroupId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //==================COUNTRY MARKET GROUP MAPPING================================================
        public static ArrayList getCountryMappingByGroupId(byte bytGroupId)
        {
            CountryMarket MarketSelect = new CountryMarket();
            ArrayList ArrResult = new ArrayList();
            using (SqlConnection cn = new SqlConnection(MarketSelect.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT group_id, country_id FROM tbl_country_market_group_country WHERE group_id=@group_id", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = bytGroupId;
                cn.Open();
                IDataReader reader = MarketSelect.ExecuteReader(cmd);
                while (reader.Read())
                {
                    ArrResult.Add((byte)reader[1]);
                }

            }
            return ArrResult;

        }
        public int InsertGroupMapping(byte groupId, byte countryId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_country_market_group_country (group_id,country_id)VALUES(@group_id,@country_id)", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = groupId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = countryId;
                //cmd.Parameters.Add("@group_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_data_Martket, StaffLogActionType.Insert, StaffLogSection.NULL,
                    null, "tbl_country_market_group_country", "group_id,country_id", "group_id,country_id", groupId, countryId);
                //========================================================================================================================================================
                //int GroupId = (int)cmd.Parameters["@group_id"].Value; ;SET @group_id=SCOPE_IDENTITY()
                return ret;
            }
        }

        public bool DeleteGroup(byte groupId, byte countryId)
        {

            IList<object[]> arroldValue = null;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_country_market_group_country", "group_id,country_id", groupId, countryId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_country_market_group_country WHERE group_id=@group_id AND country_id=@country_id", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = groupId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = countryId;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_country_market_group_country", "group_id,country_id", groupId, countryId);
            //============================================================================================================================

            return (ret == 1);
        }

        public static void inSertAndUpdateCountryMargetGroup(byte groupId, byte countryId, bool IsCheck)
        {
            CountryMarket MarketSelect = new CountryMarket();
            bool IsHaverec = false;
            using (SqlConnection cn = new SqlConnection(MarketSelect.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT group_id, country_id FROM tbl_country_market_group_country WHERE group_id=@group_id AND country_id=@country_id", cn);
                cmd.Parameters.Add("@group_id", SqlDbType.TinyInt).Value = groupId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = countryId;
                cn.Open();
                IDataReader reader = MarketSelect.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    IsHaverec = true;
            }
            if (IsHaverec)
            {
                if (!IsCheck)
                {
                    MarketSelect.DeleteGroup(groupId, countryId);
                }
            }
            else
            {
                if (IsCheck)
                {
                    MarketSelect.InsertGroupMapping(groupId, countryId);
                }
            }
        }
    }
}