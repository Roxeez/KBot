using System.Collections.Generic;

namespace Spark.Database.Reader
{
    public class TextContent : TextRegion
    {
        public TextContent(IEnumerable<TextLine> lines) : base(lines)
        {
        }
    }
}