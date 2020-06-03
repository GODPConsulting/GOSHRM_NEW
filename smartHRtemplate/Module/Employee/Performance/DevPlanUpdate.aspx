<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="DevPlanUpdate.aspx.vb"
    Inherits="GOSHRM.DevPlanUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to delete data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmPlan() {
                var confirmplan_value = document.createElement("INPUT");
                confirmplan_value.type = "hidden";
                confirmplan_value.name = "confirmplan_value";
                if (confirm("Do you want to sign Development Plan as Agreed & Discussed?")) {
                    confirmplan_value.value = "Yes";
                } else {
                    confirmplan_value.value = "No";
                }
                document.forms[0].appendChild(confirmplan_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmComplete() {
                var confirm_complete = document.createElement("INPUT");
                confirm_complete.type = "hidden";
                confirm_complete.name = "confirm_complete";
                if (confirm("Plan Completed and Send Notification to Coach?")) {
                    confirm_complete.value = "Yes";
                } else {
                    confirm_complete.value = "No";
                }
                document.forms[0].appendChild(confirm_complete);
            }
        </script>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

        </script>
    </head>
    <body>
        <form action="" id="form1">
        <div class="container col-md-12">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:TextBox runat="server" Font-Size="1px" ID="txtDept" Visible="false"></asp:TextBox>
                    <asp:TextBox runat="server" Font-Size="1px" ID="txtempid" Visible="false"></asp:TextBox>
                    <asp:TextBox runat="server" Font-Size="1px" ID="txtlocation" Visible="false"></asp:TextBox>
                    <asp:Label ID="lblempemail" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:TextBox runat="server" Font-Size="1px" Width="1px" ID="txtMthsInLastPos" Visible="False"></asp:TextBox>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:TextBox runat="server" Font-Size="1px" Width="1px" ID="txtCoachMail" Visible="False"></asp:TextBox>
                            <asp:TextBox runat="server" Font-Size="1px" Width="1px" ID="txtCoachGrade" Visible="False"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbocoach" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                </div>
                <div class="row">
                        <div class="panel panel-success">
                        <div class="panel-heading">
                             <div class="row">
                                 <div class=" col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title">
                                        Development Plan</h5>
                                    <label id="lbapproval" runat="server">
                                    </label>
                                    </div>
                                </div>
                         </div>
                            <div class="panel-body">
                            <div id="dev" runat="server">
                                <div class="row">
                                    <div class=" col-md-3">
                                        <div class="form-group">
                                            <label>
                                                DEVELOPMENT PLAN</label>
                                            <telerik:radcombobox id="cboDevPlan" runat="server" autopostback="True" forecolor="#666666"
                                                width="100%" rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                REVIEW DATE</label>
                                            <telerik:raddatepicker runat="server" mindate="1900-01-01" culture="en-US" rendermode="Lightweight"
                                                forecolor="#666666" width="100%" resolvedrendermode="Classic" id="datReviewDate"
                                                skin="Bootstrap">
                                                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                    fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight" skin="Bootstrap">
                                </calendar>
                                                <dateinput width="" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                    rendermode="Lightweight">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </dateinput>
                                                <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                            </telerik:raddatepicker>
                                        </div>
                                    </div>
                                    <div class=" col-md-3">
                                        <div class="form-group">
                                            <label>
                                                JOB GRADE</label>
                                            <telerik:raddropdownlist Enabled="false" runat="server" defaultmessage="-- Select --" dropdownheight="100px"
                                                forecolor="#666666" rendermode="Lightweight" resolvedrendermode="Classic" backcolor="White"
                                                width="100%" id="radJobGrade" autopostback="True" skin="Bootstrap">
                                            </telerik:raddropdownlist>
                                        </div>
                                    </div>
                                    <div class=" col-md-3">
                                        <div class="form-group">
                                            <label>
                                                JOB TITLE</label>
                                            <telerik:raddropdownlist Enabled="false" runat="server" defaultmessage="-- Select --" dropdownheight="100px"
                                                forecolor="#666666" rendermode="Lightweight" resolvedrendermode="Classic" backcolor="White"
                                                width="100%" id="radJobTitle" skin="Bootstrap">
                                            </telerik:raddropdownlist>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                <div class="col-md-6">
                                <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <b>EMPLOYEE</b>
                                            </div>
                                <div class="panel-body">
                                   <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                NAME</label>
                                            <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                COMMENT</label>
                                            <textarea id="aempcomment" runat="server" class="form-control" rows="4"></textarea>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <b>LINE MANAGER</b>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                NAME</label>
                                                            <telerik:radcombobox id="cbocoach" runat="server" forecolor="#666666" width="100%"
                                                                autopostback="True" skin="Bootstrap" emptymessage="-- Select Line Manager --"
                                                                filter="Contains" rendermode="Lightweight">
                                                            </telerik:radcombobox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="margin-bottom:7px;" class="col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                COMMENT</label>
                                                            <textarea id="acoachcomment" runat="server" class="form-control" rows="4"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                   <div class="row">
                                        <div class=" col-md-12">
                                            <label style="font-size: 13px">
                                                <i id="acreated" runat="server"></i>
                                            </label>
                                        </div>
                                        <div class=" col-md-12">
                                            <label id="asign" style="font-size: 13px; display:none" runat="server">
                                                Discussed and Agreed By</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-12 text-center">
                                            <label id="astat" runat="server">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 m-t-20 text-center">
                                        <button id="btsave" runat="server" onserverclick="btnSave_Click" type="submit" style="width: 150px" title="Start by setting development plans" 
                                            class="btn btn-primary btn-success">
                                            Start Planning</button>
                                        <asp:Button ID="btComplete" runat="server" Text="Complete" ForeColor="White" Width="150px"
                                            Height="35px" BorderStyle="None" Font-Size="14px" Visible="False" OnClientClick="ConfirmComplete()"
                                            ToolTip="Complete" CssClass="btn btn-info" />
                     
                                        <asp:Button ID="btnagree" runat="server" Text="Agreed" ForeColor="White" Width="150px"
                                            OnClick="btnAgreed_Click" Height="35px" BorderStyle="None" Font-Size="14px" Visible="False"
                                            OnClientClick="ConfirmPlan()" ToolTip="Agree to the development plans" CssClass="btn btn-primary btn-info" />
                                        <button id="btclose" runat="server" onserverclick="btnClose_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-danger">
                                            Back</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                <div id="plan" style="display:none" runat="server"  class="row">
                    <div class=" col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <b>DEVELOPMENT PLAN</b>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20 text-left">
                                    <button id="btaddgrid" runat="server" onserverclick="btnAddGrid_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Add Plan</button>
                                    <asp:Button ID="btdeletegrid" runat="server" Text="Delete Plan" ForeColor="White"
                                        Width="150px" Height="35px" BorderStyle="None" Font-Size="14px"
                                        OnClientClick="ConfirmComplete()" ToolTip="Complete" CssClass="btn btn-primary btn-danger" />
                                    <asp:Button ID="Button1" runat="server" Text="Complete" ForeColor="White" Width="150px"
                                            Height="35px" BorderStyle="None" Font-Size="14px" OnClientClick="ConfirmComplete()"
                                            ToolTip="Complete" CssClass="btn btn-info" />
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                            AllowPaging="True" PageSize="10" DataKeyNames="id" Width="100%" Height="50px"
                                            ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                            ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                <asp:TemplateField HeaderText="Objectives" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("MyObjectives")%>' CommandArgument='<%# Eval("id") %>'
                                                            runat="server" OnClick="OpenDetail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="interventiontype" HeaderText="Intervention Type" SortExpression="interventiontype" />
                                                <asp:BoundField DataField="targetdate" HeaderText="Target Date" SortExpression="targetdate"
                                                    DataFormatString="{0:dd, MMM yyyy}" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               
         <div id="divlastreview" runat="server" class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                LAST REVIEW</label>
                                            <telerik:raddatepicker runat="server" mindate="1900-01-01" culture="en-US" rendermode="Lightweight"
                                                forecolor="#666666" width="100%" resolvedrendermode="Classic" id="datLastReview"
                                                skin="Bootstrap">
                                                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                    fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight" skin="Bootstrap">
                                                 </calendar>
                                                <dateinput width="" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                    rendermode="Lightweight">
                                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                    <FocusedStyle Resize="None"></FocusedStyle>
                                                    <DisabledStyle Resize="None"></DisabledStyle>
                                                    <InvalidStyle Resize="None"></InvalidStyle>
                                                    <HoveredStyle Resize="None"></HoveredStyle>
                                                    <EnabledStyle Resize="None"></EnabledStyle>
                                                </dateinput>
                                                <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                            </telerik:raddatepicker>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                LAST RATING</label>
                                            <input id="alastrating" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                 </div>
            </div>
        <%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />

        <table width="100%" >
            <tr>
                <td style="width:5%" >
                </td>
                <td style="width:90%" >
                    <div>
            <asp:Label ID="lblstatus" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                ForeColor="Red" Width="100%"></asp:Label>
        </div>
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="style1" style="background-color: #1BA691; text-align: center;">
                                <asp:Label ID="Label11" runat="server" Text="Development Plan: " Font-Names="Verdana"
                                    Font-Size="16px"></asp:Label>
                                
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Development Plan"
                                    Font-Bold="True" ForeColor="#666666"></asp:Label>
                            </td>
                            <td style="width: 35%">
                               
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Department"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Employee Number"></asp:Label>
                            </td>
                            <td style="width: 35%">
                               
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Location"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Employee Name"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="100%" ID="txtName" Enabled="False"></asp:TextBox>
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Date"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Job Title"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Last Review Date"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Job Grade"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label61" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                                    ForeColor="#666666" Text="Months in Position"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                            </td>
                            <td style="width: 35%">
                                <asp:Label ID="lblempemail" runat="server" Font-Names="Verdana" Font-Size="12px"
                                    Visible="False"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label55" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                                    ForeColor="#666666" Text="Last Rating"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="20%" ID="txtLastRating"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Size="14px" Text="Line Manager"
                                    Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Line Manager"></asp:Label>
                            </td>
                            <td style="width: 35%" valign="top">
                               
                            </td>
                            <td class="style3">
                                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Grade"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="100%" ID="txtCoachGrade"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="E-Mail"></asp:Label>
                            </td>
                            <td style="width: 35%" valign="top">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="100%" ID="txtCoachMail"></asp:TextBox>
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 35%">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style4">
                                <asp:Label ID="Label62" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                                    Font-Size="12px" Text="Employee Comment"></asp:Label>
                            </td>
                            <td style="width: 35%" valign="top">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="100%" ID="txtEmpComment" Height="61px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td valign="top" class="style3">
                                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                                    ForeColor="#666666" Text="Line Manager Comment"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                    ForeColor="#666666" Font-Size="12px" Width="100%" ID="txtCoachComment" Height="61px"
                                    TextMode="MultiLine" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblcreated" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                                    ForeColor="#666666" Font-Italic="true" Width="100%"></asp:Label>
                            </td>
                            <td colspan="2" valign="top">
                                <asp:Label ID="lblsign" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Discussed and Agreed By"
                                    Font-Bold="True" ForeColor="#666666" Visible="False" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                &nbsp;
                            </td>
                            <td style="width: 35%" valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                                                Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnComplete" runat="server" Text="Complete" BackColor="#6699FF" ForeColor="White"
                                                Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" ToolTip="Plan Completed and Send Notification to Coach"
                                                OnClientClick="ConfirmComplete()" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCoachSave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                                                Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" Visible="False" ToolTip="Save Comment" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAgreed" runat="server" Text="Agreed" BackColor="#6699FF" ForeColor="White"
                                                Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" Visible="False" OnClientClick="ConfirmPlan()"
                                                ToolTip="Click to Agreed to Plan" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClose" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                                                Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style3">
                            </td>
                            <td style="width: 35%">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" Font-Names="Verdana" Font-Size="14px" Text="Development Plan"
                                    Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddGrid" runat="server" Text="Add Plan" BackColor="#1BA691" ForeColor="White"
                                    Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                    Font-Names="Verdana" Font-Size="11px" />
                            </td>
                            <td>
                                <asp:Button ID="btnDeleteGrid" runat="server" Text="Delete Plan" BackColor="#FF3300"
                                    ForeColor="White" Width="100px" Height="20px" BorderStyle="None" Style="margin-top: 0px"
                                    Font-Bold="True" Font-Names="Verdana" Font-Size="11px" 
                                    OnClientClick="Confirm()" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="width: 15%" colspan="4">
                                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True" BorderStyle="Solid"
                                    Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="id" Width="70%"
                                    Height="50px" ToolTip="click row to select record" Font-Size="12px" AutoGenerateColumns="False"
                                    GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                    CssClass="table" ShowHeaderWhenEmpty="true" EmptyDataText="No data to dislay">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="1px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" />
                                        <asp:TemplateField HeaderText="Development Objectives" ItemStyle-Width="200px" ItemStyle-Font-Bold="true"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text='<%# Eval("MyObjectives")%>' CommandArgument='<%# Eval("id") %>'
                                                    runat="server" OnClick="OpenDetail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="interventiontype" ItemStyle-Width="80px" HeaderText="Intervention Type" />
                                        <asp:BoundField DataField="targetdate" ItemStyle-Width="50px" HeaderText="Target Date" />
                                    </Columns>
                                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" VerticalAlign="Middle" />
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
                                <script type="text/javascript">
                                    function openWindow(code) {
                                        window.open("MyDevObjectives.aspx?id=" + code, "open_window", "width=500,height=600");
                                    }
                                </script>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                </td>
                <td style="width:5%" >
                </td>
            </tr>
        </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
