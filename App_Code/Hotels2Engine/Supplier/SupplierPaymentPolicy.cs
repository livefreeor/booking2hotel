using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for SupplierPayment
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public class SupplierPaymentPolicy : Hotels2BaseClass
    {
        public int PolicyId { get; set; }
        public short SupplierId { get; set; }
        private DateTime _date_start;

        public DateTime DateStart
        {
            get { return _date_start; }
            set { _date_start = value; }
        }

        private DateTime _date_end;

        public DateTime DateEnd
        {
            get { return _date_end; }
            set { _date_end = value; }
        }

        //public DateTime DateStart{ get; set; }
        //public DateTime DateEnd{ get; set; }
        public byte DayAdvance { get; set; }

        //declare Instance Linq for Supplier Module
        LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

        //public bool Update()
        //{
        //    return SupplierPaymentPolicy.updateSupplierPaymentPolicy(this.PolicyId,  this.DateStart, this.DateEnd, this.DayAdvance);
        //}

        /// <summary>
        /// Get SupplierPaymentPolicy List by SupplierId ; Returns List Collection of SupplierPaymentPolicy
        /// </summary>
        /// <param name="shrSupplierId"></param>
        /// <returns></returns>
        public List<SupplierPaymentPolicy> getListSupplierPaymentPolicyBySupplierId(short shrSupplierId)
        {
            List<SupplierPaymentPolicy> listSupplierPaymentPolicy = new List<SupplierPaymentPolicy>();

            
            var result = from spp in dcSupplier.tbl_supplier_payment_policies
                         where spp.supplier_id == shrSupplierId
                         select spp;

            foreach(var item in result)
            {
                listSupplierPaymentPolicy.Add(new SupplierPaymentPolicy{
                    PolicyId = item.policy_id,
                    SupplierId = item.supplier_id,
                    DateStart = item.date_start,
                    DateEnd = item.date_end,
                    DayAdvance = item.day_advance
                });
            }

            return listSupplierPaymentPolicy;
        }

        public static int insertNewSupplierPaymentPolicy(short shrSupplierId, DateTime dDateStart, DateTime dDateEnd, byte bytDayAdvance)
        {
            
                //declare Instance Linq for Supplier Module
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                SupplierPaymentPolicy clSupplierPaymentPolicy = new SupplierPaymentPolicy
                {
                    PolicyId = 0,
                    SupplierId = shrSupplierId,
                    DateStart = dDateStart,
                    DateEnd = dDateEnd,
                    DayAdvance = bytDayAdvance
                };

                var varInsert = new tbl_supplier_payment_policy
                {
                    policy_id= clSupplierPaymentPolicy.PolicyId,
                    supplier_id = clSupplierPaymentPolicy.SupplierId,
                    date_start = clSupplierPaymentPolicy.DateStart,
                    date_end = clSupplierPaymentPolicy.DateEnd,
                    day_advance = clSupplierPaymentPolicy.DayAdvance
                };

                dcSupplier.tbl_supplier_payment_policies.InsertOnSubmit(varInsert);
                dcSupplier.SubmitChanges();

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_payment, StaffLogActionType.Insert, StaffLogSection.Supplier,
                    shrSupplierId, "tbl_supplier_payment_policy", "policy_id,supplier_id,date_start,date_end,day_advance", "policy_id", clSupplierPaymentPolicy.PolicyId);
                //========================================================================================================================================================
                int ret = 1;
                return ret;
            
        }

        //public static bool updateSupplierPaymentPolicy(int intPolicyId, DateTime dDateStart, DateTime dDateEnd, byte bytDayAdvance)
        //{
           
        //        //declare Instance Linq for Supplier Module
        //        LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

        //        SupplierPaymentPolicy clSupplierPaymentPolicy = new SupplierPaymentPolicy
        //        {
        //            PolicyId = intPolicyId,
        //            DateStart = dDateStart,
        //            DateEnd = dDateEnd,
        //            DayAdvance = bytDayAdvance
        //        };

        //        var varUpdate = dcSupplier.tbl_supplier_payment_policies.SingleOrDefault(spp => spp.policy_id == clSupplierPaymentPolicy.PolicyId);

        //        //#Staff_Activity_Log================================================================================================ STEP 1 ==
        //        ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(varUpdate.date_start, varUpdate.date_end, varUpdate.day_advance);
        //        //============================================================================================================================
        //        varUpdate.date_start = clSupplierPaymentPolicy.DateStart;
        //        varUpdate.date_end = clSupplierPaymentPolicy.DateEnd;
        //        varUpdate.day_advance = clSupplierPaymentPolicy.DayAdvance;

        //        dcSupplier.SubmitChanges();
        //        bool ret = true;

        //        //#Staff_Activity_Log================================================================================================ STEP 2 ============
        //        StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_payment, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
        //            "tbl_supplier_payment_policy", "date_start,date_end,day_advance", arroldValue, "policy_id", intPolicyId);
        //        //==================================================================================================================== COMPLETED ========
        //        return ret;
            
            
        //}

        public static bool UpdateSupPaymentPolicy(int intPolicyId, DateTime dDateStart, DateTime dDateEnd, byte bytDayAdvance)
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_payment_policy", "date_start,date_end,day_advance", "policy_id", intPolicyId);
            //============================================================================================================================

            int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_payment_policy SET date_start={0},date_end={1},day_advance={2} WHERE policy_id={3}", dDateStart, dDateEnd, bytDayAdvance, intPolicyId);

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_payment, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_payment_policy", "date_start,date_end,day_advance", arroldValue, "policy_id", intPolicyId);
            //==================================================================================================================== COMPLETED ========
            return (intUpdate == 1);
        }

      
    }
}