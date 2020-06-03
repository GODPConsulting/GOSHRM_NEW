<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="ApplicationAssessment.aspx.vb"
    Inherits="GOSHRM.ApplicationAssessment" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Submit for final review?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmDelete() {
                var confirm_delete = document.createElement("INPUT");
                confirm_delete.type = "hidden";
                confirm_delete.name = "confirm_delete";
                if (confirm("Do you want to delete data?")) {
                    confirm_delete.value = "Yes";
                } else {
                    confirm_delete.value = "No";
                }
                document.forms[0].appendChild(confirm_delete);
            }
        </script>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }


        </script>
    </head>
    <body>
        <form>
            <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
                rel="Stylesheet" type="text/css" />

            <div class="container col-md-8">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong id="msgalert" runat="server">Danger!</strong>
                    </div>
                    <asp:Label ID="lblreviewer" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblid0" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestCount" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblQuestID" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblend" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblbutton" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblEmpSessionID" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblmgrsubmit" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lblempsubmit" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lbltrainid" runat="server" Font-Names="Verdana" Font-Size="12px" Visible="False"></asp:Label>
                    <asp:Label ID="lbldateassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                    <asp:Label ID="lblappdateassessment" runat="server" Visible="False" Font-Size="1px"></asp:Label>
                    <input id="txtEmpID" runat="server" class="form-control" type="text" disabled="disabled" visible="false" />
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h5><b id="pagetitle" runat="server"></b></h5>
                    </div>
                    <div class="panel-body">
                        <div class="row col-md-12 m-t-10">


                            <div>
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        <div class="form-group col-md-12">
                                            <label id="lblKPIType" runat="server"></label>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    EMPLOYEE NAME</label>
                                                <input id="txtName" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    JOB TITLE</label>
                                                <input id="txtJobTitle" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    JOB GRADE</label>
                                                <input id="txtJobGrade" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div class="form-group">
                                                <label>
                                                    REVIEWER</label>
                                                <telerik:RadDropDownList runat="server" Skin="Bootstrap" DefaultMessage="-- Select --" DropDownHeight="100px"
                                                    RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                                    Width="100%" ID="radReviewer" Height="150px" AutoPostBack="True"
                                                    ForeColor="#666666"></telerik:RadDropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lnkmore" runat="server" Font-Names="Verdana" Visible="false">Pick More Skills for Assessment</asp:LinkButton>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <div style="display: none">
                                                <label id="lblQuestNo" runat="server"></label>
                                            </div>
                                            <div class="col-md-12">
                                                <input type="text" class="form-control" runat="server" id="txtindicator" readonly="readonly" style="font-size: 12px" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label id="lblObjDescription" runat="server"></label>
                                        </div>

                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="txtMyperformance1" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                            </div>
                                        </div>
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    SCORE</label>
                                                <%--<telerik:RadDropDownList runat="server" Skin="Bootstrap" DefaultMessage="-- Select Score--" DropDownHeight="150px"
                                                    RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                                    Width="100%" ID="rdoempScore" Height="150px"
                                                    ForeColor="#666666"></telerik:RadDropDownList>--%>
                                                <telerik:radcombobox id="cboempScore" runat="server" filter="None" forecolor="#666666"
                                            font-names="Verdana" width="100%" skin="Bootstrap"></telerik:radcombobox>
                                            </div>
                                        </div>

                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label id="lblReviewerComment" runat="server">
                                                    REVIEWER COMMENT</label>
                                                <textarea id="txtManager1" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                            </div>
                                        </div>
                                        <div class=" col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    SCORE</label>
                                                <%--<telerik:RadDropDownList runat="server" Skin="Bootstrap" DefaultMessage="-- Select Score--" DropDownHeight="150px"
                                                    RenderMode="Lightweight" ResolvedRenderMode="Classic" BackColor="White" Font-Names="Verdana"
                                                    Width="100%" ID="rdoreviewerScore" Height="150px"
                                                    ForeColor="#666666"></telerik:RadDropDownList>--%>
                                                <telerik:radcombobox id="cboreviewerscore" runat="server" filter="None" forecolor="#666666"
                                            font-names="Verdana" width="100%" skin="Bootstrap"></telerik:radcombobox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 m-t-10 m-b-10 text-center">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" ForeColor="White"
                                                Width="150px" Height="35px" BorderStyle="None" CssClass="btn btn-info" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" />

                                            <asp:Button ID="btnNext" runat="server" Text="Next" ForeColor="White"
                                                Width="150px" Height="35px" BorderStyle="None" CssClass="btn btn-info" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" />

                                            <asp:Button ID="btnSubmit" runat="server" Text="Finish" CssClass="btn btn-success" ForeColor="White"
                                                Width="150px" Height="35px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" ToolTip="Click to submit for review" />
                                        </div>
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
                                        <script type="text/javascript">
                                            function openWindow(code) {
                                                window.open("MyAppObjectives.aspx?id=" + code, "open_window", "width=500,height=400");
                                            }
                                        </script>
                                        <asp:Label ID="lblcountStatus" runat="server" Font-Names="Verdana" Font-Size="12px"
                                            Font-Bold="True" ForeColor="Red" Width="100%"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblstatus" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True"
                                            ForeColor="Red" Width="100%"></asp:Label>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <div style="width:100% ">
                                            <div class="row">
                                                <asp:LinkButton style="margin-left:10px;" ID="lnkselfevaluation" data-toggle="tooltip" data-original-title="Self Evaluation" Height="35px" runat="server" Visible="True" CssClass="btn btn-default pull-left" >Self Evaluation                                                 <span style="margin-top:5px" aria-hidden="true" class="fa fa-send-o"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton style="margin-left:10px;" ID="lnkmanagerevaluation" data-toggle="tooltip" data-original-title="Manager Evaluation" Height="35px" runat="server" Visible="True" CssClass="btn btn-default pull-left" >Manager Evaluation                                                 <span style="margin-top:5px" aria-hidden="true" class="fa fa-send-o"></span>
                                                </asp:LinkButton>
                                            </div>
                                            <h5><b id="summaryheader" runat="server"></b></h5>
                                            <h7><b id="dateheader" runat="server"></b></h7>
                                            <asp:DataList ID="gridReview" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                                                RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="id"
                                                BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                BorderWidth="1px">
                                                <ItemTemplate>
                                                    <table class="table" width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 5%">
                                                                <p><%# Eval("rows")%></p>
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h5><p><%# Eval("KPIObjectives")%></p> </h5>                                                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 5%">                                                                
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h6><p><%# Eval("empComment")%></p></h6>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 5%">                                                                
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h6><p><%# Eval("emppoint")%></p></h6>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>

                                            <asp:DataList ID="gridManager" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" GridLines="Both" DataKeyField="id"
                                                BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                BorderWidth="1px">
                                                <ItemTemplate>
                                                    <table class="table" width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 5%">
                                                                <p><%# Eval("rows")%></p>
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h5><p><%# Eval("KPIObjectives")%></p> </h5>                                                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 5%">                                                                
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h6><p><%# Eval("ReviewerComment")%></p></h6>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 5%">                                                                
                                                            </td>
                                                            <td valign="top" style="width: 95%">
                                                                <h6><p><%# Eval("reviewerpoint")%></p></h6>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>

                                            <br />
                                            <div>
                                                <div class=" col-md-12">
                                                    <div class="form-group">
                                                        <label id="lbloverall" runat="server">
                                                            Overall Impression</label>
                                                        <input id="txtreviewercomment" runat="server" class="form-control" type="text" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" BorderStyle="None" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" ForeColor="White" Height="30px" Style="margin-top: 0px"
                                                Text="Back" Width="100px" />

                                            <asp:Button ID="btnSend2" runat="server" Text="Submit" CssClass="btn btn-success" ForeColor="White"
                                                Width="100px" Height="30px" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                                Font-Names="Verdana" Font-Size="11px" ToolTip="Click to submit for review" OnClientClick="Confirm()" />
                                        </div>
                                    </asp:View>
                                    <asp:View ID="View3" runat="server">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="m-b-20">
                                                    <div class="form-group">
                                                        <h3 id="H1" runat="server" class="page-title text-center">
                                                            <b>Application Assessment has been submitted</b></h3>
                                                    </div>
                                                    <div class="col-md-12 m-t-20 m-b-20 text-center">
                                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-success" BorderStyle="None" Font-Bold="True"
                                                            Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="30px" Style="margin-top: 0px"
                                                            Text="Close" Width="120px" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:View>
                                </asp:MultiView>
                            </div>


                        </div>


                    </div>
                </div>
            </div>

        </form>
    </body>
    </html>
</asp:Content>
