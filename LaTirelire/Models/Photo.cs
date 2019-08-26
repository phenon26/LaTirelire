namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo
    {
        public long id { get; set; }

        public int idProduit { get; set; }

        [Required]
        [StringLength(50)]
        public string nomFichier { get; set; }

        public bool? isMain { get; set; }

        public virtual Produit Produit { get; set; }
    }
}
