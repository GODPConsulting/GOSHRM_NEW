<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="LoanDetail.aspx.vb" Inherits="GOSHRM.LoanDetail" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
</head>
<%-- <script type="text/javascript" language="javascript">
    function LoadList()
    {        
        var ds=null;
        ds = <%=listFilter %>;
            $( "#txtName" ).autocomplete({
              source: ds
            });
    }
    </script>--%>
<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div class="container col-md-10">
            <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblloantype" runat="server" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text=": "></asp:Label>
                <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True"></asp:Label>
                 <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Visible="False"></asp:Label>
                <asp:Label ID="lblGradeLevel" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblstatustemp2" runat="server" Font-Size="X-Small" Text="Label" ForeColor="White"
                    Visible="False"></asp:Label>
                    <asp:TextBox ID="txtapproverlevel" runat="server" Width="13px" Style="font-size: medium;
                    font-family: Candara" Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblapprover1comment1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px">Fair Value</asp:Label>
                <asp:Label ID="lblfAIRVALUE" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
                    <asp:Label ID="lblamortisedcost" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Car Loan</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>EMPLOYEE</label>
                                <input id="lblEmpName" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>
                                    LOCATION</label>
                                <input id="lblLocation" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>LOAN DATE</label>
                                <input id="lblloandate" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>LOAN AMOUNT</label>
                                <input id="txtAmount" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label id="lblStatus2" runat="server">FINAL APPROVAL STATUS</label>
                                <input id="lblFinalStatus" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label id="lblguarantor" runat="server"></label>
                                <input id="lblguarantorname" runat="server" class="form-control" type="text" />
                            </div>
                        <div class="col-md-12">
                         <div class="form-group">
                         <label id="lblGuarantorStatus1" runat="server"></label>
                         <input id="lblGuarantorStatus" runat="server" class="form-control" type="text" />
                         </div>
                        </div>
                        <div class="col-md-12">
                         <div class="form-group">
                         <label id="lblgcomment" runat="server"></label>
                         <textarea id="lblguarantorcomment" runat="server" class="form-control" cols="4" rows="5"></textarea>
                        </div></div>
                          <div class="panel-heading">
                            <h5><b>LOAN REPAYMENT</b></h5>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    REPAYMENT START DATE</label>
                                <input type="text" id="lblrepaystartdate" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    REPAYMENT AMOUNT</label>
                               <input type="text" id="lblrepayamount" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    TENOR(MONTHS)</label>
                               <input type="text" id="lblTenor" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    ANNUAL INTEREST RATE</label>
                                <input type="text" id="txtIntRate" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    MARKET RATE</label>
                                <input type="text" id="txtMarketrate" onblur="txtMarketrate_TextChanged" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    LOAN DESCRIPTION</label>
                                <input type="text" id="lblloandesc" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    APPROVAL 1</label>
                                <input type="text" id="lblApprover1" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    APPROVAL 1 STATUS</label>
                                 <input type="text" id="lblapprover1status" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>                       
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    APPROVER 1 COMMENT</label>
                                <textarea id="lblapprover1comment" readonly runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    FAIR VALUE THE LOAN</label>
                                <telerik:RadDropDownList runat="server" ForeColor="#666666"
                                    DefaultMessage="-- Select --" DropDownHeight="100px"
                                                RenderMode="Lightweight" ResolvedRenderMode="Classic" 
                                    BackColor="White" Font-Names="Verdana" Skin="Bootstrap"
                                                Font-Size="12px" Width="100%" ID="radFairValue" Height="73px" 
                                    AutoPostBack="True" 
                                    ToolTip="YES to apply IFRS Calculation in repaying the loan or NO to repay based on amount requested and interest only">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    APPROVAL STATUS</label>                                
                                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" ForeColor="#666666"
                                    Font-Names="Verdana" Font-Size="12px" ID="cboApproval" Skin="Bootstrap" 
                                    Width="100%" AutoPostBack="True">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    APPROVAL COMMENT</label>
                                <textarea id="txtComment" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    FINANCE APPROVAL BY</label>
                                <input type="text" id="lblfinancemember" class="form-control" readonly="" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnStatus" runat="server" onserverclick="btnStatus_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
