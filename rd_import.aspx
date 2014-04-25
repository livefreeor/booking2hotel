<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rd_import.aspx.cs" Inherits="rd_import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      HOtelID [HID] (old bookingEngine) :<asp:TextBox ID="txthotelId" runat="server"></asp:TextBox><br /><br />
      ProductId [PID](new bookingEngine) :<asp:TextBox ID="txtProductId" runat="server"></asp:TextBox><br /><br />
      SupplierID [SUPID](new bookingEngine) :<asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
    </div>
        <br /><br />

        <h1>เมื่อคลิ๊กปุ่ม แต่ละ Step แล้วให้รอจนเสร็จ ก่อน แล้วค่อย กดปุ่มต่อไปนะครับ </h1>
        จะรู้ได้ไงว่า เสร็จแล้ว พอกดปุ่ม จะมีตัวหนังสือวิ่ง แจ้ง ว่าทำอะไรไปบ้าง ถ้าเสร็จแล้ว ตัวหนังสือจะหยุดวิ่ง นะครับ 
    <div>
   STEP 1 ###: <asp:Button ID="btntblCustomer" runat="server" OnClick="btntblCustomer_Onclick" Text="tbl_customer" /><br />
        <br />
    
    
    STEP 2 ###:<asp:Button ID="btnReview" runat="server" OnClick="btnReview_Onclick" Text="tbl_review_all" />
        <br />
           <br />
  STEP 3 ###: <asp:Button ID="btnBooking" runat="server" Text="tbl_booking" 
            onclick="btnBooking_Click" />
         <br />
           <br />
      Don't Click!!!  This Button ONY ME!!! <asp:Button ID="btnRecheck" runat="server" OnClick="btnRecheck_Click" Text="Recheck Payment" />
    </div>
    </form>
</body>
</html>
