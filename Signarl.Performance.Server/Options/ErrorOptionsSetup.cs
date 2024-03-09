using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Signarl.Performance.Server.Options;

internal sealed class ErrorOptionsSetup : IConfigureOptions<ErrorOptions>
{
    public void Configure(ErrorOptions options)
    {
        options.ResponseBuilder = (failures, ctx, statusCode) =>
        {
            return new ValidationProblemDetails(
                failures.GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        keySelector: e => e.Key,
                        elementSelector: e => e.Select(m => m.ErrorMessage).ToArray()))
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = statusCode,
                Instance = ctx.Request.Path,
                Extensions = { { "traceId", ctx.TraceIdentifier } }
            };
        };
    }
}