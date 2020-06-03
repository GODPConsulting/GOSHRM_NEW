<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="QueriesUpdate.aspx.vb"
    Inherits="GOSHRM.QueriesUpdate" EnableEventValidation="false" Debug="true" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Add New</title>
    <script type="text/javascript">

        function closeWin() {
            popup.close();   // Closes the new window
        }
   

    </script>
</head>
<body>
    <form>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
       <div class="container col-md-10">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong id="msgalert" runat="server">Danger!</strong>
                    <asp:TextBox ID="txtid" runat="server" Width="3px"  
                    Font-Names="Candara" Height="2px" Visible="False"></asp:TextBox> 
                    <asp:Label ID="lblinitiator" runat="server" Font-Names="Verdana" 
                            Font-Size="12px" Text="Label" Visible="False"></asp:Label>
                </div>
                   <div class="panel panel-success">
                        <div class="panel-heading">
                            <h5><b id="pagetitle" runat="server">Raised Query</b></h5>
                        </div>
                     <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>QUERIED EMPLOYEE</label>
                                <input id="lblEmpName" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>ISSUER</label>
                                <input id="lblissuer" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                         <div class=" col-md-6">
                            <div class="form-group">
                                <label>QUERY DATE</label>
                                <input id="lblQueryDate" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>EXPECTED RESPONSE DATE</label>
                                <input id="lblexpecteddate" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    EXPECTED RESPONSE TIME</label>
                                <input id="lblexpectedtime" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>QUERY STATUS</label>
                                <input id="lblquerystatus" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                         <div class=" col-md-6">
                            <div class="form-group">
                                <label>QUERY MESSAGE</label>
                                <input id="lblquery" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>
                                    EMPLOYEE RESPONSE</label>   
                                <input id="lblEmployeeResponse" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>EMPLOYEE RESPONSE DATE</label>
                                <input id="lblEmpDate" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>EMPLOYEE RESPONSE STATUS</label>
                                <input id="lblEmpStatus" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>QUERY ISSUER'S RESPONSE</label>
                                <input id="lblrorersponse" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>QUERY RECOMMENDATION</label>
                                <telerik:RadComboBox ID="cborecommendation" runat="server"  ForeColor="#666666"
                                    Font-Names="Verdana" RenderMode="Lightweight" Skin="Bootstrap" Font-Size="12px" Width="100%">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class=" col-md-6">
                            <div class="form-group">
                                <label>HR RECOMMENDATION</label>
                                <input id="txtHRComment" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>UPDATED BY</label>
                                <input id="lblupdatedby" readonly="" runat="server" class="form-control" type="text" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>UPDATED ON</label>
                                <input id="lblupdatedon" readonly="" runat="server" class="form-control" type="text" />
                            </div>
                        </div>  
                        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                                <asp:Button ID="btnNotify" runat="server" Text="Notify Employee" 
                                        BackColor="#0099FF" ForeColor="White"
                            Width="150px" Height="35px" CssClass="btn btn-primary btn-info" BorderStyle="None" Font-Names="Verdana" Font-Size="12px" 
                                        Font-Bold="True" ToolTip="notify affected employee about recommendation" />
                        </div>
                    </div>
                </div>
            </div>
             </div>
            </div>
 
            <%--<table>
                <tr>
                    <td class="style1" colspan="2" style="background-color: #1BA691">
                        <strong>Raised Query</strong>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td class="style7">
                        <asp:TextBox ID="txtid" runat="server" Width="12px" Style="font-size: medium; font-family: Candara"
                            Font-Names="Candara" Height="20px" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                            Text="Querid Employee" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblEmpName" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#666666"
                            Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                            Text="Issuer" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblissuer" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                            Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666" Text="Query Date*" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblQueryDate" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Bold="True" ForeColor="#666666"
                            Text="Expected Response Date*" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblexpecteddate" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label9" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="Expected Response Time*"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblexpectedtime" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label11" runat="server" Font-Names="Verdana" Text="Query Status" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblquerystatus" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                            Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Text="Query Message" Font-Bold="True" ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblquery" runat="server" Font-Names="Verdana" Font-Size="12px"  ForeColor="#666666"
                            Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label13" runat="server" Font-Names="Verdana" Text="Employee Response" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblEmployeeResponse" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Employee Response Date" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblEmpDate" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Text="Employee Response Status" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblEmpStatus" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="lblrorersponsess" runat="server" Font-Names="Verdana" Text="Query Issuer's Response" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblrorersponse" runat="server" Font-Names="Verdana"  ForeColor="#666666" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label14" runat="server" Font-Names="Verdana" Text="Query Recommendation" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <telerik:RadComboBox ID="cborecommendation" runat="server"  ForeColor="#666666"
                            Font-Names="Verdana" Font-Size="12px" Width="100%">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        
                        <asp:Label ID="Label16" runat="server" Font-Names="Verdana" Font-Size="12px" Font-Bold="True" ForeColor="#666666"
                            Text="HR Comment"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:TextBox ID="txtHRComment" runat="server" BorderColor="#CCCCCC"  ForeColor="#666666"
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="12px" Height="150px" 
                            TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="Updated By" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblupdatedby" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="style6" valign="top">
                        <asp:Label ID="Label12" runat="server" Font-Names="Verdana" Text="Updated On" Font-Bold="True" ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblupdatedon" runat="server" Font-Names="Verdana"  ForeColor="#666666"
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                    </td>
                    <td class="style7">
                        <asp:Label ID="lblinitiator" runat="server" Font-Names="Verdana" 
                            Font-Size="12px" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Button ID="btnAdd" runat="server" Text="Save" BackColor="#1BA691" ForeColor="White"
                            Width="150px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px"
                            ToolTip="Save Query" Font-Bold="True" />
                    </td>
                    <td class="style7">
                        <table width ="100%" >
                            <tr>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="Close" BackColor="#FF3300" ForeColor="White"
                            Width="150px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                                        Font-Bold="True" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNotify" runat="server" Text="Notify Employee" 
                                        BackColor="#0099FF" ForeColor="White"
                            Width="150px" Height="25px" BorderStyle="None" Font-Names="Verdana" Font-Size="11px" 
                                        Font-Bold="True" ToolTip="notify affected employee about recommendation" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                </tr>
            </table>
        --%>
    </form>
</body>
</html>
</asp:Content> 