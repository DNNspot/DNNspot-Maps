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
using DNNspot.Maps.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Exceptions;

namespace DNNspot.Maps.MapLocation {
    public partial class Settings : ModuleSettingsBase {
        #region Events

        public override void LoadSettings() {
            if (!IsPostBack)
                LoadControls();
        }

        public override void UpdateSettings() {
            SaveSettings();
        }

        #endregion

        #region Functions

        private void LoadControls() {
            var tabs = TabController.GetTabsBySortOrder(PortalId);

            ddlTabs.DataSource = tabs;
            ddlTabs.DataTextField = "TabName";
            ddlTabs.DataValueField = "TabId";
            ddlTabs.DataBind();

            ddlTabs.SelectByValue(Convert.ToString(Settings[ModuleSettingNames.RedirectTab] ?? TabId));

            txtSearchLabelText.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.SearchLabelText]);
            txtSearchButtonText.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.SearchButtonText]);
        }

        private void SaveSettings() {
            try {
                var moduleController = new ModuleController();

                if (ddlTabs.SelectedIndex != -1)
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.RedirectTab, ddlTabs.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.RedirectTab);


                if (!String.IsNullOrEmpty(txtSearchLabelText.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.SearchLabelText, txtSearchLabelText.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.SearchLabelText);

                if (!String.IsNullOrEmpty(txtSearchButtonText.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.SearchButtonText, txtSearchButtonText.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.SearchButtonText);

                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        #endregion
    }

    public static class ModuleSettingNames {
        public const string RedirectTab = "RedirectTab";
        public const string SearchButtonText = "SearchButtonText";
        public const string SearchLabelText = "SearchLabelText";
    }
}