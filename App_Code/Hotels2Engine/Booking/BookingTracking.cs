using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BookingTracking
/// </summary>

namespace Hotels2thailand.Booking
{
    public class BookingTracking : Hotels2BaseClass
    {
        public BookingTracking()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string TrackEntry(string entryID, byte intType)
        {

            string sqlEntry = "";
            string strReturn = "";

            if (intType == 1) //booking_id
            {
                sqlEntry = "SELECT ec.title AS entry_cat,pd.title AS page_title,e.date_entry,";
                sqlEntry = sqlEntry + " (SELECT sk.keyword FROM tbl_track_keyword sk WHERE sk.keyword_id=e.keyword_id) AS keyword,";
                sqlEntry = sqlEntry + " (SELECT ss.url FROM tbl_track_keyword sk,tbl_track_search_engine ss WHERE ss.engine_id=sk.engine_id AND sk.keyword_id=e.keyword_id) AS engine,";
                sqlEntry = sqlEntry + " (SELECT sw.url FROM tbl_track_website sw WHERE sw.website_id=e.website_id) AS website,";
                sqlEntry = sqlEntry + " (SELECT sas.url FROM tbl_aff_sites sas WHERE sas.site_id=e.aff_site_id) AS aff_site,";
                sqlEntry = sqlEntry + " (SELECT secc.title FROM tbl_track_visitor_entry_cat_campaign secc WHERE secc.campaign_id=e.campaign_id) AS campaign";
                sqlEntry = sqlEntry + " FROM tbl_track_visitor_entry_booking eb, tbl_track_visitor_entry e, tbl_track_visitor_entry_cat ec, tbl_track_page_define pd";
                sqlEntry = sqlEntry + " WHERE pd.page_id=e.page_id AND ec.entry_cat_id=e.entry_cat_id AND e.entry_id=eb.entry_id AND eb.booking_id=" + entryID;
            }
            else //entry_id
            {
                sqlEntry = "SELECT ec.title AS entry_cat,pd.title AS page_title,e.date_entry,";
                sqlEntry = sqlEntry + " (SELECT sk.keyword FROM tbl_track_keyword sk WHERE sk.keyword_id=e.keyword_id) AS keyword,";
                sqlEntry = sqlEntry + " (SELECT ss.url FROM tbl_track_keyword sk,tbl_track_search_engine ss WHERE ss.engine_id=sk.engine_id AND sk.keyword_id=e.keyword_id) AS engine,";
                sqlEntry = sqlEntry + " (SELECT sw.url FROM tbl_track_website sw WHERE sw.website_id=e.website_id) AS website,";
                sqlEntry = sqlEntry + " (SELECT sas.url FROM tbl_aff_sites sas WHERE sas.site_id=e.aff_site_id) AS aff_site,";
                sqlEntry = sqlEntry + " (SELECT secc.title FROM tbl_track_visitor_entry_cat_campaign secc WHERE secc.campaign_id=e.campaign_id) AS campaign";
                sqlEntry = sqlEntry + " FROM tbl_track_visitor_entry e, tbl_track_visitor_entry_cat ec, tbl_track_page_define pd";
                sqlEntry = sqlEntry + " WHERE pd.page_id=e.page_id AND ec.entry_cat_id=e.entry_cat_id AND e.entry_id=" + entryID;
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlEntry, cn);
                cn.Open();
                IDataReader rd = ExecuteReader(cmd);

                while (rd.Read())
                {
                    strReturn = strReturn + "<tr>\r\n";
                    strReturn = strReturn + "<td>" + rd["entry_cat"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["page_title"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["keyword"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["engine"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["website"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["aff_site"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["campaign"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + Convert.ToDateTime(rd["date_entry"]).ToString("dd MMM yy hh:mm tt") + "</td>\r\n";
                    strReturn = strReturn + "</tr>\r\n";
                }
            }

            return strReturn;
        }

        public string TrackFootPrint(string entryID, byte intType)
        {
            string sqlFoot = "";
            string strReturn = "";
            string strSearch = "";
            DateTime dateTmp = new DateTime(1,1,1);
            double intTime;

            if (intType == 1) //booking_id
            {
                sqlFoot = "SELECT pd.title AS page_title,f.time_in,";
                sqlFoot = sqlFoot + " (SELECT sik.keyword FROM tbl_track_internal_keyword sik, tbl_track_internal_search sis WHERE sis.in_keyword_id=sik.in_keyword_id AND sis.search_id=f.search_id) search_keyword,";
                sqlFoot = sqlFoot + " (SELECT sis.date_check_in FROM tbl_track_internal_search sis WHERE sis.search_id=f.search_id) search_check_in,";
                sqlFoot = sqlFoot + " (SELECT sis.date_check_out FROM tbl_track_internal_search sis WHERE sis.search_id=f.search_id) search_check_out,";
                sqlFoot = sqlFoot + " (SELECT sd.title FROM tbl_track_internal_search sis, tbl_destination sd WHERE sd.destination_id=sis.destination_id AND sis.search_id=f.search_id) search_destination,";
                sqlFoot = sqlFoot + " (SELECT sl.title FROM tbl_track_internal_search sis, tbl_location sl WHERE sl.location_id=sis.destination_id AND sis.search_id=f.search_id) search_location";
                sqlFoot = sqlFoot + " FROM tbl_track_visitor_entry_booking eb, tbl_track_visitor_foot_print f, tbl_track_page_define pd";
                sqlFoot = sqlFoot + " WHERE pd.page_id=f.page_id AND f.entry_id=eb.entry_id AND eb.booking_id=" + entryID;
                sqlFoot = sqlFoot + " ORDER BY time_in ASC";
            }
            else //entry_id
            {
                sqlFoot = "SELECT pd.title AS page_title,f.time_in,";
                sqlFoot = sqlFoot + " (SELECT sik.keyword FROM tbl_track_internal_keyword sik, tbl_track_internal_search sis WHERE sis.in_keyword_id=sik.in_keyword_id AND sis.search_id=f.search_id) search_keyword,";
                sqlFoot = sqlFoot + " (SELECT sis.date_check_in FROM tbl_track_internal_search sis WHERE sis.search_id=f.search_id) search_check_in,";
                sqlFoot = sqlFoot + " (SELECT sis.date_check_out FROM tbl_track_internal_search sis WHERE sis.search_id=f.search_id) search_check_out,";
                sqlFoot = sqlFoot + " (SELECT sd.title FROM tbl_track_internal_search sis, tbl_destination sd WHERE sd.destination_id=sis.destination_id AND sis.search_id=f.search_id) search_destination,";
                sqlFoot = sqlFoot + " (SELECT sl.title FROM tbl_track_internal_search sis, tbl_location sl WHERE sl.location_id=sis.location_id AND sis.search_id=f.search_id) search_location";
                sqlFoot = sqlFoot + " FROM tbl_track_visitor_entry e, tbl_track_visitor_foot_print f, tbl_track_page_define pd";
                sqlFoot = sqlFoot + " WHERE pd.page_id=f.page_id AND f.entry_id=e.entry_id AND e.entry_id=" + entryID;
                sqlFoot = sqlFoot + " ORDER BY time_in ASC";
            }
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlFoot, cn);
                cn.Open();
                IDataReader rd = ExecuteReader(cmd);

                while (rd.Read())
                {
                    if (!String.IsNullOrEmpty(rd["search_check_in"].ToString()))
                    {
                        strSearch = Convert.ToDateTime(rd["search_check_in"]).ToString("dd MMM yy") + "<br />\r\n";
                        strSearch = strSearch + Convert.ToDateTime(rd["search_check_out"]).ToString("dd MMM yy") + "<br />\r\n";
                        strSearch = strSearch + rd["search_destination"].ToString() + "," + rd["search_location"].ToString();
                    }
                    else
                    {
                        strSearch = "";
                    }

                    if (dateTmp != new DateTime(1, 1, 1)) // not for first row
                    {
                        intTime = Convert.ToDateTime(rd["time_in"]).Subtract(dateTmp).TotalSeconds;
                        strReturn = strReturn.Replace("#TimeTmp#", intTime.ToString());
                    }

                    strReturn = strReturn + "<tr>\r\n";
                    strReturn = strReturn + "<td>" + rd["page_title"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>#TimeTmp#</td>\r\n";
                    strReturn = strReturn + "<td>" + strSearch + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["search_keyword"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "</tr>\r\n";

                    dateTmp = Convert.ToDateTime(rd["time_in"]);
                }
            }

            return strReturn;
        }

        public string TrackOtherVisit(string entryID, byte intType)
        {
            string sqlVisitor = "";
            string strReturn = "";
            int intCountVisit = 0;

            if (intType == 1) //booking_id
            {
                sqlVisitor = "SELECT e.entry_id,ec.title AS entry_cat,pd.title AS page_title,e.date_entry,";
                sqlVisitor = sqlVisitor + " (SELECT sk.keyword FROM tbl_track_keyword sk WHERE sk.keyword_id=e.keyword_id) AS keyword,";
                sqlVisitor = sqlVisitor + " (SELECT ss.url FROM tbl_track_keyword sk,tbl_track_search_engine ss WHERE ss.engine_id=sk.engine_id AND sk.keyword_id=e.keyword_id) AS engine,";
                sqlVisitor = sqlVisitor + " (SELECT sw.url FROM tbl_track_website sw WHERE sw.website_id=e.website_id) AS website,";
                sqlVisitor = sqlVisitor + " (SELECT sas.url FROM tbl_aff_sites sas WHERE sas.site_id=e.aff_site_id) AS aff_site,";
                sqlVisitor = sqlVisitor + " (SELECT secc.title FROM tbl_track_visitor_entry_cat_campaign secc WHERE secc.campaign_id=e.campaign_id) AS campaign,";
                sqlVisitor = sqlVisitor + " (SELECT TOP 1 seb.booking_id FROM tbl_track_visitor_entry_booking seb WHERE seb.entry_id=e.entry_id) AS booking_id";
                sqlVisitor = sqlVisitor + " FROM tbl_track_visitor_entry e, tbl_track_visitor_entry_cat ec, tbl_track_page_define pd";
                sqlVisitor = sqlVisitor + " WHERE pd.page_id=e.page_id AND ec.entry_cat_id=e.entry_cat_id ";
                sqlVisitor = sqlVisitor + " AND e.visitor_id=(SELECT se.visitor_id FROM tbl_track_visitor_entry se, tbl_track_visitor_entry_booking seb WHERE se.entry_id=seb.entry_id AND seb.booking_id=" + entryID + ")";
                sqlVisitor = sqlVisitor + " ORDER BY entry_id ASC";
            }
            else //entry_id
            {
                sqlVisitor = "SELECT e.entry_id,ec.title AS entry_cat,pd.title AS page_title,e.date_entry,";
                sqlVisitor = sqlVisitor + " (SELECT sk.keyword FROM tbl_track_keyword sk WHERE sk.keyword_id=e.keyword_id) AS keyword,";
                sqlVisitor = sqlVisitor + " (SELECT ss.url FROM tbl_track_keyword sk,tbl_track_search_engine ss WHERE ss.engine_id=sk.engine_id AND sk.keyword_id=e.keyword_id) AS engine,";
                sqlVisitor = sqlVisitor + " (SELECT sw.url FROM tbl_track_website sw WHERE sw.website_id=e.website_id) AS website,";
                sqlVisitor = sqlVisitor + " (SELECT sas.url FROM tbl_aff_sites sas WHERE sas.site_id=e.aff_site_id) AS aff_site,";
                sqlVisitor = sqlVisitor + " (SELECT secc.title FROM tbl_track_visitor_entry_cat_campaign secc WHERE secc.campaign_id=e.campaign_id) AS campaign,";
                sqlVisitor = sqlVisitor + " (SELECT TOP 1 seb.booking_id FROM tbl_track_visitor_entry_booking seb WHERE seb.entry_id=e.entry_id) AS booking_id";
                sqlVisitor = sqlVisitor + " FROM tbl_track_visitor_entry e, tbl_track_visitor_entry_cat ec, tbl_track_page_define pd";
                sqlVisitor = sqlVisitor + " WHERE pd.page_id=e.page_id AND ec.entry_cat_id=e.entry_cat_id ";
                sqlVisitor = sqlVisitor + " AND e.visitor_id=(SELECT visitor_id FROM tbl_track_visitor_entry se WHERE se.entry_id=" + entryID + ")";
                sqlVisitor = sqlVisitor + " ORDER BY entry_id ASC";
            }

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlVisitor, cn);
                cn.Open();
                IDataReader rd = ExecuteReader(cmd);

                while (rd.Read())
                {
                    intCountVisit = intCountVisit + 1;

                    strReturn = strReturn + "<tr>\r\n";
                    strReturn = strReturn + "<td><a href=\"trackBooking.aspx?entryID=" + rd["entry_id"].ToString() + "\">" + intCountVisit.ToString() + "</a></td>\r\n";
                    strReturn = strReturn + "<td>" + rd["entry_cat"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["page_title"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["keyword"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["engine"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["website"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["aff_site"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["campaign"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "<td>" + Convert.ToDateTime(rd["date_entry"]).ToString("dd MMM yy hh:mm tt") + "</td>\r\n";
                    strReturn = strReturn + "<td>" + rd["booking_id"].ToString() + "</td>\r\n";
                    strReturn = strReturn + "</tr>\r\n";
                }
            }

            return strReturn;
        }
    }
}