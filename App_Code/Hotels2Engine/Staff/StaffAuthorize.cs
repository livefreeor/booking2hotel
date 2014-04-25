using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;

/// <summary>
/// Summary description for StaffAuthorize 
/// inlcuded tbl_authorize_staff and tbl_staff_authorize From LinqStaffDataContext
/// </summary>

namespace Hotels2thailand.Staffs
{

    public class StaffAuthorize:Hotels2BaseClass
    {

        public int AuhorizeId { get; set; }
        public string AuthorizeTitle { get; set; }
        public int StaffId { get; set; }

        
        //===============================================================================================
        //================================= Authorize Staff (Category) ==================================

        //declare Instant Linq for Staff_Category Module
        LinqStaffDataContext dcStaffAuth = new LinqStaffDataContext();

        /// <summary>
        /// get List Authorize_staff (Category of Authorize); static method ; Returns List Collection of StaffAuthorize
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<StaffAuthorize> getListAuthorizeStaff()
        {
            LinqStaffDataContext dcStaffAuth = new LinqStaffDataContext();

            List<StaffAuthorize> listAuth = new List<StaffAuthorize>();
            var result = from auth in dcStaffAuth.tbl_authorize_staffs
                         select auth;

            foreach (var item in result)
            {
                listAuth.Add(new StaffAuthorize { AuhorizeId = item.authorize_id, AuthorizeTitle = item.title });
            }

            return listAuth;
        }

        /// <summary>
        /// get Authorize_Staff by Id; Static Method; Return instant of StaffAuthorize
        /// </summary>
        /// <param name="intAuthId"></param>
        /// <returns></returns>
        public static StaffAuthorize getAuthorizeStaffById(int intAuthId)
        {
            LinqStaffDataContext dcStaffAuth = new LinqStaffDataContext();

            var result = dcStaffAuth.tbl_authorize_staffs.SingleOrDefault(auth => auth.authorize_id == intAuthId);

            return new StaffAuthorize
            {
                AuhorizeId = result.authorize_id,
                AuthorizeTitle = result.title
            };

        }

        //===============================================================================================
        //================================= Staff Authorize =============================================


        /// <summary>
        /// get completed List Of StaffAuthorize ; Static Method ; Returns List Collection of StaffAuthorize
        /// </summary>
        /// <param name="intStaffId"></param>
        /// <returns></returns>
        public static List<StaffAuthorize> getListStaffAuthorizeAllById(int intStaffId)
        {
            LinqStaffDataContext dcStaffAuth = new LinqStaffDataContext();

            List<StaffAuthorize> listAuth = new List<StaffAuthorize>();
            var result = from auth in dcStaffAuth.tbl_staff_authorizes
                         from auth1 in dcStaffAuth.tbl_authorize_staffs
                         where auth.authorize_id == auth1.authorize_id && auth.staff_id == intStaffId
                         select new { auth.staff_id, auth.authorize_id, auth1.title };

            foreach (var item in result)
            {
                listAuth.Add(new StaffAuthorize 
                { 
                    StaffId = item.staff_id, 
                    AuhorizeId = item.authorize_id, 
                    AuthorizeTitle = item.title 
                });
            }

            return listAuth;
        }


        /// <summary>
        /// Delete StaffAuthorize By Staff_Id And Authorize_Id ; 
        /// Static method ; Returns Bool 
        /// </summary>
        /// <param name="shrStaffId"></param>
        /// <param name="bytAuthId"></param>
        /// <returns></returns>
        public static bool deleteStaffAuthorize(short shrStaffId, byte bytAuthId)
        {
            try
            {
                LinqStaffDataContext dcStaffAuth = new LinqStaffDataContext();

                var delAuth = dcStaffAuth.tbl_staff_authorizes.Single(auth => auth.staff_id == shrStaffId && auth.authorize_id == bytAuthId);

                dcStaffAuth.tbl_staff_authorizes.DeleteOnSubmit(delAuth);
                dcStaffAuth.SubmitChanges();

                bool ret = true;
                return ret;
            }
            catch
            {
                bool ret = false;
                return ret;
            }
        }

    }
}