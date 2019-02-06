using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    [Authorize]
    public class ControlPanelController : Controller
    {
        int PERPAGE = 5;
        Models.AiBEntities db = new Models.AiBEntities();

        public ActionResult test()
        {
            return View();
        }

        // GET: ControlPanel
        public ActionResult Index()
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _AccountEditor(Models.AspNetUsers account)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            return PartialView("_AccountEditor", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountEdit(Models.AspNetUsers account)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            TempData["OperationTarget"] = "Account";
            try
            {
                Models.AspNetUsers aaccount = db.AspNetUsers.Where(a => a.Id == account.Id).First();

                if (account.Group == db.Group.Where(g => g.name == "Deleted").First().idGroup)
                {
                    aaccount.UserName = "Deleted" + db.AspNetUsers.Where(a => a.Group == aaccount.Group).Count().ToString();
                    aaccount.Email = aaccount.UserName;
                    aaccount.PasswordHash = "";
                    aaccount.Group = account.Group;
                    TempData["OperationResult"] = "Deleted";
                }
                else
                {
                    aaccount.UserName = account.UserName;
                    aaccount.Email = account.Email;
                    aaccount.Group = account.Group;
                    TempData["OperationResult"] = "Edited";
                }

                db.SaveChanges();
            }
            catch
            {
                TempData["OperationResult"] = "Not Edited";
            }

            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _AccountCreate()
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountCreate(Models.AspNetUsers account)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            TempData["OperationTarget"] = "Account";
            try
            {
                account.creationDate = DateTime.Now;
                db.AspNetUsers.Add(account);
                db.SaveChanges();

                TempData["OperationResult"] = "Created";
            }
            catch
            {
                TempData["OperationResult"] = "Not Created";
            }

            return View("Index");
        }

        private int GetParam(string paramName)
        {
            int param;
            Int32.TryParse(Request.QueryString.Get(paramName), out param);
            if (param == 0)
                param = 1;
            return param;
        }

        [ChildActionOnly]
        public PartialViewResult _AccountList()
        {
            int page = GetParam("acp");
            int sort = GetParam("acs");

            if (page < 1)
                page = 1;

            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            int deleted = groups.OrderBy(g => g.Power).First().idGroup;

            //IEnumerable<Models.AspNetUsers> accounts = db.AspNetUsers.Where(a => a.groupFK != deleted);

            var accounts = from a in db.AspNetUsers.Where(a => a.Group != deleted) select a;

            switch (sort)
            {
                case 1:
                    accounts = accounts.OrderBy(a => a.UserName);
                    break;
                case 2:
                    accounts = accounts.OrderBy(a => a.Email);
                    break;
                case 3:
                    accounts = accounts.OrderBy(a => a.Group);
                    break;
                case 4:
                    accounts = accounts.OrderBy(a => a.creationDate);
                    break;
                case -1:
                    accounts = accounts.OrderByDescending(a => a.UserName);
                    break;
                case -2:
                    accounts = accounts.OrderByDescending(a => a.Email);
                    break;
                case -3:
                    accounts = accounts.OrderByDescending(a => a.Group);
                    break;
                case -4:
                    accounts = accounts.OrderByDescending(a => a.creationDate);
                    break;
            }

            //return PartialView(accounts.Skip(page * PERPAGE).Take(PERPAGE));
            return PartialView(accounts.ToPagedList(page, PERPAGE));

        }

        // Section

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _SectionEditor(Models.Section section)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            return PartialView("_SectionEditor", section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionEdit(Models.Section section)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            TempData["OperationTarget"] = "Section";
            try
            {
                Models.Section ssection = db.Section.Where(a => a.idSection == section.idSection).First();

                ssection.name = section.name;
                ssection.readGroupFk = section.readGroupFk;
                ssection.writeGroupFk = section.writeGroupFk;

                db.SaveChanges();

                TempData["OperationResult"] = "Edited";
            }
            catch
            {
                TempData["OperationResult"] = "Not Edited";
            }

            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _SectionCreate()
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionCreate(Models.Section section)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            TempData["OperationTarget"] = "Section";
            try
            {
                db.Section.Add(section);
                db.SaveChanges();
                TempData["OperationResult"] = "Created";
            }
            catch
            {
                TempData["OperationResult"] = "Not Created";
            }

            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _SectionList()
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            int page = GetParam("sep");
            int sort = GetParam("ses");

            if (page < 1)
                page = 1;
            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            IEnumerable<Models.Section> sections = db.Section;

            switch (sort)
            {
                case 1:
                    sections = sections.OrderBy(a => a.name);
                    break;
                case 2:
                    sections = sections.OrderBy(a => a.readGroupFk);
                    break;
                case 3:
                    sections = sections.OrderBy(a => a.writeGroupFk);
                    break;
                case -1:
                    sections = sections.OrderByDescending(a => a.name);
                    break;
                case -2:
                    sections = sections.OrderByDescending(a => a.readGroupFk);
                    break;
                case -3:
                    sections = sections.OrderByDescending(a => a.writeGroupFk);
                    break;
            }

            //return PartialView(sections.Skip(page * PERPAGE).Take(PERPAGE));
            return PartialView(sections.ToPagedList(page, PERPAGE));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionDelete(Models.Section section)
        {
            if (!CheckPermision())
            {
                return HttpNotFound();
            }

            TempData["OperationTarget"] = "Section";
            try
            {
                foreach (Models.Topic topic in db.Topic.Where(t => t.sectionFK == section.idSection))
                {
                    topic.sectionFK = 1;
                }
                Models.Section delete = db.Section.Where(s => s.idSection == section.idSection).First();
                db.Section.Remove(delete);

                db.SaveChanges();

                TempData["OperationResult"] = "Deleted";
            }
            catch (DbUpdateException ex)
            {
                TempData["OperationResult"] = "Not Deleted";
            }
            return View("Index");
        }

        private Boolean CheckPermision()
        {
            try
            {
                if (db.AspNetUsers.Where(a => a.UserName == User.Identity.Name.ToString()).First().Group1.Power >= 90)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


    }
}