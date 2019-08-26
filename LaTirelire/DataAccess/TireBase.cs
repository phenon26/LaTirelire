namespace LaTirelire.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TireBase : DbContext
    {
        public TireBase()
            : base("name=TireBase3")
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DetailCommande> DetailCommandes { get; set; }
        public virtual DbSet<Fabricant> Fabricants { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Statut> Statuts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>()
                .Property(e => e.voie)
                .IsFixedLength();

            modelBuilder.Entity<Adresse>()
                .Property(e => e.codePostal)
                .IsFixedLength();

            modelBuilder.Entity<Adresse>()
                .Property(e => e.ville)
                .IsFixedLength();

            modelBuilder.Entity<Adresse>()
                .Property(e => e.pays)
                .IsFixedLength();

            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Adresse)
                .HasForeignKey(e => e.idAdresseFacturation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.Clients1)
                .WithOptional(e => e.Adresse1)
                .HasForeignKey(e => e.idAdresseLivraison);

            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.Fabricants)
                .WithRequired(e => e.Adresse)
                .HasForeignKey(e => e.idAdresse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Client>()
                .Property(e => e.nom)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.prenom)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.tel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Commandes)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.idClient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.idClient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Commande>()
                .HasMany(e => e.DetailCommandes)
                .WithRequired(e => e.Commande)
                .HasForeignKey(e => e.idCommande)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricant>()
                .Property(e => e.Nom)
                .IsFixedLength();

            modelBuilder.Entity<Fabricant>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Fabricant>()
                .Property(e => e.tel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Fabricant>()
                .HasMany(e => e.Produits)
                .WithRequired(e => e.Fabricant)
                .HasForeignKey(e => e.idFabricant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produit>()
                .Property(e => e.Nom)
                .IsFixedLength();

            modelBuilder.Entity<Produit>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Produit>()
                .Property(e => e.Couleur)
                .IsFixedLength();

            modelBuilder.Entity<Produit>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Produit)
                .HasForeignKey(e => e.idProduit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produit>()
                .HasMany(e => e.DetailCommandes)
                .WithRequired(e => e.Produit)
                .HasForeignKey(e => e.idProduit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produit>()
                .HasMany(e => e.Photos)
                .WithRequired(e => e.Produit)
                .HasForeignKey(e => e.idProduit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Statut>()
                .Property(e => e.nomStatut)
                .IsFixedLength();

            modelBuilder.Entity<Statut>()
                .HasMany(e => e.Commandes)
                .WithRequired(e => e.Statut1)
                .HasForeignKey(e => e.statut)
                .WillCascadeOnDelete(false);
        }
    }
}
