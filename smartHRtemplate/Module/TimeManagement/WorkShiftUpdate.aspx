<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="WorkShiftUpdate.aspx.vb" Inherits="GOSHRM.WorkShiftUpdate" EnableEventValidation="false" Debug="true"%>
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
    <style type="text/css">
        .style1
        {
            color: #FDFDFD;
            font-family: Candara;
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
            width: 180px;
            color: #FF0000;
        }
        .style6
        {
            width: 180px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 502px;
        }
        </style>

     
</head>



<body  onunload="window.opener.location=window.opener.location;" style="">
    <form>
    <%--<textarea cols = onkeyup="setTextBoxHeight();" onkeydown="setTextBoxHeight();" ></textarea>--%><%--<script type="text/javascript">
        function closeme() {
            window.close();
        }
        window.onblur = closeme;
    </script>--%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>

    
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <div class="container col-md-8">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                </div>
                    <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server">Work Shift</b></h5>
                    </div>
                 <div class="panel-body">
                    <form action="">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Shift Name*</label>
                                <input id="txtShiftName" runat="server" class="form-control" type="text" />
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
                                                     <telerik:RadComboBox ID="radHourEnd" runat="server" ResolvedRenderMode="Classic"
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
                                                    <telerik:RadComboBox ID="radMinEnd" runat="server" ResolvedRenderMode="Classic"
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
                                                    <telerik:RadComboBox ID="radTimeEnd" runat="server" ResolvedRenderMode="Classic"
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
                                    <input type="text" id="lblDays" runat="server" readonly="" class="form-control">
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    Required Fields*</label>
                                    <input type="text" id="lblDays1" runat="server" readonly="" class="form-control">
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
                    </form>
            </div>
        </div></div>
        </div>
    </div>
    </form>

   
</body>
</html>
</asp:Content>