using LaTirelire.DataAccess;
using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    [Authorize(Roles = "responsable,assistant")]
    public class CommandeController : Controller
    {
        IRepository<Commande> repoComm = new EFRepository<Commande>();
        IRepository<Statut> repoStatut = new EFRepository<Statut>();
        
        // GET: Commande
        public ActionResult Index(string ordre="statut")
        {
            
            switch (ordre)
            {
                case "nom":
                    return View(repoComm.Lister().OrderByDescending(c => c.Client.nom));
                case "date":
                    return View(repoComm.Lister().OrderByDescending(c => c.dateCommande));
                    break;
                case "statut":
                    return View(repoComm.Lister().OrderBy(c => c.statut));
                default:
                    return View(repoComm.Lister().OrderByDescending(c => c.dateCommande));
                    break;
            }
            return View(repoComm.Lister().OrderBy(c=>c.statut));
        }
        [Authorize(Roles = "responsable")]
        public ActionResult StatutPrecedent(int id)
        {
            Commande c = repoComm.Trouver(id);
            if (c.statut > 1)
            {
                c.statut -= 1;
            }
            repoComm.Editer(c,id);

            return RedirectToAction("Index",new {ordre="date" });
        }

        public ActionResult StatutSuivant(int id)
        {
            Commande c = repoComm.Trouver(id);
            if (c.statut<5)
            {
                c.statut += 1;
            }
            repoComm.Editer(c, id);

            return RedirectToAction("Index", new { ordre = "date" });
        }

        // GET: Commande/Details/5
        public ActionResult Details(int id)
        {
            Commande c = repoComm.Trouver(id);
            VariablesPanier varPan = PanierController.TotalDuPanier(c);
            ViewBag.Poids = varPan.poidsTotal / 1000;
            ViewBag.FraisDePort = varPan.fraisPort;
            ViewBag.TotalCommande = varPan.montantTotal;

            return View(c);
        }



        // GET: Commande/Edit/5
        [Authorize(Roles = "responsable")]
        public ActionResult Edit(int id)
        {
            Commande c = repoComm.Trouver(id);
            InitDropDownStatut();

            VariablesPanier varPan = PanierController.TotalDuPanier(c);
            ViewBag.Poids = varPan.poidsTotal / 1000;
            ViewBag.FraisDePort = varPan.fraisPort;
            ViewBag.TotalCommande = varPan.montantTotal;

            return View(c);
        }

        private void InitDropDownStatut()
        {
            List<SelectListItem> lstStatuts = new List<SelectListItem>();
            foreach (var item in repoStatut.Lister())
            {
                lstStatuts.Add(new SelectListItem() { Text = item.nomStatut, Value = item.id.ToString() });
            }
            ViewBag.dropStatuts = lstStatuts;
        }

        // POST: Commande/Edit/5
        [HttpPost]
        public ActionResult Edit(Commande c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repoComm.Editer(c, c.id);
                }
                

                return RedirectToAction("Index");
            }
            catch
            {
                InitDropDownStatut();
                return View();
            }
        }

        // GET: Commande/Delete/5
        [Authorize(Roles = "responsable")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Commande/Delete/5
        // Avec Approche TRANSACTIONNELLE
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Appel à transaction
            DbContextTransaction t = Transactions.Commencer();

            try
            {
                EFRepository<Commande> repCommande = new EFRepository<Commande>();
                EFRepository<DetailCommande> repDetail = new EFRepository<DetailCommande>();

                Commande commande = repCommande.Trouver(id);
                               
                // Première suppression : détails
                foreach (var d in commande.DetailCommandes)
                {
                    repDetail.Supprimer(d.id);
                }

                // Seconde suppression : commande
                repCommande.Supprimer(commande.id);

                // Confirmation de la transaction
                Transactions.Valider(t);

                return RedirectToAction("Index");
            }
            catch
            {
                // Annulation de la transaction
                Transactions.Annuler(t);
                return View();
            }
        }
    }
}
