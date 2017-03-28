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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel.ES;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using WA.FileHelpers.Csv;

namespace DNNspot.Maps.Maps
{
    public partial class ListMarkers : ModuleBase, IActionable
    {
        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection();

                actions.Add(GetNextActionID(), "New Marker", ModuleActionType.AddContent, "", "",
                            EditUrl(ControlKeys.EditMarker), false, SecurityAccessLevel.Edit, true, false);

                return actions;
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["custom"] != null)
            {
                CustomFilterSelection = Request.Params["custom"];
            }

            rptMarkers.DataSource = !string.IsNullOrEmpty(CustomFilterSelection) ? LoadMarkers(ModuleId, CustomFilterSelection) : LoadMarkers(ModuleId);
            rptMarkers.DataBind();

        }

        private void BindMarkers()
        {
            rptMarkers.DataSource = GetMarkersByModuleId(ModuleId);
            rptMarkers.DataBind();
        }

        private static MarkerCollection GetMarkersByModuleId(int moduleId)
        {
            MarkerCollection markers = new MarkerCollection();
            MarkerQuery q = new MarkerQuery();

            q.Where(q.ModuleId == moduleId);
            q.OrderBy(q.Title.Ascending);
            markers.Load(q);

            return markers;
        }

        protected void lbDownloadCsv_Click(object sender, EventArgs e)
        {
            ExportMarkersFromModule();
        }

        private void ExportMarkersFromModule()
        {
            var markers = MarkerCollection.LoadAll(ModuleId);

            List<CsvMarkerInfo> csvProducts = MarkerExport.GetCsvMarkersList(markers);

            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=markers-{0}.csv", ModuleId));

            MarkerExport.ExportMarkers(csvProducts, Response.OutputStream);

            Response.Flush();
            Response.End();
        }
    }
}