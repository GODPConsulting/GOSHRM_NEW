<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.vb"
    Inherits="GOSHRM.UpdateUser" EnableEventValidation="false" Debug="true" %>
    <asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

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
        .style2
        {
            font-family: Candara;
            font-size: small;
            width: 175px;
        }
        .style3
        {
            font-family: Candara;
            font-size: medium;
            height: 44px;
            width: 175px;
        }
        .style4
        {
            height: 44px;
            width: 539px;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 175px;
        }
        .style6
        {
            font-family: Candara;
            width: 175px;
        }
        .style7
        {
            width: 175px;
        }
        .style8
        {
            width: 539px;
        }
    </style>
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function cboaccess_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("btnaccess").click();
	}
//]]>
</script>
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
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtName]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        //                        url: '<%=ResolveUrl("~/AddUser.aspx/GetName") %>',
                        url: "UpdateUser.aspx/GetName",
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });   
    </script>
      <div class="container">
            <div>
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:Button ID="btnaccess" runat="server" BackColor="White" ForeColor="Black" Width="5px"
                            Height="5px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                    </div>
                </div>
                <div class="row">
                    <div class=" col-md-10">
                        <div class="panel panel-success">
                         <div class="panel-heading">
                         <h4><b>User</b></h4>
                         </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        
                                        <form action="">                                        
                                            <div class="row">
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            Username*</label>
                                                        <input id="username" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is Employee*</label>
                                                        <telerik:radcombobox id="cboIsEmp" runat="server" width="100%" font-names="Verdana"
                                                            autopostback="True" forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Name*</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <telerik:radcombobox id="cboEmployee" runat="server" filter="Contains" visible="False"
                                                                    width="100%" autopostback="True" font-names="Verdana" forecolor="#666666" rendermode="Lightweight"
                                                                    skin="Bootstrap">
                                                                </telerik:radcombobox>
                                                                <input id="fullname" runat="server" class="form-control" type="text" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboIsEmp" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Email</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <input id="usermail" runat="server" class="form-control" type="text" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Status*</label>
                                                        <telerik:radcombobox id="cbostatus" runat="server" width="100%" font-names="Verdana"
                                                            forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Role*</label>
                                                        <telerik:radcombobox id="radroletypes" runat="server" filter="Contains" skin="Bootstrap"
                                                            width="100%" font-names="Verdana" forecolor="#666666">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is System Administrator</label>
                                                        <select id="isadminsystem" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is HR Administrator</label>
                                                        <select id="isadminhr" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Is Finance Administrator</label>
                                                        <select id="isadminfinance" runat="server" class="select form-control">
                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div id="divaccesslevel" runat="server" class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Access Level*</label>
                                                        <telerik:radcombobox id="cbolevel" runat="server" checkboxes="False" filter="Contains"
                                                            autopostback="True" enablecheckallitemscheckbox="False" font-names="Verdana"
                                                            forecolor="#666666" skin="Bootstrap" rendermode="Lightweight" width="100%">
                                                        </telerik:radcombobox>
                                                    </div>
                                                </div>
                                                <div id="divaccess" runat="server" class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Access*</label>
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <telerik:radcombobox id="cboaccess" runat="server" checkboxes="True" filter="Contains"
                                                                    autopostback="True" enablecheckallitemscheckbox="True" font-names="Verdana"
                                                                    forecolor="#666666" skin="Bootstrap" rendermode="Lightweight" width="100%" onclientdropdownclosing="cboaccess_DropDownClosing">
                                                                </telerik:radcombobox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="cbolevel" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <telerik:radlistbox id="lstaccess" runat="server" enabled="False" font-names="Verdana"
                                                            forecolor="#666666" font-size="13px" width="100%" rendermode="Lightweight" skin="Bootstrap">
                                                        </telerik:radlistbox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:CheckBox ID="chkChange" runat="server" AutoPostBack="True" Font-Bold="True"
                                                    ForeColor="#666666" Style="font-family: Candara" Text="Reset Password" Font-Names="Verdana"
                                                    Font-Size="12px" />
                                            </div>
                                            <div id="divpwd" runat="server" style="display:none" class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            New Password*</label>
                                                        <input id="txtPwd" runat="server" class="form-control" type="password" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div style="margin-top:24px;" class="form-group">
                                                         <asp:Button ID="btnPwd" runat="server" CssClass="btn btn-info" Text="Reset Password"
                                                        ForeColor="White" Width="150px" Height="39px" BorderStyle="None"
                                                        Font-Names="Verdana" Font-Size="12px" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12 m-t-20 text-center">
                                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-success">
                                                    Save &amp; Update</button>
                                                <button id="Button2" runat="server" onserverclick="btnCancel_Click" type="submit"
                                                    style="width: 150px" class="btn btn-primary btn-info">
                                                    << Back</button>
                                            </div>
                                       
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<%--    <table>
        <tr>
            <td class="style1" colspan="2" style="background-color: #4CAF50">
                User
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style8">
                <asp:TextBox ID="txtid" runat="server" Width="113px" Style="font-size: medium; font-family: Candara"
                    Visible="False" Height="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="Is Employee *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style4">
                <telerik:RadComboBox ID="cboIsEmp" runat="server" ForeColor="#666666" Width="100%"
                    AutoPostBack="True" Font-Names="Verdana" Font-Size="12px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style5" valign="top">
                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="Name *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="cboEmployee" runat="server" Filter="Contains" ForeColor="#666666"
                    Visible="False" Width="100%" AutoPostBack="True" Font-Names="Verdana" Font-Size="12px">
                </telerik:RadComboBox>
                <asp:TextBox ID="txtName" runat="server" Width="100%" ForeColor="#666666" BorderColor="#CCCCCC"
                    BorderWidth="1px" Visible="False" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                <asp:HiddenField ID="hfCustomerId" runat="server" />
                <%--  <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtName"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" 
         ServiceMethod="GetNames">
    </asp:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="Username *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtusername" runat="server" Width="100%" ForeColor="#666666" BorderColor="#CCCCCC"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="Email Address *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtemail" runat="server" Width="100%" ForeColor="#666666" Font-Names="Verdana"
                    Font-Size="12px" BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="Status *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="cbostatus" runat="server" ForeColor="#666666" Width="100%"
                    Font-Names="Verdana" Font-Size="12px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Role *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="radroletypes" runat="server" Filter="Contains" ForeColor="#666666"
                    Width="100%" Font-Names="Verdana" Font-Size="12px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:CheckBox ID="chkAdmin" runat="server" Font-Names="Verdana" Font-Bold="True"
                    ForeColor="#666666" Text="Is Super Admin" Font-Size="12px" />
            </td>
            <td class="style8">
                <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Height="25px" ForeColor="#666666"
                    ReadOnly="True" Width="100%" Font-Names="Verdana" Font-Size="11px">Super Admin User: will full access to application regardless of role assigned to user</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:CheckBox ID="chkHR" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Text="Is HR Manager" Font-Size="12px" />
            </td>
            <td class="style8">
                <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" Height="25px" ForeColor="#666666"
                    ReadOnly="True" Width="100%" Font-Names="Verdana" Font-Size="11px">User able to perform key HR Role such as approve Training, handle appraisals etc</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="border-bottom-style: solid; border-bottom-color: #CCCCCC" class="style7">
                <asp:CheckBox ID="chkIsFinance" runat="server" Font-Names="Verdana" Font-Bold="True"
                    ForeColor="#666666" Text="Is Finance Manager" Font-Size="11px" />
            </td>
            <td class="style8">
                <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" Height="25px" ForeColor="#666666"
                    ReadOnly="True" Width="100%" Font-Names="Verdana" Font-Size="11px">User able to perform Finance Manager Role such as approve Loan Request</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style7" valign ="top">
                <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="Access Level *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="cbolevel" runat="server" CheckBoxes="False" Filter="Contains"
                    AutoPostBack="True" EnableCheckAllItemsCheckBox="False" 
                    Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" RenderMode="Lightweight" Width="100%">
                </telerik:RadComboBox>                
            </td>
        </tr>
        <tr>
            <td class="style7" valign ="top">
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Access *" Font-Bold="True"
                    ForeColor="#666666" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                       <telerik:RadComboBox ID="cboaccess" runat="server" CheckBoxes="True" 
                            Filter="Contains" EnableCheckAllItemsCheckBox="True" 
                    Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="12px" RenderMode="Lightweight" Width="100%" 
                            onclientdropdownclosing="cboaccess_DropDownClosing">
                </telerik:RadComboBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbolevel" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
                <telerik:RadListBox ID="lstaccess" runat="server" Enabled="False" Font-Names="Verdana"
                            ForeColor="#666666" Font-Size="11px" Width="100%">
                        </telerik:RadListBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:CheckBox ID="chkChange" runat="server" AutoPostBack="True" Font-Bold="True"
                    ForeColor="#666666" Style="font-family: Candara" Text="Reset Password" Font-Names="Verdana"
                    Font-Size="12px" />
            </td>
            <td class="style8">
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Label ID="lblNewPwd" runat="server" Font-Bold="True" ForeColor="#666666" Text="New Password"
                    Visible="False" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtPwd" runat="server" Width="70%" ForeColor="#666666" TextMode="Password"
                    Visible="False" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="12px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
            <td class="style8">
                <asp:Button ID="btnPwd" runat="server" Text="Reset Password" BackColor="#4CAF50"
                    ForeColor="White" Width="178px" Height="20px" BorderStyle="None" Visible="False"
                    Font-Names="Verdana" Font-Size="11px" />
            </td>
        </tr>
        <tr>
            <td class="style7">
            </td>
            <td class="style8">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style7">
                <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#4CAF50" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
            </td>
            <td class="style8">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" />
                <asp:Button ID="btnaccess" runat="server" BackColor="White" ForeColor="Black"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
</asp:Content> 