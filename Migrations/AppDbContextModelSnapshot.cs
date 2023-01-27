﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vendinha.Data;

#nullable disable

namespace Vendinha.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Vendinha.Models.Cliente", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<int>("idade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CPF");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Vendinha.Models.Divida", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("dataPagamento")
                        .HasColumnType("TEXT");

                    b.Property<bool>("pago")
                        .HasColumnType("INTEGER");

                    b.Property<double>("valor")
                        .HasColumnType("REAL");

                    b.HasKey("id");

                    b.HasIndex("CPF");

                    b.ToTable("Dividas");
                });

            modelBuilder.Entity("Vendinha.Models.Divida", b =>
                {
                    b.HasOne("Vendinha.Models.Cliente", "clientes")
                        .WithMany()
                        .HasForeignKey("CPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("clientes");
                });
#pragma warning restore 612, 618
        }
    }
}
