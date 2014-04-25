using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceDay
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PriceDay
    {
        public int OptionID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal DateSun { get; set; }
        public decimal DateMon { get; set; }
        public decimal DateTue { get; set; }
        public decimal DateWed { get; set; }
        public decimal DateThu { get; set; }
        public decimal DateFri { get; set; }
        public decimal DateSat { get; set; }

        public PriceDay()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}