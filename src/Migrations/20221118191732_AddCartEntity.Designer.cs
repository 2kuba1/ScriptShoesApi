﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScriptShoesCQRS.Database;

#nullable disable

namespace ScriptShoesCQRS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221118191732_AddCartEntity")]
    partial class AddCartEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ScriptShoesApi.Entities.EmailCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CodeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CodeExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeneratedCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgName")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainImg")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReviewLikes")
                        .HasColumnType("int");

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ShoesId")
                        .HasColumnType("int");

                    b.Property<string>("Sizes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShoesId");

                    b.ToTable("ShoeSizes");
                });

            modelBuilder.Entity("ScriptShoesApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AvailableFounds")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScriptShoesCQRS.Database.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double?>("AverageRating")
                        .HasColumnType("float");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("PreviousPrice")
                        .HasColumnType("real");

                    b.Property<int?>("ReviewsNum")
                        .HasColumnType("int");

                    b.Property<string>("ShoeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizesList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
