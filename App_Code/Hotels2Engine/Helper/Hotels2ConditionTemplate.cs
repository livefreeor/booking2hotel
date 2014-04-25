using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

/// <summary>
/// Summary description for Hotels2ConditionTemplate
/// </summary>
/// 
namespace Hotels2thailand
{
    public class Hotels2ConditionTemplate
    {
        public Hotels2ConditionTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static string[] _condition = new string[] { 
         " Refundable","Non refundable"};
        private static string[] _condition_th = new string[] { 
         "สามารถคืนเงิน","ไม่สามารถคืนเงิน"};
        private static string[] _empty_Lang = new string[] { 
         "Please select one..."};

        public static StringCollection Getcondition(int intLangId)
        {

            StringCollection condition = new StringCollection();
            
            switch (intLangId)
            {
                case 1:
                    condition.AddRange(_condition);
                    break;
                case 2:
                    condition.AddRange(_condition_th);
                    break;
                default:
                    condition.AddRange(_empty_Lang);
                    break;
            }
            return condition;
        }
    }
}