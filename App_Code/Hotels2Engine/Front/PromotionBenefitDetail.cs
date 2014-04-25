using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PromotionBenefitDetail
/// </summary>
public class PromotionBenefitDetail
{
    public int PromotionID { get; set; }
    public byte DayDiscountStart { get; set; }
    public byte DayDiscountNum { get; set; }
    public decimal Discount { get; set; }
    public byte PromotionType { get; set; }

	public PromotionBenefitDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
//    public decimal GetPriceBenefit()
//    { 
    
//    }
}