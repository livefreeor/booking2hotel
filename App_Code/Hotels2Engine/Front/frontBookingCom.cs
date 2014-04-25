using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Production;
/// <summary>
/// Summary description for frontBookingCom
/// </summary>
public class frontBookingCom : Hotels2BaseClass
{
    public int BookingProductId { get; set; }
    public int ProductId { get; set; }
    public byte? SaleID { get; set; }
    public byte? ManageId { get; set; }
    public byte? CommssionCat { get; set; }

    private decimal _comVal = 0;
    public decimal ComVal
    {
        get
        {
            return _comVal;
        }
        set { _comVal = value; }
    }
    //public decimal? ComVal { get; set; }

	public frontBookingCom(int intProductId, int intBookingProductId)
	{
        this.ProductId = intProductId;
        this.BookingProductId = intBookingProductId;
        //loadComVal();
		//
		// TODO: Add constructor logic here
		//
	}


    public void loadComVal()
    {
        ProductBookingEngineCommission cProductCom = new ProductBookingEngineCommission();

        cProductCom = cProductCom.GetCommission(this.ProductId);

        // if Commission Type (cat_id ) not is Step[tier] 
        // in tier type must be processing in real time only!! 
        ProductBookingEngine cProductBooking = new ProductBookingEngine();
        cProductBooking = cProductBooking.GetProductbookingEngine(this.ProductId);
        this.SaleID = cProductBooking.SalesID;
        this.ManageId = cProductBooking.ManageId;


        if (cProductCom != null)
        {
            this.CommssionCat = cProductCom.CatId;

            if (cProductCom.CatId != 3)
            {
                this.ComVal = cProductCom.Commission;
            }
        }

        //int intBookingID = 0;
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_product SET manage_id =@manage_id, sale_id=@sale_id, cat_id=@cat_id, compercentandVal=@compercentandVal WHERE booking_product_id=@booking_product_id", cn);

            cmd.Parameters.AddWithValue("@booking_product_id", this.BookingProductId);

            if (this.ManageId.HasValue)
                cmd.Parameters.AddWithValue("@manage_id", this.ManageId);
            else
                cmd.Parameters.AddWithValue("@manage_id", DBNull.Value);

            if (this.SaleID.HasValue)
                cmd.Parameters.AddWithValue("@sale_id", this.SaleID);
            else
                cmd.Parameters.AddWithValue("@sale_id", DBNull.Value);

            if (this.CommssionCat.HasValue)
                cmd.Parameters.AddWithValue("@cat_id", this.CommssionCat);
            else
                cmd.Parameters.AddWithValue("@cat_id", DBNull.Value);

            
                cmd.Parameters.AddWithValue("@compercentandVal", this.ComVal);
                cn.Open();

                ExecuteNonQuery(cmd);

        }

        
    }

    public void loadComCalculate()
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmdbok = new SqlCommand("SELECT booking_id FROM tbl_booking_product WHERE booking_product_id = @booking_product_id", cn);
            cmdbok.Parameters.AddWithValue("@booking_product_id", this.BookingProductId);
            cn.Open();
            int intBookingID = (int)ExecuteScalar(cmdbok);

            if (intBookingID > 0)
            {
                Hotels2thailand.Account.Account_Commission_Engine cCom = new Hotels2thailand.Account.Account_Commission_Engine(intBookingID, this.ComVal, this.CommssionCat);

                cCom.CalculateCommissionBhtManage();

            }
        }
    }
}