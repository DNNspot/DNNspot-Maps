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
using System.Data;
using System.Threading;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel.ES;
using DotNetNuke.Common;
using EntitySpaces.Interfaces;
using WA.Geocoding;
using WA.Geocoding.Google;

namespace DNNspot.Maps.MapLocation {
    public partial class Default : ModuleBase {
        protected void Page_Load(object sender, EventArgs e) {
            lblSearchLabel.Text = (string) (Settings[ModuleSettingNames.SearchLabelText] ?? "Address");
            btnSubmit.Text = (string) (Settings[ModuleSettingNames.SearchButtonText] ?? "Search");
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            var tabId = Convert.ToInt32(Settings[ModuleSettingNames.RedirectTab] ?? TabId);

            Response.Redirect(Globals.NavigateURL(tabId, null, new[] {"mapLocation="+txtMapLocation.Text}));
        }
    }
}