using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class PartialController : Controller
    {
        private readonly IPagePartialService _partialService;

        public PartialController(IPagePartialService partialService)
        {
            _partialService = partialService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Add(int pageId, string areaName, string controllerName, string actionName)
        {
            try
            {
                Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

                var controllerList = types.Where(t => t.Name == $"{controllerName}Controller").ToList();

                foreach (var type in controllerList)

                    if (type != null && type.GetMethod(actionName) != null)
                    {
                        await _partialService.AddAsync(pageId, areaName, controllerName, actionName);

                        return Json(new { State = true });
                    }

                return Json(new { State = false, Reason = "Invalid" });
            }
            catch (Exception)
            {
                return Json(new { State = false, Reason = "Exception" });
            }
        }
    }
}