using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OptionPrice
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class OptionDayPrice
    {
        public DateTime DateCheck { get; set; }
        public decimal PriceBase { get; set; }
        public decimal PricePromotion { get; set; }
        public decimal PriceABF { get; set; }
        public bool IsDatePromotion { get; set; }

        public OptionDayPrice()
        {
            
        }
    }
}