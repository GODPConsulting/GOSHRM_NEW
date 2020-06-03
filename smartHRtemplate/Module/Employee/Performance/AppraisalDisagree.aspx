<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppraisalDisagree.aspx.vb"
    Inherits="GOSHRM.AppraisalDisagree" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css">--%>
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function closeWin() {
            popup.close();   // Closes the new window
        }
    </script>

     <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="../../../Styles/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/css/ionicons.min.css" rel="stylesheet" type="text/css" />  
    <link href="../../../Styles/css/gridview.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Candara;
            font-weight: bold;
        }
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style5
        {
            font-family: Candara;
            font-size: medium;
            width: 124px;
        }
        .style6
        {
        }
        .style8
        {
            width: 509px;
        }
        .style9
        {
            width: 124px;
        }
    </style>
</head>

<body style="height: 317px;
    margin-bottom: 15px;">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

        <div class="container">
        <div>
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                    <asp:TextBox ID="txtid" runat="server" Visible="False" Width="1px"></asp:TextBox>
                    <asp:Label ID="lblempid" runat="server" Visible="False" Width="1px"></asp:Label>
                </div>
                <div class="col-md-8 col-md-offset-0">
                    <h5 class="page-title">Review Disagree</h5>
                    <asp:Label ID="lblid" runat="server" Font-Size="1px" Visible="False"></asp:Label>
                    <form action="">
                    <div class="row">
                        
                        <div class=" col-md-10">
                            <div class="form-group">
                                <label>COMMENT</label>
                                <textarea id="amgrcomment" runat="server" class="form-control" rows="5"></textarea>
                            </div>
                        </div>
                      
                       
                        <div class="col-md-10 m-t-20 text-center">
                            <button id="btnsave" runat="server" onserverclick="btnAdd_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-success">
                                Save</button>
                            
                            <button id="btnclose" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-info">
                                Close</button>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
 
    </form>
</body>
</html>
