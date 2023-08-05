using System;
using System.IO;

namespace MoocDownloader;

public static class Constants
{
    public static string LocalDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    #region User data

    /// <summary>
    /// Folder to store user data.
    /// </summary>
    public const string UserDataFolder = @"MoocDownloader";

    /// <summary>
    /// File where user data is stored.
    /// </summary>
    public const string UserDataFile = @"mooc_downloader.db";

    /// <summary>
    /// Path to store user data.
    /// </summary>
    public static string UserDataPath => Path.Combine(LocalDataPath, UserDataFolder);

    #endregion
}