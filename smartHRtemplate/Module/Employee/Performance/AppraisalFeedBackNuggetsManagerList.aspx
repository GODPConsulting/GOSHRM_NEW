<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="AppraisalFeedBackNuggetsManagerList.aspx.vb"
    Inherits="GOSHRM.AppraisalFeedBackNuggetsManagerList" EnableEventValidation="false" Debug="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
   
     <head>
        <title></title>
    </head>
     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllEmp(Checkbox) {
             var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
             for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                 GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
    </script>

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
         function OpenMaxWindow(file) {
             var xMax = screen.width, yMax = screen.height;
             window.open(file, 'open_window', 'scrollbars=yes,width=' + xMax + ',height=' + yMax + ',top=0,left=0,resizable=yes');
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
        .style25
        {
            width: 134px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        </style>
    <body >
        <form id="form1" action ="">
        <div class="container col-md-12">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
       
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="divdetailheader" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
<%--             <asp:LinkButton ID="btnDelete" data-toggle="tooltip" OnClientClick="Confirm()" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" onserverclick="btnAdd_Click" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> --%>
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                                                    <telerik:radcombobox id="cboStartReview" runat="server" skin="Bootstrap" forecolor="#666666"
                                                        width="100%" rendermode="Lightweight" autopostback="True">
                                                    </telerik:radcombobox>
                </div>
        </div>
        <div class="row table-responsive">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="emp_id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
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
                        <asp:BoundField DataField="emp_name" HeaderText="Reviewee Name" SortExpression="Reviews"  />
                         <asp:TemplateField HeaderText="No of Feedback Reviews" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true"
                            SortExpression="Reviews">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Performance/AppraisalFeedbackNuggetsReviewList?id={0}",
                     HttpUtility.UrlEncode(Eval("emp_id").ToString())) %>' Text='<%# Eval("Reviews")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                        <asp:BoundField DataField="emp_jgrade" HeaderText="Reviewee Jobgrade" SortExpression="emp_jgrade"/>
                        <asp:BoundField DataField="emp_title" HeaderText="Reviewee Jobtitle" SortExpression="emp_title"/> 
                        <asp:BoundField DataField="Average Scores" ItemStyle-HorizontalAlign="Right" HeaderText="Average Scores(%)" SortExpression="Average Scores"/> 
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboStartReview" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
            </div></div></div> 


       
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
