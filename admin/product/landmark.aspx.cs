using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_landmark : Hotels2BasePage
    {
        //proudct_id
        public string strProductIdQueryString
        {
            get
            {
                return Request.QueryString["pid"];
            }
        }

        //landmark_id
        public string strLandmarkId
        {
            get
            {
                return Request.QueryString["lid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Destination cDestination = new Destination();
                dropDestinationInsert.DataSource = cDestination.GetDestinationAll();
                dropDestinationInsert.DataValueField = "Key";
                dropDestinationInsert.DataTextField = "Value";
                dropDestinationInsert.DataBind();

                dropCat.DataSource = LandmarkCategory.getAllLandmarkCategory();
                dropCat.DataTextField = "Value";
                dropCat.DataValueField = "Key";
                dropCat.DataBind();


                dropLandmarkCat.DataSource = LandmarkCategory.getAllLandmarkCategory();
                dropLandmarkCat.DataTextField = "Value";
                dropLandmarkCat.DataValueField = "Key";
                dropLandmarkCat.DataBind();

                
                dropDestination.DataSource = cDestination.GetDestinationAll();
                dropDestination.DataValueField = "Key";
                dropDestination.DataTextField = "Value";
                dropDestination.DataBind();

                GVDataBind();
            }
        }


        public void GVDataBind()
        {
            byte bytcatId = byte.Parse(dropLandmarkCat.SelectedValue);
            short shrDesid = short.Parse(dropDestination.SelectedValue);
            Landmark cLandMark = new Landmark();
            GVLandmark.DataSource = cLandMark.GetLanfmarkByCatAndDesId(bytcatId,shrDesid);
            GVLandmark.DataBind();
        }
        public void dropLandmarkCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void dropDestination_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void btnSaveInsert_OnClick(object sender, EventArgs e)
        {
           string Title = txtTitle.Text;
           short shrDesId = short.Parse(dropDestinationInsert.SelectedValue);
           byte bytCatId = byte.Parse(dropCat.SelectedValue);
           string strLatitude = txtLatitude.Text;
           string strLongitude = txtLong.Text;

           Landmark cLandmark = new Landmark
           {
               DestinationID = shrDesId,
               LandmarkCategoryID = bytCatId,
               Title = Title,
               Latitude = strLatitude,
               Longitude = strLongitude

           };
           cLandmark.Insert(cLandmark);

           Response.Redirect(Request.Url.ToString());
        }

        public void btnSave_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int intLandmarkId = int.Parse(argument[0]);
            int RowIndex = int.Parse(argument[1]);

            if (btn.CommandName == "landmarkUpdate")
            {
                foreach (GridViewRow item in GVLandmark.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        if (RowIndex == item.RowIndex)
                        {
                            TextBox lbllandmark = GVLandmark.Rows[item.RowIndex].Cells[1].FindControl("lbllandmark") as TextBox;
                            TextBox lbllatitude = GVLandmark.Rows[item.RowIndex].Cells[2].FindControl("lbllatitude") as TextBox;
                            TextBox lbllongtitude = GVLandmark.Rows[item.RowIndex].Cells[3].FindControl("lbllongtitude") as TextBox;
                            TextBox txtENG = GVLandmark.Rows[item.RowIndex].Cells[4].FindControl("lbllandmark") as TextBox;
                            TextBox txtTHai = GVLandmark.Rows[item.RowIndex].Cells[5].FindControl("lbllandmark") as TextBox;
                        }
                    }
                }
            }
        }

        public void GVLandmark_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox lbllandmark = e.Row.Cells[1].FindControl("lbllandmark") as TextBox;
                TextBox lbllatitude = e.Row.Cells[2].FindControl("lbllatitude") as TextBox;
                TextBox lbllongtitude = e.Row.Cells[3].FindControl("lbllongtitude") as TextBox;
                TextBox txtENG = e.Row.Cells[4].FindControl("lbllandmark") as TextBox;
                TextBox txtTHai = e.Row.Cells[5].FindControl("lbllandmark") as TextBox;



            }
        }
    }
}