<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MyExit.aspx.vb"
    Inherits="GOSHRM.MyExit" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title> </title>
</head>

    

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
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data, delete will be cancelled if request is approved?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function ConfirmSave() {
            var confirmsave_value = document.createElement("INPUT");
            confirmsave_value.type = "hidden";
            confirmsave_value.name = "confirmsave_value";
            if (confirm("Do you want to send request, request cannot be edited after send?")) {
                confirmsave_value.value = "Yes";
            } else {
                confirmsave_value.value = "No";
            }
            document.forms[0].appendChild(confirmsave_value);
        }
    </script>
 
 


<body>
    <form id="form1" >
    <div class="content container-fluid">
        <div class="row">
            <div class="col-md-8">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" Visible="false"></asp:TextBox>
                </div>
            </div>
        </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
        
   

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                NAME</label>
                            <input id="aname" runat="server" class="form-control" type="text" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                FORWARD TO *</label>
                            <telerik:radcombobox id="radForwardTo" runat="server" filter="Contains" forecolor="#666666"
                                resolvedrendermode="Classic" width="100%" rendermode="Lightweight" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                EXIT TYPE *</label>
                            <telerik:radcombobox id="cboexittype" runat="server" forecolor="#666666" resolvedrendermode="Classic"
                                width="100%" rendermode="Lightweight" skin="Bootstrap">
                            </telerik:radcombobox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                NOTICE DATE</label>
                            <div class="cal-icon">
                               <%-- <input id="anoticedate" runat="server" class="form-control datetimepicker" type="text" /></div>--%>
                                     <telerik:raddatepicker Skin="Bootstrap" ID="anoticedate" runat="server" 
                                                    ForeColor="#666666" Culture="en-US"
                                                    MinDate="" ResolvedRenderMode="Classic" Visible="true" Width="100%">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                                </telerik:raddatepicker>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                EXIT DATE *</label>
                            <div class="cal-icon">
                                <%--<input id="aexitdate" runat="server" class="form-control datetimepicker" type="text" /></div>--%>
                                <telerik:raddatepicker Skin="Bootstrap" ID="aexitdate" runat="server" 
                                                    ForeColor="#666666" Culture="en-US"
                                                    MinDate="" ResolvedRenderMode="Classic" Visible="true" Width="100%">
                                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                        Width="">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl=""></DatePopupButton>
                                                </telerik:raddatepicker>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-8">
                        <div class="form-group">
                            <label>
                                REASON *</label>
                            <textarea id="areason" runat="server" class="form-control" rows="5" cols="1"></textarea>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8 m-t-20">
                        <asp:Button ID="btsend" runat="server" Text="Save & Send" OnClientClick="ConfirmSave()"
                            ForeColor="White" Width="150px" Height="35px" CssClass="btn btn-success" BorderStyle="None"
                            Font-Names="Verdana" Font-Size="14px" />
                        <button id="btnback" runat="server" onserverclick="btnClose_Click" type="submit"
                            style="width: 150px" class="btn btn-primary btn-info">
                            Back</button>
                    </div>
                </div>
            </asp:View>

        <asp:View ID="View2" runat="server">
           
           <br />
            <div class="row">
                    <div class=" col-md-12">
                        <div class="form-group">
                           <h3 id="lbnotify" runat="server"></h3>
                        </div>
                    </div>
                </div>
                <br />
            <div class="row">
                    <div class="col-md-4 m-t-20 text-center">
                        <button id="Button2" runat="server" onserverclick="btnOK_Click" type="submit"
                            class="btn btn-block btn-info">
                            OK</button>
                    </div>
                </div>

        </asp:View>
         <asp:View ID="View3" runat="server">
             <div class="row">
                 <div class="col-sm-3 col-md-1 col-xs-6">
                     <button id="btAdd" type="button" runat="server" class="btn btn-success" onserverclick="btnAdd_Click"
                         style="height: 30px; width: 100px">
                         Add
                     </button>
                 </div>
                 <div class="col-sm-3 col-md-1 col-xs-6">
                     <asp:Button ID="btDelete" runat="server" Text="Delete" OnClientClick="Confirm()"
                         ForeColor="White" Width="100px" Height="30px" CssClass="btn btn-danger"
                         BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                 </div>
             </div>
            <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="False"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="10" DataKeyNames="id"
                    Width="100%" Height="50px" ToolTip="click row to select record"
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
                                <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" /> 
                        <asp:BoundField DataField="exittype" HeaderText="Exit Type"  SortExpression="exittype"/>                      
                        <asp:BoundField DataField="NoticeDate" HeaderText="Notice Date"  SortExpression="NoticeDate" DataFormatString="{0:dd, MMM yyyy}"/>
                        <asp:BoundField DataField="TerminationDate" HeaderText="Exit Date"  SortExpression="TerminationDate" DataFormatString="{0:dd, MMM yyyy}"/>
                         <asp:BoundField DataField="SupervisorApproval" HeaderText="Manager's Approval"/>                        
                         <asp:BoundField DataField="Approval2" HeaderText="Higher Approval"/>                      
                        <asp:BoundField DataField="HRApproval" HeaderText="HR Approval"/>                        
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
        </asp:View>
        </asp:MultiView>
 </div>
    </div></div>
    </form>
</body>
</html>
</asp:Content>