using System;
using System.IO;

namespace MoocDownloader.App.M3U8.Adapters
{
    internal interface IAdapter : IDisposable
    {
        bool IsConnected { get; }

        Stream Connect();
    }
}