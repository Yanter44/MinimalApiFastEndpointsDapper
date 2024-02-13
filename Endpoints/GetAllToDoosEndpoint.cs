using Azure.Core;
using Azure;
using FastEndpoints;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Models;
using MinimalApi_test____Dapper___PostgreSQL.Requests;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MinimalApi_test____Dapper___PostgreSQL.Endpoints
{
    [HttpPost("/GetAllToDoList/{pageNumber?}/{pageSize?}"), AllowAnonymous]
    public class GetAllToDoosEndpoint : Endpoint<ToDoListRequestToEnpoint, List<ToDoModel>?>
    {
        private readonly IToDolister _toDolister;

        public GetAllToDoosEndpoint(IToDolister toDolister)
        {
            _toDolister = toDolister;
        }


        public override async Task HandleAsync(ToDoListRequestToEnpoint request, CancellationToken ct)
        {       
            var listToDoes = await _toDolister.GetAllToDoList(request,ct);
            await SendAsync(listToDoes ?? null, cancellation: ct);          
         
        }
       

    }
}
