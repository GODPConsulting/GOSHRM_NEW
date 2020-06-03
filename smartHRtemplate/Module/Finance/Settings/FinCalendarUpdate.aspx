<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="FinCalendarUpdate.aspx.vb"
    Inherits="GOSHRM.FinCalendarUpdate" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <title></title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
 
</head>

<body>
    <form id="form1" >
    <div class="content container-fluid">
        <div class="row">
            <div class="col-md-8">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                        id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="False"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-8 col-md-8">
                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                    Financial Calendar</h5>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-8">
                <div class="form-group">
                    <label>
                        NAME *</label>
                    <input id="aname" runat="server" class="form-control" type="text" placeholder="Year" />
                </div>
            </div>
    </div>
        <div class="row">
            <div class=" col-md-4">
                <div class="form-group">
                    <label>
                        START MONTH *</label>
                    <select id="drpstartmonth" runat="server" class="select form-control">
                        <option value="1">January</option>
                        <option value="2">February</option>
                        <option value="3">March</option>
                        <option value="4">April</option>
                        <option value="5">May</option>
                        <option value="6">June</option>
                        <option value="7">July</option>
                        <option value="8">August</option>
                        <option value="9">September</option>
                        <option value="10">October</option>
                        <option value="11">November</option>
                        <option value="12">December</option>

                    </select>
                </div>
            </div>
            <div class=" col-md-4">
                <div class="form-group">
                    <label>
                        YEAR *</label>
                    <select id="drpstartyear" runat="server" class="select form-control">                       
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-4">
                <div class="form-group">
                    <label>
                        END MONTH *</label>
                    <select id="drpendmonth" runat="server" class="select form-control">
                        <option value="1">January</option>
                        <option value="2">February</option>
                        <option value="3">March</option>
                        <option value="4">April</option>
                        <option value="5">May</option>
                        <option value="6">June</option>
                        <option value="7">July</option>
                        <option value="8">August</option>
                        <option value="9">September</option>
                        <option value="10">October</option>
                        <option value="11">November</option>
                        <option value="12">December</option>

                    </select>
                </div>
            </div>
            <div class=" col-md-4">
                <div class="form-group">
                    <label>
                         YEAR *</label>
                    <select id="drpendyear" runat="server" class="select form-control">                       
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8 m-t-20">
                <button id="btnsave" runat="server" onserverclick="btnAdd_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-success">
                    Save &amp; Update</button>
                <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                    style="width: 150px" class="btn btn-primary btn-info">
                    << Back</button>
            </div>
        </div>

    </div>



    </form>
</body>
</html>
</asp:Content>