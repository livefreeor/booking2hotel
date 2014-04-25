using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using System.Web.Configuration;
using System.Text.RegularExpressions;

public partial class extranet_promotion_pro_score_update : System.Web.UI.Page
{

    private string Con = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime dDateStart = new DateTime(2012, 05, 1);
        //DateTime dDateCheckOutSim = new DateTime(2012, 5, 6);
        //decimal decPriceBase = 0;
        //FrontProductPriceExtranet objPriceExtranet = new FrontProductPriceExtranet(897, 29, dDateStart, dDateCheckOutSim);
        //objPriceExtranet.LoadPrice();
        //OptionPrice objOptionPrice = objPriceExtranet.CalculateAll(2130, 29709, 0);
        //decPriceBase = objOptionPrice.Price;

        //Response.Write(decPriceBase);
        //Response.End();
    }
    public void update_score_factor_Onclick(object sender, EventArgs e)
    {
        using (SqlConnection cn = new SqlConnection(this.Con))
        {
            SqlCommand cmd = new SqlCommand("SELECT benefit_id,type_id FROM tbl_promotion_benefit_extra_net", cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                byte fac1 = 0;
                byte fac2 = 0;
                using (SqlConnection cn1 = new SqlConnection(this.Con))
                {
                    
                    SqlCommand cmd2 = new SqlCommand("UPDATE tbl_promotion_benefit_extra_net SET score_fac_1=@score_fac_1 ,score_fac_2=@score_fac_2 WHERE benefit_id=@benefit_id", cn1);

                    if ((byte)reader["type_id"] == 1 || (byte)reader["type_id"] == 3 || (byte)reader["type_id"] == 4)
                    {
                        fac1 = 1; fac2 = 0;
                    }
                    else
                    {
                        fac1 = 0; fac2 = 1;
                    }
                    cmd2.Parameters.Add("@score_fac_1", SqlDbType.TinyInt).Value = fac1;
                    cmd2.Parameters.Add("@score_fac_2", SqlDbType.TinyInt).Value = fac2;
                    cmd2.Parameters.Add("@benefit_id", SqlDbType.Int).Value = (int)reader["benefit_id"];
                    cn1.Open();
                    cmd2.ExecuteNonQuery();
                }
            }
        }
    }

    public void Update_Score_Onclick(object sender, EventArgs e)
    {
        IList<object> iListProEx = new List<object>();
        int count = 1;
        int bytNightPay = 0;
        byte bytProType = 0;
        decimal decDiscount = 0;
        DateTime dDateStart = DateTime.Now;
        DateTime dDateEnd = DateTime.Now;
        string ProcontentBenefit = string.Empty;

        using (SqlConnection cn = new SqlConnection(Con))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_promotion_extra_net", cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {

                PromotionExxtranet cProex = new PromotionExxtranet
                {
                    PromotionId = (int)reader["promotion_id"],
                    ProductId = (int)reader["product_id"],
                    IsBreakfast = (byte)reader["Is_breakfast"],
                    BreakfastCharge = (decimal)reader["breakfast_charge"],
                    DayMin = (byte)reader["day_min"]
                };

                iListProEx.Add(cProex);

            }
        }

            foreach (PromotionExxtranet Pro in iListProEx)
            {
                if (Pro.PromotionId > 873)
                {
                    HttpContext.Current.Response.Write(count + "---" + Pro.PromotionId + "---");
                    using (SqlConnection cn1 = new SqlConnection(this.Con))
                    {
                        SqlCommand cmd1 = new SqlCommand("SELECT TOP 1 day_discount_start,type_id,discount FROM tbl_promotion_benefit_extra_net WHERE promotion_id=@promotion_id AND status = 1", cn1);
                        cmd1.Parameters.Add("@promotion_id", SqlDbType.Int).Value = Pro.PromotionId;
                        cn1.Open();

                        IDataReader reader1 = cmd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            bytNightPay = (byte)reader1["day_discount_start"] - 1;
                            bytProType = (byte)reader1["type_id"];
                            decDiscount = (decimal)reader1["discount"];
                        }
                    }

                    using (SqlConnection cn3 = new SqlConnection(this.Con))
                    {
                        SqlCommand cmd3 = new SqlCommand("SELECT TOP 1 date_use_start, date_use_end FROM tbl_promotion_date_use_extra_net WHERE promotion_id=@promotion_id", cn3);
                        cmd3.Parameters.Add("@promotion_id", SqlDbType.Int).Value = Pro.PromotionId;
                        cn3.Open();

                        IDataReader reader2 = cmd3.ExecuteReader();
                        while (reader2.Read())
                        {
                            dDateStart = (DateTime)reader2["date_use_start"];
                            dDateEnd = (DateTime)reader2["date_use_end"];
                        }
                    }

                    
                    string strCondition = string.Empty;
                    using (SqlConnection cn5 = new SqlConnection(this.Con))
                    {
                        SqlCommand cmd5 = new SqlCommand("SELECT  condition_id FROM tbl_promotion_condition_extra_net WHERE promotion_id=@promotion_id AND status = 1", cn5);
                        cmd5.Parameters.Add("@promotion_id", SqlDbType.Int).Value = Pro.PromotionId;
                        cn5.Open();

                        IDataReader reader4 = cmd5.ExecuteReader();
                        while (reader4.Read())
                        {
                            strCondition = reader4["condition_id"].ToString() + ",";
                        }
                    }

                    string[] arrCondition = strCondition.Hotels2RightCrl(1).Split(',');
                    if (arrCondition.Count() > 0)
                    {
                        byte bytScore = 0;
                        

                        bytScore = GetScore(Pro.ProductId, dDateStart, dDateEnd, bytProType, Pro.IsBreakfast,
                           Pro.BreakfastCharge, decDiscount, arrCondition, Pro.DayMin,
                           (byte)bytNightPay, Pro.DayMin);
                        

                        using (SqlConnection cn2 = new SqlConnection(this.Con))
                        {
                            SqlCommand cmd2 = new SqlCommand("UPDATE tbl_promotion_extra_net SET promotion_score = @promotion_score WHERE promotion_id = @promotion_id", cn2);
                            cmd2.Parameters.Add("@promotion_id", SqlDbType.Int).Value = Pro.PromotionId;
                            cmd2.Parameters.Add("@promotion_score", SqlDbType.TinyInt).Value = bytScore;
                            
                            cn2.Open();
                            cmd2.ExecuteNonQuery();
                        }



                    }

                    count = count + 1;
                }
                
            }
            
        
    }

    public byte GetScore(int intProductId, DateTime dDateStart, DateTime dDateEnd,
            byte bytPromotionType, byte IsBreakfast, decimal ABFCharge, decimal DiscountCharge, string[] conditionCheck,
            byte byteFreeNightStay, byte byteFreeNightPay, byte bytMinnight)
    {

        byte ScoreResult = 0;
        //byte bytDatePeriodDefault = 5;
        decimal PriceBase = 0;
        //decimal UnitPrice = 0;
        int DatediffCheckinout = dDateEnd.Subtract(dDateStart).Days;
        int intCondition = 0;
       
       
        if (!string.IsNullOrEmpty(conditionCheck[0]))
            intCondition = int.Parse(conditionCheck[0]);
        
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

        if (cConditionExtranet != null)
        {
            int intOptionId = cConditionExtranet.OptionID;

            FrontProductPriceExtranet objPriceExtranet = new FrontProductPriceExtranet(intProductId, 29, dDateStart, dDateCheckOutSim);
            objPriceExtranet.LoadPrice();
            OptionPrice objOptionPrice = objPriceExtranet.CalculateAll(intCondition, intOptionId, 0);
            PriceBase = objOptionPrice.Price;
            HttpContext.Current.Response.Write( PriceBase + "<br/>");
            HttpContext.Current.Response.Flush();
            if (PriceBase > 0)
            {
                PriceBase = Math.Ceiling((PriceBase * (decimal)1.177));
                switch (bytPromotionType)
                {
                    //Free night
                    case 1:
                        ScoreResult = (byte)Math.Round(((((decimal)byteFreeNightStay - byteFreeNightPay) / byteFreeNightStay) * 100));
                        if (IsBreakfast == 3)
                        {
                            //ScoreResult = (byte)(((((PriceBase / byteFreeNightStay) * (byteFreeNightStay - byteFreeNightPay)) + ABFCharge) / byteFreeNightStay) * 100);
                            ScoreResult = (byte)Math.Round((((PriceBase - ((PriceBase / byteFreeNightStay) * byteFreeNightPay)) * 100) / PriceBase));
                        }
                        break;
                    //Discount All Day(%)
                    case 2:
                        ScoreResult = (byte)DiscountCharge;

                        break;
                    //Discount some day(%)
                    case 3:
                        //ScoreResult = (byte)((PriceBase - ((((PriceBase / bytMinnight) - ((PriceBase / bytMinnight) * DiscountCharge)) / 100) * bytMinnight)) / 100);
                        ScoreResult = (byte)DiscountCharge;
                        //ScoreResult = (byte)Math.Round(((((((PriceBase / bytMinnight) * DiscountCharge) / 100) * bytMinnight) * 100) / PriceBase));
                        //Response.Write((decimal)((((((PriceBase / bytMinnight) * DiscountCharge) / 100) * bytMinnight) * 100) / PriceBase));
                        //Response.Flush();
                        break;
                    //Discount some Day(bht)
                    case 4:
                        //ScoreResult = (byte)((PriceBase - (DiscountCharge * bytMinnight)) / 100);
                        ScoreResult = (byte)Math.Round((((PriceBase - (((PriceBase / bytMinnight) - DiscountCharge) * bytMinnight)) * 100) / PriceBase));
                        break;
                    //Set constant price(bht)
                    case 5:

                        break;
                    //Discount all Day(bht)
                    case 6:

                        //ScoreResult = (byte)((PriceBase - (((PriceBase / bytMinnight) - DiscountCharge) * bytMinnight) * 100) / PriceBase);
                        ScoreResult = (byte)Math.Round((((PriceBase - (((PriceBase/bytMinnight)-DiscountCharge)*bytMinnight))*100)/PriceBase));
                        break;

                }
            }
            else
            {
                ScoreResult = 0;
            }
        }

        
        


        return (byte)(ScoreResult);

    }

    public void Update_score_benefit_Onclick(object sender,  EventArgs e)
    {
        IList<object> iListProEx = new List<object>();
        
        string ProcontentBenefit = string.Empty;

        using (SqlConnection cn = new SqlConnection(Con))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_promotion_content_extra_net", cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                PromotionContentExtranet cProCon = new PromotionContentExtranet
                {
                    PromotionId = (int)reader["promotion_id"],
                    Detail = reader["detail"].ToString()
                };

                iListProEx.Add(cProCon);

            }
        }

        foreach (PromotionContentExtranet Pro in iListProEx)
        {
            byte bytScoreBenefit = 0;
            bytScoreBenefit = GetScoreBenefit(Pro.Detail);

            HttpContext.Current.Response.Write(Pro.PromotionId + "--" + bytScoreBenefit + "<br/>");
            HttpContext.Current.Response.Flush();
            using (SqlConnection cn2 = new SqlConnection(this.Con))
            {
                SqlCommand cmd2 = new SqlCommand("UPDATE tbl_promotion_extra_net SET promotion_score_benefit = @promotion_score_benefit WHERE promotion_id = @promotion_id", cn2);
                cmd2.Parameters.Add("@promotion_id", SqlDbType.Int).Value = Pro.PromotionId;
               
                cmd2.Parameters.Add("@promotion_score_benefit", SqlDbType.TinyInt).Value = bytScoreBenefit;
                cn2.Open();
                cmd2.ExecuteNonQuery();
            }
        }
        

       
    }

    private byte GetScoreBenefit(string KeyScore)
    {
        int score = 0;

        if (!string.IsNullOrEmpty(KeyScore))
        {
            ArrayList arrBenefit = KeyScore.Hotels2XMLReaderPromotionDetailExtranet();

            foreach (string Benefit in arrBenefit)
            {
                string[] KeyWord_level_1 = { "free upgrade", "upgrade" };
                string[] KeyWord_level_2 = { "free transfer", "tranfer" };
                string[] KeyWord_level_3 = { "trip", "trips", "tour", "buffet" };
                string[] KeyWord_level_4 = { "get", "spa", "discount", "dinner", "breakfast" };
                string[] KeyWord_level_5 = { "shuttle", "bus", "service" };
                string[] KeyWord_level_6 = { "Benefit wifi", "complimentary", "welcome", "drink", "Wifi", "free wifi"};

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


    public void btnShow_Onclick(object sender, EventArgs e)
    {
        byte benefitTotal = GetScoreBenefit(txtSample.Text);

        ArrayList arrBenefit = txtSample.Text.Hotels2XMLReaderPromotionDetailExtranet();
        string BenefitREsult = string.Empty;
        foreach (string item in arrBenefit)
        {
            BenefitREsult = BenefitREsult + item + "<br/>";
        }
        lblBenefitREsult.Text = BenefitREsult;
        lblScore.Text = benefitTotal.ToString();
    }
}