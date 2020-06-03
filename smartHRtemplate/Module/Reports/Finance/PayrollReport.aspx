<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayrollReport.aspx.vb"
    Inherits="GOSHRM.PayrollReport" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <html xmlns="http://www.w3.org/1999/xhtml">
  
    <title>Payroll</title>
    <body>
        <form id="form1">
         <div class="container col-md-12">
         <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">PAYROLL REPORT</b></h5>
                        </div>
                     <div class="panel-body">

       <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>COMPANY</label>
                                <telerik:RadComboBox ID="cboCompany" Skin="Bootstrap" runat="server" Width="100%" 
                                    Font-Size="12px" ForeColor="#666666" AutoPostBack="True" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>YEAR</label>
                               <telerik:RadComboBox ID="cboYear" Skin="Bootstrap" runat="server" Width="100%" 
                                    AutoPostBack="True" Filter="Contains" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>PERIOD</label>
                              <telerik:radcombobox ID="cboPeriod" Skin="Bootstrap" Runat="server" Font-Size="11px" 
                                        Height="150px" Width="100%" Font-Names="Verdana" ForeColor="#666666">
                                    </telerik:radcombobox>
                            </div>
                        </div>
                       
              </div>
              <div class="row">
                 <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-sm-6 col-md-3 col-xs-12 form-group pull-right"">
                            <asp:Button ID="Button3" CssClass="btn btn-info" runat="server" Text="Export to Excel" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                     </div>
              </div>

                <div class="row">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="empid" Font-Names="Verdana" CssClass="table table-condensed"
                        Font-Size="10px" Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords"
                        PageSize="50" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                        Width="100%" ForeColor="#666666">
                        <RowStyle BackColor="white" />
                        <Columns>
                            
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
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
                        function openWindow(code, code2, code3) {
                            window.open("SalaryMasterUpdate.aspx?empid=" + code + "&startdate=" + code2 + "&enddate=" + code3, "open_window", "width=600,height=700");
                        }
                    </script>
                </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
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
