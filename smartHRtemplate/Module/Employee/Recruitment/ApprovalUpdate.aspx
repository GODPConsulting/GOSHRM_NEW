<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ApprovalUpdate.aspx.vb"
    Inherits="GOSHRM.ApprovalUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="icon" type="image/png" href="../../../images/goshrm.png">
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
<body onunload="window.opener.location=window.opener.location;" style="height: 317px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class=" col-md-8">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:Label ID="lblID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lbllinkid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    <asp:Label ID="lblempid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                      <asp:Label ID="lbleligible" runat="server" Font-Names="Verdana" Font-Size="1px"
                    Visible="False" Font-Italic="True"></asp:Label>
                <asp:Label ID="lbljobtitle" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lbljobtitleold" runat="server" Font-Names="Verdana" Font-Size="1px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lbljobgrade" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lbljobgradeold" runat="server" Font-Names="Verdana" Font-Size="1px"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblapprovernext" runat="server" Font-Names="Verdana" Font-Size="1px"
                    Visible="False"></asp:Label>
                 <asp:Label ID="lblinitiator" runat="server" Font-Names="Verdana" Font-Size="1px"
                    Visible="False"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 col-md-offset-0">
                <h5 id="pagetitle" runat="server" class="page-title">
                    Approval Status</h5>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-10">
                <div class="panel panel-success">
                    <div class="panel-body">
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        APPROVAL</label>
                                    <telerik:raddropdownlist id="radStatus" runat="server" defaultmessage="-- Select --"
                                        font-names="Verdana" font-size="12px" height="16px" width="100%" resolvedrendermode="Classic"
                                        forecolor="#666666" rendermode="Lightweight" skin="Bootstrap">
                                    </telerik:raddropdownlist>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col-md-12">
                                <div class="form-group">
                                    <label>
                                        COMMENT</label>
                                    <textarea id="acomment" runat="server" class="form-control" rows="3" cols="1"
                                        ></textarea>
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

                    </div>
                </div>
            </div>
        </div>
    </div>
    
  
    </form>
</body>
</html>
