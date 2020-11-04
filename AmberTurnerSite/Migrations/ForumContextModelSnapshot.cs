﻿// <auto-generated />
using System;
using AmberTurnerSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmberTurnerSite.Migrations
{
    [DbContext(typeof(ForumContext))]
    partial class ForumContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AmberTurnerSite.Models.Forum", b =>
                {
                    b.Property<int>("ForumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageRating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostCreatorUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ForumID");

                    b.HasIndex("PostCreatorUserID");

                    b.ToTable("Forum");
                });

            modelBuilder.Entity("AmberTurnerSite.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AmberTurnerSite.Models.Forum", b =>
                {
                    b.HasOne("AmberTurnerSite.Models.User", "PostCreator")
                        .WithMany()
                        .HasForeignKey("PostCreatorUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
