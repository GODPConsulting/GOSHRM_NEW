<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CasualLeaves.aspx.vb"
    Inherits="GOSHRM.CasualLeaves" EnableEventValidation="false" %>--%>

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CasualLeaves.aspx.vb"
    Inherits="GOSHRM.CasualLeaves" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">

      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
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
            width: 211px;
        }
        .style6
        {
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style7
        {
            width: 563px;
        }
        .RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .style9
        {
            width: 211px;
        }
        .style10
        {
            width: 138px;
        }
        </style>
</head>

<body>
    <form id="form1" action="">

     <div class="container">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblleavetype" runat="server" Font-Bold="True" Width="1px" Height="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblLoanRefNo" runat="server" Font-Bold="True" Width="1px" Height="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblBalance0" runat="server" visible="False" Width="1px" Height="1px"></asp:Label>
                    <asp:Label ID="lblEmpID" runat="server" Visible="False" Width="1px" Height="1px"></asp:Label>
                    <asp:TextBox ID="txtapproverlevel" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                    <ContentTemplate>
                        <div id="divalert1" runat="server" visible="false" class="alert alert-info">
                            <strong id="msgalert1" runat="server"></strong>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="radStartDate" EventName="SelectedDateChanged" />
                        <asp:AsyncPostBackTrigger ControlID="radEndDate" EventName="SelectedDateChanged" />
                    </Triggers>
                </asp:UpdatePanel>

               <div class="col-xs-8">
                    <h5 id="pagetitle" runat="server" class="page-title"></h5>
               </div>

                <div class="col-md-8 col-md-offset-0">
                    <form action="">
                    <div class="row">
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>NAME</label>
                                <input id="aempname" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-5">
                            <div class="form-group">
                                <label>START DATE</label>
                                <telerik:RadDatePicker ID="radStartDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                                    RenderMode="Lightweight" Skin="Bootstrap" Width="100%">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        AutoPostBack="True" RenderMode="Lightweight">
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
                        <div class=" col-md-5">
                            <div class="form-group">
                                <label>END DATE</label>
                                <telerik:RadDatePicker ID="radEndDate" runat="server" AutoPostBack="True" ForeColor="#666666"
                                     RenderMode="Lightweight"  Skin="Bootstrap" Width="100%">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True"
                                        FastNavigationNextText="&amp;lt;&amp;lt;" RenderMode="Lightweight" Skin="Bootstrap">
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"
                                        AutoPostBack="True" RenderMode="Lightweight">
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
                        <div class=" col-md-10">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridVwHeaderChckbox" runat="server" 
                                        BorderStyle="Solid" Font-Names="Verdana" AllowPaging="True" PageSize="50" DataKeyNames="id"
                                        Width="100%" Height="50px" Font-Size="12px"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No data to display" AutoGenerateColumns="False"
                                        GridLines="Both" ForeColor="#666666" BorderWidth="1px" BorderColor="#CCCCCC"
                                        CssClass="table table-condensed">
                                        <RowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="refno" HeaderText="Annual Leave" />
                                            <asp:BoundField DataField="From" HeaderText="From" />
                                            <asp:BoundField DataField="To" HeaderText="To" />
                                            <asp:BoundField DataField="NoOfDays" HeaderText="Days" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpMode" runat="server" Width="100px" Font-Size="10px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day(s)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDays" runat="server" Width="50px" Font-Size="10px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
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
                        <div class=" col-md-5">
                            <div class="form-group">
                                <label>
                                    LEAVE BALANCE</label>
                                <input id="aBalance" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>

                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Always">
                            <ContentTemplate>
                                <div class=" col-md-5">
                                    <div class="form-group">
                                        <label>
                                            NUMBER OF DAYS</label>
                                        <input id="aDays" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="radStartDate" EventName="SelectedDateChanged" />
                                <asp:AsyncPostBackTrigger ControlID="radEndDate" EventName="SelectedDateChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                                                 
                         <div  class=" col-md-10">
                            <div class="form-group">
                                <label>ATTACHMENT</label>                               
                                <input class="form-control" type="file" id="file1" runat="server" />
                                <button id="lnkclr" runat="server" type="submit" onserverclick="lnkClear_Click" 
                                 class="btn btn-link">Clear Attachment</button>
                                 <button id="lnkdownload" runat="server" type="submit" onserverclick="lnkDownloadAttach_Click"
                                 class="btn btn-link">Download Attachment</button>
                            </div>
                        </div>
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>REASON</label>
                                <textarea id="areason" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                         <div class=" col-md-10">
                            <div class="form-group">
                                <label>MANAGER</label>
                                <input id="amgrname" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        
                        <div class=" col-md-5">
                            <div class="form-group">
                                <label>MANAGER APPROVAL</label>
                                <input id="amgrstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        <div class=" col-md-5">
                            <div class="form-group">
                                <label>HUMAN RESOURCE DEPARTMENT</label>
                                <input id="ahrstatus" runat="server" class="form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                         <div id="divmgrcomment" runat="server" class=" col-md-10">
                            <div class="form-group">
                                <label>MANAGER COMMENT</label>
                                <textarea id="amgrcomment" runat="server" class="form-control" rows="5" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div id="divhrcomment" runat="server" class=" col-md-10">
                            <div class="form-group">
                                <label>HUMAN RESOURCE COMMENT</label>
                                <textarea id="ahrcomment" runat="server" class="form-control" rows="5" readonly="readonly"></textarea>
                            </div>
                        </div>
                      
                        <div class="col-md-10 m-t-20 text-center">
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
            </div>
        </div>
    </div>


    </form>
    <div class="loading" align="center">
        Processing, please wait...<br />
        <br />
        <img src="../../../images/loaders.gif" alt="" />
    </div>
</body>
</html>
 </asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>  