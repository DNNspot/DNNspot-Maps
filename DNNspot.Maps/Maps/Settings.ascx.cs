/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework;
using DotNetNuke.Services.Exceptions;

namespace DNNspot.Maps.Maps {
    public partial class Settings : ModuleSettingsBase {
        protected string ModuleWebPath
        {
            get { return String.Format("/DesktopModules/{0}/", ModuleConfiguration.FolderName); }
        }

        private void LoadControls() {
            var filePath = String.Format("{0}uploads/{1}", ModuleWebPath, Settings[ModuleSettingNames.FileName]);

            txtGoogleApiKey.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.ApiKey]);
            txtTitle.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Title]);
            txtInfoWindowHtml.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.InfoWindowHtml]);
            txtAddress1.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Address1]);
            txtAddress2.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Address2]);
            txtCity.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.City]);
            txtRegion.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Region]);
            txtPostalCode.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.PostalCode]);
            txtCountry.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Country]);
            txtLatitude.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Latitude]);
            txtLongitude.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Longitude]);
            txtPriority.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.Priority]);
            txtCustomField.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.CustomField]);
            txtIconUrl.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.IconUrl]);
            txtIconShadowUrl.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.IconShadowUrl]);
            txtMapHeight.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MapHeight]);
            txtMapWidth.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MapWidth]);
            ddlMapMode.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.MapMode] ?? "ROADMAP")).Selected = true;
            rblMapPositioning.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.MapPositioning] ?? "0")).Selected = true;
            txtMapAddress.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MapAddress]);
            txtMapLatitude.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MapLatitude]);
            txtCustomFieldLabel.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.CustomFieldLabel]);
            txtMapLongitude.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MapLongitude]);
            txtPhoneNumber.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.PhoneNumber]);
            ddlMapZoom.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.MapZoom] ?? "10")).Selected = true;
            ddlAutoZoomLevel.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.AutoZoomLevel] ?? "-1")).Selected = true;
            ckbShowFilter.Checked = Convert.ToBoolean(ModuleSettings[ModuleSettingNames.ShowMapFilter]);
            ckbShowCustomFieldFilter.Checked = Convert.ToBoolean(ModuleSettings[ModuleSettingNames.ShowCustomFieldFilter]);
            ckbHideMapPointsOnPageLoad.Checked = Convert.ToBoolean(ModuleSettings[ModuleSettingNames.HideMapPointsOnPageLoad]);
            ckbShowProximitySearch.Checked = Convert.ToBoolean(ModuleSettings[ModuleSettingNames.ShowProximitySearch]);
            txtSearchResultsTemplate.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.SearchResultsTemplate]);
        }

        public override void LoadSettings() {
            if (!IsPostBack)
                LoadControls();
        }

        private void SaveSettings() {
            try {
                var moduleController = new ModuleController();

                if (!String.IsNullOrEmpty(txtTitle.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Title, txtTitle.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Title);

                if (!String.IsNullOrEmpty(txtInfoWindowHtml.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.InfoWindowHtml, txtInfoWindowHtml.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.InfoWindowHtml);

                if (!String.IsNullOrEmpty(txtAddress1.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Address1, txtAddress1.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Address1);

                if (!String.IsNullOrEmpty(txtAddress2.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Address2, txtAddress2.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Address2);

                if (!String.IsNullOrEmpty(txtCity.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.City, txtCity.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.City);

                if (!String.IsNullOrEmpty(txtRegion.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Region, txtRegion.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Region);

                if (!String.IsNullOrEmpty(txtPostalCode.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.PostalCode, txtPostalCode.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.PostalCode);

                if (!String.IsNullOrEmpty(txtCountry.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Country, txtCountry.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Country);

                if (!String.IsNullOrEmpty(txtLatitude.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Latitude, txtLatitude.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Latitude);

                if (!String.IsNullOrEmpty(txtLongitude.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Longitude, txtLongitude.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Longitude);

                if (!String.IsNullOrEmpty(txtIconUrl.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.IconUrl, txtIconUrl.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.IconUrl);

                if (!String.IsNullOrEmpty(txtIconShadowUrl.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.IconShadowUrl, txtIconShadowUrl.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.IconShadowUrl);

                if (!String.IsNullOrEmpty(txtMapHeight.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapHeight, txtMapHeight.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapHeight);

                if (!String.IsNullOrEmpty(txtMapWidth.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapWidth, txtMapWidth.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapWidth);

                if (!String.IsNullOrEmpty(ddlMapMode.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapMode, ddlMapMode.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapMode);

                if (!String.IsNullOrEmpty(rblMapPositioning.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapPositioning, rblMapPositioning.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapPositioning);

                if (!String.IsNullOrEmpty(txtMapAddress.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapAddress, txtMapAddress.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapAddress);

                if (!String.IsNullOrEmpty(txtMapLatitude.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapLatitude, txtMapLatitude.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapLatitude);

                if (!String.IsNullOrEmpty(txtMapLongitude.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapLongitude, txtMapLongitude.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapLongitude);

                if (!String.IsNullOrEmpty(txtCustomField.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.CustomField, txtCustomField.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.CustomField);

                if (!String.IsNullOrEmpty(txtPriority.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.Priority, txtPriority.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.Priority);

                if (!String.IsNullOrEmpty(txtPhoneNumber.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.PhoneNumber, txtPhoneNumber.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.PhoneNumber);

                if (!String.IsNullOrEmpty(ddlMapZoom.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.MapZoom, ddlMapZoom.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.MapZoom);

                if (!String.IsNullOrEmpty(ddlAutoZoomLevel.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.AutoZoomLevel, ddlAutoZoomLevel.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.AutoZoomLevel);



                if (!String.IsNullOrEmpty(txtGoogleApiKey.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.ApiKey, txtGoogleApiKey.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.ApiKey);

                if (ckbShowFilter.Checked)
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.ShowMapFilter, ckbShowFilter.Checked.ToString());
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.ShowMapFilter);

                if (ckbHideMapPointsOnPageLoad.Checked)
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.HideMapPointsOnPageLoad, ckbHideMapPointsOnPageLoad.Checked.ToString());
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.HideMapPointsOnPageLoad);

                if (ckbShowCustomFieldFilter.Checked)
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.ShowCustomFieldFilter, ckbShowCustomFieldFilter.Checked.ToString());
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.ShowCustomFieldFilter);

                if (ckbShowProximitySearch.Checked)
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.ShowProximitySearch, ckbShowProximitySearch.Checked.ToString());
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.ShowProximitySearch);

                if (!String.IsNullOrEmpty(txtCustomFieldLabel.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.CustomFieldLabel, txtCustomFieldLabel.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.CustomFieldLabel);

                if (!String.IsNullOrEmpty(txtSearchResultsTemplate.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.SearchResultsTemplate, txtSearchResultsTemplate.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.SearchResultsTemplate);

                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public override void UpdateSettings() {
            SaveSettings();
        }
    }

    public class ModuleSettingNames {
        public const string ApiKey = "ApiKey";
        public const string Address1 = "Address1";
        public const string Address2 = "Address2";
        public const string City = "City";
        public const string Country = "Country";
        public const string FileName = "FileName";
        public const string IconShadowUrl = "IconShadowUrl";
        public const string IconUrl = "IconUrl";
        public const string InfoWindowHtml = "InfoWindowHtml";
        public const string Latitude = "Latitude";
        public const string Longitude = "Longitude";
        public const string CustomField = "CustomField";
        public const string Priority = "Priority";
        public const string MapAddress = "";
        public const string MapHeight = "MapHeight";
        public const string MapLatitude = "MapLatitude";
        public const string MapLongitude = "MapLongitude";
        public const string MapMode = "MapMode";
        public const string MapPositioning = "MapPositioning";
        public const string MapWidth = "MapWidth";
        public const string MapZoom = "MapZoom";
        public const string PostalCode = "PostalCode";
        public const string Region = "Region";
        public const string Title = "Title";
        public const string AutoZoomLevel = "AutoZoomLevel";
        public const string ShowMapFilter = "ShowMapFilter";
        public const string ShowCustomFieldFilter = "ShowCustomFieldFilter";
        public const string ShowProximitySearch = "ShowProximitySearch";
        public const string SearchResultsTemplate = "SearchResultsTemplate";
        public const string PhoneNumber = "PhoneNumber";
        public const string CustomFieldLabel = "CustomFieldLabel";
        public const string HideMapPointsOnPageLoad = "HideMapPointsOnPageLoad";
    }


}