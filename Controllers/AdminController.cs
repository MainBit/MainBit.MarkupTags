using System.Web.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using MainBit.MarkupTags.Services;
using MainBit.MarkupTags.Models;

namespace MainBit.MarkupTags.Controllers
{
    [ValidateInput(false), Admin]
    public class AdminController : Controller 
    {
        private readonly IMarkupTagService _markupTagService;

        public AdminController(IMarkupTagService markupTagService, IOrchardServices orchardServices)
        {
            _markupTagService = markupTagService;
            Services = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

        [HttpGet]
        public ActionResult Index() {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage opt markup tags")))
                return new HttpUnauthorizedResult();

            var listOfRecords = _markupTagService.Get();
            if (listOfRecords == null || listOfRecords.Count == 0)
                ViewBag.EmptyMessage = T("No data");
            return View(listOfRecords);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage opt markup tags")))
                return new HttpUnauthorizedResult();

            return View(new MarkupTagRecord());
        }

        [HttpPost]
        public ActionResult Create(MarkupTagRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage opt markup tags")))
                return new HttpUnauthorizedResult();

            if (!ModelState.IsValid)
                return View(model);

            _markupTagService.Add(model.Title, model.Content, model.Zone, model.Position, model.Enable);
            Services.Notifier.Information(T("MainBit Markup Tag successfully added"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit Markup Tags")))
                return new HttpUnauthorizedResult();

            return View(_markupTagService.Get(Id));
        }

        [HttpPost]
        public ActionResult Edit(int Id, MarkupTagRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit Markup Tags")))
                return new HttpUnauthorizedResult();

            if (!ModelState.IsValid)
                return View(_markupTagService.Get(Id));
            if (_markupTagService.Set(Id, model.Title, model.Content, model.Zone, model.Position, model.Enable))
            {
                Services.Notifier.Information(T("MainBit Markup Tag successfully saved"));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit Markup Tags")))
                return new HttpUnauthorizedResult();

            if (_markupTagService.Delete(Id))
            {
                Services.Notifier.Information(T("MainBit Markup Tag successfully deleted"));
            }
            return RedirectToAction("Index");
        }

    }
}