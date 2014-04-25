using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;

/// <summary>
/// Summary description for SupplierPayment
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public class SupplierPaymentType : Hotels2BaseClass
    {
        public short PaymentType_Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }

        


        /// <summary>
        /// Get SupplierPaymentType By Id ; Returns Instance of SupplierPaymentType
        /// </summary>
        /// <param name="shrTypeId"></param>
        /// <returns></returns>
        public static SupplierPaymentType getSupplierPaymentTypeById(short shrTypeId)
        {
            //declare Instance Linq for Supplier Module
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var result = dcSupplier.tbl_supplier_payment_types.SingleOrDefault(spt => spt.payment_type_id == shrTypeId);

            SupplierPaymentType clSupplierPaymentType = new SupplierPaymentType
            {
                PaymentType_Id = result.payment_type_id,
                Title = result.title,
                Detail = result.detail
            };

            return clSupplierPaymentType;
        }

        /// <summary>
        /// Get list of SupplierPaymentType All ; Returns List Collection of SupplierPaymetnType 
        /// </summary>
        /// <returns></returns>
        public static List<SupplierPaymentType> getSupplierPaymetnTypeListALL()
        {
            //declare Instance Linq for Supplier Module
             LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var result = from spt in dcSupplier.tbl_supplier_payment_types
                         orderby spt.title ascending
                         select spt;
            List<SupplierPaymentType> listSupplierPaymentype = new List<SupplierPaymentType>();

            foreach (var item in result)
            {
                listSupplierPaymentype.Add(new SupplierPaymentType{
                    PaymentType_Id = item.payment_type_id,
                    Title = item.title,
                    Detail= item.detail
                });
            }

            return listSupplierPaymentype;
        }

        /// <summary>
        /// Insert New SupplierPaymentType ; Static Method ; Return int Identity of PaymentypeId
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strDetail"></param>
        /// <returns></returns>
        public static int insertNewSupplierPaymentType(string strTitle, string strDetail)
        {
            try
            {
                //declare Instance Linq for Supplier Module
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                SupplierPaymentType clSupplierPaymentType = new SupplierPaymentType
                {
                    PaymentType_Id = 0,
                    Title = strTitle,
                    Detail = strDetail
                };

                var varInsert = new tbl_supplier_payment_type
                {
                    payment_type_id = clSupplierPaymentType.PaymentType_Id,
                    title = clSupplierPaymentType.Title,
                    detail = clSupplierPaymentType.Detail
                };

                dcSupplier.tbl_supplier_payment_types.InsertOnSubmit(varInsert);
                dcSupplier.SubmitChanges();

                int ret = varInsert.payment_type_id;
                return ret;
            }
            catch
            {
                int ret = 0;
                return ret;
            }

        }

        /// <summary>
        /// Update New SupplierPaymentType ; Static Method ; Returns bool IsUpdated?
        /// </summary>
        /// <param name="shrTypeId"></param>
        /// <param name="strTitle"></param>
        /// <param name="strDetail"></param>
        /// <returns></returns>
        public static bool updateSupplierPaymentType(short shrTypeId, string strTitle, string strDetail)
        {
            try
            {
                //declare Instance Linq for Supplier Module
                LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

                SupplierPaymentType clSupplierPaymentType = new SupplierPaymentType
                {
                    PaymentType_Id = shrTypeId,
                    Title = strTitle,
                    Detail = strDetail
                };

                var varUpdate = dcSupplier.tbl_supplier_payment_types.Single(spt => spt.payment_type_id == clSupplierPaymentType.PaymentType_Id);
                varUpdate.title = clSupplierPaymentType.Title;
                varUpdate.detail = clSupplierPaymentType.Detail;
                dcSupplier.SubmitChanges();

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