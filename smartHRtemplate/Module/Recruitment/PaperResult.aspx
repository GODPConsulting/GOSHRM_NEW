<%--<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PaperResult.aspx.vb"
    Inherits="GOSHRM.PaperResult" EnableEventValidation="false" Debug="true" %>--%>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaperResult.aspx.vb"
    Inherits="GOSHRM.PaperResult" EnableEventValidation="false" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
    <html xmlns="http://www.w3.org/1999/xhtml">
   
    <title></title>
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
        .style24
        {
            width: 121px;
        }
        .style25
        {
            width: 399px;
            height: 12px;
        }
        .style26
        {
            width: 180px;
            height: 12px;
        }
        .style27
        {
            width: 114px;
            height: 12px;
        }
        .style29
        {
            width: 126px;
            height: 12px;
        }
        .style30
        {
            width: 543px;
            height: 12px;
        }
        </style>
    <body >
        <form id="form1" runat="server">
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <table width="100%">
                <tr style="width:100%">
                    <td style="width :100%">
                        <asp:Label ID="lblView" runat="server" Font-Names="Arial" Font-Size="Medium" 
                            Width="100%" Font-Bold="True" ForeColor="#FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <table >
                
              
           <%--     <tr>
                    <td class="style24">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Job Post"></asp:Label>
                        </td>
                    <td class="style25">
                        <telerik:RadComboBox ID="cboJobPost" runat="server" Width="100%"  Font-Names="Verdana" Font-Size="12px"
                            AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Job Test"></asp:Label>
                        </td>
                    <td class="style25">
                        <telerik:RadComboBox ID="cboJobTest" runat="server" Width="100%" 
                            Font-Names="Verdana" Font-Size="12px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style24">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px"  Text="Stage"></asp:Label>
                        </td>
                    <td class="style25">
                        <telerik:RadComboBox ID="cboStage" runat="server" Width="50%" 
                            Font-Names="Verdana" Font-Size="12px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                
                    </td>
                </tr>--%>
                <tr>
                    
                    <td >
                     <asp:TextBox ID="txtsearch" runat="server" Width="200px" Height = "20px" 
                         BorderColor="#CCCCCC" BorderWidth="1px" TextMode="Search"></asp:TextBox>
                    </td>
                    <td>
                
                <asp:Button ID="btnSend" runat="server" Text="Display" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="11px" />
                
                    </td>
                    <td>
                    </td>
                      <td class="style29">
                     <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick = "Delete" 
                         OnClientClick = "Confirm()" BackColor="#FF3300" 
                         ForeColor="White" Width="100px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
                 <td class="style26">
                               <asp:Button ID="Button1" runat="server" Text="Export" 
                         BackColor="#1BA691" ForeColor="White"
                                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                   Font-Size="11px" />
                 </td>
                 <td class="style30"  >
                     <asp:Panel ID="Panel1" runat="server" HorizontalAlign ="Right">
                         <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" 
                             Font-Size="11px" />
                         <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" 
                             BorderStyle="None" ForeColor="White" Height="20px" Text="Upload File" 
                             Width="100px" 
                             
                             ToolTip="CSV File: TestTitle,Stage e.g 1, ApplicantEmail,  Applicant Score, Date Taken e.g 31-DEC-2017" 
                             Font-Names="Verdana" Font-Size="11px" />
                     </asp:Panel>
                 </td>
                 <td >
                        <asp:Label ID="lbltestid" runat="server" Font-Names="Verdana" 
                         Font-Size="12px" Visible="False"></asp:Label>
                        <asp:Label ID="lbltesttitle" runat="server" Font-Names="Verdana" 
                         Font-Size="12px" Visible="False"></asp:Label>
                 </td>
                </tr>
            </table>
            <table style="width: 1573px" >
             <tr>
                
               
                 
             </tr>
         </table>
        </div>
 
       
        <div style="height: 163px">
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
                <script type="text/javascript">
                    function openWindow(code) {
                        window.open("JobPostingsUpdate.aspx?id=" + code, "open_window", "width=800,height=800");
                    }
                </script>
              
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" 
                AllowSorting="True" BorderStyle="Solid" 
                Font-Names="Verdana" AllowPaging="True" PageSize="40" DataKeyNames="id" 
                OnRowDataBound = "OnRowDataBound" 
                 Width="100%" Height="50px" ToolTip="click row to select record"  
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
                    <asp:BoundField DataField="JobTest" ItemStyle-Width="150px" HeaderText="Test Title" />
                    <asp:BoundField DataField="TestStage" ItemStyle-Width="10px" HeaderText="Stage" />
                    <asp:BoundField DataField="Applicant" ItemStyle-Width="100px" HeaderText="Applicant Name"  SortExpression= "Applicant"/>
                    <asp:BoundField DataField="ApplicantEmail" ItemStyle-Width="100px" HeaderText="Applicant Email" SortExpression= "ApplicantEmail"/>
                    <asp:BoundField DataField="Score" ItemStyle-Width="15px" HeaderText="Score" SortExpression= "Score" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Passmark" ItemStyle-Width="15px" HeaderText="Pass Mark" SortExpression= "Passmark" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="datetaken" ItemStyle-Width="30px" HeaderText="Date Taken" SortExpression= "datetaken" ItemStyle-HorizontalAlign="Center"/>
                      
                    <%--    <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/EmploymentStatusUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=500,height=400,scrollbars,resizable'); return false;"
                                            Text='<%# Eval("Applicant")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                        
                     
                </Columns>
                <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                
            </asp:GridView>
              
            </div>
        </div>
        </form>
    </body>
    </html>
<%--    <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/EmploymentStatusUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=500,height=400,scrollbars,resizable'); return false;"
                                            Text='<%# Eval("Applicant")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

