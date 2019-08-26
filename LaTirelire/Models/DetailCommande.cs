namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailCommande")]
    public partial class DetailCommande
    {
        public int id { get; set; }

        public int idCommande { get; set; }

        public int idProduit { get; set; }

        public int? quantite { get; set; }

        public decimal? PrixUnitaire { get; set; }

        public virtual Commande Commande { get; set; }

        public virtual Produit Produit { get; set; }
    }
}
