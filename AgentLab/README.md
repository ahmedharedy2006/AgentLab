# AgentLab

AgentLab is a lightweight, framework-agnostic AI chat playground for ASP.NET Core applications. Drop-in chat interface with markdown rendering, dark/light themes, and conversation history.

## Install

```
dotnet add package AgentLab
```

## Usage

```csharp
using AgentLab;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAgentLabChat(
    handler: async (ChatRequest request) =>
    {
        string response = await ProcessMessage(request.Message);
        return new ChatResponse(response, request.ConversationId);
    },
    configure: options =>
    {
        options.Title = "My Agent";
        options.UIRoute = "/chat";
        options.ApiRoute = "/chat/api";
    }
);

app.Run();
```

Navigate to `/chat` to open the UI.

## Features

- Markdown rendering (via marked.js)
- LaTeX math support (via KaTeX)
- Dark/light theme with persistence
- Conversation sidebar with localStorage persistence
- Collapsible sidebar (logo-only condensed mode)
- Streaming-ready API design

## Options

| Option | Default | Description |
|--------|---------|-------------|
| `Title` | `"AI Chat"` | Page title and header text |
| `UIRoute` | `"/chat"` | Route for the HTML page |
| `ApiRoute` | `"/chat/api"` | POST endpoint for messages |

## License

Licensed under the [MIT License](LICENSE).
