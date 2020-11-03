<%@ Page Language="vb" MasterPageFile="~/Recruit.Master" AutoEventWireup="true" CodeBehind="CandidateProfile.aspx.vb"
    Inherits="GOSHRM.CandidateProfile" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <style type="text/css">
            .modal
            {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
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
        </style>
         <script type="text/javascript" language="javascript">
             //    Grid View Check box
             function CheckAllEmp(Checkbox) {
                 var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
                 for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                     GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                 }
             }
            </script>

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
      
    </head>
    <body>
        <form id="form1" action="">
                <div class="container col-md-12">
                   <div class="row">
                     <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Height="16px" Width="6px"
                                Visible="False"></asp:TextBox>
                         <asp:TextBox ID="Star" runat="server" Font-Size="1px" Height="16px" Width="6px"
                                Visible="False"></asp:TextBox>

                    </div>
                 </div>
               <div class="panel panel-info">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">CANDIDATE'S PROFILE</b></h5>
                        </div>
                     <div class="panel-body">  
                    <div id="divpwd" runat="server" class="panel panel-success">
                        <div class="panel-heading">
                            <b>CREATE A PASSWORD</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PASSWORD*</label>
                                        <input id="apwd" runat="server" class="form-control" type="password" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            CONFIRM PASSWORD*</label>
                                        <input id="aconfirmpwd" runat="server" class="form-control" type="password" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row"><div class ="col-md-10">

                                     </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4"></div><div class="col-md-6">
                                <asp:Image ID="imgprofile" runat="server" CssClass="avatar" ImageUrl="~/images/logo.png"
                                    Height="120px" Width="120px" style="text-align :right" /></div> 
                                <input id="imguploads" runat="server" class="form-control" type="file" /></div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Image ID="imgClear" runat="server" ImageUrl="~/images/logo.png" Height="10px"
                                    Width="10px" Visible="False" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-4">
                            <div class="form-group">
                                <label>
                                    LAST NAME*</label>
                                <input id="alastname" runat="server" readonly class="form-control" type="text" placeholder="Surname" />
                            </div>
                        </div>
                        <div class=" col-md-8">
                            <div class="form-group">
                                <label>
                                    OTHER NAMES*</label>
                                <input id="aothername" runat="server" readonly  class="form-control" type="text" placeholder="First Name Middle Name" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    GENDER*</label>
                                <telerik:RadComboBox ID="cbogender" runat="server" ForeColor="#666666" Width="100%"
                                    RenderMode="Lightweight" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    MARITAL STATUS*</label>
                                <telerik:RadComboBox ID="cbomarital" runat="server" ForeColor="#666666" Width="100%"
                                    RenderMode="Lightweight" Skin="Bootstrap" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    CONTACT NUMBER*</label>
                                <input id="aphonenumber" runat="server" class="form-control" type="text" placeholder="Phone Number" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    EMAIL ADDRESS*</label>
                                <input id="aemailadd" runat="server" class="form-control" type="text" readonly placeholder="Personal Email Address" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    HOME ADDRESS*</label>
                                <textarea id="aaddress" runat="server" class="form-control" rows="4" cols="1" placeholder="Residential Address"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    CITY*</label>
                                <input id="acity" runat="server" class="form-control" type="text" placeholder="City or Town" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    STATE*</label>
                                <input id="astate" runat="server" class="form-control" type="text" placeholder="State or Province" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    NATIONALITY*</label>
                                <telerik:RadComboBox ID="cbonationality" runat="server" ForeColor="#666666" Width="100%"
                                    RenderMode="Lightweight" Skin="Bootstrap" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    COUNTRY*</label>
                                <telerik:RadComboBox ID="cbocountry" runat="server" ForeColor="#666666" Width="100%"
                                    RenderMode="Lightweight" Skin="Bootstrap" Filter="Contains">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    DATE OF BIRTH*</label>
                                <telerik:RadDatePicker ID="datDOB" runat="server" Width="100%" ForeColor="#666666"
                                    MinDate="1950-01-01" Skin="Bootstrap">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%">
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
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    AREA OF SPECIALISATION*</label>
                                <telerik:RadComboBox ID="cboField" runat="server" RenderMode="Lightweight" ForeColor="#666666"
                                    Width="100%" Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    EDUCATION LEVEL*</label>
                                <telerik:RadComboBox ID="cboEducation" runat="server" RenderMode="Lightweight" ForeColor="#666666"
                                    Width="100%" Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    GRADE OBTAINED*</label>
                                <telerik:RadComboBox ID="cbograde" runat="server" RenderMode="Lightweight" ForeColor="#666666"
                                    Width="100%" Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    DISCIPLINE*</label>
                                <telerik:RadComboBox ID="cboDiscipline" runat="server" RenderMode="Lightweight" ForeColor="#666666"
                                    Width="100%" Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    YEARS OF EXPERIENCE*</label>
                                <input id="aexpyears" runat="server" class="form-control" type="text" placeholder="Years of Professional Experience" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    SKILLS*</label>
                                <telerik:RadComboBox ID="cboSkills" runat="server" AutoPostBack="True" ForeColor="#666666"
                                    CheckBoxes="True" Width="100%" Skin="Bootstrap" Filter="Contains" RenderMode="Lightweight">
                                </telerik:RadComboBox>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadListBox ID="lstskills" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                            Enabled="False" Width="100%" EmptyMessage="No data" RenderMode="Lightweight"
                                            Sort="Ascending" Font-Names="Verdana" Font-Size="12px" Skin="Bootstrap">
                                            <ButtonSettings TransferButtons="All"></ButtonSettings>
                                            <EmptyMessageTemplate>
                                                No Skills
                                            </EmptyMessageTemplate>
                                        </telerik:RadListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboSkills" EventName="ItemChecked" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    LANGUAGES*</label>
                                <telerik:RadComboBox ID="cboLanguage" runat="server" AutoPostBack="True" CheckBoxes="True"
                                    ForeColor="#666666" Width="100%" Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                                </telerik:RadComboBox>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadListBox ID="lstLang" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                            Enabled="False" Width="100%" EmptyMessage="No data" RenderMode="Lightweight"
                                            Sort="Ascending" Font-Names="Verdana" Font-Size="12px" Skin="Bootstrap">
                                            <ButtonSettings TransferButtons="All"></ButtonSettings>
                                            <EmptyMessageTemplate>
                                                No Languages
                                            </EmptyMessageTemplate>
                                        </telerik:RadListBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cboLanguage" EventName="ItemChecked" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    COVER LETTER*</label>
                                <input class="form-control" type="file" id="filecoverletter" runat="server" />
                                <button id="btcoverletter" runat="server" type="submit" onserverclick="lblcoverletter_Click"
                                        style="width: 150px" class="btn btn-link">
                                        <i class="fa fa-download"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    RESUME*</label>
                                <input class="form-control" type="file" id="fileresume" runat="server" />
                                <button id="btresume" runat="server" type="submit" onserverclick="lblcv_Click"
                                        style="width: 150px" class="btn btn-link">
                                        <i class="fa fa-download"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    CERTIFICATE*</label>
                                <input class="form-control" type="file" id="filecertificate" runat="server" />
                                <button id="btcertificate" runat="server" type="submit" onserverclick="lblcertificate_Click"
                                        style="width: 150px" class="btn btn-link">
                                        <i class="fa fa-download"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>REFEREES</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            1. NAME</label>
                                        <input id="arefname1" runat="server" class="form-control" type="text" placeholder="Full Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            ORGANISATION</label>
                                        <textarea id="arefaddr1" runat="server" class="form-control" rows="4" cols="1" placeholder="Place Work and Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            POSITION
                                        </label>
                                        <input id="arefposition1" runat="server" class="form-control" type="text" placeholder="Position at work" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="arefphone1" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EMAIL ADDRESS
                                        </label>
                                        <input id="arefemail1" runat="server" class="form-control" type="text" placeholder="Personal or Official Email Address" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            YEARS KNOWN</label>
                                        <input id="arefyears1" runat="server" class="form-control" type="text" placeholder="Number of years known to the applicant" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            2. NAME</label>
                                        <input id="arefname2" runat="server" class="form-control" type="text" placeholder="Full Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            ORGANISATION</label>
                                        <textarea id="arefaddr2" runat="server" class="form-control" rows="4" cols="1" placeholder="Place Work and Address"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            POSITION
                                        </label>
                                        <input id="arefposition2" runat="server" class="form-control" type="text" placeholder="Position at work" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            PHONE NUMBER</label>
                                        <input id="arefphone2" runat="server" class="form-control" type="text" placeholder="Contact Number" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            EMAIL ADDRESS
                                        </label>
                                        <input id="arefemail2" runat="server" class="form-control" type="text" placeholder="Personal or Official Email Address" />
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            YEARS KNOWN</label>
                                        <input id="arefyears2" runat="server" class="form-control" type="text" placeholder="Number of years known to the applicant" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id = "divschool" runat="server" class="panel panel-success">
                        <div class="panel-heading">
                            <b>SCHOOL LEAVING GRADES</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" type="submit" onserverclick="btnAddResult_Click"
                                        style="width: 150px" class="btn btn-info" title="add subject with grade" >
                                        <i class="fa fa-plus"></i>Add</button>
                                     <button id="btdelete" runat="server" type="submit" onserverclick="btndelete_Click"
                                        style="width: 150px" class="btn btn-danger" title="delete subject" >
                                        <i class="fa fa-remove"></i>Delete</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridVwHeaderChckbox" runat="server" 
                                            BorderStyle="Solid" Font-Names="Verdana" PageSize="30" DataKeyNames="id" Width="100%"
                                            Height="50px" ToolTip="click row to select record" Font-Size="12px" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data to display" AutoGenerateColumns="False" GridLines="Both"
                                            ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC" CssClass="table table-condensed">
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp" runat="server" AutoPostBack="false"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                <asp:BoundField DataField="subject" HeaderText="Subject" SortExpression="subject" />
                                                <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                            </Columns>
                                            <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                        </asp:GridView>
                                        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                        <script type="text/javascript">
                                            $(function () {
                                                $("[id*=GridVwHeaderChckbox] td").hover(function () {
                                                    $("td", $(this).closest("tr")).addClass("hover_row");
                                                }, function () {
                                                    $("td", $(this).closest("tr")).removeClass("hover_row");
                                                })
                                            })
                                        </script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                     <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btsave" runat="server" type="submit" onserverclick="btnSend_Click"
                                        style="width: 150px" class="btn btn-success" >
                                        <i class="fa fa-save"></i>Save Profile</button>
                                     
                                </div>
                            </div>
                            </div></div></div>        
        </form>
        <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="~/images/loaders.gif" alt="" />
    </div>
    </body>
    </html>
</asp:Content>
