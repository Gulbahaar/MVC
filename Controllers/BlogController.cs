using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private KurumsalDBEntities db = new KurumsalDBEntities();

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blogs.Include("Kategori").ToList().OrderByDescending(x => x.BlogId));
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {

            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string blogimgname = ResimURL.FileName + imginfo.Extension;
                img.Resize(150, 100);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ResimURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blogs.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}