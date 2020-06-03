<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CoachObjectives.aspx.vb"
    Inherits="GOSHRM.CoachObjectives" EnableEventValidation="false" %>--%>


 <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CoachObjectives.aspx.vb"
    Inherits="GOSHRM.CoachObjectives" EnableEventValidation="false" Debug="true" %>


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
            if (confirm("Do you want to sign Appraisal Objective as Agreed & Discussed?")) {
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
               if (confirm("Appraisal Objective Completed and Send Notification to Coach?")) {
                   confirm_complete.value = "Yes";
               } else {
                   confirm_complete.value = "No";
               }
               document.forms[0].appendChild(confirm_complete);
           }
    </script>
    
</head>
<body>
    <form id="form1" action="">
    <div class="container col-md-12">
        <div class ="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        </div>
        <div class="row">
            <h5 id="page_title" runat="server" class="page-title">
                Appraisal Objective of Coach</h5>
                <asp:Label ID="lblTotal0" runat="server" Font-Size="1px" Height="1px" Width="1px" Visible="false"></asp:Label>
                <asp:Label ID="lblid" runat="server" Font-Size="1px" Height="1px" Width="1px" Visible="false"></asp:Label>
                <asp:TextBox ID="txtEmpID" runat="server" Font-Names="Verdana" Font-Size="1px" ForeColor="#666666" BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="row">
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            REVIEW YEAR</label>
                        <input id="areviewyear" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-6">
                    <div class="form-group">
                        <label>
                            REVIEW PERIOD</label>
                            <input id="areviewperiod" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            EMPLOYEE</label>
                        <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>               
                <div class=" col-md-12">
                    <div class="form-group">
                        <label>
                            JOB TITLE</label>
                        <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
             <div class="col-md-12 m-t-20 text-center">
                                            <button id="btnback" runat="server" onserverclick="btnClose_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-info">
                                                << Back</button>
                                        </div>
            <div class="row">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>PERFORMANCE OBJECTIVES</b></div>
                    <div class="panel-body">                        
                        <telerik:RadGrid RenderMode="Lightweight" ID="gridCompetency" runat="server" PageSize="20"
                                AllowSorting="True" AllowMultiRowSelection="True" AllowPaging="True" ShowGroupPanel="True"
                                AutoGenerateColumns="False"  BorderWidth="1px" BorderColor="#CCCCCC" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                DataKeyNames="ID" GridLines="Both" EnableGroupsExpandAll="True" ShowFooter="True"
                                ShowStatusBar="True" Width="100%" Font-Names="Verdana" Font-Size="10px">
                                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <MasterTableView Width="100%" EnableGroupsExpandAll="True">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField HeaderText="KPI Category" FieldAlias="" FieldName="KPIType"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="KPIType" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn SortExpression="kpiobjectives" HeaderText="KPI Type" ItemStyle-VerticalAlign="Top" UniqueName="kpiobjectives"
                                            HeaderButtonType="TextButton"  DataField="kpiobjectives">                                            
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn SortExpression="objectives" HeaderText="Objectives" ItemStyle-VerticalAlign="Top" UniqueName="objectives"
                                            HeaderButtonType="TextButton" DataField="objectives"    >                                        
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="successtarget" HeaderText="Success Target" ItemStyle-VerticalAlign="Top" UniqueName="successtarget"
                                            HeaderButtonType="TextButton"  DataField="successtarget">      
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="keyactions" HeaderText="Key Action" ItemStyle-VerticalAlign="Top" UniqueName="keyactions"
                                            HeaderButtonType="TextButton" DataField="keyactions">                                        
                                        </telerik:GridBoundColumn>                                                         
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                                <FilterMenu RenderMode="Lightweight">
                                </FilterMenu>
                                <HeaderContextMenu RenderMode="Lightweight">
                                </HeaderContextMenu>
                            </telerik:RadGrid>
                    </div>
                </div>
            </div>   
 </div>
 
      <%--                      <asp:Label ID="lblTotal0" runat="server" style="font-weight: 700" 
                                Font-Bold="True" Font-Names="Verdana" 
        Font-Size="11px" Visible="False"></asp:Label>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <table width="100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="style1" colspan="4" style="background-color: #1BA691; text-align: center;">
                            <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                            Appraisal Objective</td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                Text="Review Year" Font-Bold="True" ForeColor="#666666"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" 
                                Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtYear" Enabled="False" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 15%; text-indent: 15px">
                            <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Job Title"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtJobTitle" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                Text="Review Period" Font-Bold="True" ForeColor="#666666"></asp:Label>
                        </td>
                        <td style="width: 35%">
                           <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" 
                                Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtperiod" Enabled="False" 
                                ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 15%; text-indent: 15px">
                            <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Job Grade"></asp:Label>
                            </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtJobGrade" ReadOnly="True"></asp:TextBox>
                            
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" 
                                Text="Employee Number" Font-Bold="True" ForeColor="#666666"></asp:Label></td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtEmpID" runat="server" Width="30%" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                                BorderColor="#CCCCCC" BorderWidth="1px" Enabled="False" ReadOnly="True"></asp:TextBox>
                            </td>
                        <td style="width: 15%; text-indent: 15px">
                        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Department"></asp:Label>
                           </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtDept" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="Employee Name"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtName" Enabled="False" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 15%; text-indent: 15px">
                            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="Location"></asp:Label>
                        </td>
                        <td style="width: 35%">
                            <asp:TextBox runat="server" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="12px" Width="100%" ID="txtLocation" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 15%">
                             <asp:Button ID="btnClose" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                                Width="150px" Height="20px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                Font-Names="Verdana" Font-Size="12px" />
                        </td>
                        <td style="width: 35%" valign="top">
                            <table>
                            <tr>
                                                              <td>
                                  
                                </td>
                            </tr>
                                
                            </table>
                            
                        </td>
                        <td colspan="2">
                            
                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
                            
                        </td>
                    </tr>
                 
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label21" runat="server" Font-Names="Verdana" Font-Size="14px" Text="Performance Objectives"
                                Style="background-color: #1BA691; color: #FFFFFF; font-weight: 700;" Width="100%"></asp:Label>
                        </td>
                    </tr>
                  
                    <tr>
                        <td style="width: 15%" colspan="4">
                            
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
                         
                        </td>
                    </tr>             
                </table>
            </td>
            
        </tr>
    </table>
      <table>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
</asp:Content>