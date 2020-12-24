using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var storage = new Mock<IEmpStore>();
            var controller = new EmployeeController(storage.Object);

            controller.DeleteEmployee(1);
            storage.Verify(r => r.DeleteEmployee(1));
        }
        
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb2()
        {
            var storage = new Mock<IEmpStore>();
            var controller = new EmployeeController(storage.Object);
            var result = controller.DeleteEmployee(1);
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}