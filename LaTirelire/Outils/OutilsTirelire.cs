using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.Outils
{

    //public enum ThemeClient { clair, sombre, orange };


    public static class OutilsTirelire
    {
        // fraisPort définit les frais de port (en €) pour le client pour un poids = poidsPort
        public static double fraisPort { get; set; } = 3;

        // poidsPort en grammes, permet de calculer les frais de port de la commande = fraisPort*(int)PoidsCommande/poidsPort
        public static double poidsPort { get; set; } = 1500;

        // SelectListItems pour DropDownList de Thèmes Clients
        public static SelectListItem clair { get; set; } = new SelectListItem {Value = "0",Text ="clair" };
        public static SelectListItem sombre { get; set; } = new SelectListItem { Value = "1", Text = "sombre" };
        public static SelectListItem couleurs { get; set; } = new SelectListItem { Value = "2", Text = "couleurs" };
        public static IEnumerable<SelectListItem> themes = new List<SelectListItem> { clair, sombre, couleurs };



}
    


}