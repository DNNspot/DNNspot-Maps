 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadMarkers.ascx.cs"
    Inherits="DNNspot.Maps.Maps.UploadMarkers" %>
<%@ Import Namespace="DNNspot.Maps" %>
<%@ Import Namespace="DNNspot.Maps.Maps" %>
<table width="100%">
    <tbody>
        <tr>
            <td>
                <p>
                    <strong>Upload a CSV File: </strong>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <p>Markers are geocoded for a latitude and longitude upon upload.</p>
                <asp:FileUpload ID="fupCsvFile" runat="server" />
                <br />
                <br />
                <asp:Button ID="btnUpload" Text="Upload File" runat="server" OnClick="btnUpload_Click" OnClientClick="if (!validatePage()) {return false;}" UseSubmitBehavior="false" />
                <asp:Label ID="litMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <p>Please note: Google Maps API limits geocoding to 2,500 points per day.</p>            
            </td>
        </tr>
        <asp:Panel ID="pnlGeocodeMarkers" runat="server">
            <tr>
                <td>
                    <p>
                        <br />
                        <%--<strong><a href="javascript: void(0);" onclick="return popitup('<%= ModuleWebPath %>Maps/Geocode.html?moduleId=<%= ModuleId %>')">
                            Geocode module markers</a></strong>--%>
                            <asp:Button id="btnGeocodeRemainingMarkers" OnClick="btnGeocodeRemainingMarkers_Click" runat="server" Text="Geocode Remaining Markers" />
                    </p>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>
                <br />
                <hr />
                <br />
                <ul>
                    <li>
                        <asp:HyperLink ID="lnkDownload" runat="server">Download Last File Upload</asp:HyperLink></li>
                    <li><a href="<%= ModuleWebPath %>Maps/uploads/example.csv">Download Example</a></li>
                    <li><asp:LinkButton ID="lbDownloadCsv" runat="server" Text="Download Current Markers" onclick="lbDownloadCsv_Click"></asp:LinkButton></li>
                </ul>
            </td>
        </tr>
    </tbody>
</table>
<script type="text/javascript">
    function popitup(url) {
        newwindow = window.open(url, 'name', 'height=250,width=450');
        if (window.focus) { newwindow.focus() }
        return false;
    }

    function validatePage() {
        if(confirm('Are you sure you want to override any existing markers for this module?'))
        {
            jQuery("#<%= btnUpload.ClientID %>").val('Processing...');
            jQuery("#<%= btnUpload.ClientID %>").attr("disabled", true);
            jQuery("#<%= litMessage.ClientID %>").text("Uploading and geocoding markers. Please wait.");
            return true;
        }
        else
        {
            return false;
        }
    }

    jQuery(function () {
        jQuery("#<%= btnUpload.ClientID %>").val('Upload File');
        jQuery("#<%= btnUpload.ClientID %>").removeAttr("disabled");
    });
</script>
