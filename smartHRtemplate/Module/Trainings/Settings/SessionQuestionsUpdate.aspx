<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup ="true" CodeBehind="SessionQuestionsUpdate.aspx.vb" Inherits="GOSHRM.SessionQuestionsUpdate" EnableEventValidation="false" Debug="true"%>
<asp:Content ID = "content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
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
            width: 176px;
            color: #FF0000;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 176px;
        }
        .style6
        {
            width: 176px;
        }
        .RadDropDownList { display:inline-block !important; 
                                               width: 443px !important; }
                            .RadDropDownList_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadDropDownList{width:160px;line-height:1.3333em;text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;cursor:default;*zoom:1;*display:inline}.RadDropDownList_Default .rddlInner{border-radius:3px;border-color:#aaa;color:#333;background-color:#c1c1c1;background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');background-image:linear-gradient(#fafafa,#e6e6e6)}.RadDropDownList .rddlInner{vertical-align:top}.RadDropDownList .rddlInner{padding:2px 22px 2px 5px;border-width:1px;border-style:solid;display:block;position:relative;overflow:hidden}.RadDropDownList_Default .rddlDefaultMessage{color:#a5a5a5}.RadDropDownList .rddlDefaultMessage{font-style:italic}.RadDropDownList .rddlFakeInput{margin:0;padding:0;width:100%;min-height:16px;display:block;overflow:hidden}
                            .rddlFakeInput {
                                    height: 16px !important; 
                                    width: 80% !important;}.RadDropDownList_Default .rddlIcon{background-image:url('mvwres://Telerik.Web.UI, Version=2015.2.623.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');background-position:-22px -20px}.RadDropDownList .rddlIcon{width:16px;height:100%;border:0;background-repeat:no-repeat;position:absolute;top:0;right:0}.rddlSlide{float:left;display:none;position:absolute;overflow:hidden;z-index:7000}.rddlPopup_Default{border-color:#a0a0a0;color:#333;background-color:#fff;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.rddlPopup{*zoom:1;border-width:1px;border-style:solid;text-align:left;position:relative;cursor:default;width:160px;*width:158px;box-sizing:border-box}
        .style8
        {
            width: 561px;
        }
        </style>
</head>
<%-- <script type="text/javascript" language="javascript">
    function LoadList()
    {        
        var ds=null;
        ds = <%=listFilter %>;
            $( "#txtName" ).autocomplete({
              source: ds
            });
    }
    </script>--%>

 
<script type="text/javascript">
    function setHeight() {
        var tt = document.getElementById("<%=txtQuestion.ClientID%>");
        tt.style.height = tt.scrollHeight + "px";

        var tans = document.getElementById("<%=txtAnswer.ClientID%>");
        tans.style.height = tans.scrollHeight + "px";
    }
    </script>

<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
        <div class="container col-md-8">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox>
                     <asp:Label ID="lblimage" runat="server" Font-Bold="True" Font-Names="Candara" Font-Size="Small"
                    ForeColor="#FF3300" Visible="False"></asp:Label>
                     <asp:Label ID="lblHeader" runat="server" Font-Names="Verdana" 
                    Font-Size="14px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Test Questions</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>QUESTION TYPE*</label>
                                <telerik:radcombobox ID="cboQuestionType" runat="server" Width="100%" 
                                    AutoPostBack="True" Font-Names="Verdana" Font-Size="12px" 
                                    ForeColor="#666666" skin="Bootstrap">
                                </telerik:radcombobox>
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>QUESTION*</label>
                                <input id="txtQuestion" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>QUESTION NO</label>
                                <input id="txtOrder" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    POINT SCORE</label>
                                <input id="txtPoint" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label id="opt1" runat="server">OPTION A</label>
                                <input id="txtOption1" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label id="opt2" runat="server">OPTION B</label>
                                <input id="txtOption2" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label id="opt3" runat="server">OPTION C</label>
                                <input id="txtOption3" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label id="opt4" runat="server">OPTION D</label>
                                <input id="txtOption4" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                  ANSWER</label>
                                <div class="col-md-12">
                                    <asp:CheckBoxList ID="chkAnswers" runat="server" RepeatDirection="Horizontal" 
                                        TabIndex="7" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666">
                                    </asp:CheckBoxList>
                                    <asp:RadioButtonList ID="rdoAnswers" runat="server" 
                                        RepeatDirection="Horizontal" TabIndex="7" Font-Names="Verdana" 
                                        Font-Size="12px" ForeColor="#666666">
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="txtAnswer" runat="server" TextMode="MultiLine" 
                                        BorderColor="#CCCCCC" BorderWidth="1px" TabIndex="7" Width="100%" 
                                        Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                  PICTURE QUESTION</label>
                                <div>
                                    <asp:Image ID="imgProfile" runat="server" Height="400px"
                                        Width="500px" AlternateText="Picture Question" />
                                    <asp:FileUpload ID="imgUpload" runat="server" ToolTip="Browse Photo" 
                                        Font-Names="Verdana" Font-Size="12px" />
                                    <br />
                                     <asp:Button ID="btnImage" runat="server" Text="Upload Image" BackColor="#1BA691"
                                        ForeColor="White" Width="20%" Height="20px" BorderStyle="None" 
                                        Style="margin-top: 0px" Font-Names="Verdana" Font-Size="11px" />
                                    <asp:Button ID="btnImage0" runat="server" Text="Delete Image" BackColor="#FF9933"
                                        ForeColor="White" Width="20%" Height="20px" BorderStyle="None" 
                                        Style="margin-top: 0px" Font-Names="Verdana" Font-Size="11px" />
                                </div>
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
                </div>
            </div>
             </div>
            </div>
    </form>
</body>
</html>
</asp:Content>