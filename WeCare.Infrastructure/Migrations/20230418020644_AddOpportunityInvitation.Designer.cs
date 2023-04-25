﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeCare.Infrastructure;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    [DbContext(typeof(WeCareDatabaseContext))]
    [Migration("20230418020644_AddOpportunityInvitation")]
    partial class AddOpportunityInvitation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OpportunityCauseVolunteerOpportunity", b =>
                {
                    b.Property<long>("CausesId")
                        .HasColumnType("bigint")
                        .HasColumnName("causes_id");

                    b.Property<long>("VolunteerOpportunitiesId")
                        .HasColumnType("bigint")
                        .HasColumnName("volunteer_opportunities_id");

                    b.HasKey("CausesId", "VolunteerOpportunitiesId")
                        .HasName("pk_opportunity_cause_volunteer_opportunity");

                    b.HasIndex("VolunteerOpportunitiesId")
                        .HasDatabaseName("ix_opportunity_cause_volunteer_opportunity_volunteer_opportuni");

                    b.ToTable("opportunity_cause_volunteer_opportunity", "public");
                });

            modelBuilder.Entity("WeCare.Domain.Models.ConfirmationToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("token");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_confirmation_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_confirmation_tokens_user_id");

                    b.ToTable("confirmation_tokens", "public");
                });

            modelBuilder.Entity("WeCare.Domain.Models.OpportunityCause", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PrimaryColorCode")
                        .HasColumnType("text")
                        .HasColumnName("primary_color_code");

                    b.Property<string>("SecondaryColorCode")
                        .HasColumnType("text")
                        .HasColumnName("secondary_color_code");

                    b.HasKey("Id")
                        .HasName("pk_opportunity_causes");

                    b.HasIndex("Code")
                        .HasDatabaseName("ix_opportunity_causes_code");

                    b.ToTable("opportunity_causes", "public");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "politics",
                            Name = "Advocacy | Políticas Públicas"
                        },
                        new
                        {
                            Id = 2L,
                            Code = "citizen-participation",
                            Name = "Cidadania"
                        },
                        new
                        {
                            Id = 3L,
                            Code = "fight-against-hunger",
                            Name = "Combate à Fome"
                        },
                        new
                        {
                            Id = 4L,
                            Code = "fight-against-poverty",
                            Name = "Combate a Pobreza"
                        },
                        new
                        {
                            Id = 5L,
                            Code = "conscious-consumption",
                            Name = "Consumo Consciente"
                        },
                        new
                        {
                            Id = 6L,
                            Code = "children-and-youth",
                            Name = "Crianças"
                        },
                        new
                        {
                            Id = 7L,
                            Code = "culture-and-art",
                            Name = "Cultura e Arte"
                        },
                        new
                        {
                            Id = 8L,
                            Code = "community-development",
                            Name = "Desenvolvimento Comunitário"
                        },
                        new
                        {
                            Id = 9L,
                            Code = "human-rights",
                            Name = "Direitos humanos"
                        },
                        new
                        {
                            Id = 10L,
                            Code = "education",
                            Name = "Educação"
                        },
                        new
                        {
                            Id = 11L,
                            Code = "racial-equity",
                            Name = "Equidade Racial"
                        },
                        new
                        {
                            Id = 12L,
                            Code = "sports",
                            Name = "Esportes"
                        },
                        new
                        {
                            Id = 13L,
                            Code = "elderly",
                            Name = "Idosos"
                        },
                        new
                        {
                            Id = 14L,
                            Code = "youth",
                            Name = "Jovens"
                        },
                        new
                        {
                            Id = 15L,
                            Code = "lgbti",
                            Name = "LGBTI+"
                        },
                        new
                        {
                            Id = 16L,
                            Code = "environment",
                            Name = "Meio Ambiente"
                        },
                        new
                        {
                            Id = 17L,
                            Code = "urban-mobility",
                            Name = "Mobilidade Urbana"
                        },
                        new
                        {
                            Id = 18L,
                            Code = "women",
                            Name = "Mulheres"
                        },
                        new
                        {
                            Id = 19L,
                            Code = "disabled-people",
                            Name = "Pessoas com deficiência"
                        },
                        new
                        {
                            Id = 20L,
                            Code = "homeless-population",
                            Name = "População em Situação de Rua"
                        },
                        new
                        {
                            Id = 21L,
                            Code = "indigenous-people",
                            Name = "Povos Indígenas"
                        },
                        new
                        {
                            Id = 22L,
                            Code = "animal-protection",
                            Name = "Proteção Animal"
                        },
                        new
                        {
                            Id = 23L,
                            Code = "refugees",
                            Name = "Refugiados"
                        },
                        new
                        {
                            Id = 24L,
                            Code = "health",
                            Name = "Saúde"
                        },
                        new
                        {
                            Id = 25L,
                            Code = "sustainability",
                            Name = "Sustentabilidade"
                        },
                        new
                        {
                            Id = 26L,
                            Code = "professional-training",
                            Name = "Treinamento profissional"
                        });
                });

            modelBuilder.Entity("WeCare.Domain.Models.OpportunityInvitation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CandidateId")
                        .HasColumnType("bigint")
                        .HasColumnName("candidate_id");

                    b.Property<string>("InvitationMessage")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("invitation_message");

                    b.Property<long>("OpportunityId")
                        .HasColumnType("bigint")
                        .HasColumnName("opportunity_id");

                    b.Property<string>("ResponseMessage")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("response_message");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_opportunity_invitation");

                    b.HasIndex("CandidateId")
                        .HasDatabaseName("ix_opportunity_invitation_candidate_id");

                    b.HasIndex("OpportunityId")
                        .HasDatabaseName("ix_opportunity_invitation_opportunity_id");

                    b.ToTable("opportunity_invitation", "public");
                });

            modelBuilder.Entity("WeCare.Domain.Models.OpportunityRegistration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CandidateId")
                        .HasColumnType("bigint")
                        .HasColumnName("candidate_id");

                    b.Property<string>("FeedbackMessage")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("feedback_message");

                    b.Property<long>("OpportunityId")
                        .HasColumnType("bigint")
                        .HasColumnName("opportunity_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_opportunity_registrations");

                    b.HasIndex("CandidateId")
                        .HasDatabaseName("ix_opportunity_registrations_candidate_id");

                    b.HasIndex("OpportunityId")
                        .HasDatabaseName("ix_opportunity_registrations_opportunity_id");

                    b.ToTable("opportunity_registrations", "public");
                });

            modelBuilder.Entity("WeCare.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("complement");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean")
                        .HasColumnName("enabled");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_update_date");

                    b.Property<string>("LinkedIn")
                        .HasColumnType("text")
                        .HasColumnName("linked_in");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("neighborhood");

                    b.Property<string>("Number")
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea")
                        .HasColumnName("photo");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("postal_code");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("telephone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", "public");

                    b.HasDiscriminator<string>("Type").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WeCare.Domain.Models.VolunteerOpportunity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("complement");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long>("InstitutionId")
                        .HasColumnType("bigint")
                        .HasColumnName("institution_id");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_update_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("neighborhood");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<DateTime>("OpportunityDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("opportunity_date");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("photo");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("postal_code");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("street");

                    b.HasKey("Id")
                        .HasName("pk_volunteer_opportunities");

                    b.HasIndex("InstitutionId")
                        .HasDatabaseName("ix_volunteer_opportunities_institution_id");

                    b.ToTable("volunteer_opportunities", "public");
                });

            modelBuilder.Entity("WeCare.Domain.Models.Candidate", b =>
                {
                    b.HasBaseType("WeCare.Domain.Models.User");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.HasDiscriminator().HasValue("CANDIDATE");
                });

            modelBuilder.Entity("WeCare.Domain.Models.Institution", b =>
                {
                    b.HasBaseType("WeCare.Domain.Models.User");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cnpj");

                    b.HasDiscriminator().HasValue("INSTITUTION");
                });

            modelBuilder.Entity("OpportunityCauseVolunteerOpportunity", b =>
                {
                    b.HasOne("WeCare.Domain.Models.OpportunityCause", null)
                        .WithMany()
                        .HasForeignKey("CausesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_cause_volunteer_opportunity_opportunity_cause_c");

                    b.HasOne("WeCare.Domain.Models.VolunteerOpportunity", null)
                        .WithMany()
                        .HasForeignKey("VolunteerOpportunitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_cause_volunteer_opportunity_volunteer_opportuni");
                });

            modelBuilder.Entity("WeCare.Domain.Models.ConfirmationToken", b =>
                {
                    b.HasOne("WeCare.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_confirmation_tokens_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WeCare.Domain.Models.OpportunityInvitation", b =>
                {
                    b.HasOne("WeCare.Domain.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_invitation_users_candidate_id");

                    b.HasOne("WeCare.Domain.Models.VolunteerOpportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_invitation_volunteer_opportunity_opportunity_id");

                    b.Navigation("Candidate");

                    b.Navigation("Opportunity");
                });

            modelBuilder.Entity("WeCare.Domain.Models.OpportunityRegistration", b =>
                {
                    b.HasOne("WeCare.Domain.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_registrations_users_candidate_id");

                    b.HasOne("WeCare.Domain.Models.VolunteerOpportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_opportunity_registrations_volunteer_opportunity_opportunity");

                    b.Navigation("Candidate");

                    b.Navigation("Opportunity");
                });

            modelBuilder.Entity("WeCare.Domain.Models.VolunteerOpportunity", b =>
                {
                    b.HasOne("WeCare.Domain.Models.Institution", "Institution")
                        .WithMany("VolunteerOpportunities")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_volunteer_opportunities_users_institution_id");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("WeCare.Domain.Models.Institution", b =>
                {
                    b.Navigation("VolunteerOpportunities");
                });
#pragma warning restore 612, 618
        }
    }
}
