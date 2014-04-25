using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Hotels2DateTime
/// </summary>
/// 
namespace Hotels2thailand
{
    /// <summary>
    /// #,###.##
    /// </summary>
    public static class Hotels2Number
    {
        public static string Hotels2Currency(this decimal Money)
        {
            return Money.ToString("#,##0.00");
        }
    }
}