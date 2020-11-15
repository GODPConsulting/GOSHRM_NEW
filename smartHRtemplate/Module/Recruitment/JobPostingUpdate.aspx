<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobPostingUpdate.aspx.vb"
    Inherits="GOSHRM.JobPostingUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
       <%-- <script type="text/javascript" src='https://cloud.tinymce.com/5/tinymce.min.js'></script>
         <script> tinymce.init({ selector: 'textarea' });</script>
          <script type="text/javascript">
              tinymce.init({
                  selector: '#ajobdesc',
                  skin: 'dark',
                  width: 600,
                  height: 300,
                  toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons'
              });
          </script>--%>
         <%-- <script src="//cdn.ckeditor.com/4.11.2/basic/ckeditor.js"></script>--%>
         <script src="../../js/ckeditor/ckeditor.js"></script>
          
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                <div class=" col-md-8">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Label ID="lblcountry" runat="server" CssClass="lbl" ForeColor="#666666" Font-Size="1px"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 col-md-offset-0">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 col-md-offset-0">
                                    <h5 id="pagetitle" runat="server" class="page-title">
                                        Job Post</h5>
                                </div>
                            </div>
                           
                            
                                <div id="divclone" runat="server" class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                CLONE JOB</label>
                                            <telerik:radcombobox id="cboclone" runat="server" width="100%" forecolor="#666666"
                                                autopostback="True" skin="Bootstrap" tooltip="clone job post from any existing post">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Always">
                                        <ContentTemplate>
                                            <div id="divclonesource" runat="server" class=" col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        PICK CLONE SOURCE</label>
                                                    <telerik:radcombobox id="cbojobclone" runat="server" autopostback="True" filter="Contains"
                                                        forecolor="#666666" rendermode="Lightweight" skin="Bootstrap" width="100%">
                                                    </telerik:radcombobox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboclone" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                COMPANY*</label>
                                            <telerik:radcombobox id="cboOffice" runat="server" forecolor="#666666" width="100%"
                                                autopostback="True" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                HIRING MANAGER*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboHiringManager" runat="server" forecolor="#666666" width="100%"
                                                        tabindex="2" filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                JOB TITLE*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboJobTitle" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                JOB TYPE*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cbojobtype" runat="server" filter="Contains" forecolor="#666666"
                                                        rendermode="Lightweight" skin="Bootstrap" width="100%">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                AREA OF SPECILISATION*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboSpecialisation" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                EXPERIENCE LEVEL*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboExperience" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM YEARS OF EXPERIENCE</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <input id="aminexpyr" runat="server" class="form-control" type="text" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MAXIMUM YEARS OF EXPERIENCE</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <input id="amaxexpyr" runat="server" class="form-control" type="text" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM AGE</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <input id="aminage" runat="server" class="form-control" type="text" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MAXIMUM AGE</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <input id="amaxage" runat="server" class="form-control" type="text" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                STATUS</label>
                                            <input id="ajobstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                AVAILABLE NUMBER OF POSITION</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <input id="aposition" runat="server" class="form-control" type="text" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                JOB DESCRIPTION*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <textarea name="ajobdesc" id="ajobdesc" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <script>
                                                // Replace the <textarea id="editor1"> with a CKEditor
                                                // instance, using default configuration.
                                                CKEDITOR.replace('ajobdesc');
                                            </script>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                SKILLS*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <textarea id="ajobskill" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboJobTitle" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM QUALIFICATION*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboeducation" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM GRADE*</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboMinGrade" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                DISCIPLINE*</label>
                                            <telerik:radcombobox id="cboDiscipline" runat="server" forecolor="#666666" width="100%"
                                                filter="Contains" rendermode="Lightweight" skin="Bootstrap" autopostback="True"
                                                checkboxes="True">
                                            </telerik:radcombobox>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radlistbox id="lstDiscipline" runat="server" enabled="False" font-names="Verdana"
                                                        forecolor="#666666" font-size="12px" width="100%">
                                                    </telerik:radlistbox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboDiscipline" EventName="ItemChecked" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                SECONDARY SCHOOL SUBJECTS</label>
                                            <telerik:radcombobox id="cboSchoolLeaving" runat="server" forecolor="#666666" width="100%"
                                                filter="Contains" rendermode="Lightweight" skin="Bootstrap" autopostback="True"
                                                checkboxes="True">
                                            </telerik:radcombobox>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radlistbox id="lstSchoolLeaving" runat="server" enabled="False" font-names="Verdana"
                                                        forecolor="#666666" font-size="12px" width="100%">
                                                    </telerik:radlistbox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboSchoolLeaving" EventName="ItemChecked" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM SCHOOL QUALIFICATION*</label>
                                            <telerik:radcombobox id="cboMinOLGrade" runat="server" forecolor="#666666" width="100%"
                                                filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                CURRENCY*</label>
                                            <telerik:radcombobox id="cbocurrency" runat="server" forecolor="#666666" width="100%"
                                                filter="Contains" rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MINIMUM SALARY</label>
                                            <input id="aminsalary" runat="server" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                MAXIMUM SALARY</label>
                                            <input id="amaxsalary" runat="server" class="form-control" type="text" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                LOCATION</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <telerik:radcombobox id="cboLocation" runat="server" forecolor="#666666" width="100%"
                                                        filter="Contains" rendermode="Lightweight" skin="Bootstrap" autopostback="True"
                                                        checkboxes="True">
                                                    </telerik:radcombobox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbooffice" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel23" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtLocation" runat="server" Width="100%" Height="75px" ForeColor="#666666"
                                                        TextMode="MultiLine" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                                        Font-Size="12px" ReadOnly="True"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="ItemChecked" />
                                                    <asp:AsyncPostBackTrigger ControlID="cbojobclone" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                APPLY APPTITUDE TEST</label>
                                            <telerik:radcombobox id="cboOnline" runat="server" forecolor="#666666" width="100%"
                                                skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <label>
                                                OPEN TO EMPLOYEES</label>
                                            <telerik:radcombobox id="radMail" runat="server" width="100%" forecolor="#666666"
                                                rendermode="Lightweight" skin="Bootstrap">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                NOTE</label>
                                            <textarea id="anote" runat="server" class="form-control" rows="5" cols="1"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                CLOSING DATE</label>
                                            <telerik:raddatepicker id="radCloseDate" runat="server" forecolor="#666666" width="100%"
                                                skin="Bootstrap">
                                                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" enableweekends="True"
                                                    fastnavigationnexttext="&amp;lt;&amp;lt;">
                                        </calendar>
                                                <dateinput displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy" labelwidth="40%"
                                                    height="23px">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </dateinput>
                                                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                                            </telerik:raddatepicker>
                                        </div>
                                    </div>
                                    <div id="divactive" runat="server" class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                RECRUITMENT ACTIVE</label>
                                            <telerik:radcombobox id="cboActive" runat="server" width="100%" forecolor="#666666"
                                                skin="Bootstrap" rendermode="Lightweight">
                                            </telerik:radcombobox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
                                        << Back</button>
                                </div>
                           
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </body>
    </html>
</asp:Content>
