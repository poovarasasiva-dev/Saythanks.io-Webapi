using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Saythanks
{
    public partial class SaythanksContext : DbContext
    {
        public SaythanksContext()
        {
        }

        public SaythanksContext(DbContextOptions<SaythanksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArInternalMetadatum> ArInternalMetadata { get; set; }
        public virtual DbSet<Inbox> Inboxes { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=Saythanks;Username=postgres;Password=hack");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<ArInternalMetadatum>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("ar_internal_metadata_pkey");

                entity.ToTable("ar_internal_metadata");

                entity.Property(e => e.Key)
                    .HasColumnType("character varying")
                    .HasColumnName("key");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp(6) without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp(6) without time zone")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasColumnType("character varying")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Inbox>(entity =>
            {
                entity.HasKey(e => e.AuthId)
                    .HasName("inboxes_pk");

                entity.ToTable("inboxes");

                entity.Property(e => e.AuthId).HasColumnName("auth_id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EmailEnabled)
                    .HasColumnName("email_enabled")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Enabled)
                    .HasColumnName("enabled")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("notes_pk");

                entity.ToTable("notes");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("public.gen_random_uuid()");

                entity.Property(e => e.Archived).HasColumnName("archived");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body");

                entity.Property(e => e.Byline).HasColumnName("byline");

                entity.Property(e => e.InboxesAuthId)
                    .IsRequired()
                    .HasColumnName("inboxes_auth_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.InboxesAuth)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.InboxesAuthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notes_inboxes");
            });

            modelBuilder.Entity<SchemaMigration>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("schema_migrations_pkey");

                entity.ToTable("schema_migrations");

                entity.Property(e => e.Version)
                    .HasColumnType("character varying")
                    .HasColumnName("version");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
