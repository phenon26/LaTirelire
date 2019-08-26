namespace LaTirelire.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adresse")]
    public partial class Adresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adresse()
        {
            Clients = new HashSet<Client>();
            Clients1 = new HashSet<Client>();
            Fabricants = new HashSet<Fabricant>();
        }

        public int id { get; set; }

        public int? numero { get; set; }

        [Required]
        [StringLength(40)]
        public string voie { get; set; }

        [Required]
        [StringLength(10)]
        public string codePostal { get; set; }

        [Required]
        [StringLength(40)]
        public string ville { get; set; }

        [StringLength(20)]
        public string pays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Clients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Clients1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fabricant> Fabricants { get; set; }
    }
}
