using MarkdownTools.Models;
using MarkdownTools.Parser.Implementation;
using MarkdownTools.Parser.Implementation.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;

namespace MarkdownTools.Parser.Tests.Implementation
{
    [TestFixture]
    public class MarkdownParserTests
    {
        private IMarkdownParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = MarkdownParserBuilder.GetParserWithAllEvaluators();
        }

        [TestCase("WIP")]
        public void Parse(string sourceFile)
        {
            var markdown = File.ReadAllText($"TestFiles\\Inputs\\{sourceFile}.md");

            var result = _parser.Parse(markdown);

            var resultJson = JsonConvert.SerializeObject(result,
                                                         Formatting.Indented,
                                                         new JsonSerializerSettings
                                                         {
                                                             NullValueHandling = NullValueHandling.Ignore
                                                         });

            var expectedJson = File.ReadAllText($"TestFiles\\Outputs\\{sourceFile}.json");

            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void Parser_sorts_evaluators_in_precedence_order()
        {
            var attributes = ((MarkdownParser) _parser).Evaluators;

            Assert.That(attributes[0], Is.AssignableTo(typeof(CodeBlockEvaluator)));
            Assert.That(attributes[1], Is.AssignableTo(typeof(HeadingEvaluator)));

            Assert.That(attributes[attributes.Count - 2], Is.AssignableTo(typeof(WhitespaceEvaluator)));
            Assert.That(attributes[attributes.Count - 1], Is.AssignableTo(typeof(TextEvaluator)));
        }

        [Test]
        public void Ignores_ValidPreviousNodeSequenceAttribute_at_start_of_document()
        {
            var result = _parser.Parse("> BlockQuote");

            Assert.That(result.Children[0].Type == NodeType.Blockquote);
        }
    }
}