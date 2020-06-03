<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="Interviews.aspx.vb" Inherits="GOSHRM.Interviews" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
   


    <form id="form1" >
    <div class="container col-md-12">
     <div class="row">
            <div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
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
                    <button onserverclick="btnFind_Click" id="btFind" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                  <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
                    <telerik:RadComboBox ID="radYear" runat="server" RenderMode="Lightweight"
                                                    ResolvedRenderMode="Classic" Width="100%" 
                        Skin="Bootstrap" AutoPostBack="True">
                                                </telerik:RadComboBox>
                </div>
    </div>

     <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="ID"
                    OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                    AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                    BorderColor="#CCCCCC" CssClass="table table-condensed">
                    <RowStyle BackColor="White" />
                    <Columns>                        
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                        <asp:TemplateField HeaderText="Name"  ItemStyle-Font-Bold="true" SortExpression="Candidatename">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/InterviewDetail?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Candidatename")%>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression= "title" /> 
                     <asp:BoundField DataField="interviewdate" HeaderText="Interview Date" SortExpression="interviewdate" DataFormatString="{0:dd, MMM yyyy}" /> 
                      <asp:BoundField DataField="interviewtime" HeaderText="Interview Time" SortExpression="interviewtime"/>                                      
                        <asp:BoundField DataField="interviewer_recommendation" HeaderText="My Recommendation" />
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
     </div>
        </div></div>
    
    
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

</asp:Content>


