<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pro_score_update.aspx.cs" Inherits="extranet_promotion_pro_score_update" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="update_score_factor" runat="server" text="Update ScoreFactor" OnClick="update_score_factor_Onclick" />

        <asp:Button ID="Update_Score" runat="server" Text="Update Score" OnClick="Update_Score_Onclick" />

        <asp:Button ID="Update_score_benefit" runat="server" Text="Update Score Benefit" OnClick="Update_score_benefit_Onclick" />



        <br />
        <asp:TextBox ID="txtSample" runat="server" TextMode="MultiLine" Rows="10" Columns="100"></asp:TextBox>
        <asp:Button ID="btnShow" runat="server" Text="Show Benefit"  OnClick="btnShow_Onclick" />
        <asp:Label ID="lblScore"  runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblBenefitREsult" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
