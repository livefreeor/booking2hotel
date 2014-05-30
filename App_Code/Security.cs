using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace secureacceptance
{
    public static class Security
    {
        //private const String SECRET_KEY = "8c76418982c14c63a534546c5a4161262fe41a41a2fd4dd299fe9e90e9b2b5a76819e7ea1e2a4a6595ce421897aeb0319a0ad0d5ff734bb995c07bdca0f82581ab6852cb0bf945f2868a1297526268eeafffd2cbd3484ce1a194b4b7628a8cc014fe8fcc6641404a8dbc0a9a82029cf2930c9e1fe4cf45e29766d0c7fbba0dfe";

        public static String sign(IDictionary<string, string> paramsArray, String SECRET_KEY)
        {
            return sign(buildDataToSign(paramsArray), SECRET_KEY);
        }

        private static String sign(String data, String secretKey) {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
						byte[] keyByte = encoding.GetBytes(secretKey);

           //SHA256 dd = SHA256(keyByte);
           // SHA256 hmacsha256 = new SHA256(keyByte);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string,string> paramsArray) {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

	        foreach (String signedFieldName in signedFieldNames)
	        {
	             dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
	        }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign) {
            return String.Join(",", dataToSign);                         
        }
    }
}
