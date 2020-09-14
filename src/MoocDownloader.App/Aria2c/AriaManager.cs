using MoocDownloader.App.Aria2c.Attributes;
using MoocDownloader.App.Aria2c.JsonRpc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c
{
    public class AriaManager
    {
        private const string UAER_AGENT =
            @"Moozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36";

        private const string REFERER = @"http://www.icourse163.org";

        private readonly JsonRpcHttpClient RpcClient;

        public AriaManager(string rpcUrl = "http://localhost:6800/jsonrpc")
        {
            RpcClient = new JsonRpcHttpClient(new Uri(rpcUrl));
        }

        public static void Start(string aria)
        {
            const string args = @"--enable-rpc=true --rpc-allow-origin-all=true";
            var process = new Process
            {
                StartInfo =
                {
                    FileName               = aria, // command
                    Arguments              = args, // arguments
                    CreateNoWindow         = true,
                    UseShellExecute        = false, // do not create a window.
                    RedirectStandardInput  = true,  // redirect input.
                    RedirectStandardOutput = true,  // redirect output.
                    RedirectStandardError  = true,  // redirect error.
                },
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (sender, eventArgs) => { Console.WriteLine(eventArgs.Data); };
            process.ErrorDataReceived  += (sender, eventArgs) => { Console.WriteLine(eventArgs.Data); };

            process.Exited += (sender, eventArgs) => { };

            var result = process.Start();

            Log.Information($"启动 ARIA: {result}");
        }

        public async Task<string> AddUri(string url, string fileName, string path)
        {
            return await RpcClient.Invoke<string>(
                "aria2.addUri", new List<string> {url}, new Dictionary<string, string>
                {
                    {"user-agent", UAER_AGENT},
                    {"referer", REFERER},
                    {"check-certificate", "false"},
                    {"max-connection-per-server", "8"},
                    {"split", "16"},
                    {"max-concurrent-downloads", "16"},
                    {"min-split-size", "4M"},
                    {"disk-cache", "64M"},
                    {"out", $@"{fileName}"},
                    {"dir", $@"{path}"},
                }
            );
        }

        public async Task<string> AddMetaLink(string filePath)
        {
            var metaLinkBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
            return await RpcClient.Invoke<string>("aria2.addMetalink", metaLinkBase64);
        }

        public async Task<string> AddTorrent(string filePath)
        {
            var torrentBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath));
            return await RpcClient.Invoke<string>("aria2.addTorrent", torrentBase64);
        }

        public async Task<string> RemoveTask(string gid, bool forceRemove = false)
        {
            if (!forceRemove)
            {
                return await RpcClient.Invoke<string>("aria2.remove", gid);
            }
            else
            {
                return await RpcClient.Invoke<string>("aria2.forceRemove", gid);
            }
        }

        public async Task<string> PauseTask(string gid, bool forcePause = false)
        {
            if (!forcePause)
            {
                return await RpcClient.Invoke<string>("aria2.pause", gid);
            }
            else
            {
                return await RpcClient.Invoke<string>("aria2.forcePause", gid);
            }
        }

        public async Task<bool> PauseAllTasks()
        {
            return (await RpcClient.Invoke<string>("aria2.pauseAll")).Contains("OK");
        }

        public async Task<bool> UnpauseAllTasks()
        {
            return (await RpcClient.Invoke<string>("aria2.unpauseAll")).Contains("OK");
        }

        public async Task<string> UnpauseTask(string gid)
        {
            return await RpcClient.Invoke<string>("aria2.unpause", gid);
        }

        public async Task<AriaStatus> GetStatus(string gid)
        {
            return await RpcClient.Invoke<AriaStatus>("aria2.tellStatus", gid);
        }

        public async Task<AriaUri> GetUris(string gid)
        {
            return await RpcClient.Invoke<AriaUri>("aria2.getUris", gid);
        }

        public async Task<AriaFile> GetFiles(string gid)
        {
            return await RpcClient.Invoke<AriaFile>("aria2.getFiles", gid);
        }

        public async Task<AriaTorrent> GetPeers(string gid)
        {
            return await RpcClient.Invoke<AriaTorrent>("aria2.getPeers", gid);
        }

        public async Task<AriaServer> GetServers(string gid)
        {
            return await RpcClient.Invoke<AriaServer>("aria2.getServers", gid);
        }

        public async Task<AriaStatus> GetActiveStatus(string gid)
        {
            return await RpcClient.Invoke<AriaStatus>("aria2.tellActive", gid);
        }

        public async Task<AriaOption> GetOption(string gid)
        {
            return await RpcClient.Invoke<AriaOption>("aria2.getOption", gid);
        }


        public async Task<bool> ChangeOption(string gid, AriaOption option)
        {
            return (await RpcClient.Invoke<string>("aria2.changeOption", gid, option))
               .Contains("OK");
        }

        public async Task<AriaOption> GetGlobalOption()
        {
            return await RpcClient.Invoke<AriaOption>("aria2.getGlobalOption");
        }

        public async Task<bool> ChangeGlobalOption(AriaOption option)
        {
            return (await RpcClient.Invoke<string>("aria2.changeGlobalOption", option))
               .Contains("OK");
        }

        public async Task<AriaGlobalStatus> GetGlobalStatus()
        {
            return await RpcClient.Invoke<AriaGlobalStatus>("aria2.getGlobalStat");
        }

        public async Task<bool> PurgeDownloadResult()
        {
            return (await RpcClient.Invoke<string>("aria2.purgeDownloadResult")).Contains("OK");
        }

        public async Task<bool> RemoveDownloadResult(string gid)
        {
            return (await RpcClient.Invoke<string>("aria2.removeDownloadResult", gid))
               .Contains("OK");
        }

        public async Task<AriaVersionInfo> GetVersion()
        {
            return await RpcClient.Invoke<AriaVersionInfo>("aria2.getVersion");
        }

        public async Task<AriaSession> GetSessionInfo()
        {
            return await RpcClient.Invoke<AriaSession>("aria2.getSessionInfo");
        }

        public async Task<bool> Shutdown(bool forceShutdown = false)
        {
            if (!forceShutdown)
            {
                return (await RpcClient.Invoke<string>("aria2.shutdown")).Contains("OK");
            }
            else
            {
                return (await RpcClient.Invoke<string>("aria2.forceShutdown")).Contains("OK");
            }
        }

        public async Task<bool> SaveSession()
        {
            return (await RpcClient.Invoke<string>("aria2.saveSession")).Contains("OK");
        }
    }
}