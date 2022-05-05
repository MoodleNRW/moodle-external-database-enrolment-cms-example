﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using moodle_external_database_integration.Data;

#nullable disable

namespace moodle_external_database_integration.Data.Migrations
{
    [DbContext(typeof(MoodleExternalDatabaseIntegrationDbContext))]
    partial class MoodleExternalDatabaseIntegrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id");

                    b.ToTable("external_transfer_courses", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferCourseUser", b =>
                {
                    b.Property<Guid>("ExternalTransferCourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_course_id");

                    b.Property<Guid>("ExternalTransferUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_user_id");

                    b.HasKey("ExternalTransferCourseId", "ExternalTransferUserId");

                    b.HasIndex("ExternalTransferUserId");

                    b.ToTable("external_transfer_courses_users", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("e_mail");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.ToTable("external_transfer_users", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("ExternalTransferCourseId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_course_id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasColumnName("id_number")
                        .HasComputedColumnSql("CAST(id AS text)", true);

                    b.Property<int?>("MoodleId")
                        .HasColumnType("integer")
                        .HasColumnName("moodle_id");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id");

                    b.HasIndex("ExternalTransferCourseId")
                        .IsUnique();

                    b.ToTable("moodle_courses", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleEnrolment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CourseIdNumber")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasColumnName("course_id_number")
                        .HasComputedColumnSql("CAST(moodle_course_id AS text)", true);

                    b.Property<Guid?>("ExternalTransferCourseId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_course_id");

                    b.Property<Guid?>("ExternalTransferUserId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_user_id");

                    b.Property<Guid>("MoodleCourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("moodle_course_id");

                    b.Property<Guid>("MoodleUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("moodle_user_id");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<string>("UserIdNumber")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasColumnName("user_id_number")
                        .HasComputedColumnSql("CAST(moodle_user_id AS text)", true);

                    b.HasKey("Id");

                    b.HasIndex("ExternalTransferCourseId");

                    b.HasIndex("ExternalTransferUserId");

                    b.HasIndex("MoodleCourseId");

                    b.HasIndex("MoodleUserId");

                    b.ToTable("moodle_enrolments", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("e_mail");

                    b.Property<Guid?>("ExternalTransferUserId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("external_transfer_user_id");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasColumnName("id_number")
                        .HasComputedColumnSql("CAST(id AS text)", true);

                    b.Property<int?>("MoodleId")
                        .HasColumnType("integer")
                        .HasColumnName("moodle_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("ExternalTransferUserId")
                        .IsUnique();

                    b.ToTable("moodle_users", (string)null);
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferCourseUser", b =>
                {
                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferCourse", "ExternalTransferCourse")
                        .WithMany("CourseUsers")
                        .HasForeignKey("ExternalTransferCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferUser", "ExternalTransferUser")
                        .WithMany("UserCourses")
                        .HasForeignKey("ExternalTransferUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalTransferCourse");

                    b.Navigation("ExternalTransferUser");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleCourse", b =>
                {
                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferCourse", "ExternalTransferCourse")
                        .WithOne("MoodleCourse")
                        .HasForeignKey("moodle_external_database_integration.Core.Models.Moodle.MoodleCourse", "ExternalTransferCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalTransferCourse");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleEnrolment", b =>
                {
                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferCourse", "ExternalTransferCourse")
                        .WithMany("MoodleEnrolments")
                        .HasForeignKey("ExternalTransferCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferUser", "ExternalTransferUser")
                        .WithMany("MoodleEnrolments")
                        .HasForeignKey("ExternalTransferUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("moodle_external_database_integration.Core.Models.Moodle.MoodleCourse", "MoodleCourse")
                        .WithMany("MoodleEnrolments")
                        .HasForeignKey("MoodleCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("moodle_external_database_integration.Core.Models.Moodle.MoodleUser", "MoodleUser")
                        .WithMany("MoodleEnrolments")
                        .HasForeignKey("MoodleUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalTransferCourse");

                    b.Navigation("ExternalTransferUser");

                    b.Navigation("MoodleCourse");

                    b.Navigation("MoodleUser");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleUser", b =>
                {
                    b.HasOne("moodle_external_database_integration.Core.Models.External.ExternalTransferUser", "ExternalTransferUser")
                        .WithOne("MoodleUser")
                        .HasForeignKey("moodle_external_database_integration.Core.Models.Moodle.MoodleUser", "ExternalTransferUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalTransferUser");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferCourse", b =>
                {
                    b.Navigation("CourseUsers");

                    b.Navigation("MoodleCourse")
                        .IsRequired();

                    b.Navigation("MoodleEnrolments");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.External.ExternalTransferUser", b =>
                {
                    b.Navigation("MoodleEnrolments");

                    b.Navigation("MoodleUser")
                        .IsRequired();

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleCourse", b =>
                {
                    b.Navigation("MoodleEnrolments");
                });

            modelBuilder.Entity("moodle_external_database_integration.Core.Models.Moodle.MoodleUser", b =>
                {
                    b.Navigation("MoodleEnrolments");
                });
#pragma warning restore 612, 618
        }
    }
}