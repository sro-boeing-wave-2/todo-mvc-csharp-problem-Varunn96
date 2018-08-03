﻿// <auto-generated />
using System;
using Google_Keep_ToDo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Google_Keep_ToDo.Migrations
{
    [DbContext(typeof(Google_Keep_ToDoContext))]
    [Migration("20180803072810_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Google_Keep_ToDo.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CheckListName");

                    b.Property<bool>("CheckListStatus");

                    b.Property<int?>("MyNoteId");

                    b.HasKey("Id");

                    b.HasIndex("MyNoteId");

                    b.ToTable("CheckList");
                });

            modelBuilder.Entity("Google_Keep_ToDo.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LabelName");

                    b.Property<int?>("MyNoteId");

                    b.HasKey("Id");

                    b.HasIndex("MyNoteId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("Google_Keep_ToDo.Models.MyNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<bool>("PinStatus");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("MyNote");
                });

            modelBuilder.Entity("Google_Keep_ToDo.Models.CheckList", b =>
                {
                    b.HasOne("Google_Keep_ToDo.Models.MyNote")
                        .WithMany("CheckLists")
                        .HasForeignKey("MyNoteId");
                });

            modelBuilder.Entity("Google_Keep_ToDo.Models.Label", b =>
                {
                    b.HasOne("Google_Keep_ToDo.Models.MyNote")
                        .WithMany("Labels")
                        .HasForeignKey("MyNoteId");
                });
#pragma warning restore 612, 618
        }
    }
}
