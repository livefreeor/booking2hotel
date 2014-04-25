using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

public partial class test_duplicatinPriceManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            ProductPricecheck cPriceDup = new ProductPricecheck();
            lblTotal.Text = cPriceDup.GetDuplicationPriceALl().Count().ToString();
        }
    }

    protected void btnListByProduct_Click(object sender, EventArgs e)
    {
        ProductPricecheck cPriceDup = new ProductPricecheck();
        IList<object> iList = cPriceDup.GetDuplicationPrice(int.Parse(txtProductId.Text));
        GvPrice.DataSource = iList;
        GvPrice.DataBind();

        lblTotal.Text = iList.Count().ToString();
    }

    protected void btnListByCondition_Click(object sender, EventArgs e)
    {
        ProductPricecheck cPriceDup = new ProductPricecheck();
        IList<object> iList = cPriceDup.GetDuplicationPriceByCOnditionId(int.Parse(txtProductId.Text), int.Parse(txtCOnditionID.Text));
        GvPrice.DataSource = iList;
        GvPrice.DataBind();
        lblTotal.Text = iList.Count().ToString();
        
    }

    protected void btnClean_onclick(object sender, EventArgs e)
    {
        ProductPricecheck cPriceDup = new ProductPricecheck();
        foreach (GridViewRow gvRow in GvPrice.Rows)
        {
            if (gvRow.RowType == DataControlRowType.DataRow)
            {
                Label lblPriceId = (Label)GvPrice.Rows[gvRow.RowIndex].FindControl("lblPriceID");
                Label lblDAtePrice = (Label)GvPrice.Rows[gvRow.RowIndex].FindControl("lblDatePrice");
                Label lblPrice = (Label)GvPrice.Rows[gvRow.RowIndex].FindControl("lblPrice");
                int intPriceId = int.Parse(lblPriceId.Text);

                //Response.Write(intPriceId);
                
                Response.Write(intPriceId + "</br>");
                Response.Write(lblDAtePrice + "</br>");
                Response.Write(lblPrice + "</br>");
                bool ret = cPriceDup.DeletePrice(intPriceId);
                Response.Write(ret);
                Response.Write("---------------");
                Response.Write("</br></br>");
                Response.Flush();
            }
        }
    }

    protected void btnPrepare_Click(object sender, EventArgs e)
    {
        


        ProductPricecheck cPriceDup = new ProductPricecheck();
        IList<object> iListData = cPriceDup.GetDuplicationPriceByCOnditionId(int.Parse(txtProductId.Text), int.Parse(txtCOnditionID.Text));

        IList<ProductPricecheck> forcusData = new List<ProductPricecheck>();

        DateTime dDateTmp = DateTime.MinValue;
        foreach (ProductPricecheck item in iListData)
        {
            if (dDateTmp == item.DAtePrice)
            {
                forcusData.Add(new ProductPricecheck
                {
                    ProductId = item.ProductId,
                    Title = item.Title,
                    OptionId = item.OptionId,
                    OptionTitle = item.OptionTitle,
                    ConditionId = item.ConditionId,
                    ConditionTitle = item.ConditionTitle,
                    PriceId = item.PriceId,
                    DAtePrice = item.DAtePrice,
                    Price = item.Price,
                    CountDuplicate = item.CountDuplicate
                });
            }


            dDateTmp = item.DAtePrice;
        }

        lblTotal.Text = forcusData.Count().ToString();
        GvPrice.DataSource = forcusData;
        GvPrice.DataBind();
    }
}