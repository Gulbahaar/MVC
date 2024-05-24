using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDBEntities db = new KurumsalDBEntities();
        // GET: Hizmet
        public ActionResult Index()
        {
            return View(db.Hizmets.ToList());
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet,HttpPostedFileBase ResimURL)
        {
            if(ModelState.IsValid)
            {
                if (ResimURL != null)
                {

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string logoname = ResimURL.FileName + imginfo.Extension;
                    img.Resize(50, 100);
                    img.Save("~/Uploads/Hizmet/" + logoname);

                    hizmet.ResimURL = "/Uploads/Hizmet/" + logoname;

                }
                db.Hizmets.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(hizmet);
        }
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var hizmet = db.Hizmets.Find(id);
            if (hizmet==null)
            {
                return HttpNotFound();
            }
            return View(hizmet);


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id,Hizmet hizmet,HttpPostedFileBase ResimURL)
        {

            if (ModelState.IsValid)
            {
                var h = db.Hizmets.Where(x => x.HizmetId == id).SingleOrDefault();
                if (ResimURL!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string logoname = ResimURL.FileName + imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Hizmet/" + logoname);

                    h.ResimURL = "/Uploads/Hizmet/" + logoname;
                }
                h.Baslik = hizmet.Baslik;
                h.Aciklama = hizmet.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var h = db.Hizmets.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Hizmets.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}