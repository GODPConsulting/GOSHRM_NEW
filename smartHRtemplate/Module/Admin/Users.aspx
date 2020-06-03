<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Users.aspx.vb" Inherits="GOSHRM.Users" EnableEventValidation="false" Debug="true"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



 <html xmlns="http://www.w3.org/1999/xhtml">
<head >
        <script type="text/javascript" language="javascript">
            //    Grid View Check box
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
                for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                }
            }
    </script>

    <title>Users</title>
<%--    <link rel="stylesheet" href="../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../../AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="../../Skins/_all-skins.min.css"/>
    <link rel="stylesheet" href="../../css/font-awesome.min.css"/>--%>

    <script type = "text/javascript">
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
            font-family: Verdana;
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
            width: 129px;
        }
        .style26
        {
            width: 22px;
        }
        .style27
        {
            width: 35px;
        }
        .style29
        {
        }
    </style>
</head>


<body>
    <form id="form1" action ="" >
        <div class="container col-md-12">
            <div style="display:none;" id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5> 
                </div>
             <div class="panel-body">
              <div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                            <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                         <div class="col-md-4 col-sm-6 col-xs-12 form-group pull-right">
                          <button id="A1" type="button" runat="server" class="fa fa-th btn btn-default btn-sm"
                            onserverclick="BlockClick" data-toggle="tooltip" data-original-title="Cascade View" style="height:35px"></button>
                                <button id="A2" type="button" runat="server" class="fa fa-th-list btn btn-default btn-sm"
                            onserverclick="ListClick" data-toggle="tooltip" data-original-title="List View" style="height:35px"></button>                                  
                            <button id="btnuploadfile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited): USERID,FULLNAME, EMPID, USERROLE, ISSUPERUSER {Yes/No}, ISHRADMIN {Yes/No}, ISFINANCEADMIN {Yes/No}" style="height:35px"></button>
                                <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                                style="height: 35px"></button>                                
                                 <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                                style="height: 35px;"></button> 
                                <button id="btntemplate" type="button" data-toggle="tooltip" data-original-title="Download Template" runat="server" class="glyphicon glyphicon-floppy-disk btn btn-default btn-sm"
                                onserverclick="LinkButton1_Click" style="height:35px;"></button>
                                <button id="accesslink" type="button" data-toggle="tooltip" data-original-title="User Access" runat="server" class="fa fa-eye-slash btn btn-default btn-sm"
                                onserverclick="lnkAccess_Click" style="height:35px;"></button>
                                <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
                        </div>
                </div>


<div class="row">       
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="lists" runat="server">
                    <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
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
                        <asp:BoundField DataField="Rows" HeaderText="Row" SortExpression="rows" />
                        <asp:TemplateField HeaderText="User" ItemStyle-Font-Bold="true"
                            SortExpression="userid">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/updateuser?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("userid")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="Name"  HeaderText="Name" SortExpression="Name" />    
                         <asp:BoundField DataField="accesslevel"  HeaderText="Access" />                    
                        <asp:BoundField DataField="Role"  HeaderText="Role" SortExpression="Role" />
                        <asp:BoundField DataField="Status"  HeaderText="Status" SortExpression="Status"/>
                        <asp:BoundField DataField="Isemployee"  HeaderText="Employee" SortExpression="Isemployee"/>
                        <asp:BoundField DataField="isAdmin"  HeaderText="Super Admin" SortExpression="isAdmin"/>
                        <asp:BoundField DataField="isHR"  HeaderText="HR Admin" SortExpression="isHR"/>
                        <asp:BoundField DataField="isFinance"  HeaderText="Finance Admin" SortExpression="isFinance"/>
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
     
                </asp:View>
            <asp:View ID="blocks" runat="server">
                <div style="overflow:auto">
                    <asp:DataList ID="dlBlogs" runat="server" Width="99%" RepeatColumns="5" CellSpacing="0"
                        RepeatLayout="Flow" Font-Names="Arial" Font-Size="9px" GridLines="Both" DataKeyField="id"
                        BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" CssClass="row staff-grid-row"
                        BorderWidth="1px">
                        <ItemTemplate>
                            <%-- <table class="table" width="100%" >
                        <tr>
                            <td valign="top" style="width: 100%">--%>
                            <div class="col-md-4 col-sm-4 col-xs-6 col-lg-3">
                                <div class="profile-widget">
                                    <div class="profile-img rounded">
                                        <%--<a href="#" class="avatar">
                                            <%# Eval("initial")%></a>--%>
                                            <asp:ImageButton ID="imgavatar" runat="server" ToolTip="chats"  CssClass="avatar" 
                                                    onerror="this.onerror=null; this.src='/images/user-icon.png';" ImageUrl="~/images/male-avatar.png"
                                                   />
                                    </div>
                                    <h4 class="user-name m-t-10 m-b-0 text-ellipsis">
                                        <a href="#" style="color:#1BA691">
                                            <%# Eval("name")%></a></h4>
                                    <h5 class="user-name m-t-10 m-b-0 text-ellipsis">
                                        <a href="#">
                                            <%# Eval("role")%></a></h5>
                                    <div class="text-muted" style="font-size:12px" >
                                        Access Level:
                                        <%# Eval("accesslevel")%></div>
                                    <a href="#" class="btn btn-default btn-sm m-t-10">View Profile</a>
                                    <asp:Label ID="lblgender" runat="server" Height="0px" Text='<%# Eval("Gender")%>' Visible="false" />
                                </div>
                            </div>
                            <%-- </td>				
                        </tr>
                    </table>--%>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="row">
                    <ul class="pagination pagination-sm">
                        <li><a id="firstpage" runat="server" onserverclick="MoveFirst"  href="#"><<</a></li>
                        <li><a id="prevpage" runat="server" onserverclick="MovePrevious"  href="#"><</a></li>
                        <li><a id="pageno" runat="server" href="#">1</a></li>
                        <li><a id="pageof" runat="server" href="#"> of </a></li>
                        <li><a id="pagetotal" runat="server" href="#">1</a></li>
                        <li><a id="nextpage" runat="server" onserverclick="MoveNext" href="#">></a></li>
                        <li><a id="lastpage" runat="server" onserverclick="MoveLast" href="#">>></a></li>
                    </ul>
                </div>
            </asp:View>
        </asp:MultiView>
        </div>
       </div></div></div>
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>


