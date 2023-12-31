﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pvo_dictionary_api.Database;

#nullable disable

namespace pvo_dictionary_api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("pvo_dictionary_api.Models.AuditLog", b =>
                {
                    b.Property<int>("audit_log_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("action_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("reference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("screen_info")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("audit_log_id");

                    b.ToTable("audit_logs");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Concept", b =>
                {
                    b.Property<int>("concept_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("dictionary_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("concept_id");

                    b.ToTable("concepts");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.ConceptLink", b =>
                {
                    b.Property<int>("concept_link_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("concept_link_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("concept_link_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_concept_link_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("concept_link_id");

                    b.ToTable("concept_links");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.ConceptRelationship", b =>
                {
                    b.Property<int>("concept_relationship_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("concept_id")
                        .HasColumnType("int");

                    b.Property<int>("concept_link_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("dictionary_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("parent_id")
                        .HasColumnType("int");

                    b.HasKey("concept_relationship_id");

                    b.ToTable("concept_relationships");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Dialect", b =>
                {
                    b.Property<int>("dialect_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("dialect_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("dialect_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_dialect_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("dialect_id");

                    b.ToTable("dialects");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Dictionary", b =>
                {
                    b.Property<int>("dictionary_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("dictionary_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("last_view_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("dictionary_id");

                    b.ToTable("dictionaries");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Example", b =>
                {
                    b.Property<int>("example_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("detail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("detail_html")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("dialect_id")
                        .HasColumnType("int");

                    b.Property<int>("dictionary_id")
                        .HasColumnType("int");

                    b.Property<int>("mode_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("nuance_id")
                        .HasColumnType("int");

                    b.Property<int>("register_id")
                        .HasColumnType("int");

                    b.Property<int>("tone_id")
                        .HasColumnType("int");

                    b.HasKey("example_id");

                    b.ToTable("examples");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.ExampleLink", b =>
                {
                    b.Property<int>("example_link_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("dictionary_id")
                        .HasColumnType("int");

                    b.Property<string>("example_link_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("example_link_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_example_link_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("example_link_id");

                    b.ToTable("example_links");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.ExampleRelationship", b =>
                {
                    b.Property<int>("example_relationship_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("concept_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("dictionary_id")
                        .HasColumnType("int");

                    b.Property<int>("example_id")
                        .HasColumnType("int");

                    b.Property<int>("example_link_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.HasKey("example_relationship_id");

                    b.ToTable("example_relationships");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Mode", b =>
                {
                    b.Property<int>("mode_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("mode_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("mode_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_mode_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("mode_id");

                    b.ToTable("modes");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Nuance", b =>
                {
                    b.Property<int>("nuance_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("nuance_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("nuance_type")
                        .HasColumnType("int");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_nuance_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("nuance_id");

                    b.ToTable("nuance");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Register", b =>
                {
                    b.Property<int>("register_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("register_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("register_type")
                        .HasColumnType("int");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_register_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("register_id");

                    b.ToTable("registers");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.Tone", b =>
                {
                    b.Property<int>("tone_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<int>("sys_tone_id")
                        .HasColumnType("int");

                    b.Property<string>("tone_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("tone_type")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("tone_id");

                    b.ToTable("tones");
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("avatar")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("display_name")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("full_name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("otp")
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("position")
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            user_id = 1,
                            avatar = "",
                            birthday = new DateTime(2001, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            created_date = new DateTime(2023, 6, 14, 2, 40, 46, 111, DateTimeKind.Utc).AddTicks(9610),
                            display_name = "Test",
                            email = "Test@gmail.com",
                            full_name = "Test",
                            modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            password = "Test",
                            position = "",
                            status = 1,
                            user_name = "Test"
                        });
                });

            modelBuilder.Entity("pvo_dictionary_api.Models.UserSetting", b =>
                {
                    b.Property<int>("user_setting_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("setting_key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("setting_value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("user_setting_id");

                    b.ToTable("user_settings");
                });
#pragma warning restore 612, 618
        }
    }
}
