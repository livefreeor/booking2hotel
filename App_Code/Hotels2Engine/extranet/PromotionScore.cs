using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.Front;
using Hotels2thailand.ProductOption;
using System.Text.RegularExpressions;


namespace Hotels2thailand.Production
{
    /// <summary>
    /// Summary description for PromotionBenefit
    /// </summary>
    public class PromotionScore :Hotels2BaseClass
    {
     
        public int BenefitID { get; set; }
        public int PromotionId { get; set; }
        public byte DaydiscountStart { get; set; }
        public byte DaydiscountNum { get; set; }
        public byte TypeId { get; set; }
        public decimal DiscountAmount { get; set; }
        public byte Priority { get; set; }
        public bool Status { get; set; }

        public PromotionScore()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public byte GetScore(int intProductId, DateTime dDateStart, DateTime dDateEnd, 
            byte bytPromotionType, byte IsBreakfast, decimal ABFCharge, decimal DiscountCharge,  string[] conditionCheck,
            byte byteFreeNightStay, byte byteFreeNightPay, byte bytMinnight)
        {

            byte ScoreResult = 0;
            //byte bytDatePeriodDefault = 5;
            decimal PriceBase = 1;
            //decimal UnitPrice = 0;
            int DatediffCheckinout = dDateEnd.Subtract(dDateStart).Days;
            int intCondition = int.Parse(conditionCheck[0]);
            DateTime dDateCheckOutSim = DateTime.Now;

            //if (DatediffCheckinout < 5)
            //    bytDatePeriodDefault = (byte)DatediffCheckinout;

            //if (byteFreeNightStay > 5)
            //    bytDatePeriodDefault = byteFreeNightStay;

            dDateCheckOutSim = dDateStart.AddDays(bytMinnight);

            ProductConditionExtra cConditionExtranet = new ProductConditionExtra();
            //ArrayList arrBenefit = PromotionContentXMl.Hotels2XMLReaderPromotionDetailExtranet();
            //ScoreResult = GetScoreBenefit(PromotionContentXMl);

            cConditionExtranet = cConditionExtranet.getConditionByConditionId(intCondition);


            int intOptionId = cConditionExtranet.OptionID;


            FrontProductPriceExtranet objPriceExtranet = new FrontProductPriceExtranet(intProductId, 29, dDateStart, dDateCheckOutSim);
            objPriceExtranet.LoadPrice();
            OptionPrice objOptionPrice = objPriceExtranet.CalculateAll(intCondition, intOptionId, 0);
            PriceBase = objOptionPrice.Price;

            if (PriceBase != 0)
                PriceBase = Math.Ceiling((PriceBase * (decimal)1.177));
            else
                PriceBase = 1000;

            switch (bytPromotionType)
            {
                //Free night
                case 1:
                    ScoreResult = (byte)Math.Round(((((decimal)byteFreeNightStay - byteFreeNightPay) / byteFreeNightStay) * 100));
                    if (IsBreakfast == 3)
                    {
                        ScoreResult = (byte)Math.Round((((PriceBase - ((PriceBase / byteFreeNightStay) * byteFreeNightPay)) * 100) / PriceBase));
                    }
                    break;
                //Discount All Day(%)
                case 2:
                    ScoreResult = (byte)DiscountCharge;

                    break;
                //Discount some day(%)
                case 3:
                    ScoreResult = (byte)DiscountCharge;
                    break;
                //Discount some Day(bht)
                case 4:
                    ScoreResult = (byte)Math.Round((((PriceBase - (((PriceBase / bytMinnight) - DiscountCharge) * bytMinnight)) * 100) / PriceBase));
                    break;
                //Set constant price(bht)
                case 5:
                    break;
                //Discount all Day(bht)
                case 6:

                    ScoreResult = (byte)Math.Round((((PriceBase - (((PriceBase / bytMinnight) - DiscountCharge) * bytMinnight)) * 100) / PriceBase));
                    break;

            }


            return ScoreResult;
            //return (byte)(ScoreResult + GetScoreBenefit(PromotionContentXMl));

        }

        public byte GetScoreBenefit(string KeyScore)
        {
            int score = 0;

            if (!string.IsNullOrEmpty(KeyScore))
            {
                ArrayList arrBenefit = KeyScore.Hotels2XMLReaderPromotionDetailExtranet();

                foreach (string Benefit in arrBenefit)
                {
                    string[] KeyWord_level_1 = { "free upgrade", "upgrade" };
                    string[] KeyWord_level_2 = { "free transfer", "transfer" };
                    string[] KeyWord_level_3 = { "trip", "trips", "tour", "buffet" };
                    string[] KeyWord_level_4 = { "get", "spa", "discount", "dinner", "breakfast" };
                    string[] KeyWord_level_5 = { "shuttle", "bus", "service" };
                    string[] KeyWord_level_6 = { "Benefit wifi", "complimentary", "Wifi", "free wifi" };

                    //Benefit wifi", "complimentary", "welcome", "drink"  === 5%
                    //get , %, spa, discount, dinner === 7%
                    //free, trip,trips, get free, tour, free buffet, buffet === 9%
                    //free transfer, tranfer === 12%
                    //free upgrade, upgrade === 20%


                    foreach (string key in KeyWord_level_1)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 20;
                            break;
                        }
                    }

                    foreach (string key in KeyWord_level_2)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 12;
                            break;
                        }
                    }

                    foreach (string key in KeyWord_level_3)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 9;
                            break;
                        }
                    }

                    foreach (string key in KeyWord_level_4)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 7;
                            break;
                        }
                    }

                    foreach (string key in KeyWord_level_5)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 5;
                            break;
                        }
                    }


                    foreach (string key in KeyWord_level_6)
                    {
                        Regex reg = new Regex(key.ToLower());

                        if (reg.IsMatch(Benefit.ToLower()))
                        {
                            score = score + 2;
                            break;
                        }
                    }
                }

            }


            return (byte)score;
        }
    }
}