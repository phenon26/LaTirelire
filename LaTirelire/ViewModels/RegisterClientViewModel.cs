using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaTirelire.ViewModels
{
    public class RegisterClientViewModel
    {
        
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
            [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

        public Client client { get; set; }
        public Adresse AdresseFacturation { get; set; }
        public Adresse AdresseLivraison { get; set; }


    }
}