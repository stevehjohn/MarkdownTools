using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MarkdownTools.Models
{
    public class Node
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType Type { get; set; }
        public IDictionary<string, string> MetaData { get; set; }
        public string Content { get; set; }
        public string RawContent { get; set; }
        public IList<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}