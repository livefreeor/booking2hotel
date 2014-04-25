using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text;
using Hotels2thailand.Front;

public partial class test_csv_import : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void btnUpload_OnClick(Object sender, EventArgs e)
	{
        String strPath = "~/csv/";
		DataTable dt;

        StringBuilder result = new StringBuilder();
		if(this.fiUpload.HasFile)
		{

            string extension = Path.GetExtension(fiUpload.PostedFile.FileName);
            ///fiUpload.SaveAs(uploadFolder + "Test" + extension);
            this.fiUpload.SaveAs(Server.MapPath(strPath + "Test" + extension));

			//*** Read CSV to DataTable ***//
			dt = CsvCreateDataTable(strPath,"Test" + extension);
			
			//*** Insert to Database ***//
			//InsertToDatabase(dt);
            result.Append("<h1>Total Customer CSV : "+dt.Rows.Count+"</h1>");
            result.Append("<table cellpadding=\"0\" cellspacing=\"1\" style=\"background-color:#cccccc;font-size:11px;\">");
            result.Append("<tr style=\"background-color:#333333;color:#ffffff\">");
            result.Append("<th>N0.</th>");
            foreach (DataColumn column in dt.Columns)
            {
                result.Append("<th>");
                result.Append(column.ColumnName);
                result.Append("<br/><select name=\"SelectImportType\" style=\"font-size:11px;\" class=\"selectType\">");
                result.Append("<option selected=\"selected\" value=\"0#" + column.ColumnName+"\" >not import</option>");
                result.Append("<option value=\"1#" + column.ColumnName+ "\" >Full Name</option>");
                result.Append("<option  value=\"2#" + column.ColumnName+"\" >Email</option>");
                result.Append("<option  value=\"3#" + column.ColumnName+"\" >Date Birth</option>");
                result.Append("<option value=\"4#" + column.ColumnName+"\" >Address</option>");
                result.Append("<option value=\"5#" + column.ColumnName + "\" >Country</option>");
                result.Append("</select>");
                result.Append("</th>");
                 
                
            }
            result.Append("</tr>");

            int count = 1;

            foreach (DataRow row in dt.Rows)
            {
                result.Append("<tr style=\"background-color:#ffffff;\">");
                result.Append("<td style=\"font-size:11px;\">" + count + "<input type=\"checkbox\" name=\"check_record\"  value=\"" + count + "\" checked=\"checked\"  /></td>");
                foreach (DataColumn column in dt.Columns)
                {
                    result.Append("<td ><input style=\"font-size:11px;width:200px;\" type=\"text\"value=\"" + row[column.ColumnName] + "\" name=\"" + column.ColumnName + "_" + count + "\" /></td>");
                }
                result.Append("</tr>");

                count = count + 1;
            }
            result.Append("<table>");
		}

        lblText.Text = result.ToString();
    }
	
	//*** Convert CSV to DataTable ***//
	protected DataTable CsvCreateDataTable(String strPath,String strFilesName)
	{
        OleDbConnection objConn = new OleDbConnection();
		OleDbDataAdapter dtAdapter;
		DataTable dt = new DataTable();
       
       
		String strConnString;
        //Microsoft.ACE.OLEDB.12.0,Microsoft.Jet.OLEDB.4.0
        strConnString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Server.MapPath(strPath) +
		";Extended Properties='TEXT;HDR=Yes;FMT=Delimited;Format=Delimited(,)'";

		objConn = new OleDbConnection(strConnString);
		objConn.Open();

		String strSQL;
		strSQL = "SELECT * FROM " + strFilesName;
		
		dtAdapter = new OleDbDataAdapter(strSQL, objConn);
		dtAdapter.Fill(dt);

		dtAdapter = null;

		objConn.Close();
		objConn = null;

		return dt; //*** Return DataTable ***//
	}



    protected void btnImport_Click(object sender, EventArgs e)
    {
        //Response.Write(Request.Form["SelectImportType"]); 
        //Response.End();

        


        //default not identify
        byte bytCountryID = 241;
        byte bytPrefix = 1;
        int intProductId = int.Parse(Request.Form["productId"]);


        string strFullnam = string.Empty;
        string strEmail = string.Empty;
        string strDateBirth = string.Empty;
        string strAddress = string.Empty;
        string strCountry = string.Empty;
        
           


        foreach (string item in Request.Form["check_record"].Split(','))
        {

            string Valuetoimport = Request.Form["SelectImportType"];

            string[] Val = Valuetoimport.Split(',');

            foreach (string items in Val)
            {
                string[] import = items.Split('#');
                string strImportType = import[0];
                string strImportName = import[1];
                int inttype = int.Parse(strImportType);
                if (inttype > 0)
                {
                    switch (inttype)
                    {
                        case 1:
                            strFullnam = Request.Form[strImportName + "_" + item];
                            break;
                        case 2:
                            strEmail = Request.Form[strImportName + "_" + item];
                            break;
                        case 3:
                            strDateBirth = Request.Form[strImportName + "_" + item];
                            break;
                        case 4:
                            strAddress = Request.Form[strImportName + "_" + item];
                            break;
                        case 5:
                            strCountry = Request.Form[strImportName + "_" + item];
                            break;
                    }
                }
            }


            Response.Write(strFullnam + "-" + strEmail + "-" + strDateBirth + "-" + strAddress + "-" + strCountry + "<br/>");
            Response.Flush();


            Customer cCustomer = new Customer
            {
                CountryID = bytCountryID,
                PrefixID = bytPrefix,
                ProductID = intProductId,
                FullName = strFullnam,
                Email= strEmail,
                DateBirth = DateTime.Now,
                Address = strAddress,
                Password = ""

            };
            cCustomer.InsertMemberWithCheckEmail(cCustomer);
            
        }

        
    }
}