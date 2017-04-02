using Portal.CMS.Services.Authentication;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Forms.ViewModels.ContactWidgets;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Forms.Controllers
{
    public class ContactWidgetsController : Controller
    {
        #region Dependencies

        private readonly IUserService _userService;

        public ContactWidgetsController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion Dependencies

        [HttpGet]
        public ActionResult SubmitMessageWidget()
        {
            var model = new SubmitMessageViewModel();

            if (UserHelper.IsLoggedIn)
            {
                model.Name = UserHelper.FullName;
                model.EmailAddress = UserHelper.EmailAddress;
            }

            return PartialView("_SubmitMessageWidget", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitMessageWidget(SubmitMessageViewModel model)
        {
            EmailHelper.Send(
                _userService.Get(new List<string> { nameof(Admin) }).Select(x => x.EmailAddress).ToList(),
                "Contact Submitted",
                $@"<p>Hello, we thought you might like to know that a visitor to your website has submitted a message, here are the details we recorded.</p>
                <p>Name: {model.Name}</p>
                <p>Email Address: {model.EmailAddress}</p>
                <p>Subject: {model.Subject}</p>
                <p>Message: {model.Message}</p>");

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}