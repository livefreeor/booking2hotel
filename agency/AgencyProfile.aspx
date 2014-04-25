<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgencyProfile.aspx.cs" Inherits="agency_AgencyProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style-b2b.css" rel="stylesheet" />
    <script src="../scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function CheckValues() {
            if ($('#txtAgencyname').val().trim() == '') {
                //alert('Agencyname');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtContactname').val().trim() == '') {
                //alert('Contactname');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtAddress').val().trim() == '') {
                //alert('Address');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtPhone').val().trim() == '') {
                //alert('Phone');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtEmail').val().trim() == '') {
                //alert('Email');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtCommission').val().trim() == '') {
                //alert('Commission');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtUsername').val().trim() == '') {
                //alert('Username');
                alert('Please fill in all required information.');
                return false;
            }
            else if ($('#txtPassword').val().trim() == '') {
                //alert('Password');
                alert('Please fill in all required information.');
                return false;
            }
            else {
                return true;
            }
        }

        function ClearData() {
            $('#txtAgencyname').val('');
            $('#txtContactname').val('');
            $('#txtAddress').val('');
            $('#txtPhone').val('');
            $('#txtEmail').val('');
            $('#txtUsername').val('');
            $('#txtPassword').val('');
        }
    </script>
    <style>
        .setLine {
            line-height: 40px;
            width: 100%;
        }

        input[type="text"] {
            width: 80%;
        }

        textarea {
            width: 80%;
            height: 100px;
        }
        .btStyleGreen {
            background-color: #72ac58;
            border: 1px solid #2c5115;
            font-family: Tahoma;
            font-size: 12px;
            height: 22px;
            color: #ffffff;
            font-weight: bold;
            padding: 2px;
            margin: 0px;
            width: 120px;
        }

            .btStyleGreen:hover {
                cursor: pointer;
                background-color: #7eac6a;
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
                <asp:Label ID="lblHeader" CssClass="headertext" runat="server" ></asp:Label>
                <br />
                <hr style="text-align: left; border-bottom-style: solid; color: #cccccc; width: 100%;" />
                <br />
                <table class="setLine">
                    <tr>
                        <td style="width: 150px;">Agency name : * 
                        </td>
                        <td style="width: 50px;"></td>
                        <td>
                            <asp:TextBox ID="txtAgencyname" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contact name : *
                        </td>
                        <td style="width: 50px;"></td>
                        <td>
                            <asp:TextBox ID="txtContactname" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Address : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtAddress" TextMode="MultiLine" MaxLength="200" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Phone : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Commission (%) : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtCommission" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Username : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Password : *
                        </td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status : *
                        </td>
                        <td></td>
                        <td>
                            <asp:RadioButton ID="rbt1" GroupName="rbtStatus" runat="server" Text="Enable" />&nbsp;&nbsp;<asp:RadioButton ID="rbt0" GroupName="rbtStatus"  runat="server" Text="Disable" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return CheckValues();" CssClass="btStyleGreen"></asp:Button>
                            <input type="button" id="btnClear" value="Clear" onclick="ClearData();" class="btStyleGreen"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
