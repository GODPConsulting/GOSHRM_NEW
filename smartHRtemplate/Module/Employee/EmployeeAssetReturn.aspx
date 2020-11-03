<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/smartHR.Master" CodeBehind="EmployeeAssetReturn.aspx.vb" Inherits="GOSHRM.EmployeeAssetReturn" EnableEventValidation="true" %>


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
                                        Employee Asset Setup</h5>
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
                                            Name of Asset *</label>
                                        <input id="assetsname"  runat="server"  type="text" disabled="disabled" class="form-control"
                                            />
                                       
                                    </div>
                                </div>
                               
                                
                            </div>
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Asset Number*</label>
                                        <input id="assetsnumber" runat="server" disabled="disabled" type="text" class="form-control"
                                            />
                                       
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Classificaion</label>
                                        <input id="classifications" runat="server" disabled="disabled" type="text" class="form-control"
                                       />
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                <div class=" col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Asset Description *</label>
                                            <input id="assetsdescription" runat="server" disabled="disabled" type="text" class="form-control"
                                            />
                                        
                                    </div>
                                </div>
                                </div>
                            
                            <div class="row">
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Physical Condition*</label>
                                        <input id="physicalconditions" runat="server"  disabled="disabled" type="text" class="form-control"
                                            />
                                        
                                    </div>
                                </div>
                                <div class=" col-md-6">
                                    <div class="form-group">
                                        <label>
                                            LOCATION *</label>
                                      <input id="locations" runat="server" type="text" disabled="disabled" class="form-control" />
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                     <div class="form-group">
                                                        <label>
                                                            Status</label>
                                                        <telerik:RadComboBox ID="RadComboBox2" runat="server" AutoPostBack="True" Skin="Bootstrap"
                                                            ForeColor="#666666" RenderMode="Lightweight" Width="100%" ResolvedRenderMode="Classic">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="--select--" Value="--select--" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Active" Value="Active" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Inactive" Value="Inactive" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Returned" Value="Returned" />
                                                               
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                         <label>
                                            Comments *</label>
                                        <input type="text" runat="server" id="comments" disabled="disabled" class="form-control" />
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


