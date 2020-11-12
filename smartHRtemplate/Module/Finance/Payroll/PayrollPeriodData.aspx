<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="PayrollPeriodData.aspx.vb" Inherits="GOSHRM.PayrollPeriodData" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>

<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
                 <div class="container col-md-10">
                      <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Width="3px"  
                            Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                            <asp:Label ID="lblid" runat="server" style="text-align: left; margin-right: 0px;" 
                                            Width="1px" Font-Names="Verdana" Font-Size="9px" 
                            Visible="False"></asp:Label>
                            <asp:Label ID="lblpayotionid" runat="server" style="text-align: left; margin-right: 0px;" 
                                            Width="1px" Font-Names="Verdana" Font-Size="9px" 
                            Visible="False"></asp:Label>
                             <asp:Label ID="lblStart" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="1px"
                            ForeColor="#FF3300" Visible="False"></asp:Label>
                        <asp:Label ID="lblEnd" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="1px"
                            ForeColor="#FF3300" Visible="False"></asp:Label>
                    </div>
                     <div id="content" runat="server">
            <div class="row">          
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">EMPLOYEE PENSION CONTRIBUTION</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>APPROVAL STATUS*</label>
                                  <telerik:RadComboBox ID="cboapprovalstat" Runat="server" 
                                    ResolvedRenderMode="Classic" Skin="Bootstrap" Width="100%" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px">                                   
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>PERIOD*</label>
                                <input id="lblPeriod" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>NET PAY*</label>
                                <input id="lblNetPay" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>COMMENTS*</label>
                                <textarea id="txtcomment" runat="server" class="form-control" cols="4" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>APPROVED BY</label>
                                <input id="lblapprovedby" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>DATE APPROVED</label>
                                <input id="lbldateapproved" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>PAYROLL CREATED BY</label>
                                <input id="lblcreatedby" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>DATE CREATED</label>
                                <input id="lblcreatedon" runat="server" readonly class="form-control" type="text" />
                            </div>
                        </div>
                         <div id="lblPayrollStat" runat="server" class="col-md-4">
                            <div class="form-group">
                                <label>PAYROLL STATUS</label>
                                 <telerik:RadComboBox ID="cbopayrollstat" Runat="server" 
                                    ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                       
                        <div class="col-md-12 m-t-20 text-center">
                            <%--<button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>--%>
                                 <asp:Button ID="btnAdd" runat="server" Text="Save Payroll Stat" 
                                    CssClass ="btn btn-primary btn-info" ForeColor="White"
                                    Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="12px" ToolTip="lock or open Payroll" />
                                <asp:Button ID="btnStat" runat="server" Text="Save" 
                                    CssClass ="btn btn-primary btn-success" ForeColor="White"
                                    Width="150px" Height="35px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="12px" />
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
             </div></div>
            </div>
<%--
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                 <asp:Label ID="lblcompany" runat="server" style="text-align: left; margin-right: 0px;" 
                                    Width="100%" Font-Names="Verdana" Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">

                                <asp:Label ID="lblid" runat="server" style="text-align: left; margin-right: 0px;" 
                                    Width="1px" Font-Names="Verdana" Font-Size="9px" 
                    Visible="False"></asp:Label>
                     <asp:Label ID="lblpayotionid" runat="server" style="text-align: left; margin-right: 0px;" 
                                    Width="1px" Font-Names="Verdana" Font-Size="9px" 
                    Visible="False"></asp:Label>

            </td>
            <td class="style7">
                               
                                <%--<asp:Label ID="lblapproval" runat="server" style="text-align: left" 
                                    Width="1px" Font-Names="Verdana" Font-Size="Small" Visible="false"></asp:Label>
                                     <asp:Label ID="lblstat" runat="server" style="text-align: left" 
                                    Width="1px" Font-Names="Verdana" Font-Size="Small" Visible="false"></asp:Label>

            </td>
        </tr>
        <tr>
            <td valign="top" class="style6">

                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#666666" Text="Period:" style="text-align: left; margin-right: 0px;" 
                                    Width="100px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                                
                                <asp:Label ID="lblPeriod" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                </td>
        </tr>
        <tr>
            <td class="style6">
                               
                                <asp:Label ID="Label3" runat="server" Text="Net Pay:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                    Width="100px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <asp:Label ID="lblNetPay" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                              </td>
        </tr>
        <tr>
            <td class="style6" >
                               
                                <asp:Label ID="Label1" runat="server" Text="Approval Stat:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <telerik:RadComboBox ID="cboapprovalstat" Runat="server" 
                                    ResolvedRenderMode="Classic" Width="150px" DropDownAutoWidth="Enabled" ForeColor="#666666"
                                    DropDownWidth="100px" Font-Names="Verdana" Font-Size="12px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="January" Value="1" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="February" Value="2" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="March" Value="3" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="April" Value="4" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="May" Value="5" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="June" Value="6" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="July" Value="7" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="August" Value="8" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="September" Value="9" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="October" Value="10" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="November" Value="11" 
                                            Owner="cboapprovalstat" />
                                        <telerik:RadComboBoxItem runat="server" Text="December" Value="12" 
                                            Owner="cboapprovalstat" />
                                    </Items>
                                </telerik:RadComboBox>
                                
                                </td>
        </tr>
        <tr>
            <td valign="top" class="style6" >
                               
                                <asp:Label ID="Label9" runat="server" Text="Approval Comment:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <asp:TextBox ID="txtcomment" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                                    BorderStyle="Solid" BorderWidth="1px" Height="60px" TextMode="MultiLine" 
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
                               
                                </td>
        </tr>
        <tr>
            <td class="style6" >
                               
                                <asp:Label ID="Label4" runat="server" Text="Approved By:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <asp:Label ID="lblapprovedby" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                </td>
        </tr>
            <tr>
            <td class="style6" >
                               
                                <asp:Label ID="Label5" runat="server" Text="Date Approved:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <asp:Label ID="lbldateapproved" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                </td>
        </tr>
            <tr>
            <td class="style6" >
                               
                                <asp:Label ID="Label6" runat="server" Text="Payroll Created By:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <asp:Label ID="lblcreatedby" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                </td>
        </tr>
          <tr>
            <td class="style6" >
                               
                                <asp:Label ID="Label7" runat="server" Text="Data Created:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7" >
                               
                                <asp:Label ID="lblcreatedon" runat="server" style="text-align: left; margin-right: 0px;" ForeColor="#666666"
                                    Width="400px" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                </td>
        </tr>
        <tr>
            <td class="style6" >
                               
                                <asp:Label ID="lblPayrollStat" runat="server" Text="Payroll Stat:" style="text-align: left" Font-Bold="True" ForeColor="#666666"
                                     Font-Names="Verdana" Font-Size="12px"></asp:Label>

            </td>
            <td class="style7">
                               
                                <telerik:RadComboBox ID="cbopayrollstat" Runat="server" 
                                    ResolvedRenderMode="Classic" Width="150px" DropDownAutoWidth="Enabled" ForeColor="#666666"
                                    DropDownWidth="100px" Font-Names="Verdana" Font-Size="12px">
                                </telerik:RadComboBox>
                                
                                </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblStart" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300" Visible="False"></asp:Label>
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnStat" runat="server" Text="Save" 
                    BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnAdd" runat="server" Text="Save Payroll Stat" 
                    BackColor="#3399FF" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" ToolTip="lock or open Payroll" />

                     <asp:Button ID="btnCancel" runat="server" Text="Back" 
                    BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" ToolTip="" />
            </td>

        </tr>
    </table>--%>
    </form>
</body>
</html>
</asp:Content>