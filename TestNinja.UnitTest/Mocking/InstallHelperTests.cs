using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class InstallHelperTests
    {
        private Mock<IInstallerRepo> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IInstallerRepo>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloaderInstaller_DownloadFails_ReturnsFalse()
        {
            _fileDownloader.Setup(f => f.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void DownloaderInstaller_DownloadSuccess_ReturnsTrue()
        {
          var result = _installerHelper.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.True);
        }
    }
    
}