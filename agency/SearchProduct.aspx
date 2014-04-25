<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchProduct.aspx.cs" Inherits="agency_SearchProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.10.2.min.js"></script>
    <script src="../scripts/darkman_cookies.js"></script>
    <link href="css/style-b2b.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rbtHotel').click(function () {
                $('#form1').attr('target', '_self');
            });
            $('#rbtShow').click(function () {
                $('#form1').attr('target', '_self');
            });
            $('#rbtDayTrip').click(function () {
                $('#form1').attr('target', '_self');
            });
            $('#rbtWaterAct').click(function () {
                $('#form1').attr('target', '_self');
            });
            setCookie("cAgencyID", $('#hdAgencyID').val());
        });
        function setForm()
        {
            $('#form1').attr('target', '_blank');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <asp:HiddenField ID="hdAgencyID" runat="server" />
        <asp:HiddenField ID="hdCatID" runat="server" />
        <div id="wrapper">
            <div id="logo">
                <img src="image/logo.png" />
            </div>
            <br class="clear-all" />
            <div class="boxbody_b2b">
                <div class="box_column1">
                    <span class="step-choose">
                        <h1>Choose Product</h1>
                    </span>
                    <br class="clear-all" />
                    <div style="margin-left: 45px; margin-top: 20px;">
                        <asp:RadioButton ID="rbtHotel" runat="server" value="29" AutoPostBack="true" Checked="true" GroupName="rbtCat" OnCheckedChanged="rbtHotel_CheckedChanged" />
                        <label for="rbtCat1">Hotels</label>
                        <br />
                        <br />
                        <asp:RadioButton ID="rbtShow" runat="server" value="38" AutoPostBack="true" GroupName="rbtCat" OnCheckedChanged="rbtShow_CheckedChanged" />
                        <label for="rbtCat2">Shows</label>
                        <br />
                        <br />
                        <asp:RadioButton ID="rbtDayTrip" runat="server" value="34" AutoPostBack="true" GroupName="rbtCat" OnCheckedChanged="rbtDayTrip_CheckedChanged" />
                        <label for="rbtCat3">Day Trips</label>
                        <br />
                        <br />
                        <asp:RadioButton ID="rbtWaterAct" runat="server" value="36" AutoPostBack="true" GroupName="rbtCat" OnCheckedChanged="rbtWaterAct_CheckedChanged" />
                        <label for="rbtCat4">Water Activities</label>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="box_column2"></div>
                <div class="box_column3">
                    <span class="step-des">
                        <h1>Find Destination</h1>
                    </span>
                    <br class="clear-all" />
                    <h2>Search for hotel2hotel</h2>
                    <div name="doublecombo">
                        <p>
                            <select id="ddlDesination" size="1" class="list_desination" runat="server">
                            </select>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnSearch" runat="server" class="searchproduct" OnClick="btnSearch_Click" OnClientClick="setForm();"/>
                        </p>
                        <p>&nbsp; </p>
                    </div>
                </div>
                <br class="clear-all" />
            </div>

        </div>
    </form>
</body>
</html>
