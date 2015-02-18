<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="DNNspot.Maps.Maps.Default" %>
<%@ Import Namespace="DNNspot.Maps" %>
<%@ Import Namespace="DNNspot.Maps.Maps" %>
<script type="text/javascript">
    var baseServiceUrl = "<%= GetModuleFolderPath() %>Maps/Services";
    var moduleId = '<%= ModuleId %>';
    var maxPoints = '<%= maxPoints %>';
</script>
<asp:Panel ID="pnlCustomSearchFilter" runat="server" Visible="true">
    <div class="distance">Starting Address:<br /><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></div>
    <div class="customFilter"><%= CustomFieldLabel %>:<br /><asp:DropDownList ID="ddlCustomFilter" runat="server"></asp:DropDownList></div>
    <div class="proximity">Distance:<br />
        <asp:DropDownList ID="ddlProximity" runat="server">
            <asp:ListItem text="5 miles" value="5"></asp:ListItem>
            <asp:ListItem text="10 miles" value="10"></asp:ListItem>
            <asp:ListItem text="15 miles" value="15"></asp:ListItem>
            <asp:ListItem text="30 miles" value="30"></asp:ListItem>
            <asp:ListItem text="50 miles" value="50"></asp:ListItem>
            <asp:ListItem text="75 miles" value="75"></asp:ListItem>
            <asp:ListItem text="100 miles" value="100"></asp:ListItem>
            <asp:ListItem text="1000 miles" value="1000"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="searchBtn">
        <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
    </div>
</asp:Panel>
<div id="directionsPanel-<%= ModuleId %>"></div>




<asp:Panel ID="pnlNoMatches" runat="server" Visible="false" CssClass="noMatches" EnableViewState="false">
    <h3 style="margin: 8px 4px; font-size: 12px;"><asp:Literal ID="litError" runat="server"></asp:Literal>
        </h3>
</asp:Panel>
<asp:Panel ID="pnlCustomMapFilter" runat="server" Visible="false">
    <div id="mapCustomFilter<%= ModuleId %>">
        <div class="containSolution">
            <label>
                Solution<br />
                <asp:DropDownList CssClass="customField" ID="ddlCustomField" runat="server">
                </asp:DropDownList>
            </label>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlMapFilters" runat="server" Visible="false">
    <div id="mapFilter<%= ModuleId %>">
        <div class="containCountry">
            <label>
                Country<br />
                <asp:DropDownList CssClass="countries" ID="ddlCountries" runat="server">
                </asp:DropDownList>
            </label>
        </div>
        <div class="containRegion" style="display: none;">
            <label>
                Region<br />
                <select id="states<%= ModuleId %>">
                </select>
            </label>
        </div>
        <div class="containCity" style="display: none;">
            <label>
                City<br />
                <select id="cities<%= ModuleId %>">
                </select>
            </label>
        </div>
    </div>
