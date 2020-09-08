<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="successionupdate.aspx.vb"
    Inherits="GOSHRM.successionupdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
   <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
         <script type="text/javascript">

             function closeWin() {
                 popup.close();   // Closes the new window
             }
    </script>
    </telerik:RadCodeBlock>
   
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript" language="javascript">
            //    Grid View Check box
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
                for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                }
            }
    </script>
    </telerik:RadCodeBlock>
    
     <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
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
     </telerik:RadCodeBlock>

</head>

<body>
    <form id="form1" >
    <div class="container col-md-12">
            <div class="row">
                <div class="col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                        <asp:Label ID="lblID" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Label ID="lblhodid" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                                <asp:Label ID="lblmanagerid" runat="server" Visible="False" Font-Size="1px"></asp:Label>                             
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                        Succession Plan</h5>
                </div>
               
               <div id="divapprovalview" runat="server" class="col-xs-4 text-right m-b-30 pull-right" >
                    <button id="approvallink" runat="server" onserverclick="lnkApprovalStat_Click" type="submit"
                                        class="btn btn-success" title="manage approvals">
                                        Approval Status
                                    </button>
                </div>
            
           
            </div>
            <div class="row">
                <div class="col-md-10">
                    <div class="form-group">
                        <label>
                            EMPLOYEE *</label>
                        <telerik:radcombobox id="cboEmployee" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                            filter="Contains" width="100%" autopostback="True" rendermode="Lightweight" skin="Bootstrap"
                            emptymessage="-- Select Employee --">
                        </telerik:radcombobox>
                    </div>
                </div>
            </div>
            
            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                <ContentTemplate>
            <div class="row">
                <div class="col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b></b>
                        </div>
                        <div class="panel-body">
                            <div>                                
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                PERFORMANCE RATING</label>                                            
                                                    <input id="aperformancerating" runat="server" class="form-control" type="text" disabled="disabled" />                                                
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                JOB TITLE</label>
                                         
                                                    <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                                                
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                JOB GRADE</label>
                                                    <input id="ajobgrade" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divcompany" runat="server" class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                COMPANY</label>
                                                    <input id="acompany" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                OFFICE</label>
                                                    <input id="aoffice" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                LOCATION</label>
                                                    <textarea id="alocation" runat="server" class="form-control" rows="2" cols="1" readonly="readonly"></textarea>
                                        </div>
                                    </div>
                                </div>
                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
            <%--End of Panel--%>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>PLANNED POSITION</b>
                        </div>
                        <div class="panel-body">
                            <div>
                                
                                  <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                JOB TITLE *</label>
                                            <telerik:radcombobox id="radplanjobtitle" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap" 
                                                EmptyMessage="-- Select Job Title --">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                JOB GRADE *</label>
                                            <telerik:radcombobox id="radplanjobgrade" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap" 
                                                EmptyMessage="-- Select Job Grade --">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div id="divplancompany" runat="server" class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                COMPANY *</label>
                                            <telerik:radcombobox id="cboplancompany" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap" 
                                                autopostback="True" EmptyMessage="-- Select Company --">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                OFFICE *</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="radplanoffice" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                        filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap" 
                                                        EmptyMessage="-- Select Office --">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboplancompany" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                LOCATION *</label>
                                            <telerik:radcombobox id="radplanlocation" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                filter="Contains" width="100%" rendermode="Lightweight" skin="Bootstrap" 
                                                EmptyMessage="-- Select Location --">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                   </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                SUCCESSION STATUS</label>
                                            <telerik:radcombobox id="radstatus" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                                width="100%" rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                COMMENT</label>
                                            <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1" placeholder="Comment"></textarea>
                                        </div>
                                    </div>
                                </div>
                                 <div class="row">
                        <div class="m-t-20">
                            <button id="btsave" runat="server" onserverclick="btnSave_Click" type="submit" 
                                class="btn btn-primary btn-success"  >
                                Save & Update</button>
                            <button id="btclose" runat="server" onserverclick="btnclose_Click" type="submit"
                                class="btn btn-primary btn-danger">
                                << Back</button>                       
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
           


            <div id="divdetail" runat="server" class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>Succession Plan Breakdown</b>
                        </div>
                        <div class="panel-body">
                           <div class="row">
                                 <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                                        <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                        <button id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                    </div>
                                <div id="divbtndetail" runat="server" class="col-sm-6 col-md-6 col-xs-12 pull-right">
                                    <button id="btadddetail" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 100px" class="btn btn-primary btn-success" title="add detail to budget">
                                        Add Detail</button>
                                    <asp:Button ID="btdeletedetail" runat="server" Text="Delete Detail" 
                                        OnClientClick="Confirm()" BackColor="#FF3300" ForeColor="White" Width="100px"
                                        Height="34px" CssClass="btn btn-primary btn-danger" BorderStyle="None" Font-Names="Verdana"
                                        Font-Size="13px" />
                                </div>
                            </div>
                            <div class="row">
                            <div >
                                
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True"
                                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="5" DataKeyNames="id"
                                            Width="100%" Height="50px" ToolTip="click row to select record"
                                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />                                                
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" ItemStyle-Font-Underline="true" SortExpression="action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("action")%>' CommandArgument='<%# Eval("id") %>'
                                                            runat="server" OnClick="viewdetail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="expectedstartdate" HeaderText="Target Start Date" SortExpression="expectedstartdate" DataFormatString="{0:dd, MMM yyyy}" />
                                                <asp:BoundField DataField="expectedenddate" HeaderText="Target Due Date" SortExpression="expectedenddate" DataFormatString="{0:dd, MMM yyyy}"/>
                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
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
            </div>
        </div>
  <%--  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #1BA691">
                Succession Plan
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="lblID" runat="server" Visible="False" Height="2px" Width="2px" Font-Names="Verdana"
                    Font-Size="8px"></asp:Label>
            </td>
            <td class="style9">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Text="Employee" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style9">
    
                <telerik:RadComboBox ID="cboEmployee" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Filter="Contains" Width="100%" AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Company" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel21" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadDropDownList ID="radCompany" runat="server" DefaultMessage="-- Select --" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="11px" Height="23px" Width="100%">
                        </telerik:RadDropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Text="Unit/Office" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radoffice" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="llll" runat="server" Font-Names="Verdana" Text="Location" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <telerik:RadComboBox ID="radlocation" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Text="Job Grade" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
                &nbsp;
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radjobgrade" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Job Title"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radjobtitle" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Performance Rating"></asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtrating" runat="server" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Width="30%" Font-Size="13px" Font-Names="Verdana"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Text="Head of Department"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radhod" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Text="Supervisor" Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radsup" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Text="Planned Company" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <telerik:RadDropDownList ID="radPlancompany" runat="server" DefaultMessage="-- Select --" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="11px" Height="23px" Width="100%" AutoPostBack="True">
                </telerik:RadDropDownList>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label15" runat="server" Font-Names="Verdana" Text="Planned Unit/Office" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                    <ContentTemplate>
                        <telerik:RadComboBox ID="radplanoffice" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                        </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radPlancompany" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Text="Planned Location" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style9">
                <telerik:RadComboBox ID="radplanlocation" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending" Owner="radplanlocation" />
                        <telerik:RadComboBoxItem runat="server" Text="Cancelled" Value="Cancelled" Owner="radplanlocation" />
                        <telerik:RadComboBoxItem runat="server" Text="Rejected" Value="Rejected" Owner="radplanlocation" />
                        <telerik:RadComboBoxItem runat="server" Text="Approved" Value="Approved" Owner="radplanlocation" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Text="Planned Job Grade" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
                &nbsp;
            </td>
            <td class="style9">
                <telerik:RadComboBox ID="radplanjobgrade" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Filter="Contains" Width="100%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Planned Job Title"></asp:Label>
            </td>
            <td class="style9">
                <telerik:RadComboBox ID="radplanjobtitle" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Filter="Contains" Width="100%">         
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Succession Status"></asp:Label>
            </td>
            <td class="style9">
                <telerik:RadComboBox ID="radstatus" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Width="100%">                    
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style10" valign="top">
                <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px">Comment</asp:Label>
            </td>
            <td class="style9">
                <asp:TextBox ID="txtcomment" runat="server" BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666"
                    Width="100%" Font-Size="13px" BorderStyle="Solid" Height="100px" TextMode="MultiLine"
                    Font-Names="Verdana"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                
            </td>
            <td class="style9">
                
                    <asp:LinkButton ID="lnkApprovalStat" runat="server" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="11px" 
                    ToolTip="view approvals and assign approvers to approve request">Approval Status View</asp:LinkButton>
                  
            </td>
        </tr>
        <tr>
            <td class="style10">
                <asp:Button ID="btnsave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Size="11px" />
            </td>
            <td class="style9">
                <asp:Button ID="Button2" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Size="11px" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td valign="top" class="style6">
                <asp:Label ID="Label18" runat="server" Font-Names="Verdana" Text="DETAILS" Font-Size="13px" ForeColor="#666666"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="style8">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add Detail" BackColor="#1BA691" ForeColor="White"
                                Width="120px" Height="20px" BorderStyle="None" Font-Size="11px" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete Detail" BackColor="#FF3300"
                                ForeColor="White" Width="120px" Height="20px" BorderStyle="None" Font-Size="11px"
                                OnClientClick="Confirm()" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" class="style6">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True" BorderStyle="Solid"
                    Font-Names="Verdana" AllowPaging="True" PageSize="15" DataKeyNames="id" Width="100%"
                    Height="50px" Font-Size="10px" AutoGenerateColumns="False" 
                    GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" 
                    BorderColor="#CCCCCC">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="5px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" />
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-Font-Bold="true"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" Text='<%# Eval("action")%>' CommandArgument='<%# Eval("id") %>'
                                    runat="server" OnClick="viewdetail"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Responsibilities" ItemStyle-Width="150px" HeaderText="Reponsibility" />
                        <asp:BoundField DataField="expectedstartdate" ItemStyle-Width="70px" HeaderText="Expected Start Date"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="expectedenddate" ItemStyle-Width="70px" HeaderText="Expected Due Date"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="status" ItemStyle-Width="80px" HeaderText="Status" />
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                </asp:GridView>
            </td>
        </tr>
       
    </table>--%>
    </form>
</body>
</html>
</asp:Content>