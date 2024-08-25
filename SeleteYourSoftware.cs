using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation_Essentials_Software_Collection
{
    internal class SeleteYourSoftware
    {
        private static List<string> softwareList = new List<string>
        {
            "7-Zip",
            "VLC Media Player",
            "Deepl Translate",
            "微信",
            "QQ",
            "百度网盘",
            "QQ音乐",
            "网易云音乐",
            "WPS Office",
            "Visual Studio Code",
            "IntelliJ IDEA Community Edition",
            "PotPlayer",
            "ToDesk",
            "Minecraft Launcher"
        };

        private static Dictionary<string, string> downloadUrls = new Dictionary<string, string>
        {
            { "7-Zip", "https://www.7-zip.org/a/7z2408-x64.exe" },
            { "VLC Media Player", "https://get.videolan.org/vlc/3.0.21/win64/vlc-3.0.21-win64.exe" },
            { "Deepl Translate", "https://appdownload.deepl.com/windows/0install/DeepLSetup.exe" },
            { "微信", "https://dldir1v6.qq.com/weixin/Windows/WeChatSetup.exe" },
            { "QQ", "https://dldir1.qq.com/qqfile/qq/PCQQ9.7.1/QQ9.7.1.28940.exe" },
            { "QQ音乐", "https://dldir1v6.qq.com/music/clntupate/QQMusic_Setup_2037.exe" },
            { "网易云音乐", "https://d1.music.126.net/dmusic/NeteaseCloudMusic_Music_official_3.0.2.203023_64.exe" },
            { "WPS Office", "https://official-package.wpscdn.cn/wps/download/WPS_Setup_17857.exe" },
            { "Visual Studio Code", "https://vscode.download.prss.microsoft.com/dbazure/download/stable/fee1edb8d6d72a0ddff41e5f71a671c23ed924b9/VSCodeUserSetup-x64-1.92.2.exe" },
            { "IntelliJ IDEA Community Edition", "https://download-cdn.jetbrains.com.cn/idea/ideaIC-2024.2.0.2.exe" },
            { "PotPlayer", "https://t1.daumcdn.net/potplayer/PotPlayer/Version/Latest/PotPlayerSetup64.exe" },
            { "ToDesk", "https://dl.todesk.com/windows/ToDesk_Setup.exe" },
            { "Minecraft Launcher", "https://launcher.mojang.com/download/MinecraftInstaller.exe" }
        };

        private static List<bool> selectedStatus = new List<bool>(new bool[softwareList.Count]);
        private static int selectedIndex = 0;

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("使用向上和向下箭头键（↑ / ↓）来选择软件，Enter 键切换选择，Ctrl+I 键下载安装所选软件。");
            PrintSoftwareList();

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    PrintSoftwareList();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = Math.Min(softwareList.Count - 1, selectedIndex + 1);
                    PrintSoftwareList();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    selectedStatus[selectedIndex] = !selectedStatus[selectedIndex];
                    PrintSoftwareList();
                }
                else if (key.Key == ConsoleKey.I && key.Modifiers == ConsoleModifiers.Control)
                {
                    RunSelectedSoftware();
                    break;
                }
            }
        }

        private static void PrintSoftwareList()
        {
            Console.Clear();
            Console.WriteLine("使用向上和向下箭头键（↑ / ↓）来选择软件，Enter 键切换选择，Ctrl+I 键下载安装所选软件。");

            for (int i = 0; i < softwareList.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("> ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("  ");
                }

                if (selectedStatus[i])
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[X] ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("[ ] ");
                }

                Console.WriteLine(softwareList[i]);
            }
        }

        private static void RunSelectedSoftware()
        {
            bool anySelected = selectedStatus.Any(status => status);

            if (!anySelected)
            {
                Console.Clear();
                Console.WriteLine("未选择任何软件，即将退出。");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey(true);
                return;
            }

            Console.Clear();
            Console.WriteLine("正在安装所选软件，中途可能需要在另一个安装窗口中处理部分安装进程，在安装完成后下一个安装进程将自动拉起，请耐心等待...");

            for (int i = 0; i < softwareList.Count; i++)
            {
                if (selectedStatus[i])
                {
                    string softwareName = softwareList[i];
                    string downloadUrl = downloadUrls[softwareName];
                    Console.WriteLine($"正在安装 {softwareName}");
                    DownloadAndInstall.DownloadAndInstallSoftware(downloadUrl);
                }
            }
        }
    }
}
