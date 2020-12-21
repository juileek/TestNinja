using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class ErrorLoggerTest
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }
        
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            _logger.Log("a");
            
            Assert.That(_logger.LastError,Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_WhenCalledWithNullOrEmptyString_ThrowsArgumentNullException(string error)
        {
           Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args;};
            
            _logger.Log("a");
            
            Assert.That(id,Is.Not.EqualTo(Guid.Empty));
        }
    }
}