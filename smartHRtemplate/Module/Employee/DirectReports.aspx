<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="DirectReports.aspx.vb" Inherits="GOSHRM.DirectReports" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <html xmlns="http://www.w3.org/1999/xhtml">

    <title></title>
   
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

  

<body>
   
    <form id="form1" action="">
    <div class="container col-md-12">
    <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server">Danger!</strong>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                    <asp:Label ID="lblemp" runat="server" Font-Bold="True"  
            Font-Size="1px" ForeColor="#FF3300" Width="100%" Visible="False"></asp:Label>
            </div>
        </div>
    <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Head</b></h5>
                </div>
             <div class="panel-body">
 
    <div class="row">
        <div style="margin-right:20px;" class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">   
                 <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                 style="height: 35px"></button>                    
                    <input id="search" style="width:100%; margin-left:10px;" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>                   
           </div>
    </div>
    <div class="row">
        <div style="overflow: auto">
                <asp:DataList ID="dlBlogs" runat="server" Width="99%" RepeatColumns="5" CellSpacing="0"
                    RepeatLayout="Flow" Font-Names="Arial" Font-Size="9px" GridLines="Both" DataKeyField="empid"
                    BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" CssClass="row staff-grid-row"
                    BorderWidth="1px">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-4 col-xs-6 col-lg-3">
                            <div class="profile-widget">
                                <div class="profile-img">                                    
                                        <asp:ImageButton ID="imgavatar" runat="server" ToolTip='<%# Eval("name")%>'  CssClass="avatar" 
                                                    onerror="this.onerror=null; this.src='/images/blank-avatar.jpg';" ImageUrl="~/images/blank-avatar.jpg"
                                                   />
                                </div>
                                <h4 class="user-name m-t-10 m-b-0 text-ellipsis">
                                    <a href="#" style="color: #1BA691">
                                        <%# Eval("name")%></a><br /> <%# Eval("empid")%> </h4>
                                <h5 class="user-name m-t-10 m-b-0 text-ellipsis">
                                    <a href="#">
                                        <%# Eval("jobtitle")%></a></h5>
                                <h5 class="user-name m-t-10 m-b-0 text-ellipsis">
                                    <a href="#">
                                        <%# Eval("gradelevel")%></a></h5>
                                <div class="user-name m-t-10 m-b-0 text-muted">
                                    <%# Eval("office")%></div>
                                <a href="EmployeeProfile?empid=<%# Eval("empid")%>" class="btn btn-default btn-sm m-t-10">Profile</a>
                                <a href="employeejobhistory?empid=<%# Eval("empid")%>" class="btn btn-default btn-sm m-t-10">Career History</a>
                                <asp:Label ID="lblgender" runat="server" Height="0px" Text='<%# Eval("pid")%>' Visible="false" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="row">
                <ul class="pagination pagination-sm">
                    <li><a id="firstpage" runat="server" onserverclick="MoveFirst" href="#"><<</a></li>
                    <li><a id="prevpage" runat="server" onserverclick="MovePrevious" href="#"><</a></li>
                    <li><a id="pageno" runat="server" href="#">1</a></li>
                    <li><a id="pageof" runat="server" href="#">of </a></li>
                    <li><a id="pagetotal" runat="server" href="#">1</a></li>
                    <li><a id="nextpage" runat="server" onserverclick="MoveNext" href="#">></a></li>
                    <li><a id="lastpage" runat="server" onserverclick="MoveLast" href="#">>></a></li>
                </ul>
            </div>
    </div>
    </div></div>
    </div>
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
        </style>
</asp:Content>


