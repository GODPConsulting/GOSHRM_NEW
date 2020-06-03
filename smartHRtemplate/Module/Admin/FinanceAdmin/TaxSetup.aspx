<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="TaxSetup.aspx.vb"
    Inherits="GOSHRM.TaxSetup" EnableEventValidation="false" Debug="true" %>

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
            width: 314px;
        }
        .style27
        {
            height: 12px;
            width: 204px;
        }
        .style30
        {
            width: 204px;
        }
        .style31
        {
            width: 170px;
            font-size: small;
        }
        .style32
        {
            width: 34px;
        }
        .style33
        {
        }
        .style34
        {
            font-size: large;
        }
        .style35
        {
            width: 517px;
        }
        .style36
        {
            width: 170px;
            color: #FF0000;
        }
        .style37
        {
        }
        .style38
        {
            color: #FFFFFF;
            font-weight: bold;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
            background-color: #1BA691;
        }
        .style39
        {
            width: 170px;
        }
    .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}
    .RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}
    .RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}
    </style>
    <body>
        <form id="form1" action="">
         <asp:Label ID="lblid" runat="server" Text="0" Font-Names="Verdana" ForeColor="#666666" Font-Size="1px" Font-Bold="True" Visible="False"></asp:Label>
         <asp:TextBox ID="txtTaxID" runat="server" Width="10%" Font-Names="Verdana" BorderColor="#CCCCCC"
                                    ForeColor="#666666" BorderWidth="1px" Font-Size="1px" Visible="False">PAYE</asp:TextBox>
       <div class="container col-md-12">
       <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
            </div>
        </div>
       <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Tax Setting</b></h5>
                </div>
             <div class="panel-body">               
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Company</label>
                                <telerik:RadComboBox runat="server" DropDownAutoWidth="Enabled" RenderMode="Lightweight"
                                    ResolvedRenderMode="Classic" Width="100%" ID="cboCompany" 
                                    Filter="Contains" Font-Names="Verdana"
                                    Font-Size="12px" ForeColor="#666666" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                       
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Payslip Description</label>
                                <input id="taxdesc" runat="server" class="form-control" type="text" />
                            </div>
                        </div>   
                         <div class=" col-md-12">
                            <div class="form-group">
                                <label>Variable Monthly Tax Relief(A)</label>
                                <input id="taxrelief" runat="server" class="form-control" type="text" />
                            </div>
                        </div>   
                         <div class=" col-md-12">
                            <div class="form-group">
                                <label>Variable Gross Income Relief (%)(A)</label>
                                <input id="incomerelief" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Fixed Income Relief (%)(B)</label>
                                <input id="fixedrelief" runat="server" class="form-control" type="text" />
                            </div>
                        </div>                            
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Active</label>
                                <select id="isactive" runat="server" class="select form-control">
                                    <option>No</option>
                                    <option>Yes</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnSave_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button3" runat="server" onserverclick="btnBack_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                    </div>
            </div>
            </div>
    <div class="row" style="height:10px">
    </div>
        
        <div id="divdetails" runat="server" class="row">
            <div class="panel panel-success">
             <div class="panel-heading">
                    <h5><b>Settings</b></h5>
                </div>
                <div class="panel-body">
                    <div class="row">
                            <button id="btnAddTax" type="button" runat="server" class="btn btn-primary btn-success" onserverclick="btnAdd_Click"
                                style="height: 30px; width: 100px">
                                Add</button>
                            <asp:Button ID="btnDeleteTax" runat="server" Text="Delete" OnClientClick="Confirm()"
                                BackColor="#FF3300" ForeColor="White" Width="100px" Height="30px" CssClass="btn btn-primary btn-danger"
                                BorderStyle="None" Font-Names="Verdana" Font-Size="12px" />
                            <button id="btnValidateTax" type="button" runat="server" class="btn btn-primary btn-warning" onserverclick="btnValidate_Click"
                                style="height: 30px; width: 100px">
                                Validate</button>
                    </div>
                    <div class="row" style="height: 5px">
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" 
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="15" DataKeyNames="id"
                                 Width="100%" Height="50px" ToolTip="click row to select record"
                                Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                BorderColor="#CCCCCC" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="1%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="60px" ItemStyle-Font-Bold="true"
                                        SortExpression="item">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/Module/Admin/FinanceAdmin/TaxSetupUpdate.aspx?id={0}",
                     HttpUtility.UrlEncode(Eval("id").ToString())) %>' Text='Details' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" ItemStyle-Width="1px" HeaderText="Rows" />
                                    <asp:BoundField DataField="Range" ItemStyle-Width="100px" HeaderText="Range" />
                                    <asp:BoundField DataField="UpperValue" ItemStyle-Width="50px" HeaderText="Upper Value (Annual)"
                                        DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Monthly" ItemStyle-Width="50px" HeaderText="Upper Value (Monthly)"
                                        DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Rate" ItemStyle-Width="50px" HeaderText="Rate (%)" DataFormatString="{0:n}"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="MaximumTax" ItemStyle-Width="50px" HeaderText="Max. Tax (Monthly)"
                                        DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
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
                                function btnAddTax_onclick() {

                                }

                            </script>
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
