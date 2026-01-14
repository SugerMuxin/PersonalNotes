---
author: RuoSaChen
tags:
  - AI
  - teach
cover-image: Pics/claude-code-cover.png
---
![[Pasted image 20260114171137.png]]

# Claude Code 简介与安装教程

> [!info] Claude Code
> Anthropic 官方推出的 AI 编程 CLI 工具，更像是一个真正的 Agent 助手

---

## <span style="color: #9d7ed8;">🤖 1. Claude Code 是什么？</span>

Claude Code 是 Anthropic 公司发布的一款 AI 编程产品。因为 Claude 的模型和工程设计更加优秀，在使用同样的模型时，在 Claude Code 中完成的代码质量，要比在 Cursor 等 AI IDE 工具中完成的质量更高。

> [!tip] Claude Code vs Copilot
> **Claude Code 更像是一个 Agent**
>
> ### What is Agent?
> 从更广义的 AI 背景看，Agent 是一个非常古老的概念。任何能进行自我决策、与环境交互，并试图 **optimize reward**（优化奖励）的系统，都可以被称为 Agent。
>
> 但在今天大语言模型的背景下，我个人理解为：
>
> > Agent 在一个「环境」里，按照我们的要求使用「工具」完成「任务」。他可以写代码，可以执行终端命令，可以访问浏览器等等。同时对我们做过的任务有「记忆」，是一个真正的助手，**不止于 Copilot**。

---

## <span style="color: #9d7ed8;">🌍 2. Claude 的运行环境</span>

> **环境**即是本地文件环境，这其实也是当前最适配 Agent 的环境。

---

## <span style="color: #9d7ed8;">⚙️ 3. Claude 安装部署</span>

### Step 1: 安装 Claude Code

按照金山云官方教程安装：
https://docs.ksyun.com/documents/44928

> [!warning] 注意
> 需要 Node.js 才能安装。安装后在 cmd 中运行一下 `Claude`，这样才会在指定目录下生成 `.claude.json` 文件，才能进行第二步的配置。

### Step 2: 修改本地配置

**配置文件路径：** `C:\Users\你的用户名\.claude\settings.json`

```json
{
    "env": {
        "DISABLE_TELEMETRY": "1",
        "DISABLE_ERROR_REPORTING": "1",
        "CLAUDE_CODE_DISABLE_NONESSENTIAL_TRAFFIC": "1",
        "ANTHROPIC_DEFAULT_HAIKU_MODEL": "glm-4.5-air",
        "ANTHROPIC_DEFAULT_SONNET_MODEL": "glm-4.7",
        "ANTHROPIC_DEFAULT_OPUS_MODEL": "glm-4.7",
        "ANTHROPIC_BASE_URL": "https://open.bigmodel.cn/api/anthropic",
        "ANTHROPIC_AUTH_TOKEN": "你的API密钥",
        "API_TIMEOUT_MS": "3000000",
        "MCP_TOOL_TIMEOUT": "30000"
    },
    "alwaysThinkingEnabled": false
}
```

### Step 3: 配置 MCP 服务器

**配置文件路径：** `C:\Users\你的用户名\.claude.json`

```json
{
  "mcpServers": {
    "zai-mcp-server": {
      "type": "stdio",
      "command": "npx",
      "args": [
        "-y",
        "@z_ai/mcp-server"
      ],
      "env": {
        "Z_AI_API_KEY": "你的智谱API密钥",
        "Z_AI_MODE": "ZHIPU"
      }
    },
    "firecrawl-mcp": {
      "type": "http",
      "url": "http://your-server:25152/mcp",
      "headers": {
        "Authorization": "Bearer 你的Token"
      }
    }
  }
}
```

> [!info] MCP 服务器说明
> - **zai-mcp-server**: 图片/视频分析、OCR、UI 转代码等
> - **firecrawl-mcp**: 网页抓取、搜索等

### Step 4: 跳过登录（可选）

如果要跳过登录，在 `.claude.json` 中添加：

```json
"hasCompletedOnboarding": true
```

---

## <span style="color: #9d7ed8;">🚀 4. 开始使用</span>

到你想使用 Claude 的文件夹下打开 cmd 命令行，这个文件夹就是 Claude 的项目根目录了。

```bash
# 进入项目目录
cd F:\A-Ruosa\PersonalNotes

# 启动 Claude Code
claude
```

---

## <span style="color: #9d7ed8;">📚 常用命令</span>

| 命令 | 说明 |
|------|------|
| `/help` | 查看帮助 |
| `/clear` | 清空上下文 |
| `/commit` | 创建 Git commit |
| `/review-pr` | 审查 Pull Request |
| `/workflows:plan` | 制定实现计划 |
| `/workflows:work` | 执行计划 |
| `/workflows:review` | 代码审查 |

---

## <span style="color: #9d7ed8;">🔗 相关资源</span>

- [Claude Code 官方文档](https://docs.anthropic.com/)
- [复合工程插件](https://github.com/EveryInc/compound-engineering-plugin)
- [MCP 协议规范](https://modelcontextprotocol.io/)
- [Obsidian 中使用 Claude 不错的教程](https://sspai.com/post/103119)
