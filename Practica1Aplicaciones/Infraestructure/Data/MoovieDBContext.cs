﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Practica1Aplicaciones.Domain.Models;

#nullable disable

namespace Practica1Aplicaciones.Infraestructure.Data
{
    public partial class MoovieDBContext : DbContext
    {
        public MoovieDBContext()
        {
        }

        public MoovieDBContext(DbContextOptions<MoovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Moovy> Moovies { get; set; }

/* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-VOMGQE6; Initial Catalog=MoovieDB; Integrated Security=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Moovy>(entity =>
            {
                entity.HasKey(e => e.IdMoovie)
                    .HasName("pk_IdMoovie");

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PublicationYear).HasColumnName("Publication_Year");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
