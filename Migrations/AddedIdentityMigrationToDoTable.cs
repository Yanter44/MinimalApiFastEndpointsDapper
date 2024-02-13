using FluentMigrator;
using FluentMigrator.SqlServer;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi_test____Dapper___PostgreSQL.Models;

namespace MinimalApi_test____Dapper___PostgreSQL.Migrations
{
    [Migration(202402020002)]
    public class AddedIdentityMigrationToDoTable : Migration
    {     
       public override void Up()
       {
          Create.Table("ToDoList")
                .WithColumn(nameof(ToDoModel.Id)).AsGuid().PrimaryKey().Identity(1, 1)
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
