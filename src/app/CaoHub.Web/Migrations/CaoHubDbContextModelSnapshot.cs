﻿// <auto-generated />
using System;
using CaoHub.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CaoHub.Web.Migrations
{
    [DbContext(typeof(CaoHubDbContext))]
    partial class CaoHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("PaidByPersonId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaidByPersonId");

                    b.HasIndex("StoreId");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<decimal?>("UnitDiscount")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptItem");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItemPerson", b =>
                {
                    b.Property<int>("ReceiptItemId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("ReceiptItemId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("ReceiptItemPerson");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItemTax", b =>
                {
                    b.Property<int>("ReceiptItemId")
                        .HasColumnType("int");

                    b.Property<int>("TaxId")
                        .HasColumnType("int");

                    b.HasKey("ReceiptItemId", "TaxId");

                    b.HasIndex("TaxId");

                    b.ToTable("ReceiptItemTax");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StoreCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreCategoryId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.StoreCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("StoreCategory");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Rate")
                        .HasPrecision(6, 5)
                        .HasColumnType("decimal(6,5)");

                    b.HasKey("Id");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Receipt", b =>
                {
                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Person", "PaidByPerson")
                        .WithMany("Receipts")
                        .HasForeignKey("PaidByPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Store", "Store")
                        .WithMany("Receipts")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaidByPerson");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItem", b =>
                {
                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Product", "Product")
                        .WithMany("ReceiptItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Receipt", "Receipt")
                        .WithMany("ReceiptItems")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItemPerson", b =>
                {
                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Person", "Person")
                        .WithMany("ReceiptItemsPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItem", "ReceiptItem")
                        .WithMany("ReceiptItemsPeople")
                        .HasForeignKey("ReceiptItemId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("ReceiptItem");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItemTax", b =>
                {
                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItem", "ReceiptItem")
                        .WithMany("ReceiptItemsTaxes")
                        .HasForeignKey("ReceiptItemId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.Tax", "Tax")
                        .WithMany("ReceiptItemsTaxes")
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ReceiptItem");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Store", b =>
                {
                    b.HasOne("CaoHub.Web.Areas.ReceiptManagement.Models.StoreCategory", "StoreCategory")
                        .WithMany("Stores")
                        .HasForeignKey("StoreCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StoreCategory");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Person", b =>
                {
                    b.Navigation("ReceiptItemsPeople");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Product", b =>
                {
                    b.Navigation("ReceiptItems");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Receipt", b =>
                {
                    b.Navigation("ReceiptItems");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.ReceiptItem", b =>
                {
                    b.Navigation("ReceiptItemsPeople");

                    b.Navigation("ReceiptItemsTaxes");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Store", b =>
                {
                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.StoreCategory", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("CaoHub.Web.Areas.ReceiptManagement.Models.Tax", b =>
                {
                    b.Navigation("ReceiptItemsTaxes");
                });
#pragma warning restore 612, 618
        }
    }
}
