using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_test____Dapper___PostgreSQL.DtOs;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;

namespace MinimalApi_test____Dapper___PostgreSQL.Endpoints
{
    [FastEndpoints.HttpDelete("/DeleteToDo"), AllowAnonymous]
    public class DeleteToDosEndpoint : Endpoint<int, string>
    {
        private readonly IToDolister _toDolister;
        public DeleteToDosEndpoint(IToDolister itodolister)
        {
            _toDolister = itodolister;
        }
        public override async Task<string> ExecuteAsync([FromForm] int idToDo, CancellationToken ct)
        {
            var resultDelete = _toDolister.DeleteToDo(idToDo);
            if (resultDelete.IsCompleted)
            {
              return "You Deleted To Does";
            }
            return new Exception().ToString();
        }
    }
}
