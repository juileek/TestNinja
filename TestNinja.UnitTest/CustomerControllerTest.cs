using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class CustomerControllerTest
    {
        private CustomerController _controller;

        
        [SetUp]
        public void SetUp()
        {
            _controller = new CustomerController();
        }
        
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var result = _controller.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
        }
        
        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var result = _controller.GetCustomer(5);
            
            Assert.That(result,  Is.TypeOf<Ok>());
        }
        
    }
}