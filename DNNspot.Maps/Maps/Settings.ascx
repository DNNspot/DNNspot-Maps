<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DNNspot.Maps.Maps.Settings" %>
<script type="text/javascript">
    function getSelectedRadio(buttonGroup) {
       if (buttonGroup[0]) {
          for (var i=0; i<buttonGroup.length; i++) {
             if (buttonGroup[i].checked) {
                return i
             }
          }
       } else {
          if (buttonGroup.checked) { return 0; }
       }

       return -1;
    }

    function getSelectedRadioValue(buttonGroup) {
        var i = getSelectedRadio(buttonGroup);
        if (i == -1) {
            return "";
        } else {
            if (buttonGroup[i]) {
                return buttonGroup[i].value;
            } else {
                return buttonGroup.value;
            }
        }
    }

    function showPositioningOptions() {
        var selectedValue = parseInt(getSelectedRadioValue(document.forms[0].<%= rblMapPositioning.ClientID.Replace('_', '$') %>));
        var mapAddress = document.getElementById("mapAddress");
        var mapLatitude = document.getElementById("mapLatitude");
        var mapLongitude = document.getElementById("mapLongitude");
        var mapZoom = document.getElementById("mapZoom");

        switch (selectedValue) {
            case 0:
                mapAddress.style.display = 'none';
                mapLatitude.style.display = 'none';
                mapLongitude.style.display = 'none';
                mapZoom.style.display = 'none';
                break;
            case 1:
                mapAddress.style.display = 'inline-block';
                mapLatitude.style.display = 'none';
                mapLongitude.style.display = 'none';
                mapZoom.style.display = 'inline-block';
                break;
            case 2:
                mapAddress.style.display = 'none';
                mapLatitude.style.display = 'inline-block';
                mapLongitude.style.display = 'inline-block';
                mapZoom.style.display = 'inline-block';
                break;
        }
    }

    window.onload = showPositioningOptions;
</script>
<h1>
    Set the Headers for your CSV Import</h1>
    <p>If a custom token is not set, the default will be used when importing from a CSV file.</p>
<table>
    <thead>
        <tr>
            <th>
                Default Token
            </th>
            <th>
                Your Custom Header Token
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                Title
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                InfoWindowHtml
            </td>
            <td>
                <asp:TextBox ID="txtInfoWindowHtml" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Address1
            </td>
            <td>
                <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Address2
            </td>
            <td>
                <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                City
            </td>
            <td>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Region
            </td>
            <td>
                <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PostalCode
            </td>
            <td>
                <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Country
            </td>
            <td>
                <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Latitude
            </td>
            <td>
                <asp:TextBox ID="txtLatitude" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Longitude
            </td>
            <td>
                <asp:TextBox ID="txtLongitude" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Priority
            </td>
            <td>
                <asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                PhoneNumber
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                CustomField
            </td>
            <td>
                <asp:TextBox ID="txtCustomField" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                IconUrl
            </td>
            <td>
                <asp:TextBox ID="txtIconUrl" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                IconShadowUrl
            </td>
            <td>
                <asp:TextBox ID="txtIconShadowUrl" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
                <h1>
                    Map Options</h1>
            </td>
        </tr>
        <tr>
            <td>
                Google API Key
            </td>
            <td>
                <asp:TextBox ID="txtGoogleApiKey" runat="server"></asp:TextBox><small><a href="https://developers.google.com/maps/documentation/javascript/get-api-key" target="_blank">Get your API Key: https://developers.google.com/maps/documentation/javascript/get-api-key</a></small>
                
            </td>
        </tr>
        <tr>
            <td>
                Map Height
            </td>
            <td>
                <asp:TextBox ID="txtMapHeight" runat="server"></asp:TextBox>
                px
            </td>
        </tr>
        <tr>
            <td>
                Map Width
            </td>
            <td>
                <asp:TextBox ID="txtMapWidth" runat="server"></asp:TextBox>
                px
            </td>
        </tr>
        <tr>
            <td>
                Map Mode
            </td>
            <td>
                <asp:DropDownList ID="ddlMapMode" runat="server">
                    <asp:ListItem Value="ROADMAP">Map</asp:ListItem>
                    <asp:ListItem Value="SATELLITE">Satellite</asp:ListItem>
                    <asp:ListItem Value="HYBRID">Hybrid</asp:ListItem>
                    <asp:ListItem Value="TERRAIN">Terrain</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top">
                Map Position
            </td>
            <td>
                <asp:RadioButtonList ID="rblMapPositioning" runat="server">
                    <asp:ListItem Value="0" onclick="showPositioningOptions()" Selected="True">Automatic</asp:ListItem>
                    <asp:ListItem Value="1" onclick="showPositioningOptions()">Address</asp:ListItem>
                    <asp:ListItem Value="2" onclick="showPositioningOptions()">Coordinates</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <span id="mapAddress" style="display: none;">Address<br />
                    <asp:TextBox ID="txtMapAddress" runat="server"></asp:TextBox></span> <span id="mapLatitude"
                        style="display: none;">Latitude<br />
                        <asp:TextBox ID="txtMapLatitude" runat="server"></asp:TextBox></span> <span id="mapLongitude"
                            style="display: none;">Longitude<br />
                            <asp:TextBox ID="txtMapLongitude" runat="server"></asp:TextBox></span>
                <span id="mapZoom" style="display: none;">Zoom<br />
                    <asp:DropDownList ID="ddlMapZoom" runat="server">
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
            <td valign="top">
                Info Window & Filtering AutoZoom
            </td>
            <td>
                <asp:DropDownList ID="ddlAutoZoomLevel" runat="server">
                    <asp:ListItem Value="-1">Disabled</asp:ListItem>
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
            </td>
        </tr>
        <tr>
            <td>
                Show Country, State, City Filter?
            </td>
            <td>
                <asp:CheckBox ID="ckbShowFilter" Checked="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Hide Map Points on Page Load?
            </td>
            <td>
                <asp:CheckBox ID="ckbHideMapPointsOnPageLoad" Checked="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Show CustomField Filter?
            </td>
            <td>
                <asp:CheckBox ID="ckbShowCustomFieldFilter" Checked="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Show Proximity Search?
            </td>
            <td>
                <asp:CheckBox ID="ckbShowProximitySearch" Checked="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Custom Field Label
            </td>
            <td>
                <asp:TextBox ID="txtCustomFieldLabel" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Search Results Template
            </td>
            <td>
                <asp:TextBox ID="txtSearchResultsTemplate" runat="server" TextMode="MultiLine" Rows="7"
                    Columns="60"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
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
                    <li>[LINK] [/LINK] - Renders a starting link [LINK] and ending link [/LINK] to the particular
                        point in the map. The listing module MUST be on the same page as the map module.</li>
                    <li>[CUSTOMFIELD] - Renders a the CustomField for a marker</li>
                    <li>[PRIORITY] - Renders the priority field</li>
                    <li>[PHONENUMBER] - Renders the phone number field</li>
                    <li>[DIRECTIONSLINK][/DIRECTIONSLINK] - Render a directions link. Put the text you want between the tokens like this: [DIRECTIONSLINK]Find Directions[/DIRECTIONSLINK]</li>
                    <li>[IFPRIORITY][/IFPRIORITY] - Renders something if the priority field isn't blank</li>
                </ul>
            </td>
        </tr>
    </tbody>
</table>
