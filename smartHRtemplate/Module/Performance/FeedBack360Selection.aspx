<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="FeedBack360Selection.aspx.vb"
    Inherits="GOSHRM.FeedBack360Selection" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
     <link rel="stylesheet" href="../../AdminLTE/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="../../AdminLTE/dist/css/Admin-lte.min.css"/>
    <link rel="stylesheet" href="../../AdminLTE/plugins/ionicons/css/ionicons.min.css"/>
    <link rel="stylesheet" href="../../Skins/_all-skins.min.css"/>
    <link rel="stylesheet" href="../../css/font-awesome.min.css"/>
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

    <script type="text/javascript" id="telerikClientEvents3">
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
<body>
    <form>
    
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

     <div class="container col-md-12">
         <div class="row">
             <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblid" runat="server" Font-Size="6px" Visible="False"></asp:Label>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
             <div class="panel-body">
             <div class="row">
                <div class="col-md-12 m-t-20 text-center">  
                            <%--<button id="btnClose" runat="server" onserverclick="btnClose_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-danger">
                                Save Record</button>--%>
                                <asp:Button ID="btnSave" runat="server" Text="Save" ForeColor="White"
                                Width="150px" CssClass="btn btn-primary btn-success" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="12px" />
                                <asp:Button ID="btnCancel" runat="server" Text="<< Back" ForeColor="White"
                                Width="150px" CssClass="btn btn-primary btn-danger" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="12px" />
                                 <asp:Button ID="btnselect" runat="server" BackColor="White" ForeColor="Black"
                                Width="150px" CssClass="btn btn-primary btn-info" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                                Font-Size="12px" />
             </div></div>
             <div class="row">
                        <div class=" col-md-12">
                            <telerik:RadComboBox Skin="Bootstrap" ID="cboReviewer" runat="server" 
                                CheckBoxes="True" EnableCheckAllItemsCheckBox="True"
                                                RenderMode="Lightweight" Width="100%" 
                                Filter="Contains" 
                                                Font-Names="Verdana" Font-Size="12px" AutoPostBack="True" 
                                  onclientdropdownclosing="cboReviewer_DropDownClosing">
                              </telerik:RadComboBox>
                        </div>
                        <div class=" col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                <ContentTemplate>
                                    <telerik:RadListBox ID="lstReviewers" runat="server" 
                                        ResolvedRenderMode="Classic" BorderStyle="None"
                                        Enabled="False" Width="100%" 
                                        RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                            Font-Size="12px">
                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                        <EmptyMessageTemplate>
                                           None
                                        </EmptyMessageTemplate>
                                    </telerik:RadListBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboReviewer" EventName="ItemChecked" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div> 
             </div>
            <div class="row">
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <asp:GridView ID="GridVwHeaderChckbox" runat="server" AllowSorting="True"
                    BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" 
                    DataKeyNames="id" Width="100%" Height="50px" ToolTip="click row to select record" CssClass="table table-condensed"
                    Font-Size="11px" AutoGenerateColumns="False" GridLines="Vertical" ForeColor="#666666" 
                    BorderWidth="1px" BorderColor="#CCCCCC" ShowHeaderWhenEmpty="True" EmptyDataText ="No data to display">
                    <RowStyle BackColor="white" />
                    <Columns>
                        <asp:BoundField DataField="Rows" ItemStyle-Width="5px" HeaderText="Rows">
                            <ItemStyle Width="5px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="Name">
                        </asp:BoundField>                        
                        <asp:BoundField DataField="gradelevel" HeaderText="Grade">
                        </asp:BoundField>
                        <asp:BoundField DataField="office" HeaderText="Unit/Dept">
                        </asp:BoundField>
                         <asp:BoundField DataField="stat" HeaderText="Completion">
                        </asp:BoundField>
                        <asp:BoundField DataField="datesubmitted" HeaderText="Submitted On">
                        </asp:BoundField>
                        <asp:BoundField DataField="selectedby" HeaderText="Reviewer Selected By">
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                </asp:GridView>
            </div>
       </div></div></div> 
        
    </form>
</body>
</html>
</asp:Content>