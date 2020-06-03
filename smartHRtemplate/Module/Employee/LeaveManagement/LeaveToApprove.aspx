
<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LeaveToApprove.aspx.vb"
    Inherits="GOSHRM.LeaveToApprove" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 144px;
        }
        .style6
        {
            width: 144px;
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
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .RadComboBox .rcbArrowCell a{width:18px;height:22px;position:relative;outline:0;font-size:0;line-height:1px;text-decoration:none;text-indent:9999px;display:block;overflow:hidden;cursor:default;*zoom:1}
        </style>
</head>

<body style="">
    <form id="form1" >
   <div class="container col-md-8">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px" Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblLeaveRefNo" runat="server" Font-Bold="True" Visible="false" Font-Size="2px" Width="3px" Height="2px"></asp:Label>
                    <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"  Font-Italic="True" ></asp:Label>
                    <asp:Label ID="lblleavedate" runat="server" Font-Names="Verdana"  Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                    <asp:Label ID="lblleavetype" runat="server" Font-Size="1px"></asp:Label>
                </div>
               <%--<div class="col-xs-8">
                    <h5 id="pagetitle" runat="server" class="page-title"></h5>
               </div>--%>
               <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server"></b></h5>
                </div>
                <div class="panel-body">
                <div class="col-md-12 col-md-offset-0">
                    <%--<h5 class="page-title" style="color:#1BA691"></h5>--%>
                    <form action="">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>NAME</label>
                                <input id="aemployee" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>START DATE</label>
                                <input id="astartdate" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>END DATE</label>
                                <input id="aenddate" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>LENGTH OF DAY</label>
                                <input id="alength" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>NUMBER OF DAYS</label>
                                <input id="aDays" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div id="sattachment" runat="server" class=" col-md-12">
                            <div class="form-group">
                                <label>ATTACHMENT</label>
                                <button id="lnkdownld" runat="server" onserverclick="lnkDownloadAttach_Click" type="submit"
                                 class="btn btn-link">Download Attachment</button>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>REASON</label>
                                <textarea id="areason" runat="server" class="form-control" rows="5" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>MANAGER APPROVAL</label>
                                <telerik:RadComboBox ID="radApproval" ForeColor="#666666"
                                    runat="server" 
                                                    RenderMode="Lightweight" 
                                                    ResolvedRenderMode="Classic" Width="100%" Skin="Bootstrap">
                                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>HUMAN RESOURCE DEPARTMENT</label>
                                <input id="ahrstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>COMMENT</label>
                                <textarea id="acomment" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                      
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
                    </div>
                    </form>
                </div>
                </div></div>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>