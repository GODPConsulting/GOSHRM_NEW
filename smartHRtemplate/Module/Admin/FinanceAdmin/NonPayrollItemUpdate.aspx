
<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NonPayrollItemUpdate.aspx.vb" Inherits="GOSHRM.NonPayrollItemUpdate" EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="NonPayrollItemUpdate.aspx.vb"
    Inherits="GOSHRM.NonPayrollItemUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>


<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    
    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 192px;
        }
        .style6
        {
        }
        .style7
        {
            font-family: Candara;
            font-size: x-small;
            width: 192px;
            color: #FF0000;
        }
        .style8
        {
            width: 555px;
        }
        </style>
</head>
<%-- <script type="text/javascript" language="javascript">
    function LoadList()
    {        
        var ds=null;
        ds = <%=listFilter %>;
            $( "#txtName" ).autocomplete({
              source: ds
            });
    }
    </script>--%>
<body>
    <form id="form1" action ="">

    <div class="container">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                </div>
                <div class="col-md-8 col-md-offset-0">
                    <h4 class="page-title" style="color:#1BA691">Payslip Deduction Item</h4>
                    <form action="">
                    <div class="row">
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>Item</label>
                                <input id="payslipitem" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>Type</label>
                                <telerik:RadDropDownList ID="radInputType" Font-Names="Verdana" ForeColor="#666666"
                                    Font-Size="12px" runat="server" DefaultMessage="--Select--" AutoPostBack="True"
                                    Skin="Bootstrap" Width="100%">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label id="lblComponents" runat="server">Components</label>
                                        <telerik:RadComboBox ID="radComponents" runat="server" CheckBoxes="True" Filter="Contains"
                                            AutoPostBack="True" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" ForeColor="#666666"
                                            Font-Size="12px" RenderMode="Lightweight" Width="100%" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radInputType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadListBox ID="lstComponents" runat="server" Enabled="False" Font-Names="Verdana"
                                            ForeColor="#666666" Font-Size="12px" Width="100%" Skin="Bootstrap">
                                        </telerik:RadListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="CheckAllCheck" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>Position</label>
                                <input id="position" runat="server" class="form-control" type="text" />
                            </div>
                        </div>                        
                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Active</label>
                                <select id="isactive" runat="server" class="select form-control">
                                    <option>No</option>
                                    <option>Yes</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-10 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
                    </div>
                    </form>
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