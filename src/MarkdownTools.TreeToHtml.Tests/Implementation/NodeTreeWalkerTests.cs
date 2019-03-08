using MarkdownTools.Models;
using MarkdownTools.TreeToHtml.Implementation;
using NUnit.Framework;
using System;

namespace MarkdownTools.TreeToHtml.Tests.Implementation
{
    [TestFixture]
    public class NodeTreeWalkerTests
    {
        private INodeTreeWalker _walker;

        [SetUp]
        public void SetUp()
        {
            _walker = new NodeTreeWalker();
        }

        [TestCase(Theme.Dark)]
        [TestCase(Theme.Light)]
        [TestCase(Theme.Custom, "TestFiles\\CustomTheme.html")]
        public void Can_load_themes(Theme theme, string customTheme = null)
        {
            _walker.LoadTree(new Node
                             {
                                 Type = NodeType.Root
                             });

            Assert.DoesNotThrow(() => _walker.ToHtml(theme, customTheme));
        }

        [Test]
        public void Throws_argument_exception_when_custom_theme_is_null()
        {
            _walker.LoadTree(new Node
                             {
                                 Type = NodeType.Root
                             });

            Assert.Throws<ArgumentNullException>(() => _walker.ToHtml(Theme.Custom));
        }
    }
}