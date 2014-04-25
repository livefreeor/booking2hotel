using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Front;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_config_product_point : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DropProductCatDataBind();
                DropDestinationDataBind();
                DropLocationDataBind();
            }
        }

        public void DropProductCatDataBind()
        {
            ProductCategory cProcat = new ProductCategory();
            dropProductCat.DataSource = cProcat.GetProductCategory();
            dropProductCat.DataTextField = "Value";
            dropProductCat.DataValueField = "Key";
            dropProductCat.DataBind();

            ListItem newItem = new ListItem("Select", "0");
            dropProductCat.Items.Insert(0, newItem);

        }

        public void dropDestination_OnSelectedIndexChanged( object sender , EventArgs e)
        {
            if (dropDestination.SelectedValue != "0")
            {
                Location cLocation = new Location();
                dropLocation.DataSource = cLocation.dicGetLocationListByDesId(short.Parse(dropDestination.SelectedValue));
                dropLocation.DataTextField = "Value";
                dropLocation.DataValueField = "Key";
                dropLocation.DataBind();
            }

            ListItem newItem = new ListItem("Select", "0");
            dropLocation.Items.Insert(0, newItem);
        }

        public void DropDestinationDataBind()
        {
            Destination cDestination = new Destination();
            dropDestination.DataSource = cDestination.GetDestinationAll();
            dropDestination.DataTextField = "Value";
            dropDestination.DataValueField = "Key";
            dropDestination.DataBind();

            ListItem newItem = new ListItem("Select", "0");
            dropDestination.Items.Insert(0, newItem);
        }

        public void DropLocationDataBind()
        {
            if (dropDestination.SelectedValue != "0")
            {
                Location cLocation = new Location();
                dropLocation.DataSource = cLocation.dicGetLocationListByDesId(short.Parse(dropDestination.SelectedValue));
                dropLocation.DataTextField = "Value";
                dropLocation.DataValueField = "Key";
                dropLocation.DataBind();
            }

            ListItem newItem = new ListItem("Select", "0");
            dropLocation.Items.Insert(0, newItem);
        }

        public void GVProductDatabind()
        {
            Product cProduct = new Product();
            if (dropProductCat.SelectedValue != "0" && dropDestination.SelectedValue != "0")
            {
                if (dropLocation.SelectedValue == "0")
                {
                    GVProductList.DataSource = cProduct.GetProductAllByCatAndDesAndLocation(byte.Parse(dropProductCat.SelectedValue),
                        short.Parse(dropDestination.SelectedValue));
                }
                else
                {
                    GVProductList.DataSource = cProduct.GetProductAllByCatAndDesAndLocation(byte.Parse(dropProductCat.SelectedValue),
                        short.Parse(dropDestination.SelectedValue), short.Parse(dropLocation.SelectedValue));
                }

                GVProductList.DataBind();
            }
        }
        public void btnSearch_OnClick(object sender, EventArgs e)
        {
            GVProductDatabind();
        }

        public void GVProductList_OnrowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DesFolderName = (string)DataBinder.Eval(e.Row.DataItem, "DestinationFolderName");
                string FileName = (string)DataBinder.Eval(e.Row.DataItem, "ProductFileName");
                byte PCatId = (byte)DataBinder.Eval(e.Row.DataItem, "ProductCategoryID");
                string FolderResult = DesFolderName + "-" + Utility.GetProductType(PCatId)[0, 3];

                bool IsHot = (bool)DataBinder.Eval(e.Row.DataItem, "FlagFeature");

                HyperLink hlProducttitle = e.Row.FindControl("hlProducttitle") as HyperLink;
                hlProducttitle.NavigateUrl = "http://www.hotels2thailand.com/" + FolderResult + "/" + FileName;

                RadioButtonList radio = e.Row.Cells[3].FindControl("radioIsHot") as RadioButtonList;

                radio.SelectedValue = IsHot.ToString();
                



            }
        }

        public void btnSave_Onclick(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in GVProductList.Rows)
            {
                if (gvRow.RowType == DataControlRowType.DataRow)
                {
                    RadioButtonList radio = GVProductList.Rows[gvRow.DataItemIndex].Cells[3].FindControl("radioIsHot") as RadioButtonList;

                    TextBox txtPoint = GVProductList.Rows[gvRow.DataItemIndex].Cells[4].FindControl("txtPoint") as TextBox;
                    int intProductID = (int)GVProductList.DataKeys[gvRow.DataItemIndex].Value;
                    Product cProduct = new Product();
                    cProduct.UpdateSupplierFeatureAndPoint(intProductID, bool.Parse(radio.SelectedValue), byte.Parse(txtPoint.Text));

                }
            }

            GVProductDatabind();
        }
    }

    
}