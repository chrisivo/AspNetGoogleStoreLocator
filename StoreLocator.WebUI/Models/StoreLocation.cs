using System;
using System.Xml.Serialization;

namespace StoreLocator.WebUI.Models
{
	public class StoreLocation
	{
		[XmlAttribute("name", typeof(string))]
		public string StoreName { get; set; }

		[XmlAttribute("address", typeof(string))]
		public string StoreAddress { get; set; }

		[XmlAttribute("lat", typeof(double))]
		public double Latitude { get; set; }

		[XmlAttribute("lng", typeof(double))]
		public double Longitude { get; set; }
	}
}

