<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Appraisals.aspx.vb"
    Inherits="GOSHRM.Appraisals" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">

      <head>
        <title></title>
        <link rel="stylesheet" href="../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
        <link rel="stylesheet" href="../../AdminLTE/dist/css/Admin-lte.min.css"/>
        <link rel="stylesheet" href="../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
        <link rel="stylesheet" href="../../Skins/_all-skins.min.css"/>
        <link rel="stylesheet" href="../../css/font-awesome.min.css"/>
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
    <body>
        <form id="form1">
        <div class="container col-md-12">
         <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>             
                <strong id="msgalert" runat="server">Danger!</strong>               
            </div>
            <asp:TextBox ID="lblcycleid" Visible= "false" runat="server"></asp:TextBox>
        </div>
            <div id="content" runat="server">
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
             <div class="row">
            <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">  
                 <button id="btnBack" data-toggle="tooltip" data-original-title="Back" type="button" runat="server" class="fa fa-backward btn btn-default btn-sm" onserverclick="btnBack_Click"
                    style="height: 35px; width:35px;"></button>                                
                    <button id="btnUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited): EmpID, AdjustedScore (%) {note: use -1 as adjusted score to cancel adjustment}" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                </div> 
                <%--<div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                   <asp:Button ID="btnUpload" runat="server" Font-Names="Verdana"
                            Font-Size="12px" BorderStyle="None" CssClass="btn btn-primary btn-success" ForeColor="White" Height="35px" Text="Upload Adjustment"
                            Width="100%" ToolTip="CSV File (Comma Delimited): EmpID, AdjustedScore (%) {note: use -1 as adjusted score to cancel adjustment}" />
                </div> --%>   
        </div>

        <div class="row">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="1000" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="10px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666"
                    BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" CssClass="table table-condensed">
                    <RowStyle BackColor="white" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows" />
                        
                        <asp:TemplateField HeaderText="Employee" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <a href="AppraisalsFeedback.aspx?id=<%# Eval("id") %>">
                                    <%# Eval("name")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="jobgrade" ItemStyle-Width="9%" HeaderText="Job Grade"
                            SortExpression="jobgrade" />--%>
                        <asp:BoundField DataField="dept" HeaderText="Dept"
                            SortExpression="dept" />
                        <asp:BoundField DataField="revieweri" HeaderText="Reviewer I"
                            ItemStyle-HorizontalAlign="Right" SortExpression="revieweri" />
                        <asp:BoundField DataField="reviewerii" HeaderText="Reviewer II"
                            ItemStyle-HorizontalAlign="Right" SortExpression="reviewerii" />
                        <asp:BoundField DataField="Reviewee" HeaderText="Reviewee" ItemStyle-HorizontalAlign="Right"
                            SortExpression="Reviewee" />
                        <asp:TemplateField HeaderText="360 (Managers)" SortExpression="grade360_managers"
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Review360.aspx?summaryid={0}&type=s",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=800,height=700,scrollbars,resizable'); return false;"
                                    Text='<%# Eval("grade360_managers")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="360 (Peers)" SortExpression="grade360_peers"
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Review360.aspx?summaryid={0}&type=p",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=800,height=700,scrollbars,resizable'); return false;"
                                    Text='<%# Eval("grade360_peers")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="360 (Reports)" SortExpression="grade360_directreports"
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Review360.aspx?summaryid={0}&type=d",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=800,height=700,scrollbars,resizable'); return false;"
                                    Text='<%# Eval("grade360_directreports")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="360 (Self)" SortExpression="grade360_juniors"
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# string.Format("~/Module/Performance/Review360.aspx?summaryid={0}&type=m",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                    Text='<%# Eval("grade360_juniors")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="OverallGrade"  HeaderText="Overall"
                            ItemStyle-HorizontalAlign="Right" SortExpression="OverallGrade" />
                        <asp:BoundField DataField="remark"  HeaderText="Overall Remark"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="score"  HeaderText="Score(%)" ItemStyle-HorizontalAlign="Right"
                            SortExpression="score" />
                        <asp:BoundField DataField="AdjustedScore"  HeaderText="Adj(%)"
                            ItemStyle-HorizontalAlign="Right" SortExpression="AdjustedScore" />
                        <%--<asp:BoundField DataField="Feedback360Count"  HeaderText="360 Reviewers"
                            ItemStyle-HorizontalAlign="Right" SortExpression="Feedback360Count" />--%>
                        <%--<asp:BoundField DataField="recommendation"  HeaderText="Rec"
                            SortExpression="recommendation" />--%>
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
                <script type="text/javascript">
                    function openWindow(code) {
                        window.location = "AppraisalsFeedback.aspx?id=" + code;
                    }
                </script>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            </div>
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
