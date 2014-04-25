<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajax_product_detail.aspx.cs" Inherits="Hotels2thailand.UI.admin_ajax_product_detail" 
EnableViewState="false" EnableViewStateMac="false" EnableEventValidation="false" EnableSessionState="False" %>


    <form id="form1" runat="server">
    <div>
        <div class="product_detail_style">
                <div class="product_detail_style_left">
                    <table >
            <tr>
            <td >
                <h5>Product Name</h5>
            </td>
            <td>
                <asp:TextBox ID="TitleTextBox"  runat="server"  Width="600" ClientIDMode="Static" />
            </td>
            </tr>
            
            <tr>
            <td>
               <h5> Product Code</h5>
            </td>
            <td>
                <asp:TextBox ID="productCodeText" runat="server"  BackColor="#faffbd" ClientIDMode="Static" /> 
            </td>
            </tr>
            <tr>
            <td>
                <h5>Star</h5>
            </td>
            <td>
                <asp:DropDownList id="ddlStar" runat="server"  ClientIDMode="Static">
                    <asp:ListItem>1</asp:ListItem>
                     <asp:ListItem>1.5</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>2.5</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>3.5</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>4.5</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
               <h5>Internet</h5>
            </td>
            <td>
                <asp:RadioButtonList ID="ralistInternet" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td>
               <h5>Internet Free</h5>
            </td>
            <td>
                <asp:RadioButtonList ID="ralistInternetFree" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td>
               <h5> PaymentType</h5>
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentType" ClientIDMode="Static" runat="server" DataSourceID="ObjPaymentType" DataTextField="Value" DataValueField="Key" />
            </td>
            </tr>
            <tr>
            <td>
                <h5>Destination</h5>
            </td>
            <td>
                <asp:DropDownList ID="ddlDestination" runat="server"  ClientIDMode="Static"
                DataSourceID="ObjDestination" DataTextField="Value" DataValueField="Key"  />
            </td>
            </tr>
            </table>
                </div>
                <div class="product_detail_style_right" >
                    <table style="margin-top:40px">
                        <tr>
                        <th>
                           <asp:Image ID="mapicon" ClientIDMode="Static" runat="server" ImageUrl="~/images/googlemapicon.png" /> Google Map
                        </th>
                        </tr>
                        <tr>
                        <td>
                            <h5>Coor Latitude</h5>
                        </td>
                        <td>
                               <asp:TextBox  ClientIDMode="Static" ID="LatitudeTextBox" runat="server"  />
                        </td>
                        </tr>
                        <tr>
                        <td>
                           <h5> Coor Longitude</h5>
                        </td>
                        <td>
                               <asp:TextBox  ClientIDMode="Static" ID="LongitudeTextBox" runat="server"  />
                      
                </td>
            </tr>
            <tr>
            <td>
                <h5>Comment</h5>
            </td>
            <td>
                <asp:TextBox Wrap="True" ClientIDMode="Static" TextMode="MultiLine" ID="commentTextBox" runat="server" Width="350px" Rows="8" Font-Size="Small" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>
                <h5>Hotel Phone</h5>
            </td>
            <td>
                <asp:TextBox Wrap="True" ClientIDMode="Static" ID="txtPhone" runat="server" Width="350px"  Font-Size="Small" ></asp:TextBox>
            </td>
            </tr>
            
            </table>
                </div>
            </div>
            <div style=" clear:both"></div>
            <div style=" text-align:left; width:100%">
            <input type="button" name="btnSave" value="Save Product Detail"   style=" width:200px" id="tbnSave" class="btStyleGreen" onclick="SaveProductInformation();" /></div>
        
    </div>

    <asp:ObjectDataSource ID="ObjDestination" runat="server" 
            SelectMethod="GetDestinationAll" 
            TypeName="Hotels2thailand.Production.Destination"></asp:ObjectDataSource>

           <asp:ObjectDataSource ID="ObjPaymentType" runat="server" 
            SelectMethod="GetPaymentTypeAll" 
            TypeName="Hotels2thailand.Production.PaymentType"></asp:ObjectDataSource>
    </form>

