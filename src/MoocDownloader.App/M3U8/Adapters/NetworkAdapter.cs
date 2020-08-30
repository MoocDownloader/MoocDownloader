using System;
using System.IO;
using System.Net;

namespace MoocDownloader.App.M3U8.Adapters
{
    internal class NetworkAdapter : Adapter
    {
        public Uri Uri { get; }

        public NetworkAdapter(string uriString)
            : this(new Uri(uriString ?? throw new ArgumentNullException("uriString"), UriKind.Absolute))
        {
        }

        public NetworkAdapter(Uri uri)
        {
            Uri = (uri ?? throw new ArgumentNullException("uri"));
            if (!Uri.IsAbsoluteUri)
            {
                throw new UriFormatException("Invalid URI string. Required an absolute URI.");
            }
        }

        protected override Stream CreateStream()
        {
            var httpWebRequest = WebRequest.CreateHttp(Uri);
            httpWebRequest.Method    = "GET";
            httpWebRequest.UserAgent = Configuration.Default.UserAgent;
            httpWebRequest.Timeout   = Configuration.Default.RequestTimeout;
            return httpWebRequest.GetResponse().GetResponseStream();
        }
    }
}