<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="EmployeeCalendar.aspx.vb"
    Inherits="GOSHRM.EmployeeCalendar" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
  
    <title></title>
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
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        </style>
    <body>
        <form id="form1">
        <div>
            <table width="100%" >
                <tr>
                    <td class="style22">
                      
                        <asp:Label ID="lblView" runat="server" Font-Names="Candara" Font-Size="Medium" Width="100%"
                            Style="font-weight: 700; color: #FF6600"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>
        <%-- <div>
     </div>--%>
       
        <div style="height: 28px">
                <asp:Button ID="Button2" runat="server" Text="&lt; Back" BackColor="#1BA691" ForeColor="White"
                    Width="120px" Height="20px" BorderStyle="None" Font-Names="Verdana" 
                    Font-Size="11px" />
        </div>
        <div style="height: 163px">
            <div>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script type="text/javascript">
                    $(function () {
                        $("[id*=GridVwHeaderChckbox] td").hover(function () {
                            $("td", $(this).closest("tr")).addClass("hover_row");
                        }, function () {
                            $("td", $(this).closest("tr")).removeClass("hover_row");
                        })
                    })
                </script>
                <script type="text/javascript">
                    function openWindow(code) {
                        window.open("JobPostingsUpdate.aspx?id=" + code, "open_window", "width=800,height=800");
                    }
                </script>
                <script type="text/javascript">
                    function openApplicants(code) {
                        window.open("Applicants.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                    }
                </script>
                 <script type="text/javascript">
                     function openShortlists(code) {
                         window.open("ShortLists.aspx?Jobid=" + code, "open_window", "width=1000,height=800");
                     }
                </script>
                                                    <telerik:radscheduler runat="server" SelectedView="MonthView" 
                                                        SelectedDate="2016-09-08" EditFormDateFormat="dd/MM/yyyy" 
                                                        EditFormTimeFormat="HH:mm" 
                    AppointmentStyleMode="Simple" Height="561px" 
                                                        CustomAttributeNames="Activity" 
                    EnableDescriptionField="True" AllowEdit="False" 
                                                        AllowDelete="False" 
                    AllowInsert="False" RenderMode="Lightweight" 
                                                        ResolvedRenderMode="Classic" 
                    BackColor="#66CCFF" Width="100%" ID="schTmeSheet">
<ExportSettings>
<Pdf PageTopMargin="1in" PageBottomMargin="1in" PageLeftMargin="1in" 
        PageRightMargin="1in"></Pdf>
</ExportSettings>

<AdvancedForm Modal="True"></AdvancedForm>

<TimelineView UserSelectable="False"></TimelineView>

<WeekView UserSelectable="False"></WeekView>

<DayView UserSelectable="False"></DayView>

<MonthView VisibleAppointmentsPerDay="20"></MonthView>
<AppointmentTemplate>
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>' /><br />
                                                        
</AppointmentTemplate>
</telerik:RadScheduler>

            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
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
