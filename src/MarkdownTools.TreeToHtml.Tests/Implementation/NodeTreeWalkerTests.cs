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

        [Test]
        public void Processes_blockquote_node()
        {
            _walker.LoadTree(new Node
                             {
                                 Type = NodeType.Root,
                                 Children =
                                 {
                                     new Node
                                     {
                                         Type = NodeType.Blockquote,
                                         Content = "This is a blockquote."
                                     }
                                 }
                             });

            var result = _walker.ToHtml(Theme.Raw);

            Assert.That(result, Is.EqualTo("    <blockquote>\n        This is a blockquote.\n    </blockquote>\n".Replace("\n", Environment.NewLine)));
        }

        [Test]
        public void Processes_heading_node()
        {
            _walker.LoadTree(new Node());
        }
    }
}