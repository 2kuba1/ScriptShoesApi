﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScriptShoesAPI.Database;
using ScriptShoesCQRS.Database;

#nullable disable

namespace ScriptShoesAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230101014820_MySQL")]
    partial class MySQL
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ScriptShoesApi.Entities.EmailCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CodeCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CodeExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GeneratedCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EmailCodes");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgName")
                        .HasColumnType("longtext");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.MainImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasColumnType("longtext");

                    b.Property<string>("MainImg")
                        .HasColumnType("longtext");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.ToTable("MainImages");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ReviewLikes")
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.ShoeSizes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<string>("Sizes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.ToTable("ShoeSizes");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AvailableFounds")
                        .HasColumnType("double");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScriptShoesCQRS.Database.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("ScriptShoesCQRS.Database.Entities.ReviewsLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReviewsLikes");
                });

            modelBuilder.Entity("ScriptShoesCQRS.Database.Entities.Shoes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("AverageRating")
                        .HasColumnType("double");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float?>("PreviousPrice")
                        .HasColumnType("float");

                    b.Property<int?>("ReviewsNum")
                        .HasColumnType("int");

                    b.Property<string>("ShoeType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SizesList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Favorites", b =>
                {
                    b.HasOne("ScriptShoesCQRS.Database.Entities.Shoes", null)
                        .WithMany("Favorites")
                        .HasForeignKey("ShoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Images", b =>
                {
                    b.HasOne("ScriptShoesCQRS.Database.Entities.Shoes", null)
                        .WithMany("Images")
                        .HasForeignKey("ShoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.MainImages", b =>
                {
                    b.HasOne("ScriptShoesCQRS.Database.Entities.Shoes", null)
                        .WithMany("MainImages")
                        .HasForeignKey("ShoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.Reviews", b =>
                {
                    b.HasOne("ScriptShoesCQRS.Database.Entities.Shoes", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ShoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScriptShoesApi.Entities.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.ShoeSizes", b =>
                {
                    b.HasOne("ScriptShoesCQRS.Database.Entities.Shoes", null)
                        .WithMany("Sizes")
                        .HasForeignKey("ShoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.User", b =>
                {
                    b.HasOne("ScriptShoesApi.Entities.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ScriptShoesCQRS.Database.Entities.Shoes", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Images");

                    b.Navigation("MainImages");

                    b.Navigation("Reviews");

                    b.Navigation("Sizes");
                });
#pragma warning restore 612, 618
        }
    }
}
