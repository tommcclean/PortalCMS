using System.Collections.Generic;
using System.Web;

namespace PortalCMS.Library
{
  public class SessionHandler
  {
    const string sessionKey = "PortalCMS.Library.SessionHandler";
    private static Dictionary<string, object> SessionObjects
    {
      get
      {
        if (HttpContext.Current.Session[sessionKey] == null) { HttpContext.Current.Session[sessionKey] = new Dictionary<string, object>(); }

        return (Dictionary<string, object>)HttpContext.Current.Session[sessionKey];
      }
    }

    public static object Get(string itemKey)
    {
      return (SessionObjects.ContainsKey(itemKey)) ? SessionObjects[itemKey] : null;
    }

    public static void Set(string itemKey, object itemValue)
    {
      if (SessionObjects.ContainsKey(itemKey))
      {
        SessionObjects.Remove(itemKey);
      }

      SessionObjects.Add(itemKey, itemValue);
    }

    public static void Remove(string itemKey)
    {
      if (Get(itemKey) != null)
      {
        SessionObjects.Remove(itemKey);
      }
    }
  }
}
