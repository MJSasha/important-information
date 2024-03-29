﻿// <auto-generated />
using System;
using ImpInfApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImpInfApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220923104306_UserAddPhone")]
    partial class UserAddPhone
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("DayLessonsAndTimes", b =>
                {
                    b.Property<int>("DaysId")
                        .HasColumnType("int");

                    b.Property<int>("LessonsAndTimesId")
                        .HasColumnType("int");

                    b.HasKey("DaysId", "LessonsAndTimesId");

                    b.HasIndex("LessonsAndTimesId");

                    b.ToTable("DayLessonsAndTimes");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CurrentUserNote")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Information")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Information")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Teacher")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.LessonsAndTimes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonsAndTimes");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeOfCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<bool>("NeedToSend")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Pictures")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("PasswordId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PasswordId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DayLessonsAndTimes", b =>
                {
                    b.HasOne("ImpInfCommon.Data.Models.Day", null)
                        .WithMany()
                        .HasForeignKey("DaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImpInfCommon.Data.Models.LessonsAndTimes", null)
                        .WithMany()
                        .HasForeignKey("LessonsAndTimesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.LessonsAndTimes", b =>
                {
                    b.HasOne("ImpInfCommon.Data.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.News", b =>
                {
                    b.HasOne("ImpInfCommon.Data.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Note", b =>
                {
                    b.HasOne("ImpInfCommon.Data.Models.Day", "Day")
                        .WithMany("Notes")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImpInfCommon.Data.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId");

                    b.Navigation("Day");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.User", b =>
                {
                    b.HasOne("ImpInfCommon.Data.Models.Password", "Password")
                        .WithMany()
                        .HasForeignKey("PasswordId");

                    b.Navigation("Password");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.Day", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("ImpInfCommon.Data.Models.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
