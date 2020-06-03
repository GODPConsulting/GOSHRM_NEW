<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeeTrainingAssessment.aspx.vb"
    Inherits="GOSHRM.EmployeeTrainingAssessment" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Training Assessment</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

     <script type="text/javascript">
         function Complete() {
             var confirm_value1 = document.createElement("INPUT");
             confirm_value1.type = "hidden";
             confirm_value1.name = "confirm_value1";
             if (confirm("Application Assessment is complete?")) {
                 confirm_value1.value = "Yes";
             } else {
                 confirm_value1.value = "No";
             }
             document.forms[0].appendChild(confirm_value1);
         }
    </script>
    <style type="text/css">
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            }
        .style6
        {
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 632px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 632px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 632px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline} .RadDropDownList { display:inline-block !important; 
                                               width: 632px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style9
        {
            width: 548px;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .style10
        {
            width: 39px;
        }
        .style11
        {
            width: 408px;
        }
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table>
        <tr>
            <td class="style10">
            </td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblTraining" runat="server" Font-Names="Verdana" 
                    Font-Size="14px" BackColor="#1BA691" Width="100%"
                    Font-Bold="True" Style="color: #FFFFFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:TextBox ID="TextBox1" runat="server" Width="10px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
            <td class="style6" colspan="2">
                <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    
                    Text="Thank you for participating in this training. In this feedback form, there are no WRONG or RIGHT answers. Please respond to ALL the questions below to help us to improve the curriculum, training materials, and the conduct of the training" 
                    ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:TextBox ID="txtid" runat="server" Width="10px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
            <td class="style11">
            </td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="1." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="The content were organized and easy to follow" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9" valign="top">
                <asp:RadioButtonList ID="rdo1" runat="server" Font-Names="Verdana" Font-Size="12px"
                    RepeatDirection="Horizontal" ForeColor="#666666">
                    <asp:ListItem>Strongly Disagree</asp:ListItem>
                    <asp:ListItem>Disagree</asp:ListItem>
                    <asp:ListItem>Indecisive</asp:ListItem>
                    <asp:ListItem>Agree</asp:ListItem>
                    <asp:ListItem>Strongly Agree</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Text="2." ForeColor="#666666"></asp:Label>
            </td>
            <td valign="top" class="style5">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Text="The materials distributed were helpful" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9">
                <asp:RadioButtonList ID="rdo2" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>Strongly Disagree</asp:ListItem>
                    <asp:ListItem>Disagree</asp:ListItem>
                    <asp:ListItem>Indecisive</asp:ListItem>
                    <asp:ListItem>Agree</asp:ListItem>
                    <asp:ListItem>Strongly Agree</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Text="3." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" valign="top">
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Text="This training experience will be useful in my work" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9" valign="top">
                <asp:RadioButtonList ID="rdo3" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>Strongly Disagree</asp:ListItem>
                    <asp:ListItem>Disagree</asp:ListItem>
                    <asp:ListItem>Indecisive</asp:ListItem>
                    <asp:ListItem>Agree</asp:ListItem>
                    <asp:ListItem>Strongly Agree</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" >
                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Text="4." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Text="The trainer was knowledgeable about the training topics" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9">
                <asp:RadioButtonList ID="rdo4" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>Strongly Disagree</asp:ListItem>
                    <asp:ListItem>Disagree</asp:ListItem>
                    <asp:ListItem>Indecisive</asp:ListItem>
                    <asp:ListItem>Agree</asp:ListItem>
                    <asp:ListItem>Strongly Agree</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" >
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Text="5." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="12px" Text="The training objectives were met" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9">
                <asp:RadioButtonList ID="rdo5" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    RepeatDirection="Horizontal">
                    <asp:ListItem>Strongly Disagree</asp:ListItem>
                    <asp:ListItem>Disagree</asp:ListItem>
                    <asp:ListItem>Indecisive</asp:ListItem>
                    <asp:ListItem>Agree</asp:ListItem>
                    <asp:ListItem>Strongly Agree</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Text="6." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" valign="top">
                <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="12px" Text="What did you like most about this training?" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td  colspan="2">
                <asp:TextBox ID="txt6" runat="server" Width="100%" ForeColor="#666666"
                    Font-Names="Verdana" Height="78px" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" TextMode="MultiLine" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Size="12px" Text="7." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" valign="top">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    Text="What aspects of the training could be improved?"></asp:Label>
            </td>
            <td class="style9">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td  colspan="2">
                <asp:TextBox ID="txt7" runat="server" Width="100%" ForeColor="#666666"
                    Font-Names="Verdana" Height="78px" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" TextMode="MultiLine" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style11">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" Text="8." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" colspan="2" valign="top">
                <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="12px" 
                    Text="How do you hope to change your practice as a result of this training?" ForeColor="#666666"></asp:Label>
                &nbsp;
            </td>
        </tr>
        
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt8" runat="server" Width="100%" ForeColor="#666666"
                    Font-Names="Verdana" Height="78px" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" TextMode="MultiLine" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr style="margin-top:20px;">
            <td class="style10" valign="top">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Size="12px" Text="9." ForeColor="#666666"></asp:Label>
            </td>
            <td class="style5" colspan="2" valign="top">
                  <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style10">
            </td>
            <td class="style5">
                <table width="100%">
                    <tr>
                        <td>
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" />
                        </td>
                          <td>
                <asp:Button ID="btnComplete" runat="server" Text="Complete" BackColor="#0099FF" ForeColor="White"
                    Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" onclientclick="Complete()" />
                        </td>
                        <td>
                <asp:Button ID="btnCancel" runat="server" Text="Back" BackColor="#999966" ForeColor="White"
                    Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style9">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" 
                    ForeColor="#FF3300" Font-Size="12px" Font-Names="Verdana"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
