
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PayrollTimeException.aspx.vb" Inherits="GOSHRM.PayrollTimeException" EnableEventValidation="false" %>


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
            width: 95px;
        }
        .style6
        {
        }
        .style7
        {
            font-family: Candara;
            font-size: x-small;
            width: 95px;
            color: #FF0000;
        }
        .style8
        {
            width: 642px;
        }
        .style9
        {
            width: 95px;
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
<body  style="height: 317px">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
    <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        Grades exempted from Time Payroll Calculation</td>
    </tr>

      <tr>

    <td class="style9">
    
        &nbsp;</td>
    <td class="style8">
       
        <asp:Label ID="lblCompany" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
            Font-Size="13px"></asp:Label>
    
          </td>
    </tr>
    

    <tr>

    <td valign=top class="style5">
    
        <asp:Label ID="Label5" runat="server" Text="Job Grade" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
        <asp:Label ID="Label6" runat="server" style="color: #FF3300" Text="*"></asp:Label>
        </td>
    <td class="style8">
       
            <telerik:RadComboBox ID="radJobGrade" Runat="server" CheckBoxes="True" ForeColor="#666666"
            Filter="Contains" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" 
                    Font-Size="12px"
            RenderMode="Lightweight" Width="100%">
        </telerik:RadComboBox>

    </td>
    </tr>
 

    <tr>

    <td valign=top class="style5">
    
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="16px" Visible="False"></asp:TextBox>
       
        </td>
    <td class="style8">
       <table width="100%" >
        <tr>
            <td>
                                <asp:Button ID="btnAddGrade" runat="server" Text="Add" BackColor="#1BA691" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" ToolTip="Add new leave allowance grade" />                 
            </td>
              <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" BackColor="#FF3300" ForeColor="White"
                                Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="11px" 
                                    onclientclick="Confirm()" />
            </td>
              <td>
                               <asp:Button ID="btnExport" runat="server" Text="Export" BackColor="#FF9933" ForeColor="White"
                                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                    Font-Size="11px" />
            </td>
            <td>
            </td>
             <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Verdana" 
                                    Font-Size="11px" />
            </td>
             <td>
                                <asp:Button ID="btnUpload" runat="server" BackColor="#00CC99" 
                                    BorderStyle="None" ForeColor="White" Height="20px" Text="Upload File" 
                                    ToolTip="CSV File: job grade" 
                                    Width="100px" Font-Names="Verdana" Font-Size="11px" />
            </td>
        </tr>
       </table>
    </td>
    </tr>
 
    <tr>
    <td  class="style5" valign =top style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
    
        
       
        </td>
    <td    class="style8">

      
       
                   <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" GridLines="Both" BorderWidth="1px" 
                        BorderColor="#CCCCCC" DataKeyNames="id" PageSize="200" 
                    ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" BorderStyle="Solid" Font-Names="Arial" 
                    Font-Size="12px" Height="10px" ToolTip="click row to select record" Width="70%" 
                    ID="GridVwHeaderChckbox" 
                    ForeColor="#666666">
                    <%--<AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>--%>
                    <Columns>
                    <asp:TemplateField><HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" 
                                onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEmp" runat="server" />                                            
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Rows" HeaderText="Rows">
                    <ItemStyle Width="5px"></ItemStyle>
                    </asp:BoundField> 
          
                      <asp:BoundField DataField="jobgrade" HeaderText="Job Grade">
                    <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>                   
                                      
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#1BA691" ForeColor="White"></HeaderStyle>
                    </asp:GridView>

      
       
    </td>
    </tr>
 <tr>
    <td class="style6" colspan="2">
    
        &nbsp;</td>
    </tr>

     </table>
 
    </form>
</body>
</html>
