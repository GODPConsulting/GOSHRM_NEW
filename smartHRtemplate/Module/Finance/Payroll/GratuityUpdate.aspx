<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="GratuityUpdate.aspx.vb" Inherits="GOSHRM.GratuityUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript">

    function closeWin() {
        popup.close();   // Closes the new window
    }
   

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

    <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblentrystatus" runat="server" Font-Names="Verdana" ForeColor="#666666"
                    Font-Size="10px" Font-Italic="True"></asp:Label>
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">GRATUITY</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>EMPLOYEE*</label>
                                <telerik:RadComboBox runat="server" ForeColor="#666666"
                                                 Skin="Bootstrap" RenderMode="Lightweight" 
                                                    ResolvedRenderMode="Classic" Width="100%" ID="cboemp" 
                                                        Filter="Contains" AutoPostBack="True" Font-Names="Verdana" 
                                            Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>YEAR*</label>
                                <telerik:RadComboBox runat="server" ForeColor="#666666"
                                                  Skin="Bootstrap" RenderMode="Lightweight" 
                                   ResolvedRenderMode="Classic" Width="100%" ID="cboYear" 
                                                        Filter="Contains" Font-Names="Verdana" Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>MONTH</label>
                                <telerik:RadComboBox runat="server" ForeColor="#666666"
                                                    Skin="Bootstrap" RenderMode="Lightweight" 
                                                    ResolvedRenderMode="Classic" Width="100%" ID="cboMonth" Font-Names="Verdana" 
                                                            Font-Size="12px">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    GRATUITY</label>
                                <input id="txtAmount" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>CREATED BY</label>   
                                <input id="lblcreatedby" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    CREATED ON</label>
                                <input id="lblcreatedon" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    UPDATED BY</label>
                               <input id="lblupdatedby" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    UPDATED ON</label>
                                <input id="lblupdatedon" runat="server" class="form-control" type="text" />
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
   <%-- <table>
    <tr>
    <td class="style1" colspan="2" style="background-color: #1BA691">
    
        GRATUITY</td>
    </tr>

      <tr>

    <td class="style10">
    
    </td>
    <td class="style9">
       
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="20px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
    <tr>

    <td class="style10">
    
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Employee"></asp:Label>
        </td>
    <td class="style9">
       
                        <telerik:RadComboBox runat="server" ForeColor="#666666"
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="400px" ID="cboemp" 
                        Filter="Contains" AutoPostBack="True" Font-Names="Verdana" 
            Font-Size="12px">
</telerik:RadComboBox>
       
    </td>
    </tr>
   
    <tr>

    <td class="style10">
    
        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Year"></asp:Label>
        </td>
    <td class="style9">
       
                        <telerik:RadComboBox runat="server" ForeColor="#666666"
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="150px" ID="cboYear" 
                        Filter="Contains" Font-Names="Verdana" Font-Size="11px">
</telerik:RadComboBox>
                    
    </td>
    </tr>
     <tr>

    <td class="style10">
    
        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Month"></asp:Label>
        </td>
    <td class="style9">
       
                        <telerik:RadComboBox runat="server" ForeColor="#666666"
                    DropDownAutoWidth="Enabled" RenderMode="Lightweight" 
                    ResolvedRenderMode="Classic" Width="150px" ID="cboMonth" Font-Names="Verdana" 
                            Font-Size="11px">
</telerik:RadComboBox>
                    
    </td>
    </tr>
    <tr>

    <td class="style5">
    
        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
            Text="Gratuity"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:TextBox ID="txtAmount" runat="server" Width="150px" ForeColor="#666666"
            Font-Names="Verdana" 
            BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px"></asp:TextBox>
       
    </td>
    </tr>

    
     <tr>

    <td class="style10">
    
        &nbsp;</td>
    <td class="style9">
       
        <asp:Label ID="lblentrystatus" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="10px" Font-Italic="True"></asp:Label>
       
         </td>
    </tr>
     
    <tr>

    <td valign ="top" class="style10"> 
    
        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Bold="True" ForeColor="#666666"
            Text="Created By" Font-Italic="True"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:Label ID="lblcreatedby" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="10px" Font-Italic="True"></asp:Label>
       
        </td>
    </tr>
   <tr>

    <td valign ="top" class="style10"> 
    
        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Bold="True" ForeColor="#666666"
            Text="Created On" Font-Italic="True"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:Label ID="lblcreatedon" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="10px" Font-Italic="True"></asp:Label>
       
        </td>
    </tr>
    <tr>

    <td valign ="top" class="style10"> 
    
        <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Bold="True" ForeColor="#666666"
            Text="Updated By" Font-Italic="True"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:Label ID="lblupdatedby" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="10px" Font-Italic="True"></asp:Label>
       
        </td>
    </tr>
    <tr>

    <td valign ="top" class="style10"> 
    
        <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Font-Size="10px" Font-Bold="True" ForeColor="#666666"
            Text="Updated On" Font-Italic="True"></asp:Label>
        </td>
    <td class="style9">
       
        <asp:Label ID="lblupdatedon" runat="server" Font-Names="Verdana" ForeColor="#666666"
            Font-Size="10px" Font-Italic="True"></asp:Label>
       
        </td>
    </tr>
    
    
 
 <tr>
    <td valign ="top" class="style10">
        &nbsp;</td>
    <td class="style9">
       
        &nbsp;</td>
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

    <td class="style10">
    
    </td>
    <td class="style9">
       
        &nbsp;</td>
    </tr>
 <tr>
    <td class="style6" colspan="2">
    
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style10">
    
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
    </table>--%>
 
    </form>
</body>
</html>
</asp:Content> 