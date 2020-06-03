
    <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="IntervieweesRecommendation.aspx.vb"
    Inherits="GOSHRM.IntervieweesRecommendation" EnableEventValidation="false" Debug="true" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<head>
    <title>Applicants</title>
<link rel="icon" type="image/png" href="../../../images/goshrm.png">
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

 <script type="text/javascript">
     function ConfirmSend() {
         var send_value = document.createElement("INPUT");
         send_value.type = "hidden";
         send_value.name = "send_value";
         if (confirm("Send mail to shortlisted candidates")) {
             send_value.value = "Yes";
         } else {
             send_value.value = "No";
         }
         document.forms[0].appendChild(send_value);
     }
    </script>
    <script type="text/javascript">
        function OpenMaxWindow(file) {
            var xMax = screen.width, yMax = screen.height;
            window.open(file, 'open_window', 'scrollbars=yes,width=' + xMax + ',height=' + yMax + ',top=0,left=0,resizable=yes');
        } 
    </script>
</head>



<body>
        <form id="form1">

               <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        <div class="row">
            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                Head</h5>
        </div>
        <div class="row">
            <div class="col-sm-3 col-md-6 col-xs-6 pull-left">
                <p><a href="JobInterviews.aspx"><u>Interviews</u></a><label>></label><a href="Interviewees.aspx"><u>Interview Shortlists</u></a><label>></label><a href="#">Recommendations</a></p>
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
                            BorderStyle="Solid" Font-Names="Verdana" PageSize="100" DataKeyNames="id"
                            Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>                                
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" ItemStyle-VerticalAlign="top"/>
                                <asp:BoundField DataField="Employee" HeaderText="Interviewer" SortExpression="Employee" ItemStyle-VerticalAlign="top"  />                               
                                <asp:BoundField DataField="comment" HeaderText="Comment"/>
                                <asp:BoundField DataField="interviewer_recommendation" HeaderText="Recommendation" SortExpression="interviewer_recommendation"  ItemStyle-VerticalAlign="top"/>
                                <asp:BoundField DataField="dateentered" HeaderText="Date"  SortExpression="dateentered" DataFormatString="{0:dd, MMM yyyy}" />  
                                                                                            
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="top"
                                    ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/interviewevaluationform.aspx?id={0}",
                      HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="OpenMaxWindow(this.href); return false;" Text='Evaluation Form' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachement" ItemStyle-Font-Bold="true" ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("evaluationfile")%>' CommandArgument='<%# Eval("id") %>'
                                            runat="server" OnClick="LinkDownLoad"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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

        <%--<div style="border: thin solid #C0C0C0">
            <asp:Label ID="lblHeader" runat="server" BackColor="#6699FF" Font-Size="Medium" Style="color: #FFFFFF;
                font-weight: 700; font-family: Candara" Width="100%"></asp:Label>
        </div>
        <div style="border: thin solid #C0C0C0">
         <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="11px" 
                    ForeColor="Red" Width="100%" Font-Names="Verdana"></asp:Label>
        </div>
        <div style="border: thin solid #C0C0C0">
            <asp:Label ID="Label1" runat="server" BackColor="#6699FF" Font-Size="Medium" Style="color: #FFFFFF;
                font-weight: 700; font-family: Candara" Text="Applicants" Width="100%"></asp:Label>
        </div>
     
        <div style="border: thin solid #C0C0C0">
                        <asp:Button ID="btnDelete" runat="server" BackColor="#FF3300" BorderStyle="None"
                            ForeColor="White" Height="20px" Text="&lt; Back to Previous Page" Width="150px"
                            Style="text-transform: none" Font-Names="Verdana" Font-Size="11px" />
         <asp:Label ID="lblStatus0" runat="server" Font-Bold="True" Font-Size="11px" 
                    ForeColor="Red" Width="1%" Visible="False" Font-Names="Verdana"></asp:Label>
        </div>
   
        <div>
            <asp:GridView ID="gridTrainers" runat="server" AllowPaging="True" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="11px" Height="50px" Width="100%"
                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" 
                GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" 
                BorderColor="#CCCCCC" PageSize="200" 
                DataKeyNames="candidateEmail">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>                    
                    <asp:BoundField DataField="Rows" ItemStyle-Width="2px" HeaderText="Row" />                  
                    <asp:BoundField DataField="Employee" ItemStyle-Width="15%" HeaderText="Interviewer" />
                    <asp:BoundField DataField="Comment" ItemStyle-Width="50%" HeaderText="Comment" />                  
                    <asp:BoundField DataField="interviewer_recommendation" ItemStyle-Width="10%" HeaderText="interviewer_recommendation"
                         /> 
                    <asp:BoundField DataField="dateentered" ItemStyle-Width="10%" HeaderText="Updated On" ItemStyle-HorizontalAlign="Center"/>  
                    <asp:TemplateField HeaderText="" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" >
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Employee/Recruitment/interviewevaluationform.aspx?id={0}",
                      HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                Text='Evaluation Form' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="Uploaded File" ItemStyle-Width="10%" 
                        ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text = '<%# Eval("evaluationfile")%>' CommandArgument = '<%# Eval("id") %>' runat="server" OnClick = "LinkDownLoad"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        <div style="height: 163px">
         
            <div style="height: 50px">
            </div>
            <div style="border: thin solid #C0C0C0">
                <asp:Label ID="Label3" runat="server" Font-Size="Smaller"></asp:Label>
            </div>
            <div style="height: 20px">
                
            </div>
            <div>
            </div>
        </div>--%>
        </form>
    </body>
</html>
</asp:Content>