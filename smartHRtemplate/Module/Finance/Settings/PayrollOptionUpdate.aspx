<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayrollOptionUpdate.aspx.vb"
    Inherits="GOSHRM.PayrollOptionUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>


    <body>
        <form id="form1">
            <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
                rel="Stylesheet" type="text/css" />
            <div class="container col-md-10">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="3px"
                            Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                        <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px"
                            Font-Bold="True" ForeColor="#666666"
                            Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblauto" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblemail" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblovertimeenabled" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                        <asp:Label ID="lblattendance" runat="server" Text="Label" Visible="False" Width="1px"
                            Font-Size="1px"></asp:Label>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Payroll Option</b></h5>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>COMPANY</label>
                                        <telerik:RadComboBox Skin="Bootstrap" ID="cboCompany" runat="server" EnableCheckAllItemsCheckBox="True"
                                            RenderMode="Lightweight" Width="100%"
                                            AutoPostBack="True" ForeColor="#666666"
                                            Filter="Contains"
                                            Font-Names="Verdana" Font-Size="12px">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>PAYROLL CURRENCY</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="drpCurrency" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%"
                                            ToolTip="currency payroll and other amounts are automatically based on">
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>AUTO APPROVE PAYSLIP:</label>
                                        <asp:RadioButtonList Skin="Bootstrap" ID="rdoAutoApprove" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%">
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No, Payroll must go through an approval process</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                 </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MINIMUM ADJUSTMENT AMOUNT REQUIRING APPROVALS</label>
                                        <input id="txtAmount" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            PAYSLIP CAN BE APPROVED BY</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <telerik:RadComboBox ID="cboApprove" Skin="Bootstrap" runat="server"
                                                    CheckBoxes="True"
                                                    RenderMode="Lightweight" Width="100%" AutoPostBack="True" ForeColor="#666666"
                                                    Filter="Contains"
                                                    Font-Names="Verdana" Font-Size="12px">
                                                </telerik:RadComboBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group">
                                        <telerik:RadListBox ID="lstApprover" runat="server"
                                            ResolvedRenderMode="Classic" BorderStyle="None" ForeColor="#666666"
                                            Enabled="False" Width="100%"
                                            RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana"
                                            Font-Size="12px">
                                            <ButtonSettings TransferButtons="All"></ButtonSettings>
                                            <EmptyMessageTemplate>
                                                None
                                            </EmptyMessageTemplate>
                                        </telerik:RadListBox>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>AUTO EMAIL APPROVED PAYSLIP TO EMPLOYEES</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="radAutoEmailSlips" runat="server" DefaultMessage="--Select--"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px" ForeColor="#666666"
                                            ResolvedRenderMode="Classic"
                                            ToolTip="Automatically send approved payslips to employees immediately">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips"
                                                    Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips"
                                                    Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PER DAY SALARY ADJUSTMENT ON RECRUITS</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="drpAdjustment" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="16px"
                                            ToolTip="generate salary pay for new recruits based on resumption date">
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            MONTHLY SALARIES CALCULATED BASED ON ATTENDANCE</label>
                                        <telerik:RadDropDownList Skin="Bootstrap" ID="radPayOnAttendance" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px"
                                            ResolvedRenderMode="Classic"
                                            ToolTip="base salary pay on monthly attendance">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            OVERTIME PAYMENT ENABLED</label>
                                        <telerik:RadDropDownList ID="radPayOverTime" Skin="Bootstrap" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                                            Font-Names="Verdana" Font-Size="12px" Width="100%" Height="31px"
                                            ResolvedRenderMode="Classic" ToolTip="enabled overtime payment"
                                            AutoPostBack="True">
                                            <Items>
                                                <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime"
                                                    Text="No" Value="No" />
                                                <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime"
                                                    Text="Yes" Value="Yes" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Label ID="lblOvertimePaymentID" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                                Text="Overtime Payment Index:"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtOvertimeIndex" runat="server" Width="70px"
                                                Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                                BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblpaydesc" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                                Text="Overtime Payment = (Basic * (Overtime/WorkShift)) * OverTimeIndex"
                                                Font-Italic="True"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <asp:LinkButton ID="lnkexception" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        ToolTip="Grades exempted from Attendance and Overtime">Job Grades Excluded from Overtime and Attendance</asp:LinkButton>
                                </div>
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnBack_Click" type="submit" style="width: 150px"
                                        class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <table width="100%">
            <tr>
                <td class="style34" colspan="4" style="background-color: #1BA691">
                    <strong>Payroll Option</strong>
                </td>
            </tr>
            <tr>
                <td class="style37">
                </td>
                <td class="style35">                  
                    <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                        ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style31">                    
                    <asp:Label ID="lblauto1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                        Font-Bold="True" ForeColor="#666666"
                        Text="Company:"></asp:Label>
                </td>
                <td class="style35">
                              <telerik:RadComboBox ID="cboCompany" runat="server" EnableCheckAllItemsCheckBox="True"
                                                RenderMode="Lightweight" Width="500px" 
                        AutoPostBack="True" ForeColor="#666666"
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="12px">
                              </telerik:RadComboBox>
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Payroll Currency:" Visible="True"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="drpCurrency" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ToolTip="currency payroll and other amounts are automatically based on">
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style31">                    
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style31">                    
                    <asp:Label ID="lblauto0" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Auto Approve Payslip:" Visible="True"></asp:Label>
                </td>
                <td class="style35">
                    <asp:RadioButtonList ID="rdoAutoApprove" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No, Payroll must be go through an approval process</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style27">
                                 <asp:Label ID="lblauto" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>                                  
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;
                </td>
                <td class="style27">
                    &nbsp;
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" 
                        Font-Bold="True" ForeColor="#666666"
                        Text="0" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">                 
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Payslip can be approved by:" Visible="True"></asp:Label>
                </td>
                <td class="style35">
                       <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                        <ContentTemplate>
                              <telerik:RadComboBox ID="cboApprove" runat="server" 
                                CheckBoxes="True"
                                                RenderMode="Lightweight" Width="500px" AutoPostBack="True" ForeColor="#666666"
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="12px">
                              </telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style27">
                   
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    
                </td>
                <td class="style35">
                            <telerik:RadListBox ID="lstApprover" runat="server" 
                                ResolvedRenderMode="Classic" BorderStyle="None" ForeColor="#666666"
                                Enabled="False" Width="500px" 
                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                    Font-Size="12px">
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                <EmptyMessageTemplate>
                                   None
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                       
                </td>
                <td class="style30">
                </td>
                <td class="style24">
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style30">
                </td>
                <td class="style24">
                </td>
            </tr>
            <tr>
                <td class="style37">
                 <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Auto Email Approved Payslip to Employees:"></asp:Label>
                    
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radAutoEmailSlips" runat="server" DefaultMessage="--Select--"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" ForeColor="#666666"
                        ResolvedRenderMode="Classic" 
                        ToolTip="Automatically send approved payslips to employees immediately">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips" 
                                Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" DropDownList="radAutoEmailSlips" 
                                Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td valign="top" class="style27">
                    
                               <asp:Label ID="lblemail" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label></td>
                    
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    </td>
                <td class="style35">
                    
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">                    
                       <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Minimum Adjustment Amount requiring Approvals:" Visible="True"></asp:Label>
                 </td>
                <td class="style35">
                    
        <asp:TextBox ID="txtAmount" runat="server" Width="195px" ForeColor="#666666"
            Font-Names="Verdana" Font-Size="12px"
            BorderColor="#CCCCCC" BorderWidth="1px" 
                        ToolTip="Minimal Adjustment amount requiring approval before passed"></asp:TextBox>
       
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">
                    </td>
                <td class="style35">
                    
                </td>
                <td valign="top" class="style27">
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Per Day Salary Adjustment on Recruits:" Visible="True"></asp:Label>
                    
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="drpAdjustment" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="16px" 
                        ToolTip="generate salary pay for new recruits based on resumption date">
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            
            <tr>
                <td class="style37">
                      &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style27">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Monthly Salaries calculated based on Attendance:"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radPayOnAttendance" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ResolvedRenderMode="Classic" 
                        ToolTip="base salary pay on monthly attendance">
                        <Items>
                            <telerik:DropDownListItem runat="server" Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                     <asp:Label ID="lblattendance" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>                   
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td class="style37">
                      </td>
                <td class="style35">
                    </td>
                <td class="style27">
                  
                </td>
                <td>
                
                </td>
            </tr>
            <tr>
                <td class="style37">
                      <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                        Text="Overtime Payment enabled:"></asp:Label>
                   
                </td>
                <td class="style35">
                    <telerik:RadDropDownList ID="radPayOverTime" runat="server" DefaultMessage="--Select--" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px" Width="500px" Height="31px" 
                        ResolvedRenderMode="Classic" ToolTip="enabled overtime payment" 
                        AutoPostBack="True">
                        <Items>
                            <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime" 
                                Text="No" Value="No" />
                            <telerik:DropDownListItem runat="server" DropDownList="radPayOverTime" 
                                Text="Yes" Value="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td class="style27">
                                 <asp:Label ID="lblovertimeenabled" runat="server" Text="Label" Visible="False" Width="1px" 
                        Font-Size="1px"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style37">  
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                        <ContentTemplate>
                             <asp:Label ID="lblOvertimePaymentID" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                             Text="Overtime Payment Index:"></asp:Label> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                 
                                           
                </td>
                <td class="style35">
                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                                  <asp:TextBox ID="txtOvertimeIndex" runat="server" Width="70px" 
                            Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                  &nbsp;<asp:Label ID="lblpaydesc" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                             Text="Overtime Payment = (Basic * (Overtime/WorkShift)) * OverTimeIndex" 
                                      Font-Italic="True"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radPayOverTime" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                      &nbsp;</td>
                <td class="style35">
                    <asp:LinkButton ID="lnkexception" runat="server" 
            Font-Names="Verdana"  Font-Size="12px" 
                    ToolTip="Grades exempted from Attendance and Overtime">Job Grades Excluded from Overtime and Attendance</asp:LinkButton>
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                    <asp:Button ID="btnreloadapprover" runat="server" BackColor="White" ForeColor="#666666"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                </td>
                <td class="style35">
                    <asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#999966" ForeColor="White"
                        Width="120px" Height="25px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="12px" />
                   
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
