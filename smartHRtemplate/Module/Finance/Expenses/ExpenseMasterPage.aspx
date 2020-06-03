<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ExpenseMasterPage.aspx.vb"
    Inherits="GOSHRM.ExpenseMasterPage" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Employee Salary</title>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function ConfirmGenerate() {
            var confirm_gen = document.createElement("INPUT");
            confirm_gen.type = "hidden";
            confirm_gen.name = "confirm_gen";
            if (confirm("Refresh per Employee to include new Salary Items?")) {
                confirm_gen.value = "Yes";
            } else {
                confirm_gen.value = "No";
            }
            document.forms[0].appendChild(confirm_gen);
        }
    </script>
      <script type="text/javascript">
          function ConfirmRefresh() {
              var confirm_ref = document.createElement("INPUT");
              confirm_ref.type = "hidden";
              confirm_ref.name = "confirm_ref";
                if (confirm("refresh entire selected company's Employee Pay from Grade Pay Structure?")) {
                  confirm_ref.value = "Yes";
              } else {
                  confirm_ref.value = "No";
              }
              document.forms[0].appendChild(confirm_ref);
          }
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        .button
        {
            background-color: #008CBA; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
        .style104
        {
            width: 256px;
        }
                        .RadDropDownTree {
                            text-align: left;
                            white-space: nowrap;
                            display:inline-block;
                            width:100%;}.RadDropDownTree_Default{color:#333;font:normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownTree{margin:0;padding:0;*zoom:1;display:inline-block;*display:inline;text-align:left;vertical-align:middle;white-space:nowrap;cursor:default
        } 
                        .RadDropDownTree .rddtInner {
                            border: 1px solid;
                            display: block;
                            height: 16px;
                            padding: 2px 18px 2px 5px;
                            position: relative;
                        }
                        .RadDropDownTree_Default .rddtInner{border-radius:3px;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');_background-image:none;border-color:#8a8a8a;color:#333;background-color:#e8e8e8;background-image:-webkit-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);background-image:-moz-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);background-image:-ms-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);background-image:-o-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);background-image:linear-gradient(top,#faf9f9 0,#e8e8e8 100%)}.RadDropDownTree .rddtInner{vertical-align:top}.RadDropDownTree .rddtInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownTree_Default .rddtEmptyMessage{color:#a5a5a5;font-style:italic}.RadDropDownTree .rddtEmptyMessage{font-style:italic}.RadDropDownTree_Default .rddtIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Common.Sprites.radActionsSprite.png');background-position:-1px -20px}.RadDropDownTree .rddtIcon{width:18px;height:20px;border:0;background:0;background-repeat:no-repeat;position:absolute;top:0;right:0;left:auto}
        .style106
        {
            width: 117px;
        }
        .style107
        {
            width: 219px;
        }
        .style108
        {
            width: 180px;
        }
    .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 225px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline;
            margin-left: 0px;
        }.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style109
        {
            width: 163px;
        }
    </style>
    <body>
        <form id="form1">
       
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div style="height: 163px">
            <div>
                <%-- <asp:BoundField DataField="Location" ItemStyle-Width="9%" HeaderText="Location" SortExpression="location" />--%><%--</telerik:RadMultiPage>--%>
                <div>
                    <div>
                        <table>
                            <tr>
                                <td  align="right">
                                    <asp:Label ID="Label3" runat="server" Font-Size="10px" Text="Company" 
                                        Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="style104" valign="top">
               

     
                        <telerik:RadComboBox runat="server" 
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="300px" ID="radOffice" AutoPostBack="True" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                        </telerik:RadComboBox>

     
                                </td>
                                <td class="style108">
                                    <asp:CheckBox ID="chkActive" runat="server" 
                                        Font-Names="Verdana" Text="Active Pay Grade" Font-Size="10px" 
                                        Visible="False" Font-Bold="True" ForeColor="#666666" />
                                </td>
                                <td class="style109">
                                    <asp:Button ID="btnImport" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Reset " Width="140px" 
                                        ToolTip="Reset entire Employee expense structures" Font-Names="Verdana" 
                                        Font-Size="11px" onclientclick="ConfirmRefresh()" />
                                </td>
                                <td>
                                    <asp:Button ID="btnRegenerate" runat="server" BackColor="#3399FF" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Reset for New Items" Width="140px" ToolTip="Click to  generate Salary Schedule on first start or to add new pay item to employee payslip"
                                        OnClientClick="ConfirmGenerate()" Font-Names="Verdana" Font-Size="11px"/>
                                </td>
                                 <td class="style107">
                                    <asp:TextBox ID="txtsearch" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Height="20px" Width="100%" TextMode="Search"></asp:TextBox>
                                </td>
                                <td class="style106">
                                    <asp:Button ID="btnFind" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                                        Height="21px" Text="View" Width="100px" Font-Names="Verdana" Font-Size="11px"/>
                                </td>
                                <td class="style108">
                                    <asp:Button ID="btnExport" runat="server" Text="Export" BackColor="#FF9933" ForeColor="White"
                                        Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                        Font-Size="11px"/>
                                </td>
                                <td >
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" Font-Size="11px"/>
                                    <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Upload File" Width="100px" 
                                        ToolTip="CSV File: EMPID, Item, Amount" Font-Names="Verdana" 
                                        Font-Size="11px" />
                                </td>
                                <td >
                                    
                                    <asp:CheckBox ID="chkIsTransposed" runat="server" AutoPostBack="True" 
                                        Font-Names="Verdana" Text="Items are transposed" Font-Size="11px" 
                                        Font-Bold="True" 
                                        ToolTip="ie the expense items are transposed" />
                                    
                                </td>
                            </tr>
                            </table>
                       
                    </div>
            
                </div>
                <div>
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="10px"
                        Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="2000"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record" Width="100%"
                        AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="1px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Rows" SortExpression="rows" />
                            <asp:TemplateField HeaderText="Emp ID" ItemStyle-Width="40px" ItemStyle-Font-Bold="true"
                                SortExpression="employee no">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Expenses/ExpenseMasterUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                        onclick="window.open (this.href, 'popupwindow',  'width=800,height=800,scrollbars,resizable'); return false;"
                                        Text='<%# Eval("Employee No")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" ItemStyle-Width="120px" HeaderText="Name" SortExpression="name" />
                            <asp:BoundField DataField="Grade" ItemStyle-Width="100px" HeaderText="Grade" SortExpression="grade" />
                            <asp:BoundField DataField="PBT" ItemStyle-Width="80px" HeaderText="Total" SortExpression="pbt" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Company" ItemStyle-Width="100px" HeaderText="Company" SortExpression="Company" />
                            <asp:BoundField DataField="Office" ItemStyle-Width="200px" HeaderText="Office" SortExpression="office" />
                           <%-- <asp:BoundField DataField="Location" ItemStyle-Width="9%" HeaderText="Location" SortExpression="location" />--%>                            
                         
                            <asp:BoundField DataField="Job Status" ItemStyle-Width="80px" HeaderText="Job Status"
                                SortExpression="job status" />
                            <asp:BoundField DataField="Created On" ItemStyle-Width="80px" HeaderText="Created On"
                                SortExpression="created on" />
                            <asp:BoundField DataField="Is Salary Generated" ItemStyle-Width="80px" HeaderText="Generated"
                                ItemStyle-HorizontalAlign="Center" SortExpression="is salary generated" />
                        </Columns>
                        <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">








                    </script>
                    <script type="text/javascript">









                        $(function () {
                            $("[id*=GridVwHeaderChckbox] td").hover(function () {
                                $("td", $(this).closest("tr")).addClass("hover_row");
                            }, function () {
                                $("td", $(this).closest("tr")).removeClass("hover_row");
                            })
                        })
                    </script>
                    <script type="text/javascript">




                        function openWindow(code) {
                            window.open("SalaryMasterUpdate.aspx?id=" + code, "open_window", "width=600,height=700");
                        }
                    </script>
                </div>
                <%--</telerik:RadMultiPage>--%>
                <%--</telerik:RadMultiPage>--%>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    </asp:Content>
