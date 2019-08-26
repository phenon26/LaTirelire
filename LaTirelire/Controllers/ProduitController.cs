using LaTirelire.DataAccess;
using LaTirelire.Models;
using LaTirelire.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    [Authorize(Roles = "responsable,assistant")]
    public class ProduitController : Controller
    {
        // init repository des produits
        IRepository<Produit> repProduits = new EFRepository<Produit>();

        private void InitDropDownFabricants()
        {
            // init repository des fabricants
            IRepository<Fabricant> repFab = new EFRepository<Fabricant>();

            // init liste des fabricants : liste de SelectListItems
            List<SelectListItem> lstFab =
                repFab.Lister().Select(f => new SelectListItem
                {
                    Value = f.id.ToString()
                    ,
                    Text = f.Nom

                }).ToList();

            // transfert de la liste à la vue via le ViewBag
            ViewBag.listeFabricants = lstFab;

        }
        
        // GET: Produit
        public ActionResult Index(string ordre="non")
        {

            IEnumerable<Produit> produits = repProduits.Lister();

            switch (ordre)
            {
                case "Fabricant":
                    produits = repProduits.Lister().OrderBy(c => c.Fabricant.Nom);
                    break;
                case "Prix":
                    produits = repProduits.Lister().OrderBy(c => c.Prix);
                    break;
                case "Nom":
                    produits = repProduits.Lister().OrderBy(c => c.Nom);
                    break;
                case "Actif":
                    produits = repProduits.Lister().OrderBy(c => c.Actif);
                    break;
                default:
                    produits = repProduits.Lister();
                    break;
            }


            return View(produits);
        }

        // GET: Produit/Details/5
        public ActionResult Details(int id)
        {
            IRepository<Photo> repPhotos = new EFRepository<Photo>();

            ProduitPhotosViewModel prodphotVM = new ProduitPhotosViewModel();
            prodphotVM.produit = repProduits.Trouver(id);
            prodphotVM.photos = repPhotos.Lister().Where(p => p.idProduit == id);

            return View(prodphotVM);
        }

        // détail partiel pour usage dans les vues du controleur photo (ou autre)
        public PartialViewResult DetailPartiel(int id)
        {
            return PartialView(repProduits.Trouver(id));
        }


        // GET: Produit/Create
        public ActionResult Create()
        {
            InitDropDownFabricants();
            Produit niouProd = new Produit();
            return View(niouProd);
        }

        // POST: Produit/Create
        [HttpPost]
        public ActionResult Create(Produit prod , FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repProduits.Creer(prod);
                    return RedirectToAction("Index");
                }
                else return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Produit/Edit/5
        public ActionResult Edit(int id)
        {
            InitDropDownFabricants();

            return View(repProduits.Trouver(id));
        }

        // POST: Produit/Edit/5
        [HttpPost]
        public ActionResult Edit(Produit prod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string prix1 = string.Format(CultureInfo.InvariantCulture, "{0}", prod.Prix);
                    prod.Prix = double.Parse(prix1, CultureInfo.InvariantCulture);
                    repProduits.Editer(prod,prod.id);

                    return RedirectToAction("Index");
                }
                else return View();
            }
            catch
            {
                InitDropDownFabricants();

                return View();
            }
        }

        public ActionResult Activer (int id)
        {
            Produit prod = repProduits.Trouver(id);
            prod.Actif = true;
            repProduits.Editer(prod, prod.id);

            return RedirectToAction("Index");
        }

        public ActionResult Desactiver(int id)
        {
            Produit prod = repProduits.Trouver(id);
            prod.Actif = false;
            repProduits.Editer(prod, prod.id);

            return RedirectToAction("Index");
        }

        // GET: Produit/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repProduits.Trouver(id));
        }

        // POST: Produit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repProduits.Supprimer(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
