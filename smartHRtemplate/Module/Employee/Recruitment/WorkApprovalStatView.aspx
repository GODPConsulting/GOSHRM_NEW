<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkApprovalStatView.aspx.vb"
    Inherits="GOSHRM.WorkApprovalStatView" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

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
    <link href="~/css/w3.css" rel="stylesheet" type="text/css" />
    <link href="~/css/slider-goke.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
  
</head>

<body onunload="window.opener.location=window.opener.location;">
    <form id="form1" runat="server">
    <div class="container">
    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong> 
                <asp:Label ID="lblfinalapprovalstat" runat="server" Font-Size="1px" Visible="false" ></asp:Label>                 
        </div>
    </div>
     <div class="row">
        <div class="col-xs-8">
            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
            Approval Status</h5>
        </div>
    </div>
     <div id="divapprover1" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>1. APPROVER</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="approver1" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver1stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver1comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver1date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divapprover2" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>2. APPROVER</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="approver2" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver2stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver2comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver2date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divapprover3" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>3. APPROVER</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="approver3" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver3stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver3comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver3date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divapprover4" runat="server" class="row">
        <div class=" col-md-8">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b>4. APPROVER</b>
                </div>
                <div class="panel-body">
                    <div class=" col-md-8">
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        NAME</label>
                                    <input id="approver4" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <input id="approver4stat" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="approver4comment" runat="server" class="form-control" rows="3" cols="1"
                                        readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-8">
                                <div class="form-group">
                                    <label>
                                        DATE</label>
                                    <input id="approver4date" runat="server" class="form-control" type="text" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 m-t-20 text-center">
            <button id="btcancel" runat="server" onserverclick="btnCancel_Click" type="submit"
                style="width: 150px" class="btn btn-primary btn-info">
                Close</button>
        </div>
    </div>
    </div>

    </form>
</body>
</html>
