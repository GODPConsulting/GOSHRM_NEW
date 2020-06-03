<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="SecondRevewAppraisalObjectivesForm.aspx.vb"
    Inherits="GOSHRM.SecondRevewAppraisalObjectivesForm" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
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
         function openWindow(code) {
             window.open("AppObjectiveUpdate.aspx?id=" + code, "open_window", "width=1200,height=800");
         }
    </script>
    <body>
        <form id="form1">
        <div>
           <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
       
                    <div>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5><b id="pagetitle" runat="server"></b></h5>
                            </div>
                         <div class="panel-body">
                        <div class="row">
                            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                <input id="txtSurbodinateSearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                <button onserverclick="btnSurbodinateSearch_Click" id="btnSurbodinateSearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                            </div>
                             <div class="col-sm-6 col-md-3 col-xs-12 pull-right">
                                <telerik:RadComboBox ID="cboBudgetYear"  Skin="Bootstrap" runat="server" Font-Names="Verdana" 
                                         Width="100%" ForeColor="#666666" AutoPostBack="True">                                        
                                    </telerik:RadComboBox>

                            </div>
                         </div>
                    </div>
                   
                    <div>
                        <div>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=gridSurbodinate] td").hover(function () {
                                        $("td", $(this).closest("tr")).addClass("hover_row");
                                    }, function () {
                                        $("td", $(this).closest("tr")).removeClass("hover_row");
                                    })
                                })
                            </script>
                             <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                            <asp:GridView ID="gridSurbodinate" runat="server" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                                Width="100%" Height="50px" ToolTip="click row to select record"
                                Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                BorderColor="#CCCCCC" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                                        <ItemStyle Width="5px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ReviewYear" HeaderText="Review Year">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Review Period" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" SortExpression="period">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Performance/AppObjectiveUpdate.aspx?id={0}",
                                        HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                Text='<%# Eval("period")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                
                                    <asp:BoundField DataField="EmpName" HeaderText="Employee">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JobGrade" HeaderText="Job Grade">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Dept" HeaderText="Office/Dept">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AppraisalStatus" HeaderText="Review Status">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="empsubmited" HeaderText="Reviewee Submit">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mgrsubmited" HeaderText="Reviewer Submit">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="empgrade" HeaderText="Reviewee Point">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mgrgrade" HeaderText="Reviewer 1 Point">
                                    </asp:BoundField>
                                     <asp:BoundField DataField="mgrgrade2" HeaderText="Reviewer 2 Point">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="overallgrade" HeaderText="Overall Point">
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="" ItemStyle-Width="100px" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Performance/AppraisalFeedback.aspx?id={0}",
                                        HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                Text='Appraisal Form' Enabled="True" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center"></HeaderStyle>
                            </asp:GridView>  
                            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboBudgetYear" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel> 
                        </div>                       
                    </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style22
        {
        }
    </style>
</asp:Content>
