using MoocDownloader.App.M3U8.Adapters;
using MoocDownloader.App.M3U8.Core;
using MoocDownloader.App.M3U8.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class StreamAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-STREAM-INF", key);
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
            var source = value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                              .Select(e => KV.Parse(e, '='))
                              .ToList();
            if (fileInfo.Streams == null && source.Any())
                fileInfo.Streams = new List<M3UStreamInfo>();
            var m3UstreamInfo = new M3UStreamInfo();
            foreach (var keyValuePair in source)
            {
                var key = keyValuePair.Key;
                if (key != "BANDWIDTH")
                {
                    if (key != "PROGRAM-ID")
                    {
                        if (key != "CODECS")
                        {
                            if (key == "RESOLUTION")
                                m3UstreamInfo.Resolution = keyValuePair.Value;
                        }
                        else
                            m3UstreamInfo.Codecs = keyValuePair.Value;
                    }
                    else
                        m3UstreamInfo.ProgramId = To.Value<int>(keyValuePair.Value);
                }
                else
                    m3UstreamInfo.Bandwidth = To.Value<int>(keyValuePair.Value);
            }

            if (!reader.MoveNext())
                throw new InvalidDataException("Invalid M3U file. Missing a stream URI.");

            if (reader.Current != null)
            {
                var relativeUri = new Uri(reader.Current.Trim(), UriKind.RelativeOrAbsolute);
                if (!relativeUri.IsAbsoluteUri && !relativeUri.IsWellFormedOriginalString())
                    throw new InvalidDataException("Invalid M3U file. Include a invalid stream URI.",
                        new UriFormatException(reader.Current));

                if (!relativeUri.IsAbsoluteUri)
                {
                    var baseUri = Configuration.Default.BaseUri;
                    if (baseUri == null && reader.Adapter is NetworkAdapter adapter)
                        baseUri = new Uri(
                            adapter.Uri.GetComponents(UriComponents.SchemeAndServer | UriComponents.UserInfo,
                                UriFormat.SafeUnescaped), UriKind.Absolute);
                    if (baseUri != null)
                        m3UstreamInfo.Uri = new Uri(baseUri, relativeUri);
                }

                if (m3UstreamInfo.Uri == null)
                    m3UstreamInfo.Uri = relativeUri;
            }

            fileInfo.Streams?.Add(m3UstreamInfo);
        }
    }
}