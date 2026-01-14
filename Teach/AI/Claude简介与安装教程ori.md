---
author: RuoSaChen
tags:
  - AI
  - teach
---
![[Pasted image 20260114171254.png]]
### 1.Claude Code是什么？

>Claude Code，期初是 Anthropic 公司发布的一款 AI 编程产品，因为 Claude 的模型和工程设计更加优秀，在使用同样的模型时，在 Claude Code 中完成的代码质量，要比在 Cursor 等 AI IDE 工具中完成的质量更高。
>
>Claude Code**更像是一个Agent

> What is Agent
> 从更广义的AI背景看，Agent是一个非常古老的概念。任何能进行自我决策、与环境交互，并试图optimize reward（优化奖励）的系统，都可以被称为Agent。
> 
> 但是在今天大预言模型的背景下，我个人理解为：
> 
> Agent 在一个「环境」里，按照我们的要求使用「工具」完成「任务」。他可以写代码，可以执行终端命令，可以访问浏览器等等。同时还对我们做过的任务有过「记忆」，是一个真正的助手，而不至于 Copilot。


### 2.Claude 的运行环境

>- **环境**即是本地文件环境，这其实也是当前最适配 Agent 的环境；


### 3.Claude安装部署


> 1 .按照以下教程安装
https://docs.ksyun.com/documents/44928
需要node 才能安装，记得在安装以后在cmd中运行一下 Claude ，这样才能在指定目录下生成.claude.json 文件，才能进行第二步骤的配置


>2.修改本地配置为下述
```
C:\\Users\\你的用户名\\.claude\\settings.json
{
    "env": {
        "DISABLE_TELEMETRY": "1",
        "DISABLE_ERROR_REPORTING": "1",
        "CLAUDE_CODE_DISABLE_NONESSENTIAL_TRAFFIC": "1",
        "ANTHROPIC_DEFAULT_HAIKU_MODEL": "glm-4.5-air",
        "ANTHROPIC_DEFAULT_SONNET_MODEL": "glm-4.7",
        "ANTHROPIC_DEFAULT_OPUS_MODEL": "glm-4.7",
        "ANTHROPIC_BASE_URL": "https://open.bigmodel.cn/api/anthropic",
        "ANTHROPIC_AUTH_TOKEN": "b5b43e4de5c244b6b5266592e0b2a222.gUk6xtynRG25aui2",
        "API_TIMEOUT_MS": "3000000",
        "MCP_TOOL_TIMEOUT": "30000"
    },
    "alwaysThinkingEnabled": false
}
 
配置mcp

C:\\Users\\你的用户名\\.claude.json

"mcpServers": {
    "zai-mcp-server": {
      "type": "stdio",
      "command": "npx",
      "args": [
        "-y",
        "@z_ai/mcp-server"
      ],
      "env": {
        "Z_AI_API_KEY": "b5b43e4de5c244b6b5266592e0b2a222.gUk6xtynRG25aui2",
        "Z_AI_MODE": "ZHIPU"
      }
    },
    "firecrawl-mcp": {
      "type": "http",
      "url": "http://3.38.179.98:25152/mcp",
      "headers": {
        "Authorization": "Bearer LT123456@"
      }
    }
}

```



>3 . 如果要跳过登录需要在 .claude.json中添加

```

  "hasCompletedOnboarding": true,
```


>4 . 到你想使用Claude的文件夹下打开cmd命令行，这个文件夹就是Claude的项目根目录了


>5. 如果在VS中使用的话
![[Pasted image 20260114170123.png]]