<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="MyDevObjectives.aspx.vb"
    Inherits="GOSHRM.MyDevObjectives" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head>
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <script type="text/javascript" id="telerikClientEvents1">
//<![CDATA[

        function cboResponsibity_DropDownClosing(sender, args) {
            //Add JavaScript handler code here
            document.getElementById("Button3").click();
        }
//]]>
    </script>
</head>
<body>
    <form id="form1">
    <div class="content container-fluid">
        <div class="row">
            <div class=" col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtplanid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div id="divjoboffer" runat="server" class="col-md-12">
                <p>
                    <a href="DevelopmentPlans"><u>Development Plan</u></a>
                    <label>
                        >
                    </label>
                    <a id="A1" href="#" runat="server" onserverclick="btnCancel_Click"><u>Development Plan
                        Detail</u></a>
                    <label>
                        >
                    </label>
                    <a id="A2" href="#">Plan & Objectives</a>
                </p>
            </div>
        </div>
        <div class="card-box">
           
            <div class="row">
                <div >                    
                    <div class="profile-basic">
                        <div class="row">
                             <h5 class="card-title" style="color: #1BA691">
                Development Plan Objectives</h5>
                        </div>
                        <div class="row col-md-10">
                            <div class="form-group">
                                <label>COMPETENCIES *</label>
                                <telerik:RadComboBox ID="cboKPIType" Skin="Bootstrap" runat="server" Width="100%"  ForeColor="#666666"
                                    Font-Names="Verdana">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        DEVELOPMENT OBJECTIVE</label>
                                        <textarea id="adevobjective" runat="server" class="form-control" rows="5" cols="1" placeholder="Development Objective"></textarea>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        INTERVENTION TYPE</label>
                                        <input id="ainterventiontype" runat="server" class="form-control" type="text" placeholder="Intervention Type e.g training, job rotation, secondment, internship, on the job training etc" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        INTERVENTION</label>
                                        <textarea id="ainterventiondetail" runat="server" class="form-control" rows="5" cols="1" placeholder="Intervention Detail e.g IFRS Master Class Training in GODP"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        COURSES AND ACTIVITIES</label>
                                    <telerik:radcombobox id="cbotraining" runat="server" checkboxes="True" forecolor="#666666"
                                        width="100%" autopostback="True" Skin="Bootstrap" 
                                        EmptyMessage="-- Select Training --" Filter="Contains" RenderMode="Lightweight" tooltip="Trainings required to achieve objectives">
                                    </telerik:radcombobox>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                        <ContentTemplate>
                                            <telerik:radlistbox id="lsttraining" runat="server" resolvedrendermode="Classic"
                                                forecolor="#666666" borderstyle="None" enabled="False" width="100%" emptymessage="No Data"
                                                rendermode="Lightweight" sort="Ascending" 
                                                tooltip="list of trainings that may be required" Skin="Bootstrap" 
                                                Font-Size="14px">
                                                <buttonsettings transferbuttons="All"></buttonsettings>
                                                <emptymessagetemplate>
                                    No Data
                                </emptymessagetemplate>
                                            </telerik:radlistbox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbotraining" EventName="ItemChecked" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                    <label>
                                        RESPONSIBLE PERSON(S)</label>
                                    <telerik:radcombobox id="cboResponsibity" runat="server" autopostback="True" checkboxes="True"
                                        forecolor="#666666" width="100%" filter="Contains" 
                                        RenderMode="Lightweight" Skin="Bootstrap">
                                    </telerik:radcombobox>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                        <ContentTemplate>
                                            <telerik:radlistbox id="lstResponsibity" runat="server" resolvedrendermode="Classic"
                                                forecolor="#666666" borderstyle="None" enabled="False" width="100%" emptymessage="No Data"
                                                rendermode="Lightweight" sort="Ascending" 
                                                tooltip="Employees responsible to help achieve objectives" Font-Size="14px">
                                                <buttonsettings transferbuttons="All"></buttonsettings>
                                                <emptymessagetemplate>
                                    No Data
                                </emptymessagetemplate>
                                            </telerik:radlistbox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboResponsibity" EventName="ItemChecked" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-10">
                                <div class="form-group">
                                        <label>
                                            TARGET DATE</label>
                                        <telerik:raddatepicker runat="server" mindate="1900-01-01" forecolor="#666666" culture="en-US"
                                            rendermode="Lightweight" width="100%" resolvedrendermode="Classic" id="datDate"
                                            skin="Bootstrap">
                                            <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" rendermode="Lightweight"
                                                skin="Bootstrap" usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                            </calendar>
                                            <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
                                                rendermode="Lightweight">
                                            <emptymessagestyle resize="None">
                                            </emptymessagestyle>
                                            <readonlystyle resize="None">
                                            </readonlystyle>
                                            <focusedstyle resize="None">
                                            </focusedstyle>
                                            <disabledstyle resize="None">
                                            </disabledstyle>
                                            <invalidstyle resize="None">
                                            </invalidstyle>
                                            <hoveredstyle resize="None">
                                            </hoveredstyle>
                                            <enabledstyle resize="None">
                                            </enabledstyle>
                                            </dateinput>
                                            <datepopupbutton cssclass="" imageurl="" hoverimageurl=""></datepopupbutton>
                                        </telerik:raddatepicker>
                                    </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 m-t-20">
                                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                    style="width: 150px" class="btn btn-primary btn-success">
                                    Save &amp; Update</button>
                                <button id="btclose" runat="server" onserverclick="btnCancel_Click" type="submit"
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