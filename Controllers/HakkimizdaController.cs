﻿using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{

    public class HakkimizdaController : Controller
    {
        KurumsalDBEntities db = new KurumsalDBEntities();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var h = db.Hakkimizdas.ToList();
            return View(h);
        }
        public ActionResult Edit(int id)

        {
            var h = db.Hakkimizdas.Where(x => x.HakkimizdaId == id).FirstOrDefault();
            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id,Hakkimizda h)

        {
            if(ModelState.IsValid)
            {
                var hakkimizda = db.Hakkimizdas.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                hakkimizda.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }

    }
}