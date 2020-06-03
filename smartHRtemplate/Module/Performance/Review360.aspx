<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Review360.aspx.vb" Inherits="GOSHRM.Review360"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllEmp(Checkbox) {
        var gridTrainers = document.getElementById("<%=gridTrainers.ClientID %>");
        for (i = 1; i < gridTrainers.rows.length; i++) {
            gridTrainers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>


<title>Applicants</title>
<link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
     function SaveMatch() {
         var save_value = document.createElement("INPUT");
         var radJobPosts = document.getElementById("<%=lblHeader.ClientID %>");
         save_value.type = "hidden";
         save_value.name = "save_value";
         if (confirm("Do you want to Shortlist Candidates for the position " + radJobPosts.Text)) {
             save_value.value = "Yes";
         } else {
             save_value.value = "No";
         }
         document.forms[0].appendChild(save_value);
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
    .RadPicker
    {
        vertical-align: middle;
    }
    .rdfd_
    {
        position: absolute;
    }
    .RadPicker .rcTable
    {
        table-layout: auto;
    }
    .RadPicker .RadInput
    {
        vertical-align: baseline;
    }
    .RadInput_Default
    {
        font: 12px "segoe ui" ,arial,sans-serif;
    }
    .RadInput
    {
        vertical-align: middle;
    }
    .RadInput .riTextBox
    {
        height: 17px;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-position: 0 0;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif');
    }
    .RadPicker .rcCalPopup
    {
        display: block;
        overflow: hidden;
        width: 22px;
        height: 22px;
        background-color: transparent;
        background-repeat: no-repeat;
        text-indent: -2222px;
        text-align: center;
        -webkit-box-sizing: content-box;
        -moz-box-sizing: content-box;
        box-sizing: content-box;
    }
</style>
<body style="background: White">
    <body onunload="window.opener.location=window.opener.location;" style="height: 317px">
        <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>
        </div>
        <div style="border: thin solid #C0C0C0">
         <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Smaller" 
                    ForeColor="Red" Width="100%"></asp:Label>
        </div>
        <div style="border: thin solid #C0C0C0">
            <asp:Label ID="lblHeader" runat="server" BackColor="#6699FF" Font-Size="Medium" Style="color: #FFFFFF;
                font-weight: 700; font-family: Candara" Width="100%" Font-Names="Arial"></asp:Label>
        </div>
        <div>
            <table>
                <tr>
                    <td class="style22">
                        <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height="20px" BorderColor="#CCCCCC"
                            BorderWidth="1px" TextMode="Search"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                    </td>
                    <td>
                    </td>
                     <td>
                                    

                                    &nbsp;</td>
                     <td>
                <asp:Label ID="Label23" runat="server" Font-Names="Verdana" Font-Size="12px" 
                             Font-Bold="True" ForeColor="#666666"
                    Text="Max. Grade Point:"></asp:Label>
                    </td>
                    <td>
                <asp:Label ID="lblmaxgrade" runat="server" Font-Names="Verdana" Font-Size="12px" 
                            Font-Bold="True" ForeColor="#666666"></asp:Label>
                    </td>
                    <td >
                     
                         &nbsp;</td>
                         <td>
                             &nbsp;</td>
                </tr>
            </table>
        </div>
   
         
     
        
        <div>
            <asp:GridView ID="gridTrainers" runat="server" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px" Height="50px" Width="100%"
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" 
                GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" 
                BorderColor="#CCCCCC" PageSize="100" DataKeyNames="id">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>                   
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Row" />               
                    <asp:TemplateField HeaderText="Reviewer" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Review360Detail.aspx?id={0}&summaryid={1}&reviewer={2}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("appraisalsummaryid").ToString()),HttpUtility.UrlEncode(Eval("Name").ToString())) %>'
                                Text='<%# Eval("Name")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="JobTitle" ItemStyle-Width="100px" HeaderText="Job Title" />
                    <asp:BoundField DataField="Jobgrade" ItemStyle-Width="100px" HeaderText="Job Grade" />
                    <asp:BoundField DataField="Office" ItemStyle-Width="200px" HeaderText="Office" />
                    <asp:BoundField DataField="grade" ItemStyle-Width="80px" HeaderText="Grade" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="score" ItemStyle-Width="80px" HeaderText="Score (%)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="stat" ItemStyle-Width="100px" HeaderText="Complete Stat" />                 
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
   
        </form>
    </body>
</html>
