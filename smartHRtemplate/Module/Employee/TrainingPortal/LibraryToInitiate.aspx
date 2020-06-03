<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="LibraryToInitiate.aspx.vb"
    Inherits="GOSHRM.LibraryToInitiate" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Notify Trainees to commence Learning Assessment?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmApp() {
                var confirm_value1 = document.createElement("INPUT");
                confirm_value1.type = "hidden";
                confirm_value1.name = "confirm_value1";
                if (confirm("Notify Trainees to commence Application Assessment?")) {
                    confirm_value1.value = "Yes";
                } else {
                    confirm_value1.value = "No";
                }
                document.forms[0].appendChild(confirm_value1);
            }
        </script>
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }


        </script>
        <style type="text/css">
            .style1 {
                color: #FFFFFF;
                font-family: Candara;
                font-weight: bold;
            }

            .lbl {
                font-family: Candara;
                font-size: medium;
            }

            .style2 {
                font-family: Candara;
                font-size: small;
                width: 195px;
                color: #FF3300;
            }

            .style5 {
                font-family: Candara;
                font-size: medium;
                width: 195px;
            }

            .style7 {
                width: 195px;
            }

            .style8 {
                width: 468px;
            }
        </style>
        <script type="text/javascript" id="telerikClientEvents1">
            //<![CDATA[

            function drpTrainee_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button1").click();
            }
//]]>
        </script>
        <script type="text/javascript" id="telerikClientEvents2">
            //<![CDATA[

            function cboTrainer_DropDownClosing(sender, args) {
                //Add JavaScript handler code here
                document.getElementById("Button2").click();
            }
//]]>
        </script>
    </head>

    <body>
        <form>
            <asp:TextBox ID="txtid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
            <div class="container col-md-8">
                <div class="row">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                            id="msgalert" runat="server">Danger!</strong>
                    </div>
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-8">
                                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">Training Proposed for Direct Reports</h5>
                                <asp:TextBox ID="TextBox1" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="Div2" runat="server" class=" card-box">
                                <div class="card-header">
                                    <h6><a class="collapsed card-link" data-toggle="collapse" href="#collapseA">View Skills to Acquire
                                    </a></h6>
                                </div>
                                <div id="collapseA" class="collapse" data-parent="#accordion">
                                    <div class="card-body">                                                                                
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <b id="B4" runat="server">Training Skills To Acquire</b>
                                            </div>
                                            <div class="panel-body">
                                                <asp:DataList ID="gridAcquire" runat="server" Width="100%" RepeatColumns="1" CellSpacing="1"
                                                    RepeatLayout="Table" Font-Names="Arial" Font-Size="15px" GridLines="Both" DataKeyField="id"
                                                    BorderColor="#CCCCCC" ForeColor="#666666" BorderStyle="Solid"
                                                    BorderWidth="1px">
                                                    <ItemTemplate>
                                                        <table class="table" width="100%">
                                                            <tr>
                                                                <td valign="top" style="width: 100%">

                                                                    <p class="m-b-5"><%# Eval("kpiobjectives")%> <span id="datscore2" runat="server" class="text-success pull-right"><%# Eval("rating")%>%</span></p>

                                                                    <div class="progress m-b-0">
                                                                        <div id="datprogress2" runat="server" class="progress-bar progress-bar-success" role="progressbar" data-toggle="tooltip" title="40%" style="width: 40%"></div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        COURSE</label>
                                    <input id="acourse" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        SESSION</label>
                                    <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>


                            <div class="col-md-12">
                                <div id="Div1" runat="server" class=" card-box">
                                    <div class="card-header">
                                        <h6><b><a class="collapsed card-link" data-toggle="collapse" href="#collapseThree" title="Click to view trainer">Trainers
                                        </a></b></h6>
                                    </div>
                                    <div id="collapseThree" class="collapse" data-parent="#accordion">
                                        <div class="card-body">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <b id="B2" runat="server">Trainer</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <input id="atrainer" runat="server" class="form-control" type="text" disabled="disabled" />
                                                            <telerik:RadListBox ID="lsttrainers" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                                                        Enabled="False" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" Font-Names="Verdana"
                                                                        ForeColor="#666666">
                                                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                                    </telerik:RadListBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        TRAINING DATE</label>
                                    <input id="adate" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        TIME</label>
                                    <input id="atime" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        DELIVERY METHOD</label>
                                    <input id="adeliverymethod" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        SESSION TYPE</label>
                                    <input id="asessiontype" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        LOCATION</label>
                                    <textarea id="alocation" runat="server" rows="4" class="form-control" readonly="readonly"></textarea>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">
                                        NOTE</label>
                                    <textarea id="anote" runat="server" rows="4" class="form-control" cols="1" readonly="readonly"></textarea>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div id="collapse_acc" runat="server" class=" card-box">
                                    <div class="card-header">
                                        <h6><b><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" title="Click to select direct reports to send for training">Direct Reports for Training
                                        </a></b></h6>
                                    </div>
                                    <div id="collapseTwo" class="collapse" data-parent="#accordion">
                                        <div class="card-body">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <b id="B1" runat="server">Trainees</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <telerik:RadComboBox ID="cbopropose" runat="server" CheckBoxes="True"
                                                                Filter="Contains" RenderMode="Lightweight" Width="100%" Skin="Bootstrap"
                                                                Font-Names="Verdana" ForeColor="#666666" AutoPostBack="True" EnableCheckAllItemsCheckBox="True">
                                                            </telerik:RadComboBox>
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                                                <ContentTemplate>
                                                                    <telerik:RadListBox ID="lstpropose" runat="server" ResolvedRenderMode="Classic" BorderStyle="None"
                                                                        Enabled="False" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" Font-Names="Verdana"
                                                                        ForeColor="#666666">
                                                                        <ButtonSettings TransferButtons="All"></ButtonSettings>
                                                                    </telerik:RadListBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="cbopropose" EventName="ItemChecked" />
                                                                    <asp:AsyncPostBackTrigger ControlID="cbopropose" EventName="CheckAllCheck" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="control-label">
                                        COMMENT</label>
                                    <textarea id="acomment" runat="server" rows="4" class="form-control" cols="1" ></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success" title="Apply to attend training">
                                APPLY</button>
                            <button id="Button3" runat="server" onserverclick="btnCancel_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-info">
                                << BACK</button>
                        </div>
                    </div>



                </div>
        </form>
    </body>
    </html>
</asp:Content>
