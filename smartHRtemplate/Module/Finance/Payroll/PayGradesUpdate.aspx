<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="PayGradesUpdate.aspx.vb"
    Inherits="GOSHRM.PayGradesUpdate" EnableEventValidation="false" Debug="true" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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

        <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">PAY GRADE</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>JOB GRADE*</label>
                                <telerik:RadDropDownList Skin="Bootstrap" ID="cboGrade" runat="server" ForeColor="#666666"
                                    DefaultMessage="-- Select --" Font-Names="Verdana"
                                    Height="16px" Width="100%">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>PAYSLIP ITEM*</label>
                                <telerik:RadDropDownList Skin="Bootstrap" ID="cboItem" runat="server" ForeColor="#666666"
                                    DefaultMessage="-- Select --" Font-Names="Verdana"
                                    Height="16px" Width="100%" AutoPostBack="True">
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>AMOUNT</label>
                                <input id="txtAmount" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    PAYSLIP ITEM TYPE</label>
                                <input id="lblItemType" runat="server" readonly="" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>PAYSLIP ITEM CATEGORY</label>
                                <input id="lblItemCategory" runat="server" readonly="" class="form-control" type="text" />
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
   <%-- <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        PAY GRADE</td>
    </tr>

      <tr>

    <td class="style6">
    
    </td>
    <td class="style9">
       
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="20px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Job Grade"></asp:Label>
        </td>
    <td class="style9">
       
        <telerik:RadDropDownList ID="cboGrade" runat="server" ForeColor="#666666"
            DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
            Height="16px" Width="100%">
        </telerik:RadDropDownList>
       
    </td>
    </tr>
   
    <tr>

    <td class="style6">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Payslip Item"></asp:Label>
        </td>
    <td class="style9">
       
        <telerik:RadDropDownList ID="cboItem" runat="server" ForeColor="#666666"
            DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
            Height="16px" Width="100%" AutoPostBack="True">
        </telerik:RadDropDownList>
       
    </td>
    </tr>
    
    <tr>

    <td class="style5">
    
        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Amount"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:TextBox ID="txtAmount" runat="server" Width="30%" ForeColor="#666666"
            style="text-align: right;" Font-Names="Verdana" 
            BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px"></asp:TextBox>
       
    </td>
    </tr>

    
     <tr>

    <td class="style6">
    
        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Payslip Item Type"></asp:Label>
         </td>
    <td class="style9">
       
        <asp:Label ID="lblItemType" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
         </td>
    </tr>
     
    <tr>

    <td valign ="top" class="style6"> 
    
        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Payslip Item Category"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:Label ID="lblItemCategory" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="12px" style="text-transform: uppercase"></asp:Label>
       
        </td>
    </tr>
   
    
    
 
 <tr>
    <td valign ="top" class="style6">
        &nbsp;</td>
    <td class="style9">
       
        &nbsp;</td>
    </tr>
    
    
 <tr>
    <td class="style6">
    
    </td>
    <td class="style9">
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style6">
    
                     <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    <td class="style9">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "25px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    </tr>
    </table>
 --%>
    </form>
</body>
</html>
</asp:Content>