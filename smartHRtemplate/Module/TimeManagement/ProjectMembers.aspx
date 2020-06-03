<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ProjectMembers.aspx.vb"
    Inherits="GOSHRM.ProjectMembers" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">  
    <head >
        <title></title>
    </head> 
    
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
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        </style>
    <body>
        <form id="form1">
        <%--<div style="width:100%" >
            <table width="100%" >
                <tr>
                    <td >                        
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>--%>
          <div class="container col-md-12">
         <div class="row">
                 <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
         <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server"></b></h5>
                    </div>
                 <div class="panel-body">
       
        <div class="row" style="">
            <div class="form-group col-md-2 col-sm-3 col-xs-6 pull-right">
                <asp:Button ID="Button2" runat="server" Text="&lt; Back" CssClass="btn btn-primary btn-danger" ForeColor="White"
                    Width="100%" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
                    </div>
        </div>
        <div class="row" style="">
            <div>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="empid" CssClass="table table-condensed"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to view detail"
                    Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                    <RowStyle BackColor="White" />
                    <Columns>
                                        
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" />
                       <asp:TemplateField HeaderText="EMP ID">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/TimeManagement/EmployeeCalendar.aspx?EMPID={0}&PID={1}",
                     HttpUtility.UrlEncode(Eval("empid").ToString()),HttpUtility.UrlEncode(Eval("ProjectID").ToString())) %>' Text='<%# Eval("EMPID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>

                        <asp:BoundField DataField="Name" ItemStyle-Width="40%" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Office" ItemStyle-Width="40%" HeaderText="Unit/Office" />

                    </Columns>
                    <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="center" />
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
                        window.open("JobPostingsUpdate.aspx?id=" + code, "open_window", "width=800,height=800");
                    }
                </script>
                <script type="text/javascript">
                    function openApplicants(code) {
                        window.open("Applicants.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                    }
                </script>
                 <script type="text/javascript">
                     function openShortlists(code) {
                         window.open("ShortLists.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                     }
                </script>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        </style>
</asp:Content>
