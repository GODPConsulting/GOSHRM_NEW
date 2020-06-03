<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmpConfirmationUpdate.aspx.vb"
    Inherits="GOSHRM.EmpConfirmationUpdate" EnableEventValidation="false" Debug="true" %>



<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

       <script type="text/javascript">
           function Complete() {
               var confirm_value = document.createElement("INPUT");
               confirm_value.type = "hidden";
               confirm_value.name = "confirm_value";
               if (confirm("Mark as Complete and send notification to HR?")) {
                   confirm_value.value = "Yes";
               } else {
                   confirm_value.value = "No";
               }
               document.forms[0].appendChild(confirm_value);
           }
    </script>

   
</head>

<body>
    <form id="form1" action="" >
    <div class="container col-md-10">
        <div class="row">
            <div class="col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblconfirmation" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblpath" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
        <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Confirmation</b></h5>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        EMPLOYEE</label>
                    <telerik:radcombobox runat="server" resolvedrendermode="Classic"
                        forecolor="#666666" width="100%" id="cboEmployee" filter="Contains" autopostback="True"
                        rendermode="Lightweight" skin="Bootstrap">
                    </telerik:radcombobox>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Always">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>
                                OFFICE</label>
                            <input id="aoffice" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>
                                PROBATION (MONTHS)</label>
                            <input id="aprobation" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>
                                DATE JOINED</label>
                            <input id="adatejoined" runat="server" class="form-control" type="text" disabled="disabled" />
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>
                                MANAGER</label>
                            <telerik:RadComboBox runat="server" 
                                ResolvedRenderMode="Classic" ForeColor="#666666"
                                Width="100%" ID="cboManager" Filter="Contains" RenderMode="Lightweight" 
                                Skin="Bootstrap">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboEmployee" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        TARGETS ACHIEVED</label>                
                    <textarea id="lbtargetachived" runat="server" class="form-control" rows="4" cols="1" readonly="readonly"></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        AREAS OF DEVELOPMENT</label>                    
                    <textarea id="aareadevelopment" runat="server" class="form-control" rows="4" cols="1" readonly="readonly"></textarea>
                    
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        COMMENT</label>                    
                    <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1" readonly="readonly"></textarea>
                   
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        RECOMMENDATION</label>
                    <input id="arecommendation" runat="server" class="form-control" type="text" disabled="disabled" />                    
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        RATING</label>
                    
                    <telerik:radrating id="RadRating1" runat="server" enabled="False" rendermode="Lightweight"
                        skin="Bootstrap">
                    </telerik:radrating>
                   
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                        <ContentTemplate>
                          
                             <label id="lbrating" runat ="server"></label>
                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RadRating1" EventName="Rate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        STATUS</label>
                    <input id="lbcompletestat" runat="server" class="form-control" type="text" disabled="disabled" />                    
                </div>
            </div>
        </div>
        <div id="divhrmgr" runat="server" class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        HUMAN RESOURCE MANAGER</label>
                    <input id="ahrmanager" runat="server" class="form-control" type="text" disabled="disabled" />                    
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        HUMAN RESOURCE COMMENT</label>
                    <textarea id="ahrcomment" runat="server" class="form-control" rows="4" cols="1" placeholder="HR comment"></textarea>                  
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        HUMAN RESOURCE RECOMMENDATION</label>
                        <telerik:RadComboBox runat="server"
                                ResolvedRenderMode="Classic" ForeColor="#666666"
                                Width="100%" ID="cborecommendation" RenderMode="Lightweight" 
                                Skin="Bootstrap" AutoPostBack="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending" />
                                     <telerik:RadComboBoxItem runat="server" Text="Confirmed" Value="Confirmed" />
                                    <telerik:RadComboBoxItem runat="server" Text="Terminate Employment" Value="Terminate Employment" />
                                    <telerik:RadComboBoxItem runat="server" Text="Extend Probation" Value="Extend Probation" />
                                </Items>                                
                            </telerik:RadComboBox>                  
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
            <ContentTemplate>
                <div id="divhrresponse" runat="server" class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label id="lbExtendProbationID" runat="server">
                                EXTEND BY</label>
                                <input id="aProbationExtension" runat="server" class="form-control" type="text" />
                                <telerik:raddatepicker id="radConfirm" runat="server" culture="en-US" mindate="1900-01-01"
                                    forecolor="#666666" resolvedrendermode="Classic" width="100%" visible="False"
                                    skin="Bootstrap">
                                    <calendar enableweekends="True" fastnavigationnexttext="&amp;lt;&amp;lt;" usecolumnheadersasselectors="False"
                                        userowheadersasselectors="False" skin="Bootstrap">
                                        </calendar>
                                    <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" labelwidth="40%"
                                        width="">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </dateinput>
                                    <datepopupbutton cssclass="" hoverimageurl="" imageurl=""></datepopupbutton>
                                </telerik:raddatepicker>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cborecommendation" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divconfirmation" runat ="server" class="row">
            <div class="">
                <div class="form-group">
                    <div class="col-md-6">
                        <label>
                        CONFIRMATION LETTER</label>
                    </div>
                    
                    <div class=" col-md-4">
                        <div class="form-group">
                            <button id="lnkconfirmation" type="button" runat="server" class="btn-link rounded" onserverclick="lnkletter_Click">
                                <i class="fa fa-download"></i><b>Confirmation Letter</b></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 m-t-20 text-center">
                <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-success">
                    Save &amp; Update</button>
                <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-danger">
                    << Back</button>
                <button id="btoff" runat="server" onserverclick="btnConfirmation_Click" type="submit"
                    class="btn btn-warning" title="generate confirmation letter" >
                    Confirmation Letter</button>
            </div>
        </div>
    </div>
     </div>
    </div>               




    </form>
</body>
</html>
</asp:Content>