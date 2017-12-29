using Portal.CMS.Services.Authentication;
using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.PageBuilder.ViewModels.Association;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Portal.CMS.Web.Areas.PageBuilder.Controllers
{
    [AdminFilter(ActionFilterResponseType.Modal)]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class AssociationController : Controller
    {
        private readonly IPageAssociationService _associationService;
        private readonly IRoleService _roleService;

        public AssociationController(IPageAssociationService associationService, IRoleService roleService)
        {
            _associationService = associationService;
            _roleService = roleService;
        }

        [HttpGet]
        [OutputCache(Duration = 86400, VaryByParam = "pageAssociationId")]
        public ActionResult Delete(int pageAssociationId)
        {
            return View("_Delete", pageAssociationId);
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

        [HttpGet]
        public async Task<ActionResult> EditAccess(int pageAssociationId)
        {
            var pageAssociation = await _associationService.GetAsync(pageAssociationId);

            var model = new EditAccessViewModel
            {
                PageAssociationId = pageAssociationId,
                RoleList = await _roleService.GetAsync(),
                SelectedRoleList = pageAssociation.PageAssociationRoles.Select(x => x.Role.RoleName).ToList()
            };

            return View("_EditAccess", model);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<HttpStatusCodeResult> EditAccess(EditAccessViewModel model)
        {
            try
            {
                await _associationService.EditRolesAsync(model.PageAssociationId, model.SelectedRoleList);

                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}