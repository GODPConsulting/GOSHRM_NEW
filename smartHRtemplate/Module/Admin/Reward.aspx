<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Reward.aspx.vb" Inherits="GOSHRM.Reward" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <script type="text/javascript" language="javascript">
            //    Grid View Check box
            function CheckAllEmp(Checkbox) {
                var GridVwHeaderChckbox = document.getElementById("<%=gridskills.ClientID %>");
                for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                    GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                }
            }
    </script>
    <head>
        <title>Add New</title>
        <script type="text/javascript">
            function closeWin() {
                popup.close();   // Closes the new window
            }
        </script>

    </head>

    <body>
        <form id="form1">

            <div class="container col-md-10">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px"
                            Font-Names="Candara" Height="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtskillid" runat="server" Font-Size="1px" Height="1px" Width="1px"
                            Visible="False"></asp:TextBox>
                        <input id="Text1" runat="server" class="form-control" type="text" visible="false"  />
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Reward SetUp</b></h5>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Name of Bonus*</label>
                                        <input id="abonusname" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                                  <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Active</label>
                                        <telerik:RadDropDownList ID="RadDropDownList1" runat="server" DefaultMessage="-- Select --"
                                            Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>Bonus Type</label>
                                        <telerik:RadDropDownList ID="abonustype" runat="server" DefaultMessage="-- Select --"
                                            Width="100%" ForeColor="#666666"  autopostback="True" Skin="Bootstrap" >
                                        </telerik:RadDropDownList>
                                    </div>
                                </div>
                               
                              
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label id="lblcomponents" runat="server"  >
                                            Components</label>
                                        <telerik:RadComboBox ID="radComponents" runat="server" Filter="Contains" checkboxes="True" EnableCheckAllItemsCheckBox="True"
                                           autopostback="True" RenderMode="Lightweight" Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                        </ContentTemplate> 
                                    <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="abonusType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    </asp:UpdatePanel> 
                                    </div>
                                </div>
                                    
                                  <div class=" col-md-6">
                                    <div class="form-group">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate >
                                        <label id="lblstatus" runat="server">
                                            Taxable</label>
                                        <telerik:RadDropDownList ID="radStatus" runat="server" DefaultMessage="-- Select --"
                                            Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadDropDownList>
                                                </ContentTemplate>
                                             <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="abonusType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                            </asp:UpdatePanel> 
                                    </div>
                                </div>
                                  <div class=" col-md-12">
                                    <div class="form-group">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                            <ContentTemplate>
                                        <label id="lblDropDownList2" runat="server" >Bonus Model</label>
                                        <telerik:RadDropDownList ID="RadDropDownList2" AutoPostBack="true"   runat="server" DefaultMessage="-- Select --"
                                            Width="100%" ForeColor="#666666" Skin="Bootstrap">
                                        </telerik:RadDropDownList>
                                                </ContentTemplate>
                                             <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="abonusType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                            </asp:UpdatePanel> 
                                    </div>
                                </div>
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                            <ContentTemplate>
                                        <label id="lblflatpercentage" runat="server" >Flat Percentage*</label>
                                        <input id="aflatpercentage" runat="server" class="form-control" type="text" />
                                                </ContentTemplate> 
                                             <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="RadDropDownList2" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                            </asp:UpdatePanel> 

                                    </div>
                                </div>
                              
                               
                                <div class="col-md-12 m-t-20 text-center">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                        class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                <div id="pnskill" runat="server" class="panel panel-info">
                <div class="panel-heading">
                   <h6><b>Performance Score range </b></h6>  
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="search-box-wrapper col-sm-6 col-md-3 col-xs-12 form-group pull-right">
                            <input id="search" style="width: 100%" runat="server" type="text" placeholder="Search..." class="search-box-input" />
                            <button onserverclick="btnFind_Click" id="btsearch" runat="server" class="search-box-button"><i style="color: Black;" class="fa fa-search"></i></button>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 form-group pull-right">
                            <button id="btExport" data-toggle="tooltip" data-original-title="Export" type="button" runat="server" class="fa fa-download btn btn-default btn-sm" onserverclick="btnExport_Click"
                                style="height: 35px">
                            </button>
                            <button id="btnUploadFile" type="button" runat="server" class="fa fa-upload btn btn-default btn-sm"
                                onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="Upload(Skill, CourseCode, Rating)" style="margin-right: 10px; margin-left: 10px; height: 35px">
                            </button>
                            <asp:LinkButton ID="btDelete" data-toggle="tooltip" data-original-title="Delete" Height="35px" runat="server" CssClass="btn btn-default btn-sm" OnClick="Delete" OnClientClick="Confirm()">
                            <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                            </asp:LinkButton>
                            <button id="btAdd" type="button" data-toggle="tooltip" data-original-title="Add Skills" runat="server" class="glyphicon glyphicon-plus btn btn-default btn-sm" onserverclick="btnAddSkill_Click"
                                style="height: 35px; margin-right: 10px; margin-left: 10px;">
                            </button>
                        </div>
                        <div class="col-md-3 col-sm-6 col-xs-12 pull-right">
                            <input style="height: 35px;" class="form-control" type="file" id="file1" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gridskills" runat="server" OnSorting="SortRecords" AllowSorting="True"
                                BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="10" DataKeyNames="id"
                                Width="100%" Height="50px" ToolTip="click row to select record" Font-Size="12px"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Rows" HeaderText="Rows" SortExpression="rows"
                                        ItemStyle-VerticalAlign="Top" />
                                    <asp:TemplateField HeaderText="Lower Score range" SortExpression="skills"
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("skill")%>' CommandArgument='<%# Eval("id") %>'
                                                runat="server" OnClick="DrillDown"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="LowerScore" HeaderText="Rating" SortExpression="rating"
                                        ItemStyle-VerticalAlign="Top" />
                                      <asp:BoundField DataField="HigherScore" HeaderText="Rating" SortExpression="rating"
                                        ItemStyle-VerticalAlign="Top" />
                                     <asp:BoundField DataField="Percentage" HeaderText="Rating" SortExpression="rating"
                                        ItemStyle-VerticalAlign="Top" />
                                </Columns>
                                <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                            </asp:GridView>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $("[id*=gridskills] td").hover(function () {
                                        $("td", $(this).closest("tr")).addClass("hover_row");
                                    }, function () {
                                        $("td", $(this).closest("tr")).removeClass("hover_row");
                                    })
                                })
                            </script>
                        </div>
                    </div>
                </div>
            </div>
                                                </ContentTemplate>
                      <Triggers >
                                         <asp:AsyncPostBackTrigger ControlID="RadDropDownList2" EventName="SelectedIndexChanged" />
                                    </Triggers>
                     </asp:UpdatePanel> 
            </div>
            

        </form>
    </body>
    </html>
</asp:Content>
