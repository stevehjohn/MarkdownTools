using MarkdownTools.TreeToHtml.Implementation;
using NUnit.Framework;

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
    }
}