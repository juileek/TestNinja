using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class HouseKeeperTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator ;
        private Mock<IEmailSenderBase> _emailSender ;
        private Mock<HousekeeperService.IXtraMessageBox>_messageBox;
        private HousekeeperService _service;
        private readonly DateTime _statementDate = DateTime.Today;
        private HousekeeperService.Housekeeper _housekeeper;
        private  string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork  = new Mock<IUnitOfWork>();
            _housekeeper = new HousekeeperService.Housekeeper{ Email = "a",FullName = "b", Oid = 1, StatementEmailBody = "c"};
            _unitOfWork.Setup(r => r.Query<HousekeeperService.Housekeeper>()).Returns(
                new List<HousekeeperService.Housekeeper>
                {
                    _housekeeper
                    
                }.AsQueryable());
            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(s => s.SaveStatement(_housekeeper.Oid,_housekeeper.FullName,_statementDate))
                .Returns(() => _statementFileName);
            
            _emailSender = new Mock<IEmailSenderBase>();
            _messageBox = new Mock<HousekeeperService.IXtraMessageBox>();
           
           
            _service = new HousekeeperService(_unitOfWork.Object, 
            _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
        }
        
        [Test]
        public void SendStatementEmail_WhenCalled_GenerteStatement()
        {
           _service.SendStatementEmails(_statementDate);
           _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid,_housekeeper.FullName,_statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmail_HouseKeepersEmailStrings_ShouldNotGeneratorStatement(string emaiString)
        {
            _housekeeper.Email =emaiString;
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid,_housekeeper.FullName,_statementDate),Times.Never());
        }
        
        [Test]
        public void SendStatementEmail_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailStatement(string fileName)
        {
            _statementFileName = fileName;
            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();
            
            _service.SendStatementEmails(_statementDate);
            
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(),It.IsAny<string>(), HousekeeperService.MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(s => s.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }
        
        private void VerifyEmailSent()
        {
            _emailSender.Verify(es=>es.EmailFile(
                _housekeeper.Email, 
                _housekeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }
    }
}