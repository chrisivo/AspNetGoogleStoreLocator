using System;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Xml.Serialization;

// Obtained from: http://www.dotnetcurry.com/ShowArticle.aspx?ID=682

namespace StoreLocator.WebUI.Extensions
{
  public class XmlResult<T> : ViewResult
  {
    public T Data { private get; set; }

    public override void ExecuteResult(ControllerContext context)
    {
      HttpContextBase httpContextBase = context.HttpContext;
      httpContextBase.Response.Buffer = true;
      httpContextBase.Response.Clear();

      httpContextBase.Response.ContentType = "text/xml";

      using (StringWriter writer = new StringWriter())
      {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        xml.Serialize(writer, Data);
        httpContextBase.Response.Write(writer);
      }
    }
  }
}

