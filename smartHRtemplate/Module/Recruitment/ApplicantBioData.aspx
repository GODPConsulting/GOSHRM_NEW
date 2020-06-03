<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ApplicantBioData.aspx.vb"
    Inherits="GOSHRM.ApplicantBioData" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
   <link href="../../style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../style/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../style/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="../../style/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../style/css/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../../style/css/bootstrap-datetimepicker.min.css" type="text/css" />
    <link rel="stylesheet" href="../../style/plugins/morris/morris.css" />
    <link href="../../style/css/style.css" rel="stylesheet" type="text/css" />
      <link href="../../Style/css/gridview.css" rel="stylesheet" type="text/css" />

       <script type="text/javascript">
           function Confirm() {
               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Save Medical Info and sign declaration?")) {
                   confirm_value.value = "Yes";
               } else {
                   confirm_value.value = "No";
               }
               document.forms[0].appendChild(confirm_value);
           }
    </script>

    <style type="text/css">
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style6
        {
            width: 13px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        #Text1
        {
            height: 22px;
        }
        .style9
        {
        }
        .RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}
        .style10
        {
            width: 5%;
            height: 59px;
        }
        .style11
        {
            height: 59px;
        }
        .style12
        {
            width: 75%;
            height: 59px;
        }
        .style13
        {
            width: 5%;
            height: 4px;
        }
        .style14
        {
            width: 90%;
            height: 4px;
        }
        .style15
        {
            width: 39%;
        }
        .style16
        {
            width: 5%;
            height: 28px;
        }
        .style17
        {
            height: 28px;
        }
        </style>
