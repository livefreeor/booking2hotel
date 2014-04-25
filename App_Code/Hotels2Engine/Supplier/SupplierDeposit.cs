using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;

/// <summary>
/// Summary description for SupplierDeposit
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public class SupplierDeposit : Hotels2BaseClass
    {
        //public SupplierDeposit()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}
        public int DepositId { get; set; }
        public short SupplierId { get; set; }
        public int BookingProductId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

        private DateTime _date_activity;

        public DateTime DateActivity
        {
            get { return _date_activity.Hotels2ThaiDateTime() ; }
            set { _date_activity = value; }
        }


        //public DateTime DateActivity { get; set; }

        //declare Instance Linq for Supplier Module
        LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();


        public bool update()
        {
            return SupplierDeposit.updateSupplierDeposit(this.DepositId, this.SupplierId, this.BookingProductId, this.Amount, this.Comment);
        }
        

        /// <summary>
        /// Get SupplierDeposit List All ; Returns List Collection of SupplierDeposit
        /// </summary>
        /// <returns></returns>
        public List<object> getListSupplierDepositAll()
        {
            var result = from sd in dcSupplier.tbl_supplier_deposits
                         select sd;
            return MappingObjectFromDataContextCollection(result);
        }

        /// <summary>
        /// Get SupplierDeposit List By SupplierId ; Returns List collection of SupplierDeposit
        /// </summary>
        /// <param name="shrSupplierId"></param>
        /// <returns></returns>
        public List<object> getListSupplierDepositBySupplierId(short shrSupplierId)
        {
            var result = from sd in dcSupplier.tbl_supplier_deposits
                         where sd.supplier_id == shrSupplierId
                         select sd;
            return MappingObjectFromDataContextCollection(result);
        }

        /// <summary>
        /// Get SupplierDeposit List By BookingProductId ; List Collection of SupplierDeposit
        /// </summary>
        /// <param name="intBookingPId"></param>
        /// <returns></returns>
        public List<object> getListSupplierDepositByBookingProductId(int intBookingPId)
        {
            var result = from sd in dcSupplier.tbl_supplier_deposits
                         where sd.booking_product_id == intBookingPId
                         select sd;
            return MappingObjectFromDataContextCollection(result);
        }



        /// <summary>
        /// Get Instance of SupplierDeposit By Id; Returns Instance of SupplierDeposit
        /// </summary>
        /// <param name="intDepositId"></param>
        /// <returns></returns>
        public SupplierDeposit getSupplierDepositById(int intDepositId)
        {
            var result = dcSupplier.tbl_supplier_deposits.SingleOrDefault(sd => sd.deposit_id == intDepositId);

            return (SupplierDeposit)MappingObjectFromDataContext(result);
        }

        /// <summary>
        /// Static Method; InsertNewSupplierDeposit; Return int Identity of DepositId
        /// </summary>
        /// <param name="shrSupplierId"></param>
        /// <param name="intBookingPid"></param>
        /// <param name="decAmount"></param>
        /// <param name="strComment"></param>
        /// <returns></returns>
        public static int insertNewSupplierDeposit(short shrSupplierId, int intBookingPid, decimal decAmount, string strComment)
        {
            try
            {
                //declare Instance Linq for Supplier Module
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                SupplierDeposit clSupplierDeposit = new SupplierDeposit
                {
                    DepositId = 0,
                    SupplierId = shrSupplierId,
                    BookingProductId = intBookingPid,
                    Amount = decAmount,
                    Comment = strComment,
                    DateActivity = DateTime.Now
                };

                var varInsert = new tbl_supplier_deposit
                {
                    deposit_id = clSupplierDeposit.DepositId,
                    supplier_id = clSupplierDeposit.SupplierId,
                    booking_product_id = clSupplierDeposit.BookingProductId,
                    amount = clSupplierDeposit.Amount,
                    comment = clSupplierDeposit.Comment,
                    date_activity = clSupplierDeposit.DateActivity
                };

                dcSupplier.tbl_supplier_deposits.InsertOnSubmit(varInsert);
                dcSupplier.SubmitChanges();

                int ret = varInsert.deposit_id;
                return ret;
            }
            catch
            {
                int ret = 0;
                return ret;
            }
             
        }


        public static bool updateSupplierDeposit(int intDepositId, short shrSupplierId, int intBookingPid, decimal decAmount, string strComment)
        {
            try
            {
                //declare Instance Linq for Supplier Module
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                SupplierDeposit clSupplierDeposit = new SupplierDeposit
                {
                    DepositId = intDepositId,
                    SupplierId = shrSupplierId,
                    BookingProductId = intBookingPid,
                    Amount = decAmount,
                    Comment = strComment,
                    DateActivity = DateTime.Now
                };

                var varUpdate = dcSupplier.tbl_supplier_deposits.Single(sd => sd.deposit_id == clSupplierDeposit.DepositId);
                varUpdate.supplier_id = clSupplierDeposit.SupplierId;
                varUpdate.booking_product_id = clSupplierDeposit.BookingProductId;
                varUpdate.amount = clSupplierDeposit.Amount;
                varUpdate.comment = clSupplierDeposit.Comment;
                varUpdate.date_activity = clSupplierDeposit.DateActivity;

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