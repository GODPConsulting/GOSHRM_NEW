
    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="AdjustmentUpdate.aspx.vb" Inherits="GOSHRM.AdjustmentUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }   

    </script>
</head>
<body>
     <form>
        <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblref" runat="server" Font-Bold="True" ForeColor="White" 
                    Font-Names="Verdana" Visible="False" Font-Size="16px"></asp:Label>
                    <asp:Label ID="lblID" Visible="False" runat="server" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    <asp:Label ID="lblpayoption" Visible="False" runat="server" Font-Names="Verdana" 
                    Font-Size="12px"></asp:Label>
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">PAYROLL ADJUSTMENT</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                     <div class="col-md-12">
                            <div class="form-group">
                                <label>COMPANY/DEPT</label>
                                 <input id="lblcompany" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>EMPLOYEE*</label>
                                 <telerik:radcombobox Skin="Bootstrap" runat="server" 
                                    ResolvedRenderMode="Classic" ForeColor="#666666"
                                    Font-Names="Verdana" ID="cboemployee" 
                                    Width="100%" Filter="Contains" autopostback="True">
                                </telerik:radcombobox>
                                 <input id="emp1" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>PAYSLIP LINE DESCRIPTION*</label>
                                <input id="txtDesc" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>ADJUSTMENT TYPE</label>
                                <telerik:raddropdownlist Skin="Bootstrap" ID="cboAdj" runat="server" 
                                    DefaultMessage="-- Select --" ForeColor="#666666"
                                    Font-Names="Verdana"  Height="16px" Width="100%"
                                    RenderMode="Lightweight" ResolvedRenderMode="Classic">
                                    <Items>
                                        <telerik:dropdownlistitem runat="server" Text="Deduction" Value="Deduction" 
                                            DropDownList="cboAdj" />
                                        <telerik:dropdownlistitem runat="server" Text="Earning" Value="Earning" 
                                            DropDownList="cboAdj" />
                                    </Items>
                                </telerik:raddropdownlist>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    AMOUNT</label>
                                <input id="txtAmount" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>PAYMENT DATE*</label>
                                <telerik:raddatepicker Skin="Bootstrap" ID="datPayDate" Width="100%" 
                                    runat="server" RenderMode="Lightweight" ForeColor="#666666"
                                    ToolTip="Payslip date adjustment will be added">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="100%"
                                        RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:raddatepicker>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    IS TAXABLE</label>
                                <telerik:radcombobox Skin="Bootstrap" runat="server" 
                                    ResolvedRenderMode="Classic" ForeColor="#666666"
                                    Font-Names="Verdana"  ID="cboTaxable" 
                                    Width="100%">
                                </telerik:radcombobox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    NOTE</label>
                                <input id="txtnote" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    APPROVAL STATUS</label>
                                <telerik:radcombobox Skin="Bootstrap" runat="server" 
                                    ResolvedRenderMode="Classic" ForeColor="#666666"
                                    Font-Names="Verdana" ID="cboApproval" 
                                    Width="100%" AutoPostBack="True">
                                </telerik:radcombobox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    APPROVED BY</label>
                                <input id="lblApprovedBy" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    APPROVED UPDATED</label>
                                <input id="lblApprovedOn" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    UPDATED BY</label>
                                <input id="lblupdatedby" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    UPDATED ON</label>
                                <input id="lblupdatedon" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                                <asp:Button ID="btnStatus" runat="server" Text="Update Status" 
                                BackColor="#3399FF" Height="35px" ForeColor="White" CssClass="btn btn-primary btn-info"
                                Width="150px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="12px" />
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
    </form>
</body>
</html>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

</asp:Content>
