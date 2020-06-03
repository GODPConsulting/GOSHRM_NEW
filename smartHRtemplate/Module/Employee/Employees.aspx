<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Employees.aspx.vb" Inherits="GOSHRM.Employees" EnableEventValidation="false" Debug="true"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

 
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
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

      <script type = "text/javascript">
          function BulkUpload() {
              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              if (confirm("Perform bulk picture upload?")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }
    </script>
    
    
    <style = type="text/css">
         .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
         .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style> 

<body>

    <form id="form1" action="" >
    <div class="">
    <div class ="content container-fluid">
        <div id="divalert" runat="server" visible="false" class="row alert alert-info">
            <strong id="msgalert" runat="server">Danger!</strong>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        </div>
        <div id="holder" runat="server" class="panel panel-success">
                <div class="panel-heading">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                Head</h5>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                 <div class="col-sm-3 col-md-1 col-xs-6">
                        <button id="btBack" type="button" runat="server" class="danger" onserverclick="btnBack_Click"
                            style=" width: 100px">
                            << Back</button>
                    </div>
             <div class="panel-body">
    
    <div class="row filter-row"> 
        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-6 pull-right">
            <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
            <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
        </div>  
         <div id="dvcompany" runat="server" class="col-sm-3 col-md-3 col-xs-6 pull-right">
            <telerik:RadComboBox runat="server" Skin="Bootstrap"
                RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic" ID="cboCompany"
                AutoPostBack="True" Filter="Contains" Font-Names="Verdana"  ForeColor="#666666">
            </telerik:RadComboBox>
        </div>                      
    </div>

    <div style="" class="row">
     <div class="col-sm-3 col-md-3 col-xs-6">
            <telerik:RadComboBox runat="server" Skin="Bootstrap"
                RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboUploadType"
                AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666">
            </telerik:RadComboBox>
        </div>
        <div class="col-sm-3 col-md-3 col-xs-6">
            <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
        </div>
        <div class="col-sm-6 col-md-6 col-xs-12 pull-right">        
            <div class="form-group">
                    <button id="A1" type="button" runat="server" class="fa fa-th btn btn-default btn-sm"
                onserverclick="BlockClick" data-toggle="tooltip" data-original-title="Cascade View" style="height:35px"></button>
                    <button id="A2" type="button" runat="server" class="fa fa-th-list btn btn-default btn-sm"
                onserverclick="ListClick" data-toggle="tooltip" data-original-title="List View" style="height:35px"></button>
                <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                onserverclick="btnUpload_Click" style="height:35px"></button>
                <button id="btPicUpdate" data-toggle="tooltip" data-original-title="Bulk Photo Upload(Save employee photos with empid in c:\Photo)" type="button" runat="server" class="glyphicon glyphicon-open-file btn btn-default btn-sm"
                onserverclick="btnPicUpload_Click" style="height:35px"></button>
                <button id="btExport" data-toggle="tooltip" data-original-title="Download Employee List" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                style="height: 35px"></button>
                <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                </asp:LinkButton>
                 <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Employee" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                style="height: 35px;"></button>
                <button id="btntemplate" type="button" data-toggle="tooltip" data-original-title="Download Template" runat="server" class="glyphicon glyphicon-floppy-disk btn btn-default btn-sm"
                onserverclick="DownloadTemplate" style="height:35px;"></button>
                                    
                </div>
        </div>            
    </div>
    <div style="overflow: auto" class="row col-md-12">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
     <div class="table-responsive">
        <asp:View ID="lists" runat="server">                      
            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
               <ContentTemplate>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="Employee No"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed col-md-12">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                        <asp:BoundField DataField="Rows"  HeaderText="Row" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Emp ID"  ItemStyle-Font-Bold="true"
                            SortExpression="employee no"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/EmployeeData?id={0}",
                     HttpUtility.UrlEncode(Eval("Employee No").ToString())) %>' Text='<%# Eval("Employee No")%>' /></ItemTemplate></asp:TemplateField>
                        <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name" />
                        <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name" />
                        <asp:BoundField DataField="GradeLevel" HeaderText="Job Grade" SortExpression="GradeLevel" />
                        <asp:BoundField DataField="jobtitle" HeaderText="Job Title" SortExpression="jobtitle" />
                        <asp:BoundField DataField="office" HeaderText="Office" SortExpression="office" />
                       <%-- <asp:BoundField DataField="JobHistory" HeaderText="Has Job"   SortExpression="JobHistory" />--%>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                   </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
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
        </asp:View>
        </div>

        <asp:View ID="blocks" runat="server">
             <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                <ContentTemplate>           
                <asp:DataList ID="dlBlogs" runat="server" Width="100%" RepeatColumns="5" CellSpacing="0"
                    RepeatLayout="Flow" Font-Names="Arial" Font-Size="9px" GridLines="Both" DataKeyField="empid"
                    BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid" CssClass="staff-grid-row"
                    BorderWidth="0px">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-4 col-xs-12 col-lg-3">
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
                                <div class="small text-muted">
                                    <%# Eval("office")%></div>
                                <a href="EmployeeData?id=<%# Eval("empid")%>" class="btn btn-default btn-sm m-t-10">View Profile</a>
                                <asp:Label ID="lblgender" runat="server" Height="0px" Text='<%# Eval("imgtype")%>' Visible="false" />
                                <%--<asp:Label ID="lblgender" runat="server" Height="0px" Text='<%# Eval("pid")%>' Visible="false" />--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            
            <div class="row col-md-12">
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
                  </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
            </div>
    </div>
    </div>
    </div>
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


