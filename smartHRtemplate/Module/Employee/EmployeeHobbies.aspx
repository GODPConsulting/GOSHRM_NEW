<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="EmployeeHobbies.aspx.vb" Inherits="GOSHRM.EmployeeHobbies" EnableEventValidation="true" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            function closeWin() {
                popup.close();   // Closes the new window
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var star = <%=TextBox1.Text %>;
                var rating = document.getElementById("#rating")
                var icons = document.createElement("i")
                icons.className = "glyphicon glyphicon-star-empty"
                while (star <= 5) {
                    rating.appendChild(icons)
                }
                

            });
        </script>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div id="divalert" runat="server" visible="false" class="alert alert-info">
                        <strong id="msgalert" runat="server">Danger!</strong>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <asp:TextBox ID="txtid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtempid" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TextBox1" runat="server" Width="1px" Font-Size="1px" Visible="False"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-10">
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-8 col-md-12">
                                    <h5 id="pagetitle" runat="server" class="page-title" style="color: #1BA691">
                                        Employee Hobbies Setup</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            EMPLOYEE</label>
                                        <input id="aname" runat="server" class="form-control" type="text" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Name of Hobby *</label>
                                        <input id="hobbiesname" runat="server" class="form-control" type="text"
                                            />
                                       
                                    </div>
                                </div>
                                
                                
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                          Hobby Description *</label>
                                       <input id="hobbyDescriptions" runat="server" class="form-control" type="text"
                                            />
                                        
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            TRAINING RATING</label>
                                        <telerik:RadRating ID="hobbiesrate" runat="server" AutoPostBack="True" ToolTip="How you rate hobby"
                                            RenderMode="Lightweight" Skin="Bootstrap">
                                        </telerik:RadRating>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Always">
                                            <ContentTemplate>
                                                <label id="lbrating" runat="server"></label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="hobbiesrate" EventName="Rate" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                         
                  
                            <div class="row">
                                <div class="col-md-12 m-t-20">
                                    <button id="btnsave" runat="server" onserverclick="btnAdd_Click" type="submit" style="width: 150px"
                                        class="btn btn-success rounded">
                                        Save & Update</button>
                                    <button id="btnback" runat="server" onserverclick="btnCancel_Click" type="submit"
                                        style="width: 150px" class="btn btn-info rounded">
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
