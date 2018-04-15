﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SurveyApp.Contex;
using System;

namespace SurveyApp.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20180414073458_QuestionAswerDictId")]
    partial class QuestionAswerDictId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurveyApp.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerDictId");

                    b.Property<int>("QuestionId");

                    b.Property<int>("SurveyId");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("SurveyApp.Models.AnswerDict", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswersDict");
                });

            modelBuilder.Entity("SurveyApp.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerDictId");

                    b.Property<string>("Description");

                    b.Property<int>("SurveyId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SurveyApp.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("SurveyApp.Models.AnswerDict", b =>
                {
                    b.HasOne("SurveyApp.Models.Question")
                        .WithMany("AnswersDict")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("SurveyApp.Models.Question", b =>
                {
                    b.HasOne("SurveyApp.Models.Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}