using MoocDownloader.App.M3U8.Adapters;
using MoocDownloader.App.M3U8.AttributeReaders;
using MoocDownloader.App.M3U8.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace MoocDownloader.App.M3U8
{
    public class M3UFileReader : IDisposable
    {
        private M3UFileInfo cache;
        private readonly IAdapter adapter;
        private readonly IReadOnlyList<IAttributeReader> attributeReaders;

        private M3UFileReader()
        {
            attributeReaders = InitAttributeReaders();
        }

        public M3UFileReader(FileInfo file)
            : this()
        {
            adapter = new FileAdapter(file);
        }

        public M3UFileReader(string text)
            : this()
        {
            adapter = new TextAdapter(text);
        }

        public M3UFileReader(Stream stream)
            : this()
        {
            adapter = new StreamAdapter(stream);
        }

        public M3UFileReader(Uri uri)
            : this()
        {
            adapter = new NetworkAdapter(uri);
        }

        private static IReadOnlyList<IAttributeReader> InitAttributeReaders()
        {
            return new List<IAttributeReader>()
            {
                new EndListAttributeReader(),
                new DurationAttributeReader(),
                new AllowCacheAttributeReader(),
                new KeyAttributeReader(),
                new MediaAttributeReader(),
                new PlaylistTypeAttributeReader(),
                new ProgramDateTimeAttributeReader(),
                new SequenceAttributeReader(),
                new StreamAttributeReader(),
                new VersionAttributeReader()
            };
        }

        public void Dispose()
        {
            adapter?.Dispose();
        }

        public M3UFileInfo Read()
        {
            if (cache != null)
                return cache;

            using (var reader = new LineReader(adapter))
            {
                if (!reader.MoveNext())
                    throw new InvalidDataException("Invalid M3U file.");
                if (!string.Equals(reader.Current?.Trim(), "#EXTM3U"))
                    throw new InvalidDataException("Missing M3U header.");

                var fileInfo = new M3UFileInfo();
                while (reader.MoveNext())
                {
                    var flag = false;
                    foreach (var attributeReader in attributeReaders)
                    {
                        flag = attributeReader.Read(reader, fileInfo);
                        if (flag)
                            break;
                    }

                    if (flag)
                        break;
                }

                return cache = fileInfo;
            }
        }
    }
}