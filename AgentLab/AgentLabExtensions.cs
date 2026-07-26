using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AgentLab;

public static class AgentLabExtensions
{
    public static IEndpointRouteBuilder MapAgentLabChat(
        this IEndpointRouteBuilder endpoints,
        Func<ChatRequest, CancellationToken, Task<ChatResponse>> handler,
        Action<AgentLabOptions>? configure = null)
    {
        var options = new AgentLabOptions();
        configure?.Invoke(options);

        endpoints.MapGet(options.UIRoute, async (HttpContext context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            var html = AgentLabHtml.Generate(options);
            await context.Response.WriteAsync(html);
        });

        endpoints.MapPost(options.ApiRoute, async (ChatRequest request, CancellationToken ct) =>
        {
            var response = await handler(request, ct);
            return Results.Ok(response);
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder MapAgentLabChat(
        this IEndpointRouteBuilder endpoints,
        Func<ChatRequest, Task<ChatResponse>> handler,
        Action<AgentLabOptions>? configure = null)
    {
        return endpoints.MapAgentLabChat(
            (request, ct) => handler(request),
            configure
        );
    }
}
