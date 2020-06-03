

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="Feedback360Detail.aspx.vb"
    Inherits="GOSHRM.Feedback360Detail" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>


    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Submit Feedback form?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

</head>
<body >
    <form id="form1">
    <div class="container col-md-12">
        <div class="row">
            <div class=" col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                   <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
                     <asp:Label ID="lblQuestCount" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblend" runat="server" font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblQuestID" runat="server"  Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblid" runat="server"  Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblreviewerid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:TextBox ID="txtEmpID" runat="server" Font-Size="1px" ForeColor="#666666"
                     Visible="false" ></asp:TextBox>
                <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always">
                                        <ContentTemplate>
                                              <asp:Label ID="lblMyRating" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Visible="False"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdoMyRatings" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <b>360 APPRAISAL FEEDBACK
                            <label id="lblapprovals" runat="server">
                            </label>
                        </b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        REVIEW CYCLE</label>
                                    <input id="areviewcycle" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        EMPLOYEE</label>
                                    <input id="aemployee" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        JOB TITLE</label>
                                    <input id="ajobtitle" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        OFFICE</label>
                                    <input id="aoofice" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        POINT AWARDED</label>
                                    <input id="apointawarded" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        MAXIMUM POSSIBLE POINT</label>
                                    <input id="apointmax" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        SCORE (%)</label>
                                    <input id="ascore" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class=" col-md-6">
                                <div class="form-group">
                                    <label>
                                        STATUS</label>
                                    <input id="astat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        REVIEWER</label>
                                    <input id="areviewer" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>                            
                        </div>
                        <div class="row">
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label id="lbrates" runat ="server" style="font-size:13px" ></label>                                    
                                </div>
                            </div>                            
                        </div>

                    </div>
                </div>
            </div>
        </div>





        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="row">
                <div class=" col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                <ContentTemplate>
                                    <b>
                                        <label id="lbkpitype" runat="server">
                                        </label>
                                    </b>
                                </ContentTemplate>
                                <Triggers><asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" /><asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" /></Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="panel-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                <ContentTemplate>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label id="lbquestno" runat="server">1</label>
                                        <input id="lbobjective" runat="server" class="form-control" type="text" readonly="readonly" />
                                        <i><label id="lbobjectivedesc" runat="server"></label></i>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>COMMENT</label>
                                        <textarea id="acomment" runat="server" class="form-control" rows="3" cols="1" placeholder="Appraisal comment"></textarea>
                                        <asp:RadioButtonList ID="rdoMyRatings" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        RepeatDirection="Horizontal" AutoPostBack="True" Width="800px" 
                                        ForeColor="#666666">
                                    </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            </ContentTemplate>
                                <Triggers><asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" /><asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" /></Triggers>
                            </asp:UpdatePanel>
                        <div class ="row">
                        <div class="col-md-8 col-sm-8 col-xs-12 m-t-20">
                              <asp:Button ID="btnPrevious" runat="server" Text="Previous" ForeColor="White"
                                        Width="150px" CssClass="btn btn-primary btn-success" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana" />

                                    <asp:Button ID="btnNext" runat="server" Text="Next" ForeColor="White"
                                        Width="150px" CssClass="btn btn-primary btn-success" BorderStyle="None" Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana" />

                                        <asp:Button ID="btnSubmit" runat="server" Text="Finish" 
                                   ForeColor="White" CssClass="btn btn-primary btn-danger"
                                        Width="150px" BorderStyle="None" 
                                  Style="margin-top: 0px" Font-Bold="True"
                                        Font-Names="Verdana"
                                        ToolTip="Click to submit for review" onclientclick="Confirm()" />
                        </div>
                        </div>

                        </div>
                    </div>
                </div>
            </div>



            
  
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="row">
                <div class="table-responsive">
                            <asp:GridView ID="GridVwHeaderChckbox" runat="server" BorderStyle="Solid" Font-Names="Verdana"
                                AllowPaging="True" PageSize="50" DataKeyNames="id" Width="100%" Height="50px"
                                Font-Size="12px" ShowHeaderWhenEmpty="True" EmptyDataText="No data to display"
                                AutoGenerateColumns="False" GridLines="Both" ForeColor="#666666" BorderWidth="1px"
                                BorderColor="#CCCCCC" CssClass="table table-condensed">
                                <RowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Rows" ItemStyle-Width="5%" HeaderText="Row" SortExpression="rows" />
                                    <asp:BoundField DataField="appraisalitem" HeaderText="Question"></asp:BoundField>
                                    <asp:BoundField DataField="comments" HeaderText="Comments"></asp:BoundField>
                                    <asp:BoundField DataField="rating" HeaderText="Points"></asp:BoundField>
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
            <div class ="row">
              <div class="col-md-8 m-t-20">
                              <asp:Button ID="btnback" runat="server" BackColor="#999966" BorderStyle="None" Font-Bold="True"
                                Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="30px" Style="margin-top: 0px"
                                Text="Back" Width="150px" />

                                 <asp:Button ID="btnSend2" runat="server" Text="Submit for Review" BackColor="#0099FF"
                                ForeColor="White" Width="150px" Height="30px" BorderStyle="None" Style="margin-top: 0px"
                                Font-Bold="True" Font-Names="Verdana" Font-Size="12px" ToolTip="Click to submit"
                                OnClientClick="Confirm()" />
                        </div>
                        </div>

        </asp:View>
        <asp:View ID="View3" runat="server">
            <br />
            <br />
            <div>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server" Font-Names="Verdana" Font-Size="Medium" Style="text-align: center"
                                Width="100%" Font-Bold="True" ForeColor="#666666">360 Appraisal Feedback has been submitted</asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnClose" runat="server" BackColor="#1BA691" BorderStyle="None" Font-Bold="True"
                                Font-Names="Verdana" Font-Size="13px" ForeColor="White" Height="35px" Style="margin-top: 0px"
                                Text="Close" Width="300px" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
    </div>
    <div class="row">
        <div class="col-md-8 text-left">
            <div class="form-group">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Always">
                    <ContentTemplate>
                        <label id="apageno" runat="server" style="font-size: 12px">
                        </label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnPrevious" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNext" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


   
    
    
    </form>
</body>
</html>
</asp:Content>