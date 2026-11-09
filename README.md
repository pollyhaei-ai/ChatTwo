# Chat 2（台服 / Dalamud API 13）

台灣版《最終幻想XIV》用的 [Infiziert90/ChatTwo](https://github.com/Infiziert90/ChatTwo)（作者 Infi、Anna）。這個分支給 [cycleapple/XIVTCLauncher](https://github.com/cycleapple/XIVTCLauncher) 搭配 [yanmucorp/Dalamud](https://github.com/yanmucorp/Dalamud) **API 13 / .NET 9**。

它把遊戲內建的聊天視窗換成可分頁、可調顏色與字體的視窗。

**不要裝到官方 Dalamud API 14／15**（國際服 XIVLauncher）。這個組建只給台服。這不是官方插件，授權沿用上游 [MIT](LICENCE)。

## 這個版本

請用 **1.42.2**。不要用 1.42.0 或 1.42.1。

1.42.0 把頻道代碼解錯，也會改寫共用的聊天資料庫。1.42.1 修正了代碼，但分頁的頻道清單仍然是空的：舊版設定存在 `ChatCodes`，1.42.0 不再讀這個欄位，設定畫面看起來就像每個頻道都被關掉，所以聊天區還是空白。1.42.2 會把 `ChatCodes` 讀回來；若那個清單已經被存檔清掉，就把空白的分頁改成全部頻道都開。

- 對準 `Dalamud.NET.Sdk` 13.1.0 與 .NET 9。
- 台服八個世界（4028 伊弗利特～4035 泰坦）在用戶端資料裡 `IsPublic` 是 false。只靠這個旗標時，密語對象的世界清單是空的，右鍵玩家也不會出現組隊、好友、黑名單。現在這些世界會一併放行。
- 右鍵玩家名稱有 **發送組隊邀請**。同服（或還不知道自己所在世界時）送出 `/invite 名字`；人在別的世界時送出 `/invite 名字@世界`。這是聊天框文字指令，跟好友／黑名單同一條路，不呼叫尚未在台服核對過的 `InfoProxyPartyInvite`。
- 同一個選單有 **傳送好友申請**（`/friendlist add 名字@世界`）和 **封鎖功能 → 加入黑名單**（`/blist add 名字@世界`）。
- Dalamud 介面語言代碼 `tw` 會載入繁體中文。請在 `/xlsettings` 把介面語言設成 **繁體中文**。維持預設時，選單可能是英文。

跨服的 `/invite 名字@世界` 由遊戲自己解析。若該語法不被接受，遊戲會顯示錯誤，不會改走原生組隊邀請。

## 安裝

1. 在 XIVTCLauncher 啟用 Dalamud，進過一次遊戲讓資料夾出現，然後**關閉遊戲**。
2. 下載 [dist/ChatTwo-api13-tw-1.42.2.zip](dist/ChatTwo-api13-tw-1.42.2.zip)。解開後是一個 `ChatTwo` 資料夾，含 `ChatTwo.dll`、`ChatTwo.json`，以及一起載入的組件。
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

## 聊天區是空的、看起來每個頻道都被關掉

1.42.2 啟動時會做兩件事：

- 設定檔裡如果還有舊的 `ChatCodes`，就把它複製成分頁的頻道清單。
- 某個分頁的頻道清單是空的，就把每個頻道打開，並寫回設定檔。

請先關掉遊戲再換 dll。換好之後進遊戲，分頁設定裡的頻道應該是勾選的，說話、小隊、喊話會再出現。若你想要每個分頁只看一種頻道，到分頁設定裡重新勾選即可。

設定檔與聊天記錄不在插件 dll 那個資料夾。常見位置：

```text
%AppData%\FFXIVSimpleLauncher\pluginConfigs\ChatTwo.json
%AppData%\FFXIVSimpleLauncher\pluginConfigs\ChatTwo\chat-sqlite.db
```

若帳號用了自己的 Dalamud 設定檔，改到對應的 `pluginConfigs`。插件日誌會印出實際的資料庫路徑，可在 `/xllog` 搜尋 `Chat database`。

1.42.0 若有留下 `chat-sqlite.db-migration-bak`，關掉遊戲後把該備份複製成 `chat-sqlite.db`（先把現在的檔案改名留著）可以回到升級前的紀錄。沒有備份、而且紀錄已經被刪掉的話，舊內容找不回來。不要刪掉整個 `pluginConfigs\ChatTwo`，除非你確定連設定一起不要。

1.42.0 還把頻道跟玩家關係包在同一個數字裡（例如 4922，真正的頻道是低 7 位元的 58），1.42.1 之後已改回只取低 7 位元。

## 自行編譯

`Dalamud.NET.Sdk` 13.1.0 對本機的 `Dalamud.dll` 編譯。把 `DALAMUD_HOME` 指到 yanmucorp Dalamud `13.0.0.16` 裡含有 `Dalamud.dll` 的目錄（[發行檔](https://github.com/yanmucorp/Dalamud/releases) 的 `latest.7z`，或 XIVTCLauncher 的 `Injector`）。沒有設定時，SDK 會去官方啟動器的 `%AppData%\XIVLauncher\addon\Hooks\dev\`。

1. 安裝 .NET 9 SDK。`global.json` 指定 `9.0.100`，並在 9.0 功能帶內向前相容。
2. 在這個目錄執行 `dotnet build -c Release`。Linux 或 macOS 加上 `-p:EnableWindowsTargeting=true`。

專案目標是 `net9.0-windows7.0`。Release 組建會在輸出目錄產生可安裝的 zip。

## English

Unofficial Traditional Chinese adaptation of [Infiziert90/ChatTwo](https://github.com/Infiziert90/ChatTwo) by Infi and Anna, for yanmucorp Dalamud API 13 / .NET 9 and XIVTCLauncher. Do not install this on official Dalamud API 14 or 15. Upstream remains MIT.

1.42.2 is the build to install. 1.42.0 masked nothing, so packed chat codes never matched a tab, and it could delete the shared database. 1.42.1 masks with `0x7F` but still ignored the older `ChatCodes` filter, so every tab loaded with no channels selected and the log stayed blank. 1.42.2 reads `ChatCodes` again. A tab whose channel list is already empty is switched to every channel and saved. Re-check the tab filters if you want one channel per tab.

The config file is `%AppData%\FFXIVSimpleLauncher\pluginConfigs\ChatTwo.json`. The database is `%AppData%\FFXIVSimpleLauncher\pluginConfigs\ChatTwo\chat-sqlite.db`. `/xllog` prints `Chat database:` with the real path.

Taiwan worlds 4028–4035 are included for tells and the player menu. Right-click **發送組隊邀請** sends `/invite Name` on the same world and `/invite Name@World` otherwise. Friend requests and the blacklist use `/friendlist add` and `/blist add`. Native `InfoProxyPartyInvite` is not used for those worlds.

The shared database is `%AppData%\FFXIVSimpleLauncher\pluginConfigs\ChatTwo\chat-sqlite.db`. If `chat-sqlite.db-migration-bak` is still there, close the game and copy it over `chat-sqlite.db` to restore the pre-migration file.

Set Dalamud’s UI language to Traditional Chinese (`tw`) in `/xlsettings` so `Language.zh-Hant` loads. Put `ChatTwo.dll` and `ChatTwo.json` in `%AppData%\FFXIVSimpleLauncher\Dalamud\Config\devPlugins\ChatTwo\` and enable the plugin in `/xldev`.
