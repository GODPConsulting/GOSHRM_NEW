<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="WorkShiftExemptionUpdate.aspx.vb" Inherits="GOSHRM.WorkShiftExemptionUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>


     
</head>



<body>
    <form>

        <div class="container col-md-8">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
             </div>
               <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Work Shift Exemptions</b></h5>
                </div>
             <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Shift Name*</label>
                                <telerik:RadDropDownList runat="server" 
                                DefaultMessage="-- Select --" DropDownHeight="100px"
                                            RenderMode="Lightweight" ResolvedRenderMode="Classic" 
                                BackColor="White" Font-Names="Verdana" Skin="Bootstrap"
                                            Font-Size="12px" Width="100%" ID="cboShiftname"
                                            AutoPostBack="True" ForeColor="#666666">
                                        </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Job Grade*</label>
                                <telerik:RadDropDownList runat="server" 
                                DefaultMessage="-- Select --" DropDownHeight="100px"
                                            RenderMode="Lightweight" ResolvedRenderMode="Classic" 
                                BackColor="White" Font-Names="Verdana" Skin="Bootstrap"
                                            Font-Size="12px" Width="100%" ID="cboJobGrade"
                                            AutoPostBack="True" ForeColor="#666666">
                                        </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                                            <label class="control-label">From*</label>
											<div class="form-group col-md-12">												
												<%--<input type="text" class="form-control datetimepicker">--%>
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                     <telerik:RadComboBox ID="radHourStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="1" Value="1" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2" Value="2" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3" Value="3" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4" Value="4" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5" Value="5" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="6" Value="6" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="7" Value="7" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="8" Value="8" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="9" Value="9" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="10" Value="10" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="11" Value="11" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="12" Value="12" Owner="radHourStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                              
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radMinStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="00" Value="00" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="15" Value="15" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="30" Value="30" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="45" Value="45" Owner="radMinStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                                
                                                 <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radTimeStart" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" Owner="radTimeStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" Owner="radTimeStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                               
											</div>
										</div>
                        <div class="col-md-12">
                                            <label class="control-label">To*</label>
											<div class="form-group col-md-12">												
												<%--<input type="text" class="form-control datetimepicker">--%>
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                     <telerik:RadComboBox ID="radHourStart0" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="1" Value="1" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2" Value="2" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3" Value="3" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4" Value="4" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5" Value="5" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="6" Value="6" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="7" Value="7" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="8" Value="8" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="9" Value="9" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="10" Value="10" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="11" Value="11" Owner="radHourStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="12" Value="12" Owner="radHourStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                              
                                                <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radMinStart0" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="00" Value="00" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="15" Value="15" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="30" Value="30" Owner="radMinStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="45" Value="45" Owner="radMinStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                                
                                                 <div class="col-md-4 col-sm-4 col-xs-12">
                                                    <telerik:RadComboBox ID="radTimeStart0" runat="server" ResolvedRenderMode="Classic"
                                                   Width="100%" Skin="Bootstrap" Font-Names="Verdana" ForeColor="#666666">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" Owner="radTimeStart" />
                                                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" Owner="radTimeStart" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                </div>                                               
											</div>
										</div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    Duration (Hours)</label>
                                    <input type="text" id="lblDays" runat="server" class="form-control">
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

    </form>

   
</body>
</html>
</asp:Content>