using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        int PERPAGE = 20;
        Models.AiBEntities db = new Models.AiBEntities();


        // GET: Forum
        public ActionResult Index()
        {
            IEnumerable <Models.Section> sections = db.Section;
            return View(sections);
        }

        [OutputCache(Duration = 30, VaryByParam = "idSection")]
        public ActionResult Section(string idSection)
        {
            int id;
            Int32.TryParse(idSection, out id);

            if (!CheckReadPermision(db.Section.Find(id)))
                return HttpNotFound();

            int page = GetParam("page");

            if (page < 1)
                page = 1;


            try
            {
                ViewBag.sectionName = db.Section.Find(id).name;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            IEnumerable<Models.Topic> topics = db.Topic.Where(t => t.Section.idSection == id).OrderByDescending(t => t.Post.Max(p => p.date));
            return View(topics.ToPagedList(page, PERPAGE));
        }

        [OutputCache(Duration = 30, VaryByParam = "idTopic")]
        public ActionResult Topic(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            if (!CheckReadPermision(db.Topic.Find(id).Section))
                return HttpNotFound();


            int page = GetParam("page");

            if (page < 1)
                page = 1;

            IEnumerable<Models.Post> posts;
            try
            {
                ViewBag.topicName = db.Topic.Find(id).name;
                posts = db.Post.Where(p => p.Topic1.idTopic == id).OrderBy(p => p.date);
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }

            return View(posts.ToPagedList(page, PERPAGE));
        }

        public ActionResult TopicEnd(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            if (!CheckReadPermision(db.Topic.Find(id).Section))
                return HttpNotFound();

            IEnumerable<Models.Post> posts;
            int page;
            try
            {
                ViewBag.topicName = db.Topic.Find(id).name;
                posts = db.Post.Where(p => p.Topic1.idTopic == id).OrderBy(p => p.date);
                page = posts.Count() / PERPAGE + 1;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }

            return View("Topic", posts.ToPagedList(page, PERPAGE));
        }

        [HttpGet]
        [Authorize]
        public ActionResult NewTopic(string idSection)
        {
            int id;
            Int32.TryParse(idSection, out id);

            if (!CheckWritePermision(db.Section.Find(id)))
                return HttpNotFound();


            Models.NewTopic topic = new Models.NewTopic();
            try
            {
                topic.Section = db.Section.Find(id).idSection;
                /* TUTAJ POPRAWIC
                 * 
                 */
                //topic.Author = "AUTOR";
                topic.Author = db.AspNetUsers.Where(a => a.UserName == User.Identity.Name.ToString()).First().Id;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            return View(topic);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult NewTopic(Models.NewTopic newTopic)
        {
            if (!CheckWritePermision(db.Topic.Find(newTopic.Section).Section))
                return HttpNotFound();

            Models.Post post = new Models.Post();
            Models.Topic topic = new Models.Topic();
            try
            {
                post.text = newTopic.Text;
                post.author = newTopic.Author;
                post.date = DateTime.Now;
                post.Topic1 = topic;


                topic.sectionFK = newTopic.Section;
                topic.name = newTopic.Name;
                topic.Post.Add(post);

                post.date = DateTime.Now;
                
                db.Topic.Add(topic);
                db.Post.Add(post);
                
                db.SaveChanges();
                return RedirectToAction("Topic", new { idtopic = topic.idTopic });
            }
            catch
            {
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult NewPost(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            if (!CheckWritePermision(db.Topic.Find(id).Section))
                return HttpNotFound();

            Models.Post post = new Models.Post();
            try
            {
                post.topic = db.Topic.Find(id).idTopic;
                /* TUTAJ POPRAWIC
                 * 
                 */
                post.author = db.AspNetUsers.Where(a => a.UserName == User.Identity.Name.ToString()).First().Id;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            return View(post);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(Models.Post post)
        {
            if (!CheckWritePermision(post.Topic1.Section))
                return HttpNotFound();

            try
            {
                post.date = DateTime.Now;
                db.Post.Add(post);
                db.SaveChanges();
            }
            catch
            {
            }
            
            return RedirectToAction("TopicEnd", new { idtopic = post.topic});
        }

        private int GetParam(string paramName)
        {
            int param;
            Int32.TryParse(Request.QueryString.Get(paramName), out param);
            if (param == 0)
                param = 1;
            return param;
        }

        private Boolean CheckReadPermision(Models.Section section)
        {
            try
            {
                if (db.AspNetUsers.Where(a => a.UserName == User.Identity.Name.ToString()).First().Group1.Power >= section.GroupRead.Power)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private Boolean CheckWritePermision(Models.Section section)
        {
            try
            {
                if (db.AspNetUsers.Where(a => a.UserName == User.Identity.Name.ToString()).First().Group1.Power >= section.GroupWrite.Power)
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