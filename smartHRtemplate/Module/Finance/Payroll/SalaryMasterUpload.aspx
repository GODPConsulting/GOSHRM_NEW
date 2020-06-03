
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalaryMasterUpload.aspx.vb" Inherits="GOSHRM.SalaryMasterUpload" EnableEventValidation="false" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            width: 183px;
            color: #FF3300;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 183px;
        }
        .style6
        {
        }
        .style7
        {
            color: #FF3300;
        }
        .style8
        {
            width: 183px;
        }
        .style9
        {
            width: 406px;
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
<body onunload = "window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">

    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>


     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>


    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />


    <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #C0C0C0">
    
        CSV Data Import</td>
    </tr>

      <tr>

    <td class="style8">
    
    </td>
    <td class="style9">
       
    </td>
    </tr>
    

    <tr>

    <td class="style5" valign="top">
    
        Select File&nbsp; <span class="style7">*</span></td>
    <td class="style9">
       
        <asp:FileUpload ID="FileUpload1" runat="server" />
       
    </td>
    </tr>

    <tr>

    <td class="style8">
    
    </td>
    <td class="style9">
       
        &nbsp;</td>
    </tr>
    <tr>

    <td valign ="top" class="style6" colspan="2"> 
    
        <asp:Label ID="Label1" runat="server" 
            style="font-family: Candara; font-size: small" 
            Text="* Column Order should not be changed"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" 
            style="font-family: Candara; font-size: small" 
            Text="* Employee No, Salary Item, Currency and Amount are compulsory"></asp:Label>
        <br />
         <asp:Label ID="Label3" runat="server" 
            style="font-family: Candara; font-size: small" 
            
            Text="* Salary Item must be Fixed Amount items only as Percent Based Items will be generated on Pay Slip automatically"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" 
            style="font-family: Candara; font-size: small" 
            Text="* Currency Code sample: USD, NGN, GBP etc."></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" 
            style="font-family: Candara; font-size: small" 
            Text="* File Format must be CSV"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" 
            style="font-family: Candara; font-size: small" Text="* Sample CSV File "></asp:Label>
        :
        <asp:LinkButton ID="LinkButton1" runat="server" 
            style="font-size: small; font-family: Candara">Download</asp:LinkButton>
        </td>
    </tr>
 
    <tr>
    <td class="style2" 
            style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
    
        * Required Field</td>
    <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0" 
            class="style9">
       
    </td>
    </tr>
     <tr>

    <td class="style8">
    
    </td>
    <td class="style9">
       
    </td>
    </tr>
 <tr>
    <td class="style8">
    
    </td>
    <td class="style9">
       
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="Small" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style8">
    
                     <asp:Button ID="btnAdd" runat="server" Text=" Upload File" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None"/>
                 </td>
    <td class="style9">
       
                     &nbsp;</td>
    </tr>
    </table>
 
    </form>
</body>
</html>
