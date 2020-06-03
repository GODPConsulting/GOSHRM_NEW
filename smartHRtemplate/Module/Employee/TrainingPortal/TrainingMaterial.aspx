<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="TrainingMaterial.aspx.vb" Inherits="GOSHRM.TrainingMaterial" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    


<body>
   <form id="form1" >
   <div class="container col-md-12">
    <div class="row">
            <div>
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-xs-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Head</h5>
            </div>
        </div>--%>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <%--<h5><b id="divdetailheader" runat="server"></b></h5>--%>
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
                <div class="panel-body">
    <div class="row">
        <div style="margin-right:20px;" class="col-sm-3 col-md-3 col-xs-6 pull-right">
            <div class="form-group form-focus">
                <input id="search" runat="server" type="text" class="form-control floating" style="padding: 12px 20px;height: 30px"
                    placeholder="Search..." />
                <button id="btFind" type="button" runat="server" class="glyphicon glyphicon-search"
                    onserverclick="btnFind_Click" style="height: 29px; width: 40px">
                </button>
            </div>
        </div>

        <div class="col-sm-3 col-md-2 col-xs-6 pull-right">
            <button id="btBack" type="button" runat="server" class="btn btn-primary btn-danger" onserverclick="btnBack_Click"
                style="height: 30px; width: 100%">
                << Back
            </button>
        </div>


        
    </div>

    <div class="row">
            <div class="table-responsive">
                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortRecords" AllowSorting="True"
                            BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="100" DataKeyNames="id"
                             Width="100%" Height="50px" ToolTip="click row to select record"
                            Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                            AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                            BorderColor="#CCCCCC" CssClass="table table-condensed">
                            <RowStyle BackColor="White" />
                            <Columns>                                
                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                <asp:BoundField DataField="Name" HeaderText="Name" /> 
                                <asp:TemplateField HeaderText="File Name" 
                        ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text = '<%# Eval("filename")%>' CommandArgument = '<%# Eval("id") %>' runat="server" OnClick = "LinkDownLoad"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                                <asp:BoundField DataField="filetype" HeaderText="File Type" />   
                    <asp:BoundField DataField="filesize" HeaderText="Size (KB)" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" SortExpression="filesize"  />     
                    <asp:BoundField DataField="createdon" HeaderText="Date Uploaded" DataFormatString="{0:dd, MMM yyyy}" SortExpression="createdon"/>
                               
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
        </div></div>
        </div>


    
    
    </form>
</body>
</html>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
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


