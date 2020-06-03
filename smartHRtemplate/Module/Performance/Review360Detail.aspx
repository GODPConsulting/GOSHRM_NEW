<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Review360Detail.aspx.vb" Inherits="GOSHRM.Review360Detail"
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
            
                <asp:Label ID="lblreview" runat="server" Font-Names="Verdana" Font-Size="14px"  Width="100%"
                    Font-Bold="True" ForeColor="#666666"></asp:Label>
            
        </div>
   
         <div>
                        <asp:Button ID="btnBack" runat="server" Text="<< Back" BackColor="#999966" ForeColor="White"
                            Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
         </div>
     
        
        <div>
            <asp:GridView ID="gridTrainers" runat="server" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px" Height="50px" Width="100%"
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" 
                GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" 
                BorderColor="#CCCCCC" PageSize="100" DataKeyNames="id">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>                   
                    <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Row" />                                  
                    <asp:BoundField DataField="appraisalitem" ItemStyle-Width="150px" HeaderText="Appraisal Item" />
                    <asp:BoundField DataField="comments" ItemStyle-Width="150px" HeaderText="Comment" />
                    <asp:BoundField DataField="rating" ItemStyle-Width="80px" HeaderText="Grade Point" />
            
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
   
        </form>
    </body>
</html>
