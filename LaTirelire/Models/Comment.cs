namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int id { get; set; }

        public int idProduit { get; set; }

        [Required]
        [StringLength(128)]
        public string idClient { get; set; }

        public bool visible { get; set; }

        [StringLength(128)]
        public string idModo { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public string text { get; set; }

        public virtual Client Client { get; set; }

        public virtual Produit Produit { get; set; }
    }
}
