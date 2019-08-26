using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;


namespace LaTirelire.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        //protected TireBase EFContext = new TireBase();

        protected TireBase EFContext;
        public EFRepository()
        {
            if (HttpContext.Current.Session["contexte"] == null)
            {
                HttpContext.Current.Session["contexte"] = new TireBase();
            }
            else EFContext = (TireBase)HttpContext.Current.Session["contexte"];
        }

        public IEnumerable<T> Creer(T entity)
        {
            EFContext.Set<T>().Add(entity);
            EFContext.SaveChanges();

            return EFContext.Set<T>().ToList();
        }



        public IEnumerable<T> Lister()
        {
            IEnumerable<T> liste = EFContext.Set<T>().ToList();


            return liste;
        }

        public IEnumerable<T> Lister(string inclusion)
        {
            var liste = EFContext.Set<T>().Include(inclusion);


            return liste;
        }

        public bool Supprimer(int id)
        {
            bool IsDeleted = false;
            try
            {
                T objASupp = EFContext.Set<T>().Find(id);
                EFContext.Set<T>().Remove(objASupp);
                EFContext.SaveChanges();

                IsDeleted = true;
            }
            catch (Exception)
            {

                throw new FieldAccessException();
            }
            return IsDeleted;
        }

        public bool SupprimerChaine(string id)
        {
            bool IsDeleted = false;
            try
            {
                T objASupp = EFContext.Set<T>().Find(id);
                EFContext.Set<T>().Remove(objASupp);
                EFContext.SaveChanges();

                IsDeleted = true;
            }
            catch (Exception)
            {

                throw new FieldAccessException();
            }
            return IsDeleted;
        }

        public T Trouver(int id)
        {
            return EFContext.Set<T>().Find(id);
        }

        public T TrouverChaine(string id)
        {
            return EFContext.Set<T>().Find(id);
        }

        public IEnumerable<T> Editer(T entity, int id)
        {
            //EFContext.Entry<T> (entity).State = EntityState.Modified;

            T source = EFContext.Set<T>().Find(id);
            EFContext.Entry<T>(source).CurrentValues.SetValues(entity);
            EFContext.SaveChanges();

            return EFContext.Set<T>().ToList();
        }

        public IEnumerable<T> Editer(T entity, string id)
        {
            //EFContext.Entry<T> (entity).State = EntityState.Modified;

            T source = EFContext.Set<T>().Find(id);
            EFContext.Entry<T>(source).CurrentValues.SetValues(entity);
            EFContext.SaveChanges();

            return EFContext.Set<T>().ToList();
        }
    }
}