using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiBforum2.Controllers
{
    public class ControlPanelController : Controller
    {
        int PERPAGE = 5;
        Models.AiBforumEntities db = new Models.AiBforumEntities();

        public ActionResult test()
        {
            return View();
        }

        // GET: ControlPanel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _AccountEditor(Models.Account account)
        {
            return PartialView("_AccountEditor", account);
        }

        [HttpPost]
        public ActionResult AccountEdit(Models.Account account)
        {
            TempData["OperationTarget"] = "Account";
            try
            {
                Models.Account aaccount = db.Account.Where(a => a.idAccount == account.idAccount).First();

                if (account.groupFK == db.Group.Where(g => g.name == "Deleted").First().idGroup)
                {
                    aaccount.name = "Deleted"+account.idAccount;
                    aaccount.mail = account.idAccount.ToString();
                    aaccount.pass = "";
                    aaccount.groupFK = account.groupFK;
                    TempData["OperationResult"] = "Deleted";
                }
                else
                {
                    aaccount.name = account.name;
                    aaccount.mail = account.mail;
                    if (account.pass != null)
                        aaccount.pass = account.pass;
                    aaccount.groupFK = account.groupFK;
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

        public ActionResult _AccountCreate()
        {
            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;
            
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult AccountCreate(Models.Account account)
        {
            TempData["OperationTarget"] = "Account";
            try
            {
                account.creationDate = DateTime.Today;
                db.Account.Add(account);
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

        public PartialViewResult _AccountList()
        {
            int page = GetParam("acp");
            int sort = GetParam("acs");

            if (page < 1)
                page = 1;

            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            int deleted = groups.Where(g => g.name == "Deleted").First().idGroup;

            //IEnumerable<Models.Account> accounts = db.Account.Where(a => a.groupFK != deleted);

            var accounts = from a in db.Account.Where(a => a.groupFK != deleted) select a;

            switch (sort)
            {
                case 1:
                    accounts = accounts.OrderBy(a => a.name);
                    break;
                case 2:
                    accounts = accounts.OrderBy(a => a.mail);
                    break;
                case 3:
                    accounts = accounts.OrderBy(a => a.groupFK);
                    break;
                case 4:
                    accounts = accounts.OrderBy(a => a.creationDate);
                    break;
                case -1:
                    accounts = accounts.OrderByDescending(a => a.name);
                    break;
                case -2:
                    accounts = accounts.OrderByDescending(a => a.mail);
                    break;
                case -3:
                    accounts = accounts.OrderByDescending(a => a.groupFK);
                    break;
                case -4:
                    accounts = accounts.OrderByDescending(a => a.creationDate);
                    break;
            }

            //return PartialView(accounts.Skip(page * PERPAGE).Take(PERPAGE));
            return PartialView(accounts.ToPagedList(page , PERPAGE));

        }

        // Section

        [HttpGet]
        public ActionResult _SectionEditor(Models.Section section)
        {
            return PartialView("_SectionEditor", section);
        }

        [HttpPost]
        public ActionResult SectionEdit(Models.Section section)
        {
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

        public ActionResult _SectionCreate()
        {
            IEnumerable<Models.Group> groups = db.Group;
            ViewBag.groups = groups;

            return PartialView();
        }
        
        [HttpPost]
        public ActionResult SectionCreate(Models.Section section)
        {
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

        public ActionResult _SectionList()
        {
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

        public ActionResult SectionDelete(Models.Section section)
        {
            TempData["OperationTarget"] = "Section";
            try
            {
                foreach(Models.Topic topic in db.Topic.Where(t => t.sectionFK == section.idSection))
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



    }
}