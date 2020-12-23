using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IInstallerRepo _installerRepo;

        public InstallerHelper(IInstallerRepo installerRepo)
        {
            _installerRepo = installerRepo;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _installerRepo.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}