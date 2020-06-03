<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FeedBackSelection.aspx.vb"
    Inherits="GOSHRM.FeedBackSelection" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link rel="stylesheet" href="../../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../../../AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="../../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="../../../Skins/_all-skins.min.css"/>
    <link rel="stylesheet" href="../../../css/font-awesome.min.css"/>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
         <script type="text/javascript">
             function ShowProgress() {
                 setTimeout(function () {
                     var modal = $('<div />');
                     modal.addClass("modal");
                     $('body').append(modal);
                     var loading = $(".loading");
                     loading.show();
                     var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                     var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                     loading.css({ top: top, left: left });
                 }, 200);
             }
             $('form').live("submit", function () {
                 ShowProgress();
             });
    </script>
     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
           .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
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
            width: 231px;
        }
        .style6
        {
        }
        .style7
        {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function cboReviewer_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("btnselect").click();
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <table width="100%">
        <tr>
            <td>
            
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
            
            </td>
        </tr>
    </table>
    <div style="height: 15px">
            <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                Font-Size="12px" ForeColor="Red" Width="100%"></asp:Label>
        </div>
    <table>
        <tr>
            <td class="style6">                
                <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" Font-Bold="True" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" Font-Bold="True" />
            </td>
            <td>
                <asp:Label ID="lblid" runat="server" Font-Size="6px" Visible="False"></asp:Label>
            </td>
             <td>
                <asp:Label ID="lblempid" runat="server" Font-Size="6px" Visible="False"></asp:Label>
                <asp:Button ID="btnselect" runat="server" BackColor="White" ForeColor="Black"
                    Width="100px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" Visible="False" />
            </td>
        </tr>
        </table>
    <table width="100%">
      
        <tr>
            <td valign="top">

                              <telerik:RadComboBox ID="cboReviewer" runat="server" 
                                CheckBoxes="True" EnableCheckAllItemsCheckBox="True"
                                                RenderMode="Lightweight" Width="100%" 
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="11px" ForeColor="#666666" 
                                  onclientdropdownclosing="cboReviewer_DropDownClosing">
                              </telerik:RadComboBox>
                               <telerik:RadListBox ID="lstReviewers" runat="server" 
                                ResolvedRenderMode="Classic" BorderStyle="None"
                                Enabled="False" Width="100%" 
                                RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                    Font-Size="11px" ForeColor="#666666">
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                                <EmptyMessageTemplate>
                                   None
                                </EmptyMessageTemplate>
                            </telerik:RadListBox>

            </td>
          </tr>
          </table>
         <table width="100%">
          <tr>
            <td >
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" 
                    DataKeyNames="id" Width="100%" Height="50px" ToolTip="click row to select record"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" 
                    BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText ="No data to display" CssClass="table">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows">
                            <ItemStyle Width="5px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="name" ItemStyle-Width="100px" HeaderText="Name">
                            <ItemStyle Width="140px"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="gradelevel" ItemStyle-Width="150px" HeaderText="Grade">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="office" ItemStyle-Width="150px" HeaderText="Office">
                            <ItemStyle Width="120px"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="stat" ItemStyle-Width="60px" HeaderText="Completion">
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="datesubmitted" ItemStyle-Width="70px" HeaderText="Submitted On">
                            <ItemStyle Width="70px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="selectedby" ItemStyle-Width="100px" HeaderText="Reviewer Selected By">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#1BA691" ForeColor="White" HorizontalAlign="center" />
                </asp:GridView>
            </td>
        </tr>
        </table>
       
        
    </form>
    <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../../images/loaders.gif" alt="" />
    </div>
</body>
</html>
