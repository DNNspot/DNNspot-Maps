<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="DNNspot.Maps.MarkerListing.Default" %>
<%@ Import Namespace="DNNspot.Maps.MarkerListing" %>
<div id="listingMarkers<%= ModuleId %>">
    <asp:Literal ID="litListing" runat="server">
    </asp:Literal>
</div>
<asp:Panel ID="pnlGoToListing">
    <script type="text/javascript">
        function SelectPoint(lat, long, country, state, city) {
                
            var baseServiceUrl = "<%= GetModuleFolderPath() %>Maps/Services";
            var postUrl = baseServiceUrl + "/PointSelected.ashx";
            var postVars = { 'targetModuleId': moduleId, 'country': country, 'state': state, 'city': city, 'isTrial': isTrial, 'maxPoints': maxPoints, 'moduleId': '<%= ModuleId.ToString() %>' };
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
                        map_<%= TargetModuleId %>.setZoom(<%= Settings[ModuleSettingNames.LinkAutoZoom] ?? "10" %>);                                                                               
                    }
                }
            });
        }



       jQuery("#mapFilter<%= TargetModuleId %> .countries").change(function () {
            var postUrl = baseServiceUrl + "/GetMarkersTemplate.ashx";
            var country = jQuery(this).val();
            var customField = "";
            jQuery("#cities<%= TargetModuleId %>").hide(); 

            if(jQuery("#mapCustomFilter<%= TargetModuleId %> select")[0].selectedIndex >= 1)
            {
                customField = jQuery("#mapCustomFilter<%= TargetModuleId %> select").val();
            }

            var postVars = { 'targetModuleId': moduleId, 'country': country, 'moduleId': '<%= ModuleId.ToString() %>', 'customField': customField };
            jQuery.post(postUrl, postVars, function (data) {
                if (data.success) {         
                    jQuery("#listingMarkers<%= ModuleId %>").html(data.markerHtml);                     
                }
            });
        });

        jQuery("#states<%= TargetModuleId %>").change(function () {
            var postUrl = baseServiceUrl + "/GetMarkersTemplate.ashx";
            var country = jQuery("#mapFilter<%= TargetModuleId %> .countries").val();
            var state = jQuery(this).val();
            var customField = "";

            if(jQuery("#mapCustomFilter<%= TargetModuleId %> select")[0].selectedIndex >= 1)
            {
                customField = jQuery("#mapCustomFilter<%= TargetModuleId %> select").val();
            }

            var postVars = { 'targetModuleId': moduleId, 'country': country, 'state': state, 'moduleId': '<%= ModuleId.ToString() %>', 'customField': customField };
            jQuery.post(postUrl, postVars, function (data) {
                if (data.success) {         
                    jQuery("#listingMarkers<%= ModuleId %>").html(data.markerHtml);                     
                }
            });
        });

        jQuery("#cities<%= TargetModuleId %>").change(function () {                    
            var postUrl = baseServiceUrl + "/GetMarkersTemplate.ashx";
            var country = jQuery("#mapFilter<%= TargetModuleId %> .countries").val();
            var state = jQuery("#states<%= TargetModuleId %>").val();
            var city = jQuery(this).val();
            var customField = "";

            if(jQuery("#mapCustomFilter<%= TargetModuleId %> select")[0].selectedIndex >= 1)
            {
                customField = jQuery("#mapCustomFilter<%= TargetModuleId %> select").val();
            }

            var postVars = { 'targetModuleId': moduleId, 'country': country, 'state': state, 'city': city, 'moduleId': '<%= ModuleId.ToString() %>', 'customField': customField };
            jQuery.post(postUrl, postVars, function (data) {
                if (data.success) {         
                    jQuery("#listingMarkers<%= ModuleId %>").html(data.markerHtml);                     
                }
            });
        });

        jQuery("#mapCustomFilter<%= TargetModuleId %> select").change(function () {                    
            var postUrl = baseServiceUrl + "/GetMarkersTemplate.ashx";
            var country = "";
            var state = "";
            var city = "";
            var customField = jQuery(this).val();


            if(jQuery("#mapFilter<%= TargetModuleId %> .countries")[0].selectedIndex >= 1)
            {
                //country = jQuery("#mapFilter<%= TargetModuleId %> .countries").val();
            }

            if(jQuery("#cities<%= TargetModuleId %>")[0].selectedIndex >= 1)
            {
                //city = jQuery("#cities<%= TargetModuleId %>").val();
            }

            if(jQuery("#states<%= TargetModuleId %>")[0].selectedIndex >= 1)
            {
                //state = jQuery("#states<%= TargetModuleId %>").val();
            }

            var postVars = { 'targetModuleId': moduleId, 'country': country, 'state': state, 'city': city, 'moduleId': '<%= ModuleId.ToString() %>', 'customField': customField };
            jQuery.post(postUrl, postVars, function (data) {
                if (data.success) {         
                    jQuery("#listingMarkers<%= ModuleId %>").html(data.markerHtml);                     
                }
            });
        });
    </script>
</asp:Panel>