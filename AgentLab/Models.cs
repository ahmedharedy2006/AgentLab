namespace AgentLab;

public sealed record ChatRequest(
    string Message,
    string? ConversationId = null
);

public sealed record ChatResponse(
    string Response,
    string? ConversationId = null
);
