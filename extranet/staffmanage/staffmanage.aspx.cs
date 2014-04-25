using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class extranet_staffmanage : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(this.Request.QueryString["s"]))
                {
                    ChkProductList();
                    StaffDataBind();
                }
                
            }
        }

        
        public void StaffDataBind()
        {
            string StaffID = Request.QueryString["s"].Hotels2DecryptedData().Hotels2RightCrl(40);

            short shrStaffId = short.Parse(StaffID);

            StaffPageAuthorizeExtra StaffPage = new StaffPageAuthorizeExtra();

            StaffExtra cStaffExtra = new StaffExtra();

            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();

            cStaffExtra = cStaffExtra.GetStaffExtraByStaffID(shrStaffId);



            string strViewStateStaffProduct = string.Empty;
            // binding Staff Product
            foreach (ListItem item in chkListProduct.Items)
            {
                
                foreach (StaffProduct_Extra staffProduct in cStaffProduct.getProductByStaffId(shrStaffId))
                {
                    if (item.Value == staffProduct.ProductID.ToString())
                    {
                        item.Selected = true;
                        strViewStateStaffProduct = strViewStateStaffProduct + item.Value + ",";
                    }
                }
            }

            ViewState["StaffProduct"] = strViewStateStaffProduct;

            // binding StaffModule And Role

            foreach (StaffPageAuthorizeExtra module in StaffPage.GetModuleByStaffID(shrStaffId))
            {
                ////if module Staff 
                //if (module.ModuleID == 1)
                //{
                //    chkRateControl.Checked = true;
                //    dropMethodRateControl.SelectedValue = module.MethodId.ToString();
                //}

                //if module rate control
                if (module.ModuleID == 2)
                {
                    
                    chkRateControl.Checked = true;
                    dropMethodRateControl.SelectedValue = module.MethodId.ToString();
                }

                //if module rate control
                if (module.ModuleID == 8)
                {
                    chkPackage.Checked = true;
                    dropMethodPackageControl.SelectedValue = module.MethodId.ToString();
                }

                //if module Promotion
                if (module.ModuleID == 3)
                {
                    chkPromotion.Checked = true;
                    dropMethodPromotion.SelectedValue = module.MethodId.ToString();
                }
                //if module Member
                if (module.ModuleID == 9)
                {
                    chkMember.Checked = true;
                    dropMethodMember.SelectedValue = module.MethodId.ToString();
                }

                //if module allotment
                if (module.ModuleID == 4)
                {
                    chkAllotment.Checked = true;
                    dropMethodAllotment.SelectedValue = module.MethodId.ToString();
                }
                //if module order center
                if (module.ModuleID == 5)
                {
                    chkOrdercenter.Checked = true;
                    dropMethodOrdercenter.SelectedValue = module.MethodId.ToString();
                }
                //if module review
                if (module.ModuleID == 7)
                {
                    chkReview.Checked = true;
                    dropMethodreview.SelectedValue = module.MethodId.ToString();
                }
                //if module Report
                if (module.ModuleID == 6)
                {
                    checkReport.Checked = true;
                    dropMethodReport.SelectedValue = module.MethodId.ToString();
                }
                //if module Newsletter
                if (module.ModuleID == 10)
                {
                    checkNews.Checked = true;
                    dropMethodNews.SelectedValue = module.MethodId.ToString();
                }
            }

            Suppliers.Supplier cSupplier = new Suppliers.Supplier();
            cSupplier = cSupplier.getSupplierById(this.CurrentSupplierId);


            string UserNameSplit = cStaffExtra.UserName.Split('@')[0];
            string UserSurfix = cSupplier.SuffixUserExtra;
            //binding StaffDetail
            txtFullName.Text = cStaffExtra.Title;
            txtEmails.Text = cStaffExtra.Email;
            txtUsername.Text = UserNameSplit;  
            txtSurfixSup.Text = UserSurfix;
            activestaff.Checked = false;
            if (cStaffExtra.Status)
                activestaff.Checked = true;


            StaffAuthorizeExtra cStaffAuthorize = new StaffAuthorizeExtra();
            cStaffAuthorize = cStaffAuthorize.GetStaffAuthorize(shrStaffId);

            if (cStaffAuthorize.AuthorizeId == 1)
            {
                chkListProduct.Enabled = false;

                dropMethodMember.Enabled = false;
                dropMethodRateControl.Enabled = false;
                dropMethodPackageControl.Enabled = false;
                dropMethodPromotion.Enabled = false;
                dropMethodAllotment.Enabled = false;
                dropMethodOrdercenter.Enabled = false;
                dropMethodReport.Enabled = false;
                dropMethodreview.Enabled = false;
                dropMethodNews.Enabled = false;

                chkRateControl.Enabled = false;
                chkPackage.Enabled = false;
                chkPromotion.Enabled = false;
                chkAllotment.Enabled = false;
                chkOrdercenter.Enabled = false;
                checkReport.Enabled = false;
                chkMember.Enabled = false;
                chkReview.Enabled = false;
                checkNews.Enabled = false;

                activestaff.Enabled = false;
                txtSurfixSup.Enabled = false;
                txtUsername.Enabled = false;
            }
            else
            {
                dropMethodRateControl.Items[0].Enabled = false;
                dropMethodPackageControl.Items[0].Enabled = false;
                dropMethodMember.Items[0].Enabled = false;
                dropMethodPromotion.Items[0].Enabled = false;
                dropMethodAllotment.Items[0].Enabled = false;
                dropMethodOrdercenter.Items[0].Enabled = false;
                dropMethodReport.Items[0].Enabled = false;
                dropMethodreview.Items[0].Enabled = false;
                dropMethodNews.Items[0].Enabled = false;
               
            }
                

        }

        public void ChkProductList()
        {
            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            if (string.IsNullOrEmpty(this.qProductId))
            {
                chkListProduct.DataSource = cStaffProduct.getProductByStaffId(this.CurrentStaffId);
            }
            else
            {
                chkListProduct.DataSource = cStaffProduct.getProductExtraByProductId(int.Parse(this.qProductId));
            }
            chkListProduct.DataBind();

        }


        public void btnSave_Onclick(object sender, EventArgs e)
        {
            //Response.Write(ViewState["StaffProduct"]);
            //Response.End();
            string StaffID = Request.QueryString["s"].Hotels2DecryptedData().Hotels2RightCrl(40);

            short shrStaffId = short.Parse(StaffID);
            

            StaffProduct_Extra cStaffproductExtra = new StaffProduct_Extra();
            StaffExtra cStaff = new StaffExtra();
            
            //update StaffDetail 
            bool staffIsActive = false ;
            if (activestaff.Checked)
                staffIsActive = true;

            string UserName = txtUsername.Text + "@" + txtSurfixSup.Text;

            cStaff.UpdateStaffExtra(shrStaffId, txtFullName.Text, UserName, txtEmails.Text, staffIsActive);


                foreach (ListItem item in chkListProduct.Items)
                {
                    
                    if (item.Selected)
                    {
                        //Response.Write(item.Value + "<br/>");
                        //Response.Flush();
                        cStaffproductExtra.InsertStaffProductStaffManage(shrStaffId, int.Parse(item.Value));

                    }
                    else
                    {
                        cStaffproductExtra.DeleteStaffProductByStaffIdStaffMange(shrStaffId, int.Parse(item.Value));

                    }
                }
            
            
            //Update Module AND Method
            StaffPageAuthorizeExtra StaffModule = new StaffPageAuthorizeExtra();
            List<object> ListStaffModule = StaffModule.GetModuleByStaffID(shrStaffId);

            
            //if module rate control
            if (chkRateControl.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 2)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                {

                    StaffModule.UpdateStaffModule(shrStaffId, 2, oldMethod, byte.Parse(dropMethodRateControl.SelectedValue));
                }
                else
                    StaffModule.intsertStaffModule(shrStaffId, 2, byte.Parse(dropMethodRateControl.SelectedValue));
            }
            else
            {
                int count = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 2)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                {
                    //Response.Write("HELLO");
                    //Response.End();
                    StaffModule.DeleteStaffModule(shrStaffId, 2, byte.Parse(dropMethodRateControl.SelectedValue));
                }
                
            }


            //if module Member Control
            if (chkMember.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 9)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                {

                    StaffModule.UpdateStaffModule(shrStaffId, 9, oldMethod, byte.Parse(dropMethodMember.SelectedValue));
                }
                else
                    StaffModule.intsertStaffModule(shrStaffId, 9, byte.Parse(dropMethodMember.SelectedValue));
            }
            else
            {
                int count = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 9)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                {
                    //Response.Write("HELLO");
                    //Response.End();
                    StaffModule.DeleteStaffModule(shrStaffId, 9, byte.Parse(dropMethodMember.SelectedValue));
                }

            }

            //if module Package Control
            if (chkPackage.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 8)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                {

                    StaffModule.UpdateStaffModule(shrStaffId, 8, oldMethod, byte.Parse(dropMethodPackageControl.SelectedValue));
                }
                else
                    StaffModule.intsertStaffModule(shrStaffId, 8, byte.Parse(dropMethodPackageControl.SelectedValue));
            }
            else
            {
                int count = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 8)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                {
                    //Response.Write("HELLO");
                    //Response.End();
                    StaffModule.DeleteStaffModule(shrStaffId, 8, byte.Parse(dropMethodPackageControl.SelectedValue));
                }

            }

            //if module Promotion
            if (chkPromotion.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 3)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 3, oldMethod,byte.Parse(dropMethodPromotion.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 3, byte.Parse(dropMethodPromotion.SelectedValue));
            }
            else
            {
                int count = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 3)
                    {
                        count = count + 1;
                        
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 3, byte.Parse(dropMethodPromotion.SelectedValue));

            }


            //if module Allotment
            if (chkAllotment.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 4)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 4, oldMethod,byte.Parse(dropMethodAllotment.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 4, byte.Parse(dropMethodAllotment.SelectedValue));
            }
            else
            {
                int count = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 4)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 4, byte.Parse(dropMethodAllotment.SelectedValue));

            }


            //if module Order Center
            if (chkOrdercenter.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 5)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 5, oldMethod,byte.Parse(dropMethodOrdercenter.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 5, byte.Parse(dropMethodOrdercenter.SelectedValue));
            }
            else
            {
                int count = 0;
                
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 5)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 5, byte.Parse(dropMethodOrdercenter.SelectedValue));

            }

            //if module REview
            if (chkReview.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 7)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 7, oldMethod, byte.Parse(dropMethodreview.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 7, byte.Parse(dropMethodreview.SelectedValue));
            }
            else
            {
                int count = 0;

                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 6)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 6, byte.Parse(dropMethodReport.SelectedValue));

            }


            //if module Report
            if (checkReport.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 6)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 6, oldMethod, byte.Parse(dropMethodReport.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 6, byte.Parse(dropMethodReport.SelectedValue));
            }
            else
            {
                int count = 0;

                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 6)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 6, byte.Parse(dropMethodReport.SelectedValue));

            }

            //if module Newsletter
            if (checkNews.Checked)
            {
                int count = 0;
                byte oldMethod = 0;
                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 10)
                    {
                        count = count + 1;
                        oldMethod = module.MethodId;
                    }
                }

                if (count > 0)
                    StaffModule.UpdateStaffModule(shrStaffId, 10, oldMethod, byte.Parse(dropMethodNews.SelectedValue));
                else
                    StaffModule.intsertStaffModule(shrStaffId, 10, byte.Parse(dropMethodNews.SelectedValue));
            }
            else
            {
                int count = 0;

                foreach (StaffPageAuthorizeExtra module in ListStaffModule)
                {
                    if (module.ModuleID == 10)
                    {
                        count = count + 1;
                    }
                }

                if (count > 0)
                    StaffModule.DeleteStaffModule(shrStaffId, 10, byte.Parse(dropMethodNews.SelectedValue));

            }
            //reset password

            if (!string.IsNullOrEmpty(textnewPass.Text) && !string.IsNullOrEmpty(textnewPassCon.Text))
            {
                Staff.updateStaffPassWord(shrStaffId, textnewPass.Text.Trim());
               
            }

            Response.Redirect(Request.Url.ToString());
        }
    }
}