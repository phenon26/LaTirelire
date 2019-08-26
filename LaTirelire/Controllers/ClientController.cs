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
    [Authorize]
    public class ClientController : Controller
    {
        IRepository<Client> repClient = new EFRepository<Client>();
        IRepository<AspNetUser> repUser = new EFRepository<AspNetUser>();
        IRepository<AspNetRole> repRole = new EFRepository<AspNetRole>();
        IRepository<Comment> repComment = new EFRepository<Comment>();




        // GET: Client
        [Authorize(Roles="responsable,admin")]
        public ActionResult Index()
        {

            return View(repClient.Lister());
        }
        [Authorize(Roles = "responsable,admin")]
        public ActionResult IndexBO() // index pour le BackOffice (tous les utilisateurs sans les clients)
        {
            List<UsersBoViewModel> lstUsers = new List<UsersBoViewModel>();

            foreach (var humain in repUser.Lister())
            {
                if (repClient.TrouverChaine(humain.Id) == null)
                {

                    lstUsers.Add(UserBOInit(humain));
                }
            }
            return View(lstUsers);
        }
        [Authorize(Roles = "responsable,admin")]
        public ActionResult Activer(string id)
        {
            Client c = repClient.TrouverChaine(id);
            c.actif = true;
            repClient.Editer(c, id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "responsable,admin")]
        public ActionResult Desactiver(string id)
        {
            Client c = repClient.TrouverChaine(id);
            c.actif = false;
            repClient.Editer(c, id);
            return RedirectToAction("Index");
        }


        private UsersBoViewModel UserBOInit(AspNetUser user)
        {
            UsersBoViewModel nonClient = new UsersBoViewModel()
            { id = user.Id, email = user.Email, admin = false, assistant = false, responsable = false, moderateur = false };

            IEnumerable<AspNetRole> lstUserRoles = user.AspNetRoles;

            if (lstUserRoles.Count() != 0)
            {
                if (lstUserRoles.Where(r => r.Name == "admin").Count() != 0)
                {
                    nonClient.admin = true;
                }
                if (lstUserRoles.Where(r => r.Name == "assistant").Count() != 0)
                {
                    nonClient.assistant = true;
                }
                if (lstUserRoles.Where(r => r.Name == "responsable").Count() != 0)
                {
                    nonClient.responsable = true;
                }
                if (lstUserRoles.Where(r => r.Name == "moderateur").Count() != 0)
                {
                    nonClient.moderateur = true;
                }
            }
            repUser.Editer(user, user.Id);

            return nonClient;
        }



        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View(repClient.Trouver(id));
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            Client niouClient = new Client();
            return View(niouClient);
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client c, FormCollection collection)
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

        // GET: Client/Edit/5
        [Authorize(Roles = "responsable,admin")]
        public ActionResult Edit(string id)
        {
            return View(repClient.TrouverChaine(id));
        }

        // POST: Client/Edit/5
        [Authorize(Roles = "responsable,admin")]
        [HttpPost]
        public ActionResult Edit(Client client)
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

        // GET: Client/Edit/5
        [Authorize(Roles = "responsable,admin")]
        public ActionResult EditBO(string id)
        {
            return View(UserBOInit(repUser.TrouverChaine(id)));
        }

        // POST: Client/Edit/5
        [Authorize(Roles = "responsable,admin")]
        [HttpPost]
        public ActionResult EditBO(UsersBoViewModel uBoViMo) // permet d'éditer les rôles
        {
            try
            {

                AspNetUser utilisateur = repUser.TrouverChaine(uBoViMo.id);
                IEnumerable<AspNetRole> userRoles = utilisateur.AspNetRoles;
                string AdminRoleId = repRole.Lister().Where(r => r.Name == "admin").First().Id;
                string AssistantRoleId = repRole.Lister().Where(r => r.Name == "assistant").First().Id;
                string ResponsableRoleId = repRole.Lister().Where(r => r.Name == "responsable").First().Id;
                string ModerateurRoleId = repRole.Lister().Where(r => r.Name == "moderateur").First().Id;

                if (uBoViMo.admin)
                {
                    if (userRoles.Where(r=>r.Name=="admin").Count()==0)
                    {
                        utilisateur.AspNetRoles.Add(repRole.TrouverChaine(AdminRoleId));
                    }                    
                }
                else
                {                    
                    if (userRoles.Where(r => r.Name == "admin").Count() != 0)
                    {
                        utilisateur.AspNetRoles.Remove(repRole.TrouverChaine(AdminRoleId));
                    }
                }

                if (uBoViMo.assistant)
                {
                    if (userRoles.Where(r => r.Name == "assistant").Count() == 0)
                    {
                        utilisateur.AspNetRoles.Add(repRole.TrouverChaine(AssistantRoleId));
                    }
                }
                else
                {
                    if (userRoles.Where(r => r.Name == "assistant").Count() != 0)
                    {
                        utilisateur.AspNetRoles.Remove(repRole.TrouverChaine(AssistantRoleId));
                    }
                }

                if (uBoViMo.responsable)
                {
                    if (userRoles.Where(r => r.Name == "responsable").Count() == 0)
                    {
                        utilisateur.AspNetRoles.Add(repRole.TrouverChaine(ResponsableRoleId));
                    }
                }
                else
                {
                    if (userRoles.Where(r => r.Name == "responsable").Count() != 0)
                    {
                        utilisateur.AspNetRoles.Remove(repRole.TrouverChaine(ResponsableRoleId));
                    }
                }

                if (uBoViMo.moderateur)
                {
                    if (userRoles.Where(r => r.Name == "moderateur").Count() == 0)
                    {
                        utilisateur.AspNetRoles.Add(repRole.TrouverChaine(ModerateurRoleId));
                    }
                }
                else
                {
                    if (userRoles.Where(r => r.Name == "moderateur").Count() != 0)
                    {
                        utilisateur.AspNetRoles.Remove(repRole.TrouverChaine(ModerateurRoleId));
                    }
                }

                return RedirectToAction("IndexBO");
            }
            catch
            {
                return View();
            }
        }


        //// GET: Client/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    return View(repClient.TrouverChaine(id));
        //}

        // POST: Client/Delete/5
        //[HttpPost]
        //public ActionResult Delete(string id, FormCollection collection) // attention ne supprime que le client (pas le user associé)
        //                                                                 // il y a un bug : on envoie dans l'Uri plein de %20 après l'id ...
        //{
        //    try
        //    {
        //        repClient.SupprimerChaine(id);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult DeleteBO(string id)
        {
            return View(repUser.TrouverChaine(id));
        }

        [HttpPost]
        [Authorize(Roles = "responsable,admin")]
        public ActionResult DeleteBO(string id, FormCollection collection) // attention ne supprime que le user (pas l'éventuel client associé)
                                                                           // il y a un bug : on envoie dans l'Uri plein de %20 après l'id ...
        {
            try
            {
                repUser.SupprimerChaine(id.TrimEnd());

                return RedirectToAction("IndexBo");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "responsable,moderateur,assistant")]
        public PartialViewResult ListeAvisModeration ()
        {
            // renvoie la liste des avis qui n'ont pas encore été modérés
            return PartialView("_ListeAvisModeration", repComment.Lister().Where(a=>a.idModo==null));
        }
        [Authorize(Roles = "responsable,moderateur,assistant")]
        public ActionResult ListeAvis()
        {
            return View(repComment.Lister().OrderBy(a=>a.date));
        }


        // GET : rédiger un commentaire
        [AllowAnonymous]
        public PartialViewResult WriteAvis(int id) // id : idProduit
        {
            string idCl = User.Identity.GetUserId();
            Comment niouAvis = new Comment()
            { idProduit = id, idClient = idCl, visible = false, date = DateTime.Now };


            Client c = repClient.TrouverChaine(idCl);
            if (c != null) // s'il existe un client avec l'id du user courant
            {
                // s'il n'y a pas encore de commentaire pour ce produit et ce client
                if (repComment.Lister().Where(co => co.idProduit == id && co.idClient == idCl).Count() == 0)
                {
                    bool produit_deja_commande = false;
                    foreach (var commande in c.Commandes)
                    {
                        foreach (var item in commande.DetailCommandes)
                        {
                            if (item.idProduit == id)
                            {
                                produit_deja_commande = true;
                            }
                        }
                    }
                    if (produit_deja_commande)
                    {
                        ViewBag.deja_commande = true;
                    }
                    else
                    {
                        ViewBag.deja_commande = false;
                        niouAvis.text = "Vous ne pouvez commenter que les produits que vous avez déjà achetés...";
                        // dire à l'utilisateur qu'il doit commander le produit pour pouvoir laisser un commentaire
                    }
                }

                else
                {
                    ViewBag.deja_commande = false;
                    niouAvis.text = repComment.Lister().Where(co => co.idProduit == id && co.idClient == idCl).First().text;
                }
            }
            else
            {
                ViewBag.deja_commande = false;
                niouAvis.text = "Vous n'êtes pas connecté comme client. Il faut être un client connecté pour pouvoir rédiger un avis.";
            }



            return PartialView(niouAvis);
        }

        //POST : récupérer le commentaire et l'écrire ds la base
        
        [HttpPost]
        public ActionResult WriteAvis(Comment avis)
        {
            if (ModelState.IsValid && avis.text != null)
            {
                Client c = repClient.TrouverChaine(avis.idClient);

                repComment.Creer(avis);


            }


            return RedirectToAction("Detail", "Home", new { id = avis.idProduit });
        }

        // vue partielle pour afficher les commentaires
        [AllowAnonymous]
        public PartialViewResult AvisProd(int id) // id est (ici) l'id d'un produit
        {
            
                       

            IEnumerable<Comment> lstAvis = repComment.Lister().Where(c => c.visible == true && c.idProduit == id);

            foreach (Comment avis in lstAvis)
            {
                avis.Client = repClient.TrouverChaine(avis.idClient);
            }


            return PartialView(lstAvis);
        }
        [Authorize(Roles = "moderateur,assistant")]
        public ActionResult AcceptAvis (int id)
        {

            Comment avis = repComment.Trouver(id);
            avis.idModo = User.Identity.GetUserId();
            avis.visible = true;
            repComment.Editer(avis, id);

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "moderateur,assistant")]
        public ActionResult RefuseAvis(int id)
        {

            Comment avis = repComment.Trouver(id);
            avis.idModo = User.Identity.GetUserId();
            avis.visible = false;
            repComment.Editer(avis, id);

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "moderateur,assistant")]
        public ActionResult DeleteAvis(int id)
        {
            return View(repComment.Trouver(id));
        }

        [HttpPost]
        public ActionResult DeleteAvis(int id,FormCollection collection)
        {

            repComment.Supprimer(id);
            return RedirectToAction("ListeAvis");
        }

    }
}
