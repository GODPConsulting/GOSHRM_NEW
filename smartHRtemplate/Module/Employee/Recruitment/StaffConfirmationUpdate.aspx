 <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="StaffConfirmationUpdate.aspx.vb"
    Inherits="GOSHRM.StaffConfirmationUpdate" EnableEventValidation="false" Debug="true" %>
    <asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

       <script type="text/javascript">
           function Complete() {
               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Mark as Complete and send notification to HR?")) {
                   confirm_value.value = "Yes";
               } else {
                   confirm_value.value = "No";
               }
               document.forms[0].appendChild(confirm_value);
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
<%--    <table width="100%">
        <tr>
            <td class="style1" style="background-color: #1BA691">
                <strong>Employee Confirmation</strong>
            </td>
        </tr>
        </table>--%>
    <%--<table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style5">
                            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Employee:"  
                                Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                        </td>
                        <td class="style7">
                            <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" ResolvedRenderMode="Classic"
                                Width="100%" ID="cboEmployee" Filter="Contains" AutoPostBack="True">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Office:" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lbloffice" runat="server"  ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Probation (Mths):"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"  ForeColor="#666666"
                                        Font-Size="12px" Width="50px" ID="txtProbation"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Date Join:"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblDateJoin" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                Font-Size="12px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" valign="top">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Targets Achieved:"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:TextBox ID="txtTargetAchieved" runat="server" Width="100%" 
                                Font-Names="Verdana"  ForeColor="#666666"
                                Height="100px" TextMode="MultiLine" BorderColor="#CCCCCC" 
                                BorderWidth="1px" Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" valign="top">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="#666666" 
                                Font-Names="Verdana" Text="Areas for Development:"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:TextBox ID="txtAreaOfDev" runat="server" Width="100%" Font-Names="Verdana" 
                                Height="100px"  ForeColor="#666666"
                                TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" 
                                Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" valign="top">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Comment:" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:TextBox ID="txtComment" runat="server" Width="100%" Font-Names="Verdana" 
                                Height="100px"  ForeColor="#666666"
                                TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" 
                                Font-Size="12px" 
                                ToolTip="ensure that applicable company policies have been complied with"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style6">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Rating" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <telerik:RadRating ID="RadRating1" runat="server" AutoPostBack="True">
                            </telerik:RadRating>                            
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblRating" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                                        Font-Bold="True"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="RadRating1" EventName="Rate" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" valign="top">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Recommendation"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <telerik:RadDropDownList runat="server" DefaultMessage="-- Select --" DropDownHeight="100px"
                                RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                Font-Size="12px" Width="100%" ID="radRecommendation" Height="73px" AutoPostBack="True">
                                <Items>
                                    <telerik:DropDownListItem runat="server" Text="Pending" Value="Pending" />
                                    <telerik:DropDownListItem runat="server" Text="Confirmed" Value="Confirmed" />
                                    <telerik:DropDownListItem runat="server" Text="Terminate Employment" Value="Terminate Employment" />
                                    <telerik:DropDownListItem runat="server" Text="Extend Probation" Value="Extend Probation" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="lblExtendProbationID" runat="server" Font-Bold="True" 
                                ForeColor="#666666" Font-Names="Verdana" Text="Extend By:"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" 
                                        Font-Names="Verdana" ForeColor="#666666"
                                        Font-Size="12px" Width="50px" ID="txtProbationExtension" 
                                        style="text-align: center" ToolTip="extend probation by"></asp:TextBox>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" 
                                ForeColor="#666666" Font-Names="Verdana" Text="Month(s)"
                                Font-Size="12px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radRecommendation" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Edit Status:"
                                Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lblCompleteStat" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                    </table>
            </td>
            <td>
            </td>
            <td valign="top" class="style12">
                <table>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="lblHRManagerID" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                Text="HR Manager:" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style13">
                            <asp:Label ID="lblHRManager" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="lblHRCommentID" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                Text="HR Comment:" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style13">
                            <asp:Label ID="lblhrcomment" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="lblHRRecommendationID" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                Text="HR Recommendation:" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="style13">
                            <asp:Label ID="lblHRRecommendation" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                        </td>
                        <td class="style13">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style9">
                &nbsp;
                <asp:TextBox ID="txtid" runat="server" Width="1px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
            </td>
            <td class="style7">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="11px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style10">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style11">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnComplete" runat="server" Text="Complete" BackColor="#0099FF" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" 
                    ToolTip="Mark Plan as complete and send Approval Notification to HR" 
                    onclientclick="Complete()" />
            </td>
        </tr>
    </table>--%>
    <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="1px" Style="font-size: medium; font-family: Candara"
                    Font-Names="Candara" Height="16px" Visible="False"></asp:TextBox>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class=" col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title">
                                        Employee Confirmation</h5>
                                        <div id="divemplink" runat="server" class="row">
                                            <div id="divjoboffer" runat="server" class="col-sm-3 col-md-12 col-xs-6 pull-left">
                                                <p>
                                                    <a href="StaffConfirmation"><u>Due Employee Confirmation </u></a>
                                                    <label>
                                                        >
                                                    </label>
                                                    <a id="A1" href="#"><u>Employee Confirmation</u></a>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                EMPLOYEE</label>
                                            <telerik:RadComboBox runat="server" Skin="Bootstrap" ResolvedRenderMode="Classic"
                                                Width="100%" ID="cboEmployee" Filter="Contains" AutoPostBack="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                OFFICE/DEPT</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbloffice" runat="server" CssClass="form-control"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div id="divlastreview" runat="server" class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                PROBATION (MONTHS):</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtProbation"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                DATE JOIN:</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblDateJoin" runat="server" CssClass="form-control"> </asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                TARGETS ACHIEVED</label>
                                            <textarea class="form-control" cols="5" rows="4" id="txtTargetAchieved" runat="server"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                AREAS FOR DEVELOPMENT</label>
                                            <textarea class="form-control" cols="5" rows="4" id="txtAreaOfDev" runat="server"></textarea>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                COMMENT</label>
                                            <textarea class="form-control" cols="5" rows="4" id="txtComment" runat="server"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                RATING</label>
                                            <telerik:RadRating ID="RadRating1" RenderMode="Lightweight" Skin="Bootstrap" runat="server" AutoPostBack="True">
                                            </telerik:RadRating>                            
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                                <ContentTemplate>
                                                     <label id="lblRating" runat="server"></label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="RadRating1" EventName="Rate" />
                                                </Triggers>
                                            </asp:UpdatePanel>      
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                RECOMMENDATION</label>
                                            <telerik:RadDropDownList runat="server" DefaultMessage="-- Select --" DropDownHeight="100px"
                                                RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                                Skin="Bootstrap" Width="100%" ID="radRecommendation" AutoPostBack="True">
                                                <Items>
                                                    <telerik:DropDownListItem runat="server" Text="Pending" Value="Pending" />
                                                    <telerik:DropDownListItem runat="server" Text="Confirmed" Value="Confirmed" />
                                                    <telerik:DropDownListItem runat="server" Text="Terminate Employment" Value="Terminate Employment" />
                                                    <telerik:DropDownListItem runat="server" Text="Extend Probation" Value="Extend Probation" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                                <Label ID="lblExtendProbationID" runat="server">EXTEND BY(Months)</Label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtProbationExtension" 
                                                        ToolTip="extend probation by"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="radRecommendation" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-md-12">
                                     <div class="form-group">
                                            <asp:Label ID="lblHRManagerID" runat="server" CssClass="form-control" Text="HR Manager:"></asp:Label>
                                            <asp:Label ID="lblHRManager" CssClass="form-control" runat="server" ></asp:Label>
                                        </div>
                                            <asp:Label ID="lblHRCommentID" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                                Text="HR Comment:" Font-Size="12px"></asp:Label>
                                            <asp:Label ID="lblhrcomment" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
   
                                            <asp:Label ID="lblHRRecommendationID" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                                Text="HR Recommendation:" Font-Size="12px"></asp:Label>
                                            <asp:Label ID="lblHRRecommendation" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                                Font-Size="12px"></asp:Label>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#666666" Font-Names="Verdana" Text="Edit Status:"
                                                Font-Size="12px"></asp:Label>
                                            <asp:Label ID="lblCompleteStat" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                                                Font-Size="12px"></asp:Label>
                                </div>

                                 <div class="row">
                    <div class="col-md-12 m-t-20 text-left">
                        <button id="btsave" runat="server" onserverclick="btnAdd_Click" type="submit" style="width: 150px"
                            class="btn btn-primary btn-success">
                            Save</button>
                        <asp:Button ID="btnComplete" runat="server" Text="Complete" ForeColor="White" Width="150px"
                            Height="35px" BorderStyle="None" Font-Size="14px" Visible="False" OnClientClick="ConfirmComplete()"
                            ToolTip="Complete" CssClass="btn btn-primary btn-info" />
                        <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
                            style="width: 150px" class="btn btn-primary btn-danger">
                            Back</button>

                    </div>
                </div>
                            </div>
                        </div>
                    </div>
                </div

               
    </form>
</body>
</html>
</asp:Content> 