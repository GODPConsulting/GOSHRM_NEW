<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="WorkForceApproval.aspx.vb"
    Inherits="GOSHRM.WorkForceApproval" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
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
           function Complete() {
               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Mark plan as complete and send notification to HR?")) {
                   confirm_value.value = "Yes";
               } else {
                   confirm_value.value = "No";
               }
               document.forms[0].appendChild(confirm_value);
           }
    </script>

   
    <body>
        <form id="form1">
        <div class="main-wrapper">
            <div class="row">
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                            <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                            <asp:Label ID="lblDate" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-0">
                    <h5 id="pagetitle" runat="server" class="page-title">
                        Workforce Plan</h5>
                </div>
            </div>
             <div class="row">
                <div class=" col-md-8">
                    <div class="form-group">
                        <label>
                            COMPANY</label>
                        <input id="acompany" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-8">
                    <div class="form-group">
                        <label>
                            OFFICE</label>
                        <input id="aoffice" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-4">
                    <div class="form-group">
                        <label>
                            YEAR</label>
                        <input id="ayear" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
                <div class=" col-md-4">
                    <div class="form-group">
                        <label>
                            BUDGET</label>
                        <input id="abudget" runat="server" class="form-control" type="text" disabled="disabled" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class=" col-md-8">
                    <a href="#"><b>Workforce Plan Status: </b><b id="lbstat" runat="server">On-going</b></a>
                </div>
            </div>
            <div id="divapproval" runat ="server" class="row">
                <div  class=" col-md-4">
                    <div class="form-group">
                        <button id="btnapprovallink" runat="server" onserverclick="lnkApprovalStat_Click" type="submit"
                            class="btn btn-default">
                            View Approval Status</button>
                    </div>
                </div>
                <div  class=" col-md-4">
                    <div class="form-group">
                        <button id="Button1" runat="server" onserverclick="lnkapprovalupdate_Click" type="submit"
                            class="btn btn-default">
                            Update Approval Status</button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 m-t-20">
                    <button id="btcancel" runat="server" onserverclick="btnBack_Click" type="submit"
                        style="width: 150px" class="btn btn-primary btn-info">
                        << Back</button>
                </div>
            </div>
        </div>
         <div class="row">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>BUDGET SUMMARY</b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gridsummary" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="False" PageSize="12" DataKeyNames="rows"
                                Width="100%" Height="50px" Font-Size="12px"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>                                    
                                    <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                    <asp:TemplateField HeaderText="Month" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("endmonth")%>' CommandArgument='<%# Eval("endmonth") %>'
                                                runat="server" OnClick="DrillDown"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="payrollbudget" HeaderText="Payroll Budget"
                                        ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="gratuity" HeaderText="Gratuity"
                                        ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="trainingbudget" HeaderText="Training Budget"
                                        ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="otherexpense" HeaderText="Other Expense"
                                        ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" />
                                    <asp:BoundField DataField="budget" HeaderText="Total Budget"
                                        ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" />
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=gridsummary] td").hover(function () {
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
            
<div id="divdetail" runat ="server" class="row">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b id="divdetailheader" runat="server"></b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-3 col-md-2 col-xs-6">
                            <div class="form-group form-focus">
                                <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                                    placeholder="Search..." />
                                <button id="btnsearch" type="button" runat="server" class="glyphicon glyphicon-search"
                                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                                </button>
                            </div>
                        </div>                        
                    </div>
                     
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="15" DataKeyNames="rows"
                                Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>                                    
                                    <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                        <asp:TemplateField HeaderText="Job Title" ItemStyle-Font-Bold="true" SortExpression ="jobtitle">
                            <ItemTemplate>                             
                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/WorkForcePlanDetailUpdate?id={0}&year={1}",
                     HttpUtility.UrlEncode(Eval("id").ToString()),HttpUtility.UrlEncode(Eval("budgetyear").ToString())) %>' 
                                            Text='<%# Eval("jobtitle")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="jobgrade" HeaderText="Job Grade" SortExpression ="jobgrade" />                       
                            <asp:BoundField DataField="amountrequired" HeaderText="Requirement" ItemStyle-HorizontalAlign="right" SortExpression="amountrequired"/> 
                            <asp:BoundField DataField="payrollbudget"  HeaderText="Payroll Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" SortExpression="payrollbudget"/>  
                            <asp:BoundField DataField="gratuity" HeaderText="Gratuity" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" SortExpression="gratuity"/> 
                            <asp:BoundField DataField="trainingbudget"  HeaderText="Training Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" SortExpression="trainingbudget"/>                             
                            <asp:BoundField DataField="otherexpense"  HeaderText="Other Expense" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" SortExpression="otherexpense"/>    
                            <asp:BoundField DataField="budget"  HeaderText="Budget" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n}" SortExpression="budget"/> 

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

        <div class="row">
            <div class="col-md-12 text-right">
                <div class="form-group">
                    <label style="font-size:11px"><i id="createdon" runat="server"></i></label>                    
                </div>
                <div class="form-group">
                    <label style="font-size:11px"><i id="updatedon" runat="server"></i></label>                    
                </div>
            </div>
        </div>


       
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 330px;
        }
        </style>
</asp:Content>
