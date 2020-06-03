
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoanGuarantorSign.aspx.vb" Inherits="GOSHRM.LoanGuarantorSign" EnableEventValidation="false" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI.Gantt" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

    function closeWin() {
        popup.close();   // Closes the new window
    }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FDFDFD;
            font-family: Verdana;
        }
        .lbl
        {
            font-family: Verdana;
            font-size: medium;
        }
        .style2
        {
            font-family: Verdana;
            font-size: small;
            width: 223px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Verdana;
            font-size: medium;
            width: 223px;
        }
        .style6
        {
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 502px;
        }
        .style8
        {
            width: 223px;
        }
        .RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}
        </style>
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
<body onunload = "window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />


    <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">

        <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>

      <tr>

    <td class="style8">
    
        <asp:TextBox ID="txtapproverlevel" runat="server" Width="13px" 
            style="font-size: medium; font-family: Verdana" Font-Names="Verdana" 
            Height="10px" Visible="False"></asp:TextBox>
       
    </td>
    <td class="style7">
       
        <asp:TextBox ID="txtid" runat="server" Width="183px" 
            style="font-size: medium; font-family: Verdana" Font-Names="Verdana" 
            Height="10px" Visible="False"></asp:TextBox>
       
        <asp:Label ID="lblempid" runat="server" Font-Names="Verdana" Font-Size="8px" 
            Visible="False"></asp:Label>
       
        <asp:Label ID="lblapprover" runat="server" Font-Names="Verdana" Font-Size="8px" 
            Visible="False"></asp:Label>
       
        <asp:Label ID="lblguarantor" runat="server" Font-Names="Verdana" Font-Size="8px" 
            Visible="False"></asp:Label>
       
    </td>
    </tr>
    

    <tr>

    <td class="style5">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="True" 
            ForeColor="#666666" Text="Borrower" Font-Size="12px"></asp:Label>
        </td>
    <td class="style7">
       
       <asp:Label ID="lblEmpName" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
       
    </td>
    </tr>

    
    <tr>

    <td class="style8">
    
        <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Office / Department"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblOffice" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        </td>
    </tr>
    
    
 <tr>

    <td class="style8">
    
        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Loan Type"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblLoanType" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>

     
    </td>
    </tr>
    
   <tr>

    <td class="style8">
    
        <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Loan Amount"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblloanamount" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        </td>
    </tr>
    <tr>

    <td class="style8">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Loan Start Date"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblStartDate" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        </td>
    </tr>
      <tr>

    <td class="style8">
    
        <asp:Label ID="Label22" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Monthly Repayment Amount"></asp:Label>
    
    </td>
    <td class="style7">
       

     
        <asp:Label ID="lblrepay" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>

     
        </td>
    </tr>
    <tr>

    <td class="style8">
    
        <asp:Label ID="Label30" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Loan Description"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblDesc" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
    
        </td>
    </tr>
    <tr>

    <td class="style8">
    
        <asp:Label ID="Label29" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Loan Tenor (Months)"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblTenor" runat="server" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana" Width="100%"></asp:Label>
    
        </td>
    </tr>
    <tr>

    <td class="style6" colspan="2" style="background-color: #1BA691" bgcolor="#0066FF">
    
        &nbsp;</td>
    </tr>
    
    <tr>

    <td class="style8" valign=top>
    
        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Bold="True" 
            ForeColor="#666666" Font-Size="12px"
            Text="Comment"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:TextBox ID="txtComment" runat="server" Width="100%" 
            Font-Names="Verdana" ForeColor="#666666" 
            Height="100px" TextMode="MultiLine" Font-Size="12px" 
            BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
       
        </td>
    </tr>
    <tr>

    <td class="style8">
    
        <asp:Label ID="lblStatus1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Acceptance Status"></asp:Label>
    
    </td>
    <td class="style7">
       
                <telerik:RadComboBox runat="server" ResolvedRenderMode="Classic" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px" ID="cboApproval" 
                    Width="150px">
                </telerik:RadComboBox>
     
     
     
            </td>
    </tr>
    <tr>
    <td class="style2" 
            style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
    
        &nbsp;</td>
    <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0" 
            class="style7">
       
    </td>
    </tr>
 <tr>
    <td class="style8">
    
        <asp:Label ID="lblstatustemp2" runat="server" Font-Size="X-Small" Text="Label" 
            ForeColor="White" Visible="False"></asp:Label>
    
    </td>
    <td class="style7">
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style8">
    
                     <asp:Button ID="btnStatus" runat="server" Text="Update Status" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Visible="True" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"/>
                 </td>
    <td class="style7">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px" Font-Bold="True"/>
                 </td>
    </tr>
    </table>
 
    </form>
</body>
</html>
