<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LoansSchedule.aspx.vb"
    Inherits="GOSHRM.LoansSchedule" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=GridRepay.ClientID %>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>
<head>
    <title>Loan Schedule</title>
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
</head>
<body>
        <form>
         <div class="container col-md-12">
        <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                        <asp:Label ID="lblPayLine1" runat="server" BackColor="#1BA691" Font-Names="Verdana"
                                                    Font-Size="14px" Style="color: #FFFFFF" Visible="False" Width="100%">Payment View</asp:Label>
                                                <asp:Label ID="lblPayLine2" runat="server" BackColor="#1BA691" Font-Names="Verdana"
                                                    Font-Size="14px" Style="color: #1BA691" Visible="False" Width="100%">Payment</asp:Label>
                </div>
        </div>
        <div class="panel panel-success">
                    <div class="panel-heading">
                        <h4><b>LOAN SCHEDULE</b></h4>
                    </div>
                    <div class="panel-body">
        <div class="row"> 
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>BORROWER</label>
                                    <input id="lblBorrower" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        LOAN</label>
                                    <input id="lblLoanID" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>LOAN TYPE</label>
                                    <input id="lblLoanType" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
             <asp:Label ID="lblEmpID" runat="server" Visible="False" Font-Names="Verdana" Font-Size="10px"></asp:Label>
        </div>
        <div class="col-md-12 m-t-20 m-b-20 text-center">
                 <asp:Button runat="server" Text="Loan Repayments" CssClas="btn btn-primary btn-success"
                        Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="35px" Width="200px"
                        ID="btnloanrepay" Font-Bold="True"></asp:Button>

                <asp:Button runat="server" Text="Loan Schedule" CssClas="btn btn-primary btn-success" 
                        Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="35px" Width="200px"
                        ID="btnloanschedule" Font-Bold="True"></asp:Button>

                <asp:Button runat="server" Text="Amortised Cost" CssClas="btn btn-primary btn-success"
                        Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="35px" Width="200px"
                        ID="btnamortised" Font-Bold="True"></asp:Button>
            </div>
    <div class="row">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">             
            <asp:View ID="repayment" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>LOAN AMOUNT</label>
                                    <input id="lblRepayLoanAmount" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        FAIR VALUE</label>
                                    <input id="lblRepayFairValue" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>MONTHLY REPAYMENT</label>
                                    <input id="lblRepayMonthly" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-3">
                                <div class="form-group">
                                    <label>REPAYMENT START DATE</label>
                                    <input id="lblRepayStartDate" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-3">
                                <div class="form-group">
                                    <label>TENOR (MONTHS)</label>
                                    <input id="lblRepayTenor" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-3">
                                <div class="form-group">
                                    <label>ANNUAL INTEREST RATE</label>
                                    <input id="lblRepayIntRate" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-3">
                                <div class="form-group">
                                    <label>EFFECTIVE INTEREST RATE</label>
                                    <input id="lblRepayEIR" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                           <div class="col-md-12 m-t-10 m-b-10 text-left">
                                 <asp:Button ID="btnExportPay" runat="server" BorderStyle="None" 
                                                   CssClass="btn btn-info" Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="30px" Text="Export"
                                                    Width="100px"></asp:Button>
                            </div>
                           <div class="col-md-6 form-group">
                                                <label id="lblPaymentDate" runat="server">PAYMENT DATE</label>
                                                <telerik:raddatepicker Skin="Bootstrap" ID="radPaymentDate" runat="server" 
                                                    ForeColor="#666666" Culture="en-US"
                                                    MinDate="" ResolvedRenderMode="Classic" Visible="False" Width="100%">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                                </telerik:raddatepicker>
                            </div>
                            <div class="col-md-6 form-group">
                                    <div class="form-group">
                                                <label id="lblPaymentAmount" runat="server">AMOUNT PAID</label>
                                                <input id="txtPaymentAmount" runat="server" class="form-control" type="text" />
                                        </div>
                            </div>
                            <div style="display:none" class="col-md-6 form-group">
                                                <label id="lblPaymentRate" runat="server">INTEREST RATE</label>
                                                <input id="txtPaymentRate" readonly runat="server" class="form-control" type="text" />
                            </div>
                           <div class="col-md-6 form-group">
                                                <asp:Button ID="btnSavePayment" CssClass="btn btn-info" runat="server" BackColor="#1BA691" BorderStyle="None"
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="30px" Text="Save"
                                                    Visible="False" Width="100px"></asp:Button>
                                                <asp:Button ID="btnClosePayment" CssClass="btn btn-info" runat="server" BackColor="#FF3300" BorderStyle="None"
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="30px" Text="Cancel"
                                                    Visible="False" Width="100px"></asp:Button>
                                                <asp:TextBox ID="txtPaymentID" CssClass="btn btn-info" runat="server" Height="16px" Style="text-align: right"
                                                    Visible="False" Font-Size="3px">0</asp:TextBox>
                            </div>
                        </div>
                            <div class="row">
                                <asp:GridView ID="GridRepay" runat="server" AllowPaging="True" AllowSorting="True"
                                    BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px" CssClass="table table-condensed"
                                    OnRowDataBound="OnRowDataBound" PageSize="30" Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                                    <RowStyle BackColor="white" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmp" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                                    <PagerStyle Font-Names="Verdana" Font-Size="11px"></PagerStyle>
                                    <RowStyle HorizontalAlign="Right" />
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
                            </div>
                            <div class="row">
                             <asp:Label ID="lblPayStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                Style="color: #FF0000"></asp:Label>
                            </div>
                            <div class="row">
                            <div class="col-md-12 m-t-20 text-center">
                                <asp:Button ID="btnRepay" CssClass="btn btn-success" runat="server" BorderStyle="None" ForeColor="White"
                                                Height="30px" Text="Add Payment" Width="150px" Font-Names="Verdana" Font-Size="12px">
                                            </asp:Button>
                                <asp:Button ID="btnDeleteRepay" CssClass="btn btn-danger" runat="server" BorderStyle="None"
                                                ForeColor="White" Height="30px" Text="Delete Payment" Width="150px" Font-Names="Verdana"
                                                Font-Size="12px"></asp:Button>
                            </div>
                            </div>
            </asp:View>
            <asp:View  ID="schedule" runat ="server">
              <div class="row">
                                <div class="col-md-4">
                                <div class="form-group">
                                    <label>LOAN AMOUNT</label>
                                    <input id="lblLoanAmt" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>MONTHLY REPAYMENT</label>
                                    <input id="lblMonthlyPay" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>REPAYMENT START DATE</label>
                                    <input id="lblStartDate" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>TENOR (MONTHS)</label>
                                    <input id="lblTenor" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>ANNUAL INTEREST RATE</label>
                                    <input id="lblInterestRate" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>EFFECTIVE INTEREST RATE</label>
                                    <input id="lblEIR" readonly runat="server" class="form-control" type="text" />
                                </div>
                            </div>
                               <div class="col-md-12 m-t-10 m-b-10 text-left">
                                    <asp:Button ID="btnExportSchedule" CssClass="btn btn-info" runat="server" BorderStyle="None"
                                                Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="30px" Text="Export"
                                                Width="100px" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-condensed"
                                    BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px" Height="50px" OnRowDataBound="OnRowDataBound"
                                    OnSorting="SortRecords" PageSize="50" Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                                    <RowStyle BackColor="white" />
                                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                                    <PagerStyle Font-Names="Verdana" Font-Size="11px" />
                                    <RowStyle HorizontalAlign="Right" />
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
                            </div>

            </asp:View>
            <asp:View ID="amortised" runat="server">
          <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>LOAN AMOUNT</label>
                                <input id="lblAmortLoanAmount" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    FAIR VALUE</label>
                                <input id="lblAmortFairValue" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>MONTHLY REPAYMENT</label>
                                <input id="lblAmortMonthlyPay" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>REPAYMENT START DATE</label>
                                <input id="lblAmortRepayStartDate" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                         <div class=" col-md-3">
                            <div class="form-group">
                                <label>TENOR (MONTHLY)</label>
                                <input id="lblAmortTenor" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>          
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>ANNUAL INTEREST RATE</label>
                                <input id="lblAmortInterestRate" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>MARKET RATE</label>
                                <input id="lblAmortMarketRate" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>EFFECTIVE INTEREST RATE</label>
                                <input id="lblAmortEIR" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                       <div class="col-md-12 m-t-10 m-b-10 text-left">
                                                <asp:Button ID="btnExportAmort" CssClass="btn btn-info" runat="server" BorderStyle="None"
                                                    Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="30px" Text="Export"
                                                    Width="100px" />
                          </div>
                        </div>
                            <div class="row">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"  CssClass="table table-condensed"
                                    BorderStyle="Solid" Font-Names="Verdana" Font-Size="11px" Height="50px" OnRowDataBound="OnRowSurbodinateDataBound"
                                    OnSorting="SortSurbodinateRecords" PageSize="50" Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display">
                                    <RowStyle BackColor="white" />
                                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Right"></RowStyle>
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
                            </div>
                    
            </asp:View>
            
        </asp:MultiView>
    </div>
        <div class="col-md-12 m-t-20 text-center">
<%--             <asp:Button ID="btnClose" runat="server" CssClass="btn btn-info" BorderStyle="None" ForeColor="White"
                                Height="35px" Text="<<Back" Width="100px" Font-Names="Verdana" Font-Size="12px"></asp:Button>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>--%>
             <asp:Button ID="btnBack" runat="server" Height="35px" Text="Back" 
                 BorderStyle="None" CssClass="btn btn-info" ForeColor="White" Width="100px" />
        </div>
        </div>
        </div>
        </div>
        </form>
        
    </body>
</html>
</asp:Content>