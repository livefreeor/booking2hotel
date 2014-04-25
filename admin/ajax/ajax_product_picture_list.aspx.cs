using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;


public partial class ajax_product_picture_list : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }

    public string qImageCat
    {
        get { return Request.QueryString["imgcat_id"]; }
    }
    public string qMaxrow
    {
        get { return Request.QueryString["maxrow"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            //Response.Write(Request.Url.ToString());
            Response.Write(SavePicture());
            Response.End();
        }
    }

    public string SavePicture()
    {
        StringBuilder Result = new StringBuilder();

        byte bytimgCat = byte.Parse(this.qImageCat);
        int intProductId = int.Parse(this.qProductId);
        ProductPicType cPicType = new ProductPicType();
        ProductPic cProductPic = new ProductPic();

        Dictionary<byte, string> objList = new Dictionary<byte, string>();
        List<object> picObject = new List<object>();
        switch (bytimgCat)
        {
            //case image cat == product
            case 1:
                objList = cPicType.getPictureTypeIsHaveRecord(bytimgCat, intProductId);
                break;
            case 2:
                string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                if (!string.IsNullOrEmpty(OptionId))
                {
                    objList = cPicType.getPictureTypeIsHaveRecord(bytimgCat, intProductId, int.Parse(OptionId));
                }
                break;
            case 3:
                string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];
                if (!string.IsNullOrEmpty(ConstructionID))
                {
                    objList = cPicType.getPictureTypeIsHaveRecordConstruction(bytimgCat, intProductId, int.Parse(ConstructionID));
                }
                break;
            case 4:
                
                break;
        }
        if (objList.Count > 0)
        {
            Result.Append("<div style=\"margin:5px 0px 0px 0px;padding:0px;\">");
            Result.Append("<input type=\"button\" id=\"btnEdit_Delete\" style=\"float:left;display:none;\" class=\"btStyleRed\" name=\"btnEdit_Delete\" onclick=\"btnEdit_Delete_save();return false;\"  value=\"Delete\"/>&nbsp;");
            Result.Append("<input type=\"button\" id=\"btnEdit_Delete_cancel\" style=\"float:left;display:none;width:80px;;margin:0px 5px 0px 5px;\" class=\"btStyleWhite\" name=\"btnEdit_Delete_cancel\" onclick=\"showEditMode_cancel();return false;\"  value=\"cancel\"/>");

            Result.Append("<input type=\"button\" id=\"btnEdit_save_cancel\" style=\"float:right;display:none;width:80px;margin:0px 5px 0px 5px;\" class=\"btStyleWhite\" name=\"btnEdit_save_cancel\" onclick=\"showEditMode_pti_cancel();return false;\"  value=\"cancel\"/>");
            Result.Append("<input type=\"button\" id=\"btnEdit_save\" style=\"float:right;display:none;\" class=\"btStyleGreen\" name=\"btnEdit_save\" onclick=\"btnEdit_pri_save();return false;\"  value=\"Save\"/>");

            Result.Append("<input type=\"button\" id=\"btnEdit\" style=\"float:left;width:80px;\"  class=\"btStyleRed\" name=\"btnEdit\" onclick=\"showEditMode();return false;\"  value=\"Delete\"/>");
            Result.Append("<input type=\"button\" id=\"btnEdit_pri\" style=\"float:right;width:80px;\"  class=\"btStyle\" name=\"btnEdit\" onclick=\"showEditMode_pti();return false;\"  value=\"Priority\"/>");
            Result.Append("</div>");
        }
        Result.Append("<table id=\"GvPicTypeList\" style=\"width:100%\">");
        foreach (KeyValuePair<byte, string> Item in objList)
        {
            Result.Append("<tr>");
            Result.Append("<td>");

            Result.Append("<p class=\"pic_type_title_style\">" + Item.Value + "</p>");
            

            switch (bytimgCat)
            {
                //case image cat == product
                case 1:
                    picObject = cProductPic.getProductPicList(bytimgCat, Item.Key, intProductId);
                    break;
                case 2:
                    string OptionId = Request.Form["ctl00$ContentPlaceHolder1$drpOptionList"];
                    if (!string.IsNullOrEmpty(OptionId))
                    {
                        picObject = cProductPic.getProductPicList(bytimgCat, Item.Key, intProductId, int.Parse(OptionId));
                    }
                    break;
                case 3:
                    string ConstructionID = Request.Form["ctl00$ContentPlaceHolder1$drpConstrucTionList"];
                    if (!string.IsNullOrEmpty(ConstructionID))
                    {
                        picObject = cProductPic.getProductPicListConstruction(bytimgCat, Item.Key, intProductId, int.Parse(ConstructionID));
                    }
                    break;
                case 4:
                    //cProductPic.getProductPicListItinerary(bytImageCat, TypeId, intProductId, int.Parse(dropItinerary.SelectedValue));
                    break;
            }
            //Table Child Show you for PicList
            int Picnum = 1;
            Result.Append("<table id=\"GVPictureList_" + Item.Key + "\" style=\"width:100%\">");
            foreach (ProductPic picitem in picObject)
            {
                Result.Append("<tr >");
                Result.Append("<td class=\"checkEdit\" style=\"display:none;text-align:center;width:40px;\"><div id=\"chkImg_" + picitem.PicID + "\"  class=\"checkboxCheckedDefaultStyle\"></div><input style=\"display:none;\" name=\"checkPic_Del\" value=\"" + picitem.PicID + "\" type=\"checkbox\" id=\"" + picitem.PicID + "\" /></td>");
                Result.Append("<td>");
                
                Result.Append("<div class=\"pic_item_list\" >");
                Result.Append("<div class=\"pic_item_preview_process\">");
                Result.Append("<p  style=\" float:left; margin:0px 5px 0px 0px; padding:0px 0px 0px 0px; font-size:12px; font-weight:bold\">" + Picnum + "</p>");

                Result.Append("<img src=\"../.." + picitem.PicCode + "\" id=\"imagePreview_" + picitem.PicID + "\" alt=\"\" />");
                Result.Append("</div>");
                Result.Append("<div class=\"pic_item_drop\"  style=\"display:none;\">");
                Result.Append("<span class=\"pic_item_span\">Priority Show :&nbsp;&nbsp;</span>");
                Result.Append("<select  name=\"drop_priority_" + picitem.PicID + "\" class=\"dropStyle\" style=\"width:55px\" >");
                for (int si = 0; si <= 30; si++)
                {
                    if (picitem.Priority == si)
                        Result.Append("<option value=\"" + si + "\" selected=\"selected\" >" + si + "</option>");
                    else
                        Result.Append("<option value=\"" + si + "\" >" + si + "</option>");
                }
                    
                Result.Append("</select>");
                
                //Result.Append("<asp:DropDownList ID=\"dropPic_priority\"  SkinID=\"DropCustomstyle\" AutoPostBack=\"true\" runat=\"server\" OnSelectedIndexChanged=\"dropPic_priority_SelectedIndexChange\"></asp:DropDownList>");
                //Result.Append("<p class=\"pic_item_edit_content\"><asp:LinkButton ID=\"lkDisable\" runat=\"server\"  CommandName=\"disable\" CommandArgument='<%# Eval("PicID") + "," + Container.DataItemIndex  %>' OnClick="lkDisable_OnClick" ></asp:LinkButton>&nbsp;|&nbsp;");
                //Result.Append("<asp:LinkButton ID=\"lkDelete\" runat="server" Text="Delete" CommandName="del" CommandArgument='<%# Eval("PicID") + "," + Container.DataItemIndex  %>' OnClick="lkDelete_OnClick"></asp:LinkButton>");
                //Result.Append("</p>");
                Result.Append("</div>");
                Result.Append("<p class=\"pic_item_picname\"><span class=\"pic_item_span\">Picture Title :</span> " + picitem.Title + "</p>");
                Result.Append("<p class=\"pic_item_filename\"><span class=\"pic_item_span\"> File Name :</span> " + picitem.PicCode.Split('/')[picitem.PicCode.Split('/').Length - 1] + "</p>");
                //if PicTye LargSize
                if (picitem.PicTypeID == 8)
                {
                    ProductPicName cProductPicName = new ProductPicName();
                    string txtEN = string.Empty;
                    string txtTH = string.Empty;
                    //Get For Eng version
                    if (cProductPicName.getProductPicNameByIdAndLang(picitem.PicID, 1) != null)
                    {
                        txtEN = cProductPicName.getProductPicNameByIdAndLang(picitem.PicID, 1).Title;
                    }

                    //Get For Thai Version
                    if (cProductPicName.getProductPicNameByIdAndLang(picitem.PicID, 2) != null)
                    {
                        txtTH = cProductPicName.getProductPicNameByIdAndLang(picitem.PicID, 2).Title;
                    }
                    Result.Append("<a href=\"\" id=\"pictureName_" + picitem.PicID + "\">Insert Picture Title</a>");
                    Result.Append("<div id=\"insertPictureName_Lang_" + picitem.PicID + "\" >");
                    Result.Append("<div >");
                    Result.Append("<p>EN <input type=\"text\" id=\"text_EN_" + picitem.PicID + "\" name=\"text_EN_" + picitem.PicID +"\" style=\"width:700px;padding:2px;\" class=\"TextBox_Extra_normal_small\" value=\"" + txtEN + "\" ></p>");
                    Result.Append("<p>TH <input type=\"text\" id=\"text_TH_" + picitem.PicID + "\" name=\"text_TH_" + picitem.PicID + "\" style=\"width:700px;padding:2px;\" class=\"TextBox_Extra_normal_small\" value=\"" + txtTH + "\" ></p>");
                    Result.Append("<p><input type=\"button\" id=\"btPicLangSave_" + picitem.PicID + "\" onclick=\"SavePictitle('" + picitem.PicID + "');\" value=\"Save\" style=\"width:80px;padding:0px;margin:0px 0px 0px 17px;\" class=\"btStyleGreen\" ></p>");
                    Result.Append("</div>");
                    Result.Append("</div>");
                }
                Result.Append("<div style=\"clear:both\"></div> ");
                Result.Append("</div>");

                Result.Append("</td>");
                Result.Append("</tr>");

                Picnum = Picnum + 1;
            }
            
            Result.Append("</table>");
            Result.Append("<td>");
            Result.Append("</tr>");
        }

        Result.Append("</table>");

        if (objList.Count > 0)
        {
            Result.Append("<div style=\"margin:20px 0px 0px 0px;padding:0px;\">");
            Result.Append("<input type=\"button\" id=\"btnEdit_Delete_foot\" style=\"float:left;display:none;\" class=\"btStyleRed\" name=\"btnEdit_Delete_foot\" onclick=\"btnEdit_Delete_save();return false;\"  value=\"Delete\"/>");
            Result.Append("<input type=\"button\" id=\"btnEdit_Delete_cancel_foot\" style=\"float:left;display:none;width:80px;;margin:0px 5px 0px 5px;\" class=\"btStyleWhite\" name=\"btnEdit_Delete_cancel_foot\" onclick=\"showEditMode_cancel();return false;\"  value=\"cancel\"/>");

            Result.Append("<input type=\"button\" id=\"btnEdit_save_cancel_foot\" style=\"float:right;display:none;width:80px;margin:0px 5px 0px 5px;\" class=\"btStyleWhite\" name=\"btnEdit_save_cancel_foot\" onclick=\"showEditMode_pti_cancel();return false;\"  value=\"cancel\"/>");
            Result.Append("<input type=\"button\" id=\"btnEdit_save_foot\" style=\"float:right;display:none;\" class=\"btStyleGreen\" name=\"btnEdit_save_foot\" onclick=\"btnEdit_pri_save();return false;\"  value=\"Save\"/>");


            Result.Append("<input type=\"button\" id=\"btnEdit_foot\" style=\"float:left;width:80px;\"  class=\"btStyleRed\" name=\"btnEdit\" onclick=\"showEditMode();return false;\"  value=\"Delete\"/>");
            Result.Append("<input type=\"button\" id=\"btnEdit_pri_foot\" style=\"float:right;width:80px;\"  class=\"btStyle\" name=\"btnEdit\" onclick=\"showEditMode_pti();return false;\"  value=\"Priority\"/>");
            Result.Append("</div>");
        }
        return Result.ToString();
    }
    

    
}