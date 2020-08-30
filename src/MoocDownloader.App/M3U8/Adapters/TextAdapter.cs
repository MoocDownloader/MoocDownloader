using System;
using System.IO;

namespace MoocDownloader.App.M3U8.Adapters
{
    internal class TextAdapter : Adapter
    {
        public string Text { get; }

        public TextAdapter(string text)
        {
            var str = text;

            Text = str ?? throw new ArgumentNullException(nameof(text));
        }

        protected override Stream CreateStream()
        {
            return new MemoryStream(Configuration.Default.Encoding.GetBytes(Text), false);
        }
    }
}