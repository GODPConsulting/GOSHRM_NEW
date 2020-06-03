<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Education.aspx.vb" Inherits="GOSHRM.Education" EnableEventValidation="false" Debug="true"%>
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

    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp2(Checkbox) {
            var gridDiscipline = document.getElementById("<%=gridDiscipline.ClientID %>");
            for (i = 1; i < gridDiscipline.rows.length; i++) {
                gridDiscipline.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>

     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllEmp3(Checkbox) {
             var gridAcademicGrade = document.getElementById("<%=gridAcademicGrade.ClientID %>");
             for (i = 1; i < gridAcademicGrade.rows.length; i++) {
                 gridAcademicGrade.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
    </script>

     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllEmp4(Checkbox) {
             var gridOLSubject = document.getElementById("<%=gridOLSubject.ClientID %>");
             for (i = 1; i < gridOLSubject.rows.length; i++) {
                 gridOLSubject.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
    </script>

     <script type="text/javascript" language="javascript">
         //    Grid View Check box
         function CheckAllEmp5(Checkbox) {
             var gridOLGrade = document.getElementById("<%=gridOLGrade.ClientID %>");
             for (i = 1; i < gridOLGrade.rows.length; i++) {
                 gridOLGrade.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
             }
         }
    </script>

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
    .style28
    {
        width: 275px;
        font-size: x-large;
        font-family: Verdana;
    }
    </style>

<body>
    <form id="form1" action="" >
    <div class="container col-md-12">
    <div class="row">
       <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
    </div>
    <div class="content">
        <%--<h2>Educational Qualifications</h2>--%>
        <div style="margin-bottom:20px" class="row">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <button id="btnAQualification" type="button" runat="server" class="btn btn-default "
                onserverclick="btnAcademicQualification_Click" style="font-size:15px;width: 100%;">Academic Qualification</button>
        </div>
           <div class="col-md-3 col-sm-6 col-xs-12"> 
            <button id="btnADiscipline" type="button" runat="server" class="btn btn-default"
                onserverclick="btnAcademicDiscipline_Click" style="font-size:15px;width: 100%;">Academic Discipline</button></div>
                <div class="col-md-3 col-sm-6 col-xs-12">
            <button id="btnAGrade" type="button" runat="server" class="btn btn-default"
                onserverclick="btnAcademicGrade_Click" style="font-size:15px;width: 100%;">Academic Grade</button></div>
            <div class="col-md-3 col-sm-6 col-xs-12">
            <button id="btnLevelSubject" type="button" runat="server" class="btn btn-default"
                onserverclick="btnOLevelSubject_Click" style="font-size:15px;width: 100%;">Secondary School Subject</button></div>
            <div class="col-md-3 col-sm-6 col-xs-12">
            <button id="btnLevelGrade" type="button" runat="server" class="btn btn-default"
                onserverclick="btnOLevelGrade_Click" style="font-size:15px;width: 100%;">Secondary School Grade</button></div>
        </div>
        <div class="row" style="height:5px"></div>
          <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="qualifications" runat="server">
                <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitlemenu1" runat="server">Qualification</b></h5>
                        </div>
                     <div class="panel-body">
                <div class="row">
                 <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="searchmenu1" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFindMenu1_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadMenu1" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUploadMenu1_Click" data-toggle="tooltip" data-original-title="CSV File: Name, Description, Rank" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btnExportMenu1" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExportMenu1_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDeletemenu1" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete_Education" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAddMenu1" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddMenu1_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="filemenu1" runat="server" />
                </div>
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id"
                            OnRowDataBound="OnRowDataBound" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"><HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" /></HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemTemplate><asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows"
                                    ItemStyle-VerticalAlign="Top" />
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px" SortExpression="name"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/EducationUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' /></ItemTemplate></asp:TemplateField> 
                                <asp:BoundField DataField="Description" ItemStyle-Width="200px" HeaderText="Description"
                                    ItemStyle-VerticalAlign="Top" SortExpression="Description" />
                                <asp:BoundField DataField="Qualification Order" ItemStyle-Width="15px" HeaderText="Ranking"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" SortExpression="Qualification Order" />                               
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
                </div></div></div>
           
                </asp:View>
                <asp:View ID="disciplines" runat="server">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitlemenu2" runat="server"></b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                         <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                        <input id="searchmenu2" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                        <button onserverclick="btnsearchdiscipline_Click" id="btnFindMenu2" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                    </div>
                     <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                        <button id="btnUploadMenu2" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                            onserverclick="btnUploadDiscipline_Click" data-toggle="tooltip" data-original-title="CSV File: Name" style="margin-right:10px;margin-left:10px;height:35px"></button>
                            <button id="btnExportMenu2" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExportDiscipline_Click"
                        style="height: 35px"></button>
                            <asp:LinkButton ID="btnDeleteMenu2" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelDiscipline_Click" OnClientClick="Confirm()">
                                <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                             <button id="btnAddMenu2" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddDiscipline_Click"
                            style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                        <input style="height:35px;" class="form-control" type="file" id="filemenu2" runat="server" />
                    </div>
                    </div>
                <div class="row">
                    <div class="table-responsive">
                        <asp:GridView ID="gridDiscipline" runat="server" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id"
                             Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"><HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp2(this);" /></HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/><ItemTemplate><asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows"
                                    ItemStyle-VerticalAlign="Top" />
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="200px" SortExpression="name"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/DisciplineUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' /></ItemTemplate></asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                        </asp:GridView>
                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                        <script type="text/javascript">
                            $(function () {
                                $("[id*=gridDiscipline] td").hover(function () {
                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                }, function () {
                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                })
                            })
                        </script>
                    </div>
                </div></div>
                </div>          
                </asp:View>
                 <asp:View ID="grade" runat="server">
                     <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitlemenu3" runat="server"></b></h5>
                        </div>
                     <div class="panel-body">
                     <div class="row">
                     <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                            <input id="searchmenu3" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="btnSearchAcademicGrade_Click" id="btnFindMenu3" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                         <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                            <button id="btnUploadMenu3" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUploadAcademicGrade_Click" data-toggle="tooltip" data-original-title="CSV File: location,address,country,state/province,city, contactno" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                <button id="btnExportMenu3" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExpAcademicGrade_Click"
                            style="margin-right:10px;height: 35px"></button>
                                <asp:LinkButton ID="btnDeleteMenu3" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelAcademicGrade_Click" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                 <button id="btnAddMenu3" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddAcademicGrade_Click"
                                style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height:35px;" class="form-control" type="file" id="filemenu3" runat="server" />
                        </div>
                     </div>
                     <div class="row">
                         <div class="table-responsive">
                             <asp:GridView ID="gridAcademicGrade" runat="server" AllowSorting="True" BorderStyle="Solid"
                                 Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id" Height="50px"
                                 ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                 EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                 ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                 <RowStyle BackColor="White" />
                                 <Columns>
                                     <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"><HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp3(this);" /></HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemTemplate><asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false" ItemStyle-VerticalAlign="Top"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                     <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows"
                                         ItemStyle-VerticalAlign="Top" />
                                     <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" SortExpression="name"
                                         ItemStyle-VerticalAlign="Top" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/AcademicGradeUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' /></ItemTemplate></asp:TemplateField>
                                     <asp:BoundField DataField="desc" ItemStyle-Width="200px" HeaderText="Description" ItemStyle-VerticalAlign="Top" SortExpression="desc"/>  
                                    <asp:BoundField DataField="level" ItemStyle-Width="20px" HeaderText="Ranking" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" SortExpression="level" />             
  
                                 </Columns>
                                 <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                             </asp:GridView>
                             <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                             <script type="text/javascript">
                                 $(function () {
                                     $("[id*=gridAcademicGrade] td").hover(function () {
                                         $("td", $(this).closest("tr")).addClass("hover_row");
                                     }, function () {
                                         $("td", $(this).closest("tr")).removeClass("hover_row");
                                     })
                                 })
                             </script>
                         </div>
                     </div> </div>
                     </div>                        
                </asp:View>
                 <asp:View ID="OLSubject" runat="server">
                      <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5><b id="pagetitlemenu4" runat="server">Grade</b></h5>
                            </div>
                         <div class="panel-body">
                     <div class="row">
                             <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                            <input id="searchmenu4" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="btnSearchOLSubject_Click" id="btnFindMenu4" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                         <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                            <button id="btnUploadMenu4" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUploadOLSubject_Click" data-toggle="tooltip" data-original-title="" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                <button id="btnExportMenu4" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExpOLSubject_Click"
                            style="height: 35px"></button>
                                <asp:LinkButton ID="btnDeleteMenu4" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelOLSubject_Click" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                 <button id="btnAddMenu4" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddOLSubject_Click"
                                style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height:35px;" class="form-control" type="file" id="filemenu4" runat="server" />
                        </div>  
                     </div>

                     <div class="row">
                         <div class="table-responsive">
                             <asp:GridView ID="gridOLSubject" runat="server" AllowSorting="True" BorderStyle="Solid"
                                 Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id" Height="50px"
                                 ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                 EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                 ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                 <RowStyle BackColor="White" />
                                 <Columns>
                                     <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"><HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp4(this);" /></HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemTemplate><asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false" ItemStyle-VerticalAlign="Top"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                     <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows"
                                         ItemStyle-VerticalAlign="Top" />
                                     <asp:TemplateField HeaderText="Name" ItemStyle-Width="200px" SortExpression="name"
                                         ItemStyle-VerticalAlign="Top" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/OLSubjectUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' /></ItemTemplate></asp:TemplateField>
                                    
                                 </Columns>
                                 <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                             </asp:GridView>
                             <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                             <script type="text/javascript">
                                 $(function () {
                                     $("[id*=gridOLSubject] td").hover(function () {
                                         $("td", $(this).closest("tr")).addClass("hover_row");
                                     }, function () {
                                         $("td", $(this).closest("tr")).removeClass("hover_row");
                                     })
                                 })
                             </script>
                         </div>
                     </div></div>
                     </div>       
                </asp:View>
                <asp:View ID="OLGrade" runat="server">
                     <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5><b id="pagetitlemenu5" runat="server"></b></h5>
                            </div>
                         <div class="panel-body">
                     <div class="row">
                          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                            <input id="searchmenu5" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                            <button onserverclick="btnsearchOLGrade_Click" id="btnSearchMenu5" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                        </div>
                         <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                            <button id="btnUploadMenu5" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUploadOLgrade_Click" data-toggle="tooltip" data-original-title="CSV File: name" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                <button id="btnExportMenu5" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExpOLGrade_Click"
                            style="margin-right:10px;height: 35px"></button>
                                <asp:LinkButton ID="btnDeleteMenu5" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelOLGrade_Click" OnClientClick="Confirm()">
                                    <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                                 <button id="btnAddMenu5" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddOLGrade_Click"
                                style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height:35px;" class="form-control" type="file" id="filemenu5" runat="server" />
                        </div> 
                     </div>

                     <div class="row">
                         <div class="table-responsive">
                             <asp:GridView ID="gridOLGrade" runat="server" AllowSorting="True" BorderStyle="Solid"
                                 Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id" Height="50px"
                                 ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                 EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                 ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                 <RowStyle BackColor="White" />
                                 <Columns>
                                     <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"><HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp5(this);" /></HeaderTemplate><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemTemplate><asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false" ItemStyle-VerticalAlign="Top"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                                     <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows"
                                         ItemStyle-VerticalAlign="Top" />
                                     <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" SortExpression="name"
                                         ItemStyle-VerticalAlign="Top" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/OLGradeUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' /></ItemTemplate></asp:TemplateField>
                                     <asp:BoundField DataField="desc" ItemStyle-Width="200px" HeaderText="Description" SortExpression="desc" ItemStyle-VerticalAlign="Top" />  
                                    <asp:BoundField DataField="level" ItemStyle-Width="20px" HeaderText="Ranking" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" DataFormatString="{0:d}" SortExpression="level" />             
  
                                 </Columns>
                                 <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                             </asp:GridView>
                             <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                             <script type="text/javascript">
                                 $(function () {
                                     $("[id*=gridOLGrade] td").hover(function () {
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

                </asp:View>
            </asp:MultiView>
       </div>
    </div>  
    </form>
     <script src="../../Styles/js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="../../Styles/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../Styles/js/jquery.min.js" type="text/javascript"></script>
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


