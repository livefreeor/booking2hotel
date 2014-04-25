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
    public class OptionPrice
    {
        public decimal Price { get; set; }
        public decimal PriceExcludeABF { get; set; }
        public decimal PriceOwn { get; set; }
        public decimal PriceRack { get; set; }
        public decimal PriceDisplay { get; set; }
        public IList<OptionDayPrice> iListPricePerDay { get; set; }

        public OptionPrice()
        {
            Price = 0;
            PriceExcludeABF = 0;
            PriceOwn = 0;
            PriceRack = 0;
            PriceDisplay = 0;
        }
    }
}