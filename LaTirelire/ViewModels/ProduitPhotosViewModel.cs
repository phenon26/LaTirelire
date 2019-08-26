using LaTirelire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaTirelire.ViewModels
{
    public class ProduitPhotosViewModel
    {
        [Key]
        public Produit produit { get; set; }
        public IEnumerable<Photo> photos { get; set; }

    }
}