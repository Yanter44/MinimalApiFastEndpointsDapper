using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;

namespace MinimalApi_test____Dapper___PostgreSQL.Endpoints
{
    [Produces("application/json")]
    [FastEndpoints.HttpGet("/GetTimeNow"), AllowAnonymous]
    public class GetTimeNowEndpoint :EndpointWithoutRequest
    {
        private readonly IDateTime _datetime;
        public GetTimeNowEndpoint(IDateTime datetime)
        {
            _datetime = datetime;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var resulttime = await _datetime.GetTimeNow();
            var formattedDateTime = resulttime.ToString("yyyy-MM-dd HH:mm");
            await SendAsync(formattedDateTime, cancellation: ct);
        }
    }
}
