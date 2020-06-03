<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="SessionQuestions.aspx.vb" Inherits="GOSHRM.SessionQuestions" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
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
<body style="background: White">
    <body>
        <form>
        <script type="text/javascript">
            function closeme() {
                window.close();
            }
            window.onblur = closeme;
        </script>
        <div class="container col-md-12">
        <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                 <asp:TextBox ID="TextBox1" runat="server" Visible="False" Font-Size="1px"  
                Width="1px"></asp:TextBox>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Training Assessment Test</b></h5>
                </div>
             <div class="panel-body">
              <div class="panel-heading">
                    <h5><b id="lblHeader" runat="server"></b></h5>
                </div>
        <div class="row">
        <div class="search-box-wrapper col-sm-6 col-md-5 col-xs-12 form-group pull-right">
                    <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                        onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="CSV File (Comma Delimited):Question, questiontype, ordering,answer, optionA, optionB, optionC,optionD, Points" style="margin-right:10px;margin-left:10px;height:35px"></button>
                        <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                    style="margin-right:10px;height: 35px"></button>
                        <asp:LinkButton ID="btnDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                         <button id="btnAdd" type="button" data-toggle="tooltip" data-original-title="Add" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAdd_Click"
                        style="margin-left:10px;margin-right:10px;height: 35px;"></button> 
                    <input id="txtsearc" style="width:100%" runat="server" type="text" placeholder="Search..." class="search-box-input"/>
                    <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color:Black;" class="fa fa-search"></i></button>
                </div>
                 <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                    <input style="height:35px;" class="form-control" type="file" id="file1" runat="server" />
                </div>
                 <div style=" border-style: solid;border-width: 1px;border-color: #ccc;" class="col-md-4 col-sm-9 col-xs-12 pull-right">
                    <label>Assesment Duration (Minutes):</label>
                    <input type="text" id="txtDuration" style="width:50px; height:34px;" runat="server" value="10" />
                    <button id="btnSave" runat="server" onserverclick="btnSave_Click" class="btn btn-primary btn-success">Save Duration</button>
                </div>
        </div>
        <div style="height: 10px">
            <asp:TextBox ID="txtID" runat="server" Visible="False" Height="16px" Width="2px"></asp:TextBox>
            <asp:Label ID="lblHang" runat="server" Style="color: #FFFFFF"></asp:Label>
            <asp:Label ID="lblHang0" runat="server" Style="color: #FFFFFF"></asp:Label>
        </div>
        <div class="row">
            <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowPaging="True" AllowSorting="True"
                BorderStyle="Solid" Font-Names="Verdana" Font-Size="12px" Height="50px" OnRowDataBound="OnRowDataBound"
                OnSorting="SortRecords" PageSize="30" Width="100%" DataKeyNames="id" CssClass="table table-condensed" 
                AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="true" EmptyDataText="No data to display"  >
                <RowStyle BackColor="white" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Rows" />
                    <asp:TemplateField HeaderText="Question"  ItemStyle-Font-Bold="true"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Id", "SessionQuestionsUpdate.aspx?Id={0}") %>'
                                Text='<%# Eval("Questions") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Ordering" HeaderText="Ordering" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Points" HeaderText="Points" ItemStyle-HorizontalAlign="Right" />
                </Columns>
                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="Center" />
                <RowStyle HorizontalAlign="Right" />
            </asp:GridView>
        </div>
         <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="btnClose" runat="server" BorderStyle="None" ForeColor="White"
                                Text="<< Back" CssClass="btn btn-primary btn-danger" Width="100%" Font-Names="Verdana" 
                                Font-Size="12px" />
                    </div>
            </div>
        <div>           
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </div>
        </div>
        </div>
        </form>
    </body>
</html>
</asp:Content>