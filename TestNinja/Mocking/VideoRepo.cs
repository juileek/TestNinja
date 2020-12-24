using System.Collections.Generic;
using System.Linq;
namespace TestNinja.Mocking
{
    public interface IVideoRepo
    {
        IEnumerable<Video> GetUnProcesswedVideos();
    }

    public class VideoRepo : IVideoRepo
    {
        public IEnumerable<Video> GetUnProcesswedVideos()
        {
            using (var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                        where !video.IsProcessed
                        select video).ToList();

                return videos;
            }
            
        }
    }
}