</head>
<body style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div >
             <table width="100%">
                            <tr>
                                <td >
                                    <asp:Label ID="Label75" runat="server" Font-Names="Verdana" Font-Size="14px" 
                                        
                                        Text="Note: Review your responses before clicking Save Button, inputs will not be editable after clicking Save &amp; Next button" 
                                        Font-Italic="True" ForeColor="#666666"></asp:Label>
                                </td>
                            </tr>
                        </table> 
        </div>
        <div style ="text-align:center">
        
                                    
                                    
                                    <asp:Label ID="lblname" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                        Font-Size="15px" ForeColor="#666666"></asp:Label>
                                    
                                    
                                    
        </div>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                   
                    </td>
                    <td style="width: 40%">
                       
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    <asp:TextBox ID="txtmin" runat="server" Height="1px" Width="1px" BorderColor="White"
                                        BorderWidth="1px"></asp:TextBox>
                                    <asp:Label ID="lblApplicantID" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Visible="False"></asp:Label>
                                    <asp:Label ID="lblBankID" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Visible="False"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblbankstatus" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="14px" Text="BANK ACCOUNT"
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="lbljobpost" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:Label ID="Label78" runat="server" Font-Italic="True" Font-Names="Verdana" 
                                        Font-Size="12px" 
                                        Text="Note: select Others and type in your Bank Name if your Bank is not in the list"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Applicant"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox ID="txtBankApplicant" runat="server" Width="100%" Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                </td>
                                <td class="style11" valign="top">
                                    <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Bank"></asp:Label>
                                </td>
                                <td valign="top">
                                    <telerik:RadDropDownList runat="server" DefaultMessage="-- Select --" 
                                        DropDownHeight="200px"  ForeColor="#666666"
                                        RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" 
                                        Width="100%" ID="radBank" Height="73px" 
                                        AutoPostBack="True" Skin="Bootstrap">
                                    </telerik:RadDropDownList>
                                    <br />
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="100%" ID="txtBankAcct"></asp:TextBox>
                                </td>
                                <td class="style10">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Account Number"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="100%" ID="txtAccountNo"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Button ID="btnSaveAcct" runat="server" Text="Save &amp; Next" 
                                        BackColor="#1BA691" ForeColor="White"
                                        Width="150px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="12px" />
                                </td>
                                <td style="width: 75%">
                                    <asp:Button ID="btnNoAcct" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        Font-Bold="True" Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="20px"
                                        Style="margin-top: 0px" Text="Don't Have Account" Width="150px" />
                                </td>
                                <td style="width: 5%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                        <input type="hidden" id="Hidden1" value="60:00" runat="server" />
                        <input type="hidden" id="Hidden2" value="0" runat="server" />
                    </td>
                    <td style="width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    
                                    <asp:Label ID="lblPFAID" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Visible="False"></asp:Label>
                                    
                                </td>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblpfastatus" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="14px" Text="PENSION FUND ACCOUNT"
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:Label ID="Label79" runat="server" Font-Italic="True" Font-Names="Verdana" 
                                        Font-Size="12px" 
                                        Text="Note: select Others and type in your PFA Name if your PFA is not in the list"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Applicant"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox ID="txtPenApplicant" runat="server" Width="100%" Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                </td>
                                <td class="style11" valign="top">
                                    <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Pension Administrator"></asp:Label>
                                </td>
                                <td valign="top" class="style12">
                                    <telerik:RadDropDownList runat="server" DefaultMessage="-- Select --" DropDownHeight="50px"  ForeColor="#666666"
                                        RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                        Font-Size="12px" Width="100%" ID="radPenMgr" Height="73px" AutoPostBack="True">
                                    </telerik:RadDropDownList>
                                    <br />
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                        Font-Size="12px" Width="100%" ID="txtPenMgr"></asp:TextBox>
                                </td>
                                <td class="style10">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="RSA Number" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="100%" ID="txtRSA"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Button ID="btnSavePFA" runat="server" Text="Save &amp; Next" 
                                        BackColor="#1BA691" ForeColor="White"
                                        Width="150px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="12px" />
                                </td>
                                <td style="width: 75%">
                                    <asp:Button ID="btnNoPFA" runat="server" BackColor="#1BA691" BorderStyle="None" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="20px" Style="margin-top: 0px"
                                        Text="Don't Have PFA" Width="150px" />
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View8" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                        <input type="hidden" id="Hidden5" value="60:00" runat="server" />
                        <input type="hidden" id="Hidden6" value="0" runat="server" />
                    </td>
                    <td style="width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    
                                    <asp:Label ID="Label80" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Visible="False"></asp:Label>
                                    
                                </td>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblnhfstatus" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label82" runat="server" Font-Names="Verdana" Font-Size="14px" Text="NATIONAL HOUSING FUND"
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" 
                                        Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    &nbsp;</td>
                                <td style="width: 75%">
                                    <asp:Label ID="Label83" runat="server" Font-Italic="True" Font-Names="Verdana" 
                                        Font-Size="12px" 
                                        Text="Note: select Others and type in your PFA Name if your PFA is not in the list"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label84" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Applicant" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox ID="txtNHFApplicant" runat="server" Width="100%" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style16">
                                </td>
                                <td class="style17" valign="top">
                                    <asp:Label ID="Label85" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="NHF Number"></asp:Label>
                                </td>
                                <td valign="top" class="style17" >                                
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="100%" ID="txtnhfnumber"></asp:TextBox>
                                </td>
                                <td class="style16">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label86" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Account Name"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="100%" ID="txtnhfname"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Button ID="Button1" runat="server" Text="Save &amp; Next" 
                                        BackColor="#1BA691" ForeColor="White"
                                        Width="150px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="12px" />
                                </td>
                                <td style="width: 75%">
                                    <asp:Button ID="Button3" runat="server" BackColor="#1BA691" BorderStyle="None" Font-Bold="True"
                                        Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="20px" Style="margin-top: 0px"
                                        Text="Don't Have NHF" Width="150px" />
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                      
                    </td>
                    <td style="width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    
                                    <asp:Label ID="lblMedStatusSection1" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                        Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                    
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="14px" Text="MEDICAL DECLARATION FORM (Section 1)"
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" 
                                        Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    <asp:Label ID="lblNo1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Q1."></asp:Label>
                                </td>
                                <td style="width: 90%">
                                    <asp:Label ID="lblQuest1" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Text="Are you aware of any circumstances regarding your health or capacity to work that would interfere with your ability to perform the duties of the position?" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:Label ID="lblQuestItalic1" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Text="In answering this question Yes or No you are also covering factors such as: existing or exposure to infectious diseases, taking of medication/treatment on a regular basis (daily, weekly, monthly)If yes, what adjustments do you need to perform the genuine and reasonable requirements of the employment (if any)?”" 
                                        Font-Bold="False" Font-Italic="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:RadioButtonList ID="rdoQuestion1" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" RepeatDirection="Horizontal" Width="400px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes, if yes, please provide details</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtQuestionNote1" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                                        BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="12px" 
                                        Height="50px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    </td>
                                <td class="style14">
                                    <asp:Label ID="lblSection1ID" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Visible="False"></asp:Label>
                                    </td>
                                <td class="style13">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Q2."></asp:Label>
                                </td>
                                <td style="width: 90%">
                                    <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Text="Do you have an existing injury or condition or pre-existing injury or condition that could be affected by the nature of the proposed employment?" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:Label ID="Label19" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Text="Existing is a condition for which treatment is still being received. Pre-existing is where an injury or condition/s is present but treatment is not required. If yes please provide details of the injury or condition(s).If yes, what adjustments do you need to perform the genuine and reasonable requirements of the employment (if any)?”" 
                                        Font-Bold="False" Font-Italic="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:RadioButtonList ID="rdoQuestion2" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" RepeatDirection="Horizontal" Width="400px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes, if yes, please provide details</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtQuestionNote2" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                                        BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="12px" 
                                        Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    </td>
                                <td class="style14">
                                    </td>
                                <td class="style13">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Q3."></asp:Label>
                                </td>
                                <td style="width: 90%">
                                    <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                        Text="Are you taking any medication that could be important for us to know in case of emergency?" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:RadioButtonList ID="rdoQuestion3" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" RepeatDirection="Horizontal" Width="400px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes, if yes, please provide details</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtQuestNote3" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                                        BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="12px" 
                                        Height="50px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    </td>
                                <td class="style14">
                                    </td>
                                <td class="style13">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Q4."></asp:Label>
                                </td>
                                <td style="width: 90%">
                                    <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Text="Do you suffer from or carry any infectious disease or illness?" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:RadioButtonList ID="rdoQuestion4" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" RepeatDirection="Horizontal" Width="400px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes, if yes, please provide details</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    </td>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtQuestNote4" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                                        BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="12px" 
                                        Height="50px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    </td>
                                <td class="style14">
                                    </td>
                                <td class="style13">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;</td>
                                <td style="width: 90%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnMedSaveSection1" runat="server" BackColor="#1BA691" 
                                                    BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" 
                                                    ForeColor="White" Height="20px" Style="margin-top: 0px" Text="Save &amp; Next" 
                                                    Width="150px" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnMedNextSection" runat="server" BackColor="#1BA691" 
                                                    BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" 
                                                    ForeColor="White" Height="20px" Style="margin-top: 0px" Text="Next Section" 
                                                    Width="150px" Visible="False" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View4" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    <asp:TextBox ID="txtmin0" runat="server" BorderColor="White" BorderWidth="1px" 
                                        Height="1px" Width="1px"></asp:TextBox>
                                    <asp:Label ID="lblSection2ID" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Visible="False"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblmedsectionII" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label24" runat="server" Font-Names="Verdana" Font-Size="14px" 
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" 
                                        Text="MEDICAL DECLARATION FORM (Section II)" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Any heart complaint"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoheartcomplaint" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Persistent Headaches"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoheadache" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Rheumatic Fever"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdorheumatic" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Hearing Problems"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohearingproblem" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Chest Pain"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdochestpain" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Eye Disease"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoeyedisease" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="High Blood Pressure"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohbp" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Dermatitis/Skin Disease"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdodermatitis" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Shortness of Breath"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoshortbreathe" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Allergy to Penicillin, anaesthetic"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoanaesthetic" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Artificial valve/ Pacemaker"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdopacemaker" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Excessive bleeding/ blood disease"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoblooddisease" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Hepatitis, Jaundice, liver disease. "></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohepatitis" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Allergy to any medication/other"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoallergymedication" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="HIV/AIDS"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohivaids" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Gastric Ulcer"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdogastriculcer" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Diabetes"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdodiabetes" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Gout"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdogout" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Thyroid Disease"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdothyroid" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Arthritis"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoarthritis" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Tuberculosis"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdotuberculosis" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Bowel Disease/Complaint"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdobowel" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label40" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Epilepsy"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoepilepsy" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Gall Bladder Disease"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdogallbladder" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label42" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Fully Fit"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdofullyfit" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label43" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Hip, knee or joint replacement"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohipknee" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label44" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Fainting"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdofainting" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label45" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Back injury"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdobackinjury" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label46" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Blackouts"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoblackouts" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label47" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Spinal Injury"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdospinalinjury" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label48" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Emphysema"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoemphysema" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label49" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Neck Injury"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoneckinjury" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label50" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Wheeziness/bronchitis"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdowheeziness" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label51" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Bone Fractures"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdobonefracture" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label52" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Pneumonia"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdopneumonia" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label53" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Mental Illness"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdomentalillness" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label54" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Asthma"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoasthma" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Paralysis"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdoparalysis" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label56" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Any other respiratory disease"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdorespiratorydisease" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label57" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Do you Smoke?"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdosmoke" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 35%">
                                    <asp:Label ID="Label58" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" Text="Nervous System Disorders"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdonervousdisorder" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                   </td>
                                <td style="width: 35%">
                                    <asp:Label ID="Label59" runat="server" Font-Bold="False" Font-Italic="False" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        Text="Have you ever been hospitalised?"></asp:Label>
                                    </td>
                                <td style="width: 15%">
                                    <asp:DropDownList ID="rdohospitalised" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" Width="100px">
                                        
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" >
                            <tr>
                                
                                <td style="width: 95%">
                                    <asp:Label ID="Label61" runat="server" Font-Names="Verdana" Font-Size="13px" 
                                        Text="Have you ever been on Workers Compensation?" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                
                                <td style="width: 95%">
                                    <asp:RadioButtonList ID="rdoworkercompensation" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" RepeatDirection="Horizontal" Width="400px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes, If yes, please specify the following:</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" >
                            <tr>
                                <td style="width: 30%">
                                    <asp:Label ID="Label60" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Part of body injured"></asp:Label>
                                </td>
                                <td style="width: 65%">
                                    <asp:TextBox ID="txtbodyinjured" runat="server" BorderColor="#CCCCCC" 
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Width="100%" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <asp:Label ID="Label62" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Approximate date of injury:"></asp:Label>
                                </td>
                                <td style="width: 65%">
                                    <telerik:RadDatePicker ID="datInjurydate" Runat="server" Culture="en-US" 
                                        Font-Names="Verdana" Font-Size="12px" MinDate="" 
                                        ResolvedRenderMode="Classic" Enabled="False">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                         
                        </table>
                        <table width="100%" >
                                <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="Label63" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Did you receive a lump sum settlement?"></asp:Label>
                                </td>
                                <td style="width: 55%">
                                    <asp:RadioButtonList ID="rdolumpsettlement" runat="server" 
                                        Font-Names="Verdana" Font-Size="12px" RepeatDirection="Horizontal" 
                                        Width="120px" Enabled="False">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="Label64" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Did you receive a final medical clearance"></asp:Label>
                                </td>
                                <td style="width: 55%">
                                    <asp:RadioButtonList ID="rdomedicalclearance" runat="server" 
                                        Font-Names="Verdana" Font-Size="12px" RepeatDirection="Horizontal" 
                                        Width="120px" Enabled="False">
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>                             
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="Label65" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Additional space to enter detail if required:"></asp:Label>
                                </td>
                                <td style="width: 55%">
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                
                                <td style="width: 95%">
                                    <asp:TextBox ID="txtAdditions" runat="server" BorderColor="#CCCCCC" 
                                        BorderWidth="1px" Enabled="False" Font-Names="Verdana" Font-Size="12px" 
                                        Height="50px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" >
                            <tr>
                                                              <td style="width: 95%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSaveSectionII" runat="server" BackColor="#1BA691" 
                                                    BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" 
                                                    ForeColor="White" Height="20px" Style="margin-top: 0px" Text="Save &amp; Next" 
                                                    Width="150px" onclientclick="Confirm()" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button2" runat="server" BackColor="#1BA691" 
                                                    BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" 
                                                    ForeColor="White" Height="20px" Style="margin-top: 0px" Text="Next Section" 
                                                    Width="150px" Visible="False" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                        <input type="hidden" id="Hidden3" value="60:00" runat="server" />
                        <input type="hidden" id="Hidden4" value="0" runat="server" />
                    </td>
                    <td style="width: 60%">
                        <table width="100%">
                            <tr>
                                <td class="style6" colspan="2">
                                    
                                </td>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label66" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="Label67" runat="server" Font-Names="Verdana" Font-Size="14px" Text="DECLARATION"
                                        Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" 
                                        Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label68" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Name"></asp:Label>
                                </td>
                                <td style="width: 75%">
                                    <asp:TextBox ID="txtName" runat="server" Width="100%" Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:Label ID="Label72" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="of"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                </td>
                                <td valign="top" class="style11">
                                    <asp:Label ID="Label71" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Address"></asp:Label>
                                </td>
                                <td valign="top" class="style12">
                                    <asp:TextBox ID="txtAddress" runat="server" BorderColor="#CCCCCC" ForeColor="#666666"
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" 
                                        Height="55px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    <br />
                                </td>
                                <td class="style10">
                                </td>
                            </tr>
                           
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="Label69" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        
                                        Text="do sincerely declare that the contents of this form are true and correct and complete to the best of my knowledge and no information concerning my past or present state of health has been withheld. I hereby agree to undergo a health assessment by a medical practitioner if deemed necessary."></asp:Label>
                                </td>
                                <td style="width: 55%">
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                    <asp:Label ID="Label70" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                        
                                        
                                        Text="I understand that any wilfully incorrect or misleading answer or material omission which relates to any of the questions before mentioned may make me ineligible for employment, or if employed, liable to disciplinary action which may include dismissal. I understand that this pre-employment health declaration may form part of my file."></asp:Label>
                                </td>
                                <td style="width: 55%">
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                
                                <td style="width: 95%">
                                    </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td class="style15">
                                    <asp:Label ID="Label73" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Applicant’s signature {FULL NAME}:"></asp:Label>
                                </td>
                                <td style="width: 65%">
                                    <asp:TextBox ID="txtSign" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>
                            <tr>
                                <td class="style15">
                                    <asp:Label ID="Label74" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                                        Text="Date:"></asp:Label>
                                </td>
                                <td style="width: 65%">
                                    <telerik:RadDatePicker ID="datDate" Runat="server" Culture="en-US"  ForeColor="#666666"
                                        Font-Names="Verdana" Font-Size="12px" MinDate="" 
                                        ResolvedRenderMode="Classic">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%">
                                </td>
                            </tr>                            
                        </table>
                        <table width="100%" >
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSaveSectionII0" runat="server" BackColor="#1BA691" 
                                        BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="White" Height="20px" Style="margin-top: 0px" Text="Sign" 
                                        Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View6" runat="server">
            <table width="100%" >
                <tr>                    
                    <td align="center">
                        <br />
                        <asp:Label ID="lbldeclare" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            Font-Size="20px" style="color: #009933" 
                            
                            
                            Text="Bank, Pension and Medical Declaration has been successfully submitted"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View7" runat="server">
            <table width="100%" >
                <tr>                    
                    <td align="center">
                        <br />
                        <asp:Label ID="lblend" runat="server" Font-Bold="True" Font-Names="Verdana" 
                            Font-Size="20px" style="color: #009933" 
                            
                            
                            Text="Your Bank, Pension and Medical Declaration has already been successfully submitted"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:View>        
    </asp:MultiView>
    </form>
</body>
</html>
