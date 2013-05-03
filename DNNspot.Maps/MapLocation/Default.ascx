<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="DNNspot.Maps.MapLocation.Default" %>
<div class="searchForm">
    Enter your address to see a map of locations near you.
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <span class="line">Address:
        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtAddress"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="error"
            ControlToValidate="txtAddress" Display="None" ErrorMessage="Please enter your address"></asp:RequiredFieldValidator>
    </span><span class="line">Within:
        <asp:DropDownList ID="rdoRadius" CssClass="rdoRadius" runat="server">
            <asp:ListItem Value="1">1 mile</asp:ListItem>
            <asp:ListItem Value="5" Selected="True">5 miles</asp:ListItem>
            <asp:ListItem Value="10">10 miles</asp:ListItem>
            <asp:ListItem Value="15">15 miles</asp:ListItem>
            <asp:ListItem Value="1000000">All of MI</asp:ListItem>
        </asp:DropDownList>
    </span><span class="line">Need a specialist?
        <asp:DropDownList ID="ddlSpecialty" runat="server" CssClass="ddlSpecialty">
        </asp:DropDownList>
    </span><span class="line">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnSearch" OnClick="btnSearch_Click"
            OnClientClick="submitSearch()" />
    </span>
    <div class="loading" style="display: none;">
        Searching...
        <img src="<%=ModuleBaseWebPath %>images/loading_bar.gif" alt="loading..." />
    </div>
</div>
<asp:Panel ID="pnlUnableToGeocode" runat="server" Visible="false" CssClass="panel"
    EnableViewState="false">
    <h3 style="margin: 8px 4px; font-size: 12px;">
        Sorry, we could not locate your address as entered. Please try a different variation
        of the address.</h3>
</asp:Panel>
<asp:Panel ID="pnlNoMatches" runat="server" Visible="false" CssClass="panel" EnableViewState="false">
    <h3 style="margin: 8px 4px; font-size: 12px;">
        We currently do not have any locations that match your criteria</h3>
</asp:Panel>
<asp:Label ID="lblSearchLabel" runat="server"></asp:Label>
<asp:TextBox ID="txtMapLocation" runat="server"></asp:TextBox>
<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />