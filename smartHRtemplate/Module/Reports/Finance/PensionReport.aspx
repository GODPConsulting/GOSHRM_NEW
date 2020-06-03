<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PensionReport.aspx.vb"
    Inherits="GOSHRM.PensionReport" EnableEventValidation="false" Debug="true" %>

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
                            <h5><b id="pagetitle" runat="server">PENSION REPORT</b></h5>
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
                                <label>DATE</label>
                               <telerik:RadComboBox ID="cbodate" Skin="Bootstrap" runat="server" Width="100%" 
                                    AutoPostBack="True" Filter="Contains" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4 form-group">
                        <label>&nbsp;</label>
                          <div class="row">
                        <div class="col-md-6 col-sm-6 col-xs-6">
                            <asp:Button ID="Button3" CssClass="btn btn-info" runat="server" Text="Export to Excel" ForeColor="White"
                            Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-6">                        
                            <button onserverclick="Button1_Click" id="btnFind" runat="server" class="btn btn-success">Display</button>
                        </div>
                        </div>
                       </div>
              </div>
              <div class="row">
                 
              </div>

                <div class="row">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" Font-Names="Verdana" CssClass="table table-condensed"
                        Font-Size="11px" Height="50px" OnRowDataBound="OnRowDataBound" OnSorting="SortRecords"
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
