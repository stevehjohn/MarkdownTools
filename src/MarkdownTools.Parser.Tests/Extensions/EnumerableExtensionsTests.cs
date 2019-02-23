using MarkdownTools.Parser.Extensions;
using NUnit.Framework;
using System.Linq;

namespace MarkdownTools.Parser.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void IndexOf_correctly_identifies_position()
        {
            var items = new[] { 0, 1, 2, 3, 4 };

            Assert.That(items.IndexOf(i => i == 2), Is.EqualTo(2));
            Assert.That(items.IndexOf(i => i == 5), Is.EqualTo(-1));
        }

        [Test]
        public void GetRange_returns_correct_items()
        {
            var items = new[] { 0, 1, 2, 3, 4 };

            var range = items.GetRange(2, 2).ToList();

            Assert.That(range[0], Is.EqualTo(2));
            Assert.That(range[1], Is.EqualTo(3));
        }

        [Test]
        public void RemoveRange_removes_correct_items()
        {
            var items = new[] { 0, 1, 2, 3, 4 };

            var newList = items.RemoveRange(2, 2).ToList();

            Assert.That(newList.Count, Is.EqualTo(3));
            Assert.That(newList[0], Is.EqualTo(0));
            Assert.That(newList[1], Is.EqualTo(1));
            Assert.That(newList[2], Is.EqualTo(4));
        }
    }
}