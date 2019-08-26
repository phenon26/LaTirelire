namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commande")]
    public partial class Commande
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commande()
        {
            DetailCommandes = new HashSet<DetailCommande>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(128)]
        public string idClient { get; set; }

        public int statut { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateCommande { get; set; }

        public virtual Client Client { get; set; }

        public virtual Statut Statut1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCommande> DetailCommandes { get; set; }
    }
}
