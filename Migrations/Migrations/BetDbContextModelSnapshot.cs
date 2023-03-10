﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repositories.DbContexts;

#nullable disable

namespace Migrations.Migrations
{
    [DbContext(typeof(BetDbContext))]
    partial class BetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.BalanceTransactions.BalanceTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<double>("AmountBefore")
                        .HasColumnType("double precision");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("BalanceTransactions", (string)null);
                });

            modelBuilder.Entity("Entities.BetSelections.BetSelection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BetId")
                        .HasColumnType("integer");

                    b.Property<double>("Odds")
                        .HasColumnType("double precision");

                    b.Property<int>("SelectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.ToTable("BetSelections", (string)null);
                });

            modelBuilder.Entity("Entities.Bets.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("StakeAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Bets", (string)null);
                });

            modelBuilder.Entity("Entities.Players.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("Entities.BalanceTransactions.BalanceTransaction", b =>
                {
                    b.HasOne("Entities.Players.Player", "Player")
                        .WithMany("BalanceTransactions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Entities.BetSelections.BetSelection", b =>
                {
                    b.HasOne("Entities.Bets.Bet", "Bet")
                        .WithMany("BetSelections")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bet");
                });

            modelBuilder.Entity("Entities.Bets.Bet", b =>
                {
                    b.Navigation("BetSelections");
                });

            modelBuilder.Entity("Entities.Players.Player", b =>
                {
                    b.Navigation("BalanceTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
