<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test.aspx.vb" Inherits="GOSHRM.Test"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Test </title>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="javascript">

        var lmin;
        var lsec;
        var leftmin = 20;
        var leftsec = 59;
        $(document).ready(function () {
            if (parseInt(document.getElementById("statusid").value) > 0) {
                var tt = document.getElementById("<%=txtmin.ClientID%>");
                leftmin = tt.value;
                var tsec = document.getElementById("<%=txtsec.ClientID%>")
                leftsec = tsec.value;
                getremainingtim();
            }
            else {

            }
        });
        var timleft;
        function getremainingtim() {

            if (parseInt(leftsec) > 0) {
                leftsec = parseInt(leftsec) - 1;
                document.getElementById("<%=txtTime.ClientID%>").value = "Time Left " + leftmin + " Min :" + leftsec + " Sec";
                document.getElementById("<%=txtmin.ClientID%>").value = leftmin;
                document.getElementById("<%=txtsec.ClientID%>").value = leftsec;
            }
            else {
                if (parseInt(leftsec) == 0) {
                    if (parseInt(leftmin) <= 0) {
                        document.getElementById("<%=txtTime.ClientID%>").value = "Time up";
                        clearTimeout(timleft);
                    }
                    else {
                        leftmin = parseInt(leftmin) - 1;
                        leftsec = 59;
                        document.getElementById("<%=txtTime.ClientID%>").value = "Time Left " + leftmin + " Min :" + leftsec + " Sec";
                        document.getElementById("<%=txtmin.ClientID%>").value = leftmin;
                        document.getElementById("<%=txtsec.ClientID%>").value = leftsec;

                    }
                }
                document.getElementById("<%=txtTime.ClientID%>").value = "Time Left " + leftmin + " Min :" + leftsec + " Sec";
                document.getElementById("<%=txtmin.ClientID%>").value = leftmin;
                document.getElementById("<%=txtsec.ClientID%>").value = leftsec;
            }
            timleft = setTimeout("getremainingtim()", 1000);
        }
    </script>
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
            font-weight: bold;
        }
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
        .style8
        {
            width: 561px;
        }
        #Text1
        {
            height: 38px;
        }
        </style>
</head>
<script type="text/javascript">
    function setHeight() {
        var tt = document.getElementById("<%=txtQuestion.ClientID%>");
        tt.style.height = tt.scrollHeight + "px";
    }
</script>
<body onload="setHeight();" onunload="window.opener.location=window.opener.location;"
    style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 30%">
                        <input type="hidden" id="lefttime" value="60:00" runat="server" />
                        <input type="hidden" id="statusid" value="0" runat="server" />
                    </td>
                    <td style="width: 40%">
                        <table width="100%">
                            <tr>
                                <td class="style1" colspan="2" style="background-color: #1BA691">
                                    <asp:Label ID="lblHeader" runat="server" Font-Names="Arial" Font-Size="17px" Width="100%"
                                        Style="text-align: center"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" colspan="2">
                                    <asp:Image ID="Image1" runat="server" Height="39px" ImageUrl="~/images/clock1.png"
                                        Width="39px" />
                                    <asp:Label ID="lblTimeStart" runat="server" Font-Names="Arial" Font-Size="15px" Width="60%"
                                        Style="color: #00CC00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                </td>
                                <td class="style8">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="13px" Style="color: #3399FF;
                                        font-weight: 700;">Duration in Minutes:</asp:Label>
                                    <asp:TextBox ID="txtDuration" runat="server" Width="28%" BorderColor="White" BorderWidth="1px"
                                        ReadOnly="True" Style="font-weight: 700; color: #3399FF"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:TextBox ID="txtid" runat="server" Visible="False" Height="16px" Width="17px"
                                        BorderColor="White" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td class="style8">
                                    <asp:TextBox ID="txtTime" runat="server" BorderColor="White" ReadOnly="True" BorderStyle="None"
                                        Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
