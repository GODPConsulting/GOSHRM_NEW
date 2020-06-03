<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false"
    CodeBehind="EmployeeData.aspx.vb" Inherits="GOSHRM.EmployeeData" EnableEventValidation="false"
    Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Employee Detail</title>
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
            <script type="text/javascript" language="javascript">
                //    Grid View Dependants
                function CheckAllEmpDep(Checkbox) {
                    var GridVwHeaderChckbox = document.getElementById("<%=GridVwHeaderChckbox.ClientID %>");
                    for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                        GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Language
                function CheckAllEmpLang(Checkbox) {
                    var GridVwLang = document.getElementById("<%=GridVwLang.ClientID %>");
                    for (i = 1; i < GridVwLang.rows.length; i++) {
                        GridVwLang.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Skills
                function CheckAllEmpSkills(Checkbox) {
                    var GridVwSkills = document.getElementById("<%=GridVwSkills.ClientID %>");
                    for (i = 1; i < GridVwSkills.rows.length; i++) {
                        GridVwSkills.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Education
                function CheckAllEmpEdu(Checkbox) {
                    var GridVwEducation = document.getElementById("<%=GridVwEducation.ClientID %>");
                    for (i = 1; i < GridVwEducation.rows.length; i++) {
                        GridVwEducation.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Certificate
                function CheckAllEmpCert(Checkbox) {
                    var GridVwCertification = document.getElementById("<%=GridVwCertification.ClientID %>");
                    for (i = 1; i < GridVwCertification.rows.length; i++) {
                        GridVwCertification.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script type="text/javascript" language="javascript">
                //    Grid View Work History
                function CheckAllWorkHist(Checkbox) {
                    var GridVwWorkHistory = document.getElementById("<%=GridVwWorkHistory.ClientID %>");
                    for (i = 1; i < GridVwWorkHistory.rows.length; i++) {
                        GridVwWorkHistory.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
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
            <script type="text/javascript">
                function ConfirmApprove() {
                    var confirmapprove_value = document.createElement("INPUT");
                    confirmapprove_value.type = "hidden";
                    confirmapprove_value.name = "confirmapprove_value";
                    if (confirm("Approve the update?")) {
                        confirmapprove_value.value = "Yes";
                    } else {
                        confirmapprove_value.value = "No";
                    }
                    document.forms[0].appendChild(confirmapprove_value);
                }
            </script>
            <script type="text/javascript">
                function ConfirmCancel() {
                    var confirmcancel_value = document.createElement("INPUT");
                    confirmcancel_value.type = "hidden";
                    confirmcancel_value.name = "confirmcancel_value";
                    if (confirm("Cancel the update, cancellation cannot be reverted?")) {
                        confirmcancel_value.value = "Yes";
                    } else {
                        confirmcancel_value.value = "No";
                    }
                    document.forms[0].appendChild(confirmcancel_value);
                }
            </script>
        </telerik:RadScriptBlock>
        <style type="text/css">
            td {
                cursor: pointer;
            }

            .modal {
                position: fixed;
                z-index: 999;
                height: 100%;
                width: 100%;
                top: 0;
                background-color: Black;
                filter: alpha(opacity=60);
                opacity: 0.6;
                -moz-opacity: 0.8;
            }

            .center {
                z-index: 1000;
                margin: 250px auto;
                padding: 10px;
                width: 130px;
                background-color: White;
                border-radius: 10px;
                filter: alpha(opacity=100);
                opacity: 1;
                -moz-opacity: 1;
            }

            .style84 {
                width: 166px;
            }
        </style>
    </head>
    <body>
        <form id="form2" action="">
            <div>
                <div class="content container-fluid">

                    <div class="row">
                        <div id="divalert" runat="server" visible="false" class="alert alert-info">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                                id="msgalert" runat="server">Danger!</strong>
                            <asp:TextBox ID="txtID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtIDContact" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtIDEmergency" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btpersonal" runat="server" onserverclick="btnPersonal_Click" type="submit"
                                class="btn btn-default">
                                Personal Detail</button>
                            <button id="btemercontact" runat="server" onserverclick="btnEmergency_Click" type="submit"
                                class="btn btn-default">
                                Contacts & Referees</button>
                            <button id="btdependants" runat="server" onserverclick="btnDependants_Click" type="submit"
                                class="btn btn-default">
                                Dependants</button>
                            <button id="btqualification" runat="server" onserverclick="btnQualifications_Click"
                                type="submit" class="btn btn-default">
                                Qualifications</button>
                            <button id="btcareer" runat="server" onserverclick="btnWorkHistory_Click" type="submit"
                                class="btn btn-default">
                                Career</button>
                            <%-- <button id="Button5" runat="server" onserverclick="btnWorkHistory_Click" type="submit"
                                class="btn btn-default">
                                Employee Skills</button>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-8 col-md-8">
                            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Employee Profile</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div>
                                    <img id="imgphoto" runat="server" class="inline-block mb-10 img-circle" width="100" src="~/images/blank-avatar.jpg" onerror="this.onerror=null; this.src='~/images/blank-avatar.jpg';" alt="" />
                                    <div class="fileupload btn btn-default">
                                        <span class="btn-text">edit photo</span>
                                        <input id="imgUpload" runat="server" class="upload" type="file" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:CheckBox runat="server" Text="Suspend Employee Pay" Font-Names="Verdana" ID="chkPay"
                                AutoPostBack="True" Font-Size="13px" ToolTip="Payroll will be suspended for the employee"
                                Visible="False" ForeColor="#666666"></asp:CheckBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="PersonalDetail" runat="server">
                                    <div class="content container-fluid">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Basic Informations</h5>
                                            <div class="row">
                                                <div>
                                                    <div>
                                                        <div class="row">
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        EMPLOYEE ID</label>
                                                                    <input id="aempid" runat="server" class="form-control" type="text" placeholder="Employee ID" />
                                                                    <button type="submit" id="btgenerateempid" runat="server" onserverclick="btnRegenerate_Click" title="Generate Employee ID"><i class="fa fa-refresh"></i></button>
                                                                </div>
                                                            </div>

                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        LASTNAME</label>
                                                                    <input id="alastname" runat="server" class="form-control" type="text" placeholder="Surname" />
                                                                </div>
                                                            </div>
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        FIRSTNAME</label>
                                                                    <input id="afirstname" runat="server" class="form-control" type="text" placeholder="Firstname" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        MIDDLENAME</label>
                                                                    <input id="amiddlename" runat="server" class="form-control" type="text" placeholder="Middlename" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        GENDER</label>
                                                                    <select id="drpgender" runat="server" class="select form-control">
                                                                        <option value="Female">Female</option>
                                                                        <option value="Male">Male</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        DATE OF BIRTH</label>
                                                                    <div class="cal-icon">
                                                                        <telerik:RadDatePicker runat="server" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                            RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="adateofbirth"
                                                                            Skin="Bootstrap">
                                                                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                                Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                            </Calendar>
                                                                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                                RenderMode="Lightweight">
                                                                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                                <FocusedStyle Resize="None"></FocusedStyle>
                                                                                <DisabledStyle Resize="None"></DisabledStyle>
                                                                                <InvalidStyle Resize="None"></InvalidStyle>
                                                                                <HoveredStyle Resize="None"></HoveredStyle>
                                                                                <EnabledStyle Resize="None"></EnabledStyle>
                                                                            </DateInput>
                                                                            <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                        </telerik:RadDatePicker>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            </div>
                                                            <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        MARITAL STATUS</label>
                                                                    <select id="drpmaritalstatus" runat="server" class="select form-control">
                                                                        <option value="Single">Single</option>
                                                                        <option value="Married">Married</option>
                                                                        <option value="Divorced">Divorced</option>
                                                                        <option value="Widowed">Widowed</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        NATIONALITY</label>
                                                                    <select id="drpnationality" runat="server" class="select form-control">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        COUNTRY OF BIRTH</label>
                                                                    <select id="drpcountryofbirth" runat="server" class="select form-control">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                             </div>
                                                            <div class="row">
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        STATE OF ORIGIN</label>
                                                                    <input id="astateorigin" runat="server" class="form-control" type="text" placeholder="State of Origin" />
                                                                </div>
                                                            </div>
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        PLACE OF BIRTH</label>
                                                                    <input id="abirthplace" runat="server" class="form-control" type="text" placeholder="Place of Birth" />
                                                                </div>
                                                            </div>
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        INDENTIFICATION TYPE</label>
                                                                    <input id="aidtype" runat="server" class="form-control" type="text" placeholder="Type of Identication" />
                                                                </div>
                                                            </div>
                                                                </div>
                                                            <div class="row">
                                                            <div class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        IDENTIFICATION NUMBER</label>
                                                                    <input id="aidnumber" runat="server" class="form-control" type="text" placeholder="Identification Number" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        IDENTIFICATION ISSUER</label>
                                                                    <input id="aidissuer" runat="server" class="form-control" type="text" placeholder="Identification issuer" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        ID EXPIRY DATE</label>
                                                                    <div class="cal-icon">
                                                                        <%--<input id="aidexpirydate" runat="server" class="form-control datetimepicker" type="text" />--%>
                                                                        <telerik:RadDatePicker runat="server" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                            RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="aidexpirydate"
                                                                            Skin="Bootstrap">
                                                                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                                Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                            </Calendar>
                                                                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                                RenderMode="Lightweight">
                                                                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                                <FocusedStyle Resize="None"></FocusedStyle>
                                                                                <DisabledStyle Resize="None"></DisabledStyle>
                                                                                <InvalidStyle Resize="None"></InvalidStyle>
                                                                                <HoveredStyle Resize="None"></HoveredStyle>
                                                                                <EnabledStyle Resize="None"></EnabledStyle>
                                                                            </DateInput>
                                                                            <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                        </telerik:RadDatePicker>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                </div>
                                                                <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        RESUMPTION DATE</label>
                                                                    <div class="cal-icon">
                                                                        <%--<input id="adateresume" runat="server" class="form-control datetimepicker" type="text" />--%>
                                                                        <telerik:RadDatePicker runat="server" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                            RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="adateresume"
                                                                            Skin="Bootstrap">
                                                                            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                                Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                            </Calendar>
                                                                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                                RenderMode="Lightweight">
                                                                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                                <FocusedStyle Resize="None"></FocusedStyle>
                                                                                <DisabledStyle Resize="None"></DisabledStyle>
                                                                                <InvalidStyle Resize="None"></InvalidStyle>
                                                                                <HoveredStyle Resize="None"></HoveredStyle>
                                                                                <EnabledStyle Resize="None"></EnabledStyle>
                                                                            </DateInput>
                                                                            <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                        </telerik:RadDatePicker>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divconfirmexpected" runat="server" class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        EXPECTED CONFIRMATION DATE</label>
                                                                    <%--<input id="adateexpectconfirm" runat="server" class="form-control" type="text" disabled="disabled" />--%>
                                                                    <telerik:RadDatePicker runat="server" Enabled="false" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                        RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="adateexpectconfirm"
                                                                        Skin="Bootstrap">
                                                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                            Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                            RenderMode="Lightweight">
                                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                                        </DateInput>
                                                                        <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                    </telerik:RadDatePicker>
                                                                </div>
                                                            </div>
                                                            <div id="divconfirmdate" runat="server" class=" col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        CONFIRMATION DATE</label>
                                                                    <%--<input id="adateconfirm" runat="server" class="form-control" type="text" disabled="disabled" />--%>
                                                                    <telerik:RadDatePicker runat="server" Enabled="false" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                        RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="adateconfirm"
                                                                        Skin="Bootstrap">
                                                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                            Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                            RenderMode="Lightweight">
                                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                                        </DateInput>
                                                                        <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                    </telerik:RadDatePicker>
                                                                </div>
                                                            </div>
                                                             </div>
                                                                    <div class="row">
                                                            <div id="divterminatedate" visible="false" runat="server" class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        TERMINATION / RESIGNATION DATE</label>
                                                                    <%--<input id="aterminatedate" runat="server" class="form-control" type="text" disabled="disabled" />--%>
                                                                    <telerik:RadDatePicker runat="server" MinDate="1900-01-01" ForeColor="#666666" Culture="en-US"
                                                                        RenderMode="Lightweight" Width="100%" resolvedrendermode="Classic" ID="aterminatedate"
                                                                        Skin="Bootstrap">
                                                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight"
                                                                            Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%"
                                                                            RenderMode="Lightweight">
                                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                                        </DateInput>
                                                                        <DatePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                                    </telerik:RadDatePicker>
                                                                </div>
                                                            </div>
                                                            <div id="divlogcreate" runat="server" class="row">
                                                                <div class=" col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:CheckBox runat="server" AutoPostBack="True" Text="Create GOSHRM Login Profile"
                                                                            ForeColor="#666666" Font-Size="14px" ID="chklogcreate"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Contact Information</h5>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            PHONE NUMBER*
                                                        </label>
                                                        <input id="aphonenumber" runat="server" class="form-control" type="text" placeholder="Mobile / Telephone number" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            OFFICE PHONE NUMBER
                                                        </label>
                                                        <input id="aworkphonenumber" runat="server" class="form-control" type="text" placeholder="Official contact number" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            PERSONAL EMAIL ADDRESS</label>
                                                        <input id="aemailaddress" runat="server" class="form-control" type="text" placeholder="Email Address" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            OFFICE EMAIL ADDRESS</label>
                                                        <input id="aworkemailaddress" runat="server" class="form-control" type="text" placeholder="Official email address" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            HOME ADDRESS*</label>
                                                        <textarea id="aaddress" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            CITY*</label>
                                                        <input id="aaddresscity" runat="server" class="form-control" type="text" placeholder="City" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            COUNTRY*</label>
                                                        <select id="drpaddresscountry" runat="server" class="select form-control">
                                                        </select>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div id="divlogin" runat="server" class="card-box">
                                                    <h5 class="card-title" style="color: #1BA691">GOSHRM Credentials</h5>
                                                    <div class="row">
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    USERNAME*
                                                                </label>
                                                                <input id="ausername" runat="server" class="form-control" type="text" placeholder="Username" />
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    PASSWORD*</label>
                                                                <input id="apassword" runat="server" class="form-control" type="text" placeholder="Temporary password" />
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    USER ROLE*
                                                                </label>
                                                                <telerik:RadComboBox ID="cbouserrole" runat="server" CheckBoxes="False" Filter="Contains"
                                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    STATUS*</label>
                                                                <telerik:RadComboBox ID="cbouserstat" runat="server" CheckBoxes="False" Filter="Contains"
                                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    ACCESS*
                                                                </label>
                                                                <telerik:RadComboBox ID="cbouseraccess" runat="server" CheckBoxes="False" Filter="Contains"
                                                                    RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" AutoPostBack="true"
                                                                    Skin="Bootstrap">
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    ACCESS LEVEL*</label>
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                                                    <ContentTemplate>
                                                                        <telerik:RadComboBox ID="cbouseraccesslevel" runat="server" CheckBoxes="True" Filter="Contains"
                                                                            RenderMode="Lightweight" Width="100%" Sort="Ascending" ForeColor="#666666" Skin="Bootstrap">
                                                                        </telerik:RadComboBox>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="cbouseraccess" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    EMAIL ADDRESS*
                                                                </label>
                                                                <input id="auseremail" runat="server" class="form-control" type="text" placeholder="Email address" />
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-4">
                                                            <div class="form-group">
                                                                <input type="checkbox" id="chksuperadmin" runat="server" />Is Super Administrator
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-4">
                                                            <div class="form-group">
                                                                <input type="checkbox" id="chkhradmin" runat="server" />Is HR Administrator
                                                            </div>
                                                        </div>
                                                        <div class=" col-md-4">
                                                            <div class="form-group">
                                                                <input type="checkbox" id="chkfinadmin" runat="server" />Is Finance Administrator
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chklogcreate" EventName="CheckedChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="col-md-8 m-t-20">
                                            <button id="btsavepersonal" runat="server" onserverclick="btnAddPDetail_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-success">
                                                Save &amp; Update</button>
                                            <button id="btnback" runat="server" onserverclick="btnCancel_Click" type="submit"
                                                style="width: 150px" class="btn btn-info">
                                                << Back</button>
                                        </div>

                                    </div>


                                </asp:View>
                                <asp:View ID="Emergency" runat="server">
                                    <div id="collapse_acc" runat="server" class=" card-box">
                                        <div class="card-header">
                                            <h6><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" title="Click to view update and approve">Emergency Contact Updates
                                            </a></h6>
                                        </div>
                                        <div id="collapseTwo" class="collapse" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row" id="divbtnapprove" runat="server">
                                                    <div class="col-md-12 m-t-20 text-right">
                                                        <asp:LinkButton ID="lnkApprove" data-toggle="tooltip" data-original-title="Approve updates" Width="150px" runat="server" CssClass="btn btn-success" OnClientClick="ConfirmApprove()">
                    Approve <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-ok"></span>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="lnkCancel" data-toggle="tooltip" data-original-title="Cancel updates" Width="150px" runat="server" CssClass="btn btn-danger" OnClientClick="ConfirmCancel()">
                    Cancel <span style="margin-top:5px" aria-hidden="true" class="glyphicon glyphicon-minus-sign"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <b id="B1" runat="server">Update</b>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class=" col-md-6">
                                                                <div class="card-box">
                                                                    <h5 class="card-title" style="color: #1BA691">Emergency Contact 1</h5>
                                                                    <div class="row">
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    FULLNAME</label>
                                                                                <input id="tempaemername1" runat="server" disabled="disabled" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    CONTACT NUMBER</label>
                                                                                <input id="tempaemercontactnumber1" runat="server" disabled="disabled" class="form-control" type="text" placeholder="Phone number" />
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    RELATIONSHIP*</label>
                                                                                <select id="drptemprelation1" runat="server" disabled="disabled" class="select form-control">
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    HOME ADDRESS*</label>
                                                                                <textarea id="tempaemeraddress1" runat="server" disabled="disabled" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class=" col-md-6">
                                                                <div class="card-box">
                                                                    <h5 class="card-title" style="color: #1BA691">Emergency Contact 2</h5>
                                                                    <div class="row">
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    FULLNAME</label>
                                                                                <input id="tempaemername2" runat="server" disabled="disabled" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    CONTACT NUMBER</label>
                                                                                <input id="tempaemercontactnumber2" runat="server" disabled="disabled" class="form-control" type="text" placeholder="Phone number" />
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    RELATIONSHIP*</label>
                                                                                <select id="drptemprelation2" runat="server" disabled="disabled" class="select form-control">
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class=" col-md-12">
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    HOME ADDRESS*</label>
                                                                                <textarea id="tempaemeraddress2" runat="server" disabled="disabled" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                    <div class="row">
                                        <div class=" col-md-6">
                                            <div class="card-box">
                                                <h5 class="card-title" style="color: #1BA691">Emergency Contact 1</h5>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                FULLNAME</label>
                                                            <input id="aemername1" runat="server" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                CONTACT NUMBER</label>
                                                            <input id="aemercontactnumber1" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                RELATIONSHIP*</label>
                                                            <select id="drpemerrelation1" runat="server" class="select form-control">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                HOME ADDRESS*</label>
                                                            <textarea id="aemeraddress1" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class=" col-md-6">
                                            <div class="card-box">
                                                <h5 class="card-title" style="color: #1BA691">Emergency Contact 2</h5>
                                                <div class="row">
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                FULLNAME</label>
                                                            <input id="aemername2" runat="server" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                CONTACT NUMBER</label>
                                                            <input id="aemercontactnumber2" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                RELATIONSHIP*</label>
                                                            <select id="drprelation2" runat="server" class="select form-control">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class=" col-md-12">
                                                        <div class="form-group">
                                                            <label>
                                                                HOME ADDRESS*</label>
                                                            <textarea id="aemeraddress2" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Referee 1</h5>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            FULLNAME</label>
                                                        <input id="arefname1" runat="server" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            CONTACT NUMBER</label>
                                                        <input id="arefphonenumber1" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            EMAIL ADDRESS</label>
                                                        <input id="arefemailaddress1" runat="server" class="form-control" type="text" placeholder="Email address" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            YEARS KNOWN</label>
                                                        <input id="arefyrsknown1" runat="server" class="form-control" type="text" placeholder="Number of years" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            REFEREE CONFIRM? *</label>
                                                        <select id="drprefconfirmed1" runat="server" class="select form-control">
                                                            <option value="No">No</option>
                                                            <option value="Yes">Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            ORGANISATION*</label>
                                                        <textarea id="areforganisation1" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Referee 2</h5>
                                            <div class="row">
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            FULLNAME</label>
                                                        <input id="arefname2" runat="server" class="form-control" type="text" placeholder="Firstname Middlename Lastname" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            CONTACT NUMBER</label>
                                                        <input id="arefcontactnumber2" runat="server" class="form-control" type="text" placeholder="Phone number" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            EMAIL ADDRESS</label>
                                                        <input id="arefemailaddress2" runat="server" class="form-control" type="text" placeholder="Email address" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            YEARS KNOWN</label>
                                                        <input id="arefyears2" runat="server" class="form-control" type="text" placeholder="Number of years" />
                                                    </div>
                                                </div>
                                                <div class=" col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            REFEREE CONFIRM? *</label>
                                                        <select id="drprefconfirmed2" runat="server" class="select form-control">
                                                            <option value="No">No</option>
                                                            <option value="Yes">Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label>
                                                            ORGANISATION*</label>
                                                        <textarea id="areforganisation2" runat="server" class="form-control" rows="4" cols="1" placeholder="Home Address"></textarea>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8 m-t-20">
                                            <button id="btnsave" runat="server" onserverclick="btnSaveEmergency_Click" type="submit"
                                                style="width: 150px" class="btn btn-primary btn-success">
                                                Save &amp; Update</button>
                                            <button id="Button1" runat="server" onserverclick="btnCancel1_Click" type="submit"
                                                style="width: 150px" class="btn btn-info rounded">
                                                << Back</button>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="Dependants" runat="server">
                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Dependants</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnsavedependants" runat="server" onserverclick="btnAddDependents_Click" type="submit"
                                                        style="width: 150px" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeletedependants" runat="server" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" OnSorting="SortDependants" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmpDep(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                            <asp:TemplateField HeaderText="Name" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDependant" runat="server" Text='<%# Eval("Dependants")%>'
                                                                        CommandName="AddDependant" CausesValidation="false" CommandArgument='<%# Eval("id") %>'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" SortExpression="Relationship" />
                                                            <asp:BoundField DataField="Date of Birth" HeaderText="Date of Birth" SortExpression="Date of Birth" DataFormatString="{0:dd, MMM yyyy}" />
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
                                        <button id="Button2" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px" class="btn btn-info rounded">
                                            << Back</button>
                                    </div>
                                </asp:View>
                                <asp:View ID="Qualifications" runat="server">



                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Education</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnaddeducatin" runat="server" onserverclick="btnAddEducation_Click" type="submit"
                                                        style="width: 150px" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeleteeducation" runat="server" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwEducation" runat="server" OnSorting="SortEducation" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll2" runat="server" onclick="CheckAllEmpEdu(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmpEdu" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                            <asp:TemplateField HeaderText="Education" ItemStyle-Font-Bold="true" SortExpression="Qualification">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEducation" runat="server" Text='<%# Eval("Qualification")%>'
                                                                        CommandName="AddEducation" CausesValidation="false" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Institute" HeaderText="Institute" />
                                                            <asp:BoundField DataField="Start Date" HeaderText="Start Date" SortExpression="Start Date" />
                                                            <asp:BoundField DataField="Completed On" HeaderText="Completed On" SortExpression="Completed On" />
                                                            <asp:TemplateField HeaderText="Certificate" SortExpression="filename">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("id") %>'
                                                                        OnClick="DownloadEducation" Text='<%# Eval("filename")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Bold="True" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="stat" HeaderText="Acceptance Stat" />
                                                        </Columns>
                                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                                    </asp:GridView>
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $("[id*=GridVwEducation] td").hover(function () {
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


                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Professional Certifications</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnaddcertificate" runat="server" onserverclick="btnAddCert_Click" type="submit"
                                                        style="width: 150px" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeletecertificate" runat="server" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwCertification" runat="server" OnSorting="SortCertifications" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll3" runat="server" onclick="CheckAllEmpCert(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmp3" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                            <asp:TemplateField HeaderText="Certification" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkCertificate" runat="server" Text='<%# Eval("Certification")%>'
                                                                        CommandName="AddCertificate" CausesValidation="false" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Institute" HeaderText="Institute" SortExpression="Institute" />
                                                            <asp:BoundField DataField="Date Granted" HeaderText="Date Granted" SortExpression="Date Granted" />
                                                            <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" SortExpression="Expiry Date" />
                                                            <asp:TemplateField HeaderText="Certificate" SortExpression="filename">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("id") %>'
                                                                        OnClick="DownloadCertificate" Text='<%# Eval("filename")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="stat" HeaderText="Acceptance Status" SortExpression="stat" />
                                                        </Columns>
                                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                                    </asp:GridView>
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $("[id*=GridVwCertification] td").hover(function () {
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

                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Languages</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnaddlanguage" runat="server" onserverclick="btnAddLang_Click" type="submit"
                                                        style="width: 150px" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeletelanguage" runat="server" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwLang" runat="server" OnSorting="SortLanguages" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll0" runat="server" onclick="CheckAllEmpLang(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmp0" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                            <asp:TemplateField HeaderText="Language" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLanguage" runat="server" Text='<%# Eval("Language")%>' CommandName="AddLanguage"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Reading" HeaderText="Reading"></asp:BoundField>
                                                            <asp:BoundField DataField="Writing" HeaderText="Writing"></asp:BoundField>
                                                            <asp:BoundField DataField="Speaking" HeaderText="Speaking"></asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                                    </asp:GridView>
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $("[id*=GridVwLang] td").hover(function () {
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

                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Skills</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnaddskills" runat="server" onserverclick="btnAddSkill_Click" type="submit"
                                                        style="width: 150px; display:none;" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeleteskills" runat="server" Visible="false" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwSkills" runat="server" OnSorting="SortSkills" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="KpiObjective"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll1" runat="server" onclick="CheckAllEmpSkills(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmp1" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />--%>
                                                            <asp:BoundField DataField="KpiObjective" ItemStyle-Width="50%" HeaderText="Skills" SortExpression="rows" />
                                                            <%--<asp:TemplateField HeaderText="Skills" ItemStyle-Font-Bold="true" SortExpression="Skill">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSkill" runat="server" Text='<%# Eval("KpiObjective")%>' CommandName="AddSkill"
                                                                        CausesValidation="false" CommandArgument='<%# Eval("KpiObjective") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="ActualScore" HeaderText="Scores" />
                                                        </Columns>
                                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                                    </asp:GridView>
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $("[id*=GridVwSkills] td").hover(function () {
                                                                $("td", $(this).closest("tr")).addClass("hover_row");
                                                            }, function () {
                                                                $("td", $(this).closest("tr")).removeClass("hover_row");
                                                            })
                                                        })
                                                    </script>
                                                </div>
                                            </div>

                                        </div>
                                        <button id="Button3" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px" class="btn btn-info rounded">
                                            << Back</button>
                                    </div>

                                </asp:View>
                                <asp:View ID="WorkHistory" runat="server">
                                    <div class="row">
                                        <div class="card-box">
                                            <h5 class="card-title" style="color: #1BA691">Career History</h5>
                                            <div class="row">
                                                <div class="col-md-8 m-t-20">
                                                    <button id="btnaddcareer" runat="server" onserverclick="btnAddWork_Click" type="submit"
                                                        style="width: 150px" class="btn btn-primary btn-success">
                                                        Add New</button>
                                                    <asp:Button ID="btndeletecareer" runat="server" Text="Delete" OnClientClick="Confirm()"
                                                        BackColor="#FF3300" ForeColor="White" Width="150px" Height="34px" CssClass="btn btn-danger"
                                                        BorderStyle="None" Font-Names="Verdana" Font-Size="13px" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridVwWorkHistory" runat="server" AllowSorting="True"
                                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="20" DataKeyNames="id"
                                                        Width="100%" Height="50px" ToolTip="click row to select record"
                                                        Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                                        AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                                        BorderColor="#CCCCCC" CssClass="table table-condensed">
                                                        <RowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkboxSelectAll4" runat="server" onclick="CheckAllWorkHist(this);" />
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkEmp4" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                                            <asp:TemplateField HeaderText="Grade" ItemStyle-Font-Bold="true" SortExpression="Grade Level">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkWorkHistory" runat="server" Text='<%# Eval("Grade Level")%>'
                                                                        CommandName="AddWorkHistory" CausesValidation="false" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Job Title" HeaderText="Job Title" SortExpression="Job Title" />
                                                            <asp:BoundField DataField="Employment Type" HeaderText="Employment" SortExpression="Employment Type" />
                                                            <asp:BoundField DataField="Supervisor" HeaderText="Line Manager" SortExpression="Supervisor" />
                                                            <asp:BoundField DataField="Office" HeaderText="Department / Office" SortExpression="Office" />
                                                            <asp:BoundField DataField="Start Date" HeaderText="Start Date" SortExpression="Start Date" DataFormatString="{0:dd, MMM yyyy}" />
                                                            <asp:BoundField DataField="End Date" HeaderText="End Date" SortExpression="End Date" DataFormatString="{0:dd, MMM yyyy}" />
                                                        </Columns>
                                                        <HeaderStyle BackColor="White" ForeColor="#1BA691" HorizontalAlign="center" />
                                                    </asp:GridView>
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $("[id*=GridVwWorkHistory] td").hover(function () {
                                                                $("td", $(this).closest("tr")).addClass("hover_row");
                                                            }, function () {
                                                                $("td", $(this).closest("tr")).removeClass("hover_row");
                                                            })
                                                        })
                                                    </script>
                                                </div>
                                            </div>
                                        </div>
                                        <button id="Button4" runat="server" onserverclick="btnCancel_Click" type="submit"
                                            style="width: 150px" class="btn btn-info rounded">
                                            << Back</button>
                                    </div>



                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>

        </form>
    </body>
    </html>
</asp:Content>
