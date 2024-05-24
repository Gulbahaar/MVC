using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBEntities db = new KurumsalDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkimizda()
        {
            return View(db.Hakkimizdas.SingleOrDefault());
        }
        public ActionResult Hizmetlerimiz()
        {
            return View(db.Hizmets.ToList().OrderByDescending(x=>x.HizmetId));
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null, string email=null,string konu=null,string mesaj=null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalweb@gmail.com";
                WebMail.Password = "Kurumsal97";
                WebMail.SmtpPort = 587;
                WebMail.Send("kurumsalweb@gmail.com", konu, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarılı bir şekilde gönderilmiştir.";
            }
            else {
                ViewBag.Uyari = "Hata Oluştu. Tekrar Deneyiniz.";
            }
            return View();
        }
    }
}