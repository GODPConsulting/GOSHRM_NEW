<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobTitleUpdate.aspx.vb"
    Inherits="GOSHRM.JobTitleUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript" language="javascript">
                //    Grid View Check box
                function CheckAllEmp(Checkbox) {
                    var gridskills = document.getElementById("<%=gridskills.ClientID %>");
                    for (i = 1; i < gridskills.rows.length; i++) {
                        gridskills.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
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
        </telerik:RadCodeBlock>
        <style type="text/css">
            .style1 {
                color: #FFFFFF;
                font-family: Candara;
            }

            .lbl {
                font-family: Candara;
                font-size: medium;
            }

            .style2 {
                font-family: Candara;
                font-size: small;
                width: 88px;
            }

            .style6 {
                width: 581px;
            }

            .style10 {
                width: 139px;
            }

            #divSkill {
                height: 233px;
            }

            .style13 {
                width: 88px;
            }

            .style14 {
                width: 105px;
            }
        </style>
    </head>
    <body>
        <form id="form1">
            <div class="content">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-8 col-md-offset-0">
                                        <h4 class="page-title">Job Title</h4>
                                        <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtskillid" runat="server" Font-Size="1px" Height="1px" Width="1px"
                                            Visible="False"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Name*</label>
                                            <input id="jobname" runat="server" class="form-control" type="text" placeholder="enter job title" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Description</label>
                                            <textarea id="txtdescription" runat="server" class="form-control" rows="5" placeholder="enter job description"></textarea>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 m-t-20">
                                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-success">
                                                Save &amp; Update</button>
                                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-danger">
                                                << Back</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="pnskill" runat="server" class="panel panel-info">
                    <div class="panel-heading">
                        Skills
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
                                    onserverclick="btnUpload_Click" data-toggle="tooltip" data-original-title="Upload(Skills,Description,Rating,JobTitle)" style="margin-right: 10px; margin-left: 10px; height: 35px">
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
                                        <asp:TemplateField HeaderText="Skills" SortExpression="skills"
                                            ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" Text='<%# Eval("skills")%>' CommandArgument='<%# Eval("id") %>'
                                                    runat="server" OnClick="DrillDown"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="description" HeaderText="Description"
                                            SortExpression="description" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="rating" HeaderText="Weight (%)" SortExpression="rating"
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
            </div>
        </form>
    </body>
    </html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
