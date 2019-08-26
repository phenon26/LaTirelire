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
   
    public class HomeController : Controller
    {
        IRepository<Produit> repProds = new EFRepository<Produit>();
        IRepository<Photo> repPhotos = new EFRepository<Photo>();
        
        Photo defautPhoto = new Photo() { id = -1, idProduit = -1, isMain = false, nomFichier = "defaut.jpg" };

        public ActionResult Index()
        {
            
            if (IsClient())
            {
                List<ProduitPhotosViewModel> gallerie = new List<ProduitPhotosViewModel>();
                IEnumerable<Produit> produits = repProds.Lister().Where(pr => pr.Actif == true);
                foreach (var produit in produits)
                {

                    ProduitPhotosViewModel ppVm = new ProduitPhotosViewModel();
                    ppVm.produit = produit;
                    ppVm.photos = repPhotos.Lister().Where(p => p.idProduit == produit.id).ToList();


                    gallerie.Add(ppVm);
                }

                Session["IsClient"] = true;
                //Session["IsModo"] = false;
                ViewBag.IsModo = false;
                return View(gallerie);
            }
            else
            {
                Session["IsClient"] = false;
                //IEnumerable<AspNetRole> lstRoles = repRoles.Lister().Where(r=>r.Id==User.Identity.GetUserId());
                
                    //Session["IsModo"] = HttpContext.User.IsInRole("moderateur");                  
                
                

                return RedirectToAction("AccueilBO");
            }
            
        }

        protected bool IsClient() // teste si l'utilisateur est un client (vrai si n'a aucun rôle)
        {
            IRepository<AspNetRole> repRoles = new EFRepository<AspNetRole>();
            IRepository<Client> repClient = new EFRepository<Client>();
            string userID = User.Identity.GetUserId();
            return (!User.Identity.IsAuthenticated ||(
                repRoles.Lister().Where(
                    c => c.Id == userID
                    ).Count() == 0)
                && (
                repClient.TrouverChaine(userID) !=null
                ));
        }

        [Authorize(Roles = "responsable,admin,moderateur,assistant")]
        public ActionResult AccueilBO()
        {
            IRepository<AspNetUser> repUser = new EFRepository<AspNetUser>();
            IRepository<AspNetRole> repRole = new EFRepository<AspNetRole>();

            ViewBag.IsModo = repUser.TrouverChaine(User.Identity.GetUserId()).AspNetRoles.Contains(
                repRole.Lister().Where(r=>r.Name=="moderateur").First());
            
          

            return View();
        }

        public ActionResult Detail(int id)
        {
            ProduitPhotosViewModel ProdNPhotos = new ProduitPhotosViewModel();
            ProdNPhotos.produit = repProds.Trouver(id);
            ProdNPhotos.photos = repPhotos.Lister().Where(p => p.idProduit == id);
            if (ProdNPhotos.produit.Actif)
            {
                return View(ProdNPhotos);
            }
            else return RedirectToAction("Index");

        }

        public PartialViewResult SimilarProds(int id)
        {
            String couleurProd = repProds.Trouver(id).Couleur;

            IEnumerable<Produit> simProds = repProds.Lister().Where(p => (p.Couleur == couleurProd && p.id !=id)).Take(5);

            return PartialView("_SimilarProds" , simProds);
        }

        // ajout d'un article au panier et affichage du panier

        public ActionResult AjoutPanier(int id)
        {
            IRepository<DetailCommande> repLignComm = new EFRepository<DetailCommande>();

            
            
            Commande panier = (Commande) Session["panier"];
            panier.statut = 0;
            panier.dateCommande = DateTime.Today;

        
            if (panier.DetailCommandes.Where(dt=>dt.idProduit == id).Count()>0)
            {
                panier.DetailCommandes.Where(dt => dt.idProduit == id).First().quantite += 1;
            }
            else panier.DetailCommandes.Add(new DetailCommande
            {
                idProduit = id
            ,
                PrixUnitaire = (decimal)repProds.Trouver(id).Prix
            ,
                quantite = 1
            ,
                Commande = panier
                , Produit = repProds.Trouver(id)
            });

            Session["panier"] = panier;

            return RedirectToAction("Index","Panier");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}