using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation
{
    public interface IMarkdownParser
    {
        Node Parse(string markdown);
    }
}