<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Vacancies.aspx.vb" Inherits="GOSHRM.Vacancies"
    EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/Recruit.Master" AutoEventWireup="true" CodeBehind="Vacancies.aspx.vb"
    Inherits="GOSHRM.Vacancies" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
        .RadPicker
        {
            vertical-align: middle;
        }
        .rdfd_
        {
            position: absolute;
        }
        .RadPicker .rcTable
        {
            table-layout: auto;
        }
        .RadPicker .RadInput
        {
            vertical-align: baseline;
        }
        .RadInput_Default
        {
            font: 12px "segoe ui" ,arial,sans-serif;
        }
        .RadInput
        {
            vertical-align: middle;
        }
        .RadInput .riTextBox
        {
            height: 17px;
        }
        .RadPicker_Default .rcCalPopup
        {
            background-position: 0 0;
        }
        .RadPicker_Default .rcCalPopup
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif');
        }
        .RadPicker .rcCalPopup
        {
            display: block;
            overflow: hidden;
            width: 22px;
            height: 22px;
            background-color: transparent;
            background-repeat: no-repeat;
            text-indent: -2222px;
            text-align: center;
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
        }
    </style>
    <body>
        <body>
            <form id="form1" action="">
            <div class="container col-md-12">
           <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
         </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Jobs</b></h5>
                </div>
             <div class="panel-body">       
         <div class="row">
          <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">                        
                    <input id="search" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>    
        </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="code"
                        OnRowDataBound="OnRowDataBound" Width="100%" Height="50px" ToolTip="click row to select record"
                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                            <asp:TemplateField HeaderText="Code" ItemStyle-Font-Bold="true" SortExpression="Code">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Recruitment/Applications/VacancyView.aspx?Code={0}",
                     HttpUtility.UrlEncode(Eval("Code").ToString())) %>' Text='<%# Eval("Code")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Job Title" HeaderText="Job Title" SortExpression="Job Title" />
                            <asp:BoundField DataField="Job Type" HeaderText="Job Type" SortExpression="Job Type" />
                            <asp:BoundField DataField="Positions" HeaderText="Positions" SortExpression="Positions" />
                            <asp:BoundField DataField="company" HeaderText="Company" SortExpression="company" />
                            <asp:BoundField DataField="Date Posted" HeaderText="Date Posted" SortExpression="Date Posted" />
                            <asp:BoundField DataField="Closing Date" HeaderText="Closing Date" SortExpression="Closing Date" />
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
            </div></div></div>
            </form>
        </body>
    </body>
    </html>
</asp:Content>
