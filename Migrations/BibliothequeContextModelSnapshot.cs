﻿// <auto-generated />
using System;
using BackOfficeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackOfficeApp.Migrations
{
    [DbContext(typeof(BibliothequeContext))]
    partial class BibliothequeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackOfficeApp.Models.Adherent", b =>
                {
                    b.Property<int>("AdherentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdherentID"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomUtilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdherentID");

                    b.ToTable("Adherents");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomUtilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Employe", b =>
                {
                    b.Property<int>("EmployeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeID"));

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fonction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomUtilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeID");

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Emprunt", b =>
                {
                    b.Property<int>("EmpruntID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpruntID"));

                    b.Property<int>("AdherentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRetourPrevu")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRetourReel")
                        .HasColumnType("datetime2");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmpruntID");

                    b.HasIndex("AdherentID");

                    b.HasIndex("ISBN");

                    b.ToTable("Emprunts");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Livre", b =>
                {
                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AnneePublication")
                        .HasColumnType("int");

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ISBN");

                    b.ToTable("Livres");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"));

                    b.Property<int>("AdherentID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DatePrevuRetrait")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("datetime2");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReservationID");

                    b.HasIndex("AdherentID");

                    b.HasIndex("ISBN");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Emprunt", b =>
                {
                    b.HasOne("BackOfficeApp.Models.Adherent", "Adherent")
                        .WithMany()
                        .HasForeignKey("AdherentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackOfficeApp.Models.Livre", "Livre")
                        .WithMany()
                        .HasForeignKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adherent");

                    b.Navigation("Livre");
                });

            modelBuilder.Entity("BackOfficeApp.Models.Reservation", b =>
                {
                    b.HasOne("BackOfficeApp.Models.Adherent", "Adherent")
                        .WithMany()
                        .HasForeignKey("AdherentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackOfficeApp.Models.Livre", "Livre")
                        .WithMany()
                        .HasForeignKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adherent");

                    b.Navigation("Livre");
                });
#pragma warning restore 612, 618
        }
    }
}
