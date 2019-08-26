using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LaTirelire.DataAccess
{
    public static class Transactions
    {
        public static DbContextTransaction Commencer ()
        {
            TireBase contexte = (TireBase)HttpContext.Current.Session["contexte"];

            return contexte.Database.BeginTransaction();
        }

        public static void Valider(DbContextTransaction trans)
        {
            trans.Commit();
        }

        public static void Annuler(DbContextTransaction trans)
        {
            trans.Rollback();
        }

    }
}