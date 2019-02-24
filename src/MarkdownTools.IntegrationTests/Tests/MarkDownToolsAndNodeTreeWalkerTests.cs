using MarkdownTools.Parser.Implementation;
using MarkdownTools.TreeToHtml.Implementation;
using NUnit.Framework;
using System.IO;

namespace MarkdownTools.IntegrationTests.Tests
{
    [TestFixture]
    public class MarkDownToolsAndNodeTreeWalkerTests
    {
        [TestCase("WIP")]
        [TestCase("TR Delta Sync Process Illustration")]
        public void Given_known_markdown_input_generates_desired_html(string filename)
        {
            var markdown = File.ReadAllText($"TestFiles\\Inputs\\{filename}.md");

            var parser = MarkdownParserBuilder.GetParserWithAllEvaluators();

            var nodes = parser.Parse(markdown);

            var htmlGenerator = new NodeTreeWalker();

            htmlGenerator.LoadTree(nodes);

            var html = htmlGenerator.ToHtml(Theme.Dark);

            var expected = File.ReadAllText($"TestFiles\\Outputs\\{filename}.html");

            Assert.That(html, Is.EqualTo(expected));
        }
    }
}