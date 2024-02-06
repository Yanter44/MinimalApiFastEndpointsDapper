using FluentMigrator;
using MinimalApi_test____Dapper___PostgreSQL.Models;

namespace MinimalApi_test____Dapper___PostgreSQL.Migrations
{
    [Migration(202402020001)]
    public class CreateToDoTable : Migration
    {
     
        public override void Up()
        {
            Create.Table("ToDoList")
                .WithColumn(nameof(ToDoModel.Id)).AsInt32().PrimaryKey().Identity()
                .WithColumn(nameof(ToDoModel.WhatToDo)).AsString().NotNullable()
                .WithColumn(nameof(ToDoModel.is_it_done)).AsBoolean().NotNullable()
                .WithColumn("DataTimeNow").AsString().NotNullable()
                .WithColumn("MustToCompleteTime").AsString().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("ToDoList");
        }

    }
}
