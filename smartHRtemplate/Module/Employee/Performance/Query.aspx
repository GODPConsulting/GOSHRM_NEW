<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Query.aspx.vb"
    Inherits="GOSHRM.Query" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
   
    <title></title>
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
        .style29
        {
            width: 178px;
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
        .style33
        {
            width: 129px;
        }
        .style34
        {
            width: 354px;
        }
    </style>
    <body>
        <form id="form1">
        <div class="container col-md-12">
        <div class="row col-md-12">
            <div class="row col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>     
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Query</b></h5>
                </div>
             <div class="panel-body">
        <div>
            <div>
    
                            <div class="row">
                             <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                <button onserverclick="btnSubFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                            </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px" Height="50px"
                                    OnRowDataBound="OnRowDataBound" OnSorting="SortRecords" PageSize="100" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" 
                                    ToolTip="click row to select record" Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px"
                                     BorderColor="#CCCCCC" CssClass="table table-condensed">                         
                                    <RowStyle BackColor="White" />
                                    <Columns>                                        
                                        <asp:BoundField DataField="Rows" HeaderText="Rows">
                                            <ItemStyle Width="5px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Reporting Officer" SortExpression="ReportingOfficer">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server"  Font-Bold="True" NavigateUrl='<%# string.Format("~/Module/employee/performance/QueryEmployeePage.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=700,height=600,scrollbars,resizable'); return false;"
                                                    Text='<%# Eval("ReportingOfficer")%>' /></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ROStatus" HeaderText="Query Status">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpStatus" HeaderText="Employee Response Status">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QueryDate" HeaderText="Query Date">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpResponseDate" HeaderText="Response Date">
                                        </asp:BoundField>
                                         <asp:BoundField DataField="EmpResponseTime" HeaderText="Response Time">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HRAction" HeaderText="Disciplnary Action">
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center"></HeaderStyle>
                                </asp:GridView>
                                <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                                </script>
                                <script type="text/javascript">

                                    $(function () {
                                        $("[id*=GridView1] td").hover(function () {
                                            $("td", $(this).closest("tr")).addClass("hover_row");
                                        }, function () {
                                            $("td", $(this).closest("tr")).removeClass("hover_row");
                                        })
                                    })
                                </script>                  
                            </div>
                            <div>
                                <table class="style21">
                                    <tr>
                                        <td class="style29">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style29">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                 
           
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
    </style>
</asp:Content>