<%--                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                            <ContentTemplate>--%>
                                <table width="100%">
                                    <tr>
                                        <td class="style6" valign="top">
                                            <asp:Label ID="lblQuestionNo" runat="server" Font-Names="Verdana" Text="." Font-Size="12px"
                                                ForeColor="#666666"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtQuestion" runat="server" Font-Names="Arial" Font-Size="12px" ForeColor="#666666"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6" valign="top">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Image ID="imgProfile" runat="server" GenerateEmptyAlternateText="True" Height="300px"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="style6" style="border-bottom-style: solid; border-bottom-width: thin;
                                            border-bottom-color: #C0C0C0">
                                            <asp:TextBox ID="txtQuestionCount" runat="server" Visible="False" Height="16px" Width="16px"
                                                BorderColor="White" BorderWidth="1px"></asp:TextBox>
                                            <asp:TextBox ID="txtQuestionType" runat="server" Visible="False" Height="16px" Width="16px"
                                                BorderColor="White" BorderWidth="1px"></asp:TextBox>
                                            <asp:TextBox ID="txtRealAnswer" runat="server" Visible="False" Height="16px" Width="16px"
                                                BorderColor="White" BorderWidth="1px"></asp:TextBox>
                                            <asp:TextBox ID="txtsec" runat="server" Height="1px" Width="1px" BorderColor="White"
                                                BorderWidth="1px"></asp:TextBox>
                                        </td>
                                        <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                                            class="style8">
                                            <asp:CheckBoxList ID="chkAnswers" runat="server" TabIndex="7" Font-Names="Arial"
                                                Font-Size="12px" ForeColor="#666666">
                                            </asp:CheckBoxList>
                                            <asp:RadioButtonList ID="rdoAnswers" runat="server" TabIndex="7" Font-Names="Arial"
                                                Font-Size="12px" ForeColor="#666666">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                        <td class="style8">
                                            <asp:Label ID="lblPage" runat="server" Font-Names="Arial" Font-Size="12px" Style="color: #3399FF;
                                                font-weight: 700;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            <asp:TextBox ID="txtmin" runat="server" Height="1px" Width="1px" BorderColor="White"
                                                BorderWidth="1px"></asp:TextBox>
                                        </td>
                                        <td class="style8">
                                            &nbsp;
                                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Red"
                                                Font-Names="Arial"></asp:Label>
                                            <asp:Label ID="lblstart" runat="server" Font-Names="Arial" Font-Size="12px" Style="color: #00CC00"
                                                Visible="False" Width="0px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
<%--                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnStart" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnFinalSubmit" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <table width="100%">
                            <tr>
                                <td class="style6">
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnStart" runat="server" Text="Start" BackColor="#FF6600" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                                                    Font-Bold="True" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" BackColor="#1BA691" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                                                    Font-Bold="True" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnNext" runat="server" Text="Next" BackColor="#1BA691" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                                                    Font-Bold="True" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnFinalSubmit" runat="server" Text="Submit Test" BackColor="#3399FF"
                                                    ForeColor="White" Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana"
                                                    Font-Size="11px" Font-Bold="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Style="text-align: center"
                            Width="100%"></asp:Label>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:Label ID="lblFinalStatus" runat="server" Font-Names="Arial" Style="text-align: center;
                            font-weight: 700;" Text="Test successfully submitted for assessment" Width="100%"
                            ForeColor="#666666"></asp:Label>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" ForeColor="#666666" Style="text-align: center;
                            font-weight: 700;" Width="100%"></asp:Label>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Style="text-align: center"
                            Width="100%"></asp:Label>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%; text-align: center;">
                        <asp:Button ID="btnOk" runat="server" BackColor="#1BA691" BorderStyle="None" ForeColor="White"
                            Height="40px" Text="OK" Width="150px" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" />
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </form>
</body>
</html>
