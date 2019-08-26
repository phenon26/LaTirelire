using LaTirelire.DataAccess;
using LaTirelire.Models;
using LaTirelire.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    public class PanierController : Controller
    {
        IRepository<Commande> repComm = new EFRepository<Commande>();
        IRepository<DetailCommande> repDC = new EFRepository<DetailCommande>();
        IRepository<Client> repClient = new EFRepository<Client>();

        // GET: Panier
        public ActionResult Index()
        {
            Commande panier = (Commande)Session["panier"];
            VariablesPanier varPan = TotalDuPanier(panier);

            ViewBag.Poids = varPan.poidsTotal / 1000;
            ViewBag.FraisDePort = varPan.fraisPort;
            ViewBag.TotalCommande = varPan.montantTotal;

            ViewBag.TarifPort = Outils.OutilsTirelire.fraisPort;
            ViewBag.TranchePoidsPort = Outils.OutilsTirelire.poidsPort/1000;

            return View(panier.DetailCommandes);
        }

        public static VariablesPanier TotalDuPanier(Commande panier)
        {
            VariablesPanier varPanier = new VariablesPanier();
            varPanier.montantTotal = 0;
            varPanier.poidsTotal = 0;

            foreach (var item in panier.DetailCommandes)
            {
                varPanier.montantTotal += (decimal)(item.PrixUnitaire * item.quantite);
                varPanier.poidsTotal += (decimal)item.Produit.Poids * (int)item.quantite; // en grammes !!!

            }
            int tranchesPoids = (int)(varPanier.poidsTotal / (decimal)Outils.OutilsTirelire.poidsPort) + 1;

            if (varPanier.montantTotal == 0)
            {
                varPanier.fraisPort = 0; // frais de port à 0 tq qu'il n'y a rien ds le panier !!
            }
            else
            {
                varPanier.fraisPort = tranchesPoids * (decimal)Outils.OutilsTirelire.fraisPort;
            }

            varPanier.montantTotal += varPanier.fraisPort;
            return varPanier;
        }



        // POST: Panier/index mis à jour


        public PartialViewResult PartielPanier()
        {
            Commande panier = (Commande)Session["panier"];
            PartielPanierViewModel ppvm = new PartielPanierViewModel();
            ppvm.nbreArticles = panier.DetailCommandes.Count;
            ppvm.montantPanier = TotalDuPanier(panier).montantTotal;

            return PartialView("_PartielPanier", ppvm);
        }

        // GET: Panier/Create
        [Authorize]
        public ActionResult Commander()
        {

            Commande laCommande = (Commande)Session["panier"];
            
            laCommande.idClient = User.Identity.GetUserId();
            
            laCommande.dateCommande = DateTime.Now;
            laCommande.statut = 1;
            laCommande.Client = repClient.TrouverChaine(laCommande.idClient);

            

            repComm.Creer(laCommande);

            Session["panier"] = new Commande();

            return RedirectToAction("Index","Home");
        }

       

        // POST: Panier/Create
        [HttpPost]
        public ActionResult Commander(FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // QtePlus et QteMoins pour augmenter et diminuer la quantité d'une ligne de commande
        public ActionResult QtePlus(int id)
        {
            try
            {
                Commande panier = (Commande)Session["panier"];
                panier.DetailCommandes.Where(d => d.idProduit == id).First().quantite += 1;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult QteMoins(int id)
        {
            try
            {
                Commande panier = (Commande)Session["panier"];
                if (panier.DetailCommandes.Where(d => d.idProduit == id).First().quantite > 1)
                { panier.DetailCommandes.Where(d => d.idProduit == id).First().quantite -= 1; }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //POST : Maj des quantités du panier : ne fonctionne pas car
        // on ne récupère rien du formulaire
        [HttpPost]
        public ActionResult Maj(IEnumerable<DetailCommande> dc)
        {
            Commande panier = (Commande)Session["panier"];
            if (dc.Count() > 0)
            {


                int[] tabQuantites = new int[dc.Count()];
                int i = 0;
                foreach (var item in dc)
                {
                    tabQuantites[i] = (int)item.quantite;

                    i += 1;
                }
                i = 0;
                foreach (DetailCommande item in panier.DetailCommandes)
                {
                    item.quantite = tabQuantites[i];
                    i += 1;
                }
            }


            return RedirectToAction("Index");
        }


        // GET: Panier/DeleteLine/5
        public ActionResult DeleteLine(int id)
        {
            Commande panier = (Commande)Session["panier"];
            panier.DetailCommandes.Remove(panier.DetailCommandes.Where(dc => dc.idProduit == id).First());

            return RedirectToAction("Index");
        }
        public ActionResult MajLine(int id, int qte)
        {
            Commande panier = (Commande)Session["panier"];
            panier.DetailCommandes.Where(dc => dc.idProduit == id).First().quantite = qte;

            return RedirectToAction("Index");
        }

    }
    public class VariablesPanier
    {
        public decimal poidsTotal { get; set; }
        public decimal fraisPort { get; set; }
        public decimal montantTotal { get; set; }

    }
}