</asp:Panel>
<asp:Literal ID="litMarkers" runat="server"></asp:Literal>
<script type="text/javascript">
    var map_<%= ModuleId %>;
    var mapLatLngBounds_<%= ModuleId %> = new google.maps.LatLngBounds();
    var mapMarkers_<%= ModuleId %> = [];
    var lastInfoWindow_<%= ModuleId %>;
    
    var directionsDisplay_<%= ModuleId %>;
    var directionsService_<%= ModuleId %> = new google.maps.DirectionsService();


    jQuery(function($) {
        //if(markers_<%= ModuleId %>.length > 0)
            initialize_<%= ModuleId %>(markers_<%= ModuleId %>);

            <% if (pnlMapFilters.Visible) { %>
                jQuery("#<%= ddlCountries.ClientID %>").change(function () {
                    var postUrl = baseServiceUrl + "/CountriesChange.ashx";
                    var customField = jQuery("#<%= ddlCustomField.ClientID %>").val();
                    var country = jQuery(this).val();
                    jQuery(".containCity").hide(); 

                    var postVars = { 'moduleId': moduleId, 'customField': customField, 'country': country, 'maxPoints': '<%= maxPoints %>' };
                    jQuery.post(postUrl, postVars, function (data) {
                        if (data.success) {
                            jQuery("#states<%= ModuleId %>").html(data.states);
                            if(data.states.length > 0)
                            {                                   
                                initialize_<%= ModuleId %>(data.markers);    
                                  
                                jQuery(".containRegion").show();                                                                    
                            }
                        }
                    });
                });

                jQuery("#states<%= ModuleId %>").change(function () {
                    var postUrl = baseServiceUrl + "/StatesChange.ashx";
                    var customField = jQuery("#<%= ddlCustomField.ClientID %>").val();
                    var country = jQuery("#<%= ddlCountries.ClientID %>").val();
                    var state = jQuery(this).val();

                    var postVars = { 'moduleId': moduleId, 'customField': customField, 'country': country, 'state': state, 'maxPoints': '<%= maxPoints %>' };
                    jQuery.post(postUrl, postVars, function (data) {
                        if (data.success) {
                            jQuery("#cities<%= ModuleId %>").html(data.cities);
                            if(data.markers.length > 0)
                            {                                   
                                initialize_<%= ModuleId %>(data.markers);                      

                                jQuery(".containCity").show();    
                            }
                        }
                    });
                });

                jQuery("#cities<%= ModuleId %>").change(function () {                    
                    var postUrl = baseServiceUrl + "/CitiesChange.ashx";
                    var customField = jQuery("#<%= ddlCustomField.ClientID %>").val();
                    var country = jQuery("#<%= ddlCountries.ClientID %>").val();
                    var state = jQuery("#states<%= ModuleId %>").val();
                    var city = jQuery(this).val();

                    var postVars = { 'moduleId': moduleId, 'customField': customField, 'country': country, 'state': state, 'city': city, 'maxPoints': '<%= maxPoints %>' };
                    jQuery.post(postUrl, postVars, function (data) {
                        if (data.success) {                            
                            if(data.markers.length > 0)
                            {   
                                initialize_<%= ModuleId %>(data.markers);                                                         
                            }
                        }
                    });
                });

            <% } %>

            <% if (pnlCustomMapFilter.Visible) { %>
                jQuery("#<%= pnlCustomMapFilter.ClientID %>").change(function () {
                    var postUrl = baseServiceUrl + "/CustomFieldChange.ashx";
                    var customField = jQuery("#<%= ddlCustomField.ClientID %>").val();
                    var country = "";
                    var state = "";
                    var city = "";

                    if(jQuery("#<%= ddlCountries.ClientID %>")[0].selectedIndex >= 1)
                    {
                        //country = jQuery("#<%= ddlCountries.ClientID %>").val();
                    }

                    if(jQuery("#states<%= ModuleId %>")[0].selectedIndex >= 1)
                    {
                        //state = jQuery("#states<%= ModuleId %>").val();
                    }

                    if(jQuery("#cities<%= ModuleId %>")[0].selectedIndex >= 1)
                    {
                        //city = jQuery("#cities<%= ModuleId %>").val();
                    }

                    jQuery(".containCity").hide();
                    jQuery(".containRegion").hide();

                    var postVars = { 'moduleId': moduleId, 'customField': customField, 'country': country, 'state': state, 'city': city,, 'maxPoints': '<%= maxPoints %>' };
                    jQuery.post(postUrl, postVars, function (data) {
                        if (data.success) {
                            jQuery("#<%= ddlCountries.ClientID %>").html(data.countries);
                            jQuery("#states<%= ModuleId %>").html(data.states);
                            jQuery("#cities<%= ModuleId %>").html(data.cities);
                            if(data.markers.length > 0)
                            {                                   
                                initialize_<%= ModuleId %>(data.markers);                      

                            }

                            jQuery("#<%= ddlCountries.ClientID %>").val(country);
                            jQuery("#states<%= ModuleId %>").val(state);
                            jQuery("#cities<%= ModuleId %>").val(city);
                        }
                    });
                });
            <% } %>

    });

    function initialize_<%= ModuleId %>(markers) {
        mapLatLngBounds_<%= ModuleId %> = new google.maps.LatLngBounds();
        directionsDisplay_<%= ModuleId %> = new google.maps.DirectionsRenderer();

        <% if(HidePointsOnPageLoad && IsSearch == false) { %>
            var mapOptions_<%= ModuleId %> = {
                zoom: <%= MapZoom ?? Settings[ModuleSettingNames.MapZoom] ?? "10" %>,
                mapTypeId: google.maps.MapTypeId.<%= Settings[ModuleSettingNames.MapMode] ?? "ROADMAP" %>
            };
        <% } else { %>
            var mapOptions_<%= ModuleId %> = {
                zoom: <%= MapZoom ?? Settings[ModuleSettingNames.MapZoom] ?? "10" %>,
                center: new google.maps.LatLng(markers[0].Latitude, markers[0].Longitude),
                mapTypeId: google.maps.MapTypeId.<%= Settings[ModuleSettingNames.MapMode] ?? "ROADMAP" %>
            };
        <% } %>

        map_<%= ModuleId %> = new google.maps.Map(document.getElementById("DNNspot-Maps-Map-<%=ModuleId%>"), mapOptions_<%= ModuleId %>);

        setMarkers_<%= ModuleId %>(map_<%= ModuleId %>, markers);
        
        <% if ((MapLocation == null && (string)(Settings[ModuleSettingNames.MapPositioning] ?? "0") == "0") || IsSearch) { %>

            map_<%= ModuleId %>.fitBounds(mapLatLngBounds_<%= ModuleId %>);

        <% } else if (MapLocation != null || (string)(Settings[ModuleSettingNames.MapPositioning] ?? "0") == "1") { %>
            geocoder = new google.maps.Geocoder();

            geocoder.geocode( { 'address': "<%= MapLocation ?? Settings[ModuleSettingNames.MapAddress] %>"}, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    map_<%= ModuleId %>.setCenter(results[0].geometry.location);
                }
            }); 

        <% } else if ((string)(Settings[ModuleSettingNames.MapPositioning] ?? "0") == "2") { %>
        
            map_<%= ModuleId %>.setCenter(new google.maps.LatLng(<%= Settings[ModuleSettingNames.MapLatitude] %>, <%= Settings[ModuleSettingNames.MapLongitude] %>));

        <% } %>

        <% if(MapZoom != null) { %>
            setTimeout('map_<%= ModuleId %>.setZoom(<%= MapZoom %>)', 250);
        <% } %>   

        directionsDisplay_<%= ModuleId %>.setMap(map_<%= ModuleId %>);
        directionsDisplay_<%= ModuleId %>.setPanel(document.getElementById("<%= pnlSearchResultsListing.ClientID %>"));

    }

    function setMarkers_<%= ModuleId %>(map, markers) {
        for (var i = 0; i < markers.length; i++) {
            var marker = markers[i];
            var markerLatLng = new google.maps.LatLng(marker.Latitude, marker.Longitude);
            mapMarkers_<%= ModuleId %>[i] = new Object();

            mapLatLngBounds_<%= ModuleId %>.extend(markerLatLng);

            mapMarkers_<%= ModuleId %>[i].infoWindow = new google.maps.InfoWindow({
                content: '<div class=\'dnnspotMapsInfoWindow\'>' + marker.InfoWindowHtml + '</div>'
            });

            mapMarkers_<%= ModuleId %>[i].marker = new google.maps.Marker({
                map: map_<%= ModuleId %>,
                position: markerLatLng,
                title: marker.Title,
                icon: marker.IconUrl,
                shadow: marker.IconShadowUrl
            });

            mapMarkers_<%= ModuleId %>[i].listener = addMarkerListener_<%= ModuleId %>(i, mapMarkers_<%= ModuleId %>[i].marker);
        }
    }

    function addMarkerListener_<%= ModuleId %>(i, marker) {
        var listener = google.maps.event.addListener(marker, 'click', function() {
            openInfoWindow_<%= ModuleId %>(i);
        });
        return listener ;
    }
 
    function openInfoWindow_<%= ModuleId %>(i) {
        if ( typeof(lastInfoWindow_<%= ModuleId %>) == 'number' && typeof(mapMarkers_<%= ModuleId %>[lastInfoWindow_<%= ModuleId %>].infoWindow) == 'object' ) { 
            mapMarkers_<%= ModuleId %>[lastInfoWindow_<%= ModuleId %>].infoWindow.close() ;
        }

        lastInfoWindow_<%= ModuleId %> = i;    
        
        mapMarkers_<%= ModuleId %>[i].infoWindow.open(map_<%= ModuleId %>, mapMarkers_<%= ModuleId %>[i].marker) ; 
        
        if ( (<%= AutoZoomLevel %> > -1) )
        {
            map_<%= ModuleId %>.setZoom( <%= AutoZoomLevel %> ) ;
        }
    }

    function calculateDirections_<%= ModuleId %>(latitude, longitude)
    {
        var start = jQuery("#<%= txtAddress.ClientID %>").val();
        var end = new google.maps.LatLng(latitude, longitude);

        var request = {
            origin:start,
            destination:end,
            travelMode: google.maps.TravelMode.DRIVING
        };
  
        directionsService_<%= ModuleId %>.route(request, function(response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                jQuery("#<%= pnlSearchResultsListing.ClientID %>").html("");
                deleteOverlays_<%= ModuleId %>();
                directionsDisplay_<%= ModuleId %>.setDirections(response);
            }
        });

    }

    function selectPoint_<%= TargetModuleId %>(lat, long, country, state, city, markerId) {
                
        var baseServiceUrl = "<%= GetModuleFolderPath() %>Maps/Services";
        var postUrl = baseServiceUrl + "/PointSelected.ashx";
        var postVars = { 'targetModuleId': moduleId, 'country': country, 'state': state, 'city': city, 'maxPoints': 1, 'moduleId': '<%= ModuleId.ToString() %>', 'markerId': markerId };
        jQuery.post(postUrl, postVars, function (data) {
            if (data.success) {
                jQuery("#states<%= TargetModuleId %>").html(data.states);
                jQuery("#cities<%= TargetModuleId %>").html(data.cities);
                jQuery("#mapFilter<%= TargetModuleId %> select.countries").val(country);
                jQuery("#states<%= TargetModuleId %>").val(state);
                jQuery("#cities<%= TargetModuleId %>").val(city);

                jQuery("#states<%= TargetModuleId %>").show();
                jQuery("#cities<%= TargetModuleId %>").show();

                jQuery("#listingMarkers<%= ModuleId %>").html(data.markerHtml);                    

                if(data.states.length > 0)
                {                                     
                    initialize_<%= TargetModuleId %>(data.markers);
                    map_<%= TargetModuleId %>.setCenter(new google.maps.LatLng(lat, long));
                    map_<%= TargetModuleId %>.setZoom(10);                                                                               
                }
            }
        });
    }

    function deleteOverlays_<%= ModuleId %>() {
      if (mapMarkers_<%= ModuleId %>) {
        for (i in mapMarkers_<%= ModuleId %>) {
          mapMarkers_<%= ModuleId %>[i].marker.setMap(null);
        }
        mapMarkers_<%= ModuleId %>.length = 0;
      }
    }
</script>

<asp:Literal ID="litMap" runat="server"></asp:Literal>
<asp:Panel ID="pnlSearchResultsListing" runat="server">
    <asp:Literal ID="litSearchResults" runat="server"></asp:Literal>
</asp:Panel>