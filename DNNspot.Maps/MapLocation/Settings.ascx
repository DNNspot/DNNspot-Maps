<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DNNspot.Maps.MapLocation.Settings" %>
<table>
    <tr>
        <td valign="top">Redirect Page</td>
        <td><asp:DropDownList ID="ddlTabs" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Search Label</td>
        <td><asp:TextBox ID="txtSearchLabelText" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top">Search Button Text</td>
        <td><asp:TextBox ID="txtSearchButtonText" runat="server"></asp:TextBox></td>
    </tr>
</table>