using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _stack;
        
        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        [TestCase(null)]
        public void Push_ObjIsNull_ThrowsArgumentNullExecption(string obj)
        {
            Assert.That(() => _stack.Push(obj), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("abc")]
        public void Push_validArgument_AddsInTheList(string arg)
        {
            _stack.Push(arg);

           Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackHasFewObjects_ReturnsTopObject()
        {
            _stack.Push("abc");
            _stack.Push("def");
            _stack.Push("xyz");
            var result = _stack.Pop();
            Assert.That(result, Is.EqualTo("xyz"));
        }
        
        [Test]
        public void Pop_StackHasFewObjects_RemoveTopObject()
        {
            _stack.Push("abc");
            _stack.Push("def");
            _stack.Push("xyz");
            _stack.Pop();
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackHasFewObj_ReturnObjOnTop()
        {
            _stack.Push("abc");
            _stack.Push("def");
            _stack.Push("xyz");
            var result = _stack.Peek();
            Assert.That(result, Is.EqualTo("xyz"));
        }

        [Test]
        public void Peek_StackHasFewObj_DoesNotRemoveObjOnTop()
        {
            _stack.Push("abc");
            _stack.Push("def");
            _stack.Push("xyz");
            _stack.Peek();
            Assert.That(_stack.Count, Is.EqualTo(3));
        }
    }
}