using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MoocDownloader.App.M3U8
{
    /// <summary>
    /// M3U8 parser.
    /// </summary>
    public class M3U8Parser
    {
        private readonly string _m3u8;

        /// <summary>
        /// Construct a m3u8 parser.
        /// </summary>
        /// <param name="m3u8">m3u8 text.</param>
        /// <returns>m3u8 parser.</returns>
        public M3U8Parser(string m3u8)
        {
            _m3u8 = m3u8;
        }

        /// <summary>
        /// Parse the m3u8 text as m3u8 info.
        /// </summary>
        /// <returns>m3u8 info.</returns>
        public List<M3U8Media> Parse()
        {
            if (string.IsNullOrEmpty(_m3u8)) // m3u8 is nothing.
            {
                return null;
            }

            var lines = _m3u8.Replace("\r", "").Split('\n');

            if (!lines.Any()) // incorrect m3u8 format
            {
                return null;
            }

            if (lines.FirstOrDefault() != "#EXTM3U")
            {
                throw new InvalidOperationException("M3U8 playlist format is incorrect");
            }

            var mediaList     = new List<M3U8Media>();
            var mediaDetected = false;

            for (var i = 1; i < lines.Length; i++)
            {
                var media = new M3U8Media();
                var line  = lines[i];
                if (line.StartsWith("#"))
                {
                    var lineData = line.Substring(1);

                    var split = lineData.Split(':');

                    var name   = split[0];
                    var suffix = split[1];

                    if (name == "EXT-X-MEDIA")
                    {
                        mediaDetected = true;
                        media         = new M3U8Media();
                    }

                    var attributes = suffix.Split(',');
                    foreach (var item in attributes)
                    {
                        var keyvalue = item.Split('=');
                        if (keyvalue.Any())
                            switch (keyvalue[0])
                            {
                                case "TYPE":
                                    media.Type = keyvalue[1].Trim('"');
                                    break;
                                case "NAME":
                                    media.Name = keyvalue[1].Trim('"');
                                    break;
                                case "BANDWIDTH":
                                    media.Bandwidth = long.Parse(keyvalue[1], CultureInfo.InvariantCulture);
                                    break;
                                case "RESOLUTION":
                                    media.Resolution = new Resolution();

                                    var size = keyvalue[1].Split('x');

                                    if (int.TryParse(size[0], out var width))
                                    {
                                        media.Resolution.Width = width;
                                    }

                                    if (int.TryParse(size[1], out var height))
                                    {
                                        media.Resolution.Height = height;
                                    }

                                    break;
                                case "CODECS":
                                    media.Codecs = keyvalue[1].Trim('"');
                                    break;
                                case "VIDEO":
                                    media.Video = keyvalue[1].Trim('"');
                                    break;
                            }
                    }
                }
                else
                {
                    if (mediaDetected)
                    {
                        media.Url     = line;
                        mediaDetected = false;
                        mediaList.Add(media);
                    }
                }
            }

            return mediaList;
        }

        /// <summary>
        /// Create a m3u8 parser.
        /// </summary>
        /// <param name="m3u8">m3u8 text.</param>
        /// <returns>m3u8 parser.</returns>
        public static M3U8Parser Create(string m3u8)
        {
            return new M3U8Parser(m3u8);
        }
    }

    public class M3U8Media
    {
        public string     Type       { get; set; }
        public string     Name       { get; set; }
        public long       Bandwidth  { get; set; }
        public Resolution Resolution { get; set; }
        public string     Codecs     { get; set; }
        public string     Video      { get; set; }
        public string     Url        { get; set; }
    }

    public class Resolution
    {
        public int Width  { get; set; }
        public int Height { get; set; }
    }
}