<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMarker.ascx.cs"
    Inherits="DNNspot.Maps.Maps.EditMarker" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<table class="dnnFormItem">
    <tr>
        <td>
            Title:
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Address1:
        </td>
        <td>
            <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Address2:
        </td>
        <td>
            <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            City:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Region:
        </td>
        <td>
            <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Postal Code:
        </td>
        <td>
            <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Country:
        </td>
        <td>
            <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Custom Field:
        </td>
        <td>
            <asp:TextBox ID="txtCustomField" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Priority:
        </td>
        <td>
            <asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Phone Number:
        </td>
        <td>
            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            Info Window HTML:<br />
            <dnn:TextEditor id="txtInfoWindowHtml" runat="server" height="350" width="600" HtmlEncode="false">
            </dnn:TextEditor>
        </td>
    </tr>
    <tr>
        <td>
            Icon URL:
        </td>
        <td>
            <asp:TextBox ID="txtIconUrl" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Shadow URL:
        </td>
        <td>
            <asp:TextBox ID="txtShadowUrl" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Latitude:
        </td>
        <td>
            <asp:TextBox ID="txtLatitude" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Longitude:
        </td>
        <td>
            <asp:TextBox ID="txtLongitude" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Geocode marker on save?
        </td>
        <td>
            <asp:CheckBox ID="cbGeocodeOnSave" runat="server" />
        </td>
        <td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
            <asp:Button runat="server" ID="btnDelete" Text="Delete" Visible="false" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this marker?');" />
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function popitup(url) {
        newwindow = window.open(url, 'name', 'height=250,width=450');
        if (window.focus) { newwindow.focus() }
        return false;
    }

    function GeocodeMarker()
    {
        //var moduleId = <%= ModuleId %>;
        //var markerId = <%= Request.QueryString["id"] %>;
        var postUrl = "<%= ModuleWebPath %>GeocodeHandler.ashx?moduleId=" + moduleId + "&markerId=" + markerId;
        jQuery.post(postUrl, function (data) {
            jQuery('#<%= txtLongitude.ClientID.ToString() %>').val(data.longitude);
            jQuery('#<%= txtLatitude.ClientID.ToString() %>').val(data.latitude);
            jQuery('#<%= lblMessage.ClientID.ToString() %>').text(data.message);
        }, "json");
    }
</script>
