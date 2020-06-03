<%@ Page Language="vb" MasterPageFile="~/smartHR.Master" AutoEventWireup="true" CodeBehind="CompaniesUpdate.aspx.vb"
    Inherits="GOSHRM.CompaniesUpdate" EnableEventValidation="false" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
  
    </head>
  
    <body>
        <form id="form1" action="">
        <div class="container col-md-10">
        <div class="row">
            <div class="col-md-12">
                <div id="divalert" runat="server" visible="false" class="alert alert-info">
                    <strong id="msgalert" runat="server"></strong>
                </div>
            </div>
        </div>
         <div class="panel panel-success">
                <div class="panel-heading">
                    <h5><b id="pagetitle" runat="server">Third Party Companies</b></h5>
                     <asp:TextBox ID="txtid" runat="server" Visible="False" Height="1px" Width="1px"></asp:TextBox>
                </div>
             <div class="panel-body">
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        NAME*</label>
                        <input id="aname" runat="server" class="form-control" type="text" placeholder="Company name" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        CONTACT PERSON*</label>
                        <input id="acontactperson" runat="server" class="form-control" type="text" placeholder="Company staff" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        CONTACT NUMBER*</label>
                        <input id="aphonenumber" runat="server" class="form-control" type="text" placeholder="Telephone number" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                        EMAIL ADDRESS*</label>
                        <input id="aemailaddr" runat="server" class="form-control" type="text" placeholder="Company email address" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                         ADDRESS*</label>
                        <textarea id="aaddress" runat="server" class="form-control" rows="4" cols="1" placeholder="Residential Address"></textarea>
                </div>
            </div>
        </div>

        <div class="row">
            <div class=" col-md-12">
                <div class="form-group">
                    <label>
                         INDUSTRY*</label>                    
                    <telerik:RadDropDownList ID="drcompany" runat="server" 
                        RenderMode="Lightweight" ResolvedRenderMode="Classic" SelectedText="Aerospace" 
                        SelectedValue="Aerospace" Skin="Bootstrap" Width="100%">
                        <Items>
                            <telerik:DropDownListItem runat="server" Selected="True" Text="Aerospace" 
                                Value="Aerospace" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Agriculture / Forestry" 
                                Value="Agriculture/Forestry" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Automotive" Value="Automotive" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Business Services / Consultancy - Non IT" 
                                Value="Business Services/Consultancy" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Computer / Technology - Reseller (inc VAR)" 
                                Value="Computer/Technology-Reseller" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Computer / Technology - Services / Consultancy" 
                                Value="Computer/Technology-Services" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Computer / Technology-Manufacturer" 
                                Value="Computer/Technology-Manufacturer" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Education" Value="Education" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Electronics" Value="Electronics" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Finance / Banking / Insurance / Real Estate / Legal" 
                                Value="Finance/Banking/Insurance/Real Estate/Legal" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Government - National / Federal (inc Military)" 
                                Value="Government-National/Federal" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Government - Local" 
                                Value="Government-Local" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Healthcare" Value="Healthcare" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Manufacturing - Non Computer Related" 
                                Value="Manufacturing-Non Computer Related" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" 
                                Text="Media / Marketing / Entertainment / Publishing / PR" Value="media" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Retail / Wholesale" 
                                Value="retail" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Telecoms / Communications" 
                                Value="Telecoms/Communications" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Transportation / Distribution" 
                                Value="Transportation/Distribution" DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Travel" Value="travel" 
                                DropDownList="drcompany" />
                            <telerik:DropDownListItem runat="server" Text="Other" Value="other" 
                                DropDownList="drcompany" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
        </div>

        <div class="row">
        <div class="col-md-12 m-t-20 text-center">
                            <button id="btnupdate" runat="server" onserverclick="btnAdd_Click" type="submit"
                                style="width: 150px" class="btn btn-primary btn-success">
                                Save &amp; Update</button>
                            <button id="Button1" runat="server" onserverclick="btnCancel_Click" type="submit" style="width: 150px"
                                class="btn btn-primary btn-danger">
                                << Back</button>
                        </div>
</div>
       </div></div></div>
        </form>
    </body>
    </html>
</asp:Content>
