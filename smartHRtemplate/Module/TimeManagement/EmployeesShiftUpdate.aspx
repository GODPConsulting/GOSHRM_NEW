<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="EmployeesShiftUpdate.aspx.vb" Inherits="GOSHRM.EmployeesShiftUpdate" EnableEventValidation="false" Debug="true"%>
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

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div> </div>
                <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Employee Shifts</b></h5>
                </div>
             <div class="panel-body">
                    <div class="row">
                    <div class=" col-md-12">
                            <div class="form-group">
                                <label></label>
                                <input id="lblcompany" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Employee *</label>
                                <telerik:RadComboBox runat="server" Skin="Bootstrap" ResolvedRenderMode="Classic" 
                                    Font-Names="Verdana" Font-Size="12px" ID="cboEmployee" Filter="Contains" 
                                    Width="100%" ForeColor="#666666">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Shift Name*</label>
                                <telerik:RadDropDownList ID="radShift" Skin="Bootstrap" runat="server" DefaultMessage="-- Select --"
                                    Font-Names="Verdana" Font-Size="12px" Width="100%" 
                                    ResolvedRenderMode="Classic" ForeColor="#666666" ToolTip="enabled overtime payment" AutoPostBack="true" >
                                </telerik:RadDropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                                            <label class="control-label">From*</label>
											<div class="form-group col-md-12">												
                                                  <telerik:RadDatePicker Skin="Bootstrap" ID="radStartDate" runat="server" 
                                                    Width="100%" RenderMode="Lightweight" Font-Names="Verdana" 
                                                    Font-Size="12px" ForeColor="#666666">
                                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                                    </Calendar>
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                                                        LabelWidth="40%" RenderMode="Lightweight">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                </telerik:RadDatePicker>            
											</div>
										</div>
                                        <div id="lblDateTo" runat="server" class="col-md-12">
                                            <label class="control-label">To*</label>radEndDate
											<div class="form-group col-md-12">												                                              
                                                <telerik:RadDatePicker Skin="Bootstrap" ID="radEndDate" runat="server" 
                                                    Width="100%" RenderMode="Lightweight" Font-Names="Verdana" 
                                                    Font-Size="12px" ForeColor="#666666">
                                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight">
                                                    </Calendar>
                                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" 
                                                        LabelWidth="40%" RenderMode="Lightweight">
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                        <FocusedStyle Resize="None"></FocusedStyle>
                                                        <DisabledStyle Resize="None"></DisabledStyle>
                                                        <InvalidStyle Resize="None"></InvalidStyle>
                                                        <HoveredStyle Resize="None"></HoveredStyle>
                                                        <EnabledStyle Resize="None"></EnabledStyle>
                                                    </DateInput>
                                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                </telerik:RadDatePicker>                                             
											</div>
										</div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                    <ContentTemplate>
                                <label>
                                    Duration (Hours)</label>
                                    <input type="text" id="lblDays" runat="server" class="form-control"/>
                                        </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="radShift" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                        
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    Required Fields*</label>
                                    <input type="text" id="lblDays1" runat="server" class="form-control">
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:CheckBox ID="chkCurrent" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="12px" Text="Is Current Shift" />
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
            </div></div>
    </div>
    </form>

   
</body>
</html>
</asp:Content>