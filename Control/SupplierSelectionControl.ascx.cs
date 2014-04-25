using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Production;
using System.IO;


namespace Hotels2thailand.UI.Controls
{

    public partial class Control_SupplierSelectionControl : System.Web.UI.UserControl
    {
        public string strProductIdQueryString
        {
            get
            {
                return Request.QueryString["pid"];
            }
        }

        private IList<Supplier> _iList_selected = null;
        public IList<Supplier> IlistSelected
        {
            get 
            {
                if (_iList_selected == null)
                {
                    _iList_selected=  ViewStateDataSourceBinding("itemselected");
                }
                return _iList_selected;
            }
            set { _iList_selected = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ViewState["itemselected"] = string.Empty;
                string strAphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                char[] charArryAphabet = strAphabet.ToCharArray();

                Dictionary<int, string> dicListString = new Dictionary<int, string>();
                int intKey = 0;
                foreach (char item in charArryAphabet)
                {
                    dicListString.Add(intKey, item.ToString());
                    intKey = intKey + 1;
                }

                ListAphabet.DataSource = dicListString;
                ListAphabet.DataKeyField = "Value";
                ListAphabet.DataBind();

                ListAphabet.ItemCommand += new DataListCommandEventHandler(this.ListAphabet_Command);
                ListBoxSupplierListDataBind();

               
            }
            else
            {
                //GvSupplierSelectionList.DataSource = ViewStateDataSourceBinding("itemList");
                //GvSupplierSelectionList.DataBind();
            }

            
        }


        public void ListBoxSupplierListDataBind()
        {
            
            
            Supplier clSupplier = new Supplier();
            if (!string.IsNullOrEmpty(this.strProductIdQueryString))
            {
                if (ListAphabet.SelectedValue != null)
                {
                    List<object> SupplierList = clSupplier.getListSupplierByAlphabet(ListAphabet.SelectedValue.ToString(), int.Parse((this.Page as Hotels2BasePage).qProductId));
                    ListBoxSupplierList.DataSource = SupplierList;

                }
                else
                {
                    
                    List<object> SupplierList = clSupplier.getListSupplierByAlphabet("A", int.Parse((this.Page as Hotels2BasePage).qProductId));
                    ListBoxSupplierList.DataSource = SupplierList;
                }
                delone.Visible = false;
            }
            else
            {
                
                if (ListAphabet.SelectedValue != null)
                {
                    //Response.Write("ssss");
                    List<object> SupplierList = clSupplier.getListSupplierByAlphabet(ListAphabet.SelectedValue.ToString(), ViewStateDataSourceBinding("itemselected"));
                    ListBoxSupplierList.DataSource = SupplierList;
                    
                }
                else
                {
                    //Response.Write("ssssqqq");
                    List<object> SupplierList = clSupplier.getListSupplierByAlphabet("A", ViewStateDataSourceBinding("itemselected"));
                    ListBoxSupplierList.DataSource = SupplierList;
                }
            }

            ListBoxSupplierList.DataTextField = "SupplierTitle";
            ListBoxSupplierList.DataValueField = "SupplierId";
            ListBoxSupplierList.DataBind();
        }

        public void ListBoxSupplierSelectedDataBind()
        {
            if (!string.IsNullOrEmpty(this.strProductIdQueryString))
            {
                ProductSupplier cProductSupplier = new ProductSupplier();
                ListBoxSelected.DataSource = cProductSupplier.getSupplierListByProductID(int.Parse(this.strProductIdQueryString));
                ListBoxSelected.DataTextField = "SupplierTitle";
                ListBoxSelected.DataValueField = "SupplierId";
                ListBoxSelected.DataBind();
            }
            else
            {
                ListBoxSelected.DataSource = ViewStateDataSourceBinding("itemselected");
                ListBoxSelected.DataTextField = "SupplierTitle";
                ListBoxSelected.DataValueField = "SupplierId";
                ListBoxSelected.DataBind();
            }

            
        }

        protected IList<Supplier> ViewStateDataSourceBinding(string strViewStateName)
        {
            //ViewState["itemselected"] = string.Empty;
            IList<Supplier> dicItem = new List<Supplier>();
            string[] arrItem = ViewState[strViewStateName].ToString().Split(Convert.ToChar("~"));
            int count = 0;
            foreach (string item in arrItem)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] acItem = item.Split(Convert.ToChar("^"));
                    dicItem.Add(new Supplier { SupplierId = short.Parse(acItem[0]), SupplierTitle = acItem[1] });
                }
                count = count + 1;
            }
            return dicItem;
        }


        public override void DataBind()
        {
            ListBoxSupplierListDataBind();
            ListBoxSupplierSelectedDataBind();
            
        }

        
        protected void ListAphabet_Command(Object sender, DataListCommandEventArgs e)
        {
            
            // Set the SelectedIndex property to select an item in the DataList.
            ListAphabet.SelectedIndex = e.Item.ItemIndex;
            ListBoxSupplierListDataBind();
        }



        protected void selectAll_Click(object sender, EventArgs e)
        {

        }

        protected void selectone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.strProductIdQueryString))
            {
                if (!string.IsNullOrEmpty(ListBoxSupplierList.SelectedValue))
                {
                    ProductSupplier cProductSupplier = new ProductSupplier
                    {
                        ProductID = int.Parse((this.Page as Hotels2BasePage).qProductId),
                        SupplierID = short.Parse(ListBoxSupplierList.SelectedValue),
                        Status = true

                    };
                    cProductSupplier.InsertNewProductSupplier(cProductSupplier);
                }
            }
            else
            {
                ViewState["itemselected"] = ViewState["itemselected"] + ListBoxSupplierList.SelectedValue + "^" + ListBoxSupplierList.SelectedItem + "~";
            }

            ListBoxSupplierSelectedDataBind();
            ListBoxSupplierListDataBind();

        }

        protected void delone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.strProductIdQueryString))
            {
                if (!string.IsNullOrEmpty(ListBoxSelected.SelectedValue))
                {
                    ProductSupplier.DeleteProductSupplier(short.Parse(ListBoxSelected.SelectedValue), int.Parse((this.Page as Hotels2BasePage).qProductId));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ListBoxSelected.SelectedValue))
                {
                    
                    IList<Supplier> Ilist  = ViewStateDataSourceBinding("itemselected");
                    Ilist.RemoveAt(ListBoxSelected.SelectedIndex);
                    ViewState["itemselected"] = string.Empty;
                    foreach (Supplier item in Ilist)
                    {
                        ViewState["itemselected"] = ViewState["itemselected"] + item.SupplierId.ToString() + "^" + item.SupplierTitle + "~";
                    }
                }

                
            }

            ListBoxSupplierSelectedDataBind();
            ListBoxSupplierListDataBind();
        }
        protected void delAll_Click(object sender, EventArgs e)
        {

        }
}
}