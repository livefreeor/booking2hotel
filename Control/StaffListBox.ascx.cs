using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_StaffListBox : System.Web.UI.UserControl
    {
        public string QueryStringType
        {
            get { return Request.QueryString["t"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }

        public void GvStaffList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                ImageButton linkStaffStat = (ImageButton)e.Row.Cells[4].FindControl("imgButton");
                //Image imgStaffStat = (Image)e.Row.Cells[4].FindControl("imagestaffstat");


                if (bolStatus)
                {
                    linkStaffStat.ImageUrl = "../images/staffactive.png";
                }
                else
                {
                    linkStaffStat.ImageUrl = "../images/staffinactive.png";
                }

            }
        }

        
        public void GVStaffParent_OndataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView GVStaffChild = e.Row.Cells[0].FindControl("GvStaffList") as GridView;
                Label lblGroupHead = e.Row.Cells[0].FindControl("lblStaffParentHead") as Label;

                if (this.QueryStringType == "bluehouse" || string.IsNullOrEmpty(this.QueryStringType))
                {
                    short shrCategory = Convert.ToInt16(GVStaffParent.DataKeys[e.Row.DataItemIndex].Value.ToString());

                    Staff clStaff = new Staff();
                    GVStaffChild.DataSource = clStaff.getStaffListBlueHouseByCategory(shrCategory);
                    lblGroupHead.Text = Staff.getStaffCategoryById(Convert.ToByte(shrCategory));
                    GVStaffChild.AutoGenerateColumns = false;
                    GVStaffChild.DataBind();

                 }
                else if (this.QueryStringType == "partners")
                {
                    short shrSupplier = Convert.ToInt16(GVStaffParent.DataKeys[e.Row.DataItemIndex].Value.ToString());

                    Staff clStaff = new Staff();
                    Supplier clSupplier = new Supplier();
                    GVStaffChild.DataSource = clStaff.getStaffListPartnerBySupplier(shrSupplier);
                    if (clStaff.getStaffListPartnerBySupplier(shrSupplier).Count > 0)
                    {
                        lblGroupHead.Text = clSupplier.getSupplierById(shrSupplier).SupplierTitle;
                    }
                    GVStaffChild.AutoGenerateColumns = false;
                    GVStaffChild.DataBind();

                   // lblGroupHead.Text = clStaff.CatTitle;
                }
            }
        }

        protected void GvStaffList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "updatestatus")
            {
                Staff clStaff = new Staff();
                clStaff =  clStaff.getStaffById(short.Parse(e.CommandArgument.ToString()));
                if (clStaff.Status)
                {
                    clStaff.Status = false;
                }
                else
                {
                    clStaff.Status = true;
                }
                clStaff.Update();
            }

            Server.Transfer("staffmanage.aspx");
        }

        public void BindGrid()
        {
            
            if (this.QueryStringType == "bluehouse" || string.IsNullOrEmpty(this.QueryStringType))
                {
                    
                    string[] Keys = { "key" };
                    GVStaffParent.DataSource = Staff.getCategoryByBlueHouseStaff();
                    GVStaffParent.DataKeyNames = Keys;
                    GVStaffParent.AutoGenerateColumns = false;
                    GVStaffParent.DataBind();


                }
            else if (this.QueryStringType == "partners")
                {
                 
                    //Supplier clSupplier = new Supplier();
                    //GVStaffParent.DataSource = clSupplier.GetSupplierListByHaveStaff();
                    //string[] Property = { "SupplierId" };
                    //GVStaffParent.DataKeyNames = Property;
                    //GVStaffParent.AutoGenerateColumns = false;
                    //GVStaffParent.DataBind();
                }

        }

        public void BindBlueHouseStaffList()
        {

        }

        public void BindPartnerStaffList()
        {

        }

       
        
    }
}