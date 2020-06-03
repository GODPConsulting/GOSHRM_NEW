<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="WorkWeekGrade.aspx.vb" Inherits="GOSHRM.WorkWeekGrade" EnableEventValidation="false" Debug="true"%>
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
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <strong id="msgalert" runat="server">Danger!</strong>
        </div>
    
    <div class="row">
        <div class="col-xs-8">
            <h4 id="pagetitle" runat="server" class="page-title">
                Head</h4>
        </div>
        <div class="col-xs-4 text-right m-b-30">
            <button id="accesslink" type="button" runat="server" class="btn-primary rounded"
                onserverclick="btnBack_Click" style="height: 30px; width: 150px">
                << Back</button>
        </div>
    </div>

    <div class="row">
            <div class="col-sm-3 col-md-1 col-xs-6 pull-right">
                <button id="btnuploadfile" type="button" runat="server" class=" btn-info btn-block"
                    onserverclick="btnUpload_Click" title="CSV File: Grade, Day, Status, Country"
                    style="height: 30px; width: 100px">
                    Upload</button>
            </div>
            <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                <input class="form-control" type="file" id="file1" runat="server" />
            </div>
            <div class="col-sm-3 col-md-1 col-xs-6 pull-right">
                <button id="btExport" type="button" runat="server" class="btn-warning" onserverclick="btnExport_Click"
                    style="height: 30px; width: 100px">
                    Export</button>
            </div>
            <div class="col-sm-3 col-md-1 col-xs-6 pull-right">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="Delete" OnClientClick="Confirm()"
                    BackColor="#FF3300" ForeColor="White" Width="100px" Height="30px" CssClass="btn-danger"
                    BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
            </div>
            <div class="col-sm-3 col-md-1 col-xs-6 pull-right">
                 <button id="btAdd" type="button" runat="server" class="btn-success" onserverclick="btnAdd_Click"
                    style="height: 30px; width: 100px">
                    Add
                </button>
            </div>
            <div class="col-sm-3 col-md-3 col-xs-6 pull-right">              
                <div class="form-group form-focus">                    
                    <input id="search" runat="server" type="text" class="form-control floating" style="height: 30px"
                        placeholder="Search..." />
                    <button id="btnsearch" type="button" runat="server" class="glyphicon glyphicon-search"
                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                </button>
                </div>
            </div>
        </div>


  

    <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="200" DataKeyNames="id"
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
                        <asp:BoundField DataField="grade"  HeaderText="Job Grade"
                            SortExpression="grade" />  
                        <asp:TemplateField HeaderText="Day"  ItemStyle-Font-Bold="true"
                            SortExpression="day">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/WorkWeekGradeUpdate?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("day")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Status"  HeaderText="Status"
                            SortExpression="Status" />  
                          <asp:BoundField DataField="Country"  HeaderText="Country"
                            SortExpression="Country" />                      
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

     <%--<div>
         <table width="100%">
            <tr>
                 <td class="style22" >
                     <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                     </td>
             </tr>
         </table>
      <table class="style21">
             <tr>
                 <td class="style22" colspan="2">
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
                 <td>
                     <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#1BA691" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td>
                     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick = "Delete" 
                         OnClientClick = "Confirm()" BackColor="#FF3300" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td>
                 </td>
                 <td>
                     <asp:Button ID="btnExport" runat="server" Text="Export"  
                         BackColor="#1BA691" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td>
                 </td>
                   <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" 
                                    Font-Size="11px" />
                 </td>
                   <td>
                                <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" 
                                    BorderStyle="None" ForeColor="White" Height="20px" Text="Upload File" 
                                    ToolTip="CSV File: Grade, Day, Status, Country" 
                                    Width="100px" Font-Names="Verdana" Font-Size="11px" />
                 </td>
                 <td>
                 </td>
                <td>
                 </td>
                 <td>
                                <asp:Button ID="btnBack" runat="server" BackColor="#1BA691" 
                                    BorderStyle="None" ForeColor="White" Height="20px" Text="&lt;&lt; Back" 
                                    ToolTip="return to general week day setup" 
                                    Width="100px" Font-Names="Verdana" Font-Size="11px" />
                 </td>
             </tr>
         </table>
     </div>--%>
  
    <%--<div>
    
        <div>
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" 
                 Width="70%" Height="50px" ToolTip="click row to select record"  
                Font-Size="12px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" >
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                         <asp:TemplateField ItemStyle-Width="3px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="3px" HeaderText="Rows" />
                        <asp:BoundField DataField="Grade" ItemStyle-Width="100px" HeaderText="Job Grade" SortExpression="Grade"/>
                        <asp:TemplateField HeaderText="Day" ItemStyle-Width="50px" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" SortExpression="day">
                            <ItemTemplate>
                                <a href="#" onclick='openWindow("<%# Eval("id") %>");'>
                                    <%# Eval("Day")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Status" ItemStyle-Width="50px" HeaderText="Status" SortExpression="status"/>
                        <asp:BoundField DataField="Country" ItemStyle-Width="80px" HeaderText="Country" SortExpression="country"/>           
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
                    window.open("WorkWeekGradeUpdate.aspx?id=" + code, "open_window", "width=600,height=400");
                }
            </script>

   
           
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    
    </div>--%>
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
        .style23
        {
            width: 275px;
        }
    </style>
</asp:Content>


