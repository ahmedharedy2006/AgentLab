using System.Net;

namespace AgentLab;

public static class AgentLabHtml
{
    public static string Generate(AgentLabOptions options)
    {
        var title = WebUtility.HtmlEncode(options.Title);
        var apiRoute = WebUtility.HtmlEncode(options.ApiRoute);

        return $$"""
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>{{title}}</title>
<link rel="preconnect" href="https://fonts.googleapis.com">
<link href="https://fonts.googleapis.com/css2?family=Inter:opsz,wght@14..32,400;14..32,500;14..32,600&display=swap" rel="stylesheet">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.16.11/dist/katex.min.css">
<script src="https://cdn.jsdelivr.net/npm/marked@15.0.7/marked.min.js" defer></script>
<script src="https://cdn.jsdelivr.net/npm/katex@0.16.11/dist/katex.min.js" defer></script>
<script src="https://cdn.jsdelivr.net/npm/katex@0.16.11/dist/contrib/auto-render.min.js" defer></script>
<style>
:root {
    --bg: #ffffff;
    --bg-subtle: #f6f7f9;
    --bg-elevated: #ffffff;
    --text: #0f172a;
    --text-secondary: #475569;
    --text-muted: #94a3b8;
    --border: #e9edf2;
    --border-focus: #94a3b8;
    --bubble-user-bg: #0f172a;
    --bubble-user-text: #ffffff;
    --bubble-assistant-bg: #ffffff;
    --bubble-assistant-border: #e9edf2;
    --sidebar-bg: #f8f9fb;
    --sidebar-border: #e9edf2;
    --sidebar-hover: #eef0f4;
    --sidebar-active: #e2e5ea;
    --composer-bg: #ffffff;
    --composer-border: #e9edf2;
    --header-bg: rgba(255,255,255,.82);
    --header-border: #e9edf2;
    --shadow-sm: 0 1px 2px rgba(0,0,0,.04);
    --shadow-md: 0 2px 8px rgba(0,0,0,.06), 0 1px 2px rgba(0,0,0,.04);
    --shadow-lg: 0 8px 30px rgba(0,0,0,.08);
    --code-bg: #0f172a;
    --code-text: #e2e8f0;
    --accent: #0f172a;
    --accent-hover: #1e293b;
    --scrollbar: #d1d5db;
    --scrollbar-hover: #9ca3af;
}
body.dark {
    --bg: #0a0a0c;
    --bg-subtle: #121216;
    --bg-elevated: #18181c;
    --text: #ededef;
    --text-secondary: #a1a1aa;
    --text-muted: #52525b;
    --border: #27272a;
    --border-focus: #52525b;
    --bubble-user-bg: #27272a;
    --bubble-user-text: #ededef;
    --bubble-assistant-bg: #18181c;
    --bubble-assistant-border: #27272a;
    --sidebar-bg: #0c0c0e;
    --sidebar-border: #27272a;
    --sidebar-hover: #1c1c20;
    --sidebar-active: #26262a;
    --composer-bg: #0a0a0c;
    --composer-border: #27272a;
    --header-bg: rgba(10,10,12,.82);
    --header-border: #27272a;
    --shadow-sm: 0 1px 2px rgba(0,0,0,.2);
    --shadow-md: 0 2px 8px rgba(0,0,0,.3), 0 1px 2px rgba(0,0,0,.2);
    --shadow-lg: 0 8px 30px rgba(0,0,0,.4);
    --code-bg: #121216;
    --code-text: #e2e8f0;
    --accent: #ededef;
    --accent-hover: #d4d4d8;
    --scrollbar: #3f3f46;
    --scrollbar-hover: #52525b;
}
* { box-sizing: border-box; }
body {
    margin: 0;
    font-family: "Inter", system-ui, -apple-system, sans-serif;
    background: var(--bg-subtle);
    color: var(--text);
    font-size: 14.5px;
    line-height: 1.5;
    overflow: hidden;
    height: 100vh;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}
::-webkit-scrollbar { width: 5px; height: 5px; }
::-webkit-scrollbar-track { background: transparent; }
::-webkit-scrollbar-thumb { background: var(--scrollbar); border-radius: 3px; }
::-webkit-scrollbar-thumb:hover { background: var(--scrollbar-hover); }
.app { display: flex; height: 100vh; }
.sidebar {
    width: 278px;
    background: var(--sidebar-bg);
    border-right: 1px solid var(--sidebar-border);
    display: flex;
    flex-direction: column;
    flex-shrink: 0;
    overflow: hidden;
    transition: width .22s cubic-bezier(.4,0,.2,1);
}
.sidebar.condensed { width: 56px; }
.sidebar-header {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 14px;
    flex-shrink: 0;
}
.condensed .sidebar-header {
    justify-content: center;
    padding: 14px 0;
}
.btn-new-chat {
    width: 28px; height: 28px;
    border: none; border-radius: 7px;
    background: transparent;
    color: var(--text-secondary);
    cursor: pointer;
    display: flex; align-items: center; justify-content: center;
    transition: background .15s, color .15s;
    flex-shrink: 0;
}
.btn-new-chat:hover { background: var(--sidebar-hover); color: var(--text); }
.btn-new-chat svg { width: 16px; height: 16px; }
.sidebar-list {
    flex: 1; overflow-y: auto;
    padding: 2px 6px 6px;
}
.condensed .sidebar-list { display: none; }
.condensed .btn-new-chat { display: none; }
.chat-item {
    display: flex; align-items: center;
    padding: 8px;
    border-radius: 7px;
    cursor: pointer;
    margin-bottom: 1px;
    transition: background .12s;
    gap: 8px;
}
.chat-item:hover { background: var(--sidebar-hover); }
.chat-item.active { background: var(--sidebar-active); }
.chat-item-icon {
    width: 7px; height: 7px;
    border-radius: 50%;
    background: var(--text-muted);
    flex-shrink: 0;
    opacity: 0;
    transition: opacity .15s;
}
.chat-item.active .chat-item-icon { opacity: 1; background: var(--text); }
.chat-item-title {
    flex: 1; overflow: hidden;
    text-overflow: ellipsis; white-space: nowrap;
    font-size: 13.5px; font-weight: 500;
    color: var(--text);
    transition: opacity .15s;
}
.condensed .chat-item-title { opacity: 0; }
.chat-item-delete {
    width: 22px; height: 22px;
    border: none; border-radius: 5px;
    background: transparent;
    color: var(--text-muted);
    cursor: pointer;
    opacity: 0;
    display: flex; align-items: center; justify-content: center;
    transition: opacity .15s, background .15s, color .15s;
    flex-shrink: 0;
}
.chat-item:hover .chat-item-delete { opacity: 1; }
.chat-item-delete:hover { background: var(--sidebar-hover); color: var(--text); }
.chat-item-delete svg { width: 14px; height: 14px; }
.condensed .chat-item-delete { display: none; }
.condensed .chat-item-icon { display: none; }
.sidebar-brand {
    display: flex;
    align-items: center;
    gap: 8px;
    flex-shrink: 0;
    overflow: hidden;
}
.condensed .sidebar-brand {
    justify-content: center;
}
.sidebar-brand-icon {
    width: 28px; height: 28px;
    border-radius: 8px;
    background: var(--accent);
    color: var(--bg);
    display: flex; align-items: center; justify-content: center;
    flex-shrink: 0;
}
.sidebar-brand-icon svg { width: 16px; height: 16px; }
.sidebar-brand-text {
    font-size: 15px;
    font-weight: 600;
    margin: 0;
    color: var(--text);
    letter-spacing: -.02em;
    white-space: nowrap;
    transition: opacity .15s;
}
.condensed .sidebar-brand-text { opacity: 0; width: 0; overflow: hidden; margin: 0; }
.main {
    flex: 1; display: flex; flex-direction: column;
    min-width: 0;
    position: relative;
}
.header {
    height: 56px;
    display: flex; align-items: center;
    padding: 0 14px;
    background: var(--header-bg);
    backdrop-filter: blur(12px);
    -webkit-backdrop-filter: blur(12px);
    border-bottom: 1px solid var(--header-border);
    flex-shrink: 0; gap: 4px;
    position: sticky; top: 0; z-index: 10;
}
.btn-icon {
    width: 34px; height: 34px;
    border: none; border-radius: 7px;
    background: transparent;
    color: var(--text-secondary);
    cursor: pointer;
    display: flex; align-items: center; justify-content: center;
    transition: background .12s, color .12s;
    flex-shrink: 0;
}
.btn-icon:hover { background: var(--bg-subtle); color: var(--text); }
.btn-icon svg { width: 18px; height: 18px; }
.header-title {
    flex: 1; text-align: center;
    font-size: 14px; font-weight: 500;
    color: var(--text-secondary);
    letter-spacing: -.01em;
}
.chat {
    flex: 1; overflow-y: auto;
    scroll-behavior: smooth;
    padding: 12px 0 4px;
}
@keyframes msgIn {
    from { opacity: 0; transform: translateY(10px) scale(.97); }
    to { opacity: 1; transform: translateY(0) scale(1); }
}
.message {
    display: flex;
    padding: 4px 18px;
    animation: msgIn .22s cubic-bezier(.22,1,.36,1) both;
}
.message.user { justify-content: flex-end; }
.message.assistant { justify-content: flex-start; }
.user-bubble {
    display: inline-block;
    max-width: 72%;
    padding: 10px 18px;
    border-radius: 18px 18px 4px 18px;
    background: var(--bubble-user-bg);
    color: var(--bubble-user-text);
    font-size: 14px;
    line-height: 1.55;
    white-space: pre-wrap;
    word-wrap: break-word;
}
.assistant-bubble {
    display: inline-block;
    max-width: 84%;
    padding: 14px 18px;
    border-radius: 18px 18px 18px 4px;
    background: var(--bubble-assistant-bg);
    border: 1px solid var(--bubble-assistant-border);
    line-height: 1.65;
    white-space: normal;
    word-wrap: break-word;
    box-shadow: var(--shadow-sm);
}
.assistant-bubble p { margin: 0 0 10px; }
.assistant-bubble p:last-child { margin-bottom: 0; }
.assistant-bubble h1, .assistant-bubble h2, .assistant-bubble h3, .assistant-bubble h4 {
    margin: 16px 0 8px; line-height: 1.35;
    letter-spacing: -.01em;
}
.assistant-bubble h1 { font-size: 19px; }
.assistant-bubble h2 { font-size: 17px; }
.assistant-bubble h3 { font-size: 15px; }
.assistant-bubble ul, .assistant-bubble ol { margin: 6px 0; padding-left: 22px; }
.assistant-bubble li { margin: 3px 0; }
.assistant-bubble code {
    font-family: "JetBrains Mono", "SF Mono", "Fira Code", ui-monospace, monospace;
    font-size: 12.5px;
    background: var(--bg-subtle);
    padding: 2px 6px;
    border-radius: 4px;
}
.assistant-bubble pre {
    margin: 10px 0;
    background: var(--code-bg);
    color: var(--code-text);
    border-radius: 10px;
    padding: 14px 16px;
    overflow-x: auto;
}
.assistant-bubble pre code {
    background: none; padding: 0;
    font-size: 12.5px; color: inherit;
}
.assistant-bubble table {
    border-collapse: collapse; width: 100%; margin: 10px 0;
}
.assistant-bubble th, .assistant-bubble td {
    border: 1px solid var(--border);
    padding: 6px 10px; text-align: left;
}
.assistant-bubble th { background: var(--bg-subtle); font-weight: 600; }
.assistant-bubble blockquote {
    margin: 10px 0; padding: 2px 14px;
    border-left: 3px solid var(--border);
    color: var(--text-secondary);
}
.assistant-bubble hr {
    margin: 12px 0; border: none;
    border-top: 1px solid var(--border);
}
.thinking-bubble {
    display: inline-flex;
    align-items: center;
    gap: 5px;
    padding: 16px 22px;
    border-radius: 18px 18px 18px 4px;
    background: var(--bubble-assistant-bg);
    border: 1px solid var(--bubble-assistant-border);
    box-shadow: var(--shadow-sm);
}
.dot {
    width: 7px; height: 7px;
    border-radius: 50%;
    background: var(--text-muted);
    animation: dotB 1.2s infinite;
}
.dot:nth-child(2) { animation-delay: .16s; }
.dot:nth-child(3) { animation-delay: .32s; }
@keyframes dotB {
    0%, 60%, 100% { transform: translateY(0); opacity: .3; }
    30% { transform: translateY(-7px); opacity: .8; }
}
.composer {
    padding: 12px 18px 18px;
    background: var(--composer-bg);
    border-top: 1px solid var(--composer-border);
    flex-shrink: 0;
}
.composer-inner {
    max-width: 900px;
    margin: 0 auto;
    display: flex;
    gap: 10px;
    align-items: flex-end;
}
textarea {
    flex: 1;
    resize: none;
    min-height: 46px;
    max-height: 180px;
    padding: 12px 16px;
    border: 1px solid var(--border);
    border-radius: 14px;
    font-size: 14px;
    font-family: inherit;
    background: var(--bg);
    color: var(--text);
    outline: none;
    transition: border-color .18s, box-shadow .18s;
    line-height: 1.5;
}
textarea:focus {
    border-color: var(--border-focus);
    box-shadow: 0 0 0 3px rgba(148,163,184,.15);
}
textarea::placeholder { color: var(--text-muted); }
.btn-send {
    width: 46px; height: 46px;
    border: none; border-radius: 14px;
    background: var(--text);
    color: var(--bg);
    cursor: pointer;
    display: flex; align-items: center; justify-content: center;
    flex-shrink: 0;
    transition: opacity .15s, transform .1s, background .15s;
}
.btn-send:hover { background: var(--accent-hover); }
.btn-send:active { transform: scale(.9); }
.btn-send:disabled { opacity: .2; cursor: not-allowed; transform: none; }
.btn-send svg { width: 18px; height: 18px; }
.empty-state {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100%;
    color: var(--text-muted);
    gap: 12px;
    user-select: none;
    padding: 32px;
}
.empty-state svg {
    width: 40px; height: 40px;
    stroke: var(--text-muted);
    opacity: .35;
}
.empty-state p { margin: 0; font-size: 14px; font-weight: 450; }
</style>
</head>
<body>

<div class="app">
    <div class="sidebar" id="sidebar">
        <div class="sidebar-header">
            <div class="sidebar-brand">
                <div class="sidebar-brand-icon">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="18" height="14" rx="3"/><circle cx="8.5" cy="10" r="1.5" fill="currentColor"/><circle cx="15.5" cy="10" r="1.5" fill="currentColor"/><path d="M8 17v3M16 17v3M10 17l-1 3M14 17l1 3"/></svg>
                </div>
                <span class="sidebar-brand-text">AgentLab</span>
            </div>
            <button class="btn-new-chat" id="new-chat" title="New chat">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
            </button>
        </div>
        <div class="sidebar-list" id="chat-list"></div>
    </div>

    <div class="main">
        <header class="header">
            <button class="btn-icon" id="sidebar-toggle" title="Toggle sidebar">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"><line x1="4" y1="6" x2="20" y2="6"/><line x1="4" y1="12" x2="20" y2="12"/><line x1="4" y1="18" x2="20" y2="18"/></svg>
            </button>
            <span class="header-title">{{title}}</span>
            <button class="btn-icon" id="theme-toggle" title="Toggle theme">
                <svg id="theme-icon-sun" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" style="display:none">
                    <circle cx="12" cy="12" r="5"/><path d="M12 1v2M12 21v2M4.22 4.22l1.42 1.42M18.36 18.36l1.42 1.42M1 12h2M21 12h2M4.22 19.78l1.42-1.42M18.36 5.64l1.42-1.42"/>
                </svg>
                <svg id="theme-icon-moon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round">
                    <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z"/>
                </svg>
            </button>
        </header>

        <main id="chat" class="chat">
            <div class="empty-state" id="empty-state">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.3" stroke-linecap="round" stroke-linejoin="round">
                    <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"/>
                </svg>
                <p>Start a conversation</p>
            </div>
        </main>

        <div class="composer">
            <div class="composer-inner">
                <textarea id="message" placeholder="Ask your agent something..." rows="1"></textarea>
                <button class="btn-send" id="send" title="Send">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <line x1="12" y1="19" x2="12" y2="5"/><polyline points="5 12 12 5 19 12"/>
                    </svg>
                </button>
            </div>
        </div>
    </div>
</div>

<script>
const API_ROUTE = "{{apiRoute}}";
const chat = document.getElementById("chat");
const messageInput = document.getElementById("message");
const sendButton = document.getElementById("send");
const sidebar = document.getElementById("sidebar");
const sidebarToggle = document.getElementById("sidebar-toggle");
const chatList = document.getElementById("chat-list");
const newChatBtn = document.getElementById("new-chat");
const themeToggle = document.getElementById("theme-toggle");
const iconSun = document.getElementById("theme-icon-sun");
const iconMoon = document.getElementById("theme-icon-moon");
const emptyState = document.getElementById("empty-state");

let thinkingEl = null;

function getTheme() { return localStorage.getItem("agentlab-theme") || "dark"; }
function setTheme(t) {
    localStorage.setItem("agentlab-theme", t);
    document.body.classList.toggle("dark", t === "dark");
    iconSun.style.display = t === "dark" ? "" : "none";
    iconMoon.style.display = t === "dark" ? "none" : "";
}
setTheme(getTheme());
themeToggle.addEventListener("click", () => setTheme(getTheme() === "dark" ? "light" : "dark"));

function toggleSidebar() {
    sidebar.classList.toggle("condensed");
    localStorage.setItem("agentlab-condensed", sidebar.classList.contains("condensed"));
}
if (localStorage.getItem("agentlab-condensed") === "true") sidebar.classList.add("condensed");
sidebarToggle.addEventListener("click", toggleSidebar);

function getChats() { return JSON.parse(localStorage.getItem("agentlab-chats") || "[]"); }
function saveChats(c) { localStorage.setItem("agentlab-chats", JSON.stringify(c)); }
function getActiveId() { return localStorage.getItem("agentlab-active"); }
function setActiveId(id) { localStorage.setItem("agentlab-active", id); }

function createChat() {
    const id = crypto.randomUUID();
    const c = { id, title: "New Chat", messages: [], createdAt: Date.now() };
    const all = getChats(); all.unshift(c); saveChats(all);
    setActiveId(id); return c;
}
function deleteChat(id) {
    let all = getChats(); all = all.filter(x => x.id !== id); saveChats(all);
    if (getActiveId() === id) { const n = all[0] || null; setActiveId(n ? n.id : null); }
}

function esc(s) { const d = document.createElement("div"); d.textContent = s; return d.innerHTML; }

function renderMath(text) {
    const el = document.createElement("div"); el.textContent = text;
    if (typeof renderMathInElement === "function") {
        try { renderMathInElement(el, { delimiters: [{left:"$$",right:"$$",display:true},{left:"$",right:"$",display:false}], throwOnError: false }); } catch (_) {}
    }
    return el.innerHTML;
}

function renderContent(text) {
    const map = []; let i = 0;
    const p = text
        .replace(/\$\$([\s\S]*?)\$\$/g, (_, m) => { const k = `\x00MA${i}\x00`; map.push({k,m,d:true}); i++; return k; })
        .replace(/(?<!\$)\$(\S[^$]*?\S)\$(?!\$)/g, (_, m) => { const k = `\x00MB${i}\x00`; map.push({k,m,d:false}); i++; return k; });
    let html;
    if (typeof marked !== "undefined" && marked.parse) html = marked.parse(p, { breaks: true, gfm: true });
    else html = "<p>" + esc(text.replace(/\n/g, "<br>")) + "</p>";
    for (const {k,m,d} of map) html = html.replace(k, renderMath(d ? "$$"+m+"$$" : "$"+m+"$"));
    return html;
}

function addMessage(role, content, save) {
    emptyState.style.display = "none";
    const w = document.createElement("div"); w.className = "message " + role;
    const b = document.createElement("div"); b.className = role === "user" ? "user-bubble" : "assistant-bubble";
    if (role === "user") b.textContent = content;
    else b.innerHTML = renderContent(content);
    w.appendChild(b); chat.appendChild(w);
    chat.scrollTop = chat.scrollHeight;

    if (save !== false) {
        const all = getChats(); const c = all.find(x => x.id === getActiveId());
        if (c) {
            c.messages.push({ role, content });
            if (role === "user" && c.messages.filter(m => m.role === "user").length === 1)
                c.title = content.length > 46 ? content.slice(0, 46) + "..." : content;
            saveChats(all); renderSidebar();
        }
    }
}

function showThinking() { hideThinking();
    emptyState.style.display = "none";
    const w = document.createElement("div"); w.className = "message assistant"; w.id = "thinking-msg";
    const b = document.createElement("div"); b.className = "thinking-bubble";
    b.innerHTML = "<span class='dot'></span><span class='dot'></span><span class='dot'></span>";
    w.appendChild(b); chat.appendChild(w); chat.scrollTop = chat.scrollHeight;
    thinkingEl = w;
}
function hideThinking() { if (thinkingEl) { thinkingEl.remove(); thinkingEl = null; } }

function loadChat(id) {
    chat.querySelectorAll(".message").forEach(el => el.remove()); hideThinking();
    const all = getChats(); const c = all.find(x => x.id === id);
    if (c) for (const m of c.messages) addMessage(m.role, m.content, false);
    emptyState.style.display = (!c || c.messages.length === 0) ? "flex" : "none";
    renderSidebar();
}

function renderSidebar() {
    const all = getChats(); const active = getActiveId();
    chatList.innerHTML = all.map(c =>
        `<div class="chat-item${c.id===active?" active":""}" data-id="${esc(c.id)}">
            <span class="chat-item-icon"></span>
            <span class="chat-item-title">${esc(c.title)}</span>
            <button class="chat-item-delete" data-id="${esc(c.id)}">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
        </div>`
    ).join("");
    chatList.querySelectorAll(".chat-item").forEach(el => {
        el.addEventListener("click", e => {
            if (e.target.closest(".chat-item-delete")) return;
            setActiveId(el.dataset.id); loadChat(el.dataset.id);
        });
    });
    chatList.querySelectorAll(".chat-item-delete").forEach(btn => {
        btn.addEventListener("click", e => { e.stopPropagation();
            deleteChat(btn.dataset.id);
            const all = getChats(); const a = getActiveId();
            if (a && all.some(x => x.id === a)) loadChat(a);
            else if (all.length > 0) { setActiveId(all[0].id); loadChat(all[0].id); }
            else { const c = createChat(); setActiveId(c.id); loadChat(c.id); }
        });
    });
}

async function sendMessage() {
    const msg = messageInput.value.trim(); if (!msg) return;
    let all = getChats(); let a = all.find(x => x.id === getActiveId());
    if (!a) { a = createChat(); setActiveId(a.id); }
    addMessage("user", msg, true);
    messageInput.value = ""; messageInput.style.height = "auto";
    sendButton.disabled = true; showThinking();
    try {
        const r = await fetch(API_ROUTE, { method:"POST", headers:{"Content-Type":"application/json"}, body:JSON.stringify({message:msg, conversationId:getActiveId()}) });
        if (!r.ok) throw new Error("Request failed: " + r.status);
        const d = await r.json();
        const reply = d.response || d.feedback || "";
        hideThinking(); addMessage("assistant", reply, true);
        if (d.conversationId) setActiveId(d.conversationId);
    } catch (e) { hideThinking(); addMessage("assistant", "**Error:** " + e.message, true); }
    finally { sendButton.disabled = false; messageInput.focus(); }
}

function autoResize() {
    messageInput.style.height = "auto";
    messageInput.style.height = Math.min(messageInput.scrollHeight, 180) + "px";
}

function init() {
    let all = getChats(); let a = all.find(x => x.id === getActiveId());
    if (!a) { a = createChat(); setActiveId(a.id); }
    loadChat(a.id); renderSidebar();
}
sendButton.addEventListener("click", sendMessage);
messageInput.addEventListener("keydown", e => { if (e.key === "Enter" && !e.shiftKey) { e.preventDefault(); sendMessage(); } });
messageInput.addEventListener("input", autoResize);
init();
</script>
</body>
</html>
""";
    }
}
