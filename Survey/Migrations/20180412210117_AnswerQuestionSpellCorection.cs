using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SurveyApp.Migrations
{
    public partial class AnswerQuestionSpellCorection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerDictId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QestionId",
                table: "Answers",
                newName: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Answers",
                newName: "QestionId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerDictId",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