<%--    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                <asp:Label ID="lblLoanType" runat="server" Font-Bold="True" 
                    Font-Names="Verdana" Font-Size="14px" ForeColor="White"></asp:Label>
                <asp:Label ID="Label10" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True" 
                    Font-Names="Verdana" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:TextBox ID="txtapproverlevel" runat="server" Width="13px" Style="font-size: medium;
                    font-family: Candara" Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtid" runat="server" Width="13px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
                <asp:Label ID="lbleir" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblrepaymode" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Employee" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblEmpName" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Location"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblLocation" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="style8">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Loan Date"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblloandate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Loan Amount"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtAmount" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    BorderColor="#CCCCCC" BorderWidth="1px" Style="text-align: right" Width="30%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label29" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Final Approval Status"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblFinalStatus" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblguarantor" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Guarantor" Font-Bold="True" ForeColor="#666666"
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblguarantorname" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblGuarantorStatus1" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Guarantor Status" Font-Bold="True" ForeColor="#666666"
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblGuarantorStatus" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px" Width="100%"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblgcomment" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Guarantor Comment" Font-Bold="True" ForeColor="#666666"
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblguarantorcomment" runat="server" Font-Names="Verdana" ForeColor="#666666" Font-Size="12px" Width="100%"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
                <asp:Label ID="lblTitle0" runat="server" Font-Bold="True" Text="Loan Repayment" Font-Names="Verdana"
                    Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label21" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Repayment Start Date"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblrepaystartdate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label22" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Monthly Repayment Amount"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblrepayamount" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label28" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Tenor (Months)"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblTenor" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666" ToolTip="Number in Months loan will be completely repaid based on Monthly Payment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label24" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Annual Interest Rate"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtIntRate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Style="text-align: right"
                    Width="30%">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px"  Font-Bold="True" ForeColor="#666666"
                    Text="Market Rate"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtMarketrate" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Style="text-align: right"
                    Width="30%" AutoPostBack="True">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Loan Description"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblloandesc" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Approval 1"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblApprover1" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblStatus1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Approval 1 Status"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblapprover1status" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Approver 1 Comment"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblapprover1comment" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td class="style8">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Fair Value the Loan"></asp:Label>
            </td>
            <td class="style7">
                            <telerik:RadDropDownList runat="server" ForeColor="#666666"
                    DefaultMessage="-- Select --" DropDownHeight="100px"
                                RenderMode="Lightweight" ResolvedRenderMode="Classic" 
                    BackColor="White" Font-Names="Verdana"
                                Font-Size="12px" Width="150px" ID="radFairValue" Height="73px" 
                    AutoPostBack="True" 
                    ToolTip="YES to apply IFRS Calculation in repaying the loan or NO to repay based on amount requested and interest only">
                            </telerik:RadDropDownList>
            </td>
        </tr>
        
        <tr>
            <td class="style8">
                <asp:Label ID="lblapprover1comment1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px">Fair Value</asp:Label>
                <asp:Label ID="lblfAIRVALUE" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblStatus2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Approval Status"></asp:Label>
            </td>
            <td class="style7">
                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px" ID="cboApproval" 
                    Width="150px" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style8" valign="top">
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Approval Comment"></asp:Label>
            </td>
            <td class="style7">
                <asp:TextBox ID="txtComment" runat="server" Width="100%" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" Height="70px" TextMode="MultiLine" BorderColor="#CCCCCC"
                    BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Text="Finance Approval By"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblfinancemember" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                &nbsp;</td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style7">
                <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Visible="False"></asp:Label>
                <asp:Label ID="lblGradeLevel" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblamortisedcost" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Label ID="lblGradeApprover1" runat="server" Font-Names="Verdana" Font-Size="12px"
                    Visible="False"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                <asp:Button ID="btnStatus" runat="server" Text="Update Status" BackColor="#1BA691"
                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" 
                    Font-Names="Verdana" Font-Size="11px" Font-Bold="True" />
            </td>
            <td class="style7">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" Font-Bold="True" />
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
</asp:Content>