﻿// <auto-generated />
using System;
using JWTdemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWTdemo.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JWTdemo.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("8d8c0797-1bd6-4172-855d-a2d4f77bea92"),
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("43fd74d1-a081-4f47-947c-8c91f5240c5b"),
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("JWTdemo.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b61785f1-f2fc-4a90-b4f2-fd072af17979"),
                            PasswordHash = "$2a$11$OlTkuT80jopYCPO6JnS7AujHndCxMps9IYsw0CX8viqtC60cVdpLq",
                            RoleId = new Guid("8d8c0797-1bd6-4172-855d-a2d4f77bea92"),
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("20afd23c-985f-4e65-b5a5-d0e6d1506fd2"),
                            PasswordHash = "$2a$11$jzMUFmWyiEfZsagE9YBfgOIVgo65LcaUCskZucAaBCTPjIcOYKpf6",
                            RoleId = new Guid("43fd74d1-a081-4f47-947c-8c91f5240c5b"),
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("JWTdemo.Models.User", b =>
                {
                    b.HasOne("JWTdemo.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("JWTdemo.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
