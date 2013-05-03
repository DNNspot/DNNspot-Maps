<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DNNspot.Maps.MarkerListing.Settings" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<table>
    <tr>
        <td>
            Maps Module to Use:<br />
            <asp:DropDownList runat="server" ID="ddlModuleToUse">
            </asp:DropDownList>
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            List Template HTML:<br />
            <asp:TextBox ID="txtListTemplate" runat="server" TextMode="MultiLine" Rows="7" Columns="60"></asp:TextBox>
        </td>
    </tr>
<%--    <tr>
        <td>
            Hyperlink listing title to map position?<br />
            <span style="font-size: 11px;">Note: This option requires the listing module and maps
                module to be on the same page</span>
        </td>
        <td>
            <asp:CheckBox ID="ckbLinkTitles" runat="server" />
        </td>
    </tr>--%>
    <tr>
        <td>
            Link Auto Zoom
            <span style="font-size: 11px;">Note: This will set the zoom level when someone clicks the link produced using the [LINK] and [/LINK] tokens in the template below</span>
        </td>
        <td>
            <span id="hyperlink">Zoom
                <asp:DropDownList ID="ddlLinkAutoZoom" runat="server">
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                    <asp:ListItem Value="3"></asp:ListItem>
                    <asp:ListItem Value="4"></asp:ListItem>
                    <asp:ListItem Value="5"></asp:ListItem>
                    <asp:ListItem Value="6"></asp:ListItem>
                    <asp:ListItem Value="7"></asp:ListItem>
                    <asp:ListItem Value="8"></asp:ListItem>
                    <asp:ListItem Value="9"></asp:ListItem>
                    <asp:ListItem Value="10"></asp:ListItem>
                    <asp:ListItem Value="11"></asp:ListItem>
                    <asp:ListItem Value="12"></asp:ListItem>
                    <asp:ListItem Value="13"></asp:ListItem>
                    <asp:ListItem Value="14"></asp:ListItem>
                    <asp:ListItem Value="15"></asp:ListItem>
                    <asp:ListItem Value="16"></asp:ListItem>
                    <asp:ListItem Value="17"></asp:ListItem>
                    <asp:ListItem Value="18"></asp:ListItem>
                    <asp:ListItem Value="19"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </td>
    </tr>
    <tr>
        <td>
            <strong>TOKENS:</strong>
            <ul>
                <li>[TITLE] - Renders the title of the marker</li>
                <li>[INFOWINDOWHTML] - Renders the HTML of the Info Window</li>
                <li>[ADDRESS1] - Renders Address 1</li>
                <li>[ADDRESS2] - Renders Address 2</li>
                <li>[CITY] - Renders the City</li>
                <li>[REGION] - Renders the Region</li>
                <li>[POSTALCODE] - Renders the Postal Code</li>
                <li>[COUNTRY] - Renders the Country</li>
                <li>[LATITUDE] - Renders the Latitude</li>
                <li>[LONGITUDE] - Renders the Longitude</li>
                <li>[ICONURL] - Renders the Icon Url</li>
                <li>[ICONSHADOWURL] - Renders the Icon Shadow Url</li>
                <li>[LINK] [/LINK] - Renders a starting link [LINK] and ending link [/LINK] to the particular point in the map. The listing module MUST be on the same page as the map module.</li>
                <li>[CUSTOMFIELD] - Renders a the CustomField for a marker</li>
                <li>[PRIORITY] - Renders the priority field</li>
            </ul>
        </td>
    </tr>
</table>
