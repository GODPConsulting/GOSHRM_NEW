<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="SalaryMasterUpdate.aspx.vb" Inherits="GOSHRM.SalaryMasterUpdate"
    EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Employee Salary</title>
        <script type="text/javascript">
            function ConfirmGenerate() {
                var confirm_gen = document.createElement("INPUT");
                confirm_gen.type = "hidden";
                confirm_gen.name = "confirm_gen";

                if (confirm("Refresh Salary Items for Employee, existing items won't be reset?")) {
                    confirm_gen.value = "Yes";
                } else {
                    confirm_gen.value = "No";
                }
                document.forms[0].appendChild(confirm_gen);
            }
        </script>

        <script type="text/javascript">
            function ConfirmRefresh() {

                var confirm_ref = document.createElement("INPUT");
                confirm_ref.type = "hidden";
                confirm_ref.name = "confirm_ref";

                if (confirm("refresh entire selected company 's Employee Pay from Grade Pay Structure?")) {
                    confirm_ref.value = "Yes";
                } else {
                    confirm_ref.value = "No";
                }
                document.forms[0].appendChild(confirm_ref);
            }
        </script>
    </head>
    <body>
        <form id="form1">
            <div class="container">
                <div class="row">
                    <div class="col-md-10">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert" runat="server">Danger!</strong>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <asp:Label ID="lblactive" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"
                                Width="1px"></asp:Label>
                            <input id="aempid" runat="server" class="form-control" type="text" readonly="readonly" visible="false" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-8 col-md-10" style="width: 100%">
                        <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Employee Salary
                        </h5>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6 col-xs-12 pull-right">
                                        <asp:Button ID="btnRegenerate" runat="server" BorderStyle="None" ForeColor="White"
                                            Height="30px" Text="Reset for New Items" Width="150px" ToolTip="Click to  generate Salary Schedule on first start or to add new pay item to employee payslip"
                                             CssClass="btn btn-primary btn-success" Font-Size="12px" OnClientClick="ConfirmGenerate()" />
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12 pull-right">
                                        <asp:Button ID="btnImport" runat="server" BorderStyle="None" ForeColor="White" Height="30px"
                                            Text="Reset" Width="150px" ToolTip="Reset Employee salary structures"
                                            CssClass="btn btn-primary btn-success"  Font-Size="12px" OnClientClick="ConfirmRefresh()" />
                                    </div>
                                </div>



                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                Name</label>
                                            <input id="aempname" runat="server" class="form-control" type="text" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <label>
                                            Job Grade</label>
                                        <input id="aempgrade" runat="server" class="form-control" type="text" readonly="readonly" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <label>
                                            Job Title</label>
                                        <input id="aemptitle" runat="server" class="form-control" type="text" readonly="readonly" />
                                    </div>
                                    <div class=" col-md-6">
                                        <label>
                                            Department/Office</label>
                                        <input id="aempoffice" runat="server" class="form-control" type="text" readonly="readonly" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <label>
                                            Location</label>
                                        <input id="alocation" runat="server" class="form-control" type="text" readonly="readonly" />
                                    </div>
                                    <div class=" col-md-6">
                                        <label>
                                            Net Before Tax</label>
                                        <input id="aempnbt" runat="server" class="form-control" type="text" readonly="readonly"
                                            style="text-align: right" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <label>
                                            Tax</label>
                                        <input id="aemptax" runat="server" class="form-control" type="text" readonly="readonly"
                                            style="text-align: right" />
                                    </div>
                                    <div class=" col-md-6">
                                        <label>
                                            Net After Tax</label>
                                        <input id="aempnat" runat="server" class="form-control" type="text" readonly="readonly"
                                            style="text-align: right" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridRepay" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                            PageSize="100" DataKeyNames="id" Width="100%" Height="50px" ToolTip="click row to select record"
                                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="rows" ItemStyle-Width="5%" HeaderText="Row" />
                                                <asp:BoundField DataField="SalaryItem" HeaderText="Salary Item" />
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" Width="100px" Font-Names="Verdana" Font-Size="12px" AutoPostBack="False"
                                                            ForeColor="Gray" runat="server" Text='<%# Eval("Amount") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="itemtype" HeaderText="Type" />
                                                <asp:BoundField DataField="itemclass" HeaderText="Category" />
                                            </Columns>
                                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 m-t-20">
                                        <button id="btnupdate" runat="server" onserverclick="btnRepay_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-success">
                                            Save &amp; Update</button>
                                        <button id="btclose" runat="server" onserverclick="btnClose_Click" type="submit"
                                            style="width: 150px" class="btn btn-primary btn-danger">
                                            << Back</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </form>
    </body>
    </html>
</asp:Content>
