using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for GatewayMethod
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class GatewayMethod2
    {

        private byte kbankID = 3;
        private byte bblID = 6;
        private List<byte> gatewayList;
        private int productID = 0;

        public GatewayMethod2(int ProductID)
        {
            productID = ProductID;
        }

        public byte GetGateway()
        {
            byte gatewayActiveID = 3;



            DataConnect objConn = new DataConnect();


            string sqlCommand = "select gateway_id from tbl_gateway where gateway_active=1";

            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            gatewayList = new List<byte>();
            while (reader.Read())
            {
                gatewayList.Add((byte)reader["gateway_id"]);
            }

            if (CheckHighRiskCountry())
            {
                if (CheckSupplierVatInclude(productID))
                {
                    HttpContext.Current.Response.Write("Vat Include<br/>");
                    gatewayActiveID = getGatewayBySystem();
                }
                else
                {
                    HttpContext.Current.Response.Write("Vat Not Include<br/>");
                    gatewayActiveID = getGatewayAccept(bblID);
                }
            }
            else
            {
                HttpContext.Current.Response.Write("High Risk Country<br/>");
                gatewayActiveID = getGatewayAccept(kbankID);
            }



            return gatewayActiveID;

        }

        private bool checkDateTimeBetween(DateTime dateStart, DateTime dateEnd, DateTime dateCheck)
        {
            bool result = false;
            if (dateCheck.CompareTo(dateStart) >= 0 && dateCheck.CompareTo(dateEnd) <= 0)
            {
                result = true;
            }
            return result;
        }

        private byte getGatewayBySystem()
        {

            byte result = 3;
            string[] dayTitle = { "sun", "mon", "tue", "wed", "thu", "fri", "sat" };
            DateTime dateCurrent = DateTime.Now.Hotels2ThaiDateTime();
            
            DataConnect objConn = new DataConnect();
            string sqlCommand = "select gateway_id,time_start,time_end,gateway_active";
            sqlCommand = sqlCommand + " from tbl_gateway";
            sqlCommand = sqlCommand + " where day_" + dayTitle[(byte)dateCurrent.DayOfWeek] + "=1 and gateway_active=1 and status=1";

            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            int hourStart = 0;
            int hourEnd = 0;

            DateTime dateCheckStart = new DateTime();
            DateTime dateCheckEnd = new DateTime();


            while (reader.Read())
            {
                hourStart = (byte)reader["time_start"];
                hourEnd = (byte)reader["time_end"];
                result = (byte)reader["gateway_id"];

                if (hourStart == hourEnd)
                {
                    break;
                }
                else
                {
                    if (hourStart > hourEnd)
                    {
                        hourEnd = (24 - hourStart) + hourEnd;
                        dateCheckStart = DateTime.Now.Hotels2ThaiDateTime().Date.AddHours(hourStart);
                        dateCheckEnd = dateCheckStart.AddHours(hourEnd).AddMinutes(59).AddSeconds(59);
                    }
                    else {
                        hourEnd = hourEnd - hourStart;
                        dateCheckStart = DateTime.Now.Hotels2ThaiDateTime().Date.AddHours(hourStart);
                        dateCheckEnd = dateCheckStart.AddHours(hourEnd).AddMinutes(59).AddSeconds(59);
                    
                    }
                    HttpContext.Current.Response.Write("Hour Add: " + hourEnd + "<br/>");
                    

                    if (checkDateTimeBetween(dateCheckStart, dateCheckEnd, dateCurrent))
                    {
                        HttpContext.Current.Response.Write("Select Gateway by System " + dateCurrent.ToString("dd MMM yyyy hh:mm tt") + "<br/>");
                        HttpContext.Current.Response.Write("Datestart " + dateCheckStart.ToString("dd MMM yyyy hh:mm tt") + "<br/>");
                        HttpContext.Current.Response.Write("Dateend " + dateCheckEnd.ToString("dd MMM yyyy hh:mm tt") + "<br/>");
                        break;
                    }
                }

            }
            return result;
        }

        private byte getGatewayAccept(byte gatewayID)
        {
            HttpContext.Current.Response.Write("Check Gateway Accept<br/>");
            byte result = gatewayID;
            bool checkGateway = false;
            foreach (byte countGateway in gatewayList)
            {
                if (countGateway == gatewayID)
                {
                    HttpContext.Current.Response.Write("Check" + countGateway + "-" + gatewayID + "<br/>");
                    checkGateway = true;
                }
            }

            if (!checkGateway)
            {
                foreach (byte countGateway in gatewayList)
                {
                    if (countGateway != gatewayID)
                    {
                        result = countGateway;
                        break;
                    }
                }
            }
            return result;
        }

        private bool CheckHighRiskCountry()
        {
            byte[] arrCountryRisk = { 32, 36, 61, 77, 80, 98, 99, 102, 108, 121, 127, 142, 154, 160, 173, 174, 214, 219, 229, 235 };
            bool result = true;
            string ipRefer = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            ipRefer = "58.61.253.41";

            string[] ipReferSplit = ipRefer.Split('.');
            string sqlCommand = "spIPtoCountry " + ipReferSplit[0] + "," + ipReferSplit[1] + "," + ipReferSplit[2] + "," + ipReferSplit[3];

            DataConnect objConn = new DataConnect();

            byte country_id = (byte)objConn.ExecuteScalar(sqlCommand);

            objConn.Close();

            for (byte countCountry = 0; countCountry < arrCountryRisk.Length; countCountry++)
            {
                if (arrCountryRisk[countCountry] == country_id)
                {
                    result = false;
                    break;
                }
            }
            return result;

        }

        private bool CheckSupplierVatInclude(int ProductID)
        {
            bool result = true;
            DataConnect objConn = new DataConnect();

            string sqlCommand = "select top 1 s.tax_vat";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_supplier s";
            sqlCommand = sqlCommand + " where p.supplier_price=s.supplier_id and p.product_id=" + ProductID;
            byte taxVat = (byte)objConn.ExecuteScalar(sqlCommand);

            if (taxVat == 0)
            {
                result = false;
            }
            return result;
        }

    }
}