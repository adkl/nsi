using NSI.Api.Core;
using System.Web;
using System.Web.Mvc;

namespace NSI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
