using LaTirelire.DataAccess;
using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    [Authorize(Roles = "responsable")]
    public class AdresseController : Controller
    {
        IRepository<Adresse> repAdresses = new EFRepository<Adresse>();

        // GET: Adresse
        public ActionResult Index()
        {
            return View(repAdresses.Lister());
        }

        // GET: Adresse/Details/5
        public PartialViewResult Details(int id)
        {
            return PartialView("_AdresseDetails",repAdresses.Trouver(id));
        }

        // GET: Adresse/Create
        [AllowAnonymous]
        public PartialViewResult Create()
        {
            Adresse niouAdresse = new Adresse();
            niouAdresse.pays = "France";
            return PartialView("_AdresseCreate",niouAdresse);
        }

        // POST: Adresse/Create
        [HttpPost]
        public ActionResult Create(Adresse adr, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    repAdresses.Creer(adr);
                    return RedirectToAction("Index");
                }
                else return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Adresse/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(repAdresses.Trouver(id));
        }

        // POST: Adresse/Edit/5
        [HttpPost]
        public ActionResult Edit(Adresse adr, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    repAdresses.Editer(adr, adr.id);
                    return RedirectToAction("Index");
                }
                else return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Adresse/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repAdresses.Trouver(id));
        }

        // POST: Adresse/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repAdresses.Supprimer(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
