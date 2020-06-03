<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Vacancies.aspx.vb" Inherits="GOSHRM.Vacancies"
    EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/Recruit.Master" AutoEventWireup ="true" CodeBehind="MyApplications.aspx.vb" Inherits="GOSHRM.MyApplications" EnableEventValidation="false" Debug="true"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


 <html xmlns="http://www.w3.org/1999/xhtml">
<title></title>

<body>

        <form id="form1" action ="">
         <div class="container col-md-12">
           <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
         </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Job Applications</b></h5>
                </div>
             <div class="panel-body">       
         <div class="row">
          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>    
        </div>
        <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" 
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" 
                    Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>                        
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                        <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="code"/>
                        <asp:BoundField DataField="Title" HeaderText="Job Title" SortExpression="title"/>
                        <asp:BoundField DataField="specialization" HeaderText="Area of Specialisation" SortExpression="specialization" />
                        <asp:BoundField DataField="closingdate" HeaderText="Closing Date" SortExpression ="closingdate" DataFormatString="{0:dd, MMM yyyy}"/>
                        <asp:BoundField DataField="appliedon" HeaderText="Application Date" SortExpression="appliedon" DataFormatString="{0:dd, MMM yyyy}"/>
                        <asp:BoundField DataField="appstat" HeaderText="Application Status" />
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
            </div></div></div></div>




        </form>
 
</body>
</html>
</asp:Content>