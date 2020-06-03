<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="PensionManagerUpdate.aspx.vb" Inherits="GOSHRM.PensionManagerUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link rel="icon" type="image/png" href="~/images/goshrm.png">

    <title>Add New</title>
    <script type="text/javascript">

    function closeWin() {
        popup.close();   // Closes the new window
    }
   

    </script>
</head>
<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

                <div class="container col-md-8">
            <div class="row">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtPensionMgr" runat="server" Width="99%" style="text-align: left" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"
            BorderColor="#CCCCCC" BorderWidth="1px" Visible="False"></asp:TextBox>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">PENSION MANAGER</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>EMPLOYEE*</label>
                                 <telerik:RadComboBox runat="server" 
                     RenderMode="Lightweight" Skin="Bootstrap" 
                    ResolvedRenderMode="Classic" ID="cboEmployee" Width="100%" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="12px" Height="400px" 
            ForeColor="#666666">
                        </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>RSA ACCOUNT *</label>
                                <input id="txtRSA" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>PENSION MANAGER *</label>
                                <telerik:RadComboBox runat="server" 
                                    RenderMode="Lightweight" Skin="Bootstrap" 
                                    ResolvedRenderMode="Classic" Width="100%" ID="cboPenMgr" AutoPostBack="True" 
                                        Filter="Contains" Font-Names="Verdana" Font-Size="12px" Height="400px" 
                            ForeColor="#666666">
                                        </telerik:RadComboBox>
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
            </div>
<%--    <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        Pension Manager</td>
    </tr>

      <tr>

    <td class="style6">
    
    </td>
    <td class="style8">
       
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="20px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
    

    <tr>

    <td class="style5">
    
       <asp:Label ID="Label1" runat="server" Text="Employee" Font-Bold="True" 
            ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        <span class="style7">*</span></td>
    <td class="style8">
       
                        <telerik:RadComboBox runat="server" 
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="300px" ID="cboEmployee" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="12px" 
            ForeColor="#666666">
                        </telerik:RadComboBox>
        </td>
    </tr>

    
   
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Text="RSA Account"></asp:Label>
&nbsp;<span class="style7">*</span></td>
    <td class="style8">
       
        <asp:TextBox ID="txtRSA" runat="server" Width="150px" style="text-align: left" 
            ForeColor="#666666" Font-Size="12px" Font-Names="Verdana" 
            BorderColor="#CCCCCC" BorderWidth="1px"></asp:TextBox>
       
        </td>
    </tr>
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Font-Size="12px"
            Text="Pension Manager"></asp:Label>
        <span class="style7">*</span></td>
    <td class="style8">
                        <telerik:RadComboBox runat="server" 
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="300px" ID="cboPenMgr" AutoPostBack="True" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="12px" 
            ForeColor="#666666">
                        </telerik:RadComboBox>
        <br />
        <asp:TextBox ID="txtPensionMgr" runat="server" Width="99%" style="text-align: left" Font-Bold="True" ForeColor="#666666" Font-Size="12px" Font-Names="Verdana"
            BorderColor="#CCCCCC" BorderWidth="1px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
     <tr>

    <td class="style9">
    
        * Required Field</td>
    <td class="style8">
       
    </td>
    </tr>
 <tr>
    <td class="style6">
    
    </td>
    <td class="style8">
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style6">
    
                     <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    <td class="style8">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    </tr>
    </table>--%>
 
     <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
     </telerik:RadWindowManager>
 
    </form>
</body>
</html>
</asp:Content>