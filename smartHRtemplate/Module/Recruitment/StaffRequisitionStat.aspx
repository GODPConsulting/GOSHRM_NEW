<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StaffRequisitionStat.aspx.vb"
    Inherits="GOSHRM.StaffRequisitionStat" EnableEventValidation="false" %>

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
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                    id="msgalert" runat="server">Danger!</strong>
                <asp:Label ID="lblfinalapprovalstat" runat="server" Font-Size="1px"></asp:Label>
                <asp:Label ID="lblid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblhodid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lblmgrid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 col-md-offset-0">
            <h5 id="pagetitle" runat="server" class="page-title">
                Staff Requisition Approval</h5>
        </div>
    </div>
    <div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewStat" runat="server">
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
                                                <input id="avhodname" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <input id="avhodapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="avhodcomment" runat="server" class="form-control" rows="3" cols="1"
                                                    readonly="readonly"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="avhoddate" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                                                <input id="vapprovername" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <input id="vapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="vapprovercomment" runat="server" class="form-control" rows="3" cols="1"
                                                    readonly="readonly"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="vapproverdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divhrview" runat="server" class="row">
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
                                                <input id="avhrname" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <input id="avhrapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="avhrcomment" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="avhrdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="ApprovalStat" runat="server">
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
                                                <input id="aehodname" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <telerik:radcombobox id="cboaehodapproval" runat="server" forecolor="#666666" width="100%"
                                                    rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="aehodcomment" runat="server" class="form-control" rows="4" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="aehoddate" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="diveapprover2" runat="server" class="row">
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
                                                <input id="aeapprovername" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <telerik:radcombobox id="cboapproverstat" runat="server" forecolor="#666666" width="100%"
                                                    rendermode="Lightweight" skin="Bootstrap">
                                                </telerik:radcombobox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="aeappcomment" runat="server" class="form-control" rows="3" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="aeapproverdate" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divehr" runat="server" class="row">
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
                                                <input id="aehrname" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    APPROVAL</label>
                                                <input id="aehrapproval" runat="server" class="form-control" type="text" disabled="disabled" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    COMMENT</label>
                                                <textarea id="aehrcomment" runat="server" class="form-control" rows="3" cols="1"
                                                    readonly="readonly"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    DATE</label>
                                                <input id="aehrdate" runat="server" class="form-control" type="text" disabled="disabled" />
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
                
                </asp:View>
                </asp:MultiView>
                </div>
    </form>
</body>
</html>
