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
        Models.AiBforumEntities db = new Models.AiBforumEntities();


        // GET: Forum
        public ActionResult Index()
        {
            IEnumerable <Models.Section> sections = db.Section;
            return View(sections);
        }

        public ActionResult Section(string idSection)
        {
            int id;
            Int32.TryParse(idSection, out id);

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

        public ActionResult Topic(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            int page = GetParam("page");

            if (page < 1)
                page = 1;

            try
            {
                ViewBag.topicName = db.Topic.Find(id).name;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            IEnumerable<Models.Post> posts = db.Post.Where(p => p.Topic1.idTopic == id).OrderBy(p => p.date);

            return View(posts.ToPagedList(page, PERPAGE));
        }
        public ActionResult TopicEnd(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            IEnumerable<Models.Post> posts = db.Post.Where(p => p.Topic1.idTopic == id).OrderBy(p => p.date);
            int page = posts.Count() / PERPAGE + 1;

            return View("Topic",posts.ToPagedList(page, PERPAGE));
        }

        [HttpGet]
        public ActionResult NewTopic(string idSection)
        {
            int id;
            Int32.TryParse(idSection, out id);

            Models.NewTopic topic = new Models.NewTopic();
            try
            {
                topic.Section = db.Section.Find(id).idSection;
                /* TUTAJ POPRAWIC
                 * 
                 */
                topic.Author = 1;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            return View(topic);
        }

        [HttpPost]
        public ActionResult NewTopic(Models.NewTopic newTopic)
        {
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
        public ActionResult NewPost(string idTopic)
        {
            int id;
            Int32.TryParse(idTopic, out id);

            Models.Post post = new Models.Post();
            try
            {
                post.topic = db.Topic.Find(id).idTopic;
    /* TUTAJ POPRAWIC
     * 
     */
                post.author = 1;
            }
            catch
            {
                return HttpNotFound(); //Stronka 404
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult NewPost(Models.Post post)
        {
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
    }
}