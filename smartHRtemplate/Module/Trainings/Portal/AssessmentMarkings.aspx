<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AssessmentMarkings.aspx.vb"
    Inherits="GOSHRM.AssessmentMarkings" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Learning Assessment</title>
      
    <script type="text/javascript">txt

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
        .style11
        {
            width: 138px;
        }
        </style>
</head>

<body onload="setHeight();" onunload="window.opener.location=window.opener.location;">
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />       
        
 <div style="display:none">    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                       
                        <asp:Label ID="lblimage" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 60%">
                        <table width="100%">
                            <tr>
                                <td class="style1" colspan="2" style="background-color: #1BA691">
                                    <asp:Label ID="lblHeader" runat="server" Font-Names="Verdana" Font-Size="16px"
                                        Width="100%" style="text-align: center"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" colspan="2">
                                    <asp:TextBox ID="txtid" runat="server" Height="20px" Visible="False" 
                                        Width="2px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style11" valign="top" align="right">
                                    <asp:Label ID="lblQuestionNo" runat="server" Font-Names="Verdana" Text="." 
                                        Font-Size="12px" ForeColor="#666666"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtQuestion" runat="server" BorderColor="White" BorderWidth="1px"
                                        Width="100%" TabIndex="3" TextMode="MultiLine" Style="overflow: hidden" ReadOnly="True"
                                        Font-Names="Verdana" Font-Size="12px" Height="70px" ForeColor="#666666"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11" valign="top">
                                    <asp:TextBox ID="txtQuestionCount" runat="server" BorderColor="White" 
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="Small" Height="5px" 
                                        ReadOnly="True" Visible="False" Width="10px"></asp:TextBox>
                                    <asp:Label ID="lblEmpSession" runat="server" Font-Size="9px" Visible="False"></asp:Label>
                                </td>
                                <td >
                                    <asp:Image ID="imgProfile" runat="server" AlternateText="Picture Question" 
                                        GenerateEmptyAlternateText="True" Height="300px" Width="100%" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="style11" >
                                    <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Trainee Answer" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                    <asp:TextBox ID="txtQuestionType" runat="server" Visible="False" Height="16px" Width="16px"
                                        BorderColor="White" BorderWidth="1px"></asp:TextBox>
                                    <asp:TextBox ID="txtsec" runat="server" Height="1px" Width="1px" BorderColor="White"
                                        BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td >
                                    <asp:CheckBoxList ID="chkAnswers" runat="server" TabIndex="7" Font-Names="Verdana"
                                        Font-Size="12px" Enabled="False" ForeColor="#666666">
                                    </asp:CheckBoxList>
                                    <asp:RadioButtonList ID="rdoAnswers" runat="server" TabIndex="7" 
                                        Font-Names="Verdana" Font-Size="12px" Enabled="False" ForeColor="#666666">
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txtAnswers" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Width="100%" TabIndex="3" TextMode="MultiLine" Style="overflow: hidden"
                                        Font-Names="Verdana" Font-Size="12px" Height="64px" ReadOnly="True" 
                                        ForeColor="#666666"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="style11" valign="top">
                                    <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Text="Answer" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                  </td>
                                <td >
                                   
                                    <asp:TextBox ID="txtRealAnswer" runat="server" BorderColor="White" 
                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Height="77px" 
                                        TextMode="MultiLine" Width="100%" ForeColor="#666666"></asp:TextBox>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;
                                </td>
                                <td class="style8">
                                    <asp:Label ID="lblPage" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                        Style="color: #3399FF; font-weight: 700;"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="lblmark" runat="server" BorderColor="#CCCCCC" 
                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                   
                                    &nbsp;</td>
                                <td >
                                         <asp:RadioButtonList ID="rdoMarking" runat="server" Font-Names="Verdana" 
                                        Font-Size="12px" RepeatDirection="Horizontal" TabIndex="7" Width="226px" 
                                        AutoPostBack="True" ForeColor="#666666">
                                    </asp:RadioButtonList>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                   
                                    &nbsp;</td>
                                <td class="style8">
                                   
                                    <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                                    
                                </td>
                            </tr>

                            <tr>
                                <td class="style11">
                                    
                                </td>
                                <td >
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                &nbsp;</td>
                                            <td style="width: 25%"> 
                                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" BackColor="#1BA691" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                                    Font-Size="11px" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnNext" runat="server" Text="Next" BackColor="#1BA691" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                                    Font-Size="11px" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnFinish" runat="server" Text="Finish" BackColor="#3399FF" ForeColor="White"
                                                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                                    Font-Size="11px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
        </asp:View>
         <asp:View ID="View2" runat="server">
            <table width="100%">
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" 
                        style="text-align: center" 
                        Width="100%"></asp:Label>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                
                    <asp:Label ID="lblFinalStatus" runat="server" Font-Names="Arial" 
                        style="text-align: center; color: #33CC33; font-weight: 700;" Text="Test result successfully submitted" 
                        Width="100%"></asp:Label>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" 
                        style="text-align: center; color: #33CC33; font-weight: 700;" 
                        Width="100%"></asp:Label>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" 
                        style="text-align: center" 
                        Width="100%"></asp:Label>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%; text-align: center;">
                
                    <asp:Button ID="btnNext0" runat="server" BackColor="#1BA691" BorderStyle="None" 
                        ForeColor="White" Height="20px" Text="Ok" Width="120px" 
                        Font-Names="Verdana" Font-Size="12px" />
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            </table>
         </asp:View>
    </asp:MultiView>
    </div>

    <div class="container col-md-12">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        <div class="row">
            <%--<div class="col-xs-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Head</h5>
            </div>--%>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="divdetailheader" runat="server">TEST QUESTIONS</b></h5>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class="col-sm-6 col-md-12 col-xs-6 form-group text-right">
            <button id="btAdd" onserverclick="btnSaveGrid_Click" type="button" runat="server" class="btn btn-success">Save</button> 
            <button id="Button2" style="margin-left:10px;" runat="server" onserverclick="btnClose_Click" type="submit" class="btn btn-danger">Back</button>
                </div>
        </div>
        <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>
<%--                        <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />

                         <asp:BoundField DataField="Questions" HeaderText="Questions" SortExpression="Questions"  />
                        <asp:BoundField DataField="QuestionType" HeaderText="Question Type" SortExpression="QuestionType"/>
                        <asp:BoundField DataField="EmpAnswer" HeaderText="Employee Answer" SortExpression="EmpAnswer"/>
                        <asp:BoundField DataField="Answer" HeaderText="Answer" SortExpression="Answer"/> 
                       <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                 Mark
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
                            <ItemTemplate>
                            <asp:RadioButtonList ID="rblwrongcorrect" runat="server" RepeatDirection="Vertical">
                                <asp:Listitem Text= "Correct" Value="1" />
                                <asp:Listitem Text= "Wrong" Value="0" />
                            </asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            </div></div>
            </div>
    </form>
</body>
</html>
</asp:Content>
