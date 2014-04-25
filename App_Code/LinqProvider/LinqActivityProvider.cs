using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.LinqProvider.Supplier;

/// <summary>
/// Summary description for LinqActivityProvider
/// </summary>

namespace Hotels2thailand
{
    public static class LinqActivityProvider 
    {
        
        public static void Hotels2SubmitChanged(this LinqStaffDataContext dc, object objChange, object objcurrent)
        {
            Type typObj = objChange.GetType();
            Type typCurrrentObj = objcurrent.GetType();
            int count = 0;
            foreach (PropertyInfo propItem in typCurrrentObj.GetProperties())
            {
                if (propItem.CanWrite)
                {
                    PropertyInfo objFocus = typObj.GetProperties()[count];
                    HttpContext.Current.Response.Write(propItem.GetValue(objcurrent, null) + "---");
                }
                count = count + 1;
            }
            HttpContext.Current.Response.Write("<br/>");
            dc.SubmitChanges();
            int count2 = 0;
            foreach (PropertyInfo propItem in typCurrrentObj.GetProperties())
            {
                if (propItem.CanWrite)
                {
                    PropertyInfo objFocus = typObj.GetProperties()[count2];
                    HttpContext.Current.Response.Write(objFocus.GetValue(objChange, null) + "+++");
                }
                count2 = count2 + 1;
            }
            HttpContext.Current.Response.End();
        }

        public static void Hotels2SubmitChanged(this LinqProductionDataContext dc)
        {
            dc.SubmitChanges();
        }

        public static void Hotels2SubmitChanged(this LinqSupplierDataContext dc, object objChange)
        {
            Type typObj = objChange.GetType();

            foreach (PropertyInfo propItem in typObj.GetProperties())
            {
                if (propItem.CanWrite)
                {
                    HttpContext.Current.Response.Write(propItem.GetValue(objChange, null) + "---");
                }
            }

            dc.SubmitChanges();

            foreach (PropertyInfo propItem in typObj.GetProperties())
            {
                if (propItem.CanWrite)
                {
                    HttpContext.Current.Response.Write(propItem.GetValue(objChange, null) + "+++");
                }
            }
            HttpContext.Current.Response.End();
        }
        

    }
}