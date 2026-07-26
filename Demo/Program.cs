using AgentLab;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAgentLabChat(
    handler: async (ChatRequest request) =>
    {
        await Task.Delay(300);
        var md = $"""
**You said:** _{request.Message}_

---

### Capabilities

| Feature | Supported |
|---------|-----------|
| Markdown | Yes |
| Tables | Yes |
| **Bold** | Yes |
| `Code` | Yes |
| Lists | Yes |

> This is a blockquote with **nested markdown**.

```csharp
Console.WriteLine("Hello from AgentLab!");
```

- Item A
- Item B
- Item C

1. First
2. Second
3. Third
""";
        return new ChatResponse(md, request.ConversationId);
    },
    configure: options =>
    {
        options.Title = "AgentLab Demo";
        options.UIRoute = "/chat";
        options.ApiRoute = "/chat/api";
    }
);

app.Run();
