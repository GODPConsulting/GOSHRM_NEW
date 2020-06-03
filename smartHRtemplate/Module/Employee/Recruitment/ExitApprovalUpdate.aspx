<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="ExitApprovalUpdate.aspx.vb" Inherits="GOSHRM.ExitApprovalUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

    function closeWin() {
        popup.close();   // Closes the new window
    }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 177px;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 177px;
        }
        .style6
        {
            width: 177px;
        }
        .style7
        {
            width: 423px;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
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
<body onunload = "window.opener.location=window.opener.location;">
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />


    <%--<table width="100%">
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        <strong>Employee Exit</strong></td>
    </tr>

      <tr>

    <td class="style6">
    
        <asp:Label ID="lblmyapproval" runat="server" Font-Names="Verdana" Text="0" 
            Font-Size="12px" Visible="False"></asp:Label>
    
        <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Text="0" 
            Font-Size="12px" Visible="False"></asp:Label>
    
    </td>
    <td >
       
        <asp:TextBox ID="txtid" runat="server" Width="1px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="10px" Visible="False"></asp:TextBox>
       
        <asp:Label ID="lblapproverid" runat="server" Font-Names="Verdana" Text="Employee" 
            Font-Size="12px" Visible="False"></asp:Label>
    
    </td>
    </tr>
    

    <tr>

    <td class="style5">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Employee" 
            Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
        </td>
    <td >
       
        <asp:Label ID="lblemployee" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
     <tr>

    <td class="style5">
    
        <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Text="Position" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
        </td>
    <td >
       
        <asp:Label ID="lblposition" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
     <tr>

    <td class="style5">
    
        <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Text="Office/Department" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
        </td>
    <td >
       
        <asp:Label ID="lbldept" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>

    <tr>

    <td class="style6">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Text="Notice Date" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
    </td>
    <td >
       
        <asp:Label ID="lblnoticedate" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
   
    <tr>

    <td valign=top class="style6">
    
        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
            Text="Exit Date" Font-Size="12px"></asp:Label>
        </td>
    <td >
       
        <asp:Label ID="lblexitdate" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
    <tr>

    <td class="style6" valign="top">
    
        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Reason" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
    </td>
    <td >
       
        <asp:Label ID="lblreason" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
 
    <tr>
    <td class="style6"  >
    
        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="Exit Type" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
        </td>
    <td  >
       
        <asp:Label ID="lblexittype" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
    <tr>
    <td class="style6"  >
    
        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Text="My Approval" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
        </td>
    <td  >
       
                                    <telerik:RadComboBox ID="cboApproval" runat="server" Width="150px" 
                                        ForeColor="#666666">
                                    </telerik:RadComboBox>
       
    </td>
    </tr>
    <tr>

    <td class="style6" valign="top">
    
        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Text="My Comment" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
    </td>
    <td >
       
        <asp:TextBox ID="txtComment" runat="server" Width="100%" ForeColor="#666666"
            Font-Names="Verdana" 
            Height="100px" TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" 
            Font-Size="12px"></asp:TextBox>
       
    </td>
    </tr>
    <tr>

    <td class="style6" valign="top">
    
        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Text="HR Approval" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
    </td>
    <td >
       
        <asp:Label ID="lblhrapproval" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
 
    <tr>
    <td class="style6"  >
    
        <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Text="HR Comment" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
        </td>
    <td  >
       
        <asp:Label ID="lblhrcomment" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
       
    </td>
    </tr>
     <tr>

    <td class="style2">
    
        &nbsp;</td>
    <td >
       
    </td>
    </tr>
 <tr>
    <td class="style6">
    
        <asp:Label ID="lblapprover" runat="server" Font-Names="Verdana" Text="1" 
            Font-Bold="True" ForeColor="#666666"
            Font-Size="12px" Visible="False"></asp:Label>
    
    </td>
    <td >
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" 
            Font-Size="11px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style6">
    
                     <asp:Button ID="btnAdd" runat="server" Text="Save Approval" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="12px"/>
                 </td>
    <td >
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="12px"/>
                 </td>
    </tr>
    </table>--%>
    <div class="row">
    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                    <asp:Label ID="lblmyapproval" runat="server" Font-Names="Verdana" Text="0" 
                    Font-Size="12px" Visible="False"></asp:Label>
    
                    <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Text="0" 
                        Font-Size="12px" Visible="False"></asp:Label>
        
                    <asp:TextBox ID="txtid" runat="server" Width="1px" 
                        style="font-size: medium; font-family: Candara" Font-Names="Candara" 
                        Height="10px" Visible="False"></asp:TextBox>
       
                    <asp:Label ID="lblapproverid" runat="server" Font-Names="Verdana" Text="Employee" 
                        Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblapprover" runat="server" Font-Names="Verdana" Text="1" 
                            Font-Bold="True" ForeColor="#666666" Font-Size="12px" Visible="False"></asp:Label>
                </div>
                </div>
    <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class=" col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title">
                                        Employee Exit</h5>
                                        <div id="divemplink" runat="server" class="row">
                                            <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                                                <p>
                                                    <a href="ExitApprovals"><u>Job Exit </u></a>
                                                    <label>
                                                        >
                                                    </label>
                                                    <a id="A1" href="#"><u>Employee Exit</u></a>
                                                </p>
                                            </div>
                                        </div>
                                    <label id="lbapproval" runat="server">
                                    </label>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                EMPLOYEE</label>
                                            <input id="lblemployee" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                POSITION</label>
                                            <input id="lblposition" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                OFFICE/DEPT</label>
                                            <input id="lbldept" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                NOTICE DATE</label>
                                            <input id="lblnoticedate" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                EXIT DATE</label>
                                           <input type="text" class="form-control" readonly="" id="lblexitdate" runat="server" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                REASON</label>
                                            <input type="text" class="form-control" readonly="" id="lblreason" runat="server" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                EXIT TYPE</label>
                                            <input type="text" class="form-control" readonly="" id="lblexittype" runat="server" />
                                        </div>
                                    </div>
                                </div>                              
                                <div class="row">
                                <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                MY APPROVAL</label>
                                            <telerik:RadComboBox ID="cboApproval" Skin="Bootstrap" runat="server" Width="100%" 
                                                ForeColor="#666666">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                 <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                HR APPROVAL</label>
                                            <input type="text" class="form-control" readonly="" id="lblhrapproval" runat="server" />
                                        </div>
                                    </div>                                   
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                MY COMMENT</label>
                                            <textarea id="txtComment" runat="server" class="form-control" rows="4"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                HR COMMENT</label>
                                            <textarea id="lblhrcomment" readonly="" runat="server" class="form-control" rows="4"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> 
                </div>
                 <div class="row">
                    <div class="col-md-12 m-t-20 text-left">
                        <button id="btsave" runat="server" onserverclick="btnAdd_Click" type="submit" style="width: 150px"
                            class="btn btn-primary btn-success">
                            Save</button>
                        <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                            style="width: 150px" class="btn btn-primary btn-danger">
                            Close</button>
                    </div>
                </div>
 
    </form>
</body>
</html>
</asp:Content>