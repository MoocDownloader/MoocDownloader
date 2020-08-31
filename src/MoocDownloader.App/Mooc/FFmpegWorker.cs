using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MoocDownloader.App.Models;

namespace MoocDownloader.App.Mooc
{
    /// <summary>
    /// FFMPEG worker.
    /// </summary>
    public class FFmpegWorker
    {
        private readonly ConcurrentQueue<CourseVideoInfo> _videoQueue;
        private readonly EventWaitHandle                  _handle;

        private bool _running = false;

        private static readonly Lazy<FFmpegWorker> _lazy = new Lazy<FFmpegWorker>(() => new FFmpegWorker());


        private FFmpegWorker()
        {
            _videoQueue = new ConcurrentQueue<CourseVideoInfo>();
            _handle     = new AutoResetEvent(false);
        }

        public void Enqueue(CourseVideoInfo courseVideoInfo)
        {
            _videoQueue.Enqueue(courseVideoInfo);
            _handle.Set();
        }

        private void Work()
        {
            _running = true;

            while (_running)
            {
                if (_videoQueue.IsEmpty)
                {
                    _handle.WaitOne();
                }

                if (_videoQueue.TryDequeue(out var courseVideoInfo))
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {

                        }
                        catch
                        {
                        }
                    }, TaskCreationOptions.LongRunning);
                }
            }
        }

        /// <summary>
        /// stop download course video.
        /// </summary>
        public void Stop()
        {
            _running = false;
            _handle.Set();
        }


        /// <summary>
        /// start download course video by ffmpeg.
        /// </summary>
        public void Start()
        {
            Work();
        }
    }
}