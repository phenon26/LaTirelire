namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Produit")]
    public partial class Produit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produit()
        {
            Comments = new HashSet<Comment>();
            DetailCommandes = new HashSet<DetailCommande>();
            Photos = new HashSet<Photo>();
        }

        public int id { get; set; }

        public int idFabricant { get; set; }

        public bool Actif { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom { get; set; }

        public double? Prix { get; set; }

        public int? Hauteur { get; set; }

        public int? Largeur { get; set; }

        public int? Longueur { get; set; }

        public int? Poids { get; set; }

        public int? Capacite { get; set; }

        public string Description { get; set; }

        [StringLength(20)]
        public string Couleur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCommande> DetailCommandes { get; set; }

        public virtual Fabricant Fabricant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
