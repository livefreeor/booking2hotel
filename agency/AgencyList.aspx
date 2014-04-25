<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgencyList.aspx.cs" Inherits="agency_AgencyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style-b2b.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.10.2.min.js"></script>
    <style>
        a {
            font-weight: bold;
            color: green;
        }

            a:hover {
                font-weight: bold;
                color: yellowgreen;
            }

        .tbDetail {
            line-height: 25px;
        }

            .tbDetail th {
                border-bottom: 2px solid green;
                line-height: 30px;
                background-color: yellowgreen;
            }

            .tbDetail td {
                color: green;
            }

                .tbDetail td a {
                    font-weight: bold;
                }

                    .tbDetail td a:hover {
                        font-weight: bold;
                        color: yellowgreen;
                    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <asp:HiddenField ID="hdAgencyID" runat="server" />
            <div id="logo">
                <img src="image/logo.png" />
            </div>
            <br class="clear-all" />
            <div class="boxbody_b2b">
                <asp:Label ID="lblHeader" CssClass="headertext" runat="server"></asp:Label>
                <br />
                <br />
                <div>
                    <img id="ContentPlaceHolder1_imgPlus" src="../../images/plus.png">
                    <a href="AgencyProfile.aspx" >Add New Agency</a>
                </div>
                <br />
                <asp:GridView ID="gvAgency" runat="server" AllowPaging="True"></asp:GridView>
                <div id="divDetail" runat="server">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
