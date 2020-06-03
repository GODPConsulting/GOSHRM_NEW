

<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="MailTemplate.aspx.vb"
    Inherits="GOSHRM.MailTemplate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <script type="text/javascript">
        function RemoveAttach() {
            var confirm_value1 = document.createElement("INPUT");
            confirm_value1.type = "hidden";
            confirm_value1.name = "confirm_value1";
            if (confirm("Remove attachement?")) {
                confirm_value1.value = "Yes";
            } else {
                confirm_value1.value = "No";
            }
            document.forms[0].appendChild(confirm_value1);
        }
    </script>

    <style type="text/css">
        .lbl
        {
            font-family: Candara;
            font-size: medium;
        }
        .style1
        {
            width: 7%;
        }
    </style>
</head>

<body >
    <form id="form1" action ="" enctype="multipart/form-data">
    <div class="container col-md-10">
    <div class="row">
        <div id="divalert" runat="server" visible="false" class="alert alert-info">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong
                id="msgalert" runat="server">Danger!</strong>
        </div>
        </div>
        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                            </h5>
                              <asp:Label ID="lblpath" runat="server" Text="Subject" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                                    <asp:Label ID="lblempid" runat="server" Text="Subject" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                                    <asp:Label ID="lblmgr" runat="server" Text="Subject" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                                     <asp:Label ID="lblid" runat="server" Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                                    <asp:Label ID="lblcompany" runat="server" Text="Subject" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="1px" ForeColor="#666666" Visible="False"></asp:Label>
                            </div>
                            
                            <div class="panel-body">
        <div class="row">
            <div id="divjoboffer" runat="server" class="col-sm-3 col-md-6 col-xs-6 pull-left">
                <p>
                    <a href="JobInterviews"><u>Interviews</u></a>
                    <label>
                        >
                    </label>
                    <a href="Interviewees"><u>Interview Shortlists</u></a>
                    <label>
                        >
                    </label>
                    <a href="InterviewHRDetail"><u>Candidate</u></a>
                    <label>
                        >
                    </label>
                    <a href="#">Offer Letter</a>
                </p>
            </div>
            <div id="divpromotion" runat="server" class="col-sm-3 col-md-6 col-xs-6 pull-left">
                <p>
                    <a href="Promotions"><u>Promotions</u></a>
                    <label>
                        >
                    </label>
                    <a href="#" runat="server" onserverclick="btnback_Click" ><u>Promotion</u></a>
                    <label>
                        >
                    </label>
                    <a href="#">Promotion Letter</a>
                </p>
            </div>    
        </div>
    <div class="row">
        <div class="">
            <div class="form-group">
                <label>
                    TO</label>
                <input id="aemail" runat="server" class="form-control" type="text"  />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="">
            <div class="form-group">
                <label>
                    SUBJECT</label>
                <input id="asubject" runat="server" class="form-control" type="text"/>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="">
            <div class="form-group">
                <label>
                    ATTACHMENT</label>
                <button id="lnkattach" type="button" runat="server" class="btn btn-link" onserverclick="lnkattachment_Click" 
                    >LinkButton</button>
                <asp:Button ID="btnremove" runat="server" Text="Remove" BackColor="#FF3300" ForeColor="White"
                        Width="100px" Height="30px" BorderStyle="None" Font-Names="Verdana" Font-Size="12px"
                        Font-Bold="True" onclientclick="RemoveAttach()" />
                <input class="form-control" type="file" id="file1" runat="server" />
                <input class="form-control" type="file" id="file2" runat="server" />
                <input class="form-control" type="file" id="file3" runat="server" visible="false" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="">
            <div class="form-group">
                <label>
                    MESSAGE</label>
                <textarea id="amessage" runat="server" class="form-control" rows="13" cols="1"></textarea>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8 m-t-20">
            <button id="btnupdate" runat="server" type="submit" onserverclick="btnSend_Click"
                style="width: 150px" class="btn btn-success"><i class="fa fa-send"></i>
                Send</button>            
        </div>
    </div>
    </div></div></div>

    </form>
</body>
</html>
</asp:Content>