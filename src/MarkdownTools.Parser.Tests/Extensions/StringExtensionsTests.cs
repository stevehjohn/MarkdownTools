using MarkdownTools.Parser.Extensions;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("", 1, null)]
        [TestCase("Steve", 1, "teve")]
        [TestCase("Steve", 5, null)]
        [TestCase("", 0, null)]
        public void Exercise_SafeSubstring_overload_1(string input, int startIndex, string expected)
        {
            Assert.That(input.SafeSubstring(startIndex), Is.EqualTo(expected));
        }

        [TestCase("", 1, 10, null)]
        [TestCase("Steve", 1, 10, "teve")]
        [TestCase("Steve", 5, 10, null)]
        [TestCase("", 0, 10, null)]
        public void Exercise_SafeSubstring_overload_2(string input, int startIndex, int length, string expected)
        {
            Assert.That(input.SafeSubstring(startIndex, length), Is.EqualTo(expected));
        }

        [TestCase("Steve", 0, 'S')]
        [TestCase("Steve", 4, 'e')]
        [TestCase("Steve", 5, '\0')]
        public void Exercise_SafeGetChar(string input, int index, char expected)
        {
        }
    }
}