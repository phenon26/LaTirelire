using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace LaTirelire
{
     public interface IRepository<T> where T : class
    {
        

        IEnumerable<T> Lister();
        IEnumerable<T> Lister(string inclusion);

        bool Supprimer(int id);
        bool SupprimerChaine(string id);
        IEnumerable<T> Creer(T entity);
        T Trouver(int id);
        T TrouverChaine(string id);
        IEnumerable<T> Editer(T entity, int id);
        IEnumerable<T> Editer(T entity, string id);

    }
}