﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ms_recip.Data;

#nullable disable

namespace ms_recip.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ms_recip.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ms_recip.Models.IngredientModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ms_recip.Models.IngredientQuantityModel", b =>
                {
                    b.Property<Guid>("RecipId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MeasureUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.HasKey("RecipId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientQuantities");
                });

            modelBuilder.Entity("ms_recip.Models.ProfilCategoryModel", b =>
                {
                    b.Property<Guid>("ProfilId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("ProfilId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProfilCategories");
                });

            modelBuilder.Entity("ms_recip.Models.ProfilModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profils");
                });

            modelBuilder.Entity("ms_recip.Models.RecipCalendarModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RecipId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RecipId", "Date");

                    b.HasIndex("RecipId");

                    b.ToTable("RecipCalendars");
                });

            modelBuilder.Entity("ms_recip.Models.RecipCategoryModel", b =>
                {
                    b.Property<Guid>("RecipId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("RecipId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("RecipCategories");
                });

            modelBuilder.Entity("ms_recip.Models.RecipModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Authorname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CookingDuration")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Recips");
                });

            modelBuilder.Entity("ms_recip.Models.RecipStepModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RecipId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipId");

                    b.ToTable("RecipSteps");
                });

            modelBuilder.Entity("ms_recip.Models.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProfilId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("ProfilId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ms_recip.Models.IngredientQuantityModel", b =>
                {
                    b.HasOne("ms_recip.Models.IngredientModel", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ms_recip.Models.RecipModel", "Recip")
                        .WithMany("IngredientQuantities")
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recip");
                });

            modelBuilder.Entity("ms_recip.Models.ProfilCategoryModel", b =>
                {
                    b.HasOne("ms_recip.Models.CategoryModel", "Category")
                        .WithMany("Profils")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ms_recip.Models.ProfilModel", "Profil")
                        .WithMany("Categories")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Profil");
                });

            modelBuilder.Entity("ms_recip.Models.RecipCalendarModel", b =>
                {
                    b.HasOne("ms_recip.Models.RecipModel", "Recip")
                        .WithMany()
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ms_recip.Models.UserModel", "User")
                        .WithMany("RecipCalendars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ms_recip.Models.RecipCategoryModel", b =>
                {
                    b.HasOne("ms_recip.Models.CategoryModel", "Category")
                        .WithMany("Recips")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ms_recip.Models.RecipModel", "Recip")
                        .WithMany("Categories")
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Recip");
                });

            modelBuilder.Entity("ms_recip.Models.RecipStepModel", b =>
                {
                    b.HasOne("ms_recip.Models.RecipModel", "Recip")
                        .WithMany("Steps")
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recip");
                });

            modelBuilder.Entity("ms_recip.Models.UserModel", b =>
                {
                    b.HasOne("ms_recip.Models.ProfilModel", "Profil")
                        .WithMany("Users")
                        .HasForeignKey("ProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profil");
                });

            modelBuilder.Entity("ms_recip.Models.CategoryModel", b =>
                {
                    b.Navigation("Profils");

                    b.Navigation("Recips");
                });

            modelBuilder.Entity("ms_recip.Models.ProfilModel", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ms_recip.Models.RecipModel", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("IngredientQuantities");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("ms_recip.Models.UserModel", b =>
                {
                    b.Navigation("RecipCalendars");
                });
#pragma warning restore 612, 618
        }
    }
}
