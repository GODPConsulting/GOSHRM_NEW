<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="false" CodeBehind="JobTitleSkills.aspx.vb"
    Inherits="GOSHRM.JobTitleSkills" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
       
        <script type="text/javascript">

            function closeWin() {
                popup.close();   // Closes the new window
            }
   

        </script>
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to delete data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
    </telerik:RadCodeBlock>
 
</head>

<body>
    <form id="form1" >
    
        <div class="container">
            <div class="row">
                <div class=" col-md-10">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server"></strong>
                        <asp:TextBox ID="txtid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtjobid" runat="server" Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
             <div class="row">
                <div id="divjoboffer" runat="server" class="col-sm-3 col-md-6 col-xs-6 pull-left">
                    <p>
                        <a href="JobTitles"><u>Job Titles</u></a>
                        <label>
                            >
                        </label>
                        <a id="A1" href="JobTitleUpdate?id=<%=txtjobid.Text %>"><u>Job Title Detail</u></a>
                        <label>
                            >
                        </label>
                        <a id="A2" href="#">Job Skills</a>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-0">
                                    <h4 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Skills</h4>
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
                                        <label>
                                            Weight (%)*</label>
                                        <input id="aweight" runat="server" class="form-control" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-success">
                                        Save &amp; Update</button>
                                    <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-primary btn-danger">
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