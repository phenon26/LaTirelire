using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaTirelire.ViewModels
{
    public class OLDRegisterClientViewModel
    {
        // copie du RegisterViewModel (ds fichier AccountViewModels.cs)
        [Key]
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        // client View Model (moins les réf aux Commandes et Comments):
        [StringLength(132)]
        public string id { get; set; }

        public int idAdresseFacturation { get; set; }

        public int? idAdresseLivraison { get; set; }

        public bool actif { get; set; }

        
        public SelectListItem theme { get; set; }

        [StringLength(25)]
        public string nom { get; set; }

        [StringLength(25)]
        public string prenom { get; set; }

        [StringLength(20)]
        public string tel { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Adresse Adresse1 { get; set; }

        



    }
}