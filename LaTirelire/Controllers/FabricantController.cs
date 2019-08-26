using LaTirelire.DataAccess;
using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    [Authorize(Roles = "responsable,assistant")]
    public class FabricantController : Controller
    {
        IRepository<Fabricant> repFabricants = new EFRepository<Fabricant>();
        
        private void InitDropDownAdresses()
        {
            // init repository d'adresses
            IRepository<Adresse> repAdr = new EFRepository<Adresse>();

            // init de la liste de choix possibles : liste de SelectListItems
            List<SelectListItem> listAdr =
                repAdr.Lister().Select(a => new SelectListItem
                {
                    Value = a.id.ToString()
                ,
                    Text = a.ville + " - " +a.numero.ToString() + " " + a.voie.ToString()
                }
                ).ToList();

            // transfert de la liste vers la vue avec ViewBag
            ViewBag.listeAdresses = listAdr;

        }

        // GET: Fabricant
        public ActionResult Liste()
        {
            return View(repFabricants.Lister());
        }

        // GET: Fabricant/Details/5
        public ActionResult Details(int id)
        {
            return View(repFabricants.Trouver(id));
        }

        // GET: Fabricant/Create
        public ActionResult Create()
        {
            Fabricant niouFab = new Fabricant();
            InitDropDownAdresses();
            return View(niouFab);
        }

        // POST: Fabricant/Create
        [HttpPost]
        public ActionResult Create(Fabricant fab, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                repFabricants.Creer(fab);

                return RedirectToAction("Liste");

            }
            else return View();
        }

        // GET: Fabricant/Edit/5
        public ActionResult Edit(int id)
        {
            InitDropDownAdresses();
            return View(repFabricants.Trouver(id));
        }

        // POST: Fabricant/Edit/5
        [HttpPost]
        public ActionResult Edit(Fabricant fab, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repFabricants.Editer(fab,fab.id);

                    return RedirectToAction("Liste");
                }
                else return View();
            }
            catch
            {
                throw new Exception();
                return View();
            }
        }

        // GET: Fabricant/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repFabricants.Trouver(id));
        }

        // POST: Fabricant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repFabricants.Supprimer(id);

                return RedirectToAction("Liste");
            }
            catch
            {
                return View();
            }
        }
    }
}
