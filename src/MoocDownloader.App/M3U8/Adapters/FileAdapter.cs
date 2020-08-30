using System;
using System.IO;

namespace MoocDownloader.App.M3U8.Adapters
{
    internal class FileAdapter : Adapter
    {
        public FileInfo File { get; }

        public FileAdapter(string fileName)
            : this(new FileInfo(fileName ?? throw new ArgumentNullException("fileName")))
        {
        }

        public FileAdapter(FileInfo file)
        {
            File = (file ?? throw new ArgumentNullException("file"));
            if (!File.Exists)
            {
                throw new FileNotFoundException("File not found.", File.FullName);
            }
        }

        protected override Stream CreateStream()
        {
            return File.OpenRead();
        }
    }
}