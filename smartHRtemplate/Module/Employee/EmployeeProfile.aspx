<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeProfile.aspx.vb"
    Inherits="GOSHRM.EmployeeProfile" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    
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
        <script>
            function ConfirmPln(id) {
                var ID = id;
                

                $.ajax({
                    url: "<%= Page.ResolveClientUrl("~/res_new/gos.asmx/Send_Return_Request") %>",
                method: 'post',
                dataType: 'json',
                data: { ID: ID },
                    success: function (data) {
                        console.log("am here")
                    var goat = document.getElementById('<%=btnSample.ClientID%>')
                  

                    document.getElementById('<%=btnSample.ClientID%>').click();
                },
                error: function (err) {
                    //alert(JSON.stringify(err));
                    $(err).each(function (index, prog) {
                        $('#msgbox2').css('display', 'block');
                        $("#pmsg").text(prog.responseText);
                    });
                }
            });
             }
        </script>
    <head>
        <title></title>
        <style type="text/css">       
          .roundedcorners
        {
            -webkit-border-radius: 120px;
            -khtml-border-radius: 120px;
            -moz-border-radius: 120px;
            border-radius: 120px;
        }
        </style>
    </head>
    <body>
        <form id="form1" action="">
        <div class="container col-md-12">
            <div class="row">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server">Danger!</strong>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                        <asp:Label ID="lblEmpID" runat="server" Font-Names="Verdana" Font-Size="1px" Visible="false"  ></asp:Label>
                    <asp:Button runat="server" ID="btnSample" Text="" style="display:none;" OnClick="btnReturnAsset_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8">
                    <h5 class="page-title">
                        Employee Profile</h5>
                </div>
            </div>
            <div class="card-box">
                <div class="row">
                        <div class="profile-view">
                            <div class="profile-img-wrap">
                                <div class="profile-img">
                                    <a href="#">
                                        <img id="imgphoto" runat="server" class="avatar img-responsive" onerror="this.onerror=null; this.src='~/images/blank-avatar.jpg';"
                                            src="~/images/blank-avatar.jpg" alt="" /></a>
                                </div>
                            </div>
                            <div class="profile-basic">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="profile-info-left">
                                            <h4 id="pempname" runat="server" class="user-name m-t-0 m-b-0" style="color: #1BA691">
                                                John Doe</h4>
                                            <div id="pempid" runat="server" class="staff-id" style="font-size:13px" >
                                                Employee ID: FT-0001</div>
                                            <div id="pmaritalstatus" runat="server" class="staff-id" style="font-size:13px">
                                                Marital Status: Single</div>
                                            <div id="pdob" runat="server" class="staff-id" style="font-size:13px">
                                                Date of Birth: 16-Oct-1966</div>
                                            <div id="pgender" runat="server" class="staff-id" style="font-size:13px">
                                                Gender: Male</div>
                                            <div id="pbloodgroup" runat="server" class="staff-id" style="font-size:13px">
                                                Blood Group: AA</div>
                                            <div id="pcountry" runat="server" class="staff-id" style="font-size:13px">
                                                Country of Birth: Nigeria</div>
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="personal-info">
                                            <li style="font-size:13px"><span class="title">Nationality:</span> <span id="pnationality" runat="server"
                                                class="text">American</span> </li>
                                            <li style="font-size:13px"><span class="title">ID Type:</span> <span id="pidtype" runat="server" class="text">
                                                Drivers License</span> </li>
                                            <li style="font-size:13px"><span class="title">ID No.:</span> <span id="pidnumber" runat="server" class="text">
                                                A086474356</span> </li>
                                            <li style="font-size:13px"><span class="title">ID Issuer:</span> <span id="pidissuer" runat="server" class="text">
                                                FRSC</span> </li>
                                            <li style="font-size:13px"><span class="title">ID Expiry:</span> <span id="pidexpirydate" runat="server"
                                                class="text">22-Jan-2018</span> </li>
                                            <li style="font-size:13px"><span class="title">Phone No:</span> <span id="pmobileno" runat="server" class="text">
                                                +123 4567 534 98</span> </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="personal-info">
                                            <%--<li style="font-size:13px"><span class="title">Personal Email:</span> <span id="personalmail" runat="server"
                                                class="text">godp@gmail.com</span> </li>--%>
                                            <li style="font-size:13px"><span class="title">Office Email:</span> <span id="pofficemail" runat="server"
                                                class="text">info@godp.com.ng</span> </li>
                                            <li style="font-size:13px"><span class="title">Address:</span> <span id="phomeaddress" runat="server" class="text">
                                                1861 Bayonne Ave, Manchester Township, NJ, 08759</span> </li>
                                            <li style="font-size:13px"><span class="title">Hire Date:</span> <span id="presumptiondate" runat="server"
                                                class="text">23-Jan-2016</span> </li>
                                            <li style="font-size:13px"><span class="title">Confirmation:</span> <span id="pconfirmationdate" runat="server"
                                                class="text">23-Jun-2016</span> </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="card-box m-b-0">
                        <h3 class="card-title" style="color: #1BA691;">
                            <b>Dependent</b></h3>
                        <div class="chat-action-btns">
                                        <ul>
                                            <li><a id="A4" runat="server" onserverclick="btnAddDependant_Click"  title='Add new dependant' class="edit-btn"><i class="glyphicon glyphicon-plus">
                                            </i></a></li>
                                        </ul>
                                    </div>
                        <asp:DataList ID="dlDependents" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                            RepeatLayout="Table" Font-Names="Arial" Font-Size="13px" DataKeyField="id" BorderColor="Transparent"
                            ForeColor="#666666" BorderWidth="1px" ToolTip="Chats">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                            <span class="text">
                                                <%# Eval("rows")%>.</span>
                                        </td>
                                        <td>
                                            <span class="title">Name:</span> <span class="text">
                                                <%# Eval("Dependants")%></span>
                                            <br />
                                            <span class="title">Relationship:</span> <span class="text">
                                                <%# Eval("Relationship")%></span>
                                            <br />
                                            <span class="title">Date of Birth:</span> <span class="text">
                                                <%# Eval("Date of Birth")%></span>  
                                            <div class="chat-action-btns">
                                            <a href="EmployeeDependant?id=<%# Eval("id")%>&self=emp" class="btn btn-default btn-sm m-t-10" title="Edit detail">Edit</a>
                                                </div>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="card-box m-b-0" style="margin-top: 10px;">
                        <h3 class="card-title" style="color: #1BA691;">
                            <b>Emergency Contact</b></h3>
                        <div class="chat-action-btns">
                                            <%--<a href="emergencycontacts?id=<%=lblEmpID.Text%>&self=emp" class="btn btn-default btn-sm m-t-10" title="Edit">Edit</a>--%>
                                                </div>
                        <h6 class="card-title" style="color: #1BA691;">
                            First Contact</h6>
                        <span class="title">Name:</span> <span id="emername1" runat="server" class="text">Etim
                            Essang</span>
                        <br />
                        <span class="title">Relationship:</span> <span id="emerrelationship1" runat="server"
                            class="text">Brother</span>
                        <br />
                        <span class="title">Phone Number:</span> <span id="emerphone1" runat="server" class="text">
                            +123 4567 534 98</span>
                        <br />
                        <span class="title">Address:</span> <span id="emeraddr1" runat="server" class="text">
                            1861 Bayonne Ave, Manchester Township, NJ, 08759</span>
                        <br />
                        <br />
                        <h6 class="card-title" style="color: #1BA691;">
                            Second Contact</h6>
                        <span class="title">Name:</span> <span id="emername2" runat="server" class="text">Etim
                            Essang</span>
                        <br />
                        <span class="title">Relationship:</span> <span id="emerrelationship2" runat="server"
                            class="text">Brother</span>
                        <br />
                        <span class="title">Phone Number:</span> <span id="emerphone2" runat="server" class="text">
                            +123 4567 534 98</span>
                        <br />
                        <span class="title">Address:</span> <span id="emeraddr2" runat="server" class="text">
                            1861 Bayonne Ave, Manchester Township, NJ, 08759</span>
                        
                        <br />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card-box m-b-0">
                        <h3 class="card-title" style="color: #1BA691;">
                            <b>Skills</b></h3>
                        <asp:DataList ID="dlskills" runat="server" Width="100%" RepeatColumns="1" CellSpacing="2"
                            RepeatLayout="Table" Font-Names="Arial" Font-Size="14px" DataKeyField="id" BorderColor="Transparent"
                            ForeColor="#666666" BorderWidth="1px">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div class="skills">
                                                <span>
                                                    <%# Eval("Skill")%></span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card-box">
                        <h4 class="card-title" style="color: #1BA691;">
                            <b>Certification</b></h4>
                                    <div class="chat-action-btns">
                                        <ul>
                                            <li><a id="A3" runat="server" onserverclick="btnAddCert_Click"  title='Add new Certificate' class="edit-btn"><i class="glyphicon glyphicon-plus">
                                            </i></a></li>
                                        </ul>
                                    </div>
                        <div class="experience-box">
                            <ul class="experience-list">
                                <li>                                    
                                    <asp:DataList ID="dlcertification" runat="server" Width="100%" RepeatColumns="1"
                                        CellSpacing="0" RepeatLayout="Flow" Font-Names="Arial" Font-Size="14px" DataKeyField="id"
                                        BorderColor="Transparent" ForeColor="#666666" BorderWidth="1px">
                                        <ItemTemplate>                                            
                                            <div class="experience-user">
                                                <div class="before-circle">
                                                </div>
                                            </div>
                                            <div class="experience-content">
                                                <div class="timeline-content">
                                                    <div class="chat-action-btns">
                                                        <ul>
                                                            <li><a id="A1" runat="server" title='<%# Eval("id")%>' class="del-msg"></a></li>
                                                        </ul>
                                                    </div>
                                                    <a href="EmployeeCertification?self=emp&id1=<%# Eval("id")%>" class="name">
                                                       <u> <%# Eval("Certification")%></u></a>
                                                    <div><%# Eval("Institute")%>
                                                        </div>
                                                    <span class="time">
                                                        <%# Eval("Date Granted")%>
                                                        -
                                                        <%# Eval("Expiry Date")%></span>
                                                </div>
                                            </div>                                            
                                        </ItemTemplate>                                        
                                    </asp:DataList>                                   
                                </li>
                            </ul>
                         
                        </div>
                    </div>
                    </div>
                     <div class="col-md-4">
                          <div class="card-box">
                        <h4 class="card-title" style="color: #1BA691;">
                            <b>Assets</b></h4>
                                    <div class="chat-action-btns">
                                        <ul>
                                            <%--<li><a id="A5" runat="server" onserverclick="btnAddCert_Click"  title='Add new Certificate' class="edit-btn"><i class="glyphicon glyphicon-plus">
                                            </i></a></li>--%>
                                        </ul>
                                    </div>
                        <div class="experience-box">
                            <ul class="experience-list">
                                <li>                                    
                                    <asp:DataList ID="DataList1" runat="server" Width="100%" RepeatColumns="1"
                                        CellSpacing="0" RepeatLayout="Flow" Font-Names="Arial" Font-Size="14px" DataKeyField="id"
                                        BorderColor="Transparent" ForeColor="#666666" BorderWidth="1px">
                                        <ItemTemplate>                                            
                                            <div class="experience-user">
                                                <div class="before-circle">
                                                </div>
                                            </div>
                                            <div class="experience-content">
                                                <div class="timeline-content">
                                                    <div class="chat-action-btns">
                                                        <ul>
                                                            <li><a id="A1" runat="server" title='<%# Eval("id")%>' class="del-msg"></a></li>
                                                        </ul>
                                                    </div>
                                                    
                                                       <u> <%# Eval("Asset Name")%> <span style="margin-left:30px"> <a href="#" onclick='ConfirmPln(<%# Eval("id")%>);'>  <button class="btn btn-default btn-sm 
