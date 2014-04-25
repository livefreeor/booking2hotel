using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Front;

namespace Hotels2thailand.UI
{
    public partial class success_product_list : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtSearch.Text = string.Empty;
                DestinationDataBind();
                //dropWholeSaleDataBind();
                dropStatusProcessDataBind();
                //dropExpiredDataBind();

                //GvProduct List Bind()
                //gridProductDataBind();

                lnkCreate.NavigateUrl = "product.aspx?pdcid="+this.qProductCat;

                txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSearch.ClientID + "')");
                //txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ");
              
            }
        }

        //protected void btnSearch_OnClick(object sender, EventArgs e)
        //{
            
        //    if (!string.IsNullOrEmpty(txtSearch.Text))
        //    {
        //        byte intProductCatId = byte.Parse(this.qProductCat);
        //        int intDesId = Convert.ToInt32(dropDestination.SelectedValue);

        //        ProductListAdmin cProductList = new ProductListAdmin();
        //        gridProduct.DataSource = cProductList.getProductListAdVance(intProductCatId, txtSearch.Text);
        //        gridProduct.DataBind();
        //    }
        //}

        //protected void gridProduct_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //int SupplierSeleted = int.Parse(dropWholeSale.SelectedValue);
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onmouseover", "changein(this)");
        //        e.Row.Attributes.Add("onmouseout", "changeout(this)");


        //        HyperLink hlProducttitle = e.Row.Cells[0].FindControl("hlProducttitle") as HyperLink;
        //        string DesFolderName = (string)DataBinder.Eval(e.Row.DataItem, "DesFolderName");
        //        string FileName = (string)DataBinder.Eval(e.Row.DataItem, "ProductFileName");
        //        byte PCatId = (byte)DataBinder.Eval(e.Row.DataItem,"ProductCatId");

        //        string FolderResult = DesFolderName + "-" + Utility.GetProductType(PCatId)[0, 3];

        //        hlProducttitle.NavigateUrl = "http://www.hotels2thailand.com/" + FolderResult + "/" + FileName.Trim();

                
        //        int ProductId = (int)gridProduct.DataKeys[e.Row.RowIndex].Value;

              
                
        //        int intProductId = (int)DataBinder.Eval(e.Row.DataItem, "ProductId");
                
                
        //    }
        //}


       
        protected void DestinationDataBind()
        {
            Destination cDestination = new Destination();
            dropDestination.DataSource = cDestination.GetDestinationExtranetOnly();
            dropDestination.DataTextField = "Value";
            dropDestination.DataValueField = "Key";
            dropDestination.DataBind();

            ListItem cList = new ListItem("All Destination", "0");
            dropDestination.Items.Insert(0, cList);

            dropDestination.SelectedValue = "0";
        }

        
        protected void dropStatusProcessDataBind()
        {
            Status cStatus = new Status();
            dropStatusProcess.DataSource = cStatus.GetStatusByCatId(1);
            dropStatusProcess.DataTextField = "Value";
            dropStatusProcess.DataValueField = "Key";
            dropStatusProcess.DataBind();

            ListItem newLsit = new ListItem("All", "0");
            dropStatusProcess.Items.Insert(0, newLsit);

            dropStatusProcess.SelectedValue = "0";
        }

        

        //protected void btnSummit_OnClick(object sender, EventArgs e)
        //{
        //    txtSearch.Text = string.Empty;
        //    gridProductDataBind();
        //}


        //protected void gridProductDataBind()
        //{
        //    int intProductCatId =       int.Parse(this.qProductCat);
        //    int intDesId =              Convert.ToInt32(dropDestination.SelectedValue);
        //    int intStatusProcess =      Convert.ToInt32(dropStatusProcess.SelectedValue);
        //   // int intSupplierId  =        Convert.ToInt32(dropWholeSale.SelectedValue);
        //    int intStatus =             Convert.ToInt32(dropStatus.SelectedValue);
        //    bool bolRec = false;
        //    //if(dropRecommend.SelectedValue == "1")
        //    //    bolRec = true;

        //    bool bolStatus = true;
        //    if (dropStatus.SelectedValue == "0")
        //        bolStatus = false;

        //    ProductListAdmin cProductList = new ProductListAdmin();
        //    gridProduct.DataSource = cProductList.getProductListProcedure(intProductCatId, intDesId, intStatusProcess, bolStatus);
        //    gridProduct.DataBind();
            

        //}

        //protected void gridProduct_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    gridProductDataBind();

        //    gridProduct.PageIndex = e.NewPageIndex;
        //    gridProduct.DataBind();

        //}

    }
}