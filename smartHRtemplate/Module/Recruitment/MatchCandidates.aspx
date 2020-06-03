<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MatchCandidates.aspx.vb"
    Inherits="GOSHRM.MatchCandidates" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        //    Grid View Check box
        function CheckAllEmp(Checkbox) {
            var gridTrainers = document.getElementById("<%=gridTrainers.ClientID %>");
            for (i = 1; i < gridTrainers.rows.length; i++) {
                gridTrainers.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <title>Match Candidates</title>
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
        function SaveMatch() {
            var save_value = document.createElement("INPUT");
            var radJobPosts = document.getElementById("<%=radJobPosts.ClientID %>");
            save_value.type = "hidden";
            save_value.name = "save_value";
            if (confirm("Do you want to Shortlist Candidates for the position " + radJobPosts.Text)) {
                save_value.value = "Yes";
            } else {
                save_value.value = "No";
            }
            document.forms[0].appendChild(save_value);
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
        .style28
        {
        }
        .style32
        {
            width: 330px;
        }
        .style33
        {
            font-family: Candara;
            text-transform: uppercase;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
            background-color: #FFFFFF;
            font-size: small;
            text-align: center;
        }
        .style34
        {
            width: 330px;
            text-align: center;
        }
        </style>
    <body>
        <form id="form1">
        <div>
             <table width="100%">
                <tr style="width:100%">
                    <td style="width:30%">
                                    
                                    <asp:Label ID="lblHeader2" runat="server" Font-Names="Verdana" 
                            Font-Size="13px" Width="100%"
                                        Style="text-align: left" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                    
                    </td>
                    <td align="center" style="width:40%">
                                    <asp:Label ID="lblView" runat="server" Font-Names="Verdana" Font-Size="20px" Width="100%"
                                        Style="text-align: center" Font-Bold="True" ForeColor="#666666">Select a Job Post</asp:Label>
                    </td>
                    <td style="width:30%">
                        <asp:Label ID="lblExperience2" runat="server" Visible="False" Font-Names="Verdana" 
                                  Font-Size="12px"></asp:Label>
                    </td >
                </tr>
                <tr style="width:100%">
                    <td style="width:30%">
                        <asp:Label ID="lblEducation1" runat="server" Visible="False" Font-Names="Verdana" 
                                  Font-Size="12px"></asp:Label>
                        <asp:Label ID="lblAge2" runat="server" Visible="False" Font-Names="Verdana" 
                                  Font-Size="12px"></asp:Label>
                              <asp:Label ID="lblAge1" runat="server" Visible="False" Font-Names="Verdana" 
                                  Font-Size="12px"></asp:Label>
                        </td>
                    <td align="center" style="width:40%" >
                                    <asp:CheckBox ID="chkOnline" runat="server" AutoPostBack="True"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666"
                                        Text="Include Online Test Job Posts" />
                    </td>
                    <td style="width:30%">
                        <asp:Label ID="lblJobID" runat="server" Visible="False" Font-Names="Verdana" 
                            Font-Size="12px"></asp:Label>
                    </td >
                </tr>
                <tr style="width:100%">
                    <td style="width:30%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcompany" runat="server" CssClass="lbl" Text="Company" Font-Names="Verdana"
                                        Font-Size="11px" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td>
                                        <telerik:radcombobox runat="server" 
                                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                                    ResolvedRenderMode="Classic" Width="300px" ID="cboCompany" AutoPostBack="True" 
                                        Filter="Contains" Font-Names="Verdana" Font-Size="11px" ForeColor="#666666">
                                        </telerik:radcombobox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center"  style="width:40%">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                        <ContentTemplate>
                              <telerik:RadComboBox ID="radJobPosts" runat="server" Width="100%" 
                                        AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                                  ForeColor="#666666">
                                    </telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkOnline" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                                   
                                    
                                   
                    </td>
                    <td style="width:30%">
                        <asp:Label ID="lblExperience1" runat="server" Visible="False" Font-Names="Verdana" 
                                  Font-Size="12px"></asp:Label>
                    </td >
                </tr>
                </table>
            
        </div>
  
        <div  style="width:100%" >
            <table>
                <tr>
                    <td class="style32">
                        <asp:Button ID="btnSend" runat="server" Text="Shortlist Candidates" BackColor="#669999"
                            ForeColor="White" Width="150px" Height="20px" BorderStyle="None" 
                            Font-Names="Verdana" Font-Size="11px" />
                    </td>
                    <td class="style9">
                        <asp:Button ID="btnDelete" runat="server" BackColor="#CC6600" BorderStyle="None"
                            ForeColor="White" Height="20px" Text="&lt; Back to Previous Page" Width="150px"
                            Style="text-transform: none" Font-Names="Verdana" Font-Size="11px" />
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        <asp:Label ID="lblGender" runat="server" Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666" Font-Bold="True">Gender</asp:Label>
                    </td>
                    <td class="style9">
                        <telerik:RadComboBox ID="cboGender" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        <asp:Label ID="lblGender0" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666" Font-Bold="True">Nationality</asp:Label>
                    </td>
                    <td class="style9">
                        <telerik:RadComboBox ID="cboNationality" runat="server" Width="326px"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        <asp:Label ID="lblGender1" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666" Font-Bold="True">Apply Age Criteria</asp:Label>
                    </td>
                    <td class="style9">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboAgeCriteria" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Label ID="lblAge" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboAgeCriteria" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        <asp:Label ID="lblGender2" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666" Font-Bold="True">Apply Education Level Criteria</asp:Label>
                    </td>
                    <td class="style9">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboEducation" runat="server" AutoPostBack="True" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Label ID="lblEducation" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                ForeColor="#666666"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboEducation" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        <asp:Label ID="lblGender3" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666" Font-Bold="True">Apply Work Experience Criteria</asp:Label>
                    </td>
                    <td class="style9">
                        <table width="100%">
                            <tr>
                                <td>
                                     <telerik:RadComboBox ID="cboExperience" runat="server" AutoPostBack="True"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666">
                        </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                        <ContentTemplate>
                                             <asp:Label ID="lblExperience" runat="server"  Font-Names="Verdana" Font-Size="12px" 
                            ForeColor="#666666"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboExperience" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                       
                       
                    </td>
                </tr>

                <tr>
                    <td class="style34">
                    </td>
                    <td >
                        <asp:Button ID="btnSave" runat="server" Text="Save Shortlist for Next Stage" BackColor="#3399FF"
                            ForeColor="White" Width="200px" Height="20px" BorderStyle="None" Style="text-transform: none;
                            text-align: center" OnClientClick="SaveMatch()" Visible="False" 
                            Font-Names="Verdana" Font-Size="11px" />
                    </td>
                </tr>
                </table>
                <table width="100%">
                <tr>
                    
                    <td class="style9">
                        <asp:GridView ID="gridTrainers" runat="server" AllowPaging="True" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" Font-Size="11px" Height="50px" Width="100%"
                            ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" PageSize="500" DataKeyNames="Email">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Row" ItemStyle-Width="10px" HeaderText="Row" />
                                <%--<asp:TemplateField HeaderText="Applicant" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/ApplicantsView.aspx?Email={0}",
                     HttpUtility.UrlEncode(Eval("Email").ToString())) %>' Text='<%# Eval("Applicant")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Applicant" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <a href="#" onclick='openWindow("<%# Eval("Email") %>");'>
                                            <%# Eval("Applicant")%></a>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Email" ItemStyle-Width="80px" HeaderText="Email" />
                                <asp:BoundField DataField="Mobile Number" ItemStyle-Width="40px" HeaderText="Mobile Number" />
                                <asp:BoundField DataField="Gender" ItemStyle-Width="30px" HeaderText="Gender" />
                                <asp:BoundField DataField="Specialisation" ItemStyle-Width="80px" HeaderText="Specialisation" />
                                <asp:BoundField DataField="Age" ItemStyle-Width="30px" HeaderText="Age" />
                                <asp:BoundField DataField="Education" ItemStyle-Width="50px" HeaderText="Education" />
                                <asp:BoundField DataField="Experience" ItemStyle-Width="30px" HeaderText="Experience"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                            <RowStyle HorizontalAlign="Left" />
                        </asp:GridView>
                    </td>
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
                            window.open("ApplicantsView.aspx?id=" + code, "open_window", "width=800,height=800");
                        }
                    </script>
                    <script type="text/javascript">
                        function openApplicants(code) {
                            window.open("Applicants.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                        }
                    </script>
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>--%>
                </tr>
                
                
            </table>
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
        {            text-align: center;
        }
        .style23
        {
            width: 275px;
            }
    </style>
</asp:Content>
