using MarkdownTools.Parser.Extensions;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("", 1, null)]
        [TestCase("Block", 1, "lock")]
        [TestCase("Steve", 5, null)]
        [TestCase("", 0, null)]
        public void Exercise_SafeSubstring_overload_1(string input, int startIndex, string expected)
        {
            Assert.That(input.SafeSubstring(startIndex), Is.EqualTo(expected));
        }

        [TestCase("", 1, 10, null)]
        [TestCase("Block", 1, 10, "lock")]
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

        [TestCase("", null)]
        [TestCase("A string", null)]
        [TestCase("A string\n", null)]
        [TestCase("A string\nA new line", "A new line")]
        public void NewLine_returns_expected_value(string input, string expected)
        {
            input = input.Replace("\n", Environment.NewLine);

            Assert.That(input.NextLine(), Is.EqualTo(expected));
        }

        [TestCase("", "Steve", false)]
        [TestCase("Bert", "Steve", false)]
        [TestCase("Steve", "Steve", true)]
        [TestCase("   Steve", "Steve", true)]
        [TestCase("\tSteve", "Steve", true)]
        [TestCase("\t\t  Steve", "Steve", true)]
        public void StartsWithExcludingWhitespace_returns_expected_value(string input, string parameter, bool expected)
        {
            Assert.That(input.StartsWithExcludingWhitespace(parameter), Is.EqualTo(expected));
        }
    }
}