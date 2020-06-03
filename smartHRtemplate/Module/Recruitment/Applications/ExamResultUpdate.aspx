<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExamResultUpdate.aspx.vb"
    Inherits="GOSHRM.ExamResultUpdate" EnableEventValidation="false" %>

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
<body onunload="window.opener.location=window.opener.location;" style="height: 258px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <strong id="msgalert" runat="server">Danger!</strong>
            <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtappid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class=" col-md-8">
            <div class="form-group">
                <label>
                    SUBJECT*</label>
                    <telerik:RadComboBox ID="cboSubject" runat="server" 
                    RenderMode="Lightweight" ForeColor="#666666" Width="100%" 
                    Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
            </div>
        </div>  
    </div>
    <div class="row">
        <div class=" col-md-8">
            <div class="form-group">
                <label>
                    GRADE*</label>
                    <telerik:RadComboBox ID="cbograde" runat="server" 
                    RenderMode="Lightweight"  ForeColor="#666666"
                                    Width="100%" 
                    Filter="Contains" Skin="Bootstrap">
                                </telerik:RadComboBox>
            </div>
        </div>  
    </div>
    <div class="row">
    <div class="col-md-8 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                << Back</button>
                        </div>
</div>

    
    </form>
</body>
</html>
