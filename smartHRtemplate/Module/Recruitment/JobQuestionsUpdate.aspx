 <%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="JobQuestionsUpdate.aspx.vb"
    Inherits="GOSHRM.JobQuestionsUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title></title>
   
  
</head>

 
<%--<script type="text/javascript">
    function setHeight() {
        var tt = document.getElementById("<%=txtQuestion.ClientID%>");
        tt.style.height = tt.scrollHeight + "px";

        var tans = document.getElementById("<%=txtAnswer.ClientID%>");
        tans.style.height = tans.scrollHeight + "px";
    }
    </script>
--%>
<body>
    <form id="form1" >
    <div class="container">
        <div class="row">
            <div class=" col-md-10">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Visible="False" Width="1px" Height="1px"></asp:TextBox>
                    <asp:Label ID="lblimage" runat="server" Font-Bold="True" Font-Size="1px" Visible="False"></asp:Label>
                </div>
            </div>
        </div>

         <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">

        <div class="row">
            <div class="col-md-8 col-md-offset-0">
                <h5 id="pagetitle" runat="server" class="page-title">
                    Test Questions</h5>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        QUESTION TYPE</label>
                    <telerik:RadComboBox ID="cboquestiontype" runat="server" Width="100%" AutoPostBack="True"
                        ForeColor="#666666" RenderMode="Lightweight" Skin="Bootstrap">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class=" col-md-6">
                <div class="form-group">
                    <label>
                        QUESTION NUMBER</label>
                    <input id="aposition" runat="server" class="form-control" type="text" placeholder="Question Number" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        QUESTION</label>
                        <textarea id="aquestion" runat="server" class="form-control" rows="4" cols="1" placeholder="Question"></textarea>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Always">
            <ContentTemplate>
                <div id="divoptions" runat="server">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    OPTION A</label>
                                <textarea id="aoptiona" runat="server" class="form-control" rows="1" cols="1" placeholder="Option A answer"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    OPTION B</label>
                                <textarea id="aoptionb" runat="server" class="form-control" rows="1" cols="1" placeholder="Option B answer"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    OPTION C</label>
                                <textarea id="aoptionc" runat="server" class="form-control" rows="1" cols="1" placeholder="Option C answer"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    OPTION D</label>
                                <textarea id="aoptiond" runat="server" class="form-control" rows="1" cols="1" placeholder="Option D answer"></textarea>
                            </div>
                        </div>
                    </div>
                    <div id="divanswer" runat="server" class="row">
                        <div class=" col-md-12">
                            <div class="form-group">
                                <label>
                                    ANSWER</label>
                                <asp:CheckBoxList ID="chkAnswers" runat="server" RepeatDirection="Horizontal"  
                                    Font-Size="13px" CellSpacing="5"  ForeColor="#666666">
                                </asp:CheckBoxList>
                                
                                <asp:RadioButtonList ID="rdoAnswers" runat="server" Font-Size="13px"
                                    RepeatDirection="Horizontal" TabIndex="7" ForeColor="#666666">
                                </asp:RadioButtonList>
                                <textarea id="answertext" runat="server" class="form-control" rows="3" cols="1" placeholder="Answer"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboQuestionType" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row">
            <div class=" col-md-12 text-center">
                <div class="form-group">
                    <label>
                        IMAGE</label>
                        <asp:Image ID="imgProfile" runat="server" Height="400px"
                        Width="400px" AlternateText="Picture Question" CssClass="img-responsive" />
                        <input class="form-control" type="file" id="file1" runat="server" />
                        <asp:Button ID="btnremove" runat="server" Text="Remove Image" BackColor="#FF3300" ForeColor="White"
                        Width="150px" Height="30px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px"
                        Font-Bold="True" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 m-t-20 text-center">
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
        </div>



   
    </form>
</body>
</html>
</asp:Content>