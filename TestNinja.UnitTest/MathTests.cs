using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;
        
        [SetUp]
        public void SetUp()
        {
         _math = new Math();
        }
        
        [Test]
      //  [Ignore("Becoz I wanted to ")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
           var result = _math.Add(952, 48);
            
           Assert.That(result, Is.EqualTo(1000));
          
        }

        [Test]
        [TestCase(10,6,10)]
        [TestCase(10,20,20)]
        [TestCase(4,4,4)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expected)
        {
           var result = _math.Max(a, b);
            
            Assert.That(result,Is.EqualTo(expected));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterrThanZero_RetrunsOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);
            
            /*Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));*/
            
            
            Assert.That(result, Is.EquivalentTo(new[]{ 1, 3, 5}));
        }
       
    }
}