<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TimeSheetManager.aspx.vb"
    Inherits="GOSHRM.TimeSheetManager" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
    <body>
        <form id="form1">
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
        <div>
            <div>
                <%--<div>
                    <div>
                        <table>
                            <tr>
                                <td class="style33">
                                    <asp:Label ID="Label6" runat="server" Text="Project Status" Font-Size="11px" 
                                        Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="style34">
                                    <telerik:radcombobox ID="radSubStatus" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        ResolvedRenderMode="Classic" Width="100%" forecolor="#666666">
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="style21">
                        <tr>
                            <td class="style22">
                                <asp:TextBox ID="txtSubSearch" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" Height="20px" TextMode="Search" Width="251px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSubFind" runat="server" BackColor="#1BA691" BorderStyle="None"
                                    ForeColor="White" Height="20px" Text="View" Width="140px" Font-Names="Verdana"
                                    Font-Size="11px" />
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <div class="row">               
                    <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                        <input id="txtSubSearch" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                        <button onserverclick="btnSubFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                    </div>
                    <div class="col-sm-6 col-md-3 col-xs-12 pull-right">           
                    <telerik:radcombobox ID="radSubStatus" Skin="Bootstrap" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        ResolvedRenderMode="Classic" Width="100%" forecolor="#666666">
                                    </telerik:radcombobox>
                </div>
                </div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        BorderStyle="Solid" DataKeyNames="id" Font-Names="Verdana" Font-Size="11px" Height="50px"
                        OnRowDataBound="OnRowSurbodinateDataBound" OnSorting="SortSurbodinateRecords"
                        PageSize="500" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" ToolTip="click row to select record"
                        Width="100%" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC">
                        <RowStyle BackColor="white" />
                        <Columns>
                            
                            <asp:BoundField DataField="Rows" HeaderText="Rows">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Project">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/TimeManagement/ProjectMembers.aspx?ProjectID={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='<%# Eval("Name")%>' /></ItemTemplate>
                                <ItemStyle Font-Bold="True"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Client" HeaderText="Client">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="Start Date" HeaderText="Start Date">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Expected End Date" HeaderText="Expected End Date">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="End Date" HeaderText="End Date">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center"  />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
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
                            window.open("TimeSheetUpdate.aspx?id=" + code, "open_window", "width=600,height=600");
                        }
                    </script>
                </div>
                <div>
                    <table class="style21">
                        <tr>
                            <td class="style29">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style29">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div></div></div></div>
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
