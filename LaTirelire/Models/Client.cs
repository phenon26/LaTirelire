namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Commandes = new HashSet<Commande>();
            Comments = new HashSet<Comment>();
        }

        public string id { get; set; }

        public int idAdresseFacturation { get; set; }

        public int? idAdresseLivraison { get; set; }

        public bool actif { get; set; }

        public int theme { get; set; }

        [StringLength(25)]
        public string nom { get; set; }

        [StringLength(25)]
        public string prenom { get; set; }

        [StringLength(20)]
        public string tel { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Adresse Adresse1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commandes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
