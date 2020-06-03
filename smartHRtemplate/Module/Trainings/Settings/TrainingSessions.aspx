<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TrainingSessions.aspx.vb"
    Inherits="GOSHRM.TrainingSessions" EnableEventValidation="false" Debug="true" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head >
        <title></title>
         <link rel="stylesheet" href="../../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
        <link rel="stylesheet" href="../../../AdminLTE/dist/css/Admin-lte.min.css"/>
        <link rel="stylesheet" href="../../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
        <link rel="stylesheet" href="../../../Skins/_all-skins.min.css"/>
        <link rel="stylesheet" href="../../../css/font-awesome.min.css"/>
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
        .style29
        {
        }
    </style>
    <body>
        <form id="form1">

        <div class="container col-md-12">
           <div class="row">
                 <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Company Name</b></h5>
                </div>
             <div class="panel-body">
        
         <div class="row">
         <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>     
             <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                <telerik:RadComboBox runat="server" Skin="Bootstrap"
                    RenderMode="Lightweight" ResolvedRenderMode="Classic" Width="100%" ID="cboCompany"
                    AutoPostBack="True" Font-Names="Verdana" ForeColor="#666666">
                </telerik:RadComboBox>
            </div>  
           <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                <telerik:RadComboBox ID="cboyear" runat="server" Width="100%" ForeColor="#666666"
                    AutoPostBack="True" Filter="Contains" Skin="Bootstrap">
                </telerik:RadComboBox>
            </div>      
        </div>       
            
         <div class="row">
          <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                    <ContentTemplate>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="500" DataKeyNames="id" 
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" CssClass="table table-condensed" EmptyDataText="No data to display">
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" HeaderText="Rows" />
                        <asp:TemplateField HeaderText="Name"
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%--<a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                    <%# Eval("Name")%></a>--%>
                                    <a href="TrainingSessionsUpdate?id=<%# Eval("id") %>"> <%# Eval("Name")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Course"  HeaderText="Courses/Activities" SortExpression="course" />   
                         <asp:BoundField DataField="Scheduled Time" HeaderText="Scheduled Date" SortExpression="Scheduled Time"/>                                                                        
                         <asp:BoundField DataField="availabletrainness" HeaderText="Slots" SortExpression="availabletrainness" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:d}" />                         
                          <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#String.Format("~/Module/Trainings/settings/employeetrainingsession?sessionid={0}",
                                 HttpUtility.UrlEncode(Eval("id").ToString())) %>' 
                                            Text= '<%# Eval("Trainees") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#String.Format("~/Module/Trainings/settings/trainingrequests?sessionid={0}",
                                 HttpUtility.UrlEncode(Eval("id").ToString())) %>' 
                                            Text= '<%# Eval("PendingRequest") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Materials" ItemStyle-Font-Bold="true" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#String.Format("~/Module/Trainings/settings/TrainingMaterials?sessionid={0}",
                                 HttpUtility.UrlEncode(Eval("id").ToString())) %>' 
                                            Text= '<%# Eval("Materials") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="assessment" HeaderText="Learning Assessment" /> 
                       <asp:BoundField DataField="appassessstat" HeaderText="Application Assessment" /> 
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
                 </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboyear" EventName="SelectedIndexChanged" />
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
                <script type="text/javascript">
                    function openWindow(code) {
                        window.open("TrainingSessionsUpdate.aspx?id=" + code, "open_window", "width=900,height=900");
                    }
                </script>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
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
            width: 100%;
        }
        .style22
        {
        }
        </style>
</asp:Content>
