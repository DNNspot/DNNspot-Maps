
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 11:09:58 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;



namespace DNNspot.Maps.DataModel.ES
{
	/// <summary>
	/// Encapsulates the 'DNNspot_Maps_Markers' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Marker))]	
	[XmlType("Marker")]
	public partial class Marker : esMarker
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Marker();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 markerId)
		{
			var obj = new Marker();
			obj.MarkerId = markerId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 markerId, esSqlAccessType sqlAccessType)
		{
			var obj = new Marker();
			obj.MarkerId = markerId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("MarkerCollection")]
	public partial class MarkerCollection : esMarkerCollection, IEnumerable<Marker>
	{
		public Marker FindByPrimaryKey(System.Int32 markerId)
		{
			return this.SingleOrDefault(e => e.MarkerId == markerId);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Marker))]
		public class MarkerCollectionWCFPacket : esCollectionWCFPacket<MarkerCollection>
		{
			public static implicit operator MarkerCollection(MarkerCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator MarkerCollectionWCFPacket(MarkerCollection collection)
			{
				return new MarkerCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class MarkerQuery : esMarkerQuery
	{
		public MarkerQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "MarkerQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(MarkerQuery query)
		{
			return MarkerQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator MarkerQuery(string query)
		{
			return (MarkerQuery)MarkerQuery.SerializeHelper.FromXml(query, typeof(MarkerQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esMarker : esEntity
	{
		public esMarker()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 markerId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(markerId);
			else
				return LoadByPrimaryKeyStoredProcedure(markerId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 markerId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(markerId);
			else
				return LoadByPrimaryKeyStoredProcedure(markerId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 markerId)
		{
			MarkerQuery query = new MarkerQuery();
			query.Where(query.MarkerId == markerId);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 markerId)
		{
			esParameters parms = new esParameters();
			parms.Add("MarkerId", markerId);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.MarkerId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? MarkerId
		{
			get
			{
				return base.GetSystemInt32(MarkerMetadata.ColumnNames.MarkerId);
			}
			
			set
			{
				if(base.SetSystemInt32(MarkerMetadata.ColumnNames.MarkerId, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.MarkerId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.ModuleId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ModuleId
		{
			get
			{
				return base.GetSystemInt32(MarkerMetadata.ColumnNames.ModuleId);
			}
			
			set
			{
				if(base.SetSystemInt32(MarkerMetadata.ColumnNames.ModuleId, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.ModuleId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Title
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Title
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Title);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Title, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Title);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.InfoWindowHtml
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String InfoWindowHtml
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.InfoWindowHtml);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.InfoWindowHtml, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.InfoWindowHtml);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Address1
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Address1
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Address1);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Address1, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Address1);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Address2
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Address2
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Address2);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Address2, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Address2);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.City
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.City);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.City, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.City);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Region
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Region
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Region);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Region, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Region);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.PostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PostalCode
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.PostalCode);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.PostalCode, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.PostalCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Country
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Country
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Country);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Country, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Country);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Latitude
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Double? Latitude
		{
			get
			{
				return base.GetSystemDouble(MarkerMetadata.ColumnNames.Latitude);
			}
			
			set
			{
				if(base.SetSystemDouble(MarkerMetadata.ColumnNames.Latitude, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Latitude);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Longitude
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Double? Longitude
		{
			get
			{
				return base.GetSystemDouble(MarkerMetadata.ColumnNames.Longitude);
			}
			
			set
			{
				if(base.SetSystemDouble(MarkerMetadata.ColumnNames.Longitude, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Longitude);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.IconUrl
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String IconUrl
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.IconUrl);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.IconUrl, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.IconUrl);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.IconShadowUrl
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String IconShadowUrl
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.IconShadowUrl);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.IconShadowUrl, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.IconShadowUrl);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.CustomField
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CustomField
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.CustomField);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.CustomField, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.CustomField);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.Priority
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Priority
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.Priority);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.Priority, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.Priority);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.ProximityX
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Double? ProximityX
		{
			get
			{
				return base.GetSystemDouble(MarkerMetadata.ColumnNames.ProximityX);
			}
			
			set
			{
				if(base.SetSystemDouble(MarkerMetadata.ColumnNames.ProximityX, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityX);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.ProximityY
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Double? ProximityY
		{
			get
			{
				return base.GetSystemDouble(MarkerMetadata.ColumnNames.ProximityY);
			}
			
			set
			{
				if(base.SetSystemDouble(MarkerMetadata.ColumnNames.ProximityY, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityY);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.ProximityZ
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Double? ProximityZ
		{
			get
			{
				return base.GetSystemDouble(MarkerMetadata.ColumnNames.ProximityZ);
			}
			
			set
			{
				if(base.SetSystemDouble(MarkerMetadata.ColumnNames.ProximityZ, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityZ);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Maps_Markers.PhoneNumber
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PhoneNumber
		{
			get
			{
				return base.GetSystemString(MarkerMetadata.ColumnNames.PhoneNumber);
			}
			
			set
			{
				if(base.SetSystemString(MarkerMetadata.ColumnNames.PhoneNumber, value))
				{
					OnPropertyChanged(MarkerMetadata.PropertyNames.PhoneNumber);
				}
			}
		}		
		
		#endregion	

		#region .str() Properties
		
		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}
		
		public override void SetProperty(string name, object value)
		{
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "MarkerId": this.str().MarkerId = (string)value; break;							
						case "ModuleId": this.str().ModuleId = (string)value; break;							
						case "Title": this.str().Title = (string)value; break;							
						case "InfoWindowHtml": this.str().InfoWindowHtml = (string)value; break;							
						case "Address1": this.str().Address1 = (string)value; break;							
						case "Address2": this.str().Address2 = (string)value; break;							
						case "City": this.str().City = (string)value; break;							
						case "Region": this.str().Region = (string)value; break;							
						case "PostalCode": this.str().PostalCode = (string)value; break;							
						case "Country": this.str().Country = (string)value; break;							
						case "Latitude": this.str().Latitude = (string)value; break;							
						case "Longitude": this.str().Longitude = (string)value; break;							
						case "IconUrl": this.str().IconUrl = (string)value; break;							
						case "IconShadowUrl": this.str().IconShadowUrl = (string)value; break;							
						case "CustomField": this.str().CustomField = (string)value; break;							
						case "Priority": this.str().Priority = (string)value; break;							
						case "ProximityX": this.str().ProximityX = (string)value; break;							
						case "ProximityY": this.str().ProximityY = (string)value; break;							
						case "ProximityZ": this.str().ProximityZ = (string)value; break;							
						case "PhoneNumber": this.str().PhoneNumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MarkerId":
						
							if (value == null || value is System.Int32)
								this.MarkerId = (System.Int32?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.MarkerId);
							break;
						
						case "ModuleId":
						
							if (value == null || value is System.Int32)
								this.ModuleId = (System.Int32?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.ModuleId);
							break;
						
						case "Latitude":
						
							if (value == null || value is System.Double)
								this.Latitude = (System.Double?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.Latitude);
							break;
						
						case "Longitude":
						
							if (value == null || value is System.Double)
								this.Longitude = (System.Double?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.Longitude);
							break;
						
						case "ProximityX":
						
							if (value == null || value is System.Double)
								this.ProximityX = (System.Double?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityX);
							break;
						
						case "ProximityY":
						
							if (value == null || value is System.Double)
								this.ProximityY = (System.Double?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityY);
							break;
						
						case "ProximityZ":
						
							if (value == null || value is System.Double)
								this.ProximityZ = (System.Double?)value;
								OnPropertyChanged(MarkerMetadata.PropertyNames.ProximityZ);
							break;
					

						default:
							break;
					}
				}
			}
            else if (this.ContainsColumn(name))
            {
                this.SetColumn(name, value);
            }
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}		

		public esStrings str()
		{
			if (esstrings == null)
			{
				esstrings = new esStrings(this);
			}
			return esstrings;
		}

		sealed public class esStrings
		{
			public esStrings(esMarker entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MarkerId
			{
				get
				{
					System.Int32? data = entity.MarkerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarkerId = null;
					else entity.MarkerId = Convert.ToInt32(value);
				}
			}
				
			public System.String ModuleId
			{
				get
				{
					System.Int32? data = entity.ModuleId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ModuleId = null;
					else entity.ModuleId = Convert.ToInt32(value);
				}
			}
				
			public System.String Title
			{
				get
				{
					System.String data = entity.Title;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Title = null;
					else entity.Title = Convert.ToString(value);
				}
			}
				
			public System.String InfoWindowHtml
			{
				get
				{
					System.String data = entity.InfoWindowHtml;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InfoWindowHtml = null;
					else entity.InfoWindowHtml = Convert.ToString(value);
				}
			}
				
			public System.String Address1
			{
				get
				{
					System.String data = entity.Address1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address1 = null;
					else entity.Address1 = Convert.ToString(value);
				}
			}
				
			public System.String Address2
			{
				get
				{
					System.String data = entity.Address2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address2 = null;
					else entity.Address2 = Convert.ToString(value);
				}
			}
				
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
				
			public System.String Region
			{
				get
				{
					System.String data = entity.Region;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Region = null;
					else entity.Region = Convert.ToString(value);
				}
			}
				
			public System.String PostalCode
			{
				get
				{
					System.String data = entity.PostalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostalCode = null;
					else entity.PostalCode = Convert.ToString(value);
				}
			}
				
			public System.String Country
			{
				get
				{
					System.String data = entity.Country;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Country = null;
					else entity.Country = Convert.ToString(value);
				}
			}
				
			public System.String Latitude
			{
				get
				{
					System.Double? data = entity.Latitude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Latitude = null;
					else entity.Latitude = Convert.ToDouble(value);
				}
			}
				
			public System.String Longitude
			{
				get
				{
					System.Double? data = entity.Longitude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Longitude = null;
					else entity.Longitude = Convert.ToDouble(value);
				}
			}
				
			public System.String IconUrl
			{
				get
				{
					System.String data = entity.IconUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IconUrl = null;
					else entity.IconUrl = Convert.ToString(value);
				}
			}
				
			public System.String IconShadowUrl
			{
				get
				{
					System.String data = entity.IconShadowUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IconShadowUrl = null;
					else entity.IconShadowUrl = Convert.ToString(value);
				}
			}
				
			public System.String CustomField
			{
				get
				{
					System.String data = entity.CustomField;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomField = null;
					else entity.CustomField = Convert.ToString(value);
				}
			}
				
			public System.String Priority
			{
				get
				{
					System.String data = entity.Priority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Priority = null;
					else entity.Priority = Convert.ToString(value);
				}
			}
				
			public System.String ProximityX
			{
				get
				{
					System.Double? data = entity.ProximityX;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProximityX = null;
					else entity.ProximityX = Convert.ToDouble(value);
				}
			}
				
			public System.String ProximityY
			{
				get
				{
					System.Double? data = entity.ProximityY;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProximityY = null;
					else entity.ProximityY = Convert.ToDouble(value);
				}
			}
				
			public System.String ProximityZ
			{
				get
				{
					System.Double? data = entity.ProximityZ;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProximityZ = null;
					else entity.ProximityZ = Convert.ToDouble(value);
				}
			}
				
			public System.String PhoneNumber
			{
				get
				{
					System.String data = entity.PhoneNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNumber = null;
					else entity.PhoneNumber = Convert.ToString(value);
				}
			}
			

			private esMarker entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return MarkerMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public MarkerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MarkerQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(MarkerQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(MarkerQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private MarkerQuery query;		
	}



	[Serializable]
	abstract public partial class esMarkerCollection : esEntityCollection<Marker>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MarkerMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "MarkerCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public MarkerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MarkerQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(MarkerQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MarkerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(MarkerQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((MarkerQuery)query);
		}

		#endregion
		
		private MarkerQuery query;
	}



	[Serializable]
	abstract public partial class esMarkerQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MarkerMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "MarkerId": return this.MarkerId;
				case "ModuleId": return this.ModuleId;
				case "Title": return this.Title;
				case "InfoWindowHtml": return this.InfoWindowHtml;
				case "Address1": return this.Address1;
				case "Address2": return this.Address2;
				case "City": return this.City;
				case "Region": return this.Region;
				case "PostalCode": return this.PostalCode;
				case "Country": return this.Country;
				case "Latitude": return this.Latitude;
				case "Longitude": return this.Longitude;
				case "IconUrl": return this.IconUrl;
				case "IconShadowUrl": return this.IconShadowUrl;
				case "CustomField": return this.CustomField;
				case "Priority": return this.Priority;
				case "ProximityX": return this.ProximityX;
				case "ProximityY": return this.ProximityY;
				case "ProximityZ": return this.ProximityZ;
				case "PhoneNumber": return this.PhoneNumber;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem MarkerId
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.MarkerId, esSystemType.Int32); }
		} 
		
		public esQueryItem ModuleId
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.ModuleId, esSystemType.Int32); }
		} 
		
		public esQueryItem Title
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Title, esSystemType.String); }
		} 
		
		public esQueryItem InfoWindowHtml
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.InfoWindowHtml, esSystemType.String); }
		} 
		
		public esQueryItem Address1
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Address1, esSystemType.String); }
		} 
		
		public esQueryItem Address2
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Address2, esSystemType.String); }
		} 
		
		public esQueryItem City
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.City, esSystemType.String); }
		} 
		
		public esQueryItem Region
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Region, esSystemType.String); }
		} 
		
		public esQueryItem PostalCode
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.PostalCode, esSystemType.String); }
		} 
		
		public esQueryItem Country
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Country, esSystemType.String); }
		} 
		
		public esQueryItem Latitude
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Latitude, esSystemType.Double); }
		} 
		
		public esQueryItem Longitude
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Longitude, esSystemType.Double); }
		} 
		
		public esQueryItem IconUrl
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.IconUrl, esSystemType.String); }
		} 
		
		public esQueryItem IconShadowUrl
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.IconShadowUrl, esSystemType.String); }
		} 
		
		public esQueryItem CustomField
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.CustomField, esSystemType.String); }
		} 
		
		public esQueryItem Priority
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.Priority, esSystemType.String); }
		} 
		
		public esQueryItem ProximityX
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.ProximityX, esSystemType.Double); }
		} 
		
		public esQueryItem ProximityY
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.ProximityY, esSystemType.Double); }
		} 
		
		public esQueryItem ProximityZ
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.ProximityZ, esSystemType.Double); }
		} 
		
		public esQueryItem PhoneNumber
		{
			get { return new esQueryItem(this, MarkerMetadata.ColumnNames.PhoneNumber, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Marker : esMarker
	{

		
		
	}
	



	[Serializable]
	public partial class MarkerMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MarkerMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MarkerMetadata.ColumnNames.MarkerId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MarkerMetadata.PropertyNames.MarkerId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.ModuleId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MarkerMetadata.PropertyNames.ModuleId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Title, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Title;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.InfoWindowHtml, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.InfoWindowHtml;
			c.CharacterMaxLength = 1073741823;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Address1, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Address1;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Address2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Address2;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.City, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.City;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Region, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Region;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.PostalCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.PostalCode;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Country, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Country;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Latitude, 10, typeof(System.Double), esSystemType.Double);
			c.PropertyName = MarkerMetadata.PropertyNames.Latitude;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Longitude, 11, typeof(System.Double), esSystemType.Double);
			c.PropertyName = MarkerMetadata.PropertyNames.Longitude;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.IconUrl, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.IconUrl;
			c.CharacterMaxLength = 512;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.IconShadowUrl, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.IconShadowUrl;
			c.CharacterMaxLength = 512;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.CustomField, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.CustomField;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.Priority, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.Priority;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.ProximityX, 16, typeof(System.Double), esSystemType.Double);
			c.PropertyName = MarkerMetadata.PropertyNames.ProximityX;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			c.IsComputed = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.ProximityY, 17, typeof(System.Double), esSystemType.Double);
			c.PropertyName = MarkerMetadata.PropertyNames.ProximityY;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			c.IsComputed = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.ProximityZ, 18, typeof(System.Double), esSystemType.Double);
			c.PropertyName = MarkerMetadata.PropertyNames.ProximityZ;
			c.NumericPrecision = 15;
			c.IsNullable = true;
			c.IsComputed = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(MarkerMetadata.ColumnNames.PhoneNumber, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = MarkerMetadata.PropertyNames.PhoneNumber;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public MarkerMetadata Meta()
		{
			return meta;
		}	
		
		public Guid DataID
		{
			get { return base.m_dataID; }
		}	
		
		public bool MultiProviderMode
		{
			get { return false; }
		}		

		public esColumnMetadataCollection Columns
		{
			get	{ return base.m_columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string MarkerId = "MarkerId";
			 public const string ModuleId = "ModuleId";
			 public const string Title = "Title";
			 public const string InfoWindowHtml = "InfoWindowHtml";
			 public const string Address1 = "Address1";
			 public const string Address2 = "Address2";
			 public const string City = "City";
			 public const string Region = "Region";
			 public const string PostalCode = "PostalCode";
			 public const string Country = "Country";
			 public const string Latitude = "Latitude";
			 public const string Longitude = "Longitude";
			 public const string IconUrl = "IconUrl";
			 public const string IconShadowUrl = "IconShadowUrl";
			 public const string CustomField = "CustomField";
			 public const string Priority = "Priority";
			 public const string ProximityX = "ProximityX";
			 public const string ProximityY = "ProximityY";
			 public const string ProximityZ = "ProximityZ";
			 public const string PhoneNumber = "PhoneNumber";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MarkerId = "MarkerId";
			 public const string ModuleId = "ModuleId";
			 public const string Title = "Title";
			 public const string InfoWindowHtml = "InfoWindowHtml";
			 public const string Address1 = "Address1";
			 public const string Address2 = "Address2";
			 public const string City = "City";
			 public const string Region = "Region";
			 public const string PostalCode = "PostalCode";
			 public const string Country = "Country";
			 public const string Latitude = "Latitude";
			 public const string Longitude = "Longitude";
			 public const string IconUrl = "IconUrl";
			 public const string IconShadowUrl = "IconShadowUrl";
			 public const string CustomField = "CustomField";
			 public const string Priority = "Priority";
			 public const string ProximityX = "ProximityX";
			 public const string ProximityY = "ProximityY";
			 public const string ProximityZ = "ProximityZ";
			 public const string PhoneNumber = "PhoneNumber";
		}
		#endregion	

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}
		
		#region MAP esDefault
		
		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(MarkerMetadata))
			{
				if(MarkerMetadata.mapDelegates == null)
				{
					MarkerMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MarkerMetadata.meta == null)
				{
					MarkerMetadata.meta = new MarkerMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!m_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();			


				meta.AddTypeMap("MarkerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ModuleId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Title", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("InfoWindowHtml", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Address1", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Address2", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Region", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PostalCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Country", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Latitude", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("Longitude", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("IconUrl", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IconShadowUrl", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CustomField", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Priority", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProximityX", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("ProximityY", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("ProximityZ", new esTypeMap("float", "System.Double"));
				meta.AddTypeMap("PhoneNumber", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DNNspot_Maps_Markers";
				meta.Destination = "DNNspot_Maps_Markers";
				
				meta.spInsert = "proc_DNNspot_Maps_MarkersInsert";				
				meta.spUpdate = "proc_DNNspot_Maps_MarkersUpdate";		
				meta.spDelete = "proc_DNNspot_Maps_MarkersDelete";
				meta.spLoadAll = "proc_DNNspot_Maps_MarkersLoadAll";
				meta.spLoadByPrimaryKey = "proc_DNNspot_Maps_MarkersLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MarkerMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
