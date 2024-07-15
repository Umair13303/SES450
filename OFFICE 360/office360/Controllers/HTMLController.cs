using office360.Models.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Controllers
{
    public class HTMLController : Controller
    {
        [AllowAnonymous]
        // GET: HTML
        public ActionResult Button(HTMLAttributes model)
        {
            if (string.IsNullOrEmpty(model.Type))
            {
                model.Type = "button";
            }
            return View(model);
        }
        public ActionResult Label(HTMLAttributes model)
        {
            return View(model);
        }
        public ActionResult Input(HTMLAttributes model)
        {
            model.Type = (model.Type ?? "text");
            return View(model);
        }
        public ActionResult PortletHead(HTMLAttributes model)
        {
            return View(model);
        }
        public ActionResult SectionHead(HTMLAttributes model)
        {
            return View(model);
        }
        public ActionResult DataTable(DataTableAttributes model)
        {
            return View(model);
        }
        public ActionResult TextArea(HTMLAttributes model)
        {
            return View(model);
        }
        public ActionResult DropDown(DropDownAttributes model)
        {
            return View(model);
        }

        //public ActionResult ReportViewer(ReportViewer model)
        //{
        //    return View(model);
        //}
        //public ActionResult Portlet(PortletAttributes model)
        //{
        //    return View(model);
        //}
    }
}