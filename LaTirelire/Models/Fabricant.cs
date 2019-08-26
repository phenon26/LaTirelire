namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fabricant")]
    public partial class Fabricant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fabricant()
        {
            Produits = new HashSet<Produit>();
        }

        public int id { get; set; }

        public int idAdresse { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(20)]
        public string tel { get; set; }

        public virtual Adresse Adresse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produit> Produits { get; set; }
    }
}
