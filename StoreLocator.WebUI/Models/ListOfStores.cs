using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace StoreLocator.WebUI.Models
{
  [XmlRoot("markers")]
  public class ListOfStores<T> where T : StoreLocation
  {
    [XmlElement("marker")]
    public List<T> Results;
  }
}

