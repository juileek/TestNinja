using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class DemeritPointaCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;

        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(90,5)]
        [TestCase(95, 6)]
        [TestCase(300, 47)]
        public void CalculatorDemeritPoints_SpeedAndSpeedLimit_ReturnsDemeritPoints(int speed, int expected)
        {
           var result =  _demeritPointsCalculator.CalculateDemeritPoints(speed);
           
           Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        [TestCase(400)]
        public void CalculatorDemeritPoints_SpeedOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(60,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(80,3)]
        [TestCase(75, 2)]
        public void CalculatorDemeritPoints_WhenCalled_ReturnsDemeritPoints(int speed, int expected)
        {
            Assert.That(_demeritPointsCalculator.CalculateDemeritPoints(speed), Is.EqualTo(expected));
        }
    }
}