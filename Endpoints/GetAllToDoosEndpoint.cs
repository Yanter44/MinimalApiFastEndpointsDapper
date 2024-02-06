using Azure.Core;
using Azure;
using FastEndpoints;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;

namespace MinimalApi_test____Dapper___PostgreSQL.Endpoints
{
    [HttpGet("/GetAllToDoList"), AllowAnonymous]
    public class GetAllToDoosEndpoint : EndpointWithoutRequest
    {
        private readonly IToDolister _toDolister;

        public GetAllToDoosEndpoint(IToDolister toDolister)
        {
            _toDolister = toDolister;
        }
      

        public override async Task HandleAsync(CancellationToken ct)
        {     
            var listToDoes = await _toDolister.GetAllToDoList(ct);
            await SendAsync(listToDoes, cancellation: ct);          
         
        }
    }
}
