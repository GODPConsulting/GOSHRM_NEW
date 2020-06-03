<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="EmployeeTrainingSessionUpdate.aspx.vb" Inherits="GOSHRM.EmployeeTrainingSessionUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

    <div class="content container-fluid col-md-8">									
                    <div class="row">
                         <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong id="msgalert" runat="server">Danger!</strong>
                        </div>
                        <asp:TextBox ID="txtid" runat="server" Width="13px" 
                        style="font-size: medium; font-family: Candara" Font-Names="Candara" 
                        Height="20px" Visible="False"></asp:TextBox>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Employee Training &amp; Development Session</b></h5>
                        </div>
                     <div class="panel-body">      
					<div class="row">
						<div class="col-md-12">
									<div class="form-group">
										<label>Employee</label>
                                        <telerik:radcombobox runat="server" 
                                                            DropDownAutoWidth="Enabled" Skin="Bootstrap" 
                                                            ResolvedRenderMode="Classic" Width="100%" ID="cboEmployee" 
                                                    Filter="Contains" RenderMode="Lightweight">
                                        </telerik:radcombobox>
									</div>
									<div class="form-group">
										<label>Session</label>
										<telerik:raddropdownlist ID="radSession" runat="server" 
                                                    DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
                                                    Height="30px" Width="100%" Skin = "Bootstrap" 
                                                    DataTextField="Employee" DataValueField="EmpID" 
                                                    ResolvedRenderMode="Classic">
                                                </telerik:raddropdownlist>
									</div>
									<div class="form-group">
										<label>Status</label>											
                                                <telerik:RadDropDownList ID="radStatus" runat="server" 
                                                    DefaultMessage="-- Select --" Font-Names="Verdana" Font-Size="12px" 
                                                    Height="30px" Width="100%" Skin = "Bootstrap"  
                                                    DataTextField="Employee" DataValueField="EmpID" 
                                                    ResolvedRenderMode="Classic">
                                                </telerik:RadDropDownList>
									</div>
									<div class="m-t-20 text-center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Save" 
                                         ForeColor="White" Width="120px" Height = "30px" BorderStyle="None" CssClass="btn btn-success" 
                                         Font-Names="Verdana" Font-Size="12px"/>

                                         <asp:Button ID="btnCancel" runat="server" Text="<< Back" 
                                         ForeColor="White" Width="120px" Height = "30px" BorderStyle="None" CssClass="btn btn-danger"  
                                         Font-Names="Verdana" Font-Size="12px"/>
									</div>
						</div>
					</div>
				</div></div>
				</div>
    </form>
</body>
</html>
</asp:Content>