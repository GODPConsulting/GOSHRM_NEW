<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="EmployeeJobHistory.aspx.vb" Inherits="GOSHRM.EmployeeJobHistory" EnableEventValidation="false" Debug="true"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head >
        <title></title>
    </head>

<body>
   
    <form id="form1" action="">
    <div >
                <div class="content">


    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblemp" runat="server" Font-Bold="True" Font-Size="1px" Visible="False"></asp:Label>
        </div>
    </div>
                <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b>Work History</b></h5>
                </div>
             <div class="panel-body">
    <div class="row">
                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%--<h4 class="card-title" style="color: #1BA691;">
                <b>Work History</b></h4>--%>
            <div class="experience-box">
                <ul class="experience-list">
                    <li>
                        <asp:DataList ID="dlworkhistory" runat="server" Width="100%" RepeatColumns="1"
                            CellSpacing="0" RepeatLayout="Flow" Font-Names="Arial" Font-Size="14px" DataKeyField="id"
                            BorderColor="Transparent" ForeColor="#666666" BorderWidth="1px">
                            <ItemTemplate>
                                <div class="experience-user">
                                    <div class="before-circle">
                                    </div>
                                </div>
                                <div class="experience-content">
                                    <div class="timeline-content">
                                        <a class="name" style="color: #1BA691" >
                                            <%# Eval("Job Title")%></a>
                                        <div>
                                            <%# Eval("Grade Level")%></div>
                                        <div>
                                            <%# Eval("Office")%></div>
                                        <div>
                                            <%# Eval("Supervisor")%> (Line Manager)</div>
                                        <span class="time">
                                            <%# Eval("Start Date")%>
                                            -
                                            <%# Eval("End Date")%></span>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    </div>
    </div>
    </div>
    </div>
    <%-- <div>
         <table width="100%">
         <tr>
                 <td class="style22">
                     <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" 
                            Width="100%" style="font-weight: 700; color: #FF6600"></asp:Label>
                     </td>
             </tr>
        </table>
        <table class="style21">
             <tr>
                 <td class="style22">
                     <asp:TextBox ID="txtsearch" runat="server" Width="251px" Height = "20px" 
                         BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Search"></asp:TextBox>
                         <asp:Button ID="Button2" runat="server" Text="Load" BackColor="White" 
                         ForeColor="White" Width="5px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                          <asp:Button ID="btnFind" runat="server" Text="Load" BackColor="#1BA691" 
                         ForeColor="White" Width="80px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                            <asp:Button ID="Button3" runat="server" Text="Load" BackColor="White" 
                         ForeColor="White" Width="5px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                               <asp:Button ID="Button1" runat="server" Text="Export" 
                         BackColor="#3399FF" ForeColor="White"
                                    Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                   Font-Size="11px" />
                 </td>
                 <td>
                    
                 </td>
                 <td>
                               &nbsp;</td>
                 <td>
       
        
       
                 </td>
             </tr>
         </table>
     </div>
   
     
    <div >
    
        <div>
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
           
                                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid" 
                                        DataKeyNames="id" Font-Names="Verdana" 
                Font-Size="11px" Height="50px" PageSize="100" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                        ToolTip="click row to select record" Width="100%">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>                                            
                                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                            <ItemStyle Width="1%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Grade Level" HeaderText="Job Grade">
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Job Title" HeaderText="Job Title">
                                            <ItemStyle Width="12%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Employment Type" HeaderText="Employment">
                                            <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Supervisor" HeaderText="Line Manager">
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Indirect Supervisor" HeaderText="Reviewer I">
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Office" HeaderText="Office">
                                            <ItemStyle Width="18%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Location" HeaderText="Location">
                                            <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Country" HeaderText="Country">
                                            <ItemStyle Width="9%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Start Date" HeaderText="Start Date">
                                            <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="End Date" HeaderText="End Date">
                                            <ItemStyle Width="5%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="Center" />
                                    </asp:GridView>
           
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
        </style>
</asp:Content>


