<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="TrainingRequests.aspx.vb" Inherits="GOSHRM.TrainingRequests" EnableEventValidation="false" Debug="true"%>
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
    </style>

<body style="">
   


    <form id="form1" >
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
        
         <div class="row">
         <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right">
             <button id="btnApprove" type="button" data-toggle="tooltip" data-original-title="Approve" runat="server" class="glyphicon glyphicon-ok btn btn-default btn-sm" onserverclick="btnApprove_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="Button1_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
            <div class="col-sm-3 col-md-2 col-xs-6">
               <asp:Button ID="btnBack" runat="server" Text="&lt;&lt; Back" CssClass="btn btn-primary btn-danger"
                         ForeColor="White" Width="100%" Height = "35px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
            </div>
        </div>

    <div>
    
        <div class="col-md-12">
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Verdana" AllowPaging="True" PageSize="1000" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" CssClass="table table-condensed" 
                 Width="100%" Height="50px" ToolTip="click row to select record"  
                Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" 
                ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                <AlternatingRowStyle BackColor="white" />
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

                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" />
                        <asp:TemplateField HeaderText="Employee" ItemStyle-Width="100px" 
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="trainingrequestupdate.aspx?id=<%# Eval("id") %>">
                                    <%# Eval("Employee")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateRequest" ItemStyle-Width="50px" HeaderText="Request Date" ItemStyle-HorizontalAlign="Center"/> 
                        <asp:BoundField DataField="ForwardedTo" ItemStyle-Width="100px" HeaderText="Approval Forwarded To" />                            
                        <asp:BoundField DataField="ForwardApproval" ItemStyle-Width="50px" HeaderText="Line Manager Approval" />  
          
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
                    window.open("trainingrequestupdate.aspx?id=" + code);
                }
            </script>         
   
           
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
    </div>
     </div>
      </div>
     </div>
    </form>
</body>
</html>
</asp:Content>



