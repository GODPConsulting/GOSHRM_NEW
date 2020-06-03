<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveAllowanceSetup.aspx.vb"
    Inherits="GOSHRM.LeaveAllowanceSetup" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <script type="text/javascript">
        function openWindow(code) {
            window.open("Leaveallowancegrade.aspx?id=" + code, "open_window", "width=500,height=400");
        }
    </script>

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

    <body>
        <form id="form1">
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
            rel="Stylesheet" type="text/css" />
              <div class="container col-md-12">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">LEAVE ALLOWANCE</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>PAYSLIP DESCRIPTION</label>
                                <input id="txtLeaveDesc" runat="server" onserverchange="txtLeaveDesc_TextChanged" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>LEAVE START YEAR</label>
                               <telerik:RadComboBox ID="cboyear" Height="100px" runat="server" Skin="Bootstrap" 
                        EnableCheckAllItemsCheckBox="True" ForeColor="#666666"
                                        RenderMode="Lightweight" Width="100%" 
                                        Font-Names="Verdana" Font-Size="17px" 
                                        ToolTip="Earning Items to compute leave allowance">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>LEAVE ALLOWANCE(%)</label>
                                <input id="txtContribution" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    ALLOWANCE MAKEUP</label>
                                <telerik:RadComboBox ID="cboMakeup" runat="server" Skin="Bootstrap" 
                        CheckBoxes="True" EnableCheckAllItemsCheckBox="True" ForeColor="#666666"
                                        RenderMode="Lightweight" Width="100%" AutoPostBack="True" 
                                        Font-Names="Verdana" Font-Size="14px" 
                                        ToolTip="Earning Items to compute leave allowance">
                                    </telerik:RadComboBox>

                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                        <ContentTemplate>
                                            <telerik:RadListBox ID="lstMakeup" Skin="Bootstrap"  runat="server" ForeColor="#666666"
                                                ResolvedRenderMode="Classic" BorderStyle="None"
                                                Enabled="False" Width="100%" 
                                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                                    Font-Size="12px">
                                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                <EmptyMessageTemplate>
                                                   None
                                                </EmptyMessageTemplate>
                                            </telerik:RadListBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboMakeup" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-5 text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-success" ForeColor="White"
                                Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" />
                        </div>
                    </div><br />
                    <div class="row">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="B1" runat="server">LEAVE ALLOWANCE BY GRADE</b></h5>                    
                        </div>
                    
                       <div class="row m-t-10">
                                <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                                    <input id="txtsearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                                 <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">                                  
                                    <button id="btnUpload" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File: Grade, allowance(%)" style="margin-right:10px;margin-left:10px;height:35px"></button>
                                        <button id="btnExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                                    style="margin-right:10px;height: 35px"></button>
                                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnDelete_Click" OnClientClick="Confirm()">
                                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                        </asp:LinkButton>
                                         <button id="btnAddGrade" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddGrade_Click"
                                        style="height: 35px;margin-right:10px;margin-left:10px;"></button> 
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                                    <input style="height:35px;" class="form-control" type="file" id="FileUpload1" runat="server" />
                                </div>
                                <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                                    <button id="LinkButton2" type="button" runat="server" onserverclick="LinkButton2_Click" class="btn btn-info">Allowance by Employee</button>
                                </div>     
                        </div>

                        <div class="row">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-condensed"
                            AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" DataKeyNames="id" PageSize="200" 
                            ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" BorderStyle="Solid" Font-Names="Verdana" 
                            Font-Size="11px" Height="10px" ToolTip="click row to select record" Width="100%"                         
                            OnSorting="SortRecords">
                           <RowStyle BackColor="White" />
                            <Columns>
                            <asp:TemplateField><HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" 
                                        onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEmp" runat="server" />                                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                            <ItemStyle Width="5px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Job Grade" ItemStyle-Width="100px" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/Leaveallowancegrade.aspx?id={0}",
                             HttpUtility.UrlEncode(Eval("id").ToString())) %>'
                                                    Text='<%# Eval("GradeName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>                   
                            <asp:BoundField DataField="allowance" DataFormatString="{0:n}" 
                                    HeaderText="Leave Allowance(%)" SortExpression="allowance">
                            <ItemStyle HorizontalAlign="Right" Width="10px"></ItemStyle>
                            </asp:BoundField>                    
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" BackColor="white" ForeColor="#1BA691"></HeaderStyle>
                            </asp:GridView>
                        </div>
                         </div>
                </div>
            </div>

             </div>
             </div>
        <%--<table width="100%">
            <tr>
                <td class="style34" colspan="4" style="background-color: #1BA691">
                    <strong>Leave Allowance</strong></td>
            </tr>
            <tr>
                <td class="style37">

                </td>
                <td class="style35">
                    
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label2" runat="server" Text="Payslip Description" Font-Bold="True" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label ></td>
                <td class="style35">
                    <asp:TextBox ID="txtLeaveDesc" runat="server" Width="400px" Font-Names="Verdana" BorderColor="#CCCCCC" 
                        BorderWidth="1px" Font-Size="12px" ForeColor="#666666"
                        ToolTip="Leave Allowance Narration on payslip"></asp:TextBox>
                </td>
                <td class="style27">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label7" runat="server" Text="Leave Start Year" Font-Bold="True" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                </td>
                <td class="style35">
                                    <telerik:RadComboBox ID="cboyear" runat="server" 
                        EnableCheckAllItemsCheckBox="True" ForeColor="#666666"
                                        RenderMode="Lightweight" Width="150px" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        ToolTip="Earning Items to compute leave allowance">
                                    </telerik:RadComboBox>
                </td>
                <td class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style31">
                    <asp:Label ID="Label3" runat="server" Text="Leave Allowance (%)" 
                        Font-Bold="True" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                </td>
                <td class="style35">
                    <asp:TextBox ID="txtContribution" runat="server" Width="100px" Font-Names="Verdana" BorderColor="#CCCCCC" 
                        BorderWidth="1px"  Font-Size="12px" ForeColor="#666666"
                        ToolTip="general allowance percentage setting for job grades not specified with specific leave allowance percentage"></asp:TextBox>
                </td>
                <td class="style27">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Label ID="Label4" runat="server" Text="Allowance Makeup" Font-Bold="True" ForeColor="#666666"
                        Font-Names="Verdana" Font-Size="12px"></asp:Label>
                </td>
                <td class="style35">
                                    <telerik:RadComboBox ID="cboMakeup" runat="server" 
                        CheckBoxes="True" EnableCheckAllItemsCheckBox="True" ForeColor="#666666"
                                        RenderMode="Lightweight" Width="400px" AutoPostBack="True" 
                                        Font-Names="Verdana" Font-Size="12px" 
                                        ToolTip="Earning Items to compute leave allowance">
                                    </telerik:RadComboBox>
                </td>
                <td class="style27">
                   
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style37">
                    
                </td>
                <td class="style35">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                        <ContentTemplate>
                            <telerik:RadListBox ID="lstMakeup" runat="server" ForeColor="#666666"
                                ResolvedRenderMode="Classic" BorderStyle="None"
                                Enabled="False" Width="100%" 
                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                    Font-Size="12px">
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                <EmptyMessageTemplate>
                                   None
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboMakeup" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style30">
                </td>
                <td class="style24">
                </td>
            </tr>
        
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                        Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                        Font-Size="11px" />
                </td>
            </tr>
        
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
            </tr>
        
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="LEAVE ALLOWANCE BY GRADE" 
                        Font-Names="Verdana" Font-Size="16px" Font-Bold="True" ForeColor="#006600"></asp:Label >
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton2" runat="server" Font-Names="Verdana" 
                                    Font-Size="12px" ToolTip="set allowance per employee">Allowance by Employee</asp:LinkButton>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
              <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddGrade" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" ToolTip="Add new leave allowance grade" />                 
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" BackColor="#FF3300" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" 
                                    onclientclick="Confirm()" />
                            </td>
                            <td>
                               <asp:Button ID="btnExport" runat="server" Text="Export" BackColor="#FF9933" ForeColor="White"
                                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
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
                                    ToolTip="CSV File: Grade, allowance(%)" 
                                    Width="100px" Font-Names="Verdana" Font-Size="11px" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsearch" runat="server" BorderColor="#CCCCCC" BorderWidth="1px"
                                    Height="20px" Width="251px" TextMode="Search"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Text="Search" BackColor="#1BA691" ForeColor="White"
                                    Width="80px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style37">
                    &nbsp;</td>
                <td class="style35">
                   <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" DataKeyNames="id" PageSize="200" 
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" BorderStyle="Solid" Font-Names="Verdana" 
                    Font-Size="11px" Height="10px" ToolTip="click row to select record" Width="100%" 
                    ID="GridVwHeaderChckbox" 
                    OnSorting="SortRecords">
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <Columns>
                    <asp:TemplateField><HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" 
                                onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server" />                                            
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                    <ItemStyle Width="5px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Job Grade" ItemStyle-Width="100px" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Finance/Settings/Leaveallowancegrade.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' onclick="window.open (this.href, 'popupwindow',  'width=600,height=500,scrollbars,resizable'); return false;"
                                            Text='<%# Eval("GradeName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                   
                    <asp:BoundField DataField="allowance" DataFormatString="{0:n}" 
                            HeaderText="Leave Allowance(%)" SortExpression="allowance">
                    <ItemStyle HorizontalAlign="Right" Width="10px"></ItemStyle>
                    </asp:BoundField>                    
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#1BA691" ForeColor="White"></HeaderStyle>
                    </asp:GridView>
                 </td>
            </tr>
          
        </table>--%>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

	function cboApprove_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("Button1").click();
	}
//]]>
</script>

</asp:Content>
