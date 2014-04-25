using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Hotels2thailand;
using Hotels2thailand.Production;

public partial class admin_facility_generate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {

            ProductFacilitytempalte cFacTemplate = new ProductFacilitytempalte();
            dropTEmplateList.DataSource = cFacTemplate.getFacilityByCatId(1);
            dropTEmplateList.DataTextField = "TitleEn";
            dropTEmplateList.DataValueField = "Fac_id";
            dropTEmplateList.DataBind();

            foreach (ListItem item in dropTEmplateList.Items)
            {
                item.Text = System.Web.HttpUtility.HtmlDecode(item.Text);
            }

            genRecord();
        }
    }

    public void genRecord()
    {
        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_facility_product",cn);
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM tbl_fac_replace_temp WHERE cat_id = 1", cn);
            cn.Open();


            lblrec.Text = cmd.ExecuteScalar().ToString();
            lblrecom.Text = cmd2.ExecuteScalar().ToString();
        }
    }
    public void btnSeach_Onclick(object sender, EventArgs e)
    {
        ArrayList arrFac = new ArrayList();
        string TemplateKeys = dropTEmplateList.SelectedItem.Text.Trim();

        string[] arrTemplateKeys = TemplateKeys.Split(' ');
        string queryKey = "SELECT DISTINCT(title) FROM tbl_facility_product ORDER BY title";
        //string queryKey = "SELECT DISTINCT(title) FROM tbl_facility_product WHERE  title <> '" + TemplateKeys + "' AND (";
        //int count = 0;
        //foreach (string key in arrTemplateKeys)
        //{
        //    if(count > 0)
        //        queryKey = queryKey + " OR ";

        //    queryKey = queryKey + " title LIKE '%" + key + "%'";

        //    count = count + 1;
        //}
        //queryKey = queryKey + ") ORDER BY title";


        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand(queryKey, cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                arrFac.Add(reader[0].ToString());
            }
        }


        chkListFac.DataSource = arrFac;
        chkListFac.DataBind();

        ArrayList arrTitle = new ArrayList();
        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT(title) FROM tbl_fac_replace_temp WHERE cat_id = 1", cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                arrTitle.Add(reader[0].ToString());
            }
        }


        for (int i = 0; i < chkListFac.Items.Count; i++)
        {
            foreach(string strreplace in arrTitle)
            {
                if(chkListFac.Items[i].Text == strreplace)
                {
                     chkListFac.Items[i].Enabled =false;
                   
                }
            }
            
        }

        btnReplace.Visible = true;
    }

    public void btnReplace_Onclick(object sender, EventArgs e)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
        string KeyWordTemplate = dropTEmplateList.SelectedItem.Text.Trim();
        
        var stringToreplace = string.Empty;
        string OldVal = string.Empty;
        
        for (int i = 0; i < chkListFac.Items.Count; i++)
        {
            IDictionary<int, string> IdicFacToReplace = new Dictionary<int, string>();
            if (chkListFac.Items[i].Selected == true)
            {
                stringToreplace = chkListFac.Items[i].Text;
                

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT fac_id, title FROM tbl_facility_product WHERE title = @title", cn);
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = stringToreplace;
                    cn.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IdicFacToReplace.Add((int)reader[0],reader[1].ToString());
                    }
                }

                int IsUpdate = 0;
                foreach (KeyValuePair<int, string> dic in IdicFacToReplace)
                {
                    OldVal = dic.Value;
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_fac_replace_temp (id,cat_id,lang_id,title_old,title)VALUES(@id,@cat_id,@lang_id,@title_old,@title)", cn);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = dic.Key;
                        cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = 1;
                        cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 1;
                        cmd.Parameters.Add("@title_old", SqlDbType.NVarChar).Value = OldVal;
                        cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = KeyWordTemplate;
                        cn.Open();

                        SqlCommand cmd2 = new SqlCommand("UPDATE tbl_facility_product SET title=@title WHERE fac_id =@fac_id", cn);
                        cmd2.Parameters.Add("@fac_id", SqlDbType.Int).Value = dic.Key;
                        cmd2.Parameters.Add("@title", SqlDbType.NVarChar).Value = KeyWordTemplate;

                        IsUpdate = cmd2.ExecuteNonQuery();

                        if(IsUpdate == 1)
                            cmd.ExecuteNonQuery();
                    }
                }
                
            }

        }

        Response.Redirect(Request.Url.ToString());
        Response.End();
        
    }
}