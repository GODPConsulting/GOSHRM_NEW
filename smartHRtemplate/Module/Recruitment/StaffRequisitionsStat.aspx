<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StaffRequisitionsStat.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionsStat" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
  
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="~/css/fullcalendar.min.css" rel="stylesheet" />
    <link href="~/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="~/css/select2.min.css" type="text/css">
    <link rel="stylesheet" href="~/css/bootstrap-datetimepicker.min.css" type="text/css">
    <link rel="stylesheet" href="~/plugins/morris/morris.css">
    <link href="~/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/css/gridview.css" rel="stylesheet" type="text/css">
</head>
<body onunload="window.opener.location=window.opener.location;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class=" col-md-8">
            <div id="divalert" runat="server" visible="false" class="alert alert-info">
                <strong id="msgalert" runat="server"></strong>
                <asp:TextBox ID="lblid" runat="server" Width="1px" Height="1px" Visible="False"></asp:TextBox>
                <asp:Label ID="lblfinalapprovalstat" runat="server" Font-Size="1px" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 col-md-offset-0">
            <h5 id="pagetitle" runat="server" class="page-title">
                Staff Requisition Approval</h5>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>HEAD OF DEPARTMENT</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="ahodname" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                             <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="ahodapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="ahodcomment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="ahoddate" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Always">
        <ContentTemplate>
            <div id="divapprover" runat="server" class="row">
                <div class=" col-md-8">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b>APPROVER II</b>
                        </div>
                        <div class="panel-body">
                            <div class=" col-md-8">
                                <div class="row">
                                    <div class=" col-md-4">
                                        <div class="form-group">
                                            <label>
                                                NAME</label>
                                            <telerik:RadComboBox ID="cboHigherEmployee" runat="server" ForeColor="#666666" Width="100%"
                                                Filter="Contains" RenderMode="Lightweight" Skin="Bootstrap">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class=" col-md-4">
                                        <div class="form-group">
                                            <label>
                                                APPROVAL</label>
                                            <input id="approval" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-8">
                                        <div class="form-group">
                                            <label>
                                                COMMENT</label>
                                            <textarea id="approvercomment" runat="server" class="form-control" rows="3" cols="1"
                                                readonly="readonly"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" col-md-8">
                                        <div class="form-group">
                                            <label>
                                                DATE</label>
                                            <input id="approverdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkHigherApproval" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-8">
            <asp:CheckBox ID="chkHigherApproval" runat="server" AutoPostBack="True" Text="Requisition requires higher level approval (Approver II)"
                ForeColor="#666666" />
        </div>
    </div>
    <div runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>HUMAN RESOURCES</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="ahrname" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                            <div class=" col-md-4">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <telerik:RadComboBox ID="cbohrapproval" runat="server" ForeColor="#666666" Width="100%"
                                                Filter="None" RenderMode="Lightweight" Skin="Bootstrap">
                                            </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="ahrcomment" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="ahrdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            <div class="row">
        <div class="col-md-8 m-t-20">
                            <button id="btnupdate" runat="server" onserverclick="btnSave_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-info">
                                Close</button>
                        </div>
</div>
  
    </form>
</body>
</html>
