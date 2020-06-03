<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TimeSheets.aspx.vb"
    Inherits="GOSHRM.TimeSheets" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        .style29
        {
            width: 178px;
        }
        .style30
        {
            width: 93px;
        }
        .button
        {
            background-color: #008CBA; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
        .style31
        {
            width: 533px;
        }
        .style32
        {
            width: 233px;
        }
        .style33
        {
            width: 129px;
        }
        .style34
        {
            width: 354px;
        }
    </style>
    <body style="">
        <form id="form1">
   
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        </div>
      
        <div>
            <div>
                    <div>
                        <div class="container col-md-12">
                            <div class="row">
                                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                                     <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    <strong id="msgalert" runat="server">Danger!</strong>
                                </div>
                            </div>
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h5><b id="pagetitle" runat="server"></b></h5>
                                </div>
                             <div class="panel-body">       
                             <div class="row">
                             <div class="search-box-wrapper col-sm-6 col-md-4 col-xs-12 form-group pull-right"> 
                                <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                                <button onserverclick="btnSubFind_Click" id="btnsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                                </div>
                                <div class="col-sm-3 col-md-3 col-xs-6 pull-right">
                                     <telerik:RadComboBox Skin="Bootstrap" ID="radSubStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        ResolvedRenderMode="Classic" Width="100%" ForeColor="#666666">
                                    </telerik:RadComboBox>
                                </div>
                              </div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px" Height="50px"
                        OnRowDataBound="OnRowSurbodinateDataBound" OnSorting="SortSurbodinateRecords"
                        PageSize="100" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                        Width="100%" AutoGenerateColumns="False" CssClass="table table-condensed" GridLines="Vertical" ForeColor="#666666"
                        BorderWidth="1px" BorderColor="#CCCCCC">
                        <RowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEmp" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Project">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/TimeManagement/ProjectMembers.aspx?ProjectID={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Name")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Client" HeaderText="Client">
                            </asp:BoundField>
                            <asp:BoundField DataField="Start Date" HeaderText="Start Date">
                            </asp:BoundField>
                            <asp:BoundField DataField="Expected End Date" HeaderText="Expected End Date">
                            </asp:BoundField>
                            <asp:BoundField DataField="End Date" HeaderText="End Date">
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="white" ForeColor="#1BA691" HorizontalAlign="Center" />
                    </asp:GridView>
                    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript">

                    </script>
                    <script type="text/javascript">

                        $(function () {
                            $("[id*=GridView1] td").hover(function () {
                                $("td", $(this).closest("tr")).addClass("hover_row");
                            }, function () {
                                $("td", $(this).closest("tr")).removeClass("hover_row");
                            })
                        })
                    </script>
                    <script type="text/javascript">

                        function openSubWindow(code) {
                            window.open("TimeSheetUpdate.aspx?id=" + code, "open_window", "width=600,height=800");
                        }
                    </script>
                </div>
                </div></div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
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
        {
        }
    </style>
</asp:Content>
