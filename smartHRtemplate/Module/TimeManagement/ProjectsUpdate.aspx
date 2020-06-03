<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ProjectsUpdate.aspx.vb"
    Inherits="GOSHRM.ProjectsUpdate" EnableEventValidation="false" Debug="true" %>
    <asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllMember(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=GridMember.ClientID %>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>
<script type="text/javascript" language="javascript">
    //    Grid View Check box
    function CheckAllActivity(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=GridActivity.ClientID %>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Project</title>
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
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

        function cboMemberList_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button1").click();
        }
//]]>
    </script>
</head>
<script type="text/javascript">
    function setHeight() {
        var detail = document.getElementById("<%=txtDetail.ClientID%>");
        detail.style.height = detail.scrollHeight + "px";

        var act = document.getElementById("<%=txtActivity.ClientID%>");
        act.style.height = act.scrollHeight + "px";
    }
</script>
<body>
    <form>
    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>
            <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                </div>
                <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Project</b></h5>
                </div>
             <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Company*</label>
                                 <telerik:RadDropDownList Skin="Bootstrap" ID="radCompany" runat="server" DefaultMessage="-- Select --"
                                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" 
                                    ForeColor="#666666">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Client Name*</label>
                                <telerik:RadComboBox ID="cboClient" Skin="Bootstrap" runat="server" AutoPostBack="True" Filter="Contains"
                                    RenderMode="Lightweight" Width="100%" Font-Names="Verdana" 
                                    Font-Size="12px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Name </label>
                                <input id="txtName" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Project Manager</label>
                                <telerik:RadComboBox ID="cboProjectManager" Skin="Bootstrap" runat="server" AutoPostBack="True" Filter="Contains"
                                    RenderMode="Lightweight" Width="100%" Font-Names="Verdana" 
                                    Font-Size="12px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Team Lead*</label>
                                <telerik:RadComboBox ID="cboTeamLead" Skin="Bootstrap" runat="server" AutoPostBack="True" Filter="Contains"
                                    RenderMode="Lightweight" Width="100%" Font-Names="Verdana" 
                                    Font-Size="12px" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    Detail</label>
                                 <textarea id="txtDetail" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    STATUS</label>
                                <telerik:RadDropDownList Skin="Bootstrap" ID="radStatus" runat="server" DefaultMessage="-- Select --"
                                    Font-Names="Verdana" Font-Size="12px" Height="16px" Width="100%" AutoPostBack="True"
                                    ResolvedRenderMode="Classic" ForeColor="#666666" RenderMode="Lightweight">
                                    <Items>
                                        <telerik:DropDownListItem runat="server" Text="Not Started" />
                                        <telerik:DropDownListItem runat="server" Text="In Progress" />
                                        <telerik:DropDownListItem runat="server" Text="Completed" />
                                        <telerik:DropDownListItem runat="server" Text="Cancelled" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Start Date*</label>
                                <telerik:RadDatePicker Skin="Bootstrap" ID="dtStartDate" runat="server" Width="100%"
                                    RenderMode="Lightweight" Font-Names="Verdana" Font-Size="12px" 
                                    ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        Height="16px" RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Expected End Date*</label>
                                <telerik:RadDatePicker Skin="Bootstrap" ID="dtBudgetEndDate" runat="server"  Width="100%"
                                    MinDate="" RenderMode="Lightweight" Font-Names="Verdana" Font-Size="12px" 
                                    ForeColor="#666666">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        Height="16px" RenderMode="Lightweight">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class=" col-md-12">
                        <div class="form-group">
                            <label><asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                                     <asp:Label ID="lblEndDate" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666" Text="End Date"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />               
                                </Triggers>
                            </asp:UpdatePanel></label>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                     <telerik:RadDatePicker ID="dtEndDate" runat="server" Height="16px" Width="150px"
                                MinDate="" RenderMode="Lightweight" Font-Names="Verdana" Font-Size="12px" 
                                ForeColor="#666666">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                    FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                </Calendar>
                                <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                    Height="16px" RenderMode="Lightweight">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="radStatus" EventName="SelectedIndexChanged" />
               
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnSave" runat="server" onserverclick="btnSave_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button2" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div></div></div>
        </div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <div class="container col-md-10">
    <div id="details" runat ="server">
      <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="lblMembers" runat="server">Project Breakdown</b></h5>
                </div>
             <div class="panel-body">

     <table >
                    <tr>                        
                        <td>
                             <asp:LinkButton ID="lnkmembers" runat="server" Font-Names="Verdana" 
                                Font-Size="12px" ToolTip="">Project Members</asp:LinkButton>                            
                        </td>
                        <td>
                        </td>
                        <td>                           
                        </td>
                        <td>
                           <asp:LinkButton ID="lnkactivities" runat="server" Font-Names="Verdana" 
                                Font-Size="12px" 
                                ToolTip="">Project Activities</asp:LinkButton>
                        </td>
                        <td>                           
                        </td>
                    </tr>
             </table>
               <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">   
                <asp:View ID="members" runat="server">
                    <div>
                        <table class="style21">
                             <tr>
                                <td class="style22" colspan="2">
                                    <asp:Label ID="lblMemberProject" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Style="color: #FFFFFF; font-weight: 700; background-color: #1BA691" Text="Project Members"
                                        Visible="False" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style113">
                                </td>
                                <td class="style114">
                                    <asp:TextBox ID="MemberID" runat="server" AutoPostBack="True" Enabled="False" Font-Names="Candara"
                                        Font-Size="Small" Visible="False" Width="6%" Height="17px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style113" valign="top">
                                    <asp:Label ID="lblmember" runat="server" Font-Names="Verdana" Font-Size="12px" Text="Project Member(s)"
                                        Visible="False" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="style114">
                                    <telerik:RadComboBox ID="cboMemberList" runat="server" Filter="Contains" RenderMode="Lightweight"
                                        Width="100%" ResolvedRenderMode="Classic" Visible="False" 
                                        CheckBoxes="True" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#666666" AutoPostBack="True">
                                    </telerik:RadComboBox>
                                    <br />
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                        <ContentTemplate>
                             <telerik:RadListBox ID="lstMembers" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                        Enabled="False" Width="100%" Font-Names="Verdana" Font-Size="12px" 
                                        ForeColor="#666666">
                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                    </telerik:RadListBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboMemberList" EventName="SelectedIndexChanged" />
               
                        </Triggers>
                    </asp:UpdatePanel>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style113">
                                    <asp:Button ID="btnMemberSave" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Save" Visible="False" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td class="style114">
                                    <asp:Button ID="btnMemberCancel" runat="server" BackColor="Red" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Cancel" Visible="False" 
                                        Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style113">
                                </td>
                                <td class="style114">
                                    <asp:Label ID="lblstatus" runat="server" Font-Names="Candara" Font-Size="Small" Style="color: #FF0000;
                                        font-size: x-small"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style113">
                                    <asp:Button ID="btnMemberAdd" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Add" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td class="style114">
                                    <asp:Button ID="btnMemberDelete" runat="server" BackColor="Red" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Delete" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                    <asp:GridView ID="GridMember" runat="server" OnSorting="SortMemberRecords" AllowSorting="True"
                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                        OnRowDataBound="OnRowMemberDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                        Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" CssClass="table table-condensed"
                        BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpID" HeaderText="Emp ID">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Office" HeaderText="Office">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                    </script>
                    <script type="text/javascript">


                        $(function () {
                            $("[id*=GridMember] td").hover(function () {
                                $("td", $(this).closest("tr")).addClass("hover_row");
                            }, function () {
                                $("td", $(this).closest("tr")).removeClass("hover_row");
                            })
                        })
                    </script>
                </div>
                </asp:View>
                <asp:View ID="activities" runat="server">
                    <div>
                        <table class="style21">
                           
                            <tr>
                                <td class="style22" colspan="3">
                                    <asp:Label ID="lblActivityProject" runat="server" Font-Names="Candara" Font-Size="Small"
                                        Style="color: #FFFFFF; font-weight: 700; background-color: #1BA691" Text="Activity"
                                        Visible="False" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style108" valign="top">
                                    <asp:Label ID="lblActivity" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Text="Project Activity" Visible="False" Font-Bold="True" 
                                        ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="style107">
                                    <asp:TextBox ID="txtActivity" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                        Font-Names="Verdana" Font-Size="12px" TextMode="MultiLine" Visible="False" Width="100%"
                                        Height="100px" ForeColor="#666666"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style108" valign="top">
                                    <asp:Label ID="lblActEstimate" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Text="Estimated Hours to Complete Activity" Visible="False" 
                                        Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="style107" valign="top">
                                    <asp:TextBox ID="txtEstimation" runat="server" Width="10%" Style="text-align: right"
                                        Visible="False" Font-Names="Verdana" Font-Size="12px" 
                                        BorderColor="#CCCCCC" BorderWidth="1px" ForeColor="#666666">0</asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style108">
                                    <asp:Button ID="btnActivitySave" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Save" Visible="False" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td class="style107">
                                    <asp:Button ID="btnActivityCancel" runat="server" BackColor="Red" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Cancel" Visible="False" 
                                        Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style108">
                                    <asp:TextBox ID="txtActivityID" runat="server" AutoPostBack="True" 
                                        Enabled="False" Font-Names="Candara" Font-Size="10px" Height="16px" 
                                        Visible="False" Width="10%"></asp:TextBox>
                                </td>
                                <td class="style107">
                                    <asp:Label ID="lblstatus1" runat="server" Font-Names="Candara" Font-Size="12px"
                                        ></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style108">
                                    <asp:Button ID="btnActivityAdd" runat="server" BackColor="#1BA691" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Add" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td class="style107">
                                    <asp:Button ID="btnActivityDelete" runat="server" BackColor="Red" BorderStyle="None"
                                        ForeColor="White" Height="20px" Text="Delete" Width="100px" Font-Names="Verdana"
                                        Font-Size="12px" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                    <asp:GridView ID="GridActivity" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="12px" Height="50px"
                        OnRowDataBound="OnRowActivityDataBound" OnSorting="SortActivityRecords" PageSize="30"
                        Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666"
                        BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed" ShowHeaderWhenEmpty="True">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllActivity(this);" /></HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Activities">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "ActivitysUpdate.aspx?PID={0}") %>'
                                        Text='<%# Eval("Activity") %>' /></ItemTemplate>
                                <ItemStyle Font-Bold="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EstimatedHr" HeaderText="Estimated Hours for Completion">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                    </script>
                    <script type="text/javascript">

                        $(function () {
                            $("[id*=GridActivity] td").hover(function () {
                                $("td", $(this).closest("tr")).addClass("hover_row");
                            }, function () {
                                $("td", $(this).closest("tr")).removeClass("hover_row");
                            })
                        })
                    </script>
                    <script type="text/javascript">

                        function openWindow(code) {
                            window.open("LoanRequest.aspx?id=" + code, "open_window", "width=600,height=700");
                        }
                    </script>
                    <script type="text/javascript">

                        function openSchedule(code) {
                            window.open("LoanSchedule.aspx?id=" + code, "open_window", "width=600,height=700");
                        }
                    </script>
                </div>
                </asp:View>
            </asp:MultiView>

    </div></div></div></div>
    
    <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None" />
    </form>
</body>
</html>
</asp:Content>