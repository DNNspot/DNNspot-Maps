<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="DNNspot.Maps.MarkerListing.Default" %>
<%@ Import Namespace="DNNspot.Maps.MarkerListing" %>
<div id="listingMarkers<%= ModuleId %>">
    <asp:Literal ID="litListing" runat="server">
    </asp:Literal>
</div>
<asp:Panel ID="pnlGoToListing">
    <script type="text/javascript">
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