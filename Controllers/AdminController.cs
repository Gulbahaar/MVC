using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        KurumsalDBEntities db = new KurumsalDBEntities();
        public ActionResult Index()
        {
            var sorgu = db.Kategoris.ToList();
            return View(sorgu);
        }
        public ActionResult Login() 
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin) 
        {
            var login = db.Admins.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if(login.Eposta==admin.Eposta && login.Sifre==admin.Sifre)
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Kullanıcı adı veya şifre yanlış";
            return View(admin);

        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}