glyphicon glyphicon-repeat"   type="button" runat="server" data-toggle="tooltip" data-original-title="Return Asset"></button></a></span>
                                                           <span style="margin-left:30px">   <a href="EmployeeAssetReturn?id1=<%# Eval("id")%>" class="name"><button class="btn btn-default btn-sm 
glyphicon glyphicon-eye-open" type="button" runat="server" data-toggle="tooltip" data-original-title="View Asset"></button></a></span>
                                                       </u>
                                                    <div><%# Eval("Asset Number")%>
                                                        </div>
                                                    <span class="time">
                                                        <%# Eval("Location")%>
                                                        -
                                                        <%# Eval("Status")%></span>
                                                </div>
                                            </div>                                            
                                        </ItemTemplate>                                        
                                    </asp:DataList>                                   
                                </li>
                            </ul>
                         
                        </div>
                     </div>
                         </div>
                    <div class="col-md-4">
                    <div class="card-box">
                        <h3 class="card-title" style="color: #1BA691;">
                            <b>Education</b></h3>
                            <div class="chat-action-btns">
                                        <ul>
                                            <li><a id="A2" runat="server" onserverclick="btnAddEducation_Click" title='Add new education' class="edit-btn"><i class="glyphicon glyphicon-plus">
                                            </i></a></li>
                                        </ul>
                                    </div>
                        <div class="experience-box">
                            <ul class="experience-list">
                                <li>
                                    <asp:DataList ID="dlEducation" runat="server" Width="100%" RepeatColumns="1" CellSpacing="0"
                                        RepeatLayout="Flow" Font-Names="Arial" Font-Size="14px" DataKeyField="id" BorderColor="Transparent"
                                        ForeColor="#666666" BorderWidth="1px">
                                        <ItemTemplate>
                                            <div class="experience-user">
                                                <div class="before-circle">
                                                </div>
                                            </div>
                                            <div class="experience-content">
                                                <div class="timeline-content">
                                                    <div class="chat-action-btns">
                                                        <ul>
                                                            <li><a id="A1" runat="server" title='<%# Eval("id")%>' class="del-msg"></a></li>
                                                        </ul>
                                                    </div>
                                                    <a href="EmployeeEducation?self=emp&id1=<%# Eval("id")%>" class="name">
                                                       <u> <%# Eval("Qualification")%></u></a>
                                                    <div>
                                                        <%# Eval("Institute")%></div>
                                                    <span class="time">
                                                        <%# Eval("Start Date")%>
                                                        -
                                                        <%# Eval("Completed On")%></span>
                                                        
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                  <div class="col-md-4">
                          <div class="card-box">
                        <h4 class="card-title" style="color: #1BA691;">
                            <b>Hobbies</b></h4>
                                    <div class="chat-action-btns">
                                        <ul>
                                            <li><a id="A5" runat="server" onserverclick="btnAddHobby_Click"  title='Add new Hobby' class="edit-btn"><i class="glyphicon glyphicon-plus">
                                            </i></a></li>
                                        </ul>
                                    </div>
                        <div class="experience-box">
                            <ul class="experience-list">
                                <li>                                    
                                    <asp:DataList ID="DataList2" runat="server" Width="100%" RepeatColumns="1"
                                        CellSpacing="0" RepeatLayout="Flow" Font-Names="Arial" Font-Size="14px" DataKeyField="id"
                                        BorderColor="Transparent" ForeColor="#666666" BorderWidth="1px">
                                        <ItemTemplate>                                            
                                            <div class="experience-user">
                                                <div class="before-circle">
                                                </div>
                                            </div>
                                            <div class="experience-content">
                                                <div class="timeline-content">
                                                    <div class="chat-action-btns">
                                                        <ul>
                                                            <li><a id="A1" runat="server" title='<%# Eval("id")%>' class="del-msg"></a></li>
                                                        </ul>
                                                    </div>
                                                    <a href="EmployeeHobbies?id1=<%# Eval("id")%>" class="name">
                                                       <u> <%# Eval("Hobby Name")%></u></a>
                                                    <div style=""> <telerik:RadRating ID="hobbiesrate" runat="server" Value='<%# Eval("Hobby rating")%>' AutoPostBack="False" ToolTip="How you rate hobby"
                                            RenderMode="Lightweight" ReadOnly="true" Skin="Bootstrap">
                                        </telerik:RadRating>
                                                        </div>
                                                    
                                                </div>
                                            </div>                                            
                                        </ItemTemplate>                                        
                                    </asp:DataList>                                   
                                </li>
                            </ul>
                         
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
    <style type="text/css">
        .style21
        {
            width: 100%;
        }
        .style22
        {
        }
    </style>
</asp:Content>
