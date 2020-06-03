<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="GratuityGradeUpdate.aspx.vb"
    Inherits="GOSHRM.GratuityGradeUpdate" EnableEventValidation="false" Debug="true" %>
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
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Gratuity Grade</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>JOB GRADE*</label> 
                                <telerik:RadComboBox Skin="Bootstrap" ID="radJobGrade" Runat="server" ForeColor="#666666"
                                    Filter="Contains" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" 
                                            Font-Size="12px"
                                    RenderMode="Lightweight" Width="100%">
                                </telerik:RadComboBox><input id="acode" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>ITEM MAKE-UP*</label>
                                <telerik:RadComboBox Skin="Bootstrap" ID="radComponents" Runat="server" CheckBoxes="True" ForeColor="#666666"
                                    Filter="Contains" AutoPostBack="True" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" 
                                            Font-Size="12px"
                                    RenderMode="Lightweight" Width="100%">
                                </telerik:RadComboBox>

                                     <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                                <ContentTemplate>
                                                     <telerik:RadListBox ID="lstMakeup" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                                        Font-Size="11px"
                                                        Width="100%" RenderMode="Lightweight">
                                                    </telerik:RadListBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="ItemChecked" />
                                                    <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="CheckAllCheck" />
                                                </Triggers>
                                            </asp:UpdatePanel>
       
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
    
        Gratuity Grade</td>
    </tr>

      <tr>

    <td class="style9">
    
    </td>
    <td class="style8">
       
        <asp:TextBox ID="txtid" runat="server" Width="13px" 
            style="font-size: medium; font-family: Candara" Font-Names="Candara" 
            Height="16px" Visible="False"></asp:TextBox>
       
    </td>
    </tr>
    

    <tr>

    <td valign=top class="style5">
    
        <asp:Label ID="Label5" runat="server" Text="Job Grade *" Font-Names="Verdana" 
            Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
        </td>
    <td class="style8">
       
            <telerik:RadComboBox ID="radJobGrade" Runat="server" ForeColor="#666666"
            Filter="Contains" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" 
                    Font-Size="12px"
            RenderMode="Lightweight" Width="100%">
        </telerik:RadComboBox>

    </td>
    </tr>
 
    <tr>
    <td  class="style5" valign =top style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
    
        <asp:Label ID="lblMakeup" runat="server" Text="Item Make-Up *" 
            Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
            Font-Size="12px"></asp:Label>
    
        </td>
    <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0" 
            class="style8">
            <telerik:RadComboBox ID="radComponents" Runat="server" CheckBoxes="True" ForeColor="#666666"
            Filter="Contains" AutoPostBack="True" EnableCheckAllItemsCheckBox="True" Font-Names="Verdana" 
                    Font-Size="12px"
            RenderMode="Lightweight" Width="100%">
        </telerik:RadComboBox>

             <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                        <ContentTemplate>
                             <telerik:RadListBox ID="lstMakeup" runat="server" Font-Names="Verdana" ForeColor="#666666"
                                Font-Size="11px"
                                Width="100%" RenderMode="Lightweight">
                            </telerik:RadListBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="ItemChecked" />
                            <asp:AsyncPostBackTrigger ControlID="radComponents" EventName="CheckAllCheck" />
                        </Triggers>
                    </asp:UpdatePanel>
       
  
       
    </td>
    </tr>
     <tr>

    <td class="style7">
    
        * Required Field</td>
    <td class="style8">
       
    </td>
    </tr>
 <tr>
    <td class="style6" colspan="2">
    
        <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" 
            Font-Size="12px" ForeColor="#FF3300"></asp:Label>
       
    </td>
    </tr>

     <tr>

    <td class="style9">
    
                     <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    <td class="style8">
       
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#FF3300" 
                         ForeColor="White" Width="120px" Height = "20px" BorderStyle="None" 
                         Font-Names="Verdana" Font-Size="11px"/>
                 </td>
    </tr>
    </table>--%>
 
    </form>
</body>
</html>
</asp:Content>