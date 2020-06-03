<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="OLGradeUpdate.aspx.vb"
    Inherits="GOSHRM.OLGradeUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
   
</head>

<body  >
    <form id="form1" action="">
    
   <div class="container">
            <div class="row">
                <div class=" col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h4 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Languages</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Name*</label>
                                        <input id="aname" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Description</label>
                                        <textarea id="adesc" runat="server" class="form-control" rows="5"></textarea>
                                    </div>
                                </div>
                            </div>
<div class="row">
			<div class=" col-md-12">
                            <div class="form-group">
                                <label>Rank*</label>
                                <input id="arank" runat="server" class="form-control" type="text" />
                                <asp:Label ID="lblnote" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="#FF3300" Style="color: #0000FF" Font-Italic="True">Qualification Rank must be ascending from Highest to Lowest Ranked Qualification</asp:Label>
                            </div>
                        </div>
</div>

                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success rounded">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-info rounded">
                                        << Back</button>
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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>