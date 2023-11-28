﻿// <auto-generated />
using System;
using Kiran_FitnessGym_Club.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kiran_FitnessGym_Club.Migrations
{
    [DbContext(typeof(Kiran_FitnessGym_clubContext))]
    [Migration("20231127173802_narendra_fittness_clb")]
    partial class narendra_fittness_clb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.ExerciseType", b =>
                {
                    b.Property<int>("ExercciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExercciseId"), 1L, 1);

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExercciseId");

                    b.ToTable("ExerciseType");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("TrainerId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("MemberId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.FeeDetails", b =>
                {
                    b.Property<int>("FeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeeId"), 1L, 1);

                    b.Property<int?>("AmountPaid")
                        .HasColumnType("int");

                    b.Property<int?>("FeeDue")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Subscription")
                        .HasColumnType("int");

                    b.Property<int?>("TotalFees")
                        .HasColumnType("int");

                    b.HasKey("FeeId");

                    b.HasIndex("MemberId");

                    b.ToTable("FeeDetails");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.MemberRegt", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfJoin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("TrainerId");

                    b.ToTable("MemberRegt");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("FromTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ToTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.TrainerRegt", b =>
                {
                    b.Property<int>("TrainerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrainerId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Salary")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int?>("TrainingFees")
                        .HasColumnType("int");

                    b.HasKey("TrainerId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("TrainerRegt");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.Feedback", b =>
                {
                    b.HasOne("Kiran_FitnessGym_Club.Models.MemberRegt", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kiran_FitnessGym_Club.Models.TrainerRegt", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.FeeDetails", b =>
                {
                    b.HasOne("Kiran_FitnessGym_Club.Models.MemberRegt", "Member")
                        .WithMany("FeeDetails")
                        .HasForeignKey("MemberId");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.MemberRegt", b =>
                {
                    b.HasOne("Kiran_FitnessGym_Club.Models.TrainerRegt", "Trainer")
                        .WithMany("MemberRegt")
                        .HasForeignKey("TrainerId");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.TrainerRegt", b =>
                {
                    b.HasOne("Kiran_FitnessGym_Club.Models.Schedule", "Schedule")
                        .WithMany("TrainerRegt")
                        .HasForeignKey("ScheduleId");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.MemberRegt", b =>
                {
                    b.Navigation("FeeDetails");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.Schedule", b =>
                {
                    b.Navigation("TrainerRegt");
                });

            modelBuilder.Entity("Kiran_FitnessGym_Club.Models.TrainerRegt", b =>
                {
                    b.Navigation("MemberRegt");
                });
#pragma warning restore 612, 618
        }
    }
}
