
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public  class FizzBizzTests
    {
        [Test]
        [TestCase(9, "Fizz")]
        [TestCase(10, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(16, "16")]
        public  void GetOutput_IsDivisible_ReturnsExpectedString(int num, string expected)
        {
           var result =  FizzBuzz.GetOutput(num);
           
           Assert.That(result, Is.EqualTo(expected));
        }

      
        
        
    }
}