using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepo> _videoRepo;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepo = new Mock<IVideoRepo>();
            _service = new VideoService(_fileReader.Object, _videoRepo.Object);
        }
        
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsErrorMessage()
        {
           _fileReader.Setup(fr => fr.Read("video.txt")).Returns(" ");
           
           var result = _service.ReadVideoTitle();
           
            Assert.That(result,Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideoAsCSV_AllVideosProcessed_ReturnEmptyString()
        {
            _videoRepo.Setup(r => r.GetUnProcesswedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();
            
            Assert.That(result , Is.EqualTo(""));
        }
        
        [Test]
        public void GetUnprocessedVideoAsCSV_AfewVideosUnProcessed_ReturnAStringWithId()
        {
            _videoRepo.Setup(r => r.GetUnProcesswedVideos()).Returns(new List<Video>
            {
                new Video{ Id = 1},
                new Video{ Id = 2},
                new Video{ Id = 3},
                new Video{ Id = 4},
                new Video{ Id = 5},

            });

            var result = _service.GetUnprocessedVideosAsCsv();
            
            Assert.That(result , Is.EqualTo("1,2,3,4,5"));
        }
    }
}