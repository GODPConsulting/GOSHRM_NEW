<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkForceApprovalUpdate.aspx.vb"
    Inherits="GOSHRM.WorkForceApprovalUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI.Gantt" TagPrefix="cc1" %>
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
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
   <div class="container">
    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong> 
                <asp:Label ID="lblID" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                <asp:Label ID="lbllinkid" runat="server"  Font-Size="1px" Visible="False"></asp:Label>                
        </div>
    </div>
     <div class="row">
        <div class="col-xs-8">
            <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
            Approval</h5>
        </div>
    </div>
       <div class="row">
           <div class=" col-md-8">
               <div class="form-group">
                   <label>
                       APPROVAL STATUS</label>
                   <telerik:raddropdownlist id="radStatus" runat="server" defaultmessage="-- Select --"
                       height="16px" width="100%" resolvedrendermode="Classic" forecolor="#666666" rendermode="Lightweight"
                       skin="Bootstrap">
                   </telerik:raddropdownlist>
               </div>
           </div>
       </div>
       <div class="row">
           <div class=" col-md-8">
               <div class="form-group">
                   <label>
                       COMMENT</label>
                   <textarea id="acomment" runat="server" class="form-control" rows="4" cols="1"></textarea>
               </div>
           </div>
       </div>
       <div class="row">
           <div class="col-md-8 m-t-20 text-center">
               <button id="btnsaveobj" runat="server" onserverclick="btnAdd_Click" type="submit"
                   style="width: 150px" class="btn btn-primary btn-success">
                   Save & Update</button>
               <button id="btncloseobj" runat="server" onserverclick="btnCancel_Click" type="submit"
                   style="width: 150px" class="btn btn-primary btn-danger">
                   Close</button>
           </div>
       </div>
    </div>


    </form>
</body>
</html>
