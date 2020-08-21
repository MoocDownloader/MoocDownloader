namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Cookie model
    /// </summary>
    public class CookieModel
    {
        public long?  CreationTime { get; set; }
        public long?  Expiry       { get; set; }
        public string Host         { get; set; }
        public bool?  IsDomain     { get; set; }
        public bool?  IsHttpOnly   { get; set; }
        public bool?  IsSecure     { get; set; }
        public bool?  IsSession    { get; set; }
        public long?  LastAccessed { get; set; }
        public string Name         { get; set; }
        public string Path         { get; set; }
        public string RawHost      { get; set; }
        public string Value        { get; set; }
    }
}