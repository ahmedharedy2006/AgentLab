using AgentLab;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAgentLabChat(
    handler: async (ChatRequest request) =>
    {
        await Task.Delay(500);
        return new ChatResponse(
            $"You said: {request.Message}\n\nConversation: {request.ConversationId ?? "new"}",
            request.ConversationId
        );
    },
    configure: options =>
    {
        options.Title = "My Agent";
        options.UIRoute = "/chat";
        options.ApiRoute = "/chat/api";
    }
);

app.Run();
