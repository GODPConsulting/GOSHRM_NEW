
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="JobTestInviteUpdate.aspx.vb" Inherits="GOSHRM.JobTestInviteUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>

    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

	function cboCandidates_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("Button3").click();
	}
//]]>
</script>
    <script type="text/javascript" id="telerikClientEvents2">
//<![CDATA[

	function cboInterviewers_DropDownClosing(sender,args)
	{
	    //Add JavaScript handler code here
	    document.getElementById("Button4").click();
	}
//]]>
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
                    <asp:Button ID="Button3" runat="server" BackColor="White" BorderStyle="None" 
                    Font-Size="9px" />                         
                    <asp:Button ID="Button4" runat="server" BackColor="White" BorderStyle="None" 
                    Font-Size="9px" />
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lbltestid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lbljobid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblJobPost" runat="server" Font-Names="Verdana" 
                    Font-Size="20px"></asp:Label>
                    <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                     <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#666666" Font-Size="11px"
                    Text="Candidates Notified:" Font-Names="Verdana" 
                    Style="font-style: italic"></asp:Label>
                    <asp:Label ID="lblc" runat="server" Width="50px" 
                    ForeColor="#666666" Font-Size="11px"
                    Font-Names="Verdana" Style="font-style: italic"></asp:Label>
                <asp:Label ID="lblcandidatedate" runat="server" Width="150px" 
                    ForeColor="#666666" Font-Size="11px"
                    Font-Names="Verdana" Style="font-style: italic"></asp:Label>
            </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">head</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Job Test*:</label>
                                <input id="lblJobTest" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Date*:</label>
                                <telerik:RadDatePicker ID="dateInterview" runat="server" Culture="en-US" MinDate="" ForeColor="#666666"
                                    ResolvedRenderMode="Classic" RenderMode="Lightweight" Width="100%" Skin="Bootstrap" Font-Names="Verdana" 
                                    Font-Size="13px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" EnableKeyboardNavigation="True" 
                                        RenderMode="Lightweight">
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
                                <label>Time:</label>
                                <telerik:RadComboBox ID="radHourStart0" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                ResolvedRenderMode="Classic" Width="30%" RenderMode="Lightweight" 
                                 Font-Names="Verdana" Font-Size="12px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="1" Value="1" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="2" Value="2" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="3" Value="3" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="4" Value="4" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="5" Value="5" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="6" Value="6" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="7" Value="7" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="8" Value="8" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="9" Value="9" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="10" Value="10" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="11" Value="11" 
                                        Owner="radHourStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="12" Value="12" 
                                        Owner="radHourStart" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:Label ID="Label20" runat="server" Font-Names="Candara" Visible="False" 
                                Font-Italic="True">:</asp:Label>
                            <telerik:RadComboBox ID="radMinStart0" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                ResolvedRenderMode="Classic" Width="30%" RenderMode="Lightweight" 
                                 Font-Names="Verdana" Font-Size="12px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="00" Value="00" 
                                        Owner="radMinStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="15" Value="15" 
                                        Owner="radMinStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="30" Value="30" 
                                        Owner="radMinStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="45" Value="45" 
                                        Owner="radMinStart" />                        
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="radTimeStart0" Skin="Bootstrap" Runat="server" ForeColor="#666666"
                                ResolvedRenderMode="Classic" Width="30%" RenderMode="Lightweight" 
                                 Font-Names="Verdana" Font-Size="12px">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" 
                                        Owner="radTimeStart" />
                                    <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" 
                                        Owner="radTimeStart" />                 
                                </Items>
                            </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    Candidates:</label>
                                <telerik:RadComboBox Skin="Bootstrap" ID="cboCandidates" runat="server" CheckBoxes="True" Filter="Contains" ForeColor="#666666"
                                    AutoPostBack="True" EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight"
                                    Width="100%" Sort="Ascending">
                                </telerik:RadComboBox>
                                <br />
                                <telerik:RadListBox Skin="Bootstrap" ID="lstCandidates" runat="server" ForeColor="#666666"
                                    ResolvedRenderMode="Classic" BorderStyle="None"
                                    Enabled="False" Width="100%" EmptyMessage="No Candidates" 
                                    RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                                    Font-Size="12px">
                                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                                    <EmptyMessageTemplate>
                                        No Candidates
                                    </EmptyMessageTemplate>
                                </telerik:RadListBox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>Venue*:</label>
                                <input id="txtVenue" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Created By:</label>
                                <input id="lblCreateBy" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Created On:</label>
                               <input id="lblCreateOn" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Updated By:</label>
                               <input id="lblUpdatedBy" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-3">
                            <div class="form-group">
                                <label>
                                    Updated On:</label>
                               <input id="lblUpdatedOn" readonly runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnAdd" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                                <asp:Button ID="btnInvite" runat="server" Text="Send Invite to Candidates" 
                            ForeColor="White" CssClass="btn btn-primary btn-info"
                            Width="200px" Height="35px" BorderStyle="None" Font-Names="Verdana" 
                            Font-Size="12px" />
                            <button id="btnCancel" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
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
                &nbsp;<asp:Label ID="lblJobPost" runat="server" Font-Names="Verdana" 
                    Font-Size="20px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="lblid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lbltestid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
                <asp:Label ID="lbljobid" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label1" runat="server" Text="Job Test:" Font-Names="Verdana" 
                    Font-Size="12px" Font-Bold="True" ForeColor="#666666"></asp:Label>
            </td>
            <td class="style8">
                <asp:Label ID="lblJobTest" runat="server" Font-Names="Verdana" 
                    Font-Size="12px" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label2" runat="server" Text="Date:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Font-Names="Verdana" ></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadDatePicker ID="dateInterview" runat="server" Culture="en-US" MinDate="" ForeColor="#666666"
                    ResolvedRenderMode="Classic" RenderMode="Lightweight" Font-Names="Verdana" 
                    Font-Size="13px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                        FastNavigationNextText="&amp;lt;&amp;lt;" EnableKeyboardNavigation="True" 
                        RenderMode="Lightweight">
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
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label4" runat="server" Text="Time:" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                    Font-Names="Verdana" ></asp:Label>
            </td>
            <td class="style8">
                 <telerik:RadComboBox ID="radHourStart0" Runat="server" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Width="80px" RenderMode="Lightweight" 
                     Font-Names="Verdana" Font-Size="12px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="1" Value="1" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="2" Value="2" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="3" Value="3" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="4" Value="4" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="5" Value="5" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="6" Value="6" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="7" Value="7" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="8" Value="8" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="9" Value="9" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="10" Value="10" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="11" Value="11" 
                            Owner="radHourStart" />
                        <telerik:RadComboBoxItem runat="server" Text="12" Value="12" 
                            Owner="radHourStart" />
                    </Items>
                </telerik:RadComboBox>
                <asp:Label ID="Label20" runat="server" Font-Names="Candara" Visible="False" 
                    Font-Italic="True">:</asp:Label>
                <telerik:RadComboBox ID="radMinStart0" Runat="server" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Width="80px" RenderMode="Lightweight" 
                     Font-Names="Verdana" Font-Size="12px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="00" Value="00" 
                            Owner="radMinStart" />
                        <telerik:RadComboBoxItem runat="server" Text="15" Value="15" 
                            Owner="radMinStart" />
                        <telerik:RadComboBoxItem runat="server" Text="30" Value="30" 
                            Owner="radMinStart" />
                        <telerik:RadComboBoxItem runat="server" Text="45" Value="45" 
                            Owner="radMinStart" />                        
                    </Items>
                </telerik:RadComboBox>
                <telerik:RadComboBox ID="radTimeStart0" Runat="server" ForeColor="#666666"
                    ResolvedRenderMode="Classic" Width="80px" RenderMode="Lightweight" 
                     Font-Names="Verdana" Font-Size="12px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="AM" Value="AM" 
                            Owner="radTimeStart" />
                        <telerik:RadComboBoxItem runat="server" Text="PM" Value="PM" 
                            Owner="radTimeStart" />                 
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style9" valign="top">
                <asp:Label ID="Label6" runat="server" Text="Candidates:" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                    Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <telerik:RadComboBox ID="cboCandidates" runat="server" CheckBoxes="True" Filter="Contains" ForeColor="#666666"
                    AutoPostBack="True" EnableCheckAllItemsCheckBox="True" RenderMode="Lightweight"
                    Width="100%" Sort="Ascending" 
                    onclientdropdownclosing="cboCandidates_DropDownClosing">
                </telerik:RadComboBox>
                <br />
                <telerik:RadListBox ID="lstCandidates" runat="server" ForeColor="#666666"
                    ResolvedRenderMode="Classic" BorderStyle="None"
                    Enabled="False" Width="100%" EmptyMessage="No Candidates" 
                    RenderMode="Lightweight" Sort="Ascending" Font-Names="Verdana" 
                    Font-Size="12px">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                    <EmptyMessageTemplate>
                        No Candidates
                    </EmptyMessageTemplate>
                </telerik:RadListBox>
            </td>
        </tr>
        <tr>
            <td class="style9" valign="top">
                <asp:Label ID="Label3" runat="server" Text="Venue:"  Font-Bold="True" ForeColor="#666666"
                    Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style8">
                <asp:TextBox ID="txtVenue" runat="server" Height="100px" TextMode="MultiLine" ForeColor="#666666"
                    Width="100%" BorderColor="#CCCCCC" BorderWidth="1px" Font-Size="12px" 
                    Font-Names="Verdana"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="style6">
                <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#666666" Font-Size="11px"
                    Text="Candidates Notified:" Font-Names="Verdana" 
                    Style="font-style: italic"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="lblc" runat="server" Width="50px" 
                    ForeColor="#666666" Font-Size="11px"
                    Font-Names="Verdana" Style="font-style: italic"></asp:Label>

                <asp:Label ID="lblcandidatedate" runat="server" Width="150px" 
                    ForeColor="#666666" Font-Size="11px"
                    Font-Names="Verdana" Style="font-style: italic"></asp:Label>
            </td>
        </tr>
            <tr>
            <td class="style6" >
               
            </td>
            <td >
              
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label7" runat="server" Text="Created By:" Font-Bold="True" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    
            </td>
            <td class="style5">
                
                <asp:Label ID="lblCreateBy" runat="server" Width="100%" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label9" runat="server" Text="Created On:" Font-Bold="True" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style5">
                
                <asp:Label ID="lblCreateOn" runat="server" Width="100%" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label10" runat="server" Text="Updated By:" Font-Bold="True" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style5">
               
                <asp:Label ID="lblUpdatedBy" runat="server" Width="100%" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label11" runat="server" Text="Updated On:" Font-Bold="True" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
            </td>
            <td class="style5">
                
                <asp:Label ID="lblUpdatedOn" runat="server" Width="100%" ForeColor="#666666"
                    style="font-style: italic" Font-Names="Verdana" Font-Size="12px"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="style2" style="border-bottom-style: solid; border-bottom-width: thin;
                border-bottom-color: #C0C0C0">
                &nbsp;</td>
            <td style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0"
                class="style8">
            </td>
        </tr>
        <tr>
            <td class="style6">
            </td>
            <td class="style8">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="12px"
                    ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="btnInvite" runat="server" Text="Send Invite to Candidates" 
                    BackColor="#3399FF" ForeColor="White"
                    Width="200px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
            </td>
            <td class="style8">
                <table width="100%">
                    <tr>
                        <td style="width:35%">
                <asp:Button ID="btnAdd" runat="server" Text="Save" 
                    BackColor="#1BA691" ForeColor="White"
                    Width="200px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px"/>
                        </td>
                        <td style="width:65%">
                <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                    Width="200px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px"/>
                        </td>
                    </tr>
                </table>
            </td>
            
          
        </tr>
        <tr>
            <td class="style6">
                
    <asp:Button ID="Button3" runat="server" BackColor="White" BorderStyle="None" 
                    Font-Size="9px" />                         
    <asp:Button ID="Button4" runat="server" BackColor="White" BorderStyle="None" 
                    Font-Size="9px" />
                
            </td>
            
          
        </tr>
    </table>--%>
     <p>
         &nbsp;</p>
    </form>
</body>
</html>
</asp:Content>