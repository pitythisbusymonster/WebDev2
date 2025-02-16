﻿// <auto-generated />
using System;
using AmberTurnerSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmberTurnerSite.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20210107221003_Validation")]
    partial class Validation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("AmberTurnerSite.Models.Forum", b =>
                {
                    b.Property<int>("ForumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PageRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostCreatorUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ForumID");

                    b.HasIndex("PostCreatorUserID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AmberTurnerSite.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AmberTurnerSite.Models.Forum", b =>
                {
                    b.HasOne("AmberTurnerSite.Models.User", "PostCreator")
                        .WithMany()
                        .HasForeignKey("PostCreatorUserID");

                    //b.Navigation("PostCreator");
                });
#pragma warning restore 612, 618
        }
    }
}
