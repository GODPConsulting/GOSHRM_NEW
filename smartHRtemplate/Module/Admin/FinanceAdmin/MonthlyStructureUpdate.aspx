<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MonthlyStructureUpdate.aspx.vb"
    Inherits="GOSHRM.MonthlyStructureUpdate" EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MonthlyStructureUpdate.aspx.vb"
    Inherits="GOSHRM.MonthlyStructureUpdate" EnableEventValidation="false" Debug="true" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
  
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

</head>

<body >
    <form id="form1">
  
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    
    <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                </div>
                </div>
                  <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Payslip Earning Item</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Payslip Item*</label>
                                <input id="payslipitem" runat="server" class="form-control" type="text" />
                            </div>
                        </div>                        
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Taxable*</label>
                                <telerik:RadDropDownList ID="radIsTaxable" runat="server" Font-Names="Verdana" Font-Size="12px"
                                    DefaultMessage="--Select--" Width="100%" RenderMode="Lightweight" Skin="Bootstrap">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                          
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Type*</label>
                                <telerik:RadDropDownList ID="radItemType" runat="server" Font-Names="Verdana" Font-Size="12px"
                                    DefaultMessage="--Select--" Width="100%" AutoPostBack="True" RenderMode="Lightweight"
                                    Skin="Bootstrap">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                    <ContentTemplate>
                                        <label id="lblattendances" runat="server">Calculate with Attendance</label>
                                        <telerik:RadDropDownList ID="radAttendance" runat="server" Font-Names="Verdana" Font-Size="12px"
                                            DefaultMessage="--Select--" Width="100%" AutoPostBack="True" RenderMode="Lightweight"
                                            ToolTip="If pay should be computed based on attendance"
                                            Skin="Bootstrap">
                                        </telerik:RadDropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radItemType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">                             
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                    <ContentTemplate>
                                         <label id="lblMakeups" runat="server">Components</label>
                                        <telerik:RadComboBox ID="radComponents" runat="server" CheckBoxes="True" Filter="Contains"
                                            AutoPostBack="True" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" Font-Size="12px"
                                            RenderMode="Lightweight" Width="100%" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radItemType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadListBox ID="lstMakeup" runat="server" Font-Names="Verdana" Font-Size="12px"
                                            Width="100%" Visible="False" RenderMode="Lightweight" Skin="Bootstrap">
                                        </telerik:RadListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radItemType" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="ItemChecked" />
                                        <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="CheckAllCheck" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Amount*</label>
                                <input id="amount" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Position*</label>
                                <input id="position" runat="server" class="form-control" type="text" />
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
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
                </div>  </div>  </div>
        </div>

    </form>
</body>
</html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>