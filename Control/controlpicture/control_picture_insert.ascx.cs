using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI.Controls
{
    public partial class Control_controlpicture_control_picture_insert : System.Web.UI.UserControl
    {

        public byte ImageCatId
        {
            get
            {
                if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qimageCat_id))
                {
                    return byte.Parse((this.Page as Hotels2BasePage).qimageCat_id);
                }
                else
                {
                    return 1;
                };
            }
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                IDictionary<int, string> iDic = new Dictionary<int, string>();

                for (int count = 1; count <= 10; count++)
                {
                    iDic.Add(count, count.ToString());
                }
                numpicinsrt.DataSource = iDic;
                numpicinsrt.DataTextField = "Value";
                numpicinsrt.DataValueField = "Key";
                numpicinsrt.DataBind();

                GenerateTable(1, int.Parse(numpicinsrt.SelectedValue));
                //ControlDataBind();
            }
            else
            {
                GenerateTable(1, int.Parse(numpicinsrt.SelectedValue));
                //ControlDataBind();
                
            }
        }

        public void GenerateTable(int colsCount, int rowsCount)
        {
            Table table = new Table();
            table.ID = "tbl_picture_insertBox";
            table.CssClass = "tbl_picture_insertBox";
            Controls.Add(table);

            int intProductId = int.Parse((this.Page as Hotels2BasePage).qProductId);
            byte bytImageCat = 1;
            if (!string.IsNullOrEmpty((this.Page as Hotels2BasePage).qimageCat_id))
            {
                bytImageCat = byte.Parse((this.Page as Hotels2BasePage).qimageCat_id);
            }

             ProductPic cPoductPic = new ProductPic();
            
             int intPicItem = cPoductPic.getProductPicList(bytImageCat, 1, intProductId).Count;
             
            

            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell = new TableCell();
                    Control_controlpicture_control_picture_insert_box tb = (Control_controlpicture_control_picture_insert_box)Page.LoadControl("~/Control/controlpicture/control_picture_insert_box.ascx");

                    tb.ID = "insertBox_" + i + "Col_" + j;
                    tb.NumSeletedValued = (i +intPicItem+ 1).ToString();

                    
                    cell.Controls.Add(tb);
                    row.Cells.Add(cell);
                    if (i % 2 == 0)
                    {
                        cell.CssClass = "Alternatecolors";
                    }
                    else
                    {
                        cell.CssClass = "Alternatecolors_2";
                    }
                }
                table.Rows.Add(row);
            }
            TableRow Buttonrow = new TableRow();
            Button Bottons = new Button();
            Bottons.ID = "BtnSave";
            Bottons.Text = "Save";
            Bottons.SkinID = "Green";
            Bottons.Click +=new EventHandler(Bottons_Click);
            TableCell Buttoncell = new TableCell();

            Buttoncell.Controls.Add(Bottons);
            Buttonrow.Cells.Add(Buttoncell);
            table.Rows.Add(Buttonrow);
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            ViewState["cols"] = 1;
            ViewState["rows"] = int.Parse(numpicinsrt.SelectedValue);
            Control_controlpicture_control_picture_insert tb = (Control_controlpicture_control_picture_insert)this.Parent.FindControl("controlPictureInsertBox");
            Table table = (Table)tb.FindControl("tbl_picture_insertBox");


            if (table != null)
            {
                for (int count = 0; count < table.Rows.Count - 1; count++)
                {
                    Control_controlpicture_control_picture_insert_box insert_box = (Control_controlpicture_control_picture_insert_box)table.Rows[count].Cells[0].FindControl("insertBox_" + count + "Col_0");
                    insert_box.DropCatBinding();
                    insert_box.NumBinding();
                    insert_box.Binding();
                    
                }
            }
         }

        public void ControlDataBind()
        {
            //Control_controlpicture_control_picture_insert_box insert_box = (Control_controlpicture_control_picture_insert_box)Page.LoadControl("~/Control/controlpicture/control_picture_insert_box.ascx");
            Control_controlpicture_control_picture_insert tb = (Control_controlpicture_control_picture_insert)this.Parent.FindControl("controlPictureInsertBox");
            Table table = (Table)tb.FindControl("tbl_picture_insertBox");


            if (table != null)
            {
                for (int count = 0; count < table.Rows.Count - 1; count++)
                {
                    Control_controlpicture_control_picture_insert_box insert_box = (Control_controlpicture_control_picture_insert_box)table.Rows[count].Cells[0].FindControl("insertBox_" + count + "Col_0");
                    insert_box.DropCatBinding();
                    insert_box.NumBinding();
                    insert_box.Binding();

                }
            }
            
        }

        
        public void Bottons_Click(object sender, EventArgs e)
        {
            Control_controlpicture_control_picture_insert tb = (Control_controlpicture_control_picture_insert)this.Parent.FindControl("controlPictureInsertBox");
            Table table = (Table)tb.FindControl("tbl_picture_insertBox");

            int intProductId = int.Parse((this.Page as Hotels2BasePage).qProductId);


            //Create path 
            int Process = Hotels2thailand.Hotels2FolderAndPath.PicturefolderAndPathGenerate(intProductId);
            //Response.Write(Process);
            //Response.End();
            if (Process == 102)
            {
                // error : Destination Folder Name Are Empty!! Please Insert
                Response.Redirect("~/admin/hotels2exception.aspx");
                return;
            }
            if (Process == 103)
            {
                // error : There are no record Location in this product, Please Insert Lacation before
                Response.Redirect("~/admin/hotels2exception.aspx");
                return;
            }
            if (Process == 104)
            {
                // error : Location Folder Name Are Empty!! Please Insert
                Response.Redirect("~/admin/hotels2exception.aspx");
                return;
            }
            if (Process == 0)
            {
                if (table != null)
                {
                    
                    ProductPic cProductPic = new ProductPic();
                    String PicFileName = string.Empty;
                    for (int count = 0; count < table.Rows.Count - 1; count++)
                    {
                        Control_controlpicture_control_picture_insert_box insert_box = (Control_controlpicture_control_picture_insert_box)table.Rows[count].Cells[0].FindControl("insertBox_" + count + "Col_0");
                        PicFileName = Hotels2thailand.Hotels2FolderAndPath.GetPicturePath(intProductId) + insert_box.FileName;
                        switch (this.ImageCatId)
                        {
                            //case image cat == product
                            case 1:
                                cProductPic.InsertNewPicProduct(this.ImageCatId, insert_box.Type_SeletectdValue, intProductId, PicFileName, insert_box.PictureName, Convert.ToByte(count + 1));
                                break;
                            case 2:
                                DropDownList dropOptionList = (DropDownList)this.Parent.FindControl("drpOptionList");
                                cProductPic.InsertNewPicOption(this.ImageCatId, insert_box.Type_SeletectdValue, intProductId, int.Parse(dropOptionList.SelectedValue), PicFileName, insert_box.PictureName, Convert.ToByte(count + 1));
                                break;
                            case 3:
                                DropDownList dropOptionListConstruction = (DropDownList)this.Parent.FindControl("drpConstrucTionList");
                                cProductPic.InsertNewPicConstruction(this.ImageCatId, insert_box.Type_SeletectdValue, intProductId, int.Parse(dropOptionListConstruction.SelectedValue), PicFileName, insert_box.PictureName, Convert.ToByte(count + 1));
                                break;
                            case 4:
                                DropDownList dropOptionListItinerary = (DropDownList)this.Parent.FindControl("dropItinerary");
                                cProductPic.InsertNewPicItinerary(this.ImageCatId, insert_box.Type_SeletectdValue, intProductId, int.Parse(dropOptionListItinerary.SelectedValue), PicFileName, insert_box.PictureName, Convert.ToByte(count + 1));
                                break;
                        }
                        
                        
                    }

                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    Response.Write("ssssss");
                }
            }
        }

       
        
    }
}