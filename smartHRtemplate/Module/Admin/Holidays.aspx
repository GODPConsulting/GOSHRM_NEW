<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Holidays.aspx.vb" Inherits="GOSHRM.Holidays" EnableEventValidation="false" Debug="true"%>
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
    .style28
    {
        width: 275px;
        font-size: x-large;
        font-family: Candara;
    }
    </style>

<body>
   


    <form id="form1" action="">
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
        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Name, Start Period, End Period" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDeleteData" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
                </div>     
        
    </div>


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
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" ItemStyle-Font-Bold="true"
                            SortExpression="name">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/HolidaysUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("name")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="year" ItemStyle-Width="50px" HeaderText="Year" SortExpression="year" /> 
                           <asp:BoundField DataField="Date" ItemStyle-Width="50px" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd, MMM yyyy}" />                       
                           <asp:BoundField DataField="Status" ItemStyle-Width="50px" HeaderText="Status" SortExpression ="status" />  
                         <asp:BoundField DataField="Country" ItemStyle-Width="100px" HeaderText="Country" SortExpression ="country"/>  
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
        </div></div>
        </div> </div>


     <%--<div>
         <table width="100%">
            <tr>
                 <td>
                     <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                     </td>
             </tr>
        </table>
          <table class="style21">    
             <tr>
                 <td class="style22">
                     <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height = "20px" 
                         BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Search"></asp:TextBox>
                 </td>
                 <td>
                     <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td>
                 </td>
                 <td class="style25">
                     <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td class="style26">
                     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick = "Delete" 
                         OnClientClick = "Confirm()" BackColor="#FF3300" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
             </tr>
         </table>
     </div>
     
 
    
    <div style="height: 163px">
    
        <div>
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Arial" AllowPaging="True" PageSize="100" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" 
                 Width="60%" Height="50px" ToolTip="click row to select record"  
                Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                     <asp:TemplateField ItemStyle-Width="5px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows" />
                        <asp:TemplateField HeaderText="Day" ItemStyle-Width="50px" 
                            ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                    <%# Eval("Name")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Date" ItemStyle-Width="50px" HeaderText="Date" ItemStyle-HorizontalAlign="Center"/>
                         <asp:BoundField DataField="Period" ItemStyle-Width="50px" HeaderText="Period" /> 
                         <asp:BoundField DataField="Status" ItemStyle-Width="50px" HeaderText="Status" />  
                         <asp:BoundField DataField="Country" ItemStyle-Width="100px" HeaderText="Country"/>              
 
                    

                   
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                
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
                    window.open("HolidaysUpdate.aspx?id=" + code, "open_window", "width=500,height=400");
                }
            </script>

   
           
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
    </div>--%>
    
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


