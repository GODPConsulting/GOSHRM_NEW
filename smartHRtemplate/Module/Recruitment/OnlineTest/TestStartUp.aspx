<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestStartUp.aspx.vb" Inherits="GOSHRM.TestStartUp"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Applicants</title>

      <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <meta name="HandheldFriendly" content="True" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <link href="../../../style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../style/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../style/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="../../../style/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../style/css/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../../../style/css/bootstrap-datetimepicker.min.css" type="text/css" />
    <link rel="stylesheet" href="../../../style/plugins/morris/morris.css" />
    <link href="../../../style/css/style.css" rel="stylesheet" type="text/css" />
      <link href="../../../Style/css/gridview.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllEmp(Checkbox) {
        var gridTrainers = document.getElementById("<%=gridTrainers.ClientID %>");
        for (i = 1; i < gridTrainers.rows.length; i++) {
            gridTrainers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>


 


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
<%-- <script type="text/javascript">
     function SaveMatch() {
         var save_value = document.createElement("INPUT");
         var radJobPosts = document.getElementById("<%=radJobPosts.ClientID %>");
         save_value.type = "hidden";
         save_value.name = "save_value";
         if (confirm("Do you want to Shortlist Candidates for the position " + radJobPosts.Text)) {
             save_value.value = "Yes";
         } else {
             save_value.value = "No";
         }
         document.forms[0].appendChild(save_value);
     }
    </script>--%>
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
<body onunload="window.opener.location=window.opener.location;" style="background: White">
        <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>
        </div>
             
        <div style="text-align: center">
            <asp:Image ID="imgProfile" runat="server" Height="100px"
                        Width="150px" />
        </div>
        <br />
        <div style="border: thin solid #C0C0C0">
            <asp:Label ID="lblPage" runat="server" Font-Names="Candara" Font-Size="Medium" 
                style="text-align: center" Width="100%" Font-Bold="True" 
                ForeColor="#666666"></asp:Label>
        </div>
           
        <div class="container">
        <div id="divalert" runat="server" visible="false" class="alert alert-danger">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    </div>
        <div class="panel panel-success">
            <div class="panel-heading">
                <b id="lbl" runat="server">Welcome</b>
            </div>
            <div class="panel-body">
                <div style="border: thin solid #C0C0C0; text-align: center;">
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <div class="table-responsive">
                <asp:GridView ID="gridTrainers" runat="server" 
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>                                                                     
                        <asp:BoundField DataField="Code"  HeaderText="Code" SortExpression="Code" />                        
                        <asp:BoundField DataField="Title" HeaderText="Job Title" SortExpression="Job Title" />  
                        <asp:BoundField DataField="TestTitle" HeaderText="Test Title" SortExpression="TestTitle" /> 
                        <asp:BoundField DataField="Specialization" HeaderText="Specialization" SortExpression="Specialization" /> 
                        <asp:BoundField DataField="TestStage" HeaderText="Stage" SortExpression="TestStage" />
                        <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <a href="#" onclick='openWindow("<%# Eval("id") %>","<%# Eval("StageNo") %>","<%# Eval("Code") %>");'>
                                Take Test</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=gridTrainers] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
            </div>

                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                function openWindow(code, code2, code3) {
                    window.open("Test.aspx?id=" + code + "&stage=" + code2 + "&jobid=" + code3, "open_window", "menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=yes,resizable=yes,dependent,width=800,height=800,left=0,top=0");
                }
            </script>
        </div>
            </div>
        </div>

      
        
       
        </form>
    </body>
</html>
