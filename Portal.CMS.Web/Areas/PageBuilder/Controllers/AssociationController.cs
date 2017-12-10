using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    public class AssociationController : Controller
    {
        private readonly IPageAssociationService _associationService;

        public AssociationController(IPageAssociationService associationService)
        {
            _associationService = associationService;
        }

        [HttpGet]
        public ActionResult Delete(int pageAssociationId)
        {
            return View(pageAssociationId);
        }

        [HttpDelete]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> Delete(string pageAssociationId)
        {
            try
            {
                await _associationService.DeleteAsync(Convert.ToInt32(pageAssociationId));

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}