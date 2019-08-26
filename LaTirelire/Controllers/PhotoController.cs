using LaTirelire.DataAccess;
using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Controllers
{
    [Authorize(Roles = "responsable,assistant")]
    public class PhotoController : Controller
    {
        IRepository<Photo> repPhotos = new EFRepository<Photo>();
        
        // GET: Photo
        public ActionResult Index(int id =-1)
        {
            IEnumerable<Photo> listePhotos = repPhotos.Lister();
            
            if (id != -1)
            {
                listePhotos = repPhotos.Lister().Where(p => p.idProduit == id);
                ViewBag.OnlyOneProd = false;
            }
            else ViewBag.OnlyOneProd = true;


            ViewBag.idProduit = id;

            return View(listePhotos);
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            return View(repPhotos.Trouver(id));
        }

        // GET: Photo/Add
        public ActionResult Add(int id)
        {
            ViewBag.idProduit = id;
            Photo niouPhoto = new Photo();
            niouPhoto.idProduit = id;
            niouPhoto.nomFichier = "youpiyop";
            
            return View(niouPhoto);
        }
        
        // POST: Photo/Add
        [HttpPost]
        public ActionResult Add(Photo foto, HttpPostedFileBase fichierPhoto)
        {
            try
            {
                
                if (fichierPhoto != null && ModelState.IsValid)
                {
                    // on garde le nom d'origine du fichier 

                    foto.nomFichier = fichierPhoto.FileName;


                    // on écrit dans la base
                    repPhotos.Creer(foto);
                    // on sauvegarde le fichier dans le dossier /photos
                    fichierPhoto.SaveAs(Server.MapPath("~/photos/") + System.IO.Path.GetFileName(fichierPhoto.FileName));

                    return RedirectToAction("Index/" + foto.idProduit);
                }

                else return View(foto);
                

                
            }
            catch (Exception exceptionAjoutPhoto)
            {
                return View();
            }
        }
        // action qui ajoute l'attribut IsMain à la photo désignée 
        //(et mets les IsMain à false pour les autres photos du meme produit)
        public ActionResult IsMain(int id)
        {
            Photo pho = repPhotos.Trouver(id);
            pho.isMain = true;
            repPhotos.Editer(pho, id);
            IEnumerable<Photo> autresPho = repPhotos.Lister().Where(p => p.idProduit == pho.idProduit && p!=pho);
            foreach (var tof in autresPho)
            {
                tof.isMain = false;
                repPhotos.Editer(tof, id);
            }

            return RedirectToAction("Index",new {id=pho.idProduit });
        }


        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repPhotos.Trouver(id));
        }

        // POST: Photo/Edit/5
        [HttpPost]
        public ActionResult Edit(Photo foto, FormCollection collection)
        {
            try
            {
                repPhotos.Editer(foto,(int) foto.id);

                return RedirectToAction("Index/"+foto.idProduit);
            }
            catch
            {
                return View();
            }
        }

        // GET: Photo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repPhotos.Trouver(id));
        }

        // POST: Photo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                int idProd = repPhotos.Trouver(id).idProduit;

                repPhotos.Supprimer(id);

                return RedirectToAction("Index/"+idProd);
            }
            catch
            {
                return View();
            }
        }
    }
}
