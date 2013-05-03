using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace DNNspot.Maps.MarkerListing
{
    public partial class Settings : ModuleSettingsBase
    {
        //protected DotNetNuke.UI.UserControls.TextEditor txtListTemplate;

        protected string ModuleWebPath
        {
            get { return String.Format("/DesktopModules/{0}/", ModuleConfiguration.FolderName); }
        }

        private void LoadControls()
        {
            //var filePath = String.Format("{0}uploads/{1}", ModuleWebPath, Settings[ModuleSettingNames.FileName]);
            LoadModulesAvailable();
            txtListTemplate.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.ListTemplate]);

            ddlLinkAutoZoom.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.LinkAutoZoom] ?? "10")).Selected = true;

            if (ModuleSettings[ModuleSettingNames.TargetModule] != null)
            {
                ddlModuleToUse.Items.FindByValue(Convert.ToString(ModuleSettings[ModuleSettingNames.TargetModule])).
                    Selected = true;
            }

            //ckbLinkTitles.Checked = Convert.ToBoolean(ModuleSettings[ModuleSettingNames.LinkMapListing]);

        }

        public override void LoadSettings()
        {
            if (!IsPostBack)
                LoadControls();
        }

        private void SaveSettings()
        {
            try
            {
                var moduleController = new ModuleController();

                if (!String.IsNullOrEmpty(txtListTemplate.Text))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.ListTemplate, txtListTemplate.Text);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.ListTemplate);
                if (!String.IsNullOrEmpty(ddlModuleToUse.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.TargetModule, ddlModuleToUse.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.TargetModule);

                //if (ckbLinkTitles.Checked)
                //    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.LinkMapListing, ckbLinkTitles.Checked.ToString());
                //else
                //    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.LinkMapListing);

                if (!String.IsNullOrEmpty(ddlLinkAutoZoom.SelectedValue))
                    moduleController.UpdateModuleSetting(ModuleId, ModuleSettingNames.LinkAutoZoom, ddlLinkAutoZoom.SelectedValue);
                else
                    moduleController.DeleteModuleSetting(ModuleId, ModuleSettingNames.LinkAutoZoom);

                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public override void UpdateSettings()
        {
            SaveSettings();
        }

        private void LoadModulesAvailable()
        {
            List<TabModuleMatch> modules = DnnHelper.GetTabsWithModuleByModuleDefinitionName(PortalId, "DNNspot-Maps");

            if (modules.Count > 0)
            {
                ddlModuleToUse.DataSource = modules;
                ddlModuleToUse.DataValueField = "ModuleId";
                ddlModuleToUse.DataTextField = "DisplayName";
                ddlModuleToUse.DataBind();

                ddlModuleToUse.Items.Insert(0, new ListItem { Text = "Select a Module", Value = String.Empty, Selected = false });
            }
        }
    }

    public class ModuleSettingNames
    {
        public const string ListTemplate = "ListTemplate";
        public const string TargetModule = "TargetModule";
        //public const string LinkMapListing = "LinkMapListing";
        public const string LinkAutoZoom = "LinkAutoZoom";
    }

}