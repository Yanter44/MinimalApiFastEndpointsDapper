using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using MinimalApi_test____Dapper___PostgreSQL.Models;

namespace MinimalApi_test____Dapper___PostgreSQL.Endpoints
{
   
    [FastEndpoints.HttpPost("/AddToDo"), AllowAnonymous]

    public class AddToDosEndpoint : Endpoint<ToDoModelDtos , string>
    {
        private readonly IToDolister _toDolister;
        public AddToDosEndpoint(IToDolister itodolister)
        {
            _toDolister = itodolister;
        }
        public override async Task<string> ExecuteAsync([FromForm] ToDoModelDtos todomodel, CancellationToken ct)
        {
            var listToDoes = await _toDolister.AddToDo(todomodel);
            return "You Added new To Does";
        }

    }
}
