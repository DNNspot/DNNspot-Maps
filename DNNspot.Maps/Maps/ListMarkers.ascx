<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListMarkers.ascx.cs" Inherits="DNNspot.Maps.Maps.ListMarkers" %>
<asp:LinkButton ID="lbDownloadCsv" runat="server" 
    Text="Download Current Markers" onclick="lbDownloadCsv_Click"></asp:LinkButton>
<asp:Repeater ID="rptMarkers" runat="server">
    <HeaderTemplate>
        <table width="100%" id="listMarkers" class="TabularData" cellspacing="0">
            <thead>
                <tr>
                    <th></th>
					<th>Title</th>
					<th>Address1</th>
					<th>Address2</th>
					<th>City</th>
					<th>Region</th>
					<th>Postal Code</th>
                    <th>Country</th>
					<th>Priority</th>
					<th>Geocoded?</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="<%# (Container.ItemIndex % 2 == 0) ? "" : "TableRowAlt" %>">
            <td><a href="<%# EditUrl("id", Eval("MarkerId").ToString(),"EditMarker") %>"><strong>Edit</strong></a></td>
            <td><%# Eval("Title")%></td>
            <td><%# Eval("Address1")%></td>
            <td><%# Eval("Address2")%></td>
            <td><%# Eval("City")%></td>
            <td><%# Eval("Region")%></td>
            <td><%# Eval("PostalCode")%></td>
            <td><%# Eval("Country")%></td>
            <td><%# Convert.ToString(Eval("Priority")) == "zzzz" ? "" : Eval("Priority")%></td>
            <td><%# Eval("Latitude") != null ? "Yes" : "No"%></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody> </table>
    </FooterTemplate>
</asp:Repeater>