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
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Maps.Common;
using DNNspot.Maps.DataModel.ES;
using DNNspot.Newtonsoft.Json;

namespace DNNspot.Maps.Maps
{
    public partial class EditMarker : ModuleBase
    {
        private int _markerId;
        protected DotNetNuke.UI.UserControls.TextEditor txtInfoWindowHtml;
        Marker marker = new Marker();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                _markerId = Convert.ToInt32(Request.QueryString["id"]);
                btnDelete.Visible = true;
            }

            if (!IsPostBack)
            {
                LoadMarker();
            }
        }

        void LoadMarker()
        {
            marker.LoadByPrimaryKey(_markerId);

            txtTitle.Text = marker.Title;
            txtAddress1.Text = marker.Address1;
            txtAddress2.Text = marker.Address2;
            txtCity.Text = marker.City;
            txtRegion.Text = marker.Region;
            txtPostalCode.Text = marker.PostalCode;
            txtCountry.Text = marker.Country;
            txtLatitude.Text = Convert.ToString(marker.Latitude);
            txtLongitude.Text = Convert.ToString(marker.Longitude);
            txtInfoWindowHtml.Text = marker.InfoWindowHtml;
            txtCustomField.Text = marker.CustomField;
            txtPhoneNumber.Text = marker.PhoneNumber;
            txtPriority.Text = marker.Priority == "zzzz" ? "" : marker.Priority;

            txtIconUrl.Text = marker.IconUrl;
            txtShadowUrl.Text = marker.IconShadowUrl;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (cbGeocodeOnSave.Checked)
            {
                SaveMarker();

                var deserialized = GeocodeMarker();
                if (deserialized.message.ToLower().Contains("successfully"))
                {
                    Marker marker = new Marker();
                    marker.LoadByPrimaryKey(_markerId);
                    Response.Redirect(EditUrl(ControlKeys.ListMarkers));
                }
                else
                {
                    lblMessage.Text = "Marker was saved. There was an error geocoding this marker:" + deserialized.message;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtLongitude.Text) || String.IsNullOrEmpty(txtLatitude.Text))
                {
                    lblMessage.Text =
                        "No latitude and longitude entered. Please enter a latitude and longitude or check the Geocode Markers on Save box.";
                }
                else
                {
                    SaveMarker();
                    Response.Redirect(EditUrl(ControlKeys.ListMarkers));
                }
            }
        }

        private GeocodeResponse GeocodeMarker()
        {
            string url = Request.Url.Scheme + "://" + Request.Url.Host + ModuleWebPath +
                         "Maps/GeocodeHandler.ashx?moduleId=" + ModuleId + "&markerId=" + _markerId;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            //webRequest.Method = "POST";
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";

            string jsonResponse = "";
            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader responseReader = new StreamReader(webResponse.GetResponseStream()))
            {
                jsonResponse = responseReader.ReadToEnd();
                responseReader.Close();
            }

            return JsonConvert.DeserializeObject<GeocodeResponse>(jsonResponse);
        }

        private void SaveMarker()
        {
            Marker marker = new Marker();

            if (_markerId > 0)
            {
                marker.LoadByPrimaryKey(_markerId);
            }
            else
            {
                marker.Save();
                if (marker.MarkerId.HasValue)
                {
                    _markerId = Convert.ToInt32(marker.MarkerId);
                }
            }

            marker.Title = txtTitle.Text;
            marker.Address1 = txtAddress1.Text;
            marker.Address2 = txtAddress2.Text;
            marker.City = txtCity.Text;
            marker.Region = txtRegion.Text;
            marker.PostalCode = txtPostalCode.Text;
            marker.Country = txtCountry.Text;
            marker.InfoWindowHtml = txtInfoWindowHtml.Text;
            marker.CustomField = txtCustomField.Text;
            marker.PhoneNumber = txtPhoneNumber.Text;

            marker.Priority = txtPriority.Text == "" ? "zzzz" : txtPriority.Text;

            if (!String.IsNullOrEmpty(txtLatitude.Text))
            {
                marker.Latitude = Convert.ToDouble(txtLatitude.Text);
            }

            if (!String.IsNullOrEmpty(txtLongitude.Text))
            {
                marker.Longitude = Convert.ToDouble(txtLongitude.Text);
            }

            marker.IconShadowUrl = txtShadowUrl.Text;
            marker.IconUrl = txtIconUrl.Text;
            marker.ModuleId = ModuleId;
            marker.Save();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl(ControlKeys.ListMarkers));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMarker();

            Response.Redirect(EditUrl(ControlKeys.ListMarkers));
        }

        private void DeleteMarker()
        {
            Marker marker = new Marker();
            marker.LoadByPrimaryKey(_markerId);
            marker.MarkAsDeleted();
            marker.Save();
        }
    }
}