# Chat 2（台服 / Dalamud API 13）

台灣版《最終幻想XIV》用的 [Infiziert90/ChatTwo](https://github.com/Infiziert90/ChatTwo)（作者 Infi、Anna）。這個分支給 [cycleapple/XIVTCLauncher](https://github.com/cycleapple/XIVTCLauncher) 搭配 [yanmucorp/Dalamud](https://github.com/yanmucorp/Dalamud) **API 13 / .NET 9**。

它把遊戲內建的聊天視窗換成可分頁、可調顏色與字體的視窗。

**不要裝到官方 Dalamud API 14／15**（國際服 XIVLauncher）。這個組建只給台服。這不是官方插件，授權沿用上游 [MIT](LICENCE)。

## 這個版本

請用 **1.42.0**（上游 1.41.2 的台服移植）。

- 對準 `Dalamud.NET.Sdk` 13.1.0 與 .NET 9。
- 台服八個世界（4028 伊弗利特～4035 泰坦）在用戶端資料裡 `IsPublic` 是 false。只靠這個旗標時，密語對象的世界清單是空的，右鍵玩家也不會出現組隊、好友、黑名單。現在這些世界會一併放行。
- 右鍵玩家名稱有 **發送組隊邀請**。同服（或還不知道自己所在世界時）送出 `/invite 名字`；人在別的世界時送出 `/invite 名字@世界`。這是聊天框文字指令，跟好友／黑名單同一條路，不呼叫尚未在台服核對過的 `InfoProxyPartyInvite`。
- 同一個選單有 **傳送好友申請**（`/friendlist add 名字@世界`）和 **封鎖功能 → 加入黑名單**（`/blist add 名字@世界`）。
- Dalamud 介面語言代碼 `tw` 會載入繁體中文。請在 `/xlsettings` 把介面語言設成 **繁體中文**。維持預設時，選單可能是英文。

跨服的 `/invite 名字@世界` 由遊戲自己解析。若該語法不被接受，遊戲會顯示錯誤，不會改走原生組隊邀請。

## 安裝

1. 在 XIVTCLauncher 啟用 Dalamud，進過一次遊戲讓資料夾出現，然後**關閉遊戲**。
2. 下載這個分支裡的 [dist/ChatTwo-api13-tw-1.42.0.zip](dist/ChatTwo-api13-tw-1.42.0.zip)。解開後是一個 `ChatTwo` 資料夾，含 `ChatTwo.dll`、`ChatTwo.json`，以及一起載入的組件。
3. 把這個資料夾放到下面的路徑。`dll` 和 `json` 要在同一層，不要再多包一層 `ChatTwo`：

```text
%AppData%\FFXIVSimpleLauncher\Dalamud\Config\devPlugins\ChatTwo\
```

帳號如果用自己的插件設定檔，改放：

```text
%AppData%\FFXIVSimpleLauncher\Profiles\<設定檔 ID>\Dalamud\devPlugins\ChatTwo\
```

4. 進遊戲後用 `/xldev` 啟用 Chat 2。同一個視窗可以重新載入。

`/chat2` 開啟設定。

## 自行編譯

`Dalamud.NET.Sdk` 13.1.0 對本機的 `Dalamud.dll` 編譯。把 `DALAMUD_HOME` 指到 yanmucorp Dalamud `13.0.0.16` 裡含有 `Dalamud.dll` 的目錄（[發行檔](https://github.com/yanmucorp/Dalamud/releases) 的 `latest.7z`，或 XIVTCLauncher 的 `Injector`）。沒有設定時，SDK 會去官方啟動器的 `%AppData%\XIVLauncher\addon\Hooks\dev\`。

1. 安裝 .NET 9 SDK。`global.json` 指定 `9.0.100`，並在 9.0 功能帶內向前相容。
2. 在這個目錄執行 `dotnet build -c Release`。Linux 或 macOS 加上 `-p:EnableWindowsTargeting=true`。

專案目標是 `net9.0-windows7.0`。Release 組建會在輸出目錄產生可安裝的 zip。

## English

Unofficial Traditional Chinese adaptation of [Infiziert90/ChatTwo](https://github.com/Infiziert90/ChatTwo) by Infi and Anna, for yanmucorp Dalamud API 13 / .NET 9 and XIVTCLauncher. Do not install this on official Dalamud API 14 or 15. Upstream remains MIT.

1.42.0 retargets 1.41.2. Taiwan worlds 4028–4035 are not flagged public in the client data; they are included for tells and for the player menu. Right-click **發送組隊邀請** sends `/invite Name` on the same world and `/invite Name@World` otherwise, through the chat box. Friend requests and the blacklist use `/friendlist add` and `/blist add` the same way. Native `InfoProxyPartyInvite` is not used for those worlds.

Set Dalamud’s UI language to Traditional Chinese (`tw`) in `/xlsettings` so `Language.zh-Hant` loads. Put `ChatTwo.dll` and `ChatTwo.json` in `%AppData%\FFXIVSimpleLauncher\Dalamud\Config\devPlugins\ChatTwo\` and enable the plugin in `/xldev`